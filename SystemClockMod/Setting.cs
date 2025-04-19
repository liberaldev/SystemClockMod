using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

namespace SystemClockMod
{
    [FileLocation(nameof(Setting))]
    public class Setting : ModSetting
    {
        public Setting(IMod mod) : base(mod)
        {
        }

        public override void SetDefaults()
        {
        }

        public bool DisplaySecond { get; set; }
    }
}
