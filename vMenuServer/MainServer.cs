﻿using GHMatti.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;
using System.Dynamic;
using static vMenuServer.DebugLog;

namespace vMenuServer
{

    public static class DebugLog
    {
        /// <summary>
        /// Global log data function, only logs when debugging is enabled.
        /// </summary>
        /// <param name="data"></param>
        public static void Log(dynamic data)
        {
            if (MainServer.DebugMode)
            {
                Debug.Write(data.ToString() + "\n");
            }
        }
    }

    public class MainServer : BaseScript
    {
        public static bool UpToDate = true;
        // Debug shows more information when doing certain things. Leave it off to improve performance!
        public static bool DebugMode = GetResourceMetadata(GetCurrentResourceName(), "server_debug_mode", 0) == "true" ? true : false;

        public static string Version { get { return GetResourceMetadata(GetCurrentResourceName(), "version", 0); } }

        private int currentHours = 9;
        private int currentMinutes = 0;
        private string currentWeather = "CLEAR";
        private bool dynamicWeather = true;
        private bool blackout = false;
        private bool freezeTime = false;
        private int dynamicWeatherTimeLeft = 5 * 12 * 10; // 5 seconds * 12 (because the loop checks 12 times a minute) * 10 (10 minutes)
        private long gameTimer = GetGameTimer();
        private List<string> CloudTypes = new List<string>()
        {
            "Cloudy 01",
            "RAIN",
            "horizonband1",
            "horizonband2",
            "Puffs",
            "Wispy",
            "Horizon",
            "Stormy 01",
            "Clear 01",
            "Snowy 01",
            "Contrails",
            "altostratus",
            "Nimbus",
            "Cirrus",
            "cirrocumulus",
            "stratoscumulus",
            "horizonband3",
            "Stripey",
            "horsey",
            "shower",
        };

