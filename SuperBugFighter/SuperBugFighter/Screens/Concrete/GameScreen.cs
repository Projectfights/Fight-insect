using Bug.GameObjects;
using Bug.Screens.Abstract;
using Bug.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Screens.Concrete
{
    class GameScreen : Screen
    {
        private Physics p;
        private Background bg;
        private Fighter p1, p2;

        public GameScreen(int widthScreen, int heightScreen, IScreenMaster master) : base(widthScreen, heightScreen, master)
        {
            p = new Physics();
            bg = new Background(Load<Texture2D>("Image/bg"));
            p1 = new Fighter(new Vector2(50, 150), Load<Texture2D>("Image/blue"), Keys.A, Keys.D);
            p2 = new Fighter(new Vector2(750, 150), Load<Texture2D>("Image/red"), Keys.Left, Keys.Right);
        }

        public override void Update(GameTime gameTime)
        {
            p.Update(gameTime, new List<Dynamic>() { p1, p2 }, new List<GameObject>() { p1, p2 });
            p1.Update(gameTime);
            p2.Update(gameTime);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            bg.Draw(spriteBatch);
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
