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
        protected Vector2 pos;

        //Default constuctor, sets position to (0,0)
        public GameObject()
        {
            this.pos = new Vector2();
        }

        //Constructor, sets the initial position to the given point
        public GameObject(Vector2 pos)
        {
            this.pos = pos;
        }

        //Update the state of this GameObject
        public abstract void Update(GameTime gameTime);

        //Draw this GameObject to the screen
        public abstract void Draw(SpriteBatch spriteBatch);



    }
}
