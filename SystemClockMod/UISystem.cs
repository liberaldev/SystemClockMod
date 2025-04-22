using System;
using System.Linq;
using Colossal.UI.Binding;
using Game.SceneFlow;
using Game.Settings;
using Game.UI;
using SystemClockMod.Utils;

namespace SystemClockMod
{
    public partial class UISystem : UISystemBase
    {
        private string CurrentTimeString { get; set; } = GetFormattedSystemTime();
        private static readonly string[] LocalesWithAmpmPrefix = ["ko-KR"];
        
        private static string GetFormattedSystemTime()
        {
            var interfaceSettings = GameManager.instance.settings.userInterface;
            var nowDateTime = DateTime.Now;
            
            var hour = nowDateTime.Hour;
            var formattedTime = $"{nowDateTime.Minute:00}{(Mod.Setting.DisplaySeconds ? ":" + nowDateTime.Second.ToString("00") : "")}";

            if (interfaceSettings.timeFormat == InterfaceSettings.TimeFormat.TwelveHours)
            {
                var ampm = nowDateTime.ToString("tt", System.Globalization.CultureInfo.InvariantCulture) == "AM"
                    ? "SystemClock.AnteMeridiem".Translate()
                    : "SystemClock.PostMeridiem".Translate();

                if (hour >= 13) hour -= 12;

                if (!LocalesWithAmpmPrefix.Contains(GameManager.instance.localizationManager.activeLocaleId))
                {
                    formattedTime = $"{hour:00}:{formattedTime} {ampm}";
                }
                else
                {
                    formattedTime = $"{ampm} {hour:00}:{formattedTime}";
                }
            }
            else
            {
                formattedTime = $"{hour:00}:{formattedTime}";
            }

            return formattedTime;
        }

        private System.Timers.Timer _timer;

        protected override void OnCreate()
        {
            base.OnCreate();
            AddUpdateBinding(new GetterValueBinding<string>(Mod.ID, "FormattedSystemTime", (() => CurrentTimeString)));
            _timer = new System.Timers.Timer(1000); // 1초마다
            _timer.Elapsed += (s, e) => CurrentTimeString = GetFormattedSystemTime();
            _timer.AutoReset = true;
            _timer.Start();
        }
    }
}