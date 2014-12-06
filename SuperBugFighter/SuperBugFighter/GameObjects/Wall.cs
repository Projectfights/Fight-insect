using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.GameObjects
{
    class Wall : GameObject
    {
        private Rectangle bounds;

        public Wall(Rectangle bounds_)
            : base(new Vector2(bounds_.X, bounds_.Y))
        {
            bounds = bounds_;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
           
        }

        public override Rectangle GetBoundingBox()
        {
            return new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

    }
}
