using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Display
{
    public delegate void OnSelect();

    class Button : UiElement
    {
        public OnSelect select;

        private bool pressed;
        private Texture2D up, down;

        public Button(int x, int y, Texture2D up_, Texture2D down_) : base(x, y)
        {
            pressed = false;
            up = up_;
            down = down_;
        }

        public override bool Collide(int pointX, int pointY)
        {
            return pointX > x && pointX < (x + tex.Width) && pointY > y && pointY < (y + tex.Height);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(tex, new Rectangle(x, y, tex.Width, tex.Height), Color.White);
        }

        public void SetPressed(bool pressed_, bool toggle = true)
        {
            if (pressed && !pressed_ && select != null && toggle)
            {
                select();
            }
            pressed = pressed_;
        }

        protected Texture2D tex
        {
            get 
            {
                return pressed ? down : up;
            }
        }


        internal bool IsPressed()
        {
            return pressed;
        }
    }
}
