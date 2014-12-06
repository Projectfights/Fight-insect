using Bug.Systems;
using Bug.Utils;
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
        private AnimatedTexture2D anim;

        public bool flip{get; private set;}

        //Horizontal move speed
        private float speed;
        private float invulnTime;
        private FighterInput input;

        private HitBox punch;

        //Figher health
        public double Health {get; private set; }

        public Fighter(Vector2 pos, AnimatedTexture2D anim_, FighterInput input_, HitBox punch_, bool flip_, float speed_) : base(pos)
        {
            anim = anim_;
            input = input_;
            flip = flip_;
            speed = speed_;
            Health = 100;
            punch = punch_;
            invulnTime = 0;
        }

        private void HandleInputs()
        {
            double left = input.left();
            double right = input.right();
            bool gotDirInput = false;

            //Move left if left is pressed
            if (left > 0)
            {
                Vel = new Vector2((float)(-speed * left), Vel.Y);
                gotDirInput = true;
                flip = true;
            }
            //Move right if right is pressed
            else if (right > 0)
            {
                Vel = new Vector2((float)(speed * right), Vel.Y);
                gotDirInput = true;
                flip = false;
            }
            //Jump if up is pressed

            if (input.up() == 1 && Vel.Y == 0)
            {
                Vel = new Vector2(Vel.X, -speed * 5);
            }

            //If no directional input, reset X velocity.
            if (!gotDirInput)
            {
                Vel = new Vector2(0, Vel.Y);
            }

            //Punch if punch key is pressed
            if (input.punch())
            {
                punch.Trigger();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (invulnTime > 0)
            {
                invulnTime -= gameTime.ElapsedGameTime.Milliseconds;
            }
            anim.UpdateTime(gameTime);
            punch.Update(gameTime);
            HandleInputs();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, GetBoundingBox(), new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0, Vector2.Zero, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(anim.GetSheet(), GetBoundingBox(), anim.GetWindow(), Color.White, 0, Vector2.Zero, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public override Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Pos.X, (int)Pos.Y, anim.GetWidth(), anim.GetHeight());//new Rectangle((int)Pos.X, (int)Pos.Y, tex.Width, tex.Height);
        }

        public override void OnCollision(GameObject other, Direction dir)
        {
            if (other is HitBox && invulnTime <= 0)
            {
                HitBox h = (HitBox)other;
                if (h.parent != this)
                {
                    this.Health -= h.GetDamage();
                    invulnTime = 1000;
                }
            }
            if (other is Fighter)
            {
                switch(dir)
                {
                    case Direction.N:
                        Vel = new Vector2(Vel.X, Math.Max(Vel.Y,0));
                       // ResetPosY();
                        //Decrease health if hit from above
                        Health -= 1.0 / 500.0;
                        break;
                    case Direction.E:
                        Vel = new Vector2(Math.Max(Vel.X, 0), Vel.Y);
                        //ResetPosX();
                        break;
                    case Direction.S:
                        Vel = new Vector2(Vel.X, Math.Min(Vel.Y, 0));
                       // ResetPosY();
                        break;
                    case Direction.W:
                        Vel = new Vector2(Math.Min(Vel.X, 0), Vel.Y);
                        //ResetPosX();
                        break;
                }
            }
        }
    }
}
