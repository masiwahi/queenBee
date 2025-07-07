[System.Serializable]
public class SaveData
{
    public long royalJelly;             // 환생(분봉) 재화
    public long honey;                  // 기본 재화

    public float temperature;           // 온도

    public long queenHealth;            // 여왕(클리커) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public long queenHealthPrice;       // 여왕(클리커) 체력이 강화 비용
    public long queenStorage;           // 여왕(클리커) 꿀주머니(저장공간)이 많으면 꿀을 더 많이 저장해서 운반 가능
    public long queenStoragePrice;      // 여왕(클리커) 꿀주머니(저장공간) 강화 비용

    public long beeHealthPrice;         // 일벌(오토) 체력이 강화 비용
    public long beeStoragePrice;        // 일벌(오토) 꿀주머니(저장공간) 강화 비용
    public long beeSpeedPrice;          // 일벌(오토) 꿀주머니(저장공간) 강화 비용

    public long beeCount;               // 꿀벌(오토) 가동 수
    public long beeCountPrice;          // 꿀벌(오토) 구매 가격
    public long honeyComb;              // 꿀벌이 지낼 수 있는 공간(벌집)
    public long honeyCombPrice;         // 꿀벌이 지낼 수 있는 공간(벌집) 구매 가격

    public long royalQueenHealth;       // 여왕(클리커) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public long royalQueenHealthPrice;  // 여왕(클리커) 체력이 강화 비용
    public long royalQueenStorage;      // 여왕(클리커) 꿀주머니(저장공간)이 많으면 꿀을 더 많이 저장해서 운반 가능
    public long royalQueenStoragePrice; // 여왕(클리커) 꿀주머니(저장공간) 강화 비용
    public long royalBeeHealthPrice;    // 일벌(오토) 체력이 강화 비용
    public long royalBeeStoragePrice;   // 일벌(오토) 꿀주머니(저장공간) 강화 비용

    public long beeHealth;              // 꿀벌(오토) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public long beeStorage;             // 꿀벌(오토) 저장공간이 많으면 꿀을 더 많이 저장해서 운반 가능
    public long beeSpeed;               // 꿀벌(오토) 속도가 오르면 꿀을 더 빨리 가져옴

    public long royalBeeHealth;         // 꿀벌(오토) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public long royalBeeStorage;        // 꿀벌(오토) 저장공간이 많으면 꿀을 더 많이 저장해서 운반 가능

    public string Queen;
    public string Bee;
    public string Map;

    public bool[] mapSkinList = { true, false, false, false, false, false};

    public bool[] queenSkinList = { true, false, false, false, false, false, false, false, false, false, false };

    public bool[] beeSkinList = { true, false, false, false, false, false, false, false, false, false, false };
}
