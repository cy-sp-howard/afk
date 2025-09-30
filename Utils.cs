using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BhModule.Afk
{
    public static class Utils
    {
        public static NotifyClass Notify = new NotifyClass();
    }
   
    public class NotifyClass : Control
    {
        private float duration = 3000;
        private string message;
        private bool waitingForPaint = true;
        private DateTime msgStartTime = DateTime.Now;
        public override void DoUpdate(GameTime gameTime)
        {
            Size = new Point(Parent.Size.X, 200);
            Location = new Point(0, Parent.Size.Y / 10 * 2);
        }
        protected override void Paint(SpriteBatch spriteBatch, Rectangle bounds)
        {
            if (message != null)
            {
                if (waitingForPaint) msgStartTime = DateTime.Now;
                float existTime = (float)(DateTime.Now - msgStartTime).TotalMilliseconds;
                float remainTime = duration - (float)existTime;
                float opacity = remainTime > 1000 ? 1 : remainTime / 1000;
                if (opacity < 0)
                {
                    Clear();
                    return;
                }
                Color textColor = Color.Yellow * opacity;
                spriteBatch.DrawStringOnCtrl(this, message, GameService.Content.DefaultFont32, new Rectangle(0, 0, Width, Height), textColor, false, false, 1, HorizontalAlignment.Center, VerticalAlignment.Top);
            }
            waitingForPaint = false;
        }
        public void Clear()
        {
            Parent = null;
            message = null;
        }
        public void Show(string text, float duration = 3000)
        {
            Parent = GameService.Graphics.SpriteScreen;
            msgStartTime = DateTime.Now;
            message = text;
            this.duration = duration;
        }
        protected override CaptureType CapturesInput()
        {
            return CaptureType.None;
        }
    }
}
