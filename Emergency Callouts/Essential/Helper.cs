﻿using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Rage;
using LSPD_First_Response.Mod.API;
using System.IO;
using Rage.Native;
using static EmergencyCallouts.Essential.Helper;
using System.Media;
using System.Net;

namespace EmergencyCallouts.Essential
{
    internal static class Project
    {
        #region Name
        internal static string Name
        {
            get { return Assembly.GetExecutingAssembly().GetName().ToString(); }
        }
        #endregion

        #region LocalVersion
        internal static string LocalVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5); }
        }
        #endregion

        #region SettingsPath
        internal static string SettingsPath
        {
            get { return "Plugins/LSPDFR/Emergency Callouts.ini"; }
        }
        #endregion
    }

    internal static class Helper
    {
        internal static Random random = new Random();
        internal static Ped MainPlayer => Game.LocalPlayer.Character;

        internal static bool PUBRemoteState;
        internal static bool TRERemoteState;
        internal static bool BURRemoteState;
        internal static bool DOMRemoteState;
        internal static bool SUSRemoteState;

        internal static class Entity
        {
            #region  Dismiss
            internal static void Dismiss(Ped ped)
            {
                if (ped.Exists()) { ped.Dismiss(); }
            }
            internal static void Dismiss(Vehicle vehicle)
            {
                if (vehicle.Exists()) { vehicle.Dismiss(); }
            }
            #endregion

            #region Delete
            internal static void Delete(Blip blip)
            {
                if (blip.Exists()) { blip.Delete(); }
            }
            internal static void Delete(Ped ped)
            {
                if (ped.Exists()) { ped.Delete(); }
            }
            internal static void Delete(Vehicle vehicle)
            {
                if (vehicle.Exists()) { vehicle.Delete(); }
            }
            internal static void Delete(Rage.Object Object)
            {
                if (Object.Exists()) { Object.Delete(); }
            }
            #endregion

            #region Enable
            internal static void Enable(Blip blip)
            {
                if (blip.Exists()) { blip.Alpha = 1f; }
            }
            #endregion

            #region Disable
            internal static void Disable(Blip blip)
            {
                if (blip.Exists()) { blip.Alpha = 0f; }
            }
            #endregion

            #region Kill
            internal static void Kill(Ped ped)
            {
                if (ped.Exists()) { ped.Kill(); }
            }
            #endregion

            #region Resurrect
            internal static void Resurrect(Ped ped)
            {
                if (ped.Exists() && ped.IsDead) { ped.Resurrect(); }
            }
            #endregion

            #region EnableRoute
            internal static void EnableRoute(Blip blip)
            {
                if (blip.Exists()) { blip.IsRouteEnabled = true; }
            }
            #endregion

            #region DisableRoute
            internal static void DisableRoute(Blip blip)
            {
                if (blip.Exists()) { blip.DisableRoute(); }
            }
            #endregion

            #region GetRandomMaleModel
            internal static string GetRandomMaleModel()
            {
                string[] maleModels =
                {
                    "a_m_m_afriamer_01", "ig_barry", "u_m_y_baygor", "a_m_o_beach_01", "a_m_y_beach_01",
                    "u_m_m_aldinapoli", "a_m_y_beach_02", "a_m_y_beachvesp_01", "a_m_y_beachvesp_02", "ig_benny",
                    "s_m_y_ammucity_01", "ig_beverly", "a_m_y_bevhills_01", "a_m_m_bevhills_02", "a_m_y_bevhills_02",
                    "ig_andreas", "cs_carbuyer", "s_m_o_busker_01", "ig_car3guy1", "g_m_m_chigoon_01",
                    "u_m_y_antonb", "g_m_m_chigoon_02", "csb_chin_goon", "u_m_y_chip", "ig_claypain",
                    "g_m_m_armboss_01", "s_m_m_cntrybar_01", "csb_customer", "a_m_y_cyclist_01", "ig_dale",
                    "g_m_m_armgoon_01", "ig_davenorton", "s_m_y_dealer_01", "ig_devin", "ig_dom",
                    "g_m_y_armgoon_02", "s_m_y_doorman_01", "a_m_y_downtown_01", "ig_drfriedlander", "a_m_m_eastsa_01",
                    "g_m_m_armlieut_01", "a_m_m_eastsa_02", "a_m_y_eastsa_02", "u_m_m_edtoh", "ig_fabien",
                    "s_m_m_autoshop_01", "g_m_y_famca_01", "g_m_y_famdnf_01", "g_m_y_famfor_01", "a_m_m_farmer_01",
                    "ig_money", "a_m_m_fatlatin_01", "ig_fbisuit_01", "u_m_y_fibmugger_01",
                    "g_m_y_azteca_01", "u_m_o_finguru_01", "csb_fos_rep", "player_one", "ig_g",
                    "g_m_y_ballaeast_01", "csb_g", "a_m_m_genfat_01", "a_m_m_genfat_02", "a_m_y_genstreet_01",
                    "g_m_y_ballaorig_01", "a_m_y_genstreet_02", "u_m_m_glenstank_01", "a_m_m_golfer_01", "s_m_m_hairdress_01",
                    "ig_ballasog", "s_m_m_highsec_02", "a_m_y_hipster_02", "csb_hugh", "a_m_m_indian_01",
                    "g_m_y_ballasout_01", "ig_jay_norris", "u_m_m_jesus_01", "u_m_m_jewelsec_01", "ig_jimmyboston",
                    "u_m_m_bankman", "u_m_m_jewelthief", "ig_josh", "ig_joeminuteman", "ig_jimmydisanto",
                    "ig_bankman", "cs_johnnyklebitz", "g_m_y_korean_01", "g_m_y_korlieut_01", "a_m_o_ktown_01",
                    "s_m_y_barman_01", "a_m_y_ktown_02", "ig_lamardavis", "a_m_y_latino_01", "ig_lazlow",
                };

                int num = random.Next(maleModels.Length);

                return maleModels[num];
            }
            #endregion

            #region GetRandomFemaleModel
            internal static string GetRandomFemaleModel()
            {
                string[] femaleModels =
                {
                    "ig_abigail", "ig_amandatownley", "csb_anita", "ig_ashley", "s_f_y_bartender_01",
                    "a_f_m_bevhills_01", "a_f_m_bevhills_02", "a_f_y_bevhills_03", "a_f_y_bevhills_04", "mp_f_boatstaff_01",
                    "a_f_y_business_02", "a_f_m_business_02", "a_f_y_business_01", "a_f_y_business_04", "u_f_y_comjane",
                    "ig_denise", "csb_denise_friend", "cs_debra", "a_f_m_eastsa_01", "a_f_y_eastsa_02",
                    "a_f_y_epsilon_01", "a_f_m_fatwhite_01", "s_f_m_fembarber", "a_f_y_fitness_01", "a_f_y_fitness_02",
                    "a_f_y_genhot_01", "a_f_o_genstreet_01", "a_f_y_golfer_01", "cs_guadalope", "a_f_y_hipster_02",
                    "a_f_y_hipster_03", "a_f_y_hipster_04", "a_f_o_indian_01", "ig_janet", "u_f_y_jewelass_01",
                    "ig_jewelass", "ig_kerrymcintosh", "a_f_o_ktown_01", "a_f_m_ktown_02", "ig_magenta",
                    "ig_maryann", "u_f_y_mistress", "ig_molly", "u_f_o_moviestar", "ig_mrsphillips",
                    "ig_mrs_thornhill", "ig_natalia", "ig_paige", "ig_patricia", "u_f_y_princess",
                    "a_f_m_salton_01", "a_f_o_salton_01", "a_f_y_rurmeth_01", "a_f_y_runner_01", "a_f_y_scdressy_01",
                    "ig_screen_writer", "s_f_m_shop_high", "s_f_y_shop_low", "s_f_y_shop_mid", "a_f_y_skater_01",
                    "a_f_o_soucent_01", "a_f_y_soucent_01", "a_f_m_soucent_02", "a_f_o_soucent_02", "a_f_y_soucent_02",
                    "a_f_y_soucent_03", "a_f_m_soucentmc_01", "u_f_y_spyactress", "s_f_m_sweatshop_01", "s_f_y_sweatshop_01",
                    "ig_tanisha", "a_f_y_tennis_01", "ig_tonya", "a_f_y_tourist_01", "a_f_y_tourist_02",
                    "g_f_y_vagos_01", "a_f_y_vinewood_01", "a_f_y_vinewood_02", "a_f_y_vinewood_03",
                    "a_f_y_vinewood_04", "u_m_m_willyfist", "a_f_y_yoga_01", "a_f_y_femaleagent", "mp_f_chbar_01",
                    "mp_f_counterfeit_01", "mp_f_execpa_01", "mp_f_execpa_02", "ig_jackie", "ig_isldj_04_d_01",
                    "s_f_y_beachbarstaff_01", "ig_patricia_02"
                };

                int num = random.Next(femaleModels.Length);

                return femaleModels[num];

            }
            #endregion
        }

        internal static class Display
        {
            #region HideSubtitle
            internal static void HideSubtitle()
            {
                Game.DisplaySubtitle("");
            }
            #endregion

            #region ArrestLine
            internal static void ArrestLine()
            {
                if (Settings.DisplayArrestLine == true)
                {
                    string[] lines = { "I need to pee, please hurry up.", "What exactly am I under arrest for?",
                                    "Why did I do this...", "I'd like to speak to my lawyer ASAP.", "Even god can't help you when I'm out of these cuffs!", "Remember my name...",
                                    "Do I get to sit with the prostitutes?", "How long is the ride to the station?", "I'm not answering any questions.", "I can't go to those cells! I'm germophopic!",
                                    "Where can I get those handcuffs?", "Do you have these cuffs in pink?", "I bet you got some of these cuffs at home.", "Do you even know who I am?"};

                    int num = random.Next(lines.Length);

                    Game.DisplaySubtitle("~r~Arrestee~s~: " + lines[num]);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Displayed ArrestLine");
                }
                else
                {
                    Game.LogTrivial("[INFO] Emergency Callouts: DisplayArrestLine is False; won't display arrest line");
                }
            }
            #endregion

            #region AttachMessage
            internal static void AttachMessage()
            {
                Game.DisplayNotification(Settings.NotificationIconDictionary, Settings.NotificationIconName, Settings.DispatchName, $"~{Settings.SubtitleColor}~" + Settings.AttachMessageSubtitle, Settings.AttachMessageText);
            }
            #endregion

            #region DetachMessage
            internal static void DetachMessage()
            {
                Game.DisplayNotification(Settings.NotificationIconDictionary, Settings.NotificationIconName, Settings.DispatchName, $"~{Settings.SubtitleColor}~" + Settings.DetachMessageSubtitle, Settings.DetachMessageText);
            }
            #endregion

            #region CalloutDetails
            internal static void CalloutDetails(string details)
            {
                Game.DisplayNotification(Settings.NotificationIconDictionary, Settings.NotificationIconName, Settings.DispatchName, $"~{Settings.SubtitleColor}~" + Settings.CalloutDetailsSubtitle, details);
            }
            #endregion

            #region ArriveSubtitle
            internal static void ArriveSubtitle(string action, string name, char color)
            {
                Game.DisplaySubtitle($"{action} the ~{color}~{name}~s~ in the ~y~area~s~.", 10000);
            }
            #endregion

            #region HintEndCallout
            internal static void HintEndCallout()
            {
                Game.DisplayHelp("Press ~y~End~s~ to end the callout.", 20000);
            }
            #endregion

            #region EndCallout
            internal static void EndCallout()
            {
                if (Game.IsKeyDown(Settings.EndKey))
                {
                    MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("random@arrests"), "generic_radio_enter", 3f, AnimationFlags.SecondaryTask | AnimationFlags.UpperBodyOnly);
                    Game.DisplayNotification($"~b~{Settings.Callsign}~s~: {Settings.EndCalloutRequest}");
                    GameFiber.Sleep(2000);
                    Play.CodeFourAudio();
                    GameFiber.Sleep(2700);
                    Functions.StopCurrentCallout();
                    GameFiber.Sleep(500);
                }
            }
            #endregion
        }

        internal static class Vehicles
        {
            #region GetRandomFourDoor
            internal static string GetRandomFourDoor()
            {
                string[] vehicles = 
                {  
                    "BISON", "BISON2", "BALLER", "BALLER2", "BALLER3", "BALLER4", "CAVALCADE", "CAVALCADE2", "CONTENDER", "DUBSTA", "FQ2", "GRANGER", "VIRGO",
                    "GRESLEY", "HABANERO", "DUKES", "BJXL", "CAVALCADE", "F620", "FELON", "FELON2", "HUNTLEY", "LANDSTALKER", "LANDSTALKER2", "MESA", "PRIMO",
                    "EMPEROR", "FUGITIVE", "INTRUDER", "PREMIER", "SURGE", "TAILGATER", "TAILGATER2", "EMPEROR2", "GLENDALE", "DILETTANTE", 
                    "WARRENER", "DUKES", "VIRGO", "BUFFALO", "ASEA", "RANCHERXL", "CASCO", "EXEMPLAR", "SENTINEL", "CHINO", "SULTAN", "BUFFALO2", 
                    "REBEL", "SCHWARZER", "CARBONIZZARE", "SULTAN", "EXEMPLAR", "MASSACRO", "PRAIRIE", "ASTEROPE", "WASHINGTON", "XLS", "REBLA",
                };

                int num = random.Next(vehicles.Length);
                return vehicles[num];
            }
            #endregion

            #region GetRandomMotorcycle
            internal static string GetRandomMotorcycle()
            {
                string[] motorcycles =
                {
                    "AKUMA", "AVARUS", "BAGGER", "BATI", "BATI2", "BF400", "CARBONRS", "CHIMERA", "CLIFFHANGER", "DAEMON", "DAEMON2", "DIABLOUS",
                    "DIABLOUS2", "DOUBLE", "ENDURO", "ESSKEY", "FCR", "FCR2", "MANCHEZ", "NEMESIS", "PCJ", "RATBIKE", "RUFFIAN", "SANCHEZ", 
                    "SANCTUS", "SOVEREIGN", "THRUST", "VADER", "VINDICATOR", "WOLFSBANE", "ZOMBIEA", "ZOMBIEB", "SANCHEZ2", "DEFILER"
                };

                int num = random.Next(motorcycles.Length);
                return motorcycles[num];
            }
            #endregion

            #region GetRandomVan
            internal static string GetRandomVan()
            {
                string[] vans = { "SPEEDO", "BURRITO", "RUMPO", "RUMPO2", "RUMPO3", "BURRITO2", "BURRITO3", "BURRITO4", "PONY2", "SPEEDO4", "YOUGA" };

                int num = random.Next(vans.Length);
                return vans[num];
            }
            #endregion
        }

        internal static class Play
        {
            #region PursuitAudio
            internal static void PursuitAudio()
            {
                if (Settings.PlayPursuitAudio == true)
                {
                    Functions.PlayScannerAudio("OFFICERS_REPORT CRIME_RESIST_ARREST");
                }
            }
            #endregion

            #region CodeFourAudio
            internal static void CodeFourAudio()
            {
                Functions.PlayScannerAudio("REPORT_RESPONSE_COPY CODE_FOUR NO_UNITS_REQUIRED");
            }
            #endregion
        }

        internal static class Check
        {
            #region EndKeyDown
            internal static void EndKeyDown()
            {
                if (Game.IsKeyDown(Settings.EndKey)) { Display.EndCallout(); }
            }
            #endregion

            #region NaturalEnding
            internal static void NaturalEnding(Ped ped)
            {
                if (ped.Exists())
                {
                    if (ped.IsCuffed)
                    {
                        Functions.StopCurrentCallout();
                        Play.CodeFourAudio();
                    }
                    else if (ped.IsDead && MainPlayer.IsInAnyPoliceVehicle)
                    {
                        Display.HintEndCallout();
                    }
                }
            }
            #endregion

            #region PreventDistanceCrash
            internal static void PreventDistanceCrash(Vector3 CalloutPosition, bool OnScene, bool PedFound)
            {
                if (MainPlayer.Position.DistanceTo(CalloutPosition) > 400f && OnScene == true && PedFound == true)
                {
                    Game.LogTrivial("[INFO] Emergency Callouts: Too far from callout position, ending callout to prevent crash");
                    Functions.StopCurrentCallout();
                    Play.CodeFourAudio();
                }
            }
            #endregion

            #region PreventResponderCrash
            internal static void PreventResponderCrash(Ped ped)
            {
                if (ped.Exists()) 
                {
                    foreach (Ped FirstResponder in World.GetAllPeds())
                    {
                        if (FirstResponder.Position.DistanceTo(ped.Position) < 5f)
                        {
                            if (FirstResponder.Model.Name.ToLower() == "s_m_m_paramedic_01") // Ambulance
                            {
                                Functions.StopCurrentCallout();
                                Play.CodeFourAudio();
                            }
                            else if (FirstResponder.Model.Name.ToLower() == "s_m_m_doctor_01") // Coroner
                            {
                                Functions.StopCurrentCallout();
                                Play.CodeFourAudio();
                            }
                            else if (FirstResponder.Model.Name.ToLower() == "s_m_y_fireman_01") // First Responder
                            {
                                Functions.StopCurrentCallout();
                                Play.CodeFourAudio();
                            }
                        }
                    }
                }
            }

            internal static void PreventParamedicCrash(Ped ped, Ped ped2)
            {
                if (ped.Exists() && ped2.Exists())
                {
                    foreach (Ped FR in World.GetAllPeds())
                    {
                        if (FR.Position.DistanceTo(ped.Position) < 5f || FR.Position.DistanceTo(ped2.Position) < 5f)
                        {
                            if (FR.Model.Name.ToLower() == "s_m_m_paramedic_01")
                            {
                                Functions.StopCurrentCallout();
                                Play.CodeFourAudio();
                            }
                            else if (FR.Model.Name.ToLower() == "s_m_m_doctor_01")
                            {
                                Functions.StopCurrentCallout();
                                Play.CodeFourAudio();
                            }
                            else if (FR.Model.Name.ToLower() == "s_m_y_fireman_01")
                            {
                                Functions.StopCurrentCallout();
                                Play.CodeFourAudio();
                            }
                        }
                    }
                }
            }
            #endregion

            #region RemoteState
            internal static void RemoteState()
            {
                try
                {
                    WebClient client = new WebClient();

                    string states = client.DownloadString("https://pastebin.com/raw/hS4WP9w6");

                    Game.LogTrivial("[INFO] Emergency Callouts: Downloaded remote callout states");

                    // Public Intoxication
                    #region StateCheck
                    if (states.Contains("PUB"))
                    {
                        PUBRemoteState = true;
                        Game.LogTrivial("[INFO] Emergency Callouts: PUBRemoteState = true");
                    }
                    else
                    {
                        PUBRemoteState = false;
                        Game.LogTrivial("[WARN] Emergency Callouts: PUBRemoteState = false");
                        Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~b~Public Intoxication", "has been ~y~remotely disabled~s~.\nThis was ~g~intentionally done~s~ by the developer.");
                    }
                    #endregion

                    // Burglary
                    #region StateCheck
                    if (states.Contains("BUR"))
                    {
                        BURRemoteState = true;
                        Game.LogTrivial("[INFO] Emergency Callouts: BURRemoteState = true");
                    }
                    else
                    {
                        BURRemoteState = false;
                        Game.LogTrivial("[WARN] Emergency Callouts: BURRemoteState = false");
                        Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~b~Burglary In Progress", "has been ~y~remotely disabled~s~.\nThis was ~g~intentionally done~s~ by the developer.");
                    }
                    #endregion

                    // Trespassing
                    #region StateCheck
                    if (states.Contains("TRE"))
                    {
                        TRERemoteState = true;
                        Game.LogTrivial("[INFO] Emergency Callouts: TRERemoteState = true");
                    }
                    else
                    {
                        TRERemoteState = false;
                        Game.LogTrivial("[WARN] Emergency Callouts: TRERemoteState = false");
                        Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~b~Trespassing", "has been ~y~remotely disabled~s~.\nThis was ~g~intentionally done~s~ by the developer.");
                    }
                    #endregion

                    // Domestic Violence
                    #region StateCheck
                    if (states.Contains("DOM"))
                    {
                        DOMRemoteState = true;
                        Game.LogTrivial("[INFO] Emergency Callouts: DOMRemoteState = true");
                    }
                    else
                    {
                        DOMRemoteState = false;
                        Game.LogTrivial("[WARN] Emergency Callouts: DOMRemoteState = false");
                        Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~b~Domestic Violence", "has been ~y~remotely disabled~s~.\nThis was ~g~intentionally done~s~ by the developer.");
                    }
                    #endregion

                    // Suspicious Activity
                    #region StateCheck
                    if (states.Contains("SUS"))
                    {
                        SUSRemoteState = true;
                        Game.LogTrivial("[INFO] Emergency Callouts: SUSRemoteState = true");
                    }
                    else
                    {
                        SUSRemoteState = false;
                        Game.LogTrivial("[WARN] Emergency Callouts: SUSRemoteState = false");
                        Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~b~Suspicious Activity", "has been ~y~remotely disabled~s~.\nThis was ~g~intentionally done~s~ by the developer.");
                    }
                    #endregion
                }
                catch (WebException)
                {
                    Game.LogTrivial("[ERROR] Emergency Callouts: Could not check online callout states, callout states are set to true");

                    PUBRemoteState = true;
                    BURRemoteState = true;
                    TRERemoteState = true;
                    DOMRemoteState = true;
                    SUSRemoteState = true;
                }
            }
            #endregion
        }

        internal class Log
        {
            #region CalloutAccepted
            internal static void CalloutAccepted(string CalloutMessage, int ScenarioNumber)
            {
                Game.LogTrivial($"[TRACE] Emergency Callouts: Created callout ({CalloutMessage}, Scenario {ScenarioNumber})");
            }
            #endregion

            #region CalloutEnded
            internal static void CalloutEnded(string CalloutMessage, int ScenarioNumber)
            {
                Game.LogTrivial($"[TRACE] Emergency Callouts: Ended callout ({CalloutMessage}, Scenario {ScenarioNumber})");
            }
            #endregion

            #region CalloutException
            internal static void CalloutException(object o, string method, Exception e)
            {
                Game.LogTrivial($"[ERROR] Emergency Callouts: {e.Message} At {o.GetType().Name}.{method}()");
                Game.LogTrivial($"[INFO] Emergency Callouts: Using version {Project.LocalVersion}");

                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~r~Issue detected!", "Please fill in a ~g~bug report form~s~.\nThat can be found on the ~y~Emergency Callouts Page~s~.");

                try
                {
                    WebClient hitUpdater = new WebClient();
                    hitUpdater.DownloadString("https://pastebin.com/raw/Li5KFks3");
                    Game.LogTrivial("[INFO] Emergency Callouts: Sent an error hit to a remote counter");
                }
                catch (WebException webEx)
                {
                    Game.LogTrivial("[ERROR] Emergency Callouts: " + webEx.Message);
                }
            }
            #endregion
        }

        internal static class FileExists
        {
            #region EmergencyCalloutsINI
            public static bool EmergencyCalloutsINI()
            {
                if (File.Exists(Project.SettingsPath))
                {
                    Game.LogTrivial("[INFO] Emergency Callouts: Found 'Emergency Callouts.ini'");
                    return true;
                }
                else
                {
                    Game.LogTrivial("[WARN] Emergency Callouts: Did not find 'Emergency Callouts.ini', set defaults");
                    return false;
                }
            }
            #endregion

            #region StopThePed
            public static bool StopThePed(bool log)
            {
                if (File.Exists("Plugins/LSPDFR/StopThePed.dll"))
                {
                    if (log == true)
                    {
                        Game.LogTrivial("[INFO] Emergency Callouts: Found 'StopThePed.dll'");
                    }
                    return true;
                }
                else
                {
                    if (log == true)
                    {
                        Game.LogTrivial("[WARN] Emergency Callouts: Did not find 'StopThePed.dll'");
                    }
                    return false;
                }
            }
            #endregion

            #region UltimateBackup
            public static bool UltimateBackup(bool log)
            {
                if (File.Exists("Plugins/LSPDFR/UltimateBackup.dll"))
                {
                    if (log == true)
                    {
                        Game.LogTrivial("[INFO] Emergency Callouts: Found 'UltimateBackup.dll'");
                    }
                    return true;
                }
                else
                {
                    if (log == true)
                    {
                        Game.LogTrivial("[WARN] Emergency Callouts: Did not find 'UltimateBackup.dll'");
                    }
                    return false;
                }
            }

            #endregion
        }

        internal static class Colors
        {
            #region Red
            /// <summary>
            /// Criminals, Deadly, Safety Hazard.
            /// </summary>
            public static Color Red = Color.FromArgb(224, 50, 50);
            #endregion

            #region Yellow
            /// <summary>
            /// Routes, Areas, Suspects.
            /// </summary>
            public static Color Yellow = Color.FromArgb(240, 200, 80);
            #endregion

            #region Blue
            /// <summary>
            /// Law Enforcement, Security.
            /// </summary>
            public static Color Blue = Color.FromArgb(93, 182, 229);
            #endregion

            #region Orange
            /// <summary>
            /// Caution
            /// </summary>
            public static Color Orange = Color.FromArgb(234, 142, 80);
            #endregion

            #region Green
            /// <summary>
            /// Civilians
            /// </summary>
            public static Color Green = Color.FromArgb(114, 204, 114);
            #endregion

            #region Purple
            /// <summary>
            /// Animals, obstacles.
            /// </summary>
            public static Color Purple = Color.FromArgb(171, 60, 230);
            #endregion
        }
    }

    internal static class Inventory
    {
        #region GiveRandomWeapon
        internal enum WeaponType
        {
            Melee,
            Handgun,
            SubmachingeGun,
            AssaultRifle,
            Shotgun,
            MachineGun,
            SniperRifle,
        }

        internal static void GiveRandomWeapon(this Ped ped, Enum weaponType, short ammoCount, bool equipNow)
        {
            #region Melee
            if (weaponType.ToString() == WeaponType.Melee.ToString())
            {
                string[] meleeWeapons =
                {
                    "WEAPON_DAGGER",
                    "WEAPON_BAT",
                    "WEAPON_BOTTLE",
                    "WEAPON_CROWBAR",
                    "WEAPON_UNARMED",
                    "WEAPON_FLASHLIGHT",
                    "WEAPON_GOLFCLUB",
                    "WEAPON_HAMMER",
                    "WEAPON_HATCHET",
                    "WEAPON_KNUCKLE",
                    "WEAPON_KNIFE",
                    "WEAPON_MACHETE",
                    "WEAPON_SWITCHBLADE",
                    "WEAPON_NIGHTSTICK",
                    "WEAPON_WRENCH",
                    "WEAPON_BATTLEAXE",
                    "WEAPON_POOLCUE",
                };

                int num = random.Next(meleeWeapons.Length);
                if (ped.Exists()) { ped.Inventory.GiveNewWeapon(meleeWeapons[num], ammoCount, equipNow); }

            }
            #endregion

            #region Handgun
            if (weaponType.ToString() == WeaponType.Handgun.ToString())
            {
                string[] handguns =
                {
                    "WEAPON_PISTOL",
                    "WEAPON_COMBATPISTOL",
                    "WEAPON_PISTOL50",
                    "WEAPON_VINTAGEPISTOL",
                    "WEAPON_SNSPISTOL",
                    "WEAPON_HEAVYPISTOL",
                    "WEAPON_CERAMICPISTOL",
                };

                int num = random.Next(handguns.Length);
                if (ped.Exists()) { ped.Inventory.GiveNewWeapon(handguns[num], ammoCount, equipNow); }
            }
            #endregion

            #region Submachine Gun
            if (weaponType.ToString() == WeaponType.SubmachingeGun.ToString())
            {
                string[] submachineGuns =
                {
                    "WEAPON_MICROSMG",
                    "WEAPON_SMG",
                    "WEAPON_ASSAULTSMG",
                    "WEAPON_MINISMG"
                };

                int num = random.Next(submachineGuns.Length);
                if (ped.Exists()) { ped.Inventory.GiveNewWeapon(submachineGuns[num], ammoCount, equipNow); }

            }
            #endregion

            #region Assault Rifle
            if (weaponType.ToString() == WeaponType.SubmachingeGun.ToString())
            {
                string[] rifles =
                {
                    "WEAPON_CARBINERIFLE",
                    "WEAPON_ASSAULTRIFLE",
                    "WEAPON_ADVANCEDRIFLE",
                    "WEAPON_SPECIALCARBINE",
                    "WEAPON_BULLPUPRIFLE",
                };

                int num = random.Next(rifles.Length);
                if (ped.Exists()) { ped.Inventory.GiveNewWeapon(rifles[num], ammoCount, equipNow); }

            }
            #endregion

            #region Shotgun
            if (weaponType.ToString() == WeaponType.SubmachingeGun.ToString())
            {
                string[] shotguns =
                {
                    "WEAPON_PUMPSHOTGUN",
                    "WEAPON_SAWNOFFSHOTGUN",
                    "WEAPON_BULLPUPSHOTGUN",
                    "WEAPON_HEAVYSHOTGUN",
                    "WEAPON_DBSHOTGUN",
                    "WEAPON_COMBATSHOTGUN"
                };

                int num = random.Next(shotguns.Length);
                if (ped.Exists()) { ped.Inventory.GiveNewWeapon(shotguns[num], ammoCount, equipNow); }

            }
            #endregion

            #region MachineGun
            if (weaponType.ToString() == WeaponType.SubmachingeGun.ToString())
            {
                string[] machineGuns =
                {
                    "WEAPON_MG",
                    "WEAPON_COMBATMG",
                    "WEAPON_GUSENBERG"
                };

                int num = random.Next(machineGuns.Length);
                if (ped.Exists()) { ped.Inventory.GiveNewWeapon(machineGuns[num], ammoCount, equipNow); }

            }
            #endregion

            #region Sniper Rifles
            if (weaponType.ToString() == WeaponType.SubmachingeGun.ToString())
            {
                string[] sniperRifles =
                {
                    "WEAPON_SNIPERRIFLE",
                    "WEAPON_HEAVYSNIPER",
                    "WEAPON_MARKSMANRIFLE"
                };

                int num = random.Next(sniperRifles.Length);
                if (ped.Exists()) { ped.Inventory.GiveNewWeapon(sniperRifles[num], ammoCount, equipNow); }

            }
            #endregion
        }
        #endregion

        #region ClearInventory
        internal static void ClearInventory(this Ped ped)
        {
            if (ped.Exists()) { ped.Inventory.Weapons.Clear(); }
        }
        #endregion
    }

    internal static class ExtensionMethods
    {
        #region SetDefaults
        /// <summary>
        /// Sets ped persistency and blocks permanent events.
        /// </summary>
        internal static void SetDefaults(this Ped ped)
        {
            if (ped.Exists())
            {
                ped.BlockPermanentEvents = true;
                ped.IsPersistent = true;
            }
        }
        #endregion

        #region SetIntoxicated
        /// <summary>
        /// Sets ped to drunk with a walking style
        /// </summary>
        /// <param name="ped"></param>
        internal static void SetIntoxicated(this Ped ped)
        {
            AnimationSet animSet = new AnimationSet("move_m@drunk@verydrunk");
            animSet.LoadAndWait();
            ped.MovementAnimationSet = animSet;

            if (FileExists.StopThePed(false))
            {
                StopThePed.API.Functions.setPedAlcoholOverLimit(ped, true);
            }
        }
        #endregion

        #region ScaleForPed
        internal static void ScaleForPed(this Blip blip)
        {
            if (blip.Exists()) { blip.Scale = 0.75f; }
        }
        #endregion

        #region IsDetained
        internal static bool IsDetained(this Ped ped)
        {
            if ((Functions.IsPedStoppedByPlayer(ped) || StopThePed.API.Functions.isPedStopped(ped)) && FileExists.StopThePed(false) == true && ped.Exists())
            {
                return true;
            }
            else return false;
        }
        #endregion
    }
}