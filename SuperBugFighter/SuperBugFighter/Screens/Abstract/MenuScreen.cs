using Bug.Display;
using Bug.GameObjects;
using Bug.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Screens.Abstract
{
    abstract class MenuScreen : Screen
    {
        protected FighterInput input;

        protected UiElement bg, title;
        protected List<Button> buttons;
        protected int current;
        protected bool buttonPressed;
        private long timeout;
        private bool hovering;
        private bool startHovering;

        public MenuScreen(int widthScreen, int heightScreen, IScreenMaster master)
            : base(widthScreen, heightScreen, master)
        {
            buttons = new List<Button>();
            current = -1;
            buttonPressed = false;
            timeout = 0;
            hovering = false;
            hovering = true;
        }

        private void UpdateButton(Button button, bool sel)
        {
            if (current == -1)
            {
                if (button.Collide(Mouse.GetState().Position.X, Mouse.GetState().Position.Y))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        button.SetPressed(true);
                    }
                    else
                    {
                        if (button.IsPressed())
                        {
                            Audio.GetInstance().Play(Load<SoundEffect>("Audio/buttonClick"));
                        }
                        button.SetPressed(false);
                    }

                    if (startHovering)
                    {
                        Audio.GetInstance().Play(Load<SoundEffect>("Audio/buttonHover"));
                        startHovering = false;
                    }

                    hovering = true;
                }
            }
            else
            {
                startHovering = true;

                if (sel && buttonPressed && !input.select())
                {
                    Audio.GetInstance().Play(Load<SoundEffect>("Audio/buttonClick"));
                    button.SetPressed(false);
                }
                else if (sel)
                {
                    button.SetPressed(true);
                }
                else
                {
                    button.SetPressed(false, false);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(input.select()) 
            {
                buttonPressed = true;
            }

            if (timeout <= 0)
            {
                if (input.up() > .999)
                {
                    Audio.GetInstance().Play(Load<SoundEffect>("Audio/buttonHover"));
                    buttonPressed = false;

                    if (current == -1)
                    {
                        current = 0;
                    }
                    else
                    {
                        current = (current - 1 + buttons.Count) % buttons.Count;
                    }

                    timeout = 200;
                }
                else if (input.down() > .999)
                {
                    Audio.GetInstance().Play(Load<SoundEffect>("Audio/buttonHover"));
                    buttonPressed = false;

                    if (current == -1)
                    {
                        current = 0;
                    }
                    else
                    {
                        current = (current + 1) % buttons.Count;
                    }

                    timeout = 200;
                }
            }
            else
            {
                timeout -= gameTime.ElapsedGameTime.Milliseconds;
            }

            hovering = false;

            for(int i = 0; i < buttons.Count; i++)
            {
                UpdateButton(buttons[i], i == current);
            }

            startHovering = !hovering;
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Begin();
            bg.Draw(batch);
            title.Draw(batch);
            foreach(Button button in buttons) 
            {
                button.Draw(batch);
            }
            batch.End();
        }
    }
}
