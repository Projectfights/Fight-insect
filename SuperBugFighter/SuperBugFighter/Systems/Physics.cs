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
        private static readonly double GroundY = 270;

        public void ApplyGravity(Dynamic o)
        {
            if (o.GetBoundingBox().Bottom < GroundY)
            {
                o.Vel = new Vector2(o.Vel.X, (float)(o.Vel.Y * (1 - Gravity) + GSpeed * Gravity));
            }
            else
            {
                o.Vel = new Vector2(o.Vel.X, Math.Min(o.Vel.Y, 0));
                o.SetPos(new Vector2(o.Pos.X, (float)(GroundY - o.GetBoundingBox().Height)));
            }
        }

        private void Collision(Dynamic o1, GameObject o2)
        {
            Rectangle bb1 = o1.GetBoundingBox();
            Rectangle bb2 = o2.GetBoundingBox();

            if (bb1.Intersects(bb2))
            {
                //Calculate how far you would need to move
                //in each direction to get out
                int left = Math.Abs(bb1.Right - bb2.Left);
                int top = Math.Abs(bb1.Bottom - bb2.Top);
                int right = Math.Abs(bb1.Left - bb2.Right);
                int bot = Math.Abs(bb1.Top - bb2.Bottom);

                if (top < bot)
                {
                    if (left < right)
                    {
                        o1.OnCollision(o2, top < left ? Direction.S : Direction.E);
                        if (o2 is Dynamic)
                        {
                            Dynamic do2 = (Dynamic)o2;
                            do2.OnCollision(o1, top < left ? Direction.N : Direction.W);
                        }
                    }
                    else
                    {
                        o1.OnCollision(o2, top < right ? Direction.S : Direction.W);
                        if (o2 is Dynamic)
                        {
                            Dynamic do2 = (Dynamic)o2;
                            do2.OnCollision(o1, top < right ? Direction.N : Direction.E);
                        }
                    }
                }
                else
                {
                    if (left < right)
                    {
                        o1.OnCollision(o2, bot < left ? Direction.N : Direction.E);
                        if (o2 is Dynamic)
                        {
                            Dynamic do2 = (Dynamic)o2;
                            do2.OnCollision(o1, bot < left ? Direction.S : Direction.W);
                        }
                    }
                    else
                    {
                        o1.OnCollision(o2, bot < right ? Direction.N : Direction.W);
                        if (o2 is Dynamic)
                        {
                            Dynamic do2 = (Dynamic)o2;
                            do2.OnCollision(o1, bot < right ? Direction.S : Direction.E);
                        }
                    }
                }

               /*
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
                */ 
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

                //o.Vel = new Vector2(0, o.Vel.Y);
            }
        }
    }
}
