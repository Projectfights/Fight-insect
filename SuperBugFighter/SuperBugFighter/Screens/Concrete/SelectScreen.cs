using Bug.Screens.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Screens.Concrete
{
    class SelectScreen : MenuScreen
    {
        public SelectScreen(int width, int height, IScreenMaster master)
            : base(width, height, master)
        {
            
        }
    }
}
