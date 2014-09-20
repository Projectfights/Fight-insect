using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Display
{
    abstract class UiElement
    {
        public int x, y;

        public UiElement(int x_, int y_)
        {
            x = x_;
            y = y_;
        }

        public abstract bool Collide(int pointX, int pointY);

        public abstract void Draw(SpriteBatch batch);
    }
}
