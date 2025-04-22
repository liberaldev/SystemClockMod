using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using LibShared.Localization;

namespace SystemClockMod
{
    
    public class Mod : IMod
    {
        
        public static ILog log = LogManager.GetLogger($"{nameof(SystemClockMod)}.{nameof(Mod)}")
            .SetShowsErrorsInUI(false);
        internal static Setting Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}"); 
            Setting = new Setting(this);
            Setting.RegisterInOptionsUI();
            
            /* Thanks for zakuro9715
             * source: https://github.com/zakuro9715/CS2-AdvancedSimulationSpeed/blob/main/AdvancedSimulationSpeed/Mod.cs
             */
            var localizationManager = GameManager.instance.localizationManager;
            LocaleLoader.Load(log, localizationManager);
            
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }
}