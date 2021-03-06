﻿namespace vMenuClient
{
    public enum Permission
    {
        // Global
        Everything,
        DontKickMe,
        NoClip,

        // Online Players
        OPMenu,
        OPAll,
        OPTeleport,
        OPWaypoint,
        OPSpectate,
        OPSummon,
        OPKill,
        OPKick,
        OPPermBan,
        OPTempBan,
        OPUnban,

        // Player Options
        POMenu,
        POAll,
        POGod,
        POInvisible,
        POFastRun,
        POFastSwim,
        POSuperjump,
        PONoRagdoll,
        PONeverWanted,
        POSetWanted,
        POIgnored,
        POFunctions,
        POFreeze,
        POScenarios,

        // Vehicle Options
        VOMenu,
        VOAll,
        VOGod,
        VOSpecialGod,
        VORepair,
        VOWash,
        VOEngine,
        VOChangePlate,
        VOMod,
        VOColors,
        VOLiveries,
        VOComponents,
        VODoors,
        VOWindows,
        VOFreeze,
        VOTorqueMultiplier,
        VOPowerMultiplier,
        VOFlip,
        VOAlarm,
        VOCycleSeats,
        VOEngineAlwaysOn,
        VONoSiren,
        VONoHelmet,
        VOLights,
        VODelete,
        VOUnderglow,


        // Vehicle Spawner
        VSMenu,
        VSAll,
        VSDisableReplacePrevious,
        VSSpawnByName,
        VSAddon,
        VSCompacts,
        VSSedans,
        VSSUVs,
        VSCoupes,
        VSMuscle,
        VSSportsClassic,
        VSSports,
        VSSuper,
        VSMotorcycles,
        VSOffRoad,
        VSIndustrial,
        VSUtility,
        VSVans,
        VSCycles,
        VSBoats,
        VSHelicopters,
        VSPlanes,
        VSService,
        VSEmergency,
        VSMilitary,
        VSCommercial,
        VSTrains,

        // Saved Vehicles
        SVMenu,
        SVAll,
        SVSpawn,

        // Player Appearance
        PAMenu,
        PAAll,
        PACustomize,
        PASpawnSaved,
        PASpawnNew,

        // Time Options
        TOMenu,
        TOAll,
        TOFreezeTime,
        TOSetTime,

        // Weather Options
        WOMenu,
        WOAll,
        WODynamic,
        WOBlackout,
        WOSetWeather,
        WORemoveClouds,
        WORandomizeClouds,

        // Weapon Options
        WPMenu,
        WPAll,
        WPGetAll,
        WPRemoveAll,
        WPUnlimitedAmmo,
        WPNoReload,
        WPSpawn,
        WPSetAllAmmo,

        //Weapons Permissions
        WPAPPistol,
        WPAdvancedRifle,
        WPAssaultRifle,
        WPAssaultRifleMk2,
        WPAssaultSMG,
        WPAssaultShotgun,
        WPBZGas,
        WPBall,
        WPBat,
        WPBattleAxe,
        WPBottle,
        WPBullpupRifle,
        WPBullpupRifleMk2,
        WPBullpupShotgun,
        WPCarbineRifle,
        WPCarbineRifleMk2,
        WPCombatMG,
        WPCombatMGMk2,
        WPCombatPDW,
        WPCombatPistol,
        WPCompactGrenadeLauncher,
        WPCompactRifle,
        WPCrowbar,
        WPDagger,
        WPDoubleAction,
        WPDoubleBarrelShotgun,
        WPFireExtinguisher,
        WPFirework,
        WPFlare,
        WPFlareGun,
        WPFlashlight,
        WPGolfClub,
        WPGrenade,
        WPGrenadeLauncher,
        WPGrenadeLauncherSmoke,
        WPGusenberg,
        WPHammer,
        WPHatchet,
        WPHeavyPistol,
        WPHeavyShotgun,
        WPHeavySniper,
        WPHeavySniperMk2,
        WPHomingLauncher,
        WPKnife,
        WPKnuckleDuster,
        WPMG,
        WPMachete,
        WPMachinePistol,
        WPMarksmanPistol,
        WPMarksmanRifle,
        WPMarksmanRifleMk2,
        WPMicroSMG,
        WPMiniSMG,
        WPMinigun,
        WPMolotov,
        WPMusket,
        WPNightVision,
        WPNightstick,
        WPParachute,
        WPPetrolCan,
        WPPipeBomb,
        WPPistol,
        WPPistol50,
        WPPistolMk2,
        WPPoolCue,
        WPProximityMine,
        WPPumpShotgun,
        WPPumpShotgunMk2,
        WPRPG,
        WPRailgun,
        WPRevolver,
        WPRevolverMk2,
        WPSMG,
        WPSMGMk2,
        WPSNSPistol,
        WPSNSPistolMk2,
        WPSawnOffShotgun,
        WPSmokeGrenade,
        WPSniperRifle,
        WPSnowball,
        WPSpecialCarbine,
        WPSpecialCarbineMk2,
        WPStickyBomb,
        WPStunGun,
        WPSweeperShotgun,
        WPSwitchBlade,
        WPUnarmed,
        WPVintagePistol,
        WPWrench,

        // Misc Settings
        //MSMenu, (removed because this menu should always be allowed).
        MSAll,
        MSClearArea,
        MSTeleportToWp,
        MSShowCoordinates,
        MSShowLocation,
        MSJoinQuitNotifs,
        MSDeathNotifs,
        MSNightVision,
        MSThermalVision,

        // Voice Chat
        VCMenu,
        VCAll,
        VCEnable,
        VCShowSpeaker,
        VCStaffChannel,

    };
}
