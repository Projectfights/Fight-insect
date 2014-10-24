using Bug;
using Bug.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Systems
{
    enum Direction
    {
        N,E,S,W
    }

    class Physics
    {
        private static readonly double Gravity = .1;
        private static readonly double GSpeed = .4;
        private static readonly double GroundY = 300;

        public void ApplyGravity(Dynamic o)
        {
            if (o.Pos.Y < GroundY)
            {
                o.Vel = new Vector2(o.Vel.X, (float)(o.Vel.Y * (1 - Gravity) + GSpeed * Gravity));
            }
            else
            {
                o.Vel = new Vector2(o.Vel.X, Math.Min(o.Vel.Y, 0));
            }
        }

        private void Collision(Dynamic o1, GameObject o2)
        {
            Rectangle bb1 = o1.GetBoundingBox();
            Rectangle bb2 = o2.GetBoundingBox();

            int left = bb1.Right - bb2.Left;
            int top = bb1.Bottom - bb2.Top;

            if (left > 0 && left < (bb1.Width + bb2.Width) && top > 0 && top < (bb1.Height + bb2.Height))
            {
                bool l = left < (bb1.Width + bb2.Width) / 2;
                bool t = top < (bb1.Height + bb2.Height) / 2;
                int xDist = l ? left : (bb1.Width + bb2.Width) - left;
                int yDist = t ? top : (bb1.Height + bb2.Height) - top;

                if (xDist > yDist)
                {
                    o1.OnCollision(o2, t ? Direction.N : Direction.S);
                }
                else
                {
                    o1.OnCollision(o2, l ? Direction.W : Direction.E);
                }
            }
        }

        public void Update(GameTime gameTime, List<Dynamic> objects, List<GameObject> environment)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalMilliseconds;

            foreach (Dynamic o in objects)
            {
                ApplyGravity(o);

                o.SetPos(o.Pos + o.Vel * (float)deltaTime);

                foreach (GameObject e in environment)
                {
                    if (o != e)
                    {
                        Collision(o, e);
                    }
                }

                o.Vel = new Vector2(0, o.Vel.Y);
            }
        }
    }
}
