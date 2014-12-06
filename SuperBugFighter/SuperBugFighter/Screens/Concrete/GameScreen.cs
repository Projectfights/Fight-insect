using Bug.Display;
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
using System.Threading;

namespace Bug.Screens.Concrete
{
    class GameScreen : Screen
    {
        private Timer timer;

        private Image center;
        private bool pause;

        private Physics p;
        private Background bg;
        private Fighter p1, p2;
        private HealthBar h1, h2;
        private Wall w1, w2;

        private AnimatedTexture2D LoadAnim(String name)
        {
            var sheet = Load<Texture2D>("Anim/" + name + "Sheet");
            var anim = Load<AnimatedTexture2D>("Anim/" + name);
            anim.SetSheet(sheet);
            return anim;
        }
        private Fighter GetFighter(Vector2 pos, Texture2D overlay, FighterInput input, bool flip, String type)
        {
            switch (type)
            {
                case ("wasp"):
                    float speed = .7f;
                    double health = 100;
                    double power = 10;

                    var waspIdle = LoadAnim("waspIdle");
                    var waspPunch = LoadAnim("waspPunch");
                    var waspRecoil = LoadAnim("waspRecoil");
                    List<HitBox.HitBoxFrame> hitFrames = new List<HitBox.HitBoxFrame>();
                    hitFrames.Add(new HitBox.HitBoxFrame(waspPunch.GetNumFrames() / waspPunch.GetFps(), new Rectangle(waspPunch.GetWidth(), 50, 10, 10), power));
                    var punch = new HitBox(p1, hitFrames); //Gets null for p1


                    return new Fighter(pos, overlay, waspIdle, waspPunch, waspRecoil, input, punch, flip, speed, health, power);
                case ("beetle"):
                    float speed2 = .4f;
                    double health2 = 100;
                    double power2 = 11;

                    var beetleIdle = LoadAnim("beetleIdle");
                    var beetlePunch = LoadAnim("beetlePunch");
                    var beetleRecoil = LoadAnim("beetleRecoil");
                    List<HitBox.HitBoxFrame> hitFrames2 = new List<HitBox.HitBoxFrame>();
                    hitFrames2.Add(new HitBox.HitBoxFrame(beetlePunch.GetNumFrames(), new Rectangle(beetlePunch.GetWidth(), 50, 10, 10), power2));
                    var punch2 = new HitBox(p2, hitFrames2); //Gets null for p2, fix is below

                    return new Fighter(pos, overlay, beetleIdle, beetlePunch, beetleRecoil, input, punch2, flip, speed2, health2, power2);
            }

            return null;
        }

        public GameScreen(int widthScreen, int heightScreen, IScreenMaster master) : base(widthScreen, heightScreen, master)
        {
            p = new Physics();
            bg = new Background(Load<Texture2D>("Image/stage"));

            var pos1 = new Vector2(50, 150);
            var pos2 = new Vector2(440, 150);
            
            p1 = GetFighter(pos1, Load<Texture2D>("Image/p1"), Settings.in1, false, Settings.P1_FIGHTER);
            p2 = GetFighter(pos2, Load<Texture2D>("Image/p2"), Settings.in2, true, Settings.P2_FIGHTER);

            h1 = new HealthBar(p1, Load<Texture2D>("Image/blue"), false, new Vector2(10, 10));
            h2 = new HealthBar(p2, Load<Texture2D>("Image/red"), true, new Vector2(widthScreen - 110, 10));

            w1 = new Wall(new Rectangle(-30,-1000,30,1000+heightScreen));
            w2 = new Wall(new Rectangle(widthScreen, -1000, 30, 1000+heightScreen));

        }

        public override void Update(GameTime gameTime)
        {
            if (!pause)
            {
                p.Update(gameTime, new List<Dynamic>() { p1, p2 }, new List<GameObject>() { p1, p2, p1.GetPunch(), p2.GetPunch(), w1, w2 });
                p1.Update(gameTime);
                p2.Update(gameTime);

                Texture2D tex = null;
                if (p1.Health <= 0 && p2.Health <= 0)
                {
                    // Draw
                    tex = Load<Texture2D>("Image/draw");
                }
                else if (p2.Health <= 0)
                {
                    // P1 Wins
                    tex = Load<Texture2D>("Image/p1win");
                }
                else if (p1.Health <= 0)
                {
                    // P2 Wins
                    tex = Load<Texture2D>("Image/p2win");
                }

                if (tex != null)
                {
                    pause = true;
                    center = new Image(widthScreen / 2 - tex.Width / 2, heightScreen / 2 - tex.Height / 2, tex);
                    timer = new Timer(obj => { ChangeScreen<MainMenuScreen>(); }, null, 3000, System.Threading.Timeout.Infinite);
                }
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

            if (center != null)
            {
                center.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
