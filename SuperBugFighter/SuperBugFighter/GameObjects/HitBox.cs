using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bug;
using Bug.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bug.GameObjects
{
    class HitBox : GameObject
    {
        public Fighter parent;
        private double elapsedTime;
        private int index;
        private bool active;
        private List<HitBoxFrame> frames;        

        public HitBox(Fighter parent, List<HitBoxFrame> frames)
            : base()
        {
            this.parent = parent;
            this.elapsedTime = 0;
            this.index = 0;
            active = false;
            this.frames = frames;
        }


        public override void Update(GameTime t)
        {
            if (active)
            {
                elapsedTime += t.ElapsedGameTime.Milliseconds;

                while (elapsedTime >= frames[index].frameTime)
                {
                    elapsedTime -= frames[index].frameTime;
                    index++;
                    if (index >= frames.Count)
                    {
                        elapsedTime = 0;
                        index = 0;
                        active = false;
                        break;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch s)
        {
            
        }

        public override Rectangle GetBoundingBox()
        {
            if (active)
            {
                Rectangle rect = new Rectangle();
                rect.X =  frames[index].box.X + (parent.flip?-frames[index].box.Width-parent.GetBoundingBox().Width:0) + (int)parent.Pos.X;
                rect.Y = frames[index].box.Y + (int)parent.Pos.Y;
                rect.Width = frames[index].box.Width;
                rect.Height = frames[index].box.Height;
                return rect;
            }
            else
            {
                return new Rectangle(-100, -100, 0, 0);
            }
        }

        public void Trigger()
        {
            active = true;
        }

        public bool IsActive()
        {
            return active;
        }

        public double GetDamage()
        {
            return frames[index].damage;
        }

        public struct HitBoxFrame
        {
            public double frameTime;
            public Rectangle box;
            public double damage;

            public HitBoxFrame(double time, Rectangle box, double damage)
            {
                this.frameTime = time;
                this.box = box;
                this.damage = damage;
            }
        }
    }

    
}
