using Bug.Systems;
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
    class Fighter : Dynamic
    {
        //Fighter texture
        private Texture2D tex;

        private float speed;

        //Keys to use for left and right
        private Keys left, right;

        public Fighter(Vector2 pos, Texture2D tex_, Keys left_, Keys right_) : base(pos)
        {
            tex = tex_;
            speed = .5f;
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
                    Vel = new Vector2(-speed, Vel.Y);
                }
                //Move right if right is pressed
                else if (k == right)
                {
                    Vel = new Vector2(speed, Vel.Y);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, GetBoundingBox(), Color.White);
        }

        public override Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Pos.X, (int)Pos.Y, tex.Width, tex.Height);
        }

        public override void OnCollision(GameObject other, Direction dir)
        {
            if (other is Fighter)
            {
                float x = Vel.X;
                switch(dir)
                {
                    case Direction.E:
                        x = Math.Max(x, 0);
                        break;
                    case Direction.W:
                        x = Math.Min(x, 0);
                        break;
                    default:
                        break;
                }
                Vel = new Vector2(x, Vel.Y);
            }
        }
    }
}
