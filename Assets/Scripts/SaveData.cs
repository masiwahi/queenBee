using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public long royalJelly;
    public long honey;

    public float temperature;
    public float temperatureChange;

    public List<string> upgradeKey = new();
    public List<long> upgradeValue = new();
    public List<string> upgradePriceKey = new();
    public List<long> upgradePriceValue = new();

    public string Queen;
    public string Bee;
    public string Map;

    public bool[] mapSkinList = { true, false, false, false, false, false};

    public bool[] queenSkinList = { true, false, false, false, false, false, false, false, false, false, false };

    public bool[] beeSkinList = { true, false, false, false, false, false, false, false, false, false, false };

    public bool[] questClearList = { false, false, false, false, false };
    public int[] questCount = { 0, 0, 0, 0, 0 };

    public string lastPlayTime;
    public int day;

    public string collectStartTime;
    public bool collecting;
    public List<string> itemName = new();

    public int[] itemList = new int[32];

    public int tutorialCount;
}
