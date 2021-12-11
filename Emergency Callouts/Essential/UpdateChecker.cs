﻿using EmergencyCallouts.Essential;
using Rage;
using System;
using System.Net;

namespace EmergencyCallouts
{
    internal class UpdateChecker
    {
        internal static bool UpdateAvailable()
        {
            WebClient webClient = new WebClient();
            Uri OnlineVersionURI = new Uri("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=20730&textOnly=1");
            string OnlineVersion = null;

            try
            {
                Game.LogTrivial("[INFO] Emergency Callouts: Checking for updates");

                OnlineVersion = webClient.DownloadString(OnlineVersionURI).Trim();
                OnlineVersion = "0.1.0"; // ! Temp
            }
            catch (WebException)
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~y~Error", "Failed to check for updates; Possible network error.");
                Game.LogTrivial("[WARN] Emergency Callouts: Checked for updates; Failed to check");
            }

            if (OnlineVersion != Project.LocalVersion)
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", $"~r~{Project.LocalVersion} ~b~by Faya", $"Found update ~g~{OnlineVersion} ~s~available for you!");
                Game.LogTrivial("[WARN] Emergency Callouts: Checked for updates; Found an update");
                return true;
            }
            else if (OnlineVersion.ToLower() == "file hidden")
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", $"~r~{Project.LocalVersion} ~b~by Faya", "New update is being reviewed by LSPDFR!");
                Game.LogTrivial("[WARN] Emergency Callouts: Checked for updates; File is hidden");
                return true;
            }
            else
            {
                Game.DisplayNotification("commonmenu", "shop_tick_icon", "Emergency Callouts", $"~g~{Project.LocalVersion} ~b~by Faya", "~y~Reporting for duty!");
                Game.LogTrivial("[INFO] Emergency Callouts: Checked for updates; None available");
                return false;
            }
        }
    }
}