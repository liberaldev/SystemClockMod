using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI.Widgets;

namespace SystemClockMod
{
    [FileLocation(nameof(SystemClockMod))]
    public class Setting(IMod mod) : ModSetting(mod)
    {
        private const string Section = "Main";
        
        public override void SetDefaults()
        {
            ClockSizeSetting = ClockSize.Medium;
            DisplaySeconds = false;
        }

        public enum ClockSize
        {
            Small,
            Medium,
            Large
        }
        
        [SettingsUISection(Section)]
        public ClockSize ClockSizeSetting { get; set; }

        [SettingsUISection(Section)]
        public bool DisplaySeconds { get; set; }
    }
}
