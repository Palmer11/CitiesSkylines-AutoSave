# Cities: Skylines - AutoSave

#### Description
Keep track of your progress with auto saving. Mod will backup your progress in specified iterations. System can be customized via configuration file.

- Configuration file will be loaded when OnLevelLoaded() is called. File will be created if none exists.
- The default number of files that will be save is 50. Maximum is 999.
- The default time delay between each save is 600 second / 10 minutes.
- The default file prefix for each autosave is "autosave_".

#### Notes:
- When OnLevelLoaded() is called, system will verify your old autosaves. If file iteration is misaligned (numaric count off), files will be reorganized and/or renamed.
- When the maximum file count has been reached, files from the start will begin overwriting. Remember to clear your autosaves, or old progress could be lost if file count is low.
- Do not remove auto saves while in simulation. Return to main menu or quit the game before removing old auto saves.

Configuration File Location:
```
C:\Users\%user%\AppData\Local\Colossal Order\Cities_Skylines
```

If you would like to enable achievements. Visit here: http://steamcommunity.com/sharedfiles/filedetails/?id=407055819
