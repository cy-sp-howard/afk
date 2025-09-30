using Blish_HUD.Controls.Extern;
using Blish_HUD.Controls.Intern;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace BhModule.Afk
{
    public class BotService
    {
        private readonly AfkModule module;
        public static event EventHandler OnLoaded;
        public Timer KeepAliveTimer;

        public BotService(AfkModule module)
        {
            this.module = module;
        }
        public void Load()
        {
            InitKeepAlive();
        }
        public void Upadate()
        {
        }
        public void Unload()
        {
            KeepAliveTimer.Stop();
            KeepAliveTimer.Dispose();
        }
        public void InitKeepAlive()
        {
            KeepAliveTimer = new Timer(10000);
            KeepAliveTimer.Elapsed += async delegate {
                Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton1.Value.PrimaryKey);
                await Task.Delay(50);
                Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton2.Value.PrimaryKey);
            };
            if (module.Settings.KeepAlive.Value)
            {
                KeepAliveTimer.Start();
            }
        }
    }
}
