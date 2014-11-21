using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.GameObjects
{
    class FighterInput
    {
        private PlayerIndex player;

        private Keys leftKey
        {
            get
            {
                return player == PlayerIndex.One ? Keys.A : Keys.Left;
            }
        }

        private Keys rightKey
        {
            get
            {
                return player == PlayerIndex.One ? Keys.D : Keys.Right;
            }
        }

        private Keys upKey
        {
            get
            {
                return player == PlayerIndex.One ? Keys.W : Keys.Up;
            }
        }

        public FighterInput(PlayerIndex player_)
        {
            player = player_;
        }

        public double left()
        {
            if (GamePad.GetState(player).IsConnected)
            {
                if (GamePad.GetState(player).IsButtonDown(Buttons.DPadLeft))
                {
                    return 1;
                }
                else
                {
                    double val = GamePad.GetState(player).ThumbSticks.Left.X;
                    if (val < 0)
                        return Math.Abs(val);
                }
            }

            if (Keyboard.GetState().IsKeyDown(leftKey))
            {
                return 1;
            }

            return 0;
        }

        public double right()
        {
            if (GamePad.GetState(player).IsConnected)
            {
                if (GamePad.GetState(player).IsButtonDown(Buttons.DPadRight))
                {
                    return 1;
                }
                else
                {
                    double val = GamePad.GetState(player).ThumbSticks.Left.X;
                    if (val > 0)
                        return val;
                }
            }
            if (Keyboard.GetState().IsKeyDown(rightKey))
            {
                return 1;
            }

            return 0;
        }

        public double up()
        {
            if (GamePad.GetState(player).IsConnected)
            {
                if (GamePad.GetState(player).IsButtonDown(Buttons.DPadUp) || GamePad.GetState(player).IsButtonDown(Buttons.A))
                {
                    return 1;
                }
                else
                {
                    double val = GamePad.GetState(player).ThumbSticks.Left.Y;
                    if (val > .2)
                        return 1;
                }
            }
            if (Keyboard.GetState().IsKeyDown(upKey))
            {
                return 1;
            }
            return 0;
        }
    }
}
