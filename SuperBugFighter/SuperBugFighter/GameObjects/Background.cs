using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.GameObjects
{
    class Background : GameObject
    {
        Texture2D tex;

        public Background(Texture2D tex_) : base(Vector2.Zero)
        {
            tex = tex_;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, GetBoundingBox(), Color.White);
        }

        public override Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Pos.X, (int)Pos.Y, tex.Width, tex.Height);
        }

    }
}
