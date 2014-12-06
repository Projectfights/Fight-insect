using Bug.Display;
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
        private Image overlay;
        private bool punching;

        //Fighter texture
        private AnimatedTexture2D idleAnim, punchAnim;

        private AnimatedTexture2D anim
        {
            get
            {
                return punching ? punchAnim : idleAnim;
            }
        }

        public bool flip{get; private set;}

        //Horizontal move speed
        private float speed;
        private float invulnTime;
        private FighterInput input;

        private HitBox punch;

        //Figher health
        public double Health { get; private set; }
        public double Power { get; private set; }

        private Vector2 getOverlayPos()
        {
            return new Vector2((int)(Pos.X + anim.GetWidth() / 2 - 10), (int)(Pos.Y - 50));
        }

        public Fighter(Vector2 pos, Texture2D overlay_, AnimatedTexture2D idleAnim_, AnimatedTexture2D punchAnim_, FighterInput input_, HitBox punch_, bool flip_, float speed_, double health_, double power_)
            : base(pos)
        {
            idleAnim = idleAnim_;
            punchAnim = punchAnim_;

            Vector2 ov = getOverlayPos();
            overlay = new Image((int) ov.X, (int) ov.Y, overlay_);

            input = input_;
            flip = flip_;
            speed = speed_;
            Health = health_;
            Power = power_;
            punch = punch_;
            invulnTime = 0;

            punching = false;
            punch.parent = this;
        }

        private void HandleInputs()
        {
            double left = input.left();
            double right = input.right();
            bool gotDirInput = false;

            if (!punch.IsActive())
            {
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

                if (1 - input.up() < .001 && Vel.Y == 0)
                {
                    Vel = new Vector2(Vel.X, -speed * 5);
                }
            }

            //If no directional input, reset X velocity.
            if (!gotDirInput)
            {
                Vel = new Vector2(0, Vel.Y);
            }

            if(punching && punchAnim.Looped) 
            {
                idleAnim.Reset();
                punching = false;

                if (flip)
                {
                    SetPos(new Vector2(Pos.X + punchAnim.GetWidth() - idleAnim.GetWidth(), Pos.Y));
                }
            }


            //Punch if punch key is pressed
            if (input.punch() && !punching)
            {
                punch.Trigger();
                punchAnim.Reset();
                punching = true;

                if (flip)
                {
                    SetPos(new Vector2(Pos.X - punchAnim.GetWidth() + idleAnim.GetWidth(), Pos.Y));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 ov = getOverlayPos();
            overlay.x = (int) ov.X;
            overlay.y = (int) ov.Y;

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
            overlay.Draw(spriteBatch);
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

        internal GameObject GetPunch()
        {
            return punch;
        }
    }
}
