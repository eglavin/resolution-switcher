using System.Runtime.InteropServices;

namespace ResolutionSwitcher.Functions;
public class DeviceContext
{
    /// <summary>
    /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen.
    /// You can use the returned handle in subsequent GDI functions to draw in the DC.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdc
    /// <param name="hWnd">A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC retrieves the DC for the entire screen.</param>
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    /// <summary>
    /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications.
    /// </summary>
    /// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-releasedc
    /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
    /// <param name="hDC">A handle to the DC to be released.</param>
    [DllImport("user32.dll")]
    public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    public static IntPtr GetDeviceContext()
    {
        return GetDC(IntPtr.Zero);
    }

    public static bool ReleaseDeviceContext(IntPtr hDC)
    {
        return ReleaseDC(IntPtr.Zero, hDC);
    }
}
