using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.GameObjects
{
    class FighterInput
    {
        private PlayerIndex player;

        public FighterInput(PlayerIndex player_)
        {
            player = player_;
        }

        public double left()
        {
            return 0d;
        }

        public double right()
        {
            return 0d;
        }

        public double up()
        {
            return 0d;
        }
    }
}
