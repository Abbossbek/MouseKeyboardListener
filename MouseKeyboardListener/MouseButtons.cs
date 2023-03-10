using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MouseKeyboardListener
{
    [ComVisible(true)]
    [Flags]
    public enum MouseButtons
    {
        Left = 0x100000,
        None = 0x0,
        Right = 0x200000,
        Middle = 0x400000,
        XButton1 = 0x800000,
        XButton2 = 0x1000000
    }
}
