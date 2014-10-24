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

        //Horizontal move speed
        private float speed;

        //Keys to use for left and right
        private Keys left, right, up;

        //Figher health
        public double Health {get; private set; }

        public Fighter(Vector2 pos, Texture2D tex_, float speed_, Keys left_, Keys right_, Keys up_) : base(pos)
        {
            tex = tex_;
            speed = speed_;
            left = left_;
            right = right_;
            up = up_;
            Health = 1;
        }

        public override void Update(GameTime gameTime)
        {
            //Iterate over pressed keys to check if the left/right keys for this figher are pressed
            Keys[] newKeys = Keyboard.GetState().GetPressedKeys();
            bool gotDirInput = false;
            foreach(Keys k in newKeys)
            {
                //Move left if left is pressed
                if (k == left)
                {
                    Vel = new Vector2(-speed, Vel.Y);
                    gotDirInput = true;
                }
                //Move right if right is pressed
                else if (k == right)
                {
                    Vel = new Vector2(speed, Vel.Y);
                    gotDirInput = true;
                }
                //Jump if up is pressed
                else if (k == up && Vel.Y == 0)
                {
                    Vel = new Vector2(Vel.X, -speed * 5);
                }
            }

            //If no directional input, reset X velocity.
            if (!gotDirInput)
            {
               Vel = new Vector2(0, Vel.Y);
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
                switch(dir)
                {
                    case Direction.N:
                        Vel = new Vector2(Vel.X, Math.Min(Vel.Y,0));
                        ResetPosY();
                        //Decrease health if hit from above
                        Health -= 1.0 / 500.0;
                        break;
                    case Direction.E:
                        Vel = new Vector2(Math.Max(Vel.X, 0), Vel.Y);
                        ResetPosX();
                        break;
                    case Direction.S:
                        Vel = new Vector2(Vel.X, Math.Max(Vel.Y, 0));
                        ResetPosY();
                        break;
                    case Direction.W:
                        Vel = new Vector2(Math.Min(Vel.X, 0), Vel.Y);
                        ResetPosX();
                        break;
                }
            }
        }
    }
}