        public List<string> aceNames = new List<string>()
        {
            // Global
            "Everything",
            "DontKickMe",
            "NoClip",

            // Online Players
            "OPMenu",
            "OPAll",
            "OPTeleport",
            "OPWaypoint",
            "OPSpectate",
            "OPSummon",
            "OPKill",
            "OPKick",
            "OPPermBan",
            "OPTempBan",
            "OPUnban",

            // Player Options
            "POMenu",
            "POAll",
            "POGod",
            "POInvisible",
            "POFastRun",
            "POFastSwim",
            "POSuperjump",
            "PONoRagdoll",
            "PONeverWanted",
            "POSetWanted",
            "POIgnored",
            "POFunctions",
            "POFreeze",
            "POScenarios",

            // Vehicle Options
            "VOMenu",
            "VOAll",
            "VOGod",
            "VOSpecialGod",
            "VORepair",
            "VOWash",
            "VOEngine",
            "VOChangePlate",
            "VOMod",
            "VOColors",
            "VOLiveries",
            "VOComponents",
            "VODoors",
            "VOWindows",
            "VOFreeze",
            "VOTorqueMultiplier",
            "VOPowerMultiplier",
            "VOFlip",
            "VOAlarm",
            "VOCycleSeats",
            "VOEngineAlwaysOn",
            "VONoSiren",
            "VONoHelmet",
            "VOLights",
            "VODelete",
            "VOUnderglow",
            
            // Vehicle Spawner
            "VSMenu",
            "VSAll",
            "VSDisableReplacePrevious",
            "VSSpawnByName",
            "VSAddon",
            "VSCompacts",
            "VSSedans",
            "VSSUVs",
            "VSCoupes",
            "VSMuscle",
            "VSSportsClassic",
            "VSSports",
            "VSSuper",
            "VSMotorcycles",
            "VSOffRoad",
            "VSIndustrial",
            "VSUtility",
            "VSVans",
            "VSCycles",
            "VSBoats",
            "VSHelicopters",
            "VSPlanes",
            "VSService",
            "VSEmergency",
            "VSMilitary",
            "VSCommercial",
            "VSTrains",

            // Saved Vehicles
            "SVMenu",
            "SVAll",
            "SVSpawn",

            // Player Appearance
            "PAMenu",
            "PAAll",
            "PACustomize",
            "PASpawnSaved",
            "PASpawnNew",

            // Time Options
            "TOMenu",
            "TOAll",
            "TOFreezeTime",
            "TOSetTime",

            // Weather Options
            "WOMenu",
            "WOAll",
            "WODynamic",
            "WOBlackout",
            "WOSetWeather",
            "WORemoveClouds",
            "WORandomizeClouds",

            // Weapon Options
            "WPMenu",
            "WPAll",
            "WPGetAll",
            "WPRemoveAll",
            "WPUnlimitedAmmo",
            "WPNoReload",
            "WPSpawn",
            "WPSetAllAmmo",
            
            // Weapons Permissions
            "WPSniperRifle",
            "WPFireExtinguisher",
            "WPCompactGrenadeLauncher",
            "WPSnowball",
            "WPVintagePistol",
            "WPCombatPDW",
            "WPHeavySniperMk2",
            "WPHeavySniper",
            "WPSweeperShotgun",
            "WPMicroSMG",
            "WPWrench",
            "WPPistol",
            "WPPumpShotgun",
            "WPAPPistol",
            "WPBall",
            "WPMolotov",
            "WPSMG",
            "WPStickyBomb",
            "WPPetrolCan",
            "WPStunGun",
            "WPAssaultRifleMk2",
            "WPHeavyShotgun",
            "WPMinigun",
            "WPGolfClub",
            "WPFlareGun",
            "WPFlare",
            "WPGrenadeLauncherSmoke",
            "WPHammer",
            "WPCombatPistol",
            "WPGusenberg",
            "WPCompactRifle",
            "WPHomingLauncher",
            "WPNightstick",
            "WPRailgun",
            "WPSawnOffShotgun",
            "WPSMGMk2",
            "WPBullpupRifle",
            "WPFirework",
            "WPCombatMG",
            "WPCarbineRifle",
            "WPCrowbar",
            "WPFlashlight",
            "WPDagger",
            "WPGrenade",
            "WPPoolCue",
            "WPBat",
            "WPPistol50",
            "WPKnife",
            "WPMG",
            "WPBullpupShotgun",
            "WPBZGas",
            "WPUnarmed",
            "WPGrenadeLauncher",
            "WPNightVision",
            "WPMusket",
            "WPProximityMine",
            "WPAdvancedRifle",
            "WPRPG",
            "WPPipeBomb",
            "WPMiniSMG",
            "WPSNSPistol",
            "WPPistolMk2",
            "WPAssaultRifle",
            "WPSpecialCarbine",
            "WPRevolver",
            "WPMarksmanRifle",
            "WPBattleAxe",
            "WPHeavyPistol",
            "WPKnuckleDuster",
            "WPMachinePistol",
            "WPCombatMGMk2",
            "WPMarksmanPistol",
            "WPMachete",
            "WPSwitchBlade",
            "WPAssaultShotgun",
            "WPDoubleBarrelShotgun",
            "WPAssaultSMG",
            "WPHatchet",
            "WPBottle",
            "WPCarbineRifleMk2",
            "WPParachute",
            "WPSmokeGrenade",

            // Misc Settings
            //"MSMenu", (removed because this menu should always be allowed).
            "MSAll",
            "MSClearArea",
            "MSTeleportToWp",
            "MSShowCoordinates",
            "MSShowLocation",
            "MSJoinQuitNotifs",
            "MSDeathNotifs",
            "MSNightVision",
            "MSThermalVision",

            // Voice Chat
            "VCMenu",
            "VCAll",
            "VCEnable",
            "VCShowSpeaker",
            "VCStaffChannel",
        };
        public List<string> addonVehicles = new List<string>();
        public List<string> addonPeds = new List<string>();
        public List<string> addonWeapons = new List<string>();

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainServer()
        {
            RegisterCommand("vmenuserver", new Action<dynamic, List<dynamic>, string>((dynamic source, List<dynamic> args, string rawCommand) =>
            {
                if (args != null)
                {
                    if (args.Count > 0)
                    {
                        if (args[0].ToString().ToLower() == "debug")
                        {
                            DebugMode = !DebugMode;
                            if (source == 0)
                            {
                                Debug.WriteLine($"Debug mode is now set to: {DebugMode}.");
                            }
                            else
                            {
                                new PlayerList()[source].TriggerEvent("chatMessage", $"vMenu Debug mode is now set to: {DebugMode}.");
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"vMenu is currently running version: {Version}.");
                    }
                }
            }), true);

            if (GetCurrentResourceName() != "vMenu")
            {
                Exception InvalidNameException = new Exception("\r\n\r\n[vMenu] INSTALLATION ERROR!\r\nThe name of the resource is not valid. " +
                    "Please change the folder name from '" + GetCurrentResourceName() + "' to 'vMenu' (case sensitive) instead!\r\n\r\n\r\n");
                try
                {
                    throw InvalidNameException;
                }
                catch (Exception e)
                {
                    Debug.Write(e.Message);
                }
            }
            else
            {
                // Add event handlers.
                EventHandlers.Add("vMenu:SummonPlayer", new Action<Player, int>(SummonPlayer));
                EventHandlers.Add("vMenu:KillPlayer", new Action<Player, int>(KillPlayer));
                EventHandlers.Add("vMenu:KickPlayer", new Action<Player, int, string>(KickPlayer));
                EventHandlers.Add("vMenu:RequestPermissions", new Action<Player>(SendPermissionsAsync));
                EventHandlers.Add("vMenu:UpdateServerWeather", new Action<string, bool, bool>(UpdateWeather));
                EventHandlers.Add("vMenu:UpdateServerWeatherCloudsType", new Action<bool>(UpdateWeatherCloudsType));
                EventHandlers.Add("vMenu:UpdateServerTime", new Action<int, int, bool>(UpdateTime));
                EventHandlers.Add("vMenu:DisconnectSelf", new Action<Player>(DisconnectSource));

                string addons = LoadResourceFile(GetCurrentResourceName(), "addons.json") ?? LoadResourceFile(GetCurrentResourceName(), "config/addons.json") ?? "{}";
                var json = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(addons);

                if (json.ContainsKey("vehicles"))
                {
                    foreach (var modelName in json["vehicles"])
                    {
                        Log("Addon vehicle loaded: " + modelName);
                        addonVehicles.Add(modelName);
                    }
                }

                if (json.ContainsKey("peds"))
                {
                    foreach (var modelName in json["peds"])
                    {
                        Log("Addon ped loaded:" + modelName);
                        addonPeds.Add(modelName);
                    }
                }

                if (json.ContainsKey("weapons"))
                {
                    foreach (var modelName in json["weapons"])
                    {
                        Log("Addon weapon loaded:" + modelName);
                        addonWeapons.Add(modelName);
                    }
                }

                if ((GetConvar("vMenuDisableDynamicWeather", "false") ?? "false").ToLower() == "true")
                {
                    dynamicWeather = false;
                }
                Tick += WeatherLoop;
                Tick += TimeLoop;
            }
        }

        /// <summary>
        /// Disconnect the source player because they used the disconnect menu button.
        /// </summary>
        /// <param name="src"></param>
        private void DisconnectSource([FromSource] Player src)
        {
            src.Drop("You disconnected yourself.");
        }
        #endregion

        #region Manage weather and time changes.
        /// <summary>
        /// Loop used for syncing and keeping track of the time in-game.
        /// </summary>
        /// <returns></returns>
        private async Task TimeLoop()
        {
            await Delay(4000);
            if (freezeTime)
            {
                TriggerClientEvent("vMenu:SetTime", currentHours, currentMinutes, freezeTime);
            }
            else
            {
                currentMinutes += 2;
                if (currentMinutes > 59)
                {
                    currentMinutes = 0;
                    currentHours++;
                }
                if (currentHours > 23)
                {
                    currentHours = 0;
                }
                TriggerClientEvent("vMenu:SetTime", currentHours, currentMinutes, freezeTime);
            }
        }

        /// <summary>
        /// Task used for syncing and changing weather dynamically.
        /// </summary>
        /// <returns></returns>
        private async Task WeatherLoop()
        {
            await Delay(5000);
            if (dynamicWeather)
            {
                dynamicWeatherTimeLeft -= 10;
                if (dynamicWeatherTimeLeft < 10)
                {
                    dynamicWeatherTimeLeft = 5 * 12 * 10;
                    RefreshWeather();

                    if (DebugMode)
                    {
                        long gameTimer2 = GetGameTimer();
                        Log($"Duration: {((gameTimer2 - gameTimer) / 100).ToString()}. New Weather Type: {currentWeather}");
                        gameTimer = gameTimer2;
                    }
                }
            }
            else
            {
                dynamicWeatherTimeLeft = 5 * 12 * 10;
            }
            TriggerClientEvent("vMenu:SetWeather", currentWeather, blackout, dynamicWeather);
        }

        /// <summary>
        /// Select a new random weather type, based on the current weather and some patterns.
        /// </summary>
        private void RefreshWeather()
        {
            var random = new Random().Next(20);
            if (currentWeather == "RAIN" || currentWeather == "THUNDER")
            {
                currentWeather = "CLEARING";
            }
            else if (currentWeather == "CLEARING")
            {
                currentWeather = "CLOUDS";
            }
            else
            {
                switch (random)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        currentWeather = (currentWeather == "EXTRASUNNY" ? "CLEAR" : "EXTRASUNNY");
                        break;
                    case 6:
                    case 7:
                    case 8:
                        currentWeather = (currentWeather == "SMOG" ? "FOGGY" : "SMOG");
                        break;
                    case 9:
                    case 10:
                    case 11:
                        currentWeather = (currentWeather == "CLOUDS" ? "OVERCAST" : "CLOUDS");
                        break;
                    case 12:
                    case 13:
                    case 14:
                        currentWeather = (currentWeather == "CLOUDS" ? "OVERCAST" : "CLOUDS");
                        break;
                    case 15:
                        currentWeather = (currentWeather == "OVERCAST" ? "THUNDER" : "OVERCAST");
                        break;
                    case 16:
                        currentWeather = (currentWeather == "CLOUDS" ? "EXTRASUNNY" : "RAIN");
                        break;
                    case 17:
                    case 18:
                    case 19:
                    default:
                        currentWeather = (currentWeather == "FOGGY" ? "SMOG" : "FOGGY");
                        break;
                }
            }

        }
        #endregion

