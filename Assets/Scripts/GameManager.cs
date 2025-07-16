using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static long royalJelly = 100000;      // 환생(분봉) 재화
    public static long honey = 100000;           // 기본 재화

    public static float temperature = 1f;   // 온도(온도에 따라 활동기가 있음)
    public float temperatureChange = 0.5f;  // 온도 변화량
    public static float minTemp = -5f;      // 최저 온도
    public static float maxTemp = 15f;      // 최고 온도

    public long queenHealth;                // 여왕(클리커) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public long queenHealthPrice;           // 여왕(클리커) 체력이 강화 비용
    public long queenStorage;               // 여왕(클리커) 꿀주머니(저장공간)이 많으면 꿀을 더 많이 저장해서 운반 가능
    public long queenStoragePrice;          // 여왕(클리커) 꿀주머니(저장공간) 강화 비용

    public long beeHealthPrice;             // 일벌(오토) 체력이 강화 비용
    public long beeStoragePrice;            // 일벌(오토) 꿀주머니(저장공간) 강화 비용
    public long beeSpeedPrice;              // 일벌(오토) 꿀주머니(저장공간) 강화 비용

    public long beeCount;                   // 꿀벌(오토) 가동 수
    public long beeCountPrice;              // 꿀벌(오토) 구매 가격
    public long honeyComb;                  // 꿀벌이 지낼 수 있는 공간(벌집)
    public long honeyCombPrice;             // 꿀벌이 지낼 수 있는 공간(벌집) 구매 가격

    public int honeyCombWidth;              // 가로 벌집 최대 설치 수
    public float honeyCombX;                // 벌집 가로 간격
    public float honeyCombY;                // 벌집 세로 간격

    public long royalQueenHealth;           // 여왕(클리커) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public long royalQueenHealthPrice;      // 여왕(클리커) 체력이 강화 비용
    public long royalQueenStorage;          // 여왕(클리커) 꿀주머니(저장공간)이 많으면 꿀을 더 많이 저장해서 운반 가능
    public long royalQueenStoragePrice;     // 여왕(클리커) 꿀주머니(저장공간) 강화 비용
    public long royalBeeHealthPrice;        // 일벌(오토) 체력이 강화 비용
    public long royalBeeStoragePrice;       // 일벌(오토) 꿀주머니(저장공간) 강화 비용

    public Text HoneyText;                  // 꿀벌 양 체크하는 글씨
    public Text RoyalJellyText;             // 로얄젤리 양 확인하는 글씨
    public Text temperatureText;            // 온도 변화 확인하는 글씨

    public Text queenHealthText;            // 여왕벌 체력 강화확인 글씨
    public Text queenStorageText;           // 여왕벌 꿀주머니 강화확인 글씨
    public Text honeyCombText;              // 벌집 강화확인 글씨
    public Text queenHealthLevel;           // 여왕벌 체력 강화확인 글씨
    public Text queenStorageLevel;          // 여왕벌 꿀주머니 강화확인 글씨
    public Text honeyCombLevel;             // 벌집 강화확인 글씨

    public Text beeHealthText;              // 일벌 체력 강화확인 글씨
    public Text beeStorageText;             // 일벌 꿀주머니 강화확인 글씨
    public Text beeSpeedText;               // 일벌 꿀주머니 강화확인 글씨
    public Text beeCountText;               // 일벌 수 강화확인 글씨
    public Text beeHealthLevel;             // 일벌 체력 강화확인 글씨
    public Text beeStorageLevel;            // 일벌 꿀주머니 강화확인 글씨
    public Text beeSpeedLevel;              // 일벌 꿀주머니 강화확인 글씨
    public Text beeCountLevel;              // 일벌 수 강화확인 글씨

    public Text royalQueenHealthText;       // 여왕벌 체력 강화확인 글씨
    public Text royalQueenStorageText;      // 여왕벌 꿀주머니 강화확인 글씨
    public Text royalBeeHealthText;         // 일벌 체력 강화확인 글씨
    public Text royalBeeStorageText;        // 일벌 꿀주머니 강화확인 글씨
    public Text royalQueenHealthLevel;      // 여왕벌 체력 강화확인 글씨
    public Text royalQueenStorageLevel;     // 여왕벌 꿀주머니 강화확인 글씨
    public Text royalBeeHealthLevel;        // 일벌 체력 강화확인 글씨
    public Text royalBeeStorageLevel;       // 일벌 꿀주머니 강화확인 글씨

    public Button queenHealthBtn;           // 여왕벌 체력 강화 버튼
    public Button queenStorageBtn;          // 여왕벌 꿀주머니 강화 버튼
    public Button honeyCombBtn;             // 벌집 강화 버튼
    public Button swarmingBtn;              // 분봉하기 버튼

    public Button beeHealthBtn;             // 일벌 체력 강화 버튼
    public Button beeStorageBtn;            // 일벌 꿀주머니 강화 버튼
    public Button beeSpeedBtn;              // 일벌 꿀주머니 강화 버튼
    public Button beeCountBtn;              // 일벌 수 강화 버튼

    public Button royalQueenHealthBtn;      // 여왕벌 체력 강화 버튼
    public Button royalQueenStorageBtn;     // 여왕벌 꿀주머니 강화 버튼
    public Button royalBeeHealthBtn;        // 일벌 체력 강화 버튼
    public Button royalBeeStorageBtn;       // 일벌 꿀주머니 강화 버튼

    public GameObject prefabHoney;          // 클릭시 꿀이 늘어나는 모션
    public GameObject prefabHoneyComb;      // 벌집 구매시 증가하는 벌집
    public GameObject prefabHoneyCombBG;    // 벌집 구매시 증가하는 벌집 배경
    public GameObject prefabAd;          // 클릭시 광고시청 보상

    public static string Queen = "queenBee";// 여왕 스킨 이름
    public static string Bee = "beeBasic";  // 일벌 스킨 이름

    public static string Map = "Basic";     // 맵 스킨 이름

    public Image queenSelectSkinImage;      // 여왕 스킨 선택 이미지
    public Image beeSelectSkinImage;        // 일벌 스킨 선택 이미지
    public GameObject backgroundImage;      // 배경 스킨 이미지
    public GameObject hiveImage;            // 벌집 배경 스킨 이미지

    private int upgradeGraph = 17;          // 일반 강화 상승 비율
    private int combGraph = 57;             // 벌집 강화 상승 비율
    private int beeGraph = 37;              // 일벌 수 강화 상승 비율
    private int speedGraph = 137;           // 일벌 속도 강화 상승 비율

    private Vector2 queenSpot;              // 여왕 복귀 지점
    public GameObject tutorial;             // 튜토리얼 표시 화면

    // Start is called before the first frame update
    void Start()
    {
        queenSpot = GameObject.FindGameObjectWithTag("queen").transform.position;
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path))
        {
            Load();
            FillHoneyComb();
            FillBee();
            tutorial.SetActive(false);
        }
        else
        {

        }
        FillHoneyCombBG();
        SettingSkin();
        MapSelect();
        QueenSkinSelect();
        StartCoroutine(TemperatureChangeWork());
        StartCoroutine(advertiseWork());
    }

    // Update is called once per frame
    void Update()
    {
        ShowHoney();

        UpdateQueenHealthText();
        UpdateQueenStorageText();
        UpdateHoneyCombText();

        QueenHealthButtonActiveCheck();
        QueenStorageButtonActiveCheck();
        HoneyCombButtonActiveCheck();
        SwarmingButtonActiveCheck();

        UpdateBeeHealthText();
        UpdateBeeStorageText();
        UpdateBeeSpeedText();
        UpdateBeeCountText();

        BeeHealthButtonActiveCheck();
        BeeStorageButtonActiveCheck();
        BeeSpeedButtonActiveCheck();
        BeeCountButtonActiveCheck();

        UpdateRoyalQueenHealthText();
        UpdateRoyalQueenStorageText();
        UpdateRoyalBeeHealthText();
        UpdateRoyalBeeStorageText();

        RoyalQueenHealthButtonActiveCheck();
        RoyalQueenStorageButtonActiveCheck();
        RoyalBeeHealthButtonActiveCheck();
        RoyalBeeStorageButtonActiveCheck();

        HoneyIncrease();
    }

    // 게임 종료시 진행 내역 저장
    private void OnApplicationQuit()
    {
        Save();
    }

    // 시간마다 온도 변화
    IEnumerator TemperatureChangeWork()
    {
        while (true)
        {
            if (temperature <= minTemp)
            {
                temperatureChange = 0.5f;
            }
            else if (temperature >= maxTemp)
            {
                temperatureChange = -0.5f;
            }
            temperature += temperatureChange;

            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator advertiseWork()
    {
        while (true)
        {
            Instantiate(prefabAd, new Vector2(-3.5f, 3.5f), Quaternion.identity);

            yield return new WaitForSeconds(50f);
        }
    }

    // 화면 클릭 시 꿀 획득 (여왕벌이 꿀을 구해 옴)
    void HoneyIncrease()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if (temperature >= 0 && EventSystem.current.IsPointerOverGameObject() == false)
                {
                    honey += queenHealth + queenStorage + royalQueenHealth + royalQueenStorage;

                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(prefabHoney, mousePosition, Quaternion.identity);
                }
            }
        }
    }

    // 상단 재화 표시 업데이트
    void ShowHoney()
    {
        if (honey == 0)
        {
            HoneyText.text = "0";
        }
        else
        {
            HoneyText.text = DivideNumber(honey);
        }

        if (royalJelly == 0)
        {
            RoyalJellyText.text = "0";
        }
        else
        {
            RoyalJellyText.text = DivideNumber(royalJelly);
        }
        temperatureText.text = temperature + "℃";
    }

    // 여왕벌 체력 관련 표시 업데이트
    void UpdateQueenHealthText()
    {
        queenHealthLevel.text = "Lv." + queenHealth;
        queenHealthText.text = DivideNumber(queenHealthPrice);
    }

    // 여왕벌 체력 강화 진행
    public void UpgradeQueenHealth()
    {
        if (honey >= queenHealthPrice)
        {
            honey -= queenHealthPrice;
            queenHealth += 1;
            queenHealthPrice += queenHealth * upgradeGraph;
        }
    }

    // 여왕벌 체력 버튼 활성화
    void QueenHealthButtonActiveCheck()
    {
        if (honey >= queenHealthPrice)
        {
            queenHealthBtn.interactable = true;
        }
        else
        {
            queenHealthBtn.interactable = false;
        }
    }

    // 여왕벌 꿀주머니 관련 표시 업데이트
    void UpdateQueenStorageText()
    {
        queenStorageLevel.text = "Lv." + queenStorage;
        queenStorageText.text = DivideNumber(queenStoragePrice);
    }

    // 여왕벌 꿀주머니 강화 진행
    public void UpgradeQueenStorage()
    {
        if (honey >= queenStoragePrice)
        {
            honey -= queenStoragePrice;
            queenStorage += 1;
            queenStoragePrice += queenStorage * upgradeGraph;
        }
    }

    // 여왕벌 꿀주머니 버튼 활성화
    void QueenStorageButtonActiveCheck()
    {
        if (honey >= queenStoragePrice)
        {
            queenStorageBtn.interactable = true;
        }
        else
        {
            queenStorageBtn.interactable = false;
        }
    }

    // 벌집 관련 표시 업데이트
    void UpdateHoneyCombText()
    {
        honeyCombLevel.text = "Lv." + honeyComb;
        honeyCombText.text = DivideNumber(honeyCombPrice);
    }

    // 벌집 강화 진행
    public void UpgradeHoneyComb()
    {
        if (honey >= honeyCombPrice)
        {
            honey -= honeyCombPrice;
            honeyComb += 1;
            honeyCombPrice += honeyComb * combGraph;

            CreateHoneyComb();
            if(honeyComb%5 == 0 && honeyComb > 14)
            {
                CreateHoneyCombBG();
            }
        }
    }

    // 벌집 강화 버튼 활성화
    void HoneyCombButtonActiveCheck()
    {
        if (honey >= honeyCombPrice)
        {
            honeyCombBtn.interactable = true;
        }
        else
        {
            honeyCombBtn.interactable = false;
        }
    }

    // 벌집 생성
    void CreateHoneyComb()
    {
        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;
        float spotX = honeyCombSpot.x + ((honeyComb - 1) % honeyCombWidth) * honeyCombX;
        float spotY = honeyCombSpot.y - ((honeyComb - 1) / honeyCombWidth) * honeyCombY - (((honeyComb - 1) % honeyCombWidth) % 2) * (honeyCombY / 2);

        Instantiate(prefabHoneyComb, new Vector2(spotX, spotY), Quaternion.identity);
    }

    // 배경 화면 부족 시 벌집 배경 생성
    void CreateHoneyCombBG()
    {
        Vector2 honeyCombBGSpot = GameObject.Find("honeycombBackground").transform.position;
        float spotX = honeyCombBGSpot.x;
        float spotY = honeyCombBGSpot.y - (1.2f * (honeyComb / 5));

        Instantiate(prefabHoneyCombBG, new Vector2(spotX, spotY), Quaternion.identity);

        if (honeyComb / 5 > 2)
        {
            mainCameraDrag.mainLimitMinY -= 1.2f;
            mainCameraDrag.limitMinY = mainCameraDrag.mainLimitMinY;
        }
    }

    // 일벌 체력 관련 표시 업데이트
    void UpdateBeeHealthText()
    {
        beeHealthLevel.text = "Lv." + beeWork.beeHealth;
        beeHealthText.text = DivideNumber(beeHealthPrice);
    }

    // 일벌 체력 강화 진행
    public void UpgradeBeeHealth()
    {
        if (honey >= beeHealthPrice)
        {
            honey -= beeHealthPrice;
            beeWork.beeHealth += 1;
            beeHealthPrice += beeWork.beeHealth * upgradeGraph;
        }
    }

    // 일벌 체력 버튼 활성화
    void BeeHealthButtonActiveCheck()
    {
        if (honey >= beeHealthPrice)
        {
            beeHealthBtn.interactable = true;
        }
        else
        {
            beeHealthBtn.interactable = false;
        }
    }

    // 일벌 꿀주머니 관련 표시 업데이트
    void UpdateBeeStorageText()
    {
        beeStorageLevel.text = "Lv." + beeWork.beeStorage;
        beeStorageText.text = DivideNumber(beeStoragePrice);
    }

    // 일벌 꿀주머니 강화 진행
    public void UpgradeBeeStorage()
    {
        if (honey >= beeStoragePrice)
        {
            honey -= beeStoragePrice;
            beeWork.beeStorage += 1;
            beeStoragePrice += beeWork.beeStorage * upgradeGraph;
        }
    }

    // 일벌 꿀주머니 버튼 활성화
    void BeeStorageButtonActiveCheck()
    {
        if (honey >= beeStoragePrice)
        {
            beeStorageBtn.interactable = true;
        }
        else
        {
            beeStorageBtn.interactable = false;
        }
    }
    
    // 일벌 속도 관련 표시 업데이트
    void UpdateBeeSpeedText()
    {
        beeSpeedLevel.text = "Lv." + beeWork.beeSpeed;
        beeSpeedText.text = DivideNumber(beeSpeedPrice);
    }

    // 일벌 속도 강화 진행
    public void UpgradeBeeSpeed()
    {
        if (honey >= beeSpeedPrice)
        {
            honey -= beeSpeedPrice;
            beeWork.beeSpeed += 1;
            beeSpeedPrice += beeWork.beeSpeed * speedGraph;
        }
    }

    // 일벌 속도 버튼 활성화
    void BeeSpeedButtonActiveCheck()
    {
        if (honey >= beeSpeedPrice && beeWork.beeSpeed < 50)
        {
            beeSpeedBtn.interactable = true;
        }
        else
        {
            beeSpeedBtn.interactable = false;
        }
    }

    // 일벌 수 관련 표시 업데이트
    void UpdateBeeCountText()
    {
        beeCountLevel.text = "Lv." + beeCount;
        beeCountText.text = DivideNumber(beeCountPrice);
    }

    // 일벌 수 강화 진행
    public void UpgradeBeeCount()
    {
        if (honey >= beeCountPrice && beeCount < honeyComb)
        {
            honey -= beeCountPrice;
            beeCount += 1;
            beeCountPrice += beeCount * beeGraph;

            CreateBeeCount();
        }
    }

    // 일벌 수 증가 버튼 활성화
    void BeeCountButtonActiveCheck()
    {
        if (honey >= beeCountPrice && beeCount < honeyComb)
        {
            beeCountBtn.interactable = true;
        }
        else
        {
            beeCountBtn.interactable = false;
        }
    }

    // 일벌 생성
    void CreateBeeCount()
    {
        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;
        float spotX = honeyCombSpot.x + ((beeCount - 1) % honeyCombWidth) * honeyCombX;
        float spotY = honeyCombSpot.y - ((beeCount - 1) / honeyCombWidth) * honeyCombY - (((beeCount - 1) % honeyCombWidth) % 2) * (honeyCombY / 2);


        GameObject prefabBee = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/" + Bee + ".prefab", typeof(GameObject));
        Instantiate(prefabBee, new Vector2(spotX, spotY), Quaternion.identity);
    }

    // 로얄젤리 여왕벌 체력 관련 표시 업데이트
    void UpdateRoyalQueenHealthText()
    {
        royalQueenHealthLevel.text = "Lv." + royalQueenHealth;
        royalQueenHealthText.text = DivideNumber(royalQueenHealthPrice);
    }

    // 로얄젤리 여왕벌 체력 강화 진행
    public void UpgradeRoyalQueenHealth()
    {
        if (royalJelly >= royalQueenHealthPrice)
        {
            royalJelly -= royalQueenHealthPrice;
            royalQueenHealth += 1;
            royalQueenHealthPrice += royalQueenHealth * upgradeGraph;
        }
    }

    // 로얄젤리 여왕벌 체력 버튼 활성화
    void RoyalQueenHealthButtonActiveCheck()
    {
        if (royalJelly >= royalQueenHealthPrice)
        {
            royalQueenHealthBtn.interactable = true;
        }
        else
        {
            royalQueenHealthBtn.interactable = false;
        }
    }

    // 로얄젤리 여왕벌 꿀주머니 관련 표시 업데이트
    void UpdateRoyalQueenStorageText()
    {
        royalQueenStorageLevel.text = "Lv." + royalQueenStorage;
        royalQueenStorageText.text = DivideNumber(royalQueenStoragePrice);
    }

    // 로얄젤리 여왕벌 꿀주머니 강화 진행
    public void UpgradeRoyalQueenStorage()
    {
        if (royalJelly >= royalQueenStoragePrice)
        {
            royalJelly -= royalQueenStoragePrice;
            royalQueenStorage += 1;
            royalQueenStoragePrice += royalQueenStorage * upgradeGraph;
        }
    }

    // 로얄젤리 여왕벌 꿀주머니 버튼 활성화
    void  RoyalQueenStorageButtonActiveCheck()
    {
        if (royalJelly >= royalQueenStoragePrice)
        {
            royalQueenStorageBtn.interactable = true;
        }
        else
        {
            royalQueenStorageBtn.interactable = false;
        }
    }

    // 로얄젤리 일벌 체력 관련 표시 업데이트
    void UpdateRoyalBeeHealthText()
    {
        royalBeeHealthLevel.text = "Lv." + beeWork.royalBeeHealth;
        royalBeeHealthText.text = DivideNumber(royalBeeHealthPrice);
    }

    // 로얄젤리 일벌 체력 강화 진행
    public void UpgradeRoyalBeeHealth()
    {
        if (royalJelly >= royalBeeHealthPrice)
        {
            royalJelly -= royalBeeHealthPrice;
            beeWork.royalBeeHealth += 1;
            royalBeeHealthPrice += beeWork.royalBeeHealth * upgradeGraph;
        }
    }

    // 로얄젤리 일벌 체력 버튼 활성화
    void RoyalBeeHealthButtonActiveCheck()
    {
        if (royalJelly >= royalBeeHealthPrice)
        {
            royalBeeHealthBtn.interactable = true;
        }
        else
        {
            royalBeeHealthBtn.interactable = false;
        }
    }

    // 로얄젤리 일벌 꿀주머니 관련 표시 업데이트
    void UpdateRoyalBeeStorageText()
    {
        royalBeeStorageLevel.text = "Lv." + beeWork.royalBeeStorage;
        royalBeeStorageText.text = DivideNumber(royalBeeStoragePrice);
    }

    // 로얄젤리 일벌 꿀주머니 강화 진행
    public void UpgradeRoyalBeeStorage()
    {
        if (royalJelly >= royalBeeStoragePrice)
        {
            royalJelly -= royalBeeStoragePrice;
            beeWork.royalBeeStorage += 1;
            royalBeeStoragePrice += beeWork.royalBeeStorage * upgradeGraph;
        }
    }

    // 로얄젤리 일벌 꿀주머니 버튼 활성화
    void RoyalBeeStorageButtonActiveCheck()
    {
        if (royalJelly >= royalBeeStoragePrice)
        {
            royalBeeStorageBtn.interactable = true;
        }
        else
        {
            royalBeeStorageBtn.interactable = false;
        }
    }

    // 분봉하기 버튼 활성화
    void SwarmingButtonActiveCheck()
    {
        if (queenHealth + queenStorage + beeWork.beeHealth + beeWork.beeStorage >= 100)
        {
            swarmingBtn.interactable = true;
        }
        else
        {
            swarmingBtn.interactable = false;
        }
    }

    // 분봉 모든 강화 사항을 초기화 하고 로얄젤리를 습득
    public void Swarming()
    {
        honey = 0;

        if (queenHealth + queenStorage + beeWork.beeHealth + beeWork.beeStorage >= 100)
        {
            royalJelly += (queenHealth + queenStorage + beeWork.beeHealth + beeWork.beeStorage - 89) / 10;
        }

        queenHealth = 0;
        queenHealthPrice = 5;
        queenStorage = 1;
        queenStoragePrice = 5;
        honeyComb = 1;
        honeyCombPrice = 5;

        beeWork.beeHealth = 0;
        beeHealthPrice = 5;
        beeWork.beeStorage = 1;
        beeStoragePrice = 5;
        beeWork.beeSpeed = 0;
        beeSpeedPrice = 5;
        beeCount = 0;
        beeCountPrice = 5;

        GameObject[] bees = GameObject.FindGameObjectsWithTag("bee");
        for(int i = 0; i < bees.Length; i++)
        {
            Destroy(bees[i]);
        }

        GameObject[] honeycombs = GameObject.FindGameObjectsWithTag("honeycomb");
        for (int j = 1; j < honeycombs.Length; j++)
        {
            Destroy(honeycombs[j]);
        }
    }

    // 진행 내역 저장하기
    void Save()
    {
        SaveData saveData = new SaveData();

        saveData.royalJelly = royalJelly;
        saveData.honey = honey;

        saveData.queenHealth = queenHealth;
        saveData.queenHealthPrice = queenHealthPrice;
        saveData.queenStorage = queenStorage;
        saveData.queenStoragePrice = queenStoragePrice;

        saveData.beeHealthPrice = beeHealthPrice;
        saveData.beeStoragePrice = beeStoragePrice;
        saveData.beeSpeedPrice = beeSpeedPrice;

        saveData.beeCount = beeCount;
        saveData.beeCountPrice = beeCountPrice;
        saveData.honeyComb = honeyComb;
        saveData.honeyCombPrice = honeyCombPrice;

        saveData.royalQueenHealth = royalQueenHealth;
        saveData.royalQueenHealthPrice = royalQueenHealthPrice;
        saveData.royalQueenStorage = royalQueenStorage;
        saveData.royalQueenStoragePrice = royalQueenStoragePrice;
        saveData.royalBeeHealthPrice = royalBeeHealthPrice;
        saveData.royalBeeStoragePrice = royalBeeStoragePrice;

        saveData.beeHealth = beeWork.beeHealth;
        saveData.beeStorage = beeWork.beeStorage;
        saveData.beeSpeed = beeWork.beeSpeed;

        saveData.royalBeeHealth = beeWork.royalBeeHealth;
        saveData.royalBeeStorage = beeWork.royalBeeStorage;

        saveData.Queen = Queen;
        saveData.Bee = Bee;

        saveData.Map = Map;

        saveData.queenSkinList = SkinManager.queenSkinList;
        saveData.beeSkinList = SkinManager.beeSkinList;
        saveData.mapSkinList = MapManager.mapSkinList;

        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData, path);
    }

    // 저장 내역 불러오기
    void Load()
    {
        SaveData saveData = new SaveData();

        string path = Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<SaveData>(path);

        royalJelly = saveData.royalJelly;
        honey = saveData.honey;

        queenHealth = saveData.queenHealth;
        queenHealthPrice = saveData.queenHealthPrice;
        queenStorage = saveData.queenStorage;
        queenStoragePrice = saveData.queenStoragePrice;

        beeHealthPrice = saveData.beeHealthPrice;
        beeStoragePrice = saveData.beeStoragePrice;
        beeSpeedPrice = saveData.beeSpeedPrice;

        beeCount = saveData.beeCount;
        beeCountPrice = saveData.beeCountPrice;
        honeyComb = saveData.honeyComb;
        honeyCombPrice = saveData.honeyCombPrice;

        royalQueenHealth = saveData.royalQueenHealth;
        royalQueenHealthPrice = saveData.royalQueenHealthPrice;
        royalQueenStorage = saveData.royalQueenStorage;
        royalQueenStoragePrice = saveData.royalQueenStoragePrice;
        royalBeeHealthPrice = saveData.royalBeeHealthPrice;
        royalBeeStoragePrice = saveData.royalBeeStoragePrice;

        beeWork.beeHealth = saveData.beeHealth;
        beeWork.beeStorage = saveData.beeStorage;
        beeWork.beeSpeed = saveData.beeSpeed;

        beeWork.royalBeeHealth = saveData.royalBeeHealth;
        beeWork.royalBeeStorage = saveData.royalBeeStorage;

        Queen = saveData.Queen;
        Bee = saveData.Bee;

        Map = saveData.Map;

        SkinManager.queenSkinList = saveData.queenSkinList;
        SkinManager.beeSkinList = saveData.beeSkinList;
        MapManager.mapSkinList = saveData.mapSkinList;
    }

    // 저장 내역에 맞게 벌집 생성
    void FillHoneyComb()
    {
        GameObject[] honeycombs = GameObject.FindGameObjectsWithTag("honeycomb");

        if (honeyComb != honeycombs.Length)
        {
            for (int i = honeycombs.Length; i < honeyComb; i++)
            {
                Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;
                float spotX = honeyCombSpot.x + (i % honeyCombWidth) * honeyCombX;
                float spotY = honeyCombSpot.y - (i / honeyCombWidth) * honeyCombY - ((i % honeyCombWidth) % 2) * (honeyCombY / 2);

                Instantiate(prefabHoneyComb, new Vector2(spotX, spotY), Quaternion.identity);
            }
        }
    }

    // 저장내역에 맞게 벌집 배경 생성
    void FillHoneyCombBG()
    {
        GameObject[] honeycombBGs = GameObject.FindGameObjectsWithTag("honeycombBackground");

        if (honeyComb / 5 > honeycombBGs.Length || 3 > honeycombBGs.Length)
        {
            for (int i = honeycombBGs.Length; i <= honeyComb / 5 || i < 3; i++)
            {
                Vector2 honeyCombBGSpot = GameObject.Find("honeycombBackground").transform.position;
                float spotX = honeyCombBGSpot.x;
                float spotY = honeyCombBGSpot.y - (1.2f * i);

                Instantiate(prefabHoneyCombBG, new Vector2(spotX, spotY), Quaternion.identity);
                if(i > 2)
                {
                    mainCameraDrag.mainLimitMinY -= 1.2f;
                    mainCameraDrag.limitMinY = mainCameraDrag.mainLimitMinY;

                }
            }
        }
    }

    // 저장내역에 맞게 일벌 생성
    void FillBee()
    {
        GameObject[] bees = GameObject.FindGameObjectsWithTag("bee");
        for (int i = 0; i < bees.Length; i++)
        {
            Destroy(bees[i]);
        }

        for (int i = 0; i < beeCount; i++)
        {
            StartCoroutine(BeeDelay(i));
        }
    }

    // 스킨 변경시 일벌 스킨 적용 후 재생성
    public void ChangeBee()
    {
        if (Bee != SkinManager.Bee)
        {
            Bee = SkinManager.Bee;
            FillBee();
        }
    }

    // 일벌이 일하러 날아가는 타이밍 조절
    IEnumerator BeeDelay(int i)
    {
        GameObject prefabBeeStop = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/beeStop.prefab", typeof(GameObject));
        prefabBeeStop.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + Bee + "5.png", typeof(Sprite));

        GameObject prefabBee = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/" + Bee + ".prefab", typeof(GameObject));

        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;
        float spotX = honeyCombSpot.x + (i % honeyCombWidth) * honeyCombX;
        float spotY = honeyCombSpot.y - (i / honeyCombWidth) * honeyCombY - ((i % honeyCombWidth) % 2) * (honeyCombY / 2);

        GameObject Beestop = Instantiate(prefabBeeStop, new Vector2(spotX, spotY), Quaternion.identity);


        int rand = Random.Range(0, 11);

        if(temperature < 0)
        {
            rand = 0;
        }

        yield return new WaitForSeconds(rand / 2f);

        Instantiate(prefabBee, new Vector2(spotX, spotY), Quaternion.identity);
        Destroy(Beestop);

        yield break;

    }

    // 여왕 선택 스킨 적용
    public void QueenSkinSelect()
    {
        float spotX = queenSpot.x;
        float spotY = queenSpot.y;

        GameObject prefabQueen = GameObject.FindGameObjectWithTag("queen");
        Destroy(prefabQueen);

        prefabQueen = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/queen/" + Queen + ".prefab", typeof(GameObject));
        queenSelectSkinImage.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/queen/" + Queen + "1.png", typeof(Sprite));
        beeSelectSkinImage.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + Bee + "5.png", typeof(Sprite));

        Instantiate(prefabQueen, new Vector2(spotX, spotY), Quaternion.identity);
    }

    // 맵 선택 스킨 적용
    public void MapSelect()
    {
        backgroundImage.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/background/background" + Map + ".jpg", typeof(Sprite));
        hiveImage.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/hive/hive" + Map + ".png", typeof(Sprite));

        int skinNum = 0;
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        for (int i = 1; i < btnList.Length; i++)
        {
            string check = btnList[i].GetComponent<SpriteRenderer>().name;
            check = check.Substring(3);

            if (check == Map)
            {
                skinNum = i;
            }
        }
        maxTemp = MapManager.maxTempList[skinNum];
        minTemp = MapManager.minTempList[skinNum];
    }

    // 저장 내역에 맞게 스킨 페이지 밎 맵 페이지 구매내역 적용
    void SettingSkin()
    {
        GameObject[] skinList = GameObject.FindGameObjectsWithTag("queenSkin");

        for (int i = 1; i < SkinManager.queenSkinList.Length; i++)
        {
            if (SkinManager.queenSkinList[i])
            {
                Transform buyBtn = skinList[i].transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = skinList[i].transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                string skinName = skinList[i].GetComponent<SpriteRenderer>().sprite.name;
                skinName = skinName.Substring(0, skinName.Length - 4) + "1";

                skinList[i].GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/queen/" + skinName + ".png", typeof(Sprite));
            }
        }

        GameObject[] beeSkinList = GameObject.FindGameObjectsWithTag("beeSkin");

        for (int i = 1; i < SkinManager.beeSkinList.Length; i++)
        {
            if (SkinManager.beeSkinList[i])
            {
                Transform buyBtn = beeSkinList[i].transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = beeSkinList[i].transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                string skinName = beeSkinList[i].GetComponent<SpriteRenderer>().sprite.name;
                skinName = skinName.Substring(0, skinName.Length - 4) + "5";

                beeSkinList[i].GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + skinName + ".png", typeof(Sprite));
            }
        }

        GameObject[] mapSkinList = GameObject.FindGameObjectsWithTag("mapSkin");

        for (int i = 1; i < MapManager.mapSkinList.Length; i++)
        {
            if (MapManager.mapSkinList[i])
            {
                Transform buyBtn = mapSkinList[i].transform.Find("Canvas").Find("buyMap");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = mapSkinList[i].transform.Find("Canvas").Find("selectMap");
                selectBtn.gameObject.SetActive(true);
            }
        }
    }


    // 숫자가 천단위 기준으로 반올림하여 문자로 축약 계산
    string DivideNumber(long num)
    {
        long divineNum = num;
        int divisionNumber = 0;
        while (divineNum / 1000  > 0)
        {
            divineNum = divineNum / 1000;
            divisionNumber++;
        }
        char divisionWord = (char) (divisionNumber + 64);


        if (divisionNumber == 0)
        {
            return divineNum.ToString();
        }
        else 
        {
            if(divisionNumber == 1)
            {
                divisionNumber = 100;
            }
            else
            {
                int add = 1;
                for (int i = 1; i < divisionNumber; i++)
                {
                    add *= 1000;
                }
                divisionNumber = add * 100;
            }
            num = num / divisionNumber;
            num = num - divineNum * 10;

            return divineNum.ToString() + "." + num.ToString() + divisionWord;
        }
    }
}
