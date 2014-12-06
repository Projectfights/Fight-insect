using Bug.Display;
using Bug.GameObjects;
using Bug.Screens.Abstract;
using Bug.Utils;
using GameLogic.Model.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Screens.Concrete
{
    class SelectScreen1 : MenuScreen
    {
        public SelectScreen1(int width, int height, IScreenMaster master)
            : base(width, height, master)
        {
            Texture2D up = Load<Texture2D>("Image/buttonUp");
            Texture2D down = Load<Texture2D>("Image/buttonDown");
            SpriteFont font = Load<SpriteFont>("Font/text");

            int centerX = widthScreen / 2 - up.Width / 2;

            ColorRect c = new ColorRect(0, 0, widthScreen, heightScreen, Color.Black);
            bg = c;

            Label t = new Label(widthScreen / 2, 10, font, "P1, Select Character");
            t.x -= (int)t.GetDim().X / 2;
            title = t;

            Button back = new TextButton(centerX, heightScreen - up.Height - 10, up, down, font, "Back");

            back.select += delegate() { ChangeScreen<MainMenuScreen>(); };

            buttons.Add(back);


            CharacterButton wasp = new CharacterButton(100, 100, LoadAnim("waspIdle"), up, down);
            wasp.select += delegate() { Settings.P1_FIGHTER = "wasp"; ChangeScreen<SelectScreen2>(); };
            buttons.Add(wasp);

            CharacterButton beetle = new CharacterButton(180, 100, LoadAnim("beetleIdle"), up, down);
            beetle.select += delegate() { Settings.P1_FIGHTER = "beetle"; ChangeScreen<SelectScreen2>(); };
            buttons.Add(beetle);
        }

        private AnimatedTexture2D LoadAnim(String name)
        {
            var sheet = Load<Texture2D>("Image/" + name + "Sheet");
            var anim = Load<AnimatedTexture2D>("Image/" + name);
            anim.SetSheet(sheet);
            return anim;
        }
    }

    class SelectScreen2 : MenuScreen
    {
         public SelectScreen2(int width, int height, IScreenMaster master)
            : base(width, height, master)
        {
            Texture2D up = Load<Texture2D>("Image/buttonUp");
            Texture2D down = Load<Texture2D>("Image/buttonDown");
            SpriteFont font = Load<SpriteFont>("Font/text");

            int centerX = widthScreen / 2 - up.Width / 2;

            ColorRect c = new ColorRect(0, 0, widthScreen, heightScreen, Color.Black);
            bg = c;

            Label t = new Label(widthScreen / 2, 10, font, "P2, Select Character");
            t.x -= (int)t.GetDim().X / 2;
            title = t;

            Button back = new TextButton(centerX, heightScreen - up.Height - 10, up, down, font, "Back");

            back.select += delegate() { ChangeScreen<SelectScreen1>(); };

            buttons.Add(back);


            CharacterButton wasp = new CharacterButton(100, 100, LoadAnim("waspIdle"), up, down);
            wasp.select += delegate() { Settings.P2_FIGHTER = "wasp"; ChangeScreen<GameScreen>(); };
            buttons.Add(wasp);

            CharacterButton beetle = new CharacterButton(180, 100, LoadAnim("beetleIdle"), up, down);
            beetle.select += delegate() { Settings.P2_FIGHTER = "beetle"; ChangeScreen<GameScreen>(); };
            buttons.Add(beetle);
        }

        private AnimatedTexture2D LoadAnim(String name)
        {
            var sheet = Load<Texture2D>("Image/" + name + "Sheet");
            var anim = Load<AnimatedTexture2D>("Image/" + name);
            anim.SetSheet(sheet);
            return anim;
        }
    }
}
