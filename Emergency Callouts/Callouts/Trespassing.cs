﻿using EmergencyCallouts.Essential;
using LSPD_First_Response.Engine.UI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using System;
using static EmergencyCallouts.Essential.Color;
using static EmergencyCallouts.Essential.Helper;
using Entity = EmergencyCallouts.Essential.Helper.Entity;

namespace EmergencyCallouts.Callouts
{
    [CalloutInfo("Trespassing", CalloutProbability.Medium)]
    public class Trespassing : Callout
    {
        readonly int ScenarioNumber = random.Next(1, 6);

        bool CalloutActive;
        bool OnScene;
        bool PedFound;
        bool PedDetained;
        bool PedArrested;
        bool DialogueStarted;

        // Main
        #region Positions
        Vector3 Entrance;
        Vector3 Center;

        readonly Vector3[] CalloutPositions =
        {
            new Vector3(512f, -610.72f, 24.43f),   // La Mesa Railyard
            new Vector3(-1106.7f, -1975.5f, 0f),   // LSIA Scrapyard
            new Vector3(2165.78f, 4758.762f, 42f), // McKenzie Airstrip 
        };
        #endregion

        // La Mesa Railyard
        #region Positions
        readonly Vector3[] RailyardHidingPositions =
        {
            new Vector3(488.5f, -631f, 24.98f),   // Alley
            new Vector3(523.5f, -563f, 24.765f),  // Power Unit 1
            new Vector3(524.5f, -592f, 24.788f),  // Power Unit 2
            new Vector3(487f, -555.5f, 25.992f),  // Container
            new Vector3(498f, -532f, 24.75114f),  // Pipes
            new Vector3(532f, -560.50f, 24.800f), // Between Train Carrier
            new Vector3(492f, -588.5f, 24.7189f), // Corner Garbage Collector
            new Vector3(493f, -579.913f, 24.57f), // Small Wooden Crate
            new Vector3(481.693f, -591f, 24.75f), // Alley 2
        };

        readonly float[] RailyardHidingPositionsHeadings =
        {
            146f,
            187f,
            15f,
            350f,
            80f,
            80f,
            1f,
            186f,
            300f,
        };

        readonly Vector3[] RailyardManagerPositions =
        {
            new Vector3(495.3361f, -585.4279f, 24.73708f), // Boxes
            new Vector3(495.5332f, -577.2258f, 24.65661f), // Boxes 2
            new Vector3(485.5925f, -634.8498f, 24.92816f), // Crates
        };

        readonly float[] RailyardManagerHeadings =
        {
            90f, // Boxes
            55f, // Boxes 2
            112f // Crates
        };

        readonly Vector3[] RailyardFirePositions =
        {
            new Vector3(485f, -636.5899f, 25.02777f), // Alley
            new Vector3(522.1501f, -592.4759f, 25f),  // Power Unit
            new Vector3(500f, -609.7313f, 24.75132f), // Building 1
            new Vector3(493f, -573.0732f, 24.59121f)  // Building 2
        };

        readonly Vector3[] RailyardWeldingPositions =
        {
            new Vector3(491.9123f, -554.114f, 24.7505f), // Container
        };

        readonly float[] RailyardWeldingHeadings =
        {
            212f,
        };
        #endregion

        // LSC Scrapyard
        #region Positions
        readonly Vector3[] ScrapyardHidingPositions =
        {
            new Vector3(-1155.449f, -2030.024f, 13.16065f), // Lifter
            new Vector3(-1179.068f, -2081.575f, 13.83974f), // Red Small Container
            new Vector3(-1180.712f, -2072.301f, 14.45590f), // Garbage container
            new Vector3(-1171.513f, -2071.196f, 13.96500f), // Abandoned Train Carrier
            new Vector3(-1181.182f, -2046.646f, 13.92571f), // Abandoned bus
        };

        readonly float[] ScrapyardHidingPositionsHeadings =
        {
            222f,
            346f,
            270f,
            145f,
            156f,
        };

