using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InputListener
{
    class ScrollEvent : Event
    {
        public int Scroll { get; private set; }

        public ScrollEvent(int type, Point screenLocation, Point worldLocation, int scroll)
            : base(type, screenLocation, worldLocation)
        {
            Scroll = scroll;
        }
    }
}
