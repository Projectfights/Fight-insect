using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Display
{
    class SizedButton : Button
    {
        private Rectangle bounds;

        public SizedButton(Rectangle bounds, Texture2D up, Texture2D down) : base(x,y,up,down)
        {

        }

        public override bool Collide(int pointX, int pointY)
        {
            return bounds.Contains(bounds.X, bounds.Y);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(tex, bounds, Color.White);
        }
    }
}
