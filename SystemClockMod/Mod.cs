using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using LibShared.Localization;

namespace SystemClockMod
{
    public class Mod : IMod
    {
        private static readonly InterfaceSettings InterfaceSettings = new InterfaceSettings();
        internal const string ID = "SystemClockMod";
        private static readonly ILog LOG = LogManager.GetLogger($"{nameof(SystemClockMod)}.{nameof(Mod)}")
            .SetShowsErrorsInUI(false);
        internal static Setting Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            LOG.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                LOG.Info($"Current mod asset at {asset.path}"); 
            Setting = new Setting(this);
            Setting.RegisterInOptionsUI();
            
            /* Thanks for zakuro9715
             * source: https://github.com/zakuro9715/CS2-AdvancedSimulationSpeed/blob/main/AdvancedSimulationSpeed/Mod.cs
             */
            var localizationManager = GameManager.instance.localizationManager;
            LocaleLoader.Load(LOG, localizationManager);
            
            AssetDatabase.global.LoadSettings(nameof(SystemClockMod), Setting, new Setting(this));
            
            updateSystem.UpdateAt<UISystem>(SystemUpdatePhase.UIUpdate);
        }

        public void OnDispose()
        {
            LOG.Info(nameof(OnDispose));
            if (Setting == null) return;
            Setting.UnregisterInOptionsUI();
            Setting = null;
        }
    }
}