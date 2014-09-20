using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InputListener
{
    public class Event
    {
        public int Type { get; private set; }
        public Point ScreenLocation { get; private set; }
        public Point WorldLocation { get; private set; }

        public Event(int type_, Point screenLocation_, Point worldLocation_)
        {
            Type = type_;
            ScreenLocation = screenLocation_;
            WorldLocation = worldLocation_;
        }

    }
}
