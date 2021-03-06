﻿using MU.DataBase;
using MuEmu.Data;
using MuEmu.Entity;
using MuEmu.Monsters;
using MuEmu.Network.Data;
using MuEmu.Network.Game;
using MuEmu.Resources;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuEmu
{
    public enum Spell : ushort
    {
        None,
        Poison,
        Meteorite,
        Lighting,
        FireBall,
        Flame,
        Teleport,
        Ice,
        Twister,
        EvilSpirit,
        Hellfire,
        PowerWave,
        AquaBeam,
        Cometfall,
        Inferno,
        TeleportAlly,
        SoulBarrier,
        EnergyBall,
        Defense,
        Falling_Slash,
        Lunge,
        Uppercut,
        Cyclone,
        Slash,
        Triple_Shot,
        Heal,
        GreaterDefense,
        GreaterDamage,
        Summon_Goblin = 30,
        Summon_StoneGolem,
        Summon_Assassin,
        Summon_EliteYeti,
        Summon_DarkKnight,
        Summon_Bali,
        Summon_Soldier,
        Decay = 38,
        IceStorm,
        Nova,
        TwistingSlash,
        RagefulBlow,
        DeathStab,
        CrescentMoonSlash,
        ManaGlaive,
        Starfall,
        Impale,
        GreaterFortitude,
        FireBreath,
        FlameofEvilMonster,
        IceArrow,
        Penetration,
        InfinityArrow,
        FireSlash = 55,
        PowerSlash,
        SpiralSlash,
        Force = 60,
        FireBurst,
        Earthshake,
        Summon,
        IncreaseCriticalDmg,
        ElectricSpike,
        ForceWave,
        Stern,
        CancelStern,
        SwellMana,
        Transparency,
        CancelTransparency,
        CancelMagic,
        ManaRays,
        FireBlast,
        PlasmaStorm = 76,
        ShadowArrow,
        FireScream,
        DrainLife = 214,
        ChainLighting,
        ElectricSurge,
        Reflex,
        Sleep = 219,
        Night,
        MagicSpeedUp,
        MagicDefenseUp,
        Sahamutt,
        Neil,
        GhostPhantom,

        RedStorm = 230,
        MagicCircle = 233,
        LightingStorm = 237,
    }
    public class Spells
    {
        private static readonly ILogger Logger = Log.ForContext(Constants.SourceContextPropertyName, nameof(ResourceCache));
        private Dictionary<Spell, SpellInfo> _spellList;
        private List<Buff> _buffs;
        private List<Spell> _newSpell = new List<Spell>();
        
        public Monster Monster { get; }
        public Player Player { get; }
        public Character Character { get; }

        public Spells(Monster monster)
        {
            Monster = monster;
            _spellList = new Dictionary<Spell, SpellInfo>();
            _buffs = new List<Buff>();
        }

        public Spells(Character @char, CharacterDto character)
        {
            _spellList = new Dictionary<Spell, SpellInfo>();
            Player = @char.Player;
            Character = @char;
            _buffs = new List<Buff>();


            var spells = ResourceCache.Instance.GetSkills();
            
            foreach (var skill in Character.BaseInfo.Spells)
            {
                var spell = spells[skill];
                _spellList.Add(skill, spell);
                Logger
                    .ForAccount(Player.Session)
                    .Information($"Class Skill Added: {spell.Name}");
            }

            foreach (var skill in character.Spells.Select(x => (Spell)x.Magic))
            {
                var spell = spells[skill];
                _spellList.Add(skill, spell);
                Logger
                    .ForAccount(Player.Session)
                    .Information($"Learned {spell.Name} Skill Added");
            }
        }

        private void Add(Spell skill)
        {
            var spells = ResourceCache.Instance.GetSkills();

            if (!_spellList.ContainsKey(skill))
            {
                _spellList.Add(skill, spells[skill]);
                _newSpell.Add(skill);
            }
        }

        public async Task<bool> TryAdd(Spell skill)
        {
            var spells = ResourceCache.Instance.GetSkills();
            var spell = spells.Where(x => x.Key == skill).Select(x => x.Value).FirstOrDefault();
            if(spell == null)
            {
                throw new ArgumentException($"Can't find skill {skill}");
            }

            if(spell.ReqLevel > Character.Level)
            {
                await Player.Session.SendAsync(new SNotice(NoticeType.Blue, $"You need reach Lv. {spell.ReqLevel}"));
                return false;
            }

            if(spell.Str > Character.Strength)
            {
                await Player.Session.SendAsync(new SNotice(NoticeType.Blue, $"You need {spell.Str} of Strength"));
                return false;
            }

            if(spell.Agility > Character.Agility)
            {
                await Player.Session.SendAsync(new SNotice(NoticeType.Blue, $"You need {spell.Agility} of Agility"));
                return false;
            }

            if (spell.Energy > Character.Energy)
            {
                await Player.Session.SendAsync(new SNotice(NoticeType.Blue, $"You need {spell.Energy} of Energy"));
                return false;
            }

            var pos = _spellList.Count;

            Add(skill);

            if(Player.Status == LoginStatus.Playing)
            {
                await Player.Session.SendAsync(new SSpells(0, new MuEmu.Network.Data.SpellDto
                {
                    Index = (byte)pos,
                    Spell = (ushort)skill,
                }));
                await Player.Session.SendAsync(new SNotice(NoticeType.Blue, $"You have learned: {spell.Name}"));
            }

            return true;
        }

        public void Remove(Spell skill)
        {
            _spellList.Remove(skill);
        }

        public void Remove(SpellInfo skill)
        {
            Remove(skill.Number);
        }

        public List<SpellInfo> SpellList => _spellList.Select(x => x.Value).ToList();
        public IDictionary<Spell, SpellInfo> SpellDictionary => _spellList;

        public IEnumerable<Buff> BuffList => _buffs;

        public async void SendList()
        {
            var i = 0;
            var list = new List<MuEmu.Network.Data.SpellDto>();
            foreach(var magic in _spellList)
            {
                list.Add(new MuEmu.Network.Data.SpellDto
                {
                    Index = (byte)i,
                    Spell = (ushort)magic.Key,
                });
                i++;
            }
            await Player.Session.SendAsync(new SSpells(0, list.ToArray()));
        }

        public async void SetBuff(SkillStates effect, TimeSpan time, Character source = null)
        {
            if (_buffs.Any(x => x.State == effect))
                return;

            var buff = new Buff { State = effect, EndAt = DateTimeOffset.Now.Add(time), };
            var @char = Player.Character;

            switch (effect)
            {
                case SkillStates.ShadowPhantom:
                    buff.AttackAdd = @char.Level / 3 + 45;
                    buff.DefenseAdd = @char.Level / 3 + 50;
                    break;
                case SkillStates.SoulBarrier:
                    buff.DefenseAddRate = 10 + source.AgilityTotal / 50 + source.EnergyTotal / 200;
                    break;
                case SkillStates.Defense:
                    buff.DefenseAdd = source.EnergyTotal / 8;
                    break;
                case SkillStates.Attack:
                    buff.AttackAdd = source.EnergyTotal / 7;
                    break;
                case SkillStates.SwellLife:
                    buff.LifeAdd = 0;
                    break;
                case SkillStates.HAttackPower:
                    buff.AttackAdd = 25;
                    break;
                case SkillStates.HAttackSpeed:
                    break;
                case SkillStates.HDefensePower:
                    buff.DefenseAdd = 100;
                    break;
                case SkillStates.HMaxLife:
                    buff.LifeAdd = 500;
                    break;
                case SkillStates.HMaxMana:
                    buff.ManaAdd = 500;
                    break;
            }

            _buffs.Add(buff);

            var m = new SViewSkillState(1, (ushort)Player.Session.ID, (byte)effect);

            await Player.Session.SendAsync(m);
            await Player.SendV2Message(m);
        }

        public async void ClearBuffTimeOut()
        {
            var b = _buffs.Where(x => x.EndAt > DateTimeOffset.Now);
            var rem = _buffs.Except(b);
            _buffs = b.ToList();

            try
            {
                foreach (var r in rem)
                    await DelBuff(r.State);
            }catch(Exception)
            {
                _buffs.Clear();
                return;
            }
        }

        public async Task DelBuff(SkillStates effect)
        {
            var m = new SViewSkillState(0, (ushort)Player.Session.ID, (byte)effect);

            await Player.Session.SendAsync(m);
            await Player.SendV2Message(m);
        }

        public async void AttackSend(Spell spell, ushort Target, bool Success)
        {
            Target = Success ? (ushort)(Target | 0x8000) : Target;

            var message = new SMagicAttack(spell, (ushort)Player.Session.ID, Target);

            if (Monster == null)
            {

                await Player
                    .Session
                    .SendAsync(message);

                await Player.SendV2Message(message);
            }
        }

        public SkillStates[] ViewSkillStates => _buffs.Select(x => x.State).ToArray();

        public async Task Save(GameContext db)
        {
            if (!_newSpell.Any())
                return;

            await db.Spells.AddRangeAsync(_newSpell.Select(x => new MU.DataBase.SpellDto
            {
                CharacterId = Character.Id,
                Level = 1,
                Magic = (short)x,
            }));

            _newSpell.Clear();
        }
    }
}
