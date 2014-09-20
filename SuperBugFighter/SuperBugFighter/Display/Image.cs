using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Display
{

    class Image : UiElement
    {
        protected Texture2D tex;

        public Image(int x, int y, Texture2D tex_)
            : base(x, y)
        {
            tex = tex_;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(tex, new Rectangle(x, y, tex.Width, tex.Height), Color.White);
        }

        public override bool Collide(int pointX, int pointY)
        {
            return pointX > x && pointX < (x + tex.Width) && pointY > y && pointY < (y + tex.Height);
        }
    }
}
