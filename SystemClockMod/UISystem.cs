using System;
using System.Linq;
using System.Security.AccessControl;
using Colossal.UI.Binding;
using Game.Modding;
using Game.Settings;
using Game.UI;
using SystemClockMod.Utils;

namespace SystemClockMod
{
    public partial class UISystem : UISystemBase
    {
        private readonly InterfaceSettings _interfaceSettings = new InterfaceSettings();

        private string GetTime()
        {
            var now = DateTime.Now;
            string[] beginAmpmLocales = ["ko-KR"];
            var hour = now.Hour;

            var result = $"{now.Minute:00}{(Mod.Setting.DisplaySeconds ? ":" + now.Second.ToString("00") : "")}";

            if (_interfaceSettings.timeFormat == InterfaceSettings.TimeFormat.TwelveHours)
            {
                var ampm = now.ToString("tt", System.Globalization.CultureInfo.InvariantCulture) == "AM"
                    ? "SystemClock.AnteMeridiem".Translate()
                    : "SystemClock.PostMeridiem".Translate();

                if (hour >= 13) hour -= 12;

                if (!beginAmpmLocales.Contains(_interfaceSettings.currentLocale))
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
            AddUpdateBinding(new GetterValueBinding<string>(Mod.ID, "displayActualSeconds", GetTime));
        }
    }
}