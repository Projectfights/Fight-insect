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
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Bug.Utils;
using Bug.GameObjects;

namespace Bug.Screens.Concrete
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen(int widthScreen, int heightScreen, IScreenMaster master) : base(widthScreen, heightScreen, master)
        {
            SoundEffect song = Load<SoundEffect>("Audio/Menu");
 
            SoundEffectInstance soundEffectInstance = song.CreateInstance();
            soundEffectInstance.IsLooped = true;
            soundEffectInstance.Play();

            Settings.in1 = new FighterInput(PlayerIndex.One);
            Settings.in2 = new FighterInput(PlayerIndex.Two);

            input = Settings.in1;

            Texture2D up = Load<Texture2D>("Image/buttonUp");
            Texture2D down = Load<Texture2D>("Image/buttonDown");
            SpriteFont font = Load<SpriteFont>("Font/text");

            int centerX = widthScreen / 2 - up.Width / 2;
            int buttonStartY = heightScreen / 3;
            int buttonSpacing = 10;

            bg = new Image(0, 0, Load<Texture2D>("Image/mainBg"));

            Label t = new Label(widthScreen / 2, 10, font, "");
            t.x -= (int)t.GetDim().X / 2;
            title = t;

            Button newGame = new TextButton(300, 100, up, down, font, "Play?");
            //Button loadGame = new TextButton(centerX, buttonStartY + up.Height + buttonSpacing, up, down, font, "Load Game");
            //Button options = new TextButton(centerX, buttonStartY + 2 * (up.Height + buttonSpacing), up, down, font, "Options");

            newGame.select += delegate() { soundEffectInstance.Stop(); ChangeScreen<SelectScreen1>(); };
            //loadGame.select += delegate() { soundEffectInstance.Stop(); ChangeScreen<LoadGameScreen>(); };
            //options.select += delegate() { soundEffectInstance.Stop(); ChangeScreen<OptionScreen>(); };

            buttons.Add(newGame);
            //buttons.Add(loadGame);
            //buttons.Add(options);
        }
    }
}
