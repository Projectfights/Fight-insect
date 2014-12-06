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

        private AnimatedTexture2D LoadAnim(String name)
        {
            var sheet = Load<Texture2D>("Image/" + name + "Sheet");
            var anim = Load<AnimatedTexture2D>("Image/" + name);
            anim.SetSheet(sheet);
            return anim;
        }
        private Fighter GetFighter(Vector2 pos, Texture2D overlay, FighterInput input, bool flip, String type)
        {
            switch (type)
            {
                case ("wasp"):
                    var waspIdle = LoadAnim("waspIdle");
                    var waspPunch = LoadAnim("waspPunch");
                    List<HitBox.HitBoxFrame> hitFrames = new List<HitBox.HitBoxFrame>();
                    hitFrames.Add(new HitBox.HitBoxFrame(waspPunch.GetNumFrames() / waspPunch.GetFps(), new Rectangle(waspPunch.GetWidth(), 50, 10, 10), 10));
                    var punch = new HitBox(p1, hitFrames); //Gets null for p1

                    float speed = .75f;
                    double health = 100;
                    double power = 10;

                    return new Fighter(pos, overlay, waspIdle, waspPunch, input, punch, flip, speed, health, power);
                case ("beetle"):
                    var beetleIdle = LoadAnim("beetleIdle");
                    var beetlePunch = LoadAnim("beetlePunch");
                    List<HitBox.HitBoxFrame> hitFrames2 = new List<HitBox.HitBoxFrame>();
                    hitFrames2.Add(new HitBox.HitBoxFrame(beetlePunch.GetNumFrames(), new Rectangle(beetlePunch.GetWidth(), 50, 10, 10), 10));
                    var punch2 = new HitBox(p2, hitFrames2); //Gets null for p2, fix is below
                    
                    float speed2 = .25f;
                    double health2 = 150;
                    double power2 = 15;

                    return new Fighter(pos, overlay, beetleIdle, beetlePunch, input, punch2, flip, speed2, health2, power2);
            }

            return null;
        }

        public GameScreen(int widthScreen, int heightScreen, IScreenMaster master) : base(widthScreen, heightScreen, master)
        {
            p = new Physics();
            bg = new Background(Load<Texture2D>("Image/stage"));

            var red = Load<Texture2D>("Image/red");
            var blue = Load<Texture2D>("Image/blue");

            var pos1 = new Vector2(50, 150);
            var pos2 = new Vector2(440, 150);
            var in1 = new FighterInput(PlayerIndex.One);
            var in2 = new FighterInput(PlayerIndex.Two);


            p1 = GetFighter(pos1, blue, in1, false, Settings.P1_FIGHTER);
            p2 = GetFighter(pos2, red, in2, true, Settings.P2_FIGHTER);

            h1 = new HealthBar(p1, Load<Texture2D>("Image/blue"), false, new Vector2(10, 10));
            h2 = new HealthBar(p2, Load<Texture2D>("Image/red"), true, new Vector2(widthScreen - 110, 10));
        }

        public override void Update(GameTime gameTime)
        {
            p.Update(gameTime, new List<Dynamic>() { p1, p2 }, new List<GameObject>() { p1, p2, p1.GetPunch(), p2.GetPunch() });
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
