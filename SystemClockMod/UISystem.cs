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
        private static string ClockSizeSetting => Mod.Setting.ClockSize;

        private ValueBinding<string> _clockSize;
        
        
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

                switch (hour)
                {
                    case 0:
                        hour = 12;
                        break;
                    case >= 13:
                        hour -= 12;
                        break;
                }

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
            
            /* Thanks for zakuro9715
             * source: https://github.com/zakuro9715/CS2-AdvancedSimulationSpeed/blob/main/AdvancedSimulationSpeed/Systems/UISystem.cs
             */
            Mod.Setting.onSettingsApplied += (Game.Settings.Setting settings) =>
            {
                _clockSize.Update(ClockSizeSetting);
            };
            
            AddBinding(_clockSize = new ValueBinding<string>(Mod.ID, "ClockSize", Mod.Setting.ClockSize));
            _timer = new System.Timers.Timer(1000); // 1초마다
            _timer.Elapsed += (s, e) => CurrentTimeString = GetFormattedSystemTime();
            _timer.AutoReset = true;
            _timer.Start();
        }
    }
}