        #region Sync weather & time with clients
        /// <summary>
        /// Update the weather for all clients.
        /// </summary>
        /// <param name="newWeather"></param>
        /// <param name="blackoutNew"></param>
        /// <param name="dynamicWeatherNew"></param>
        private void UpdateWeather(string newWeather, bool blackoutNew, bool dynamicWeatherNew)
        {
            currentWeather = newWeather;
            blackout = blackoutNew;
            dynamicWeather = dynamicWeatherNew;
            TriggerClientEvent("vMenu:SetWeather", currentWeather, blackout, dynamicWeather);
        }

        /// <summary>
        /// Set a new random clouds type and opacity for all clients.
        /// </summary>
        /// <param name="removeClouds"></param>
        private void UpdateWeatherCloudsType(bool removeClouds)
        {
            if (removeClouds)
            {
                TriggerClientEvent("vMenu:SetClouds", 0f, "removed");
            }
            else
            {
                float opacity = float.Parse(new Random().NextDouble().ToString());
                string type = CloudTypes[new Random().Next(0, CloudTypes.Count)];
                TriggerClientEvent("vMenu:SetClouds", opacity, type);
            }
        }

        /// <summary>
        /// Set and sync the time to all clients.
        /// </summary>
        /// <param name="newHours"></param>
        /// <param name="newMinutes"></param>
        /// <param name="freezeTimeNew"></param>
        private void UpdateTime(int newHours, int newMinutes, bool freezeTimeNew)
        {
            currentHours = newHours;
            currentMinutes = newMinutes;
            freezeTime = freezeTimeNew;
            TriggerClientEvent("vMenu:SetTime", currentHours, currentMinutes, freezeTime);
        }
        #endregion

