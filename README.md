# Quick Access For Files
Saves information separately to be able to read it quickly during a list of files.

[![Build And Test .NET Core Projects](https://github.com/tomashalac/QuickAccessForFiles/actions/workflows/main.yml/badge.svg)](https://github.com/tomashalac/QuickAccessForFiles/actions/workflows/main.yml)

## Applications

In a game you want to save the player's games and in the menu you want to show statistics of that game and a screenshot, this allows you to save that information in the same file so that you only have to load the minimum necessary information from the file. And the advantage that this information is already in the file is that if the saves are sent between players, this information would still be available. This does not happen if you save this information in a different file.


### Example
I use this system in a Unity3d asset. It's called ["Save is easy"](https://assetstore.unity.com/packages/tools/input-management/save-is-easy-57432#releases).


## Code example

We are going to save a class and we want the title to be accessible when it is listed, we can achieve this by adding an attribute to it.
```c#
[Serializable]
private class ToSave {
    [QuickAccess]
    public string Title;

    public string Document;
}
```

#### To save the object:
```c#
QuickAccess.QuickAccess.Save("MyFileName.bin", toSave);
```

#### To load the QuickAccess information alone:
(you do this with each file when you list them in the menu.)
```c#
ToSave load = QuickAccess.QuickAccess.LoadQuickData<ToSave>("MyFileName.bin");
```

#### To load all the information:
```c#
ToSave load = QuickAccess.QuickAccess.Load<ToSave>("MyFileName.bin");
```