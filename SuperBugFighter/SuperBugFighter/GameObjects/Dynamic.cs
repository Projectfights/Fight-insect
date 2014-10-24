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

        public abstract void OnCollision(GameObject other, Direction dir);
    }
}
