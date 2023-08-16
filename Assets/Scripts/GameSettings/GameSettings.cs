using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityJSON;
using System;

public class GameSettings
{
    private const string fileName = "/GameSettings.json";

    private static GameSettings instance;

    public static GameSettings Instance
    {
        get
        {
            if (instance == null)
            {
                if (FileExist())
                {
                    Load();
                }
                else
                {
                    instance = new GameSettings();
                    Save();
                }
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private static string Path
    {
        get
        {
            return Application.persistentDataPath + fileName;
        }
    }

    [JSONNode] private int index;

    public List<SuitsSettings> SuitSettingsList { get; set; } = new List<SuitsSettings>() { new SuitsSettings(1) };

    [JSONNode(NodeOptions.DontSerialize)]
    public SuitsSettings CurrentSuitSettings
    {
        get
        {
            return SuitSettingsList[index];
        }
    }

    public int Count { get; set; } = 8;

    public void SetSuitCount(int count)
    {
        var suitSettings = SuitSettingsList.FirstOrDefault(x => x.SuitsCount == count);

        if (suitSettings == null)
        {
            suitSettings = new SuitsSettings(count);

            SuitSettingsList.Add(suitSettings);
        }

        index = SuitSettingsList.IndexOf(suitSettings);

        Save();
    }

    private static void Save()
    {
        DataPersistance.Save(Instance, Path);
    }

    private static void Load()
    {
        Instance = DataPersistance.Load<GameSettings>(Path);
    }

    private static bool FileExist()
    {
        return System.IO.File.Exists(Path) && 
            !ThrowsException(() => DataPersistance.Load<GameSettings>(Path));
    }

    private static bool ThrowsException(Action action)
    {
        try
        {
            action();
        }
        catch
        {
            return true;
        }

        return false;
    }

    public void SetBest(float bestTime, float bestMoves)
    {
        CurrentSuitSettings.SetBest(bestTime, bestMoves);
        Save();
    }
}