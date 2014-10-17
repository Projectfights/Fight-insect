using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.GameObjects
{
    //A player-controlled fighter
    class Fighter : GameObject
    {
        //Fighter texture
        private Texture2D tex;
        //Keys to use for left and right
        private Keys left, right;

        public Fighter(Vector2 pos, Texture2D tex_, Keys left_, Keys right_) : base(pos)
        {
            tex = tex_;
            left = left_;
            right = right_;
        }

        public override void Update(GameTime gameTime)
        {
            //Iterate over pressed keys to check if the left/right keys for this figher are pressed
            Keys[] newKeys = Keyboard.GetState().GetPressedKeys();
            foreach(Keys k in newKeys)
            {
                //Move left if left is pressed
                if (k == left)
                {
                    pos.X -= 3;
                }
                //Move right if right is pressed
                else if (k == right)
                {
                    pos.X += 3;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle((int) pos.X, (int) pos.Y, tex.Width, tex.Height), Color.White);
        }
    }
}
