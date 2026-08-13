using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts.Components
{
    public struct ElementComponent
    {
        public Element elementType;
    }

    public enum Element
    {
        None,
        Fire,
        Ice,
        Poison
        //Lightning,
        //Light,
        //Dark,
        //Wind,
        //Earth,
        //Sound,
        //Time

    }
}
