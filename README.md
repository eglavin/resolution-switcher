# Resolution Switcher Planning

## What I want to do with resolution switcher

1. Pick a better name for it
2. I want to be able to switch monitor modes from the windows tray bar
3. I want to be able to define modes using a gui
   1. Show the user the current monitors and modes
   2. Create an easy to use gui to define new modes
4. A mode should be a combination of monitors
   1. Main setup - Triple monitors setup enabled by default
   2. Work setup - Single monitor as primary, other monitors disabled
   3. Couch setup - TV as primary, other monitors disabled

### References:

- http://www.pinvoke.net/default.aspx/user32.GetMonitorInfo
- https://www.codeproject.com/Articles/36664/Changing-Display-Settings-Programmatically
- https://github.com/timmui/ScreenResolutionChanger/blob/master/C%23%20Script/Set-ScreenResolutionEx.cs
- https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-changedisplaysettingsexa
