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
        private string SystemTime { get; set; } = GetSystemTime();
        private static readonly string[] AmpmPrefixLocales = ["ko-KR"];
        
        private static string GetSystemTime()
        {
            var interfaceSettings = GameManager.instance.settings.userInterface;
            var nowDateTime = DateTime.Now;
            
            var hour = nowDateTime.Hour;
            var result = $"{nowDateTime.Minute:00}{(Mod.Setting.DisplaySeconds ? ":" + nowDateTime.Second.ToString("00") : "")}";

            if (interfaceSettings.timeFormat == InterfaceSettings.TimeFormat.TwelveHours)
            {
                var ampm = nowDateTime.ToString("tt", System.Globalization.CultureInfo.InvariantCulture) == "AM"
                    ? "SystemClock.AnteMeridiem".Translate()
                    : "SystemClock.PostMeridiem".Translate();

                if (hour >= 13) hour -= 12;

                if (!AmpmPrefixLocales.Contains(GameManager.instance.localizationManager.activeLocaleId))
                {
                    result = $"{hour:00}:{result} {ampm}";
                }
                else
                {
                    result = $"{ampm} {hour:00}:{result}";
                }
            }
            else
            {
                result = $"{hour:00}:{result}";
            }

            return result;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            AddUpdateBinding(new GetterValueBinding<string>(Mod.ID, "GetSystemTime", (() => SystemTime)));
            var timer = new System.Timers.Timer(1000); // 1초마다
            timer.Elapsed += (s, e) => SystemTime = GetSystemTime();
            timer.AutoReset = true;
            timer.Start();
        }
    }
}