using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InputListener
{
    public interface EventListener
    {
        void OnEvent<T>(T e) where T : Event;
    }
}