        readonly Vector3[] ScrapyardManagerPositions =
        {
            new Vector3(-1161.378f, -2061.15f, 13.77043f),  // Huge Gas Containers
            new Vector3(-1157.412f, -2032.295f, 13.16054f), // Industrial Crane
            new Vector3(-1180.037f, -2058.742f, 14.09963f), // Casual Spot
        };

        readonly float[] ScrapyardManagerHeadings =
        {
            224f, 
            343f, 
            262f,
        };

        readonly Vector3[] ScrapyardFirePositions =
        {
            new Vector3(-1157.412f, -2032.295f, 13.16054f), // Industrial Crane
            new Vector3(-1161.378f, -2061.15f, 13.77043f),  // Huge Gas Containers
            new Vector3(-1167.67f, -2044.833f, 14.02154f),  // Small Boxes
        };

        readonly Vector3[] ScrapyardWeldingPositions =
        {
            new Vector3(), //
        };

        readonly float[] ScrapyardWeldingHeadings =
        {
            0f,
        };
        #endregion

        // McKenzie Airstrip
        #region Positions
        readonly Vector3[] AirstripHidingPositions =
        {
            new Vector3(2149.073f, 4781.637f, 41.01651f), // Behind Hangar Garbage Container
            new Vector3(2121.007f, 4783.326f, 40.97028f), // Hangar Desk
            new Vector3(2120.194f, 4774.568f, 41.17796f), // Outside Cement Mixer
            new Vector3(2093.352f, 4738.548f, 41.3352f),  // Gas Tank
            new Vector3(2112.155f, 4759.638f, 41.25103f), // Gas Tank & Scrap holder
        };

        readonly float[] AirstripHidingPositionsHeadings =
        {
            200f,
            220f,
            120f,
            190f,
            5f,
        };

        readonly Vector3[] AirstripManagerPositions =
        {
            new Vector3(2137.664f, 4791.458f, 40.9702f), // Hangar Table
            new Vector3(2135.579f, 4772.35f, 40.97029f), // Red Tool Storage
            new Vector3(2144.962f, 4776.65f, 40.97034f), // Pile of Boxes
        };

        readonly float[] AirstripManagerHeadings =
        {
            290f,
            190f,
            317f,
        };

        readonly Vector3[] AirstripFirePositions =
        {
            new Vector3(2144.962f, 4776.65f, 40.97034f), // Pile of Boxes
            new Vector3(2108.356f, 4762.68f, 41.04375f), // Gas Tank
            new Vector3(2125.861f, 4774.83f, 40.97033f), // Pile of Boxes 2
        };

        readonly Vector3[] AirstripWeldingPositions =
        {
            new Vector3(), //
        };

        readonly float[] AirstripWeldingHeadings =
        {
            0f,
        };
        #endregion

        Ped Suspect;
        Ped Guard;

        Blip SuspectBlip;
        Blip GuardBlip;
        Blip EntranceBlip;
        Blip SearchArea;

        public override bool OnBeforeCalloutDisplayed()
        {
            CalloutPosition = new Vector3(0, 0, 3000);
            foreach (Vector3 loc in CalloutPositions)
            {
                if (Vector3.Distance(MainPlayer.Position, loc) < Vector3.Distance(MainPlayer.Position, CalloutPosition))
                {
                    CalloutPosition = loc;
                }
            }

            ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, Settings.SearchAreaSize / 2.5f);

            CalloutMessage = Settings.TrespassingName;

            Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_TRESPASSING IN_OR_ON_POSITION", CalloutPosition);

            return base.OnBeforeCalloutDisplayed();
        }

        public override void OnCalloutNotAccepted()
        {
            Game.LogTrivial("[INFO] Emergency Callouts: Callout not accepted");
            Functions.PlayScannerAudio("PED_RESPONDING_DISPATCH");

            base.OnCalloutNotAccepted();
        }

