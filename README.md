# Resolution Switcher

## What I want resolution switcher to do

1. I want to be able to switch monitor modes from the windows tray bar
1. I want to be able to define modes using a gui:
   1. Show the user the current monitors and modes
   1. Create an easy to use gui to define new modes
1. A mode could be a combination of enabled monitors like so:
   1. Main setup - Triple monitors setup enabled by default
   1. Work setup - Single monitor as primary, other monitors disabled
   1. Couch setup - TV as primary, other monitors disabled

## A mode should include

- The combination of monitors enabled
- The selected resolution of each monitor
- The primary monitor

## References

- [Pinvoke](http://www.pinvoke.net)
- [Microsoft - ChangeDisplaySettingsExA function (winuser.h)](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-changedisplaysettingsexa)
- [Github - timmui / ScreenResolutionChanger](https://github.com/timmui/ScreenResolutionChanger/tree/master)
- [Github - VFPX / Win32API](https://github.com/VFPX/Win32API/blob/master/libraries/user32)
- [Code Project - Changing Display Settings Programmatically](https://www.codeproject.com/Articles/36664/Changing-Display-Settings-Programmatically)
