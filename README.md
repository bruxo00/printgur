# Printgur
Simple tool to take screenshots and upload them directly to Imgur.com. It will stay in your System Tray.

![App](https://i.imgur.com/bVVRl2k.png)

![Context](https://i.imgur.com/OpJzqtr.png)

## Requirements
- .NET Framework 4.6
- .NET Framework 4.7.2

## Automatic Install
1. Download the [Installer](https://github.com/bruxo00/printgur/releases/latest/download/Installer.exe).
2. Execute it.

**Warning:** Your antivirus will probably be triggered by the installer as it needs admin rights and makes changes in the registry in order to add the option in the context menu and to add to startup. It also packs the dependencies inside the .exe, so you can ignore your antivirus.

## Manual Install
1. Download the [Binaries](https://github.com/bruxo00/printgur/releases/latest/download/Installer.exe)
2. Extract them somewhere
3. Run Printgur.exe

## Build It Yourself
1. Create a new application at [IMGUR](https://api.imgur.com/oauth2/addclient)
2. Rename **/Prints/App.config.example** to **/Prints/App.config**
3. Add your credentials to that file in the appSettings section
4. Build the app

## Todo
- [X] Add minimize to startup
- [ ] Optimize RAM usage by the snipping tool
- [ ] Add a screenshot editor
- [ ] Create uninstaller
- [X] Add hotkey to screenshot
