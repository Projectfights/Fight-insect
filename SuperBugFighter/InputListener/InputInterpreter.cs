using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InputListener
{
    class InputInterpreter
    {
        private HashSet<Keys> pressedKeys;
        private bool left, middle, right;
        private int scroll, scrollDelta;

        public InputInterpreter()
        {
            left = middle = right = false;
            scroll = scrollDelta = 0;
            pressedKeys = new HashSet<Keys>();
        }

        private InputType InputFromKey(Keys k, bool up)
        {
            return KeyboardMapper.InputFromKey(k, up);
        }

        private void MouseEvents(List<InputType> events)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !left)
            {
                left = true;
                events.Add(InputType.MouseLeft_Down);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released && left)
            {
                left = false;
                events.Add(InputType.MouseLeft_Up);
            }

            if (Mouse.GetState().MiddleButton == ButtonState.Pressed && !middle)
            {
                middle = true;
                events.Add(InputType.MouseMiddle_Down);
            }
            else if (Mouse.GetState().MiddleButton == ButtonState.Released && middle)
            {
                middle = false;
                events.Add(InputType.MouseMiddle_Up);
            }

            if (Mouse.GetState().RightButton == ButtonState.Pressed && !right)
            {
                right = true;
                events.Add(InputType.MouseRight_Down);
            }
            else if (Mouse.GetState().RightButton == ButtonState.Released && right)
            {
                right = false;
                events.Add(InputType.MouseRight_Up);
            }

            scrollDelta = scroll - Mouse.GetState().ScrollWheelValue;
            scroll = Mouse.GetState().ScrollWheelValue;

            if (scrollDelta < 0)
            {
                events.Add(InputType.MouseScroll_Down);
            }
            else if(scrollDelta > 0)
            {
                events.Add(InputType.MouseScroll_Up);
            }
        }

        private void KeyEvents(List<InputType> events)
        {
            Keys[] newKeys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys k in pressedKeys.Union(newKeys))
            {
                if (!pressedKeys.Contains(k))
                {
                    events.Add(InputFromKey(k, false));
                }
                else if (!newKeys.Contains(k))
                {
                    events.Add(InputFromKey(k, true));
                }
            }

            pressedKeys.Clear();
            pressedKeys.UnionWith(newKeys);
        }

        public List<InputType> Process()
        {
            List<InputType> res = new List<InputType>();
            MouseEvents(res);
            KeyEvents(res);

            return res;
        }

        public Point GetLocation()
        {
            return Mouse.GetState().Position;
        }

        public int GetScrollAmount()
        {
            return scrollDelta;
        }
    }
}