        public override bool OnCalloutAccepted()
        {
            try
            {
                // Callout Accepted
                Log.CalloutAccepted(CalloutMessage, ScenarioNumber);

                // Attach Message
                Display.AttachMessage();

                // Callout Details
                Display.CalloutDetails(Settings.TrespassingDetails);

                // EntranceBlip
                EntranceBlip = new Blip(Entrance);

                // Suspect
                Suspect = new Ped(CalloutPosition);
                Suspect.SetDefaults();
                Game.LogTrivial($"[TRACE] Emergency Callouts: Created Suspect ({Suspect.Model.Name}) at " + Suspect.Position);

                // SuspectBlip
                SuspectBlip = Suspect.AttachBlip();
                SuspectBlip.SetColor(Colors.Yellow);
                SuspectBlip.ScaleForPed();
                Entity.Disable(SuspectBlip);

                CalloutHandler();
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "OnCalloutAccepted", e);
            }

            return base.OnCalloutAccepted();
        }

        private void CalloutHandler()
        {
            #region CalloutHandler
            try
            {
                CalloutActive = true;

                // Positioning
                #region Positioning
                if (CalloutPosition == CalloutPositions[0]) // La Mesa Railyard
                {
                    Center = new Vector3(512f, -610.72f, 24.43f);
                    Entrance = new Vector3(510.59f, -666.95f, 24.40f);
                    EntranceBlip.Position = Entrance;
                }
                else if (CalloutPosition == CalloutPositions[1]) // LSC Scrapyard
                {
                    Center = new Vector3(-1170.024f, -2045.655f, 14.22536f);
                    Entrance = new Vector3(-1156.879f, -1988.801f, 13.16036f);
                    EntranceBlip.Position = Entrance;
                }
                else if (CalloutPosition == CalloutPositions[2]) // McKenzie Airstrip
                {
                    Center = new Vector3(2118.948f, 4802.422f, 41.19594f);
                    Entrance = new Vector3(2165.78f, 4758.762f, 42f);
                    EntranceBlip.Position = Entrance;
                }
                #endregion

                // Scenario Deciding
                switch (ScenarioNumber)
                {
                    case 1:
                        Scenario1();
                        break;
                    case 2:
                        Scenario2();
                        break;
                    case 3:
                        Scenario3();
                        break;
                    case 4:
                        Scenario4();
                        break;
                    case 5:
                        Scenario5();
                        break;
                }

                // Enabling Route
                Entity.EnableRoute(EntranceBlip);
                Game.LogTrivial("[INFO] Emergency Callouts: Enabled route to EntranceBlip");
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "CalloutHandler", e);
            }
            #endregion
        }

        private void RetrieveHidingPosition()
        {
            #region Positions
            if (CalloutPosition == CalloutPositions[0]) // La Mesa Railyard
            {
                int RailyardHidingSpotNum = random.Next(RailyardHidingPositions.Length);
                Suspect.Position = RailyardHidingPositions[RailyardHidingSpotNum];
                Suspect.Heading = RailyardHidingPositionsHeadings[RailyardHidingSpotNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            else if (CalloutPosition == CalloutPositions[1]) // LSC Scrapyard
            {
                int ScrapyardHidingSpotNum = random.Next(ScrapyardHidingPositions.Length);
                Suspect.Position = ScrapyardHidingPositions[ScrapyardHidingSpotNum];
                Suspect.Heading = ScrapyardHidingPositionsHeadings[ScrapyardHidingSpotNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            else if (CalloutPosition == CalloutPositions[2]) // McKenzie Airstrip
            {
                int AirstripHidingSpotNum = random.Next(AirstripHidingPositions.Length);
                Suspect.Position = AirstripHidingPositions[AirstripHidingSpotNum];
                Suspect.Heading = AirstripHidingPositionsHeadings[AirstripHidingSpotNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            #endregion
        }

        private void RetrieveManagerPosition()
        {
            #region Positions
            Entity.Delete(Suspect);

            if (CalloutPosition == CalloutPositions[0]) // La Mesa Railyard
            {
                Suspect = new Ped("ig_lifeinvad_01", CalloutPosition, 0f);
                Suspect.SetDefaults();

                SuspectBlip = Suspect.AttachBlip();
                SuspectBlip.SetColor(Colors.Yellow);
                SuspectBlip.ScaleForPed();
                Entity.Disable(SuspectBlip);

                int ManagerPositionNum = random.Next(RailyardManagerPositions.Length);
                Suspect.Position = RailyardManagerPositions[ManagerPositionNum];
                Suspect.Heading = RailyardManagerHeadings[ManagerPositionNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            else if (CalloutPosition == CalloutPositions[1]) // LSC Scrapyard
            {
                Suspect = new Ped("ig_chef", CalloutPosition, 0f);
                Suspect.SetDefaults();

                SuspectBlip = Suspect.AttachBlip();
                SuspectBlip.SetColor(Colors.Yellow);
                SuspectBlip.ScaleForPed();
                Entity.Disable(SuspectBlip);

                int ManagerPositionNum = random.Next(ScrapyardManagerPositions.Length);
                Suspect.Position = ScrapyardManagerPositions[ManagerPositionNum];
                Suspect.Heading = ScrapyardManagerHeadings[ManagerPositionNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            else if (CalloutPosition == CalloutPositions[2]) // McKenzie Airstrip
            {
                Suspect = new Ped("player_two", CalloutPosition, 0f);
                Suspect.SetDefaults();

                SuspectBlip = Suspect.AttachBlip();
                SuspectBlip.SetColor(Colors.Yellow);
                SuspectBlip.ScaleForPed();
                Entity.Disable(SuspectBlip);

                int ManagerPositionNum = random.Next(AirstripManagerPositions.Length);
                Suspect.Position = AirstripManagerPositions[ManagerPositionNum];
                Suspect.Heading = AirstripManagerHeadings[ManagerPositionNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            #endregion
        }

        private void RetrieveArsonPosition()
        {
            #region Positions
            if (CalloutPosition == CalloutPositions[0]) // La Mesa Railyard
            {
                int FirePositionNum = random.Next(RailyardFirePositions.Length);
                Suspect.Position = RailyardFirePositions[FirePositionNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            else if (CalloutPosition == CalloutPositions[1]) // LSC Scrapyard
            {
                int FirePositionNum = random.Next(ScrapyardFirePositions.Length);
                Suspect.Position = ScrapyardFirePositions[FirePositionNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            else if (CalloutPosition == CalloutPositions[2]) // McKenzie Airstrip
            {
                int FirePositionNum = random.Next(AirstripFirePositions.Length);
                Suspect.Position = AirstripFirePositions[FirePositionNum];
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@base"), "base", 4f, AnimationFlags.StayInEndFrame);
            }
            #endregion
        }

        private void RetrieveWeldingPosition()
        {
            #region Positions
            if (CalloutPosition == CalloutPositions[0]) // La Mesa Railyard
            {
                int WeldingPositionNum = random.Next(RailyardWeldingPositions.Length);
                Suspect.Position = RailyardWeldingPositions[WeldingPositionNum];
                Suspect.Heading = RailyardWeldingHeadings[WeldingPositionNum];
            }
            else if (CalloutPosition == CalloutPositions[1]) // LSC Scrapyard
            {
                int WeldingPositionNum = random.Next(ScrapyardWeldingPositions.Length);
                Suspect.Position = ScrapyardWeldingPositions[WeldingPositionNum];
                Suspect.Heading = ScrapyardWeldingHeadings[WeldingPositionNum];
            }
            else if (CalloutPosition == CalloutPositions[2]) // McKenzie Airstrip
            {
                int WeldingPositionNum = random.Next(AirstripWeldingPositions.Length);
                Suspect.Position = AirstripWeldingPositions[WeldingPositionNum];
                Suspect.Heading = AirstripWeldingHeadings[WeldingPositionNum];
            }
            Suspect.Tasks.PlayAnimation(new AnimationDictionary("amb@world_human_welding@male@base"), "base", 5f, AnimationFlags.Loop);
            
            Rage.Object entity = new Rage.Object("prop_weld_torch", Suspect.Position);
            
            entity.AttachTo(Suspect, 0, Vector3.Zero, Rotator.Zero);

            #endregion
        }

        private void Scenario1() // Pursuit
        {
            #region Scenario 1
            try
            {
                // Retrieve Hiding Position
                RetrieveHidingPosition();

                GameFiber.StartNew(delegate
                {
                    while (CalloutActive)
                    {
                        GameFiber.Yield();
                        if (PedFound == true)
                        {
                            Entity.Delete(SuspectBlip);
                            Game.LogTrivial("[TRACE] Emergency Callouts: Deleted SuspectBlip");

                            LHandle pursuit = Functions.CreatePursuit();
                            Game.LogTrivial("[TRACE] Emergency Callouts: Created pursuit");

                            Functions.AddPedToPursuit(pursuit, Suspect);
                            Game.LogTrivial("[TRACE] Emergency Callouts: Added Suspect to pursuit");

                            Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                            Game.LogTrivial("[TRACE] Emergency Callouts: Set pursuit is active for player");

                            Functions.AddPedContraband(Suspect, LSPD_First_Response.Engine.Scripting.Entities.ContrabandType.Weapon, "Crowbar");
                            Game.LogTrivial("[TRACE] Emergency Callouts: Added \"WEAPON_CROWBAR\" to Suspect contraband");

                            Play.PursuitAudio();
                            Game.LogTrivial("[TRACE] Emergency Callouts: Played pursuit audio");

                            break;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "Scenario1", e);
            }
            #endregion
        }

        private void Scenario2() // Stop
        {
            #region Scenario 2
            try
            {
                // Retrieve Hiding Position
                RetrieveHidingPosition();

                GameFiber.StartNew(delegate
                {
                    while (CalloutActive)
                    {
                        GameFiber.Yield();

                        if (PedFound == true)
                        {
                            // Clear Suspect Tasks
                            Suspect.Tasks.Clear();
                            Game.LogTrivial("[TRACE] Emergency Callouts: Assigned Suspect tasks to null");

                            // Suspect Achieve Player Heading
                            Suspect.Tasks.AchieveHeading(MainPlayer.Heading - 180f);
                            Game.LogTrivial("[TRACE] Emergency Callouts: Assigned Suspect to face player");

                            break;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "Scenario2", e);
            }
            #endregion
        }
       
        private void Scenario3() // Manager
        {
            #region Scenario 3
            try
            {
                // Retrieve Manager Position
                RetrieveManagerPosition();

                string[] dialogue =
                {
                    "~y~Person~s~: Can I help you sir? I'm in charge of the place here.",
                    $"~b~{Settings.Callsign}~s~: Yes, we're looking for a person matching your description, do you have anything to prove that you work here?",
                    "~y~Person~s~: Yes ofcourse, here it is.",
                    $"~b~{Settings.Callsign}~s~: Okay, looks fine to me, when did you last come here?",
                    "~g~Person~s~: A few minutes ago, when my shift started.",
                    $"~b~{Settings.Callsign}~s~: Then the caller must've made a mistake.",
                    "~g~Person~s~: Well, I'm glad he called, we actually have alot of kids sneaking around here.",
                    $"~b~{Settings.Callsign}~s~: Okay, well, I'm going back out on patrol, see you later!",
                    "~g~Person~s~: Goodbye!",
                };

                int line = 0;
                int num = random.Next(RailyardManagerPositions.Length);

                // Random date generator (maxValue must be 1 higher than the number you'd want)
                int day = random.Next(1, 31);
                int month = random.Next(1, 13);
                int year = random.Next(DateTime.Now.Year, DateTime.Now.Year + 7);

                SuspectBlip = Suspect.AttachBlip();
                SuspectBlip.SetColor(Colors.Yellow);
                SuspectBlip.ScaleForPed();
                Entity.Disable(SuspectBlip);
                Game.LogTrivial("[TRACE] Emergency Callouts: Created SuspectBlip");

                // Inspect animation
                Suspect.Tasks.PlayAnimation(new AnimationDictionary("anim@amb@inspect@crouch@male_a@idles"), "idle_a", 5f, AnimationFlags.Loop);
                Game.LogTrivial("[TRACE] Emergency Callouts: Assigned Suspect to play animation");

                Functions.SetPedCantBeArrestedByPlayer(Suspect, true);
                Game.LogTrivial("[TRACE] Emergency Callouts: Set ped cant be arrested by player (Suspect)");

                GameFiber.StartNew(delegate
                {
                    while (CalloutActive)
                    {
                        GameFiber.Yield();

                        if (MainPlayer.Position.DistanceTo(Suspect.Position) < 3f)
                        {
                            if (Game.IsKeyDown(Settings.TalkKey))
                            {
                                DialogueStarted = true;
                                Game.LogTrivial("[TRACE] Emergency Callouts: Dialogue Started");

                                Suspect.Tasks.Clear();
                                Game.LogTrivial("[TRACE] Emergency Callouts: Cleared Suspect tasks");

                                Suspect.Tasks.AchieveHeading(MainPlayer.Heading - 180).WaitForCompletion();
                                Game.LogTrivial("[TRACE] Emergency Callouts: Suspect achieved player heading");

                                Game.DisplaySubtitle(dialogue[line], 99999);
                                line++;

                                if (line == 3)
                                {
                                    Suspect.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, AnimationFlags.None).WaitForCompletion();
                                    Game.LogTrivial("[TRACE] Emergency Callouts: Assigned Suspect to play animation");

                                    if (CalloutPosition == CalloutPositions[0]) // La Mesa Railyard
                                    {
                                        Game.DisplayNotification("heisthud", "hc_n_ric", "Go Loco Railroad", "~y~Richard Lukens", $"~b~Position~s~: Manager \n~g~Location~s~: La Mesa \n~c~Valid until {month}/{day}/{year}");
                                    }
                                    else if (CalloutPosition == CalloutPositions[1]) // LSC Scrapyard
                                    {
                                        Game.DisplayNotification("heisthud", "hc_n_che", "Los Santos Customs", "~y~Jimmy Macmillan", $"~b~Position~s~: Manager \n~g~Location~s~: Los Santos Int'l \n~c~Valid until {month}/{day}/{year}");
                                    }
                                    else if (CalloutPosition == CalloutPositions[2]) // McKenzie Airstrip
                                    {
                                        Game.DisplayNotification("heisthud", "hc_trevor", "Trevor Philips Industries", "~y~Trevor Philips", $"~b~Position~s~: CEO \n~g~Location~s~: Grapeseed \n~c~The best drugs you can buy!");
                                    }

                                    Game.LogTrivial("[TRACE] Emergency Callouts: Displayed Manager credentials");
                                }

                                if (line == 4)
                                {
                                    MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("mp_common"), "givetake1_b", 5f, AnimationFlags.None);
                                    Game.LogTrivial("[TRACE] Emergency Callouts: Assigned MainPlayer to play animation");

                                    SuspectBlip.SetColor(Colors.Green);
                                    Game.LogTrivial("[TRACE] Emergency Callouts: Changed SuspectBlip color to green");
                                }

                                if (line == dialogue.Length)
                                {
                                    GameFiber.Sleep(3000);
                                    Play.CodeFourAudio();
                                    Game.LogTrivial("[TRACE] Emergency Callouts: Played code four audio");

                                    End();
                                    break;
                                }
                                GameFiber.Sleep(500);
                            }
                            else
                            {
                                if (DialogueStarted == false)
                                {
                                    Game.DisplayHelp($"Press ~y~{Settings.TalkKey}~s~ to talk to the ~y~suspect~s~.");
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "Scenario3", e);
            }
            #endregion
        }

        private void Scenario4() // Attempted Arson
        {
            #region Scenario 4
            try
            {
                // Retrieve Fire Position
                RetrieveArsonPosition();

                // Give Suspect Weapon
                Suspect.Inventory.GiveNewWeapon("WEAPON_PETROLCAN", -1, true);
                Game.LogTrivial($"[TRACE] Emergency Callouts: Assigned ({Suspect.Inventory.EquippedWeapon}) to Suspect inventory");

                GameFiber.StartNew(delegate
                {
                    while (CalloutActive)
                    {
                        GameFiber.Yield();

                        if (PedFound == true)
                        {
                            // Clear Suspect Tasks
                            Suspect.Tasks.Clear();
                            Game.LogTrivial("[TRACE] Emergency Callouts: Cleared Suspect tasks");

                            // Put Suspect Hands Up
                            Suspect.Tasks.PutHandsUp(-1, MainPlayer);
                            Game.LogTrivial("[TRACE] Emergency Callouts: Assigned Suspect to put hands up");

                            break;
                        }
                        else
                        {
                            Suspect.Tasks.FireWeaponAt(Suspect.Position, -1, FiringPattern.FullAutomatic); // Fires weapon at Suspect Position, might not work.
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "Scenario4", e);
            }
            #endregion
        }

        private void Scenario5() // Knocked Out Guard
        {
            #region Scenario 5
            try
            {
                // Retrieve Welding Position
                RetrieveWeldingPosition();

                // Change SuspectBlip color
                SuspectBlip.SetColor(Colors.Red);
                Game.LogTrivial("[TRACE] Emergency Callouts: Changed SuspectBlip color to red");

                // Guard
                Guard = new Ped("csb_prolsec", CalloutPosition.Around2D(5f), 0f);
                Guard.SetDefaults();
                Game.LogTrivial("[TRACE] Emergency Callouts: Created Guard");
                Game.LogTrivial("[INFO] Emergency Callouts: Guard model: " + Guard.Model.Name);
                Game.LogTrivial("[INFO] Emergency Callouts: Guard position: " + Guard.Position);

                // Kill Guard
                Entity.Kill(Guard);
                Game.LogTrivial("[TRACE] Emergency Callouts: Killed Guard");

                // GuardBlip
                GuardBlip = Guard.AttachBlip();
                GuardBlip.SetColor(Colors.Blue);
                GuardBlip.ScaleForPed();
                Entity.Disable(GuardBlip);
                Game.LogTrivial("[TRACE] Emergency Callouts: Created GuardBlip");


                GameFiber.StartNew(delegate
                {
                    while (CalloutActive)
                    {
                        GameFiber.Yield();

                        if (MainPlayer.Position.DistanceTo(Guard.Position) < 5f && Guard.Exists())
                        {
                            // Enable SuspectBlip
                            Entity.Enable(GuardBlip);
                            Game.LogTrivial("[TRACE] Emergency Callouts: Enabled GuardBlip");

                            // Delete SearchArea
                            Entity.Delete(SearchArea);
                            Game.LogTrivial("[TRACE] Emergency Callouts: Deleted SearchArea");
                            
                            Game.DisplayHelp("The ~b~guard~s~ appears to be ~r~unconscious~s~.\nrequest an ~g~ambulance~s~.");
                            break;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "Scenario5", e);
            }
            #endregion
        }

        public override void Process()
        {
            base.Process();
            try
            {
                Check.EndKeyDown();
                Check.NaturalEnding(Suspect);
                Check.PreventDistanceCrash(CalloutPosition, OnScene, PedFound);
                Check.PreventResponderCrash(Suspect);
                Check.PreventResponderCrash(Guard);

                #region OnPlayerArrival
                if (MainPlayer.Position.DistanceTo(Entrance) < 15f && OnScene == false)
                {
                    // Set OnScene
                    OnScene = true;
                    Game.LogTrivial("[TRACE] Emergency Callouts: Entered scene");

                    // Display Arriving Subtitle
                    Display.ArriveSubtitle("Find", "trespasser", 'r');

                    // Disable route
                    Entity.DisableRoute(EntranceBlip);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Disabled route");

                    // Delete EntranceBlip
                    Entity.Delete(EntranceBlip);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Deleted EntranceBlip");

                    // Create SearchArea
                    SearchArea = new Blip(Center, 85f);
                    SearchArea.SetColor(Colors.Yellow);
                    SearchArea.Alpha = 0.5f;
                    Game.LogTrivial("[TRACE] Emergency Callouts: Created SearchArea");
                }
                #endregion

                #region OnPedFound
                if (MainPlayer.Position.DistanceTo(Suspect.Position) < 5f && PedFound == false && OnScene == true && Suspect.Exists())
                {
                    // Set PedFound
                    PedFound = true;
                    Game.LogTrivial("[TRACE] Emergency Callouts: Found Suspect");

                    // Hide Subtitle
                    Display.HideSubtitle();
                    Game.LogTrivial("[TRACE] Emergency Callouts: Hid subtitle");

                    // Enable SuspectBlip
                    Entity.Enable(SuspectBlip);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Enabled SuspectBlip");

                    // Delete SearchArea
                    Entity.Delete(SearchArea);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Deleted SearchArea");
                }
                #endregion

                #region OnPedDetained
                if (Suspect.IsDetained() == true && PedDetained == false && Suspect.Exists())
                {
                    // Set PedDetained
                    PedDetained = true;
                    Game.LogTrivial("[TRACE] Emergency Callouts: Suspect detained");

                    // Delete SuspectBlip
                    Entity.Delete(SuspectBlip);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Deleted SuspectBlip");
                }
                #endregion

                #region OnPedArrested
                if (PedArrested == false && Suspect.IsCuffed && Suspect.Exists())
                {
                    // Set PedArrested
                    PedArrested = true;
                    Game.LogTrivial("[TRACE] Emergency Callouts: Suspect arrested");

                    // Display ArrestLine
                    Display.ArrestLine();
                }
                #endregion

                #region OnPlayerLeave
                if (MainPlayer.Position.DistanceTo(CalloutPosition) > Settings.SearchAreaSize * 3f && OnScene == true)
                {
                    // Set OnScene
                    OnScene = false;
                    Game.LogTrivial("[TRACE] Emergency Callouts: Left scene");

                    // Disable SuspectBlip
                    Entity.Disable(SuspectBlip);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Disabled SuspectBlip");

                    // Delete SearchArea
                    Entity.Delete(SearchArea);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Deleted SearchArea");

                    // Create EntranceBlip
                    EntranceBlip = new Blip(Entrance);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Created EntranceBlip");

                    // Enable Route
                    Entity.EnableRoute(EntranceBlip);
                    Game.LogTrivial("[TRACE] Emergency Callouts: Enabled route to EntranceBlip");
                }
                #endregion
            }
            catch (Exception e)
            {
                Log.CalloutException(this, "Process", e);
                End();
            }
        }

        public override void End()
        {
            base.End();
            CalloutActive = false;

            Entity.Dismiss(Suspect);
            Entity.Dismiss(Guard);
            Entity.Delete(SuspectBlip);
            Entity.Delete(GuardBlip);
            Entity.Delete(SearchArea);
            Entity.Delete(EntranceBlip);

            Display.HideSubtitle();
            Display.DetachMessage();
            Log.CalloutEnded(CalloutMessage, ScenarioNumber);
        }
    }
}