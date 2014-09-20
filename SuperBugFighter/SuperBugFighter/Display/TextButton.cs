using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Display
{
    class TextButton : Button
    {
        private SpriteFont font;
        private string text;

        public TextButton(int x, int y, Texture2D up, Texture2D down, SpriteFont font_, String text_) : base(x, y, up, down)
        {
            font = font_;
            text = text_;
        }

        public override void Draw(SpriteBatch batch)
        {
 	        base.Draw(batch);
            Vector2 dim = font.MeasureString(text);
            int textX = x + tex.Width / 2 - (int)dim.X / 2;
            int textY = y + tex.Height / 2 - (int)dim.Y / 2;

            batch.DrawString(font, text, new Vector2(textX, textY), Color.White);
        }
    }
}
