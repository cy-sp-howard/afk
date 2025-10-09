using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Input;
using Blish_HUD.Settings;
using Blish_HUD.Settings.UI.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace BhModule.Afk
{
    public class ModuleSettings
    {
        private readonly AfkModule module;
        public SettingEntry<bool> KeepAlive { get; private set; }
        public SettingEntry<int> KeepAliveInterval { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveToggleKey { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton1 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton2 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton3 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton4 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton5 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton6 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton7 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton8 { get; private set; }
        public SettingEntry<KeyBinding> KeepAliveButton9 { get; private set; }
        public SettingEntry<KeyBinding> HealButton { get; private set; }
        public ModuleSettings(AfkModule module, SettingCollection settings)
        {
            this.module = module;
            InitUISetting(settings);
        }
        private void InitUISetting(SettingCollection settings)
        {
            this.KeepAlive = settings.DefineSetting(nameof(this.KeepAlive), true, () => "Keep Character Alive.", () => "");
            this.KeepAliveInterval = settings.DefineSetting(nameof(this.KeepAliveInterval), 5, () => $"Use Skill Interval - {this.KeepAliveInterval.Value}s", () => "");
            this.KeepAliveInterval.SetRange(3, 60);
            this.KeepAliveInterval.SettingChanged += delegate
            {
                module.BotService.KeepAliveTimer.Interval = this.KeepAliveInterval.Value * 1000;
            };
            this.KeepAliveToggleKey = settings.DefineSetting(nameof(this.KeepAliveToggleKey), new KeyBinding(Keys.OemMinus), () => "Toggle Keep Alive", () => "");
            this.KeepAliveButton1 = settings.DefineSetting(nameof(this.KeepAliveButton1), new KeyBinding(Keys.D1), () => "Trigger Button 1", () => "");
            this.KeepAliveButton2 = settings.DefineSetting(nameof(this.KeepAliveButton2), new KeyBinding(Keys.None), () => "Trigger Button 2", () => "");
            this.KeepAliveButton3 = settings.DefineSetting(nameof(this.KeepAliveButton3), new KeyBinding(Keys.None), () => "Trigger Button 3", () => "");
            this.KeepAliveButton4 = settings.DefineSetting(nameof(this.KeepAliveButton4), new KeyBinding(Keys.None), () => "Trigger Button 4", () => "");
            this.KeepAliveButton5 = settings.DefineSetting(nameof(this.KeepAliveButton5), new KeyBinding(Keys.None), () => "Trigger Button 5", () => "");
            this.KeepAliveButton6 = settings.DefineSetting(nameof(this.KeepAliveButton6), new KeyBinding(Keys.None), () => "Trigger Button 6", () => "");
            this.KeepAliveButton7 = settings.DefineSetting(nameof(this.KeepAliveButton7), new KeyBinding(Keys.None), () => "Trigger Button 7", () => "");
            this.KeepAliveButton8 = settings.DefineSetting(nameof(this.KeepAliveButton8), new KeyBinding(Keys.None), () => "Trigger Button 8", () => "");
            this.KeepAliveButton9 = settings.DefineSetting(nameof(this.KeepAliveButton9), new KeyBinding(Keys.None), () => "Trigger Button 9", () => "");
            this.KeepAliveToggleKey.Value.Enabled = true;
            this.KeepAliveToggleKey.Value.Activated += ToggleKeepAlive;

            this.HealButton = settings.DefineSetting(nameof(this.HealButton), new KeyBinding(Keys.F), () => "Heal Button", () => "");
        }
        private void ToggleKeepAlive(object sender, System.EventArgs args)
        {
            KeepAlive.Value = !KeepAlive.Value;
            if (KeepAlive.Value) module.BotService.KeepAliveTimer.Start();
            else module.BotService.KeepAliveTimer.Stop();
            Utils.Notify.Show(KeepAlive.Value ? "Enable Keep Alive." : "Disable Keep Alive.");
        }
        public void Unload()
        {
            this.KeepAliveToggleKey.Value.Activated -= ToggleKeepAlive;
        }
    }
    public class AfkSettingsView(SettingCollection settings) : View
    {
        static Padding messagePadding;
        FlowPanel rootflowPanel;
        readonly SettingCollection settings = settings;
        protected override void Build(Container buildPanel)
        {
            rootflowPanel = new FlowPanel()
            {
                Size = buildPanel.Size,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
                ControlPadding = new Vector2(5, 2),
                OuterControlPadding = new Vector2(10, 15),
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.AutoSize,
                AutoSizePadding = new Point(0, 15),
                Parent = buildPanel
            };
            messagePadding = new Padding() { Parent = rootflowPanel };

            foreach (var setting in settings.Where(s => s.SessionDefined))
            {
                IView settingView;

                if ((settingView = SettingView.FromType(setting, rootflowPanel.Width)) != null)
                {
                    ViewContainer container = new()
                    {
                        WidthSizingMode = SizingMode.Fill,
                        HeightSizingMode = SizingMode.AutoSize,
                        Parent = rootflowPanel
                    };
                    if (setting.EntryKey == "KeepAliveInterval" && setting is SettingEntry<int> settingInt && settingView is IntSettingView settingViewInt)
                    {
                        settingInt.SettingChanged += delegate
                        {
                            settingViewInt.DisplayName = settingInt.GetDisplayNameFunc();
                        };
                    }
                    if (!(settingView is SettingsView)) container.Show(settingView);
                    if (setting.EntryKey == "KeepAliveButton1") new Label() { Parent = rootflowPanel, Text = "Trigger In Combat", AutoSizeWidth = true };
                }
            }

            rootflowPanel.ShowBorder = true;
            rootflowPanel.CanCollapse = true;
        }
        public static void SetMsg(string text)
        {
            if (AfkSettingsView.messagePadding == null) return;
            AfkSettingsView.messagePadding.message = text;
        }
        private class Padding : Control
        {
            public string message = "";
            public Padding(int height = 16)
            {
                Size = new Point(0, height);
            }
            protected override void Paint(SpriteBatch spriteBatch, Rectangle bounds)
            {
                Width = Parent.Width;
                if (message == "") return;
                spriteBatch.DrawStringOnCtrl(this, message, GameService.Content.DefaultFont14, new Rectangle(0, 0, Width, Height), Color.Red, false, false, 1, HorizontalAlignment.Center, VerticalAlignment.Middle);
            }
        }
    }
}
