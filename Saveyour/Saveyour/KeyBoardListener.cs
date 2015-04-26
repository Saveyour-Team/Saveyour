using System;
using System.Windows.Forms; 
using System.Runtime.InteropServices;


public sealed class KeyboardHook : IDisposable
{
	//This code was taken from Christian Liensberger's blog and modified a bit to fit SaveYour
	// Register a global hotkey
	[DllImport("user32.dll")]
	private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    	
	// Unregister a global hotkey
 	[DllImport("user32.dll")]
    	private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


	//A private window class so we can hide our listener
    private class Window : NativeWindow, IDisposable
    {
        private static int WM_HOTKEY = 0x0312;

        public Window()
        {
            // create a handle for the window, since it's not visible we have to call this directly rather than creaing a control
            this.CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // check if we got a hot key pressed.
            if (m.Msg == WM_HOTKEY)
            {
                // get the keys, LPARAM bits 0 to 15 are the modifier keys
		// bits 16 to 31 encode the key value
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);//Recovers bits 16 to 31 and casts to a key
                ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);//Recovers bits 0 to 15 and casts as modifier keys

                // invoke the event to notify the parent.
                if (KeyPressed != null)
                    KeyPressed(this, new KeyPressedEventArgs(modifier, key));
            }
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;


        public void Dispose()
        {
            this.DestroyHandle();
        }

    }

    private Window _window = new Window();
    private int _currentId;

    public KeyboardHook()
    {
        // register keypress events
        _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
        {
            if (KeyPressed != null)
                KeyPressed(this, args);
        };
    }

   /** Registers the given hotkey and modifiers and listens for them**/
    public void RegisterHotKey(ModifierKeys modifier, Keys key)
    {
        // increment the counter.
        _currentId = _currentId + 1;

        // register the hot key.
        if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
            throw new InvalidOperationException("Couldn’t register the hot key.");
    }

   //Event handler for the keypresses 
    public event EventHandler<KeyPressedEventArgs> KeyPressed;


    public void Dispose()
    {
        // unregister all the registered hot keys.        
        for (int i = _currentId; i> 0; i--)
        {
            UnregisterHotKey(_window.Handle, i);
        }

        // dispose the inner window class.
        _window.Dispose();
    }

}

//EventArgs for keypresses, which will be past to whatever method is called from the keypress
public class KeyPressedEventArgs : EventArgs
{
    private ModifierKeys _modifier;
    private Keys _key;

    internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
    {
        _modifier = modifier;
        _key = key;
    }

    public ModifierKeys Modifier
    {
        get { return _modifier; }
    }

    public Keys Key
    {
        get { return _key; }
    }
}

// Possible Modifier Keys
[Flags]
public enum ModifierKeys : uint
{
    Alt = 1,
    Control = 2,
    Shift = 4,
    Win = 8
}





















