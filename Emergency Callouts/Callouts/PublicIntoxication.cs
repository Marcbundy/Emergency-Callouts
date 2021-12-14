﻿using EmergencyCallouts.Essential;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using static EmergencyCallouts.Essential.Color;
using static EmergencyCallouts.Essential.Helper;
using Entity = EmergencyCallouts.Essential.Helper.Entity;

namespace EmergencyCallouts.Callouts
{
    [CalloutInfo("Public Intoxication", CalloutProbability.Medium)]
    public class PublicIntoxication : Callout
    {
        bool OnScene;
        bool NearPed;
        bool DetainedPed;
        bool NeedsRefreshing;

        Ped Suspect;
        Blip EntranceBlip;
        Blip SearchArea;
        Blip SuspectBlip;

        public override bool OnBeforeCalloutDisplayed()
        {
            CalloutPosition = World.GetNextPositionOnStreet(MainPlayer.Position.Around(100f, Settings.CalloutDistance));

            CalloutMessage = "Public Intoxication";
            CalloutDetails = "There are multiple reports of a person under the influence of alcohol.";

            ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 60f);
            AddMinimumDistanceCheck(30f, CalloutPosition);

            Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT CRIME_PUBLIC_INTOXICATION IN_OR_ON_POSITION UNITS_RESPOND_CODE_02", CalloutPosition);

            return base.OnBeforeCalloutDisplayed();
        }

        public override void OnCalloutNotAccepted()
        {
            Functions.PlayScannerAudio("PED_RESPONDING");
            base.OnCalloutNotAccepted();
        }

        public override bool OnCalloutAccepted()
        {
            EntranceBlip = new Blip(CalloutPosition);
            EntranceBlip.EnableRoute();

            Suspect = new Ped(Entity.GetRandomMaleModel(), CalloutPosition, 0f);
            Suspect.SetDefaults();
            Suspect.SetIntoxicated();
            Game.LogTrivial($"[Emergency Callouts]: Created Suspect ({Suspect.Model.Name}) at " + Suspect.Position);

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.SetColor(Colors.Yellow);
            SuspectBlip.ScaleForPed();
            SuspectBlip.Disable();

            Suspect.Tasks.Wander();
            
            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();

            #region OnPlayerArrival
            if (MainPlayer.Position.DistanceTo(CalloutPosition) < Settings.SearchAreaSize && !OnScene)
            {
                // Remove EntranceBlip
                EntranceBlip.Remove();

                // Create SearchArea
                SearchArea = new Blip(Suspect.Position.Around(5f, 30f), Settings.SearchAreaSize);
                SearchArea.SetColor(Colors.Yellow);
                SearchArea.Alpha = 0.5f;

                // Display Subtitle
                Game.DisplaySubtitle("Find the ~y~drunk person~s~ in the ~y~area~s~.");

                Game.LogTrivial("[Emergency Callouts]: Arrived on scene");

                OnScene = true;
            }
            #endregion

            #region NearPed
            if (MainPlayer.Position.DistanceTo(Suspect.Position) < 5f && !NearPed && OnScene && Suspect)
            {
                // Hide Subtitle
                Display.HideSubtitle();

                // Enable SuspectBlip
                SuspectBlip.Enable();

                // Remove SearchArea
                SearchArea.Remove();

                Game.LogTrivial("[Emergency Callouts]: Near Suspect");

                NearPed = true;
            }
            #endregion

            #region DetainedPed
            if (Suspect.IsDetained() && !DetainedPed)
            {
                // Remove SuspectBlip
                SuspectBlip.Remove();

                DetainedPed = true;
            }
            #endregion

            #region OnPlayerLeave
            if (MainPlayer.Position.DistanceTo(CalloutPosition) > Settings.SearchAreaSize * 3f && OnScene)
            {
                // Set OnScene
                OnScene = false;

                // Disable SuspectBlip
                SuspectBlip.Disable();

                // Delete SearchArea
                SearchArea.Remove();

                // Create EntranceBlip
                EntranceBlip = new Blip(CalloutPosition);

                // Enable Route
                EntranceBlip.EnableRoute();

                Game.LogTrivial("[Emergency Callouts]: Player left scene");
            }
            #endregion

            #region RefreshSearchArea
            if (!NearPed)
            {
                if (Suspect.Position.DistanceTo(CalloutPosition) < Settings.SearchAreaSize)
                {
                    NeedsRefreshing = false;
                }
                else
                {
                    NeedsRefreshing = true;
                }
            }

            if (Suspect.Position.DistanceTo(CalloutPosition) > Settings.SearchAreaSize && NeedsRefreshing == true)
            {
                CalloutPosition = Suspect.Position;
                SearchArea.Remove();

                SearchArea = new Blip(Suspect.Position.Around(10f, 30f), Settings.SearchAreaSize);
                SearchArea.SetColor(Colors.Yellow);
                SearchArea.Alpha = 0.5f;
                Game.LogTrivial("[Emergency Callouts]: Refreshed SearchArea");

                Functions.PlayScannerAudioUsingPosition("SUSPECT IN_OR_ON_POSITION", Suspect.Position);
            }
            #endregion
        }

        public override void End()
        {
            base.End();
        }
    }
}