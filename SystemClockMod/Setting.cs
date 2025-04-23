using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI.Widgets;

namespace SystemClockMod
{
    [FileLocation(nameof(Setting))]
    public class Setting(IMod mod) : ModSetting(mod)
    {
        public override void SetDefaults()
        {
            ClockSize = "Medium";
            DisplaySeconds = false;
        }
        
        [SettingsUIDropdown(typeof(Setting), nameof(GetSizeDropdownItems))]
        public string ClockSize { get; set; } = "Medium";

        public DropdownItem<string>[] GetSizeDropdownItems()
        {
            var items = new DropdownItem<string>[]
            {
                new DropdownItem<string>
                {
                    value = "Small",
                    displayName = GetOptionLabelLocaleID("Small"),
                },
                new DropdownItem<string>
                {
                    value = "Medium",
                    displayName = GetOptionLabelLocaleID("Medium"),
                },
                new DropdownItem<string>
                {
                    value = "Large",
                    displayName = GetOptionLabelLocaleID("Large"),
                },
            };

            return items;
        }

        public bool DisplaySeconds { get; set; }
        
        
    }
}