        #region Online Players Menu Actions
        /// <summary>
        /// Kick a specific player.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="kickReason"></param>
        private void KickPlayer([FromSource] Player source, int target, string kickReason = "You have been kicked from the server.")
        {
            if (IsPlayerAceAllowed(source.Handle, "vMenu.OnlinePlayers.Kick") || IsPlayerAceAllowed(source.Handle, "vMenu.Everything") ||
                IsPlayerAceAllowed(source.Handle, "vMenu.OnlinePlayers.All"))
            {
                // If the player is allowed to be kicked.
                Player targetPlayer = new PlayerList()[target];
                if (targetPlayer != null)
                {
                    if (!IsPlayerAceAllowed(targetPlayer.Handle, "vMenu.DontKickMe"))
                    {
                        TriggerEvent("vMenu:KickSuccessful", source.Name, kickReason, targetPlayer.Name);

                        KickLog($"Player: {source.Name} has kicked: {targetPlayer.Name} for: {kickReason}.");
                        TriggerClientEvent(player: source, eventName: "vMenu:Notify", args: $"The target player (<C>{targetPlayer.Name}</C>) has been kicked.");

                        // Kick the player from the server using the specified reason.
                        DropPlayer(targetPlayer.Handle, kickReason);
                        return;
                    }
                    // Trigger the client event on the source player to let them know that kicking this player is not allowed.
                    TriggerClientEvent(player: source, eventName: "vMenu:Notify", args: "Sorry, this player can ~r~not ~w~be kicked.");
                    return;
                }
                TriggerClientEvent(player: source, eventName: "vMenu:Notify", args: "An unknown error occurred. Report it here: vespura.com/vmenu");
            }
            else
            {
                BanManager.BanCheater(new PlayerList()[target]);
            }
        }

