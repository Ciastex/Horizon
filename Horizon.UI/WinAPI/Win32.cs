using System;
using System.Runtime.InteropServices;

namespace Horizon.UI.WinAPI
{
    public class Win32
    {
        [Flags]
        public enum GwlExStyle
        {
            WS_EX_TOOLWINDOW = 0x80
        }

        public enum GwlIndex
        {
            GWL_EXSTYLE = -20
        }

        public enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        public enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, GwlIndex gwlindex);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hwnd, GwlIndex gwlIndex, int newLong);
    }
}
