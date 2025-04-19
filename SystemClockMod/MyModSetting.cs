using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

namespace SystemClockMod
{
    [FileLocation(nameof(MyModSetting))]
    public class MyModSetting : ModSetting
    {
        public MyModSetting(IMod mod) : base(mod)
        {
        }

        public override void SetDefaults()
        {
        }
        [SettingsUIDisplayName(overrideId: "DisplaySecond", overrideValue:"Display Second")]
        public bool Toggle { get; set; }
    }
}