        /// <summary>
        /// Kill a specific player.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void KillPlayer([FromSource] Player source, int target)
        {
            if (IsPlayerAceAllowed(source.Handle, "vMenu.OnlinePlayers.Kill") || IsPlayerAceAllowed(source.Handle, "vMenu.Everything") ||
                IsPlayerAceAllowed(source.Handle, "vMenu.OnlinePlayers.All"))
            {
                Player targetPlayer = new PlayerList()[target];
                if (targetPlayer != null)
                {
                    // Trigger the client event on the target player to make them kill themselves. R.I.P.
                    TriggerClientEvent(player: targetPlayer, eventName: "vMenu:KillMe");
                    return;
                }
                TriggerClientEvent(player: source, eventName: "vMenu:Notify", args: "An unknown error occurred. Report it here: vespura.com/vmenu");
            }
            else
            {
                BanManager.BanCheater(new PlayerList()[target]);
            }
        }

        /// <summary>
        /// Teleport a specific player to another player.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void SummonPlayer([FromSource] Player source, int target)
        {
            if (IsPlayerAceAllowed(source.Handle, "vMenu.OnlinePlayers.Summon") || IsPlayerAceAllowed(source.Handle, "vMenu.Everything") ||
                IsPlayerAceAllowed(source.Handle, "vMenu.OnlinePlayers.All"))
            {
                // Trigger the client event on the target player to make them teleport to the source player.
                Player targetPlayer = new PlayerList()[target];
                if (targetPlayer != null)
                {
                    TriggerClientEvent(player: targetPlayer, eventName: "vMenu:GoToPlayer", args: source.Handle);
                    return;
                }
                TriggerClientEvent(player: source, eventName: "vMenu:Notify", args: "An unknown error occurred. Report it here: vespura.com/vmenu");
            }
            else
            {
                BanManager.BanCheater(new PlayerList()[target]);
            }
        }
        #endregion

