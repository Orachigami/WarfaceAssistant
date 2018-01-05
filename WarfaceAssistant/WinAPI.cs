using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WarfaceAssistant
{
    static class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr Hwnd, int iCmdShow);
    }
}
