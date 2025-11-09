using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static long royalJelly = 0;      // 환생(분봉) 재화
    public static long honey = 0;           // 기본 재화

    public static float temperature = 10f;   // 온도(온도에 따라 활동기가 있음)
    public float temperatureChange = -0.5f;  // 온도 변화량
    public static float minTemp = -5f;      // 최저 온도
    public static float maxTemp = 15f;      // 최고 온도

    public int honeyCombWidth;              // 가로 벌집 최대 설치 수
    public float honeyCombX;                // 벌집 가로 간격
    public float honeyCombY;                // 벌집 세로 간격

    public string lastPlayTime;
    public int day;

    public Dictionary<string, long> upgrade = new Dictionary<string, long>();
    public Dictionary<string, long> upgradePrice = new Dictionary<string, long>();
    public Dictionary<string, Text> upgradeText = new Dictionary<string, Text>();
    public Dictionary<string, Text> upgradeLevel = new Dictionary<string, Text>();
    public Dictionary<string, Button> upgradeBtn = new Dictionary<string, Button>();

    public Text HoneyText;                  // 꿀벌 양 체크하는 글씨
    public Text RoyalJellyText;             // 로얄젤리 양 확인하는 글씨
    public Text temperatureText;            // 온도 변화 확인하는 글씨

    public Button swarmingBtn;              // 분봉하기 버튼

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

    private int upgradeGraph = 13;          // 일반 강화 상승 비율
    private int combGraph = 57;             // 벌집 강화 상승 비율
    private int beeGraph = 37;              // 일벌 수 강화 상승 비율
    private int speedGraph = 137;           // 일벌 속도 강화 상승 비율
    private int maxHoneyComb;

    private Vector2 queenSpot;              // 여왕 복귀 지점
    public GameObject tutorial;             // 튜토리얼 표시 화면

    public GameObject rewardPanel;
    public Text maxTimeText;
    public Text alertText;
    public Text rewardText;
    public GameObject exitGamePanel;
    public GameObject questPanel;

    public GameObject canvasSkin;
    public GameObject canvasMap;

    public Button skinExitBtn;
    public Button mapExitBtn;

    public static bool[] questClearList = { false, false, false, false, false };
    private int[] questCondition = { 100, 5, 1, 2, 4 };
    private int[] questReward = { 10, 10, 10, 20, 50 };
    public static int[] questCount = { 0, 0, 0, 0, 0 };


    GameObject[] panelList;
    private List<GameObject> questList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SettingObject();
        HideObject();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        queenSpot = GameObject.FindGameObjectWithTag("queen").transform.position;
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path))
        {
            Load();
            FillHoneyComb();
            FillBee();
            AwayReward();
        }
        FillHoneyCombBG();
        SettingSkin();
        MapSelect();
        QueenSkinSelect();

        if(TutorialManager.Count > 56)
        {
            tutorial.SetActive(false);
        }

        for (int i = 0; i < questClearList.Length; i++)
        {
            if (questClearList[i])
            {
                ClearQuest(i);
            }
        }

        foreach (KeyValuePair<string, Text> pair in upgradeText)
        {
            UpdateUpgradeText(pair.Key);
        }

        StartCoroutine(TemperatureChangeWork());
        StartCoroutine(AdvertiseWork());
    }

    // Update is called once per frame
    void Update()
    {
        ShowHoney();

        SwarmingButtonActiveCheck();

        foreach (KeyValuePair<string, Button> pair in upgradeBtn)
        {
            UpgradeyActiveCheck(pair.Key);
        }

        HoneyIncrease();

        if (Input.GetKey("escape"))
        {
            
            if (canvasMap.activeSelf)
            {
                mapExitBtn.onClick.Invoke();
            }
            else if (canvasSkin.activeSelf)
            {
                skinExitBtn.onClick.Invoke();
            }
            else
            {
                bool activeCount = true;
                for(int i = 0; i < panelList.Length; i++)
                {
                    if (panelList[i].activeSelf)
                    {
                        panelList[i].SetActive(false);
                        activeCount = false;
                    }
                }
                if (activeCount)
                {
                    exitGamePanel.SetActive(true);
                }
            }
        }

        UpdateQuestText();
        ResetQuest();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    // 게임 종료시 진행 내역 저장
    private void OnApplicationQuit()
    {
        Save();
    }

    void SettingObject()
    {
        // 강화 레벨

        upgrade.Add("queenHealth", 0);
        upgrade.Add("queenStorage", 1);
        upgrade.Add("honeycomb", 1);

        upgrade.Add("beeHealth", 0);
        upgrade.Add("beeStorage", 1);
        upgrade.Add("beeSpeed", 0);
        upgrade.Add("beeCount", 0);

        upgrade.Add("royalQueenHealth", 0);
        upgrade.Add("royalQueenStorage", 0);
        upgrade.Add("royalBeeHealth", 0);
        upgrade.Add("royalBeeStorage", 0);

        // 강화 비용

        upgradePrice.Add("queenHealth", 5);
        upgradePrice.Add("queenStorage", 5);
        upgradePrice.Add("honeycomb", 5);

        upgradePrice.Add("beeHealth", 5);
        upgradePrice.Add("beeStorage", 5);
        upgradePrice.Add("beeSpeed", 5);
        upgradePrice.Add("beeCount", 5);

        upgradePrice.Add("royalQueenHealth", 5);
        upgradePrice.Add("royalQueenStorage", 5);
        upgradePrice.Add("royalBeeHealth", 5);
        upgradePrice.Add("royalBeeStorage", 5);

        // 강화 비용 표시

        upgradeText.Add("queenHealth", GameObject.Find("queenHealthButtonText").GetComponent<Text>());
        upgradeText.Add("queenStorage", GameObject.Find("queenStorageButtonText").GetComponent<Text>());
        upgradeText.Add("honeycomb", GameObject.Find("honeyCombButtonText").GetComponent<Text>());

        upgradeText.Add("beeHealth", GameObject.Find("beeHealthButtonText").GetComponent<Text>());
        upgradeText.Add("beeStorage", GameObject.Find("beeStorageButtonText").GetComponent<Text>());
        upgradeText.Add("beeSpeed", GameObject.Find("beeSpeedButtonText").GetComponent<Text>());
        upgradeText.Add("beeCount", GameObject.Find("beeCountButtonText").GetComponent<Text>());

        upgradeText.Add("royalQueenHealth", GameObject.Find("royalQueenHealthButtonText").GetComponent<Text>());
        upgradeText.Add("royalQueenStorage", GameObject.Find("royalQueenStorageButtonText").GetComponent<Text>());
        upgradeText.Add("royalBeeHealth", GameObject.Find("royalBeeHealthButtonText").GetComponent<Text>());
        upgradeText.Add("royalBeeStorage", GameObject.Find("royalBeeStorageButtonText").GetComponent<Text>());

        // 강화 레벨 표시

        upgradeLevel.Add("queenHealth", GameObject.Find("queenHealthLevel").GetComponent<Text>());
        upgradeLevel.Add("queenStorage", GameObject.Find("queenStorageLevel").GetComponent<Text>());
        upgradeLevel.Add("honeycomb", GameObject.Find("honeyCombLevel").GetComponent<Text>());

        upgradeLevel.Add("beeHealth", GameObject.Find("beeHealthLevel").GetComponent<Text>());
        upgradeLevel.Add("beeStorage", GameObject.Find("beeStorageLevel").GetComponent<Text>());
        upgradeLevel.Add("beeSpeed", GameObject.Find("beeSpeedLevel").GetComponent<Text>());
        upgradeLevel.Add("beeCount", GameObject.Find("beeCountLevel").GetComponent<Text>());

        upgradeLevel.Add("royalQueenHealth", GameObject.Find("royalQueenHealthLevel").GetComponent<Text>());
        upgradeLevel.Add("royalQueenStorage", GameObject.Find("royalQueenStorageLevel").GetComponent<Text>());
        upgradeLevel.Add("royalBeeHealth", GameObject.Find("royalBeeHealthLevel").GetComponent<Text>());
        upgradeLevel.Add("royalBeeStorage", GameObject.Find("royalBeeStorageLevel").GetComponent<Text>());

        // 강화 버튼

        upgradeBtn.Add("queenHealth", GameObject.Find("queenHealthButton").GetComponent<Button>());
        upgradeBtn.Add("queenStorage", GameObject.Find("queenStorageButton").GetComponent<Button>());
        upgradeBtn.Add("honeycomb", GameObject.Find("honeyCombButton").GetComponent<Button>());

        upgradeBtn.Add("beeHealth", GameObject.Find("beeHealthButton").GetComponent<Button>());
        upgradeBtn.Add("beeStorage", GameObject.Find("beeStorageButton").GetComponent<Button>());
        upgradeBtn.Add("beeSpeed", GameObject.Find("beeSpeedButton").GetComponent<Button>());
        upgradeBtn.Add("beeCount", GameObject.Find("beeCountButton").GetComponent<Button>());

        upgradeBtn.Add("royalQueenHealth", GameObject.Find("royalQueenHealthButton").GetComponent<Button>());
        upgradeBtn.Add("royalQueenStorage", GameObject.Find("royalQueenStorageButton").GetComponent<Button>());
        upgradeBtn.Add("royalBeeHealth", GameObject.Find("royalBeeHealthButton").GetComponent<Button>());
        upgradeBtn.Add("royalBeeStorage", GameObject.Find("royalBeeStorageButton").GetComponent<Button>());

        // 업그레이드 메뉴

        panelList = GameObject.FindGameObjectsWithTag("upgradePanel");

        // 퀘스트 메뉴

        GameObject[] quest = GameObject.FindGameObjectsWithTag("quest");
        questList = quest.OrderByDescending(go => go.transform.position.y).ToList();
    }

    void HideObject()
    {
        GameObject.Find("QueenUpgradePanel").SetActive(false);
        GameObject.Find("BeeUpgradePanel").SetActive(false);
        GameObject.Find("RoyalUpgradePanel").SetActive(false);
        questPanel.SetActive(false);

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
            Save();
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator AdvertiseWork()
    {
        yield return new WaitForSeconds(30f);
        while (true)
        {
            Instantiate(prefabAd, new Vector2(-3.5f, 3.5f), Quaternion.identity);

            yield return new WaitForSeconds(180f);
        }
    }

    // 화면 클릭 시 꿀 획득 (여왕벌이 꿀을 구해 옴)
    void HoneyIncrease()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null)
                {
                    if (temperature >= 0 && EventSystem.current.IsPointerOverGameObject() == false)
                    {
                        long gainHoney = upgrade["queenHealth"] + upgrade["queenStorage"] + upgrade["royalQueenHealth"] + upgrade["royalQueenStorage"];
                        if (ItemManager.itemList[13] > 0)
                        {
                            gainHoney += upgrade["queenHealth"] * (ItemManager.itemList[13] + 1) / 1000;
                        }
                        if (ItemManager.itemList[12] > 0)
                        {
                            gainHoney += upgrade["queenStorage"] * (ItemManager.itemList[12] + 1) / 1000;
                        }
                        if (ItemManager.itemList[0] > 0)
                        {
                            gainHoney += gainHoney * (ItemManager.itemList[0] + 1) / 1000;
                        }
                        if (ItemManager.itemList[18] > 0)
                        {
                            gainHoney += gainHoney * (ItemManager.itemList[18] + 1) / 1000;
                        }

                        honey += gainHoney;

                        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Instantiate(prefabHoney, mousePosition, Quaternion.identity);
                        questCount[0]++;
                    }
                }
            }
        }
    }
    void AwayReward()
    {
        string[] lastTimeString = lastPlayTime.Split("/");
        int[] lastTimeInt = new int[lastTimeString.Length];
        for (int i = 0; i < lastTimeString.Length; i++)
        {
            lastTimeInt[i] = int.Parse(lastTimeString[i]);
        }

        if (DateTime.Now.Year > lastTimeInt[0])
        {
            lastTimeInt[1] -= 12;
        }
        if (DateTime.Now.Month > lastTimeInt[1])
        {
            lastTimeInt[2] -= 30;
        }
        if (DateTime.Now.Day > lastTimeInt[2])
        {
            lastTimeInt[3] -= 24;
        }
        if (DateTime.Now.Hour > lastTimeInt[3])
        {
            lastTimeInt[4] -= 60;
        }

        int emptyMinute = DateTime.Now.Minute - lastTimeInt[4];
        int skinCount = 0;
        for (int i = 1; i < SkinManager.queenSkinList.Length; i++)
        {
            if (SkinManager.queenSkinList[i])
            {
                skinCount++;
            }
        }
        for (int i = 1; i < SkinManager.beeSkinList.Length; i++)
        {
            if (SkinManager.beeSkinList[i])
            {
                skinCount++;
            }
        }
        int maxMin = 180 + (skinCount * 30);
        int item = 0;
        if (ItemManager.itemList[2] > 0)
        {
            item += ItemManager.itemList[2] + 1;
        }
        if (ItemManager.itemList[16] > 0)
        {
            item += ItemManager.itemList[16] + 1;
        }
        if (ItemManager.itemList[26] > 0)
        {
            item += ItemManager.itemList[26] + 1;
        }
        maxMin = maxMin + maxMin * item / 1000;

        if (emptyMinute > 5 && upgrade["beeCount"] > 0)
        {
            rewardPanel.SetActive(true);
            if (emptyMinute > maxMin)
            {
                emptyMinute = maxMin;
            }

            int additional = 0;

            if (ItemManager.itemList[3] > 0)
            {
                additional += ItemManager.itemList[3];
            }
            if (ItemManager.itemList[21] > 0)
            {
                additional += ItemManager.itemList[21];
            }
            if (ItemManager.itemList[31] > 0)
            {
                additional += ItemManager.itemList[31];
            }
            additional = (int)(additional * (emptyMinute * (upgrade["beeHealth"] + upgrade["beeStorage"]) * upgrade["beeCount"]) / 1000);

            maxTimeText.text = "최대 보상 시간 : " + maxMin / 60 + "시간 " + maxMin % 60 + "분";
            alertText.text = "오프라인 " + emptyMinute / 60 + "시간 " + emptyMinute % 60 + "분 동안\n일벌이 벌어온 보상입니다!";
            rewardText.text = DivideNumber(emptyMinute * (upgrade["beeHealth"] + upgrade["beeStorage"]) * upgrade["beeCount"] + additional);
        }
    }

    public void takeReward()
    {
        string[] lastTimeString = lastPlayTime.Split("/");
        int[] lastTimeInt = new int[lastTimeString.Length];
        for (int i = 0; i < lastTimeString.Length; i++)
        {
            lastTimeInt[i] = int.Parse(lastTimeString[i]);
        }
        if (DateTime.Now.Year > lastTimeInt[0])
        {
            lastTimeInt[1] -= 12;
        }
        if (DateTime.Now.Month > lastTimeInt[1])
        {
            lastTimeInt[2] -= 30;
        }
        if (DateTime.Now.Day > lastTimeInt[2])
        {
            lastTimeInt[3] -= 24;
        }
        if (DateTime.Now.Hour > lastTimeInt[3])
        {
            lastTimeInt[4] -= 60;
        }

        int emptyMinute = DateTime.Now.Minute - lastTimeInt[4];
        int skinCount = 0;
        for (int i = 1; i < SkinManager.queenSkinList.Length; i++)
        {
            if (SkinManager.queenSkinList[i])
            {
                skinCount++;
            }
        }
        for (int i = 1; i < SkinManager.beeSkinList.Length; i++)
        {
            if (SkinManager.beeSkinList[i])
            {
                skinCount++;
            }
        }

        if (emptyMinute > 5)
        {
            int maxMin = 180 + (skinCount * 30);
            int item = 0;
            if (ItemManager.itemList[2] > 0)
            {
                item += ItemManager.itemList[2] + 1;
            }
            if (ItemManager.itemList[16] > 0)
            {
                item += ItemManager.itemList[16] + 1;
            }
            if (ItemManager.itemList[26] > 0)
            {
                item += ItemManager.itemList[26] + 1;
            }
            maxMin = maxMin + maxMin * item / 1000;

            if (emptyMinute > maxMin)
            {
                emptyMinute = maxMin;
            }

            int additional = 0;

            if (ItemManager.itemList[3] > 0)
            {
                additional += ItemManager.itemList[3];
            }
            if (ItemManager.itemList[21] > 0)
            {
                additional += ItemManager.itemList[21];
            }
            if (ItemManager.itemList[31] > 0)
            {
                additional += ItemManager.itemList[31];
            }
            additional = (int) (additional * (emptyMinute * (upgrade["beeHealth"] + upgrade["beeStorage"]) * upgrade["beeCount"]) / 1000);

            honey += (emptyMinute * (upgrade["beeHealth"] + upgrade["beeStorage"]) * upgrade["beeCount"]) + additional ;
        }
        rewardPanel.SetActive(false);

    }
    void ResetQuest()
    {
        if (DateTime.Now.Day != day)
        {
            for (int i = 0; i < questCount.Length; i++)
            {
                questCount[i] = 0;
                questClearList[i] = false;
            }
            day = DateTime.Now.Day;
        }
    }

    void UpdateQuestText()
    {
        questCount[4] = 0;
        for (int i = 0;i< questClearList.Length; i++)
        {
            if (questClearList[i])
            {
                questCount[4]++;
            }
        }

        for(int j = 0; j < questList.Count; j++)
        {
            if (!questClearList[j])
            {
                questList[j].transform.Find("progressText").GetComponent<Text>().text = questCount[j] + " / " + questCondition[j];
            }

            if (questCount[j] < questCondition[j] || questClearList[j])
            {
                questList[j].transform.Find("clearButton").GetComponent<Button>().interactable = false;
            }
            else
            {
                questList[j].transform.Find("clearButton").GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ClearQuest(int num)
    {
        if (questCount[num] >= questCondition[num])
        {
            questClearList[num] = true;
            royalJelly += questReward[num];
            questList[num].transform.Find("progressText").GetComponent<Text>().text = "Clear";
            questList[num].transform.Find("clearButton").GetComponent<Button>().interactable = false;
            questList[num].transform.Find("clearButton").Find("Image").gameObject.SetActive(false);
            questList[num].transform.Find("clearButton").Find("Text").gameObject.SetActive(false);
        }
    }

    // 상단 재화 표시 업데이트
    void ShowHoney()
    {
        HoneyText.text = DivideNumber(honey);
        RoyalJellyText.text = DivideNumber(royalJelly);
        temperatureText.text = temperature + "℃";
    }

    // 여왕벌 체력 관련 표시 업데이트

    void UpdateUpgradeText(string upgradeName)
    {
        long discount = 0;
        switch (upgradeName)
        {
            case "queenHealth":
                if (ItemManager.itemList[17] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[17] + 1) / 1000;
                }
                break;
            case "queenStorage":
                if (ItemManager.itemList[19] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[19] + 1) / 1000;
                }
                break;
            case "beeHealth":
                if (ItemManager.itemList[27] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[27] + 1) / 1000;
                }
                break;
            case "beeStorage":
                if (ItemManager.itemList[29] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[29] + 1) / 1000;
                }
                break;
            case "royalQueenHealth":
            case "royalQueenStorage":
            case "royalBeeHealth":
            case "royalBeeStorage":
                if (ItemManager.itemList[8] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[8] + 1) / 1000;
                }
                break;
            default: break;
        }

        upgradeText[upgradeName].text = DivideNumber(upgradePrice[upgradeName] - discount);
        upgradeLevel[upgradeName].text = "Lv." + DivideNumber(upgrade[upgradeName]);
        if (upgradeName == "honeycomb" && upgrade[upgradeName] >= maxHoneyComb)
        {
            upgradeText[upgradeName].text = "MAX";
        }
        else if (upgradeName == "beeCount" && upgrade[upgradeName] >= upgrade["honeycomb"])
        {
            upgradeText[upgradeName].text = "MAX";
        }
        else if (upgradeName == "beeSpeed" && upgrade[upgradeName] >= 50)
        {
            upgradeText[upgradeName].text = "MAX";
        }
    }

    public void BuyUpgradeHoney(string upgradeName)
    {
        long discount = 0;
        switch (upgradeName)
        {
            case "queenHealth":
                if (ItemManager.itemList[17] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[17] + 1) / 1000;
                }
                break;
            case "queenStorage":
                if (ItemManager.itemList[19] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[19] + 1) / 1000;
                }
                break;
            case "beeHealth":
                if (ItemManager.itemList[27] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[27] + 1) / 1000;
                }
                break;
            case "beeStorage":
                if (ItemManager.itemList[29] != 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[29] + 1) / 1000;
                }
                break;
            default: break;
        }

        if (honey >= upgradePrice[upgradeName] - discount)
        {
            honey -= upgradePrice[upgradeName] - discount;
            upgrade[upgradeName] += 1;
            if (upgradeName == "honeycomb")
            {
                int honeyComb = (int)upgrade["honeycomb"];
                if (ItemManager.itemList[5] > 0)
                {
                    if (ItemManager.itemList[5] < 9)
                    {
                        honeyComb += 1;
                    }
                    else
                    {
                        honeyComb += ItemManager.itemList[5] / 10;
                    }
                }

                upgradePrice[upgradeName] += upgrade[upgradeName] * combGraph;
                CreateHoneyComb();
                if (honeyComb % 5 == 0 && honeyComb > 14)
                {
                    CreateHoneyCombBG();
                }
            }
            else if (upgradeName == "beeCount")
            {
                upgradePrice[upgradeName] += upgrade[upgradeName] * beeGraph;
                CreateBeeCount();
            }
            else if (upgradeName == "beeSpeed")
            {
                upgradePrice[upgradeName] += upgrade[upgradeName] * speedGraph;
            }
            else
            {
                upgradePrice[upgradeName] += upgrade[upgradeName] * upgradeGraph;
            }
            UpdateUpgradeText(upgradeName);
            questCount[1]++;
        }
    }

    public void BuyUpgradeJelly(string upgradeName)
    {
        long discount = 0;
        if (ItemManager.itemList[8] != 0)
        {
            discount += upgradePrice[upgradeName] * (ItemManager.itemList[8] + 1) / 1000;
        }
        if (royalJelly >= upgradePrice[upgradeName] - discount)
        {
            royalJelly -= upgradePrice[upgradeName];
            upgrade[upgradeName] += 1;
            upgradePrice[upgradeName] += upgrade[upgradeName] * upgradeGraph;
            UpdateUpgradeText(upgradeName);
            questCount[1]++;
        }
    }

    void UpgradeyActiveCheck(string upgradeName)
    {
        long discount = 0;
        switch (upgradeName)
        {
            case "queenHealth":
                if (ItemManager.itemList[17] > 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[17] + 1) / 1000;
                }
                break;
            case "queenStorage":
                if (ItemManager.itemList[19] > 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[19] + 1) / 1000;
                }
                break;
            case "beeHealth":
                if (ItemManager.itemList[27] > 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[27] + 1) / 1000;
                }
                break;
            case "beeStorage":
                if (ItemManager.itemList[29] > 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[29] + 1) / 1000;
                }
                break;
            case "royalQueenHealth":
            case "royalQueenStorage":
            case "royalBeeHealth":
            case "royalBeeStorage":
                if (ItemManager.itemList[8] > 0)
                {
                    discount += upgradePrice[upgradeName] * (ItemManager.itemList[8] + 1) / 1000;
                }
                break;
            default: break;
        }

        int beeCount = (int)upgrade["beeCount"];
        if (ItemManager.itemList[25] > 0)
        {
            if (ItemManager.itemList[25] < 9)
            {
                beeCount += 1;
            }
            else
            {
                beeCount += ItemManager.itemList[25] / 10;
            }
        }

        int honeyComb = (int)upgrade["honeycomb"];
        if (ItemManager.itemList[5] > 0)
        {
            if (ItemManager.itemList[5] < 9)
            {
                honeyComb += 1;
            }
            else
            {
                honeyComb += ItemManager.itemList[5] / 10;
            }
        }

        if (upgradeName == "honeycomb" && honeyComb >= maxHoneyComb)
        {
            upgradeBtn[upgradeName].interactable = false;
        }
        else if (upgradeName == "beeCount" && beeCount >= honeyComb)
        {
            upgradeBtn[upgradeName].interactable = false;
        }
        else if (upgradeName == "beeSpeed" && upgrade[upgradeName] >= 50)
        {
            upgradeBtn[upgradeName].interactable = false;
        }
        else if (upgradeName[..5] == "royal")
        {
            if (royalJelly < upgradePrice[upgradeName] - discount)
            {
                upgradeBtn[upgradeName].interactable = false;
            }
            else
            {
                upgradeBtn[upgradeName].interactable = true;
            }
        }
        else if (honey < upgradePrice[upgradeName] - discount)
        {
            upgradeBtn[upgradeName].interactable = false;
        }
        else
        {
            upgradeBtn[upgradeName].interactable = true;
        }
    }

    // 벌집 생성
    void CreateHoneyComb()
    {
        int honeyComb = (int)upgrade["honeycomb"];
        if (ItemManager.itemList[5] > 0)
        {
            if (ItemManager.itemList[5] < 9)
            {
                honeyComb += 1;
            }
            else
            {
                honeyComb += ItemManager.itemList[5] / 10;
            }
        }

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
        float spotY = honeyCombBGSpot.y - (1.2f * (upgrade["honeycomb"] / 5));

        Instantiate(prefabHoneyCombBG, new Vector2(spotX, spotY), Quaternion.identity);

        if (upgrade["honeycomb"] / 5 > 2)
        {
            mainCameraDrag.mainLimitMinY -= 1.2f;
            mainCameraDrag.limitMinY = mainCameraDrag.mainLimitMinY;
        }
    }

    // 일벌 생성
    void CreateBeeCount()
    {
        int beeCount = (int)upgrade["beeCount"];
        if (ItemManager.itemList[25] > 0)
        {
            if (ItemManager.itemList[25] < 9)
            {
                beeCount += 1;
            }
            else
            {
                beeCount += ItemManager.itemList[25] / 10;
            }
        }

        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;
        float spotX = honeyCombSpot.x + ((beeCount - 1) % honeyCombWidth) * honeyCombX;
        float spotY = honeyCombSpot.y - ((beeCount - 1) / honeyCombWidth) * honeyCombY - (((beeCount - 1) % honeyCombWidth) % 2) * (honeyCombY / 2);


        GameObject prefabBee = Resources.Load<GameObject>("Prefabs/bee/" + Bee);
        Instantiate(prefabBee, new Vector2(spotX, spotY), Quaternion.identity);
    }

    // 분봉하기 버튼 활성화
    void SwarmingButtonActiveCheck()
    {
        if (upgrade["queenHealth"] + upgrade["queenStorage"] + upgrade["beeHealth"] + upgrade["beeStorage"] >= 100)
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
        long reward = upgrade["queenHealth"] + upgrade["queenStorage"] + upgrade["beeHealth"] + upgrade["beeStorage"];
        long additional = 0;

        if (reward >= 100)
        {
            honey = 0;
            if (ItemManager.itemList[7] != 0)
            {
                additional += reward * (ItemManager.itemList[7] + 1) / 1000;
            }
            if (ItemManager.itemList[15] != 0)
            {
                additional += reward * (ItemManager.itemList[15] + 1) / 1000;
            }
            royalJelly += (reward + additional) / 10;


            upgrade["queenHealth"] = 0;
            upgradePrice["queenHealth"] = 5;
            upgrade["queenStorage"] = 1;
            upgradePrice["queenStorage"] = 5;
            upgrade["honeycomb"] = 1;
            upgradePrice["honeycomb"] = 5;

            upgrade["beeHealth"] = 0;
            upgradePrice["beeHealth"] = 5;
            upgrade["beeStorage"] = 1;
            upgradePrice["beeStorage"] = 5;
            upgrade["beeSpeed"] = 0;
            upgradePrice["beeSpeed"] = 5;
            upgrade["beeCount"] = 0;
            upgradePrice["beeCount"] = 5;

            GameObject[] bees = GameObject.FindGameObjectsWithTag("bee");
            for (int i = 0; i < bees.Length; i++)
            {
                Destroy(bees[i]);
            }

            GameObject[] honeycombs = GameObject.FindGameObjectsWithTag("honeycomb");
            for (int j = 1; j < honeycombs.Length; j++)
            {
                Destroy(honeycombs[j]);
            }
            foreach (KeyValuePair<string, Text> pair in upgradeText)
            {
                UpdateUpgradeText(pair.Key);
            }
        }
    }

    // 진행 내역 저장하기
    void Save()
    {
        SaveData saveData = new SaveData();

        saveData.royalJelly = royalJelly;
        saveData.honey = honey;

        saveData.upgradeKey = new List<string>(upgrade.Keys);
        saveData.upgradeValue = new List<long>(upgrade.Values);
        saveData.upgradePriceKey = new List<string>(upgradePrice.Keys);
        saveData.upgradePriceValue = new List<long>(upgradePrice.Values);

        saveData.Queen = Queen;
        saveData.Bee = Bee;

        saveData.Map = Map;

        saveData.temperature = temperature;
        saveData.temperatureChange = temperatureChange;

        saveData.queenSkinList = SkinManager.queenSkinList;
        saveData.beeSkinList = SkinManager.beeSkinList;
        saveData.mapSkinList = MapManager.mapSkinList;

        saveData.lastPlayTime = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Hour + "/" + DateTime.Now.Minute;

        saveData.day = day;
        saveData.questClearList = questClearList;
        saveData.questCount = questCount;

        saveData.collecting = CollectManager.collecting;
        saveData.collectStartTime = CollectManager.collectStartTime;
        saveData.itemName = CollectManager.itemName;

        saveData.itemList = ItemManager.itemList;

        saveData.tutorialCount = TutorialManager.Count;

        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData, path);
    }

    // 저장 내역 불러오기
    void Load()
    {
        SaveData saveData = new SaveData();

        string path = UnityEngine.Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<SaveData>(path);

        royalJelly = saveData.royalJelly;
        honey = saveData.honey;

        for (int i = 0; i < saveData.upgradeKey.Count; i++)
        {
            upgrade[saveData.upgradeKey[i]] = saveData.upgradeValue[i];
        }
        for (int i = 0; i < saveData.upgradePriceKey.Count; i++)
        {
            upgradePrice[saveData.upgradePriceKey[i]] = saveData.upgradePriceValue[i];
        }

        Queen = saveData.Queen;
        Bee = saveData.Bee;

        Map = saveData.Map;

        temperature = saveData.temperature;
        temperatureChange = saveData.temperatureChange;

        SkinManager.queenSkinList = saveData.queenSkinList;
        SkinManager.beeSkinList = saveData.beeSkinList;
        MapManager.mapSkinList = saveData.mapSkinList;

        day = saveData.day;
        questClearList = saveData.questClearList;
        questCount = saveData.questCount;

        lastPlayTime = saveData.lastPlayTime;

        CollectManager.collecting = saveData.collecting;
        CollectManager.collectStartTime = saveData.collectStartTime;
        CollectManager.itemName = saveData.itemName;

        ItemManager.itemList = saveData.itemList;

        TutorialManager.Count = saveData.tutorialCount;
    }

    public void CheckMaxHoneyComb()
    {
        maxHoneyComb = 15;
        for (int i = 1; i < MapManager.mapSkinList.Length; i++)
        {
            if (MapManager.mapSkinList[i])
            {
                maxHoneyComb += 5;
            }
        }

        if (ItemManager.itemList[4] > 0)
        {
            if (ItemManager.itemList[4] < 9)
            {
                maxHoneyComb += 1;
            }
            else
            {
                maxHoneyComb += (ItemManager.itemList[4] + 1) / 10;
            }
        }
    }

    // 저장 내역에 맞게 벌집 생성
    void FillHoneyComb()
    {
        CheckMaxHoneyComb();

        GameObject[] honeycombs = GameObject.FindGameObjectsWithTag("honeycomb");

        if (upgrade["honeycomb"] != honeycombs.Length)
        {
            for (int i = honeycombs.Length; i < upgrade["honeycomb"]; i++)
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

        if (upgrade["honeycomb"] / 5 > honeycombBGs.Length || 3 > honeycombBGs.Length)
        {
            for (int i = honeycombBGs.Length; i <= upgrade["honeycomb"] / 5 || i < 3; i++)
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

        int beeCount = (int) upgrade["beeCount"];
        if (ItemManager.itemList[25] > 0)
        {
            if(ItemManager.itemList[25] < 9)
            {
                beeCount += 1;
            }
            else
            {
                beeCount += ItemManager.itemList[25] / 10;
            }
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
        GameObject prefabBeeStop = Resources.Load<GameObject>("Prefabs/bee/beeStop");
        prefabBeeStop.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/bee/" + Bee + "5");

        GameObject prefabBee = Resources.Load<GameObject>("Prefabs/bee/" + Bee);

        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;
        float spotX = honeyCombSpot.x + (i % honeyCombWidth) * honeyCombX;
        float spotY = honeyCombSpot.y - (i / honeyCombWidth) * honeyCombY - ((i % honeyCombWidth) % 2) * (honeyCombY / 2);

        GameObject Beestop = Instantiate(prefabBeeStop, new Vector2(spotX, spotY), Quaternion.identity);


        int rand = UnityEngine.Random.Range(0, 11);

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

        prefabQueen = Resources.Load<GameObject>("Prefabs/queen/" + Queen);
        queenSelectSkinImage.sprite = Resources.Load<Sprite>("Sprites/queen/" + Queen + "1");
        beeSelectSkinImage.sprite = Resources.Load<Sprite>("Sprites/bee/" + Bee + "5");

        Instantiate(prefabQueen, new Vector2(spotX, spotY), Quaternion.identity);
    }

    // 맵 선택 스킨 적용
    public void MapSelect()
    {
        backgroundImage.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/background/background" + Map);
        hiveImage.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/hive/hive" + Map);

        int skinNum = 0;
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedbtnList = btnList.OrderBy(go => go.transform.position.y).ToList();

        for (int i = 1; i < sortedbtnList.Count; i++)
        {
            string check = sortedbtnList[i].GetComponent<SpriteRenderer>().name;
            check = check[3..];

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
        List<GameObject> sorteSkinList = skinList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        for (int i = 1; i < SkinManager.queenSkinList.Length; i++)
        {
            if (SkinManager.queenSkinList[i])
            {
                Transform buyBtn = sorteSkinList[i].transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = sorteSkinList[i].transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                string skinName = sorteSkinList[i].GetComponent<SpriteRenderer>().sprite.name;
                skinName = skinName.Substring(0, skinName.Length - 4) + "1";

                sorteSkinList[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/queen/" + skinName);
            }
        }

        GameObject[] beeSkinList = GameObject.FindGameObjectsWithTag("beeSkin");
        List<GameObject> sortedbeeSkinList = beeSkinList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        for (int i = 1; i < SkinManager.beeSkinList.Length; i++)
        {
            if (SkinManager.beeSkinList[i])
            {
                Transform buyBtn = sortedbeeSkinList[i].transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = sortedbeeSkinList[i].transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                string skinName = sortedbeeSkinList[i].GetComponent<SpriteRenderer>().sprite.name;
                skinName = skinName.Substring(0, skinName.Length - 4) + "5";

                sortedbeeSkinList[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/bee/" + skinName);
            }
        }

        GameObject[] mapSkinList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedmapSkinList = mapSkinList.OrderBy(go => go.transform.position.y).ToList();

        for (int i = 1; i < MapManager.mapSkinList.Length; i++)
        {
            if (MapManager.mapSkinList[i])
            {
                Transform buyBtn = sortedmapSkinList[i].transform.Find("Canvas").Find("buyMap");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = sortedmapSkinList[i].transform.Find("Canvas").Find("selectMap");
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
