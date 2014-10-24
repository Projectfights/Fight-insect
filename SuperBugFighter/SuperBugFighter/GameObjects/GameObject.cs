using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bug
{
    //GameObject is the base class for all entities in the game
    //A GameObject can be Updated and Drawn
    public abstract class GameObject
    {
        //Position of the GameObject
        public Vector2 Pos { get; private set; }

        //Default constuctor, sets position to (0,0)
        public GameObject()
        {
            this.Pos = new Vector2();
        }

        //Constructor, sets the initial position to the given point
        public GameObject(Vector2 pos)
        {
            this.Pos = pos;
        }

        public virtual void SetPos(Vector2 pos)
        {
            Pos = pos;
        }

        //Update the state of this GameObject
        public abstract void Update(GameTime gameTime);

        //Draw this GameObject to the screen
        public abstract void Draw(SpriteBatch spriteBatch);

        // Get the collision box for the game object
        public abstract Rectangle GetBoundingBox();
    }
}
