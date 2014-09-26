using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bug
{
    public abstract class GameObject
    {
        protected Vector2 pos;

        public GameObject()
        {
            this.pos = new Vector2();
        }

        public GameObject(Vector2 pos)
        {
            this.pos = pos;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);



    }
}
