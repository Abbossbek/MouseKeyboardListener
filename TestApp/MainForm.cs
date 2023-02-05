using MouseKeyboardListener;
using MouseKeyboardListener.WinApi;

using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        public static KeyboardHookListener m_KeyboardHookManager;
        public static MouseHookListener m_MouseHookManager;
        public MainForm()
        {
            GimmeTray();
            InitializeComponent();
            m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            m_KeyboardHookManager.Enabled = true;
            m_KeyboardHookManager.KeyDown += M_KeyboardHookManager_KeyDown;
            m_KeyboardHookManager.KeyPress += M_KeyboardHookManager_KeyPress;
            m_KeyboardHookManager.KeyUp += M_KeyboardHookManager_KeyUp;

            m_MouseHookManager = new MouseHookListener(new GlobalHooker());
            m_MouseHookManager.Enabled = true;
            m_MouseHookManager.MouseMove += M_MouseHookManager_MouseMove;
        }

        private void M_MouseHookManager_MouseMove(object sender, MouseKeyboardListener.MouseEventArgs e)
        {
            lblX.Text = e.X.ToString();
            lblY.Text = e.Y.ToString();
        }

        private void M_KeyboardHookManager_KeyUp(object sender, MouseKeyboardListener.KeyEventArgs e)
        {
            listBox1.Items.Add($"KeyUp: {e.KeyData}");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }

        private void M_KeyboardHookManager_KeyPress(object sender, MouseKeyboardListener.KeyPressEventArgs e)
        {
            listBox1.Items.Add($"KeyPress: {e.KeyChar}");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }

        private void M_KeyboardHookManager_KeyDown(object sender, MouseKeyboardListener.KeyEventArgs e)
        {
            listBox1.Items.Add($"KeyDown: {e.KeyData}");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }

        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        private void GimmeTray()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Exit", null, OnExit);

            // Create a tray icon.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "Windows Key Blocker";
            trayIcon.Icon = new Icon(SystemIcons.Shield, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
        }
        private void OnExit(object sender, EventArgs e)
        {
            Console.WriteLine(" ! OSK exited.");
            trayIcon.Dispose();
            m_KeyboardHookManager.Dispose();
            Application.Exit();
        }
    }
}