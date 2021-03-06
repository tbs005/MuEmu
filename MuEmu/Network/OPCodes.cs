﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuEmu.Network
{
    public enum ConOpCode : ushort
    {
        CSWelcome = 0x0100,
        GSJoin = 0xFF10,
        GSKeep = 0xFF11,
    }


    public enum GlobalOpCode : ushort
    {
        LiveClient = 0x000E,
    }

    public enum CSOpCode : ushort
    {
        JoinResult = 0x00F1,
        Login = 0x01F1,
        CharacterList = 0x00F3,
        CharacterCreate = 0x01F3,
        CharacterDelete = 0x02F3,
        JoinMap2 = 0x03F3,
        JoinMap = 0x15F3,
    }

    public enum GameOpCode : ushort
    {
        Warp = 0x028E,
        Tax = 0x1AB2,
        KillCount = 0x01B8,
        ClientClose = 0x02F1,
        ClientMessage = 0x03F1,
        LevelUp = 0x5F3,
        PointAdd = 0x06F3,
        Damage = 0x07F3,
        Inventory = 0x10F3,
        Spells = 0x11F3,
        DataLoadOK = 0x12F3,
        Equipament = 0x13F3,
        OneItemSend = 0x14F3,
        SkillKey = 0x30F3,
        Command = 0x40F3,
        NewQuestInfo = 0x1AF6,
        QuestDetails = 0x1BF6,
        QuestWindow = 0x01F9,
        JewelMix = 0x00BC,
        JewelUnMix = 0x01BC,
        GeneralChat0 = 0xFF00,
        GeneralChat1 = 0xFF01,
        WhisperChat = 0xFF02,
        GameSecurity = 0xFF03,
        ViewSkillState = 0xFF07,
        EventState = 0xFF0B,
        Notice = 0xFF0D,
        Weather = 0xFF0F,
        ViewPortCreate = 0xFF12,
        ViewPortMCreate = 0xFF13,
        ViewPortDestroy = 0xFF14,
        AttackResult = 0xFF15,
        KillPlayer = 0xFF16,
        DiePlayer = 0xFF17,
        Rotation = 0xFF18,
        MagicAttack = 0xFF19,
        Teleport = 0xFF1C,
        MagicDuration = 0xFF1E,
        ViewPortMCall = 0xFF1F,
        ViewPortItemCreate = 0xFF20,
        ViewPortItemDestroy = 0xFF21,
        ItemGet = 0xFF22,
        ItemThrow = 0xFF23,
        MoveItem = 0xFF24,
        HealthUpdate = 0xFF26,
        ManaUpdate = 0xFF27,
        InventoryItemDelete = 0xFF28,
        ItemUseSpecialTime = 0xFF29,
        InventoryItemDurUpdate = 0xFF2A,
        Talk = 0xFF30,
        CloseWindow = 0xFF31,
        Buy = 0xFF32,
        Sell = 0xFF33,
        ItemModify = 0xFF34,
        PartyRequest = 0xFF40,
        PartyResult = 0xFF41,
        PartyList = 0xFF42,
        PartyDelUser = 0x0FF43,
        ViewPortChange = 0xFF45,
        SetMapAtt = 0xFF46,
        Effect = 0xFF48,
        ViewPortGuildCreate = 0xFF65,
        WarehouseMoney = 0xFF81,
        WarehouseUseEnd = 0xFF82,
        ChaosBoxItemMixButtonClick = 0xFF86,
        ChaosBoxUseEnd = 0xFF87,
        EventEnterCount = 0xFF9F,
        QuestInfo = 0xFFA0,
        FriendList = 0xFFC0,
        FriendRequest = 0xFFC2,
        AddLetter = 0xFFC6,
        Move = 0xFFD3,
        PositionSet = 0xFFD6,
        Attack = 0xFFD7,
        Position = 0xFFDF,
    }

    public enum GuildOpCode
    {
        GuildListAll = 0xFF52,
        MasterQuestion = 0xFF54,
        GuildSaveInfo = 0xFF55,
        GuildViewPort = 0xFF65,
        GuildReqViewport = 0xFF66,
    }

    public enum EventOpCode
    {
        RemainTime = 0xFF91,
        LuckyCoinsCount = 0x0BBF,
        LuckyCoinsRegistre = 0x0CBF,

        DevilSquareSet = 0xFF92,
        EventChipInfo = 0xFF94,
        BloodCastleEnter = 0xFF9A,
        BloodCastleState = 0xFF9B,

        CrywolfBenefit = 0x09BD,
    }

    public enum CashOpCode : ushort
    {
        CashItems = 0x05D0,
        CashPoints = 0x04F5,
    }

    public enum QuestOpCode : ushort
    {
        SetQuest = 0xFFA1,
        SetQuestState = 0xFFA2,
        QuestPrize = 0xFFA3,
    }
}
