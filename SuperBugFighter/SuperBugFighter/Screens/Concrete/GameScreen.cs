using Bug.GameObjects;
using Bug.Screens.Abstract;
using Bug.Systems;
using Bug.Utils;
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
        private HealthBar h1, h2;
        private HitBox punch1, punch2;

        public GameScreen(int widthScreen, int heightScreen, IScreenMaster master) : base(widthScreen, heightScreen, master)
        {
            p = new Physics();
            bg = new Background(Load<Texture2D>("Image/stage"));
            
            var sheet = Load<Texture2D>("Image/waspIdleSheet");
            var anim = Load<AnimatedTexture2D>("Image/waspIdle");
            anim.SetSheet(sheet);
            var anim2 = Load<AnimatedTexture2D>("Image/waspIdle");
            anim.SetSheet(sheet);

            var in1 = new FighterInput(PlayerIndex.One);
            var in2 = new FighterInput(PlayerIndex.Two);

            List<HitBox.HitBoxFrame> hitFrames = new List<HitBox.HitBoxFrame>();
            hitFrames.Add(new HitBox.HitBoxFrame(100, new Rectangle(anim.GetWidth(), 50, 10, 10), 10));
            punch1 = new HitBox(p1, hitFrames); //Gets null for p1
            punch2 = new HitBox(p2, hitFrames); //Gets null for p2, fix is below

            p1 = new Fighter(new Vector2(50, 150), anim, in1, punch1, false, .5f);
            p2 = new Fighter(new Vector2(440, 150), anim2, in2, punch2, true, .5f);

            punch1.parent = p1;
            punch2.parent = p2;

            h1 = new HealthBar(p1, Load<Texture2D>("Image/blue"), new Vector2(10, 10));
            h2 = new HealthBar(p2, Load<Texture2D>("Image/red"), new Vector2(widthScreen - 110, 10));
        }

        public override void Update(GameTime gameTime)
        {
            p.Update(gameTime, new List<Dynamic>() { p1, p2 }, new List<GameObject>() { p1, p2, punch1, punch2 });
            p1.Update(gameTime);
            p2.Update(gameTime);
            if (p1.Health <= 0 || p2.Health <= 0)
            {
                ChangeScreen<MainMenuScreen>();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            bg.Draw(spriteBatch);
            p1.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            h1.Draw(spriteBatch);
            h2.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
