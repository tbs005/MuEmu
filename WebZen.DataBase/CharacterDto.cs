﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MU.DataBase
{
    [Table("Character")]
    public class CharacterDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterId { get; set; }

        public string Name { get; set; }

        public int Class { get; set; }

        public int? GuildId { get; set; }
        public GuildDto Guild { get; set; }

        public byte Map { get; set; }
        public short X { get; set; }
        public short Y { get; set; }

        // Stats Info
        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Level { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Life { get; set; }
        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort MaxLife { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Mana { get; set; }
        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort MaxMana { get; set; }

        public long Experience { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort LevelUpPoints { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Str { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Agility { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Vitality { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Energy { get; set; }

        [Column(TypeName = "SMALLINT(5) UNSIGNED")]
        public ushort Command { get; set; }

        [Column(TypeName = "INT(11) UNSIGNED")]
        public uint Money { get; set; }
        
        // Inventory
        public List<ItemDto> Items { get; set; }

        // Spells
        public List<SpellDto> Spells { get; set; }

        // Quest
        public List<QuestDto> Quests { get; set; }

        // Friends
        public List<FriendDto> Friends { get; set; }

        public SkillKeyDto SkillKey { get; set; }

        public int AccountId { get; set; }
        public AccountDto Account { get; set; }
    }
}
