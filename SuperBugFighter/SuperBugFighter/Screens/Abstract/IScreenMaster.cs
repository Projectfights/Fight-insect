using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bug.Screens.Abstract
{
    public interface IScreenMaster
    {
        C Load<C>(string name);
        void Change<S>() where S : Screen;
    }
}
