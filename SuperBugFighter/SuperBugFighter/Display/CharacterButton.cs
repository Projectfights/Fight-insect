using Bug.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Display
{
    class CharacterButton : Button
    {
        private const int SIZE = 75;
        private const int PADDING = 5;
        AnimatedTexture2D anim;

        public CharacterButton(int x, int y, AnimatedTexture2D anim_, Texture2D up, Texture2D down) : base(x,y,up,down)
        {
            anim = anim_;
        }

        public override bool Collide(int pointX, int pointY)
        {
            return pointX > x && pointX < (x + SIZE) && pointY > y && pointY < (y + SIZE);
        }

        public override void Draw(SpriteBatch batch)
        {
            anim.UpdateTime(new GameTime(new TimeSpan(), new TimeSpan(0,0,0,0,1000/30)));
            batch.Draw(tex, new Rectangle(x,y, SIZE,SIZE), Color.White);
            batch.Draw(anim.GetSheet(), new Rectangle(x+PADDING, y+PADDING, (int)((float)anim.GetWidth()/anim.GetHeight()*(SIZE-2*PADDING)),
                SIZE-2*PADDING), anim.GetWindow(), Color.White);
        }
    }
}
