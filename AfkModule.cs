using Blish_HUD;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Modules;
using Blish_HUD.Modules.Managers;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Linq;

namespace BhModule.Afk
{
    [Export(typeof(Blish_HUD.Modules.Module))]
    public class AfkModule : Blish_HUD.Modules.Module
    {
        private static readonly Logger Logger = Logger.GetLogger<AfkModule>();
        #region Service Managers
        internal SettingsManager SettingsManager => this.ModuleParameters.SettingsManager;
        internal ContentsManager ContentsManager => this.ModuleParameters.ContentsManager;
        internal DirectoriesManager DirectoriesManager => this.ModuleParameters.DirectoriesManager;
        internal Gw2ApiManager Gw2ApiManager => this.ModuleParameters.Gw2ApiManager;
        #endregion
        public BotService BotService { get; private set; }
        public ModuleSettings Settings { get; private set; }
        public static AfkModule Instance;
        public static ModuleManager InstanceManager;
        [ImportingConstructor]
        public AfkModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters)
        {
            Instance = this;
        }

        protected override void DefineSettings(SettingCollection settings)
        {
            this.Settings = new ModuleSettings(this, settings);
        }
        public override IView GetSettingsView()
        {
            return new AfkSettingsView(SettingsManager.ModuleSettings);
        }

        protected override void Initialize()
        {
            this.BotService = new BotService(this);
            InstanceManager = GameService.Module.Modules.FirstOrDefault(m => m.ModuleInstance == this);
        }
        protected override async Task LoadAsync()
        {
            this.BotService.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            this.BotService.Upadate();
        }

        protected override void Unload()
        {
            this.BotService.Unload();
        }
    }

}
