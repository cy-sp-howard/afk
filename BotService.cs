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
            var s = module.Settings;
            KeepAliveTimer = new Timer(s.KeepAliveInterval.Value * 1000);
            KeepAliveTimer.Elapsed += async delegate {
                bool isInCombat = GameService.Gw2Mumble.PlayerCharacter.IsInCombat;
                if (isInCombat)
                {
                    Keyboard.Stroke((VirtualKeyShort)s.HealButton.Value.PrimaryKey);
                    if (s.HealButton.Value.PrimaryKey != 0) await Task.Delay((int)(s.HealButtonWaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton2.Value.PrimaryKey);
                    if (s.KeepAliveButton2.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton2WaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton3.Value.PrimaryKey);
                    if (s.KeepAliveButton3.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton3WaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton4.Value.PrimaryKey);
                    if (s.KeepAliveButton4.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton4WaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton5.Value.PrimaryKey);
                    if (s.KeepAliveButton5.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton5WaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton6.Value.PrimaryKey);
                    if (s.KeepAliveButton6.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton6WaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton7.Value.PrimaryKey);
                    if (s.KeepAliveButton7.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton7WaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton8.Value.PrimaryKey);
                    if (s.KeepAliveButton8.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton8WaitTime.Value * 1000));
                    Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton9.Value.PrimaryKey);
                    if (s.KeepAliveButton9.Value.PrimaryKey != 0) await Task.Delay((int)(s.KeepAliveButton9WaitTime.Value * 1000));
                }
                Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton1.Value.PrimaryKey);
                Task.Delay((int)(s.KeepAliveButton1WaitTime.Value * 1000)).GetAwaiter().GetResult();


            };
            if (s.KeepAlive.Value)
            {
                KeepAliveTimer.Start();
                Keyboard.Stroke((VirtualKeyShort)s.KeepAliveButton1.Value.PrimaryKey);
            }
        }
    }
}
