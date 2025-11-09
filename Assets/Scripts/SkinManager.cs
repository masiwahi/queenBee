using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkinManager : MonoBehaviour
{
    public Image queenSelectSkinImage;      // 여왕벌 선택 스킨 이미지
    public Image beeSelectSkinImage;        // 일벌 선택 스킨 이미지

    public Text HoneyText;                  // 꿀벌 양 체크하는 글씨
    public Text RoyalJellyText;             // 로얄젤리 양 확인하는 글씨

    public Sprite selectImg;                // 선택시 배경 이미지
    public Sprite basicImg;                 // 미선택시 배경 이미지
    public Sprite jellyImg;                 // 로얄젤리 이미지
    public Sprite honeyImg;                 // 벌꿀 이미지
    public Sprite jellyLockImg;             // 비어있는 로얄젤리 이미지
    public Sprite honeyLockImg;             // 비어있는 벌꿀 이미지

    public string Queen;                    // 여왕 선택 스킨 이름
    public static string Bee = "beeBasic";  // 일벌 선택 스킨 이름

    public static bool[] queenSkinList = { true, false, false, false, false, false, false, false, false, false, false };    // 여왕 구매 스킨 목록
    private bool[] queenSkinJellyList = { false, false, false, true, true, true, true, true, true, true, true };            // 여왕 스킨 젤리로 구매 가능 여부
    private long[] queenSkinPriceList = { 0, 10000, 100000, 1000, 10000, 15000, 30000, 50000, 70000, 100000, 150000 };      // 여왕 스킨 가격

    public static bool[] beeSkinList = { true, false, false, false, false, false, false, false, false, false, false };      // 일벌 구매 스킨 목록
    private bool[] beeSkinJellyList = { false, false, false, true, true, true, true, true, true, true, true };              // 일벌 스킨 젤리로 구매 가능 여부
    private long[] beeSkinPriceList = { 0, 15000, 150000, 1500, 15000, 20000, 35000, 60000, 80000, 130000, 200000 };        // 일벌 스킨 가격

    // Start is called before the first frame update
    void Start()
    {
        ShowSkinMoney();
    }

    // Update is called once per frame
    void Update()
    {
        ShowHoney();
        ActiveSkinMoney();
    }

    // 화면 상단 로얄젤리와 벌꿀양 업데이트
    void ShowHoney()
    {
        HoneyText.text = DivideNumber(GameManager.honey);
        RoyalJellyText.text = DivideNumber(GameManager.royalJelly);
    }

    // 여왕 스킨 선택
    public void queenSkinSelect(GameObject skin)
    {
        queenSelectSkinImage.sprite = skin.GetComponent<SpriteRenderer>().sprite;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");
        List<GameObject> sortedbtnList = btnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        Button skinBtn = skin.GetComponentInChildren<Button>();
        for (int i = 0; i < sortedbtnList.Count; i++)
        {
            Button checkBtn = sortedbtnList[i].GetComponentInChildren<Button>();
            if (checkBtn != skinBtn)
            {
                checkBtn.GetComponent<Image>().sprite = basicImg;
            }
        }
        skinBtn.GetComponent<Image>().sprite = selectImg;

        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;

        GameManager.Queen = skinName[..^1];
        Queen = skinName[..^1];
    }

    // 여왕 스킨 구매
    public void queenSkinBuy(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;
        skinName = skinName[..^4];
        int skinNum = 0;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");
        List<GameObject> sortedbtnList = btnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        for (int i = 0; i < sortedbtnList.Count; i++)
        {
            string check = sortedbtnList[i].GetComponent<SpriteRenderer>().sprite.name;
            check = check[..^4];

            if (check == skinName)
            {
                skinNum = i;
            }
        }
        long discount = 0;
        if (ItemManager.itemList[9] != 0)
        {
            discount += queenSkinPriceList[skinNum] * (ItemManager.itemList[9] + 1) / 1000;
        }
        if (ItemManager.itemList[14] != 0)
        {
            discount += queenSkinPriceList[skinNum] * (ItemManager.itemList[14] + 1) / 1000;
        }

        long money = queenSkinPriceList[skinNum] - discount;
        bool jelly = queenSkinJellyList[skinNum];
        if (jelly)
        {
            if (GameManager.royalJelly >= money)
            {
                GameManager.royalJelly -= money;

                Transform buyBtn = skin.transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = skin.transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                skin.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/queen/" + skinName + "1");
                queenSkinList[skinNum] = true;
            }
        }
        else
        {
            if (GameManager.honey >= money)
            {
                GameManager.honey -= money;

                Transform buyBtn = skin.transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = skin.transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                skin.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/queen/" + skinName + "1");
                queenSkinList[skinNum] = true;
            }
        }
    }

    // 일벌 스킨 구매
    public void beeSkinSelect(GameObject skin)
    {
        beeSelectSkinImage.sprite = skin.GetComponent<SpriteRenderer>().sprite;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("beeSkin");
        List<GameObject> sortedbtnList = btnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        Button skinBtn = skin.GetComponentInChildren<Button>();
        for (int i = 0; i < sortedbtnList.Count; i++)
        {
            Button checkBtn = sortedbtnList[i].GetComponentInChildren<Button>();
            if (checkBtn != skinBtn)
            {
                checkBtn.GetComponent<Image>().sprite = basicImg;
            }
        }
        skinBtn.GetComponent<Image>().sprite = selectImg;

        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;

        Bee = skinName[..^1];
    }

    //일벌 스킨 구매
    public void beeSkinBuy(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;
        skinName = skinName[..^4];
        int skinNum = 0;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("beeSkin");
        List<GameObject> sortedbtnList = btnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        for (int i = 0; i < sortedbtnList.Count; i++)
        {
            string check = sortedbtnList[i].GetComponent<SpriteRenderer>().sprite.name;
            check = check[..^4];

            if (check == skinName)
            {
                skinNum = i;
            }
        }

        long discount = 0;
        if (ItemManager.itemList[9] != 0)
        {
            discount += queenSkinPriceList[skinNum] * (ItemManager.itemList[9] + 1) / 1000;
        }
        if (ItemManager.itemList[24] != 0)
        {
            discount += queenSkinPriceList[skinNum] * (ItemManager.itemList[24] + 1) / 1000;
        }

        long money = beeSkinPriceList[skinNum] - discount;
        bool jelly = beeSkinJellyList[skinNum];
        if (jelly)
        {
            if (GameManager.royalJelly >= money)
            {
                GameManager.royalJelly -= money;

                Transform buyBtn = skin.transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = skin.transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                skin.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/bee/" + skinName + "5");
                beeSkinList[skinNum] = true;
            }
        }
        else
        {
            if (GameManager.honey >= money)
            {
                GameManager.honey -= money;

                Transform buyBtn = skin.transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = skin.transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                skin.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/bee/" + skinName + "5");
                beeSkinList[skinNum] = true;
            }
        }
    }

    // 스킨 페이지 들어가기
    public void StartEnterSkin()
    {
        Bee = GameManager.Bee;
        Queen = GameManager.Queen;
    }
    // 스킨 페이지 나가기
    public void StartExitSkin()
    {
        GameManager.Queen = Queen;
    }

    // 스킨 구매 가격 보여주기
    void ShowSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");
        List<GameObject> sortedbtnList = btnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        for (int i = 1; i < sortedbtnList.Count; i++)
        {
            if (!queenSkinList[i])
            {
                long discount = 0;
                if (ItemManager.itemList[9] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[9] + 1) / 1000;
                }
                if (ItemManager.itemList[14] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[14] + 1) / 1000;
                }
                
                Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buySkin");
                if (queenSkinJellyList[i])
                {
                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyImg;
                }
                buyBtn.Find("Text").GameObject().GetComponent<Text>().text = DivideNumber(queenSkinPriceList[i] - discount);
            }
        }

        GameObject[] beeBtnList = GameObject.FindGameObjectsWithTag("beeSkin");
        List<GameObject> sortedbeeBtnList = beeBtnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        for (int i = 1; i < sortedbeeBtnList.Count; i++)
        {
            if (!beeSkinList[i])
            {
                long discount = 0;
                if (ItemManager.itemList[9] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[9] + 1) / 1000;
                }
                if (ItemManager.itemList[24] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[24] + 1) / 1000;
                }

                Transform buyBtn = sortedbeeBtnList[i].transform.Find("Canvas").Find("buySkin");
                if (beeSkinJellyList[i])
                {
                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyImg;
                }
                buyBtn.Find("Text").GameObject().GetComponent<Text>().text = DivideNumber(beeSkinPriceList[i] - discount);
            }
        }
    }

    //스킨 구매 가능시 구매버튼 활성화
    void ActiveSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");
        List<GameObject> sortedbtnList = btnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        Color color = new Color32(112, 59, 0, 255);

        for (int i = 1; i < sortedbtnList.Count; i++)
        {
            if (!queenSkinList[i])
            {
                long discount = 0;
                if (ItemManager.itemList[9] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[9] + 1) / 1000;
                }
                if (ItemManager.itemList[14] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[14] + 1) / 1000;
                }


                if (queenSkinJellyList[i])
                {
                    if (GameManager.royalJelly < queenSkinPriceList[i] - discount)
                    {
                        Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
                else
                {
                    if (GameManager.honey < queenSkinPriceList[i] - discount)
                    {
                        Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
            }
        }

        GameObject[] beeBtnList = GameObject.FindGameObjectsWithTag("beeSkin");
        List<GameObject> sortedbeeBtnList = beeBtnList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();

        for (int i = 1; i < sortedbeeBtnList.Count; i++)
        {
            
            if (!beeSkinList[i])
            {
                long discount = 0;
                if (ItemManager.itemList[9] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[9] + 1) / 1000;
                }
                if (ItemManager.itemList[24] != 0)
                {
                    discount += queenSkinPriceList[i] * (ItemManager.itemList[24] + 1) / 1000;
                }

                if (beeSkinJellyList[i])
                {
                    if (GameManager.royalJelly < beeSkinPriceList[i] - discount)
                    {
                        Transform buyBtn = sortedbeeBtnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = sortedbeeBtnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
                else
                {
                    if (GameManager.honey < beeSkinPriceList[i] - discount)
                    {
                        Transform buyBtn = sortedbeeBtnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = sortedbeeBtnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
            }
        }
    }

    // 숫자가 천단위 기준으로 반올림하여 문자로 축약 계산
    string DivideNumber(long num)
    {
        long divineNum = num;
        int divisionNumber = 0;
        while (divineNum / 1000 > 0)
        {
            divineNum = divineNum / 1000;
            divisionNumber++;
        }
        char divisionWord = (char)(divisionNumber + 64);


        if (divisionNumber == 0)
        {
            return divineNum.ToString();
        }
        else
        {
            if (divisionNumber == 1)
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