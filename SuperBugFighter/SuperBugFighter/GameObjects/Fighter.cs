using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.GameObjects
{
    class Fighter : GameObject
    {
        private Texture2D tex;
        private Keys left, right;

        public Fighter(Vector2 pos, Texture2D tex_, Keys left_, Keys right_) : base(pos)
        {
            tex = tex_;
            left = left_;
            right = right_;
        }

        public override void Update(GameTime gameTime)
        {
            Keys[] newKeys = Keyboard.GetState().GetPressedKeys();
            foreach(Keys k in newKeys)
            {
                if (k == left)
                {
                    pos.X -= 3;
                }
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
