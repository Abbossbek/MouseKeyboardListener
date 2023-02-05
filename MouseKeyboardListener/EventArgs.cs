using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using MouseKeyboardListener.WinApi;

namespace MouseKeyboardListener
{
    [ComVisible(true)]
    public class KeyEventArgs : EventArgs
    {
        private readonly Keys keyData;

        private bool handled;

        private bool suppressKeyPress;

        public virtual bool Alt => (keyData & Keys.Alt) == Keys.Alt;

        public bool Control => (keyData & Keys.Control) == Keys.Control;

        public bool Handled
        {
            get
            {
                return handled;
            }
            set
            {
                handled = value;
            }
        }

        public Keys KeyCode
        {
            get
            {
                Keys keys = keyData & Keys.KeyCode;
                if (!Enum.IsDefined(typeof(Keys), (int)keys))
                {
                    return Keys.None;
                }

                return keys;
            }
        }

        public int KeyValue => (int)(keyData & Keys.KeyCode);

        public Keys KeyData => keyData;

        public Keys Modifiers => keyData & Keys.Modifiers;

        public virtual bool Shift => (keyData & Keys.Shift) == Keys.Shift;

        public bool SuppressKeyPress
        {
            get
            {
                return suppressKeyPress;
            }
            set
            {
                suppressKeyPress = value;
                handled = value;
            }
        }

        public KeyEventArgs(Keys keyData)
        {
            this.keyData = keyData;
        }
    }

    [ComVisible(true)]
    public class KeyPressEventArgs : EventArgs
    {
        private char keyChar;

        private bool handled;

        public char KeyChar
        {
            get
            {
                return keyChar;
            }
            set
            {
                keyChar = value;
            }
        }

        public bool Handled
        {
            get
            {
                return handled;
            }
            set
            {
                handled = value;
            }
        }

        public KeyPressEventArgs(char keyChar)
        {
            this.keyChar = keyChar;
        }
    }

    [ComVisible(true)]
    public class MouseEventArgs : EventArgs
    {
        private readonly MouseButtons button;

        private readonly int clicks;

        private readonly int x;

        private readonly int y;

        private readonly int delta;

        public MouseButtons Button => button;

        public int Clicks => clicks;

        public int X => x;

        public int Y => y;

        public int Delta => delta;

        public Point Location => new Point(x, y);

        public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        {
            this.button = button;
            this.clicks = clicks;
            this.x = x;
            this.y = y;
            this.delta = delta;
        }
    }
}
