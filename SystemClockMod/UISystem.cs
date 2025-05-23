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
        private static string ClockSizeSetting => Mod.Setting.ClockSizeSetting.ToString();
        
        private System.Timers.Timer _timer;
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
                    ? "SystemClockMod.AnteMeridiem".Translate()
                    : "SystemClockMod.PostMeridiem".Translate();

                switch (hour)
                {
                    case 0:
                        hour = 12;
                        break;
                    case >= 13:
                        hour -= 12;
                        break;
                }

                formattedTime = !LocalesWithAmpmPrefix.Contains(GameManager.instance.localizationManager.activeLocaleId) ? $"{hour:00}:{formattedTime} {ampm}" : $"{ampm} {hour:00}:{formattedTime}";
            }
            else
            {
                formattedTime = $"{hour:00}:{formattedTime}";
            }

            return formattedTime;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            
            AddUpdateBinding(new GetterValueBinding<string>(Mod.ID, "FormattedSystemTime", () => CurrentTimeString));
            
            _timer = new System.Timers.Timer(1000); // 1초마다
            _timer.Elapsed += (_, _) => CurrentTimeString = GetFormattedSystemTime();
            _timer.AutoReset = true;
            _timer.Start();
            
            AddBinding(_clockSize = new ValueBinding<string>(Mod.ID, "ClockSize", ClockSizeSetting));
            
            /* Thanks for zakuro9715
             * source: https://github.com/zakuro9715/CS2-AdvancedSimulationSpeed/blob/main/AdvancedSimulationSpeed/Systems/UISystem.cs
             */
            Mod.Setting.onSettingsApplied += (_) =>
            {
                _clockSize.Update(ClockSizeSetting);
            };
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (_timer == null) return;
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }
    }
}