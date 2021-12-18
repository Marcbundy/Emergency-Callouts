﻿using EmergencyCalloutsLE.Essential;
using Rage;
using System;
using System.Net;

namespace EmergencyCalloutsLE
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
                Game.LogTrivial("[Emergency Callouts - Law Enforcement]: Checking for updates");

                OnlineVersion = webClient.DownloadString(OnlineVersionURI).Trim();
                OnlineVersion = "0.1.0"; // ! Temp
            }
            catch (WebException)
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts - Law Enforcement", "~y~Error", "Failed to check for updates; Possible network error.");
                Game.LogTrivial("[Emergency Callouts - Law Enforcement]: Checked for updates; Failed to check");
            }

            if (OnlineVersion != Project.LocalVersion)
            {
                GameFiber.Wait(5000);
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts - Law Enforcement", $"~r~{Project.LocalVersion} ~b~by Faya", $"Found update ~g~{OnlineVersion} ~s~available for you!");
                Game.LogTrivial("[Emergency Callouts - Law Enforcement]: Checked for updates; Found an update");
                return true;
            }
            else if (OnlineVersion.ToLower() == "file hidden")
            {
                GameFiber.Wait(5000);
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts - Law Enforcement", $"~r~{Project.LocalVersion} ~b~by Faya", "New update is being reviewed by LSPDFR!");
                Game.LogTrivial("[Emergency Callouts - Law Enforcement]: Checked for updates; File is hidden");
                return true;
            }
            else
            {
                GameFiber.Wait(5000);
                Game.DisplayNotification("commonmenu", "shop_tick_icon", "Emergency Callouts - Law Enforcement", $"~g~{Project.LocalVersion} ~b~by Faya", "~y~Reporting for duty!");
                Game.LogTrivial("[Emergency Callouts - Law Enforcement]: Checked for updates; None available");
                return false;
            }
        }
    }
}