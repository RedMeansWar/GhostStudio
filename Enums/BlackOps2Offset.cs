namespace GhostStudio.Enums
{
    public enum BlackOps2CheatProtectionOffset : uint
    {
        ChallengesCheat = 0x8259A65C,
        /// <summary>
        /// Bytes = NOP(0x60, 00, 00, 00)
        /// jtag.SetMemory(0x8259A65C, new byte[] { 0x60, 0x00, 0x00, 0x00 });
        /// </summary>
        CallsCheat = 0x82497EB0,
        CheatsCheat = 0x82497F30,
        WriteCheat = 0x82497EE0,
        ReadCheat = 0x82497EC8,
        BanCheat1 = 0x82599680,
        BanCheat2 = 0x82599670,
        BanCheat3 = 0x82599628,
        BanCheat4 = 0x8259964C, 
        BanChecksCheat = 0x825996AC,
        ConsoleChecksCheat = 0x825996B4,
        XUIDChecksCheat = 0x82599644,
        DataMonitorCheat = 0x827DB494,
        ModulehandlerCheat = 0x824E0CD8,
        DemonWareChallengesCheat1 = 0x82273D7C,
        DemonWareChallengesCheat2 = 0x825035F8,
        DemonWareChallengesCheat3 = 0x8236CE5C
    }

    public enum BlackOps2AutowallOffset : uint
    {
        Offset1 = 0x822DFB90,
        Offset2 = 0x82258520,
        Offset3 = 0x82257E30,
        Offset4 = 0x82258CE4,
        Offset5 = 0x82258FAC,
        Offset6 = 0x8225900C,
        Offset7 = 0x82258D60,
        Offset8 = 0x82259B40
    }

    public enum BlackOps2GetGameAddressesOffset : uint
    {
        Offset1 = 0x82C55D60,
        Offset2 = 0x841E1B30
    }

    public enum BlackOps2ErrorPatchOffset : uint
    {
        Offset1 = 0x8259B6A7,
        Offset2 = 0x822D1110
    }

    public enum BlackOps2FunctionOffset : uint
    {
        DvarGetFloat = 0x8229F028,
        DvarGetBool = 0x82154FF8,
        DvarFindVariable = 0x82496430,
        AddCommandDrawText = 0x828B8BA0,
        AddCommandDrawStretchPic = 0x828B86C0,
        TextWidth = 0x828B6FD8,
        TextHeight = 0x82490390,
        NormalizeTextScale = 0x828B6ED8,
        GetSpreadForWeapon = 0x826BB4E0,
        GetWeaponDefinition = 0x826BF988,
        GetWeaponDefintion2 = 0x826BF970,
        RegisterMaterialHandler = 0x828B78F0,
        ClientRegisterFont = 0x82275F78,
        FindXAssetHeader = 0x822CAE50,
        DrawRotatedPicPhysical = 0x821C7F58,
        DObjectGetWorldTagPosition = 0x821D03F0,
        IsEntityFriendly = 0x821CD948,
        GetPlayherViewOrigin = 0x822544B0,
        LocationalTrace = 0x8225C568,
        GetWeaponIndexForName = 0x826C06E8,
        AllLocalClientsDisconnected = 0x827504D0,
        AddReliableCommand = 0x822786E0,
        GetScreenDimensions = 0x82261D30,
        ClientBufferAddText = 0x824015E0,
        GetClientDObject = 0x82414578,
        GetString = 0x82533528,
        TransformSeed = 0x826961B8,
        RandomBulletDir = 0x82696250,
        Sub82697FC0 = 0x82697FC0,
        FireBulletPenetrate = 0x82258840,
        OutOfBandwidthPrint = 0x8241D580,
        UiOpenToastPopup = 0x82454800
    }

    public enum BlackOps2HookOffset : uint
    {
        SndEndFrame = 0x828B9F58,
        XamInputGetState = 0x827D8A48,
        ClientCreateNewCommand = 0x82261A40,
        ClientSendCommand = 0x8225EAA8
    }

    public enum BlackOps2StructOffset : uint
    {
        UiContext = 0x83BA29F0,
        ClientActive = 0x82C70F4C,
        Centity = 0x82BBC554,
        CgArray = 0x82BBAE68,
        CgsArray = 0x82BBAE44,
        NetworkInfo = 0x82CAC3A0,
        PlayerState = 0x83551A10
    }

    public enum BlackOps2LiveOffset : uint
    {
        GetCurrentSession = 0x8259B878,
        GetPlayerNetworkAddress = 0x825B70B8,
        XamGetUserName = 0x8293D724
    }

    public enum BlackOps2MiscOffset : ulong
    {
        DvSendServerCommand = 0x8242FB70,
        ConsoleCMD = 0x824015E0,
        SetClientStats = 0x82085654,
        ClassItemCount = 0x826A5FBC,
        CrashFix = 0x8295FFF8,
        EmptyMemory = 0x82C55D00,
        ScreenPlacement = 0x82CBC168,
        ServerId = 0x82C15758,

        AllEmblemsPurchased = 0x1CE080012,
        AllItemsPurchased = 0x1CD7CE012,
        AllEmblemsUnlocked = 0x1CE07A012,
        AllItemsUnlocked = 0x1CD7C8012,
        SpawnAi = 0x1CA516012,
        
        HostName = 0x1CD66C012,
        GameStartTimerLength = 0x1CD5B2012,
        GameStartPrivateTimerLength = 0x1CD5A6012
    }

    public enum BlackOps2InGameOffset : uint
    {
        JumpHeight = 0x82085654,
        FallDamage = 0x82003FD4,
        BodyColor = 0x83C56038,

        /// <summary>
        /// Bytes: On: (0x1) | Off: (0x0)
        /// On: jtag.SetMemory(0x821F5B7F, new byte[] { 0x01, });
        /// Off: jtag.SetMemory(0x821F5B7F, new byte[] { 0x00 });
        /// </summary>
        RedBoxes = 0x821F5B7F,

        /// <summary>
        /// Bytes: On: (0x2b, 11, 0, 1) | Off: (0x2b, 11, 0, 0)
        /// On: jtag.SetMemory(0x82255E1C, new byte[] { 0x2B, 0x11, 0x00, 0x01 });
        /// Off: jtag.SetMemory(0x82255E1C, new byte[] { 0x2B, 0x11, 0x00, 0x00 });
        /// </summary>
        Laser = 0x82255E1C,

        /// <summary>
        /// Bytes: On: (0x38, 0xc0, 0xff, 0xff) | Off: (0x7f, 0xa6, 0xeb, 120)
        /// On: jtag.SetMemory(0x821FC04C, new byte[] { 0x60, 0x00, 0x00, 0x00 });
        /// Off: jtag.SetMemory(0x821FC04C, new byte[] { 0x60, 0x00, 0x00, 0x00 });
        /// </summary>
        Chams = 0x821FC04C,

        /// <summary>
        /// Bytes: On: (0x60, 0x00, 0x00, 0x00) | Off: (0x48, 0x46, 0x13, 0x4)
        /// On: jtag.SetMemory(0x82259BC8, new byte[] { 0x60, 0x00, 0x00, 0x00 });
        /// Off: jtag.SetMemory(0x82259BC8, new byte[] { 0x48, 0x46, 0x13, 0x4 });
        /// </summary>
        NoRecoil = 0x82259BC8,

        NoSway = 0x826C6E6C,
        FOV = 0x82BC2774,
        Radar = 0x821B8FD4
    }

    public enum BlackOps2StatsOffset : uint
    {
        StatEntry = 0x840C0500,
        Kills = 0x84348D00,
        Deaths = 0x84348AD2,
        KillSteak = 0x84348FE8,
        Hits = 0x84348A45,
        Prestige = 0x843491A4,
        XPLevel = 0x843491BC,
        Misses = 0x843492CA,
        GamesPlayed = 0x83583941,
        HeadShots = 0x839ADBB1,
        Looses = 0x84348D72,
        Score = 0x843491E0,
        TotalShots = 0x839ADC06,
        TimePlayed = 0x8434929A,
        WinLossRatio = 0x839ADC0A,
        PrestigeTokens = 0x8435292E,
        Wins = 0x843492E2,
        WinStreak = 0x835839A3,
        UnlockEntryTitles = 0x8435429F,
        UnlockEntryWeapons = 0x8434AF80
    }

    public enum BlackOps2ClassOffset : uint
    {
        Class1Primary = 0x84353A4A,
        Class1Secondary = 0x84353A58,
        Class1PrimaryCamo = 0x84353A50,
        Class1SecondaryCamo = 0x84353A5D,
        Class1Perk1 = 0x84353A66,
        Class1Perk2 = 0x84353A67,
        Class1Perk3 = 0x84353A68,
        Class1Lethal = 0x84353A6C,
        Class1Tatical = 0x84353A6D,

        Class2Primary = 0x84353A7E,
        Class2Secondary = 0x84353A8C,
        Class2PrimaryCamo = 0x84353A83,
        Class2SecondaryCamo = 0x84353A92,
        Class2Perk1 = 0x84353A9A,
        Class2Perk2 = 0x84353A9B,
        Class2Perk3 = 0x84353A9C,
        Class2Lethal = 0x84353AA0,
        Class2Tatical = 0x84353AA1,

        Class3Primary = 0x84353AB2,
        Class3Secondary = 0x84353AC1,
        Class3PrimaryCamo = 0x84353AB9,
        Class3SecondaryCamo = 0x84353AC6,
        Class3Perk1 = 0x84353ACF,
        Class3Perk2 = 0x84353AD0,
        Class3Perk3 = 0x84353AD1,
        Class3Lethal = 0x84353AD5,
        Class3Tatical = 0x84353AD6,

        Class4Class4Primary = 0x84353AE7,
        Class4Secondary = 0x84353AF5,
        Class4PrimaryCamo = 0x84353AED,
        Class4SecondaryCamo = 0x84353AFB,
        Class4Perk1 = 0x84353B03,
        Class4Perk2 = 0x84353B04,
        Class4Perk3 = 0x84353B05,
        Class4Lethal = 0x84353B09,
        Class4Tatical = 0x84353B0C,
            
        Class5Primary = 0x84353B1C,
        Class5Secondary = 0x84353B2A,
        Class5PrimaryCamo = 0x84353B22,
        Class5SecondaryCamo = 0x84353B30,
        Class5Perk1 = 0x84353B38,
        Class5Perk2 = 0x84353B39,
        Class5Perk3 = 0x84353B3A,
        Class5Lethal = 0x84353B3E,
        Class5Tatical = 0x84353B40,
            
        Class6Primary = 0x84353B50,
        Class6Secondary = 0x84353B5E,
        Class6PrimaryCamo = 0x84353B56,
        Class6SecondaryCamo = 0x84353B64,
        Class6Perk1 = 0x84353B6C,
        Class6Perk2 = 0x84353B6D,
        Class6Perk3 = 0x84353B6E,
        Class6Lethal = 0x84353B72,
        Class6Tatical = 0x84353B75,

        Class7Primary = 0x84353B85,
        Class7Secondary = 0x84353B93,
        Class7PrimaryCamo = 0x84353B8B,
        Class7SecondaryCamo = 0x84353B99,
        Class7Perk1 = 0x84353BA1,
        Class7Perk2 = 0x84353BA2,
        Class7Perk3 = 0x84353BA3,
        Class7Lethal = 0x84353BA7,
        Class7Tatical = 0x84353BA9,

        Class8Primary = 0x84353BB9,
        Class8Secondary = 0x84353BC7,
        Class8PrimaryCamo = 0x84353BBF,
        Class8SecondaryCamo = 0x84353BCD,
        Class8Perk1 = 0x84353BD5,
        Class8Perk2 = 0x84353BD6,
        Class8Perk3 = 0x84353BD7,
        Class8Lethal = 0x84353BDB,
        Class8Tatical = 0x84353BDE,

        Class9Primary = 0x84353BEE,
        Class9Secondary = 0x84353BFC,
        Class9PrimaryCamo = 0x84353BF4,
        Class9SecondaryCamo = 0x84353C02,
        Class9Perk1 = 0x84353C0A,
        Class9Perk2 = 0x84353C0B,
        Class9Perk3 = 0x84353C0C,
        Class9Lethal = 0x84353C10,
        Class9Tatical = 0x84353C12,
            
        Class10Primary = 0x84353C22,
        Class10Secondary = 0x84353C30,
        Class10PrimaryCamo = 0x84353C28,
        Class10SecondaryCamo = 0x84353C36,
        Class10Perk1 = 0x84353C3E,
        Class10Perk2 = 0x84353C3F,
        Class10Perk3 = 0x84353C40,
        Class10Lethal = 0x84353C44,
        Class10Tatical = 0x84353C47
    }

    public enum BlackOps2PerkOffset : ulong
    {

    }

    public enum BlackOps2CamosOffset : uint
    {
        // Normal
        /// <summary>
        /// 1
        /// </summary>
        None = 0x1,

        /// <summary>
        /// 5
        /// </summary>
        DEVGRU = 0x5,

        /// <summary>
        /// 9
        /// </summary>
        ATAC = 0x9,

        /// <summary>
        /// 13
        /// </summary>
        ERDL = 0xD,

        /// <summary>
        /// 17
        /// </summary>
        Siberia = 0x11,

        /// <summary>
        /// 21
        /// </summary>
        Choco = 0x15,

        /// <summary>
        /// 25
        /// </summary>
        BlueTiger = 0x19,

        /// <summary>
        /// 29
        /// </summary>
        Bloodshot = 0x1D,

        /// <summary>
        /// 33
        /// </summary>
        GhostEx = 0x21,

        /// <summary>
        /// 37
        /// </summary>
        Kryptek = 0x25,

        /// <summary>
        /// 41
        /// </summary>
        Carbon = 0x29,

        /// <summary>
        /// 45
        /// </summary>
        CherryBlossom = 0x2D,

        /// <summary>
        /// 49
        /// </summary>
        ArtOfWar = 0x31,

        /// <summary>
        /// 53
        /// </summary>
        Ronin = 0x35,

        /// <summary>
        /// 57
        /// </summary>
        Skulls = 0x39,

        /// <summary>
        /// 61
        /// </summary>
        Gold = 0x3D,

        /// <summary>
        /// 65
        /// </summary>
        Diamond = 0x41,

        // DLC1 Camos
        /// <summary>
        /// 69
        /// </summary>
        EliteMember = 0x45,
        
        /// <summary>
        /// 73
        /// </summary>
        CEDigital = 0x48,
        
        /// <summary>
        /// 183
        /// </summary>
        Jungle = 0xB7,
        
        /// <summary>
        /// 188
        /// </summary>
        Benjamins = 0xBC,
        
        /// <summary>
        /// 192
        /// </summary>
        DiaDeMuertos = 0xC0,
        
        /// <summary>
        /// 196
        /// </summary>
        Graffiti = 0xC4,
        
        /// <summary>
        /// 200
        /// </summary>
        Kawaii = 0xC8,
        
        /// <summary>
        /// 204
        /// </summary>
        PartyRock = 0xCC,
        
        /// <summary>
        /// 208
        /// </summary>
        PackAPunch = 0xD0,

        /// <summary>
        /// 212
        /// </summary>
        Viper = 0xD4,
        
        /// <summary>
        /// 216
        /// </summary>
        Bacon = 0xD8,
        
        /// <summary>
        /// 184
        /// </summary>
        UK = 0xB8
    }

    public enum BlackOps2ClassFreezeOffset : uint
    {
        /// <summary>
        /// jtag.SetMemory(0x84353AA2, new byte[] { 0x10, 0x7A });
        /// </summary>
        Class2 = 0x84353AA2,

        /// <summary>
        /// jtag.SetMemory(0x84353B0B, new byte[] { 0x10, 0x7A });
        /// </summary>
        Class4 = 0x84353B0B,

        /// <summary>
        /// jtag.SetMemory(0x84353B74, new byte[] { 0x10, 0x7A });
        /// </summary>
        Class6 = 0x84353B74,

        /// <summary>
        /// jtag.SetMemory(0x84353BDD, new byte[] { 0x10, 0x7A });
        /// </summary>
        Class8 = 0x84353BDD,

        /// <summary>
        /// jtag.SetMemory(0x84353C46, new byte[] { 0x10, 0x7A });
        /// </summary>
        Class10 = 0x84353C46
    }
}
