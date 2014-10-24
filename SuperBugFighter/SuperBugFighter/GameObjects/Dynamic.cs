using Bug;
using Bug.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.GameObjects
{
    abstract class Dynamic : GameObject
    {
        private Vector2 oldPos;
        public Vector2 Vel { get; set; }

        //Default constuctor, sets position to (0,0)
        public Dynamic(): base()
        {
            Vel = new Vector2();
        }

        //Constructor, sets the initial position to the given point
        public Dynamic(Vector2 pos)
            : base(pos)
        {
            Vel = new Vector2();
        }

        //Constructor, sets the initial position to the given point
        public Dynamic(Vector2 pos, Vector2 vel_)
            : base(pos)
        {
            Vel = vel_;
        }

        public override void SetPos(Vector2 pos)
        {
            oldPos = Pos;
            base.SetPos(pos);
        }

        public void ResetPos()
        {
            SetPos(oldPos);
        }

        public void ResetPosX()
        {
            SetPos(new Vector2(oldPos.X, Pos.Y));
        }

        public void ResetPosY()
        {
            SetPos(new Vector2(Pos.X, oldPos.Y));
        }

        public abstract void OnCollision(GameObject other, Direction dir);
    }
}
