using Bug.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bug.Screens.Abstract;
using GameLogic.Model.Display;

namespace Bug.Screens.Concrete
{
    class LoadGameScreen : MenuScreen
    {
        public LoadGameScreen(int widthScreen, int heightScreen, IScreenMaster master) : base(widthScreen, heightScreen, master)
        {
            Texture2D up = Load<Texture2D>("Image/buttonUp");
            Texture2D down = Load<Texture2D>("Image/buttonDown");
            SpriteFont font = Load<SpriteFont>("Font/text");

            int centerX = widthScreen / 2 - up.Width / 2;

            ColorRect c = new ColorRect(0, 0, widthScreen, heightScreen, Color.Black);
            bg = c;

            Label t = new Label(widthScreen / 2, 10, font, "Load Game");
            t.x -= (int)t.GetDim().X / 2;
            title = t;

            Button back = new TextButton(centerX, heightScreen - up.Height - 10, up, down, font, "Back");

            back.select += delegate() { ChangeScreen<MainMenuScreen>(); };

            buttons.Add(back);
        }
    }
}
