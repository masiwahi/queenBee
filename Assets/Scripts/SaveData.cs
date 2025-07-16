[System.Serializable]
public class SaveData
{
    public long royalJelly;
    public long honey;

    public float temperature;

    public long queenHealth;
    public long queenHealthPrice;
    public long queenStorage;
    public long queenStoragePrice;

    public long beeHealthPrice;
    public long beeStoragePrice;
    public long beeSpeedPrice;

    public long beeCount;
    public long beeCountPrice;
    public long honeyComb;
    public long honeyCombPrice;

    public long royalQueenHealth;
    public long royalQueenHealthPrice;
    public long royalQueenStorage;
    public long royalQueenStoragePrice;
    public long royalBeeHealthPrice;
    public long royalBeeStoragePrice;

    public long beeHealth;
    public long beeStorage;
    public long beeSpeed;

    public long royalBeeHealth;
    public long royalBeeStorage;

    public string Queen;
    public string Bee;
    public string Map;

    public bool[] mapSkinList = { true, false, false, false, false, false};

    public bool[] queenSkinList = { true, false, false, false, false, false, false, false, false, false, false };

    public bool[] beeSkinList = { true, false, false, false, false, false, false, false, false, false, false };
}
