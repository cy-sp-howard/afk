using Blish_HUD;
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
            Init();
        }
        public void Upadate()
        {
        }
        public void Unload()
        {
            KeepAliveTimer.Stop();
            KeepAliveTimer.Dispose();
        }
        public void Init()
        {
            KeepAliveTimer = new Timer(module.Settings.KeepAliveInterval.Value * 1000);
            KeepAliveTimer.Elapsed += async delegate {
                bool isInCombat = GameService.Gw2Mumble.PlayerCharacter.IsInCombat;
                if (isInCombat)
                {
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.HealButton.Value.PrimaryKey);
                    if (module.Settings.HealButton.Value.PrimaryKey != 0) await Task.Delay(1000);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton2.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton2.Value.PrimaryKey != 0) await Task.Delay(200);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton3.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton3.Value.PrimaryKey != 0) await Task.Delay(200);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton4.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton4.Value.PrimaryKey != 0) await Task.Delay(200);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton5.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton5.Value.PrimaryKey != 0) await Task.Delay(200);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton6.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton6.Value.PrimaryKey != 0) await Task.Delay(200);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton7.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton7.Value.PrimaryKey != 0) await Task.Delay(200);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton8.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton8.Value.PrimaryKey != 0) await Task.Delay(200);
                    Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton9.Value.PrimaryKey);
                    if (module.Settings.KeepAliveButton9.Value.PrimaryKey != 0) await Task.Delay(200);
                }
                Keyboard.Stroke((VirtualKeyShort)module.Settings.KeepAliveButton1.Value.PrimaryKey);


            };
            if (module.Settings.KeepAlive.Value)
            {
                KeepAliveTimer.Start();
            }
        }
    }
}
