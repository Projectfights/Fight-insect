using Bug.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Screens.Abstract
{
    public abstract class Screen
    {
        private IScreenMaster c;

        protected int widthScreen, heightScreen;

        public Screen(int widthScreen_, int heightScreen_, IScreenMaster c_)
        {
            widthScreen = widthScreen_;
            heightScreen = heightScreen_;
            c = c_;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch batch);

        public C Load<C>(string name)
        {
            return c.Load<C>(name);
        }

        protected void ChangeScreen<S>() where S : Screen
        {
            c.Change<S>();
        }
    }
}
