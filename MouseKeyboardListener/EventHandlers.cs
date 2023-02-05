using System;
using System.Collections.Generic;
using System.Text;

namespace MouseKeyboardListener
{
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);
    public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
}
