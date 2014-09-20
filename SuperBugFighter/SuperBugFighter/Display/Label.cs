using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Display
{
    class Label : UiElement
    {
        private SpriteFont font;
        private string text;

        public Label(int x, int y, SpriteFont font_, String text_)
            : base(x, y)
        {
            font = font_;
            text = text_;
        }

        public Vector2 GetDim()
        {
            return font.MeasureString(text);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.DrawString(font, text, new Vector2(x, y), Color.White);
        }

        public override bool Collide(int pointX, int pointY)
        {
            Vector2 dim = font.MeasureString(text);
            return pointX > x && pointX < (x + dim.X) && pointY > y && pointY < (y + dim.Y);
        }
    }
}