        #region Send Permissions & Settings to the requesting client
        /// <summary>
        /// Send the permissions to the client that requested it.
        /// </summary>
        /// <param name="player"></param>
        private async void SendPermissionsAsync([FromSource] Player player)
        {
            // First send the vehicle & ped addons list
            TriggerClientEvent(player, "vMenu:SetupAddonCars", "vehicles", addonVehicles);
            TriggerClientEvent(player, "vMenu:SetupAddonPeds", "peds", addonPeds);
            TriggerClientEvent(player, "vMenu:SetupAddonWeapons", "weapons", addonWeapons);

            // Get Permissions
            Dictionary<string, bool> perms = new Dictionary<string, bool>();
            foreach (string ace in aceNames)
            {
                var realAceName = GetRealAceName(ace);
                var allowed = IsPlayerAceAllowed(player.Handle, realAceName);
                perms.Add(ace, allowed);
            }

            // Get Settings
            Dictionary<string, string> options = new Dictionary<string, string>
            {
                { "menuKey", GetConvarInt("vMenuToggleMenuKey", 244).ToString() ?? "244" },
                { "noclipKey", GetConvarInt("vMenuNoClipKey", 289).ToString() ?? "289" },
                { "disableSync", GetConvar("vMenuDisableTimeAndWeatherSync", "false") ?? "false"}
            };

            // Send Permissions
            TriggerClientEvent(player, "vMenu:SetPermissions", perms);

            // Send Settings
            await Delay(50);
            TriggerClientEvent(player, "vMenu:SetOptions", options);
            while (!UpdateChecker.CheckedForUpdates)
            {
                await Delay(0);
            }
            if (!UpToDate)
            {
                TriggerClientEvent(player, "vMenu:OutdatedResource");
            }
        }

        private string GetRealAceName(string inputString)
        {
            string outputString = inputString;
            var prefix = inputString.Substring(0, 2);

            if (prefix == "OP")
            {
                outputString = "vMenu.OnlinePlayers." + inputString.Substring(2);
            }
            else if (prefix == "PO")
            {
                outputString = "vMenu.PlayerOptions." + inputString.Substring(2);
            }
            else if (prefix == "VO")
            {
                outputString = "vMenu.VehicleOptions." + inputString.Substring(2);
            }
            else if (prefix == "VS")
            {
                outputString = "vMenu.VehicleSpawner." + inputString.Substring(2);
            }
            else if (prefix == "SV")
            {
                outputString = "vMenu.SavedVehicles." + inputString.Substring(2);
            }
            else if (prefix == "PA")
            {
                outputString = "vMenu.PlayerAppearance." + inputString.Substring(2);
            }
            else if (prefix == "TO")
            {
                outputString = "vMenu.TimeOptions." + inputString.Substring(2);
            }
            else if (prefix == "WO")
            {
                outputString = "vMenu.WeatherOptions." + inputString.Substring(2);
            }
            else if (prefix == "WP")
            {
                outputString = "vMenu.WeaponOptions." + inputString.Substring(2);
            }
            else if (prefix == "MS")
            {
                outputString = "vMenu.MiscSettings." + inputString.Substring(2);
            }
            else if (prefix == "VC")
            {
                outputString = "vMenu.VoiceChat." + inputString.Substring(2);
            }
            else
            {
                outputString = "vMenu." + inputString;
            }

            return outputString;
        }
        #endregion


        /// <summary>
        /// If enabled using convars, will log all kick actions to the server console as well as an external file.
        /// </summary>
        /// <param name="kickLogMesage"></param>
        private static void KickLog(string kickLogMesage)
        {
            if (GetConvar("vMenuLogKickActions", "false") == "true")
            {
                string file = LoadResourceFile(GetCurrentResourceName(), "vmenu.log") ?? "";
                DateTime date = DateTime.Now;
                string formattedDate = (date.Day < 10 ? "0" : "") + date.Day + "-" +
                    (date.Month < 10 ? "0" : "") + date.Month + "-" +
                    (date.Year < 10 ? "0" : "") + date.Year + " " +
                    (date.Hour < 10 ? "0" : "") + date.Hour + ":" +
                    (date.Minute < 10 ? "0" : "") + date.Minute + ":" +
                    (date.Second < 10 ? "0" : "") + date.Second;
                string outputFile = file + $"[\t{formattedDate}\t] [KICK ACTION] {kickLogMesage}\n";
                SaveResourceFile(GetCurrentResourceName(), "vmenu.log", outputFile, outputFile.Length);
                Debug.Write(kickLogMesage + "\n");
            }
        }
    }
}
