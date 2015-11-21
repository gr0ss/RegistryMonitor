# RegistryMonitor

The purpose of this project is to continually monitor a single registry key and update a systray icon to reflect what the value of the registry key currently is.  This project is being made to be vague and open to people using it however they see fit.  For my personal use, I use it to tell me what database I am currently connected to, and for the ability to change databases with 2 hotkeys instead of manually editing the registry every time I want to switch databases.

Features:
 - Ability to setup/alter anything you want about the icon that is created.
 - Ability to add as many possibilities of registry key values as you want.
 - Ability to setup/alter the registry key value with hotkeys or right clicking systray icon.
 - Ability to setup/alter hotkeys for those registry key values and global hotkey to open menu on systray.
 - Ability to add links to commonly used executables such as scripts you write, with hotkeys for easy access.

## Installation

1. Clone To Desktop
2. Go to Libraries Folder and Clone whatever repos are in there to the libraries folder
3. Build
4. Run build

As of now, if things have changed, it might be a good idea to delete your bin folder or at least the *.json files in it after syncing because when I add features it can sometimes crash based on outdated json files.  Make sure you backup your json files though so you can copy the setup you have made.

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## Credits

Everybody that contributes :D

## License

live free or die...