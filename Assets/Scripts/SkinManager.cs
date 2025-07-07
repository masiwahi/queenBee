using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class SkinManager : MonoBehaviour
{
    public Image queenSelectSkinImage;
    public Image beeSelectSkinImage;

    public Text HoneyText;              // 꿀벌 양 체크하는 글씨
    public Text RoyalJellyText;         // 로얄젤리 양 확인하는 글씨

    public Sprite selectImg;
    public Sprite basicImg;
    public Sprite jellyImg;
    public Sprite honeyImg;
    public Sprite jellyLockImg;
    public Sprite honeyLockImg;

    public string Queen;
    public static string Bee = "beeBasic";

    public static bool[] queenSkinList = { true, false, false, false, false, false, false, false, false, false, false };
    private bool[] queenSkinJellyList = { false, false, false, true, true, true, true, true, true, true, true };
    private long[] queenSkinPriceList = { 0, 10000, 100000, 1000, 10000, 15000, 30000, 50000, 70000, 100000, 150000 };

    public static bool[] beeSkinList = { true, false, false, false, false, false, false, false, false, false, false };
    private bool[] beeSkinJellyList = { false, false, false, true, true, true, true, true, true, true, true };
    private long[] beeSkinPriceList = { 0, 15000, 150000, 1500, 15000, 20000, 35000, 60000, 80000, 130000, 200000 };

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

    void ShowHoney()
    {
        if (GameManager.honey == 0)                                                     // 보유한 꿀 개수가 0개인지 확인
        {
            HoneyText.text = "0";                                         // 꿀 0 개로 표시
        }
        else
        {
            HoneyText.text = DivideNumber(GameManager.honey);              // 보유한 꿀 개수 표시
        }

        if (GameManager.royalJelly == 0)                                                // 보유한 로얄젤리 개수가 0개인지 확인
        {
            RoyalJellyText.text = "0";                                    // 로얄젤리 0 개로 표시
        }
        else
        {
            RoyalJellyText.text = DivideNumber(GameManager.royalJelly);    // 보유한 로얄젤리 개수 표시
        }
    }

    public void queenSkinSelect(GameObject skin)
    {
        queenSelectSkinImage.sprite = skin.GetComponent<SpriteRenderer>().sprite;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");        
        Button skinBtn = skin.GetComponentInChildren<Button>();
        for (int i = 0; i < btnList.Length; i++)
        {
            Button checkBtn = btnList[i].GetComponentInChildren<Button>();
            if (checkBtn != skinBtn)
            {
                checkBtn.GetComponent<Image>().sprite = basicImg;
            }
        }
        skinBtn.GetComponent<Image>().sprite = selectImg;

        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;

        GameManager.Queen = skinName.Substring(0, skinName.Length - 1);
        Queen = skinName.Substring(0, skinName.Length - 1);
    }

    public void queenSkinBuy(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;
        skinName = skinName.Substring(0, skinName.Length - 4);
        int skinNum = 0;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");
        for (int i = 0; i < btnList.Length; i++)
        {
            string check = btnList[i].GetComponent<SpriteRenderer>().sprite.name;
            check = check.Substring(0, check.Length - 4);

            if (check == skinName)
            {
                skinNum = i;
            }
        }

        long money = queenSkinPriceList[skinNum];
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

                skin.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/queen/" + skinName + "1.png", typeof(Sprite));
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

                skin.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/queen/" + skinName + "1.png", typeof(Sprite));
                queenSkinList[skinNum] = true;
            }
        }
    }

    public void beeSkinSelect(GameObject skin)
    {
        beeSelectSkinImage.sprite = skin.GetComponent<SpriteRenderer>().sprite;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("beeSkin");
        Button skinBtn = skin.GetComponentInChildren<Button>();
        for (int i = 0; i < btnList.Length; i++)
        {
            Button checkBtn = btnList[i].GetComponentInChildren<Button>();
            if (checkBtn != skinBtn)
            {
                checkBtn.GetComponent<Image>().sprite = basicImg;
            }
        }
        skinBtn.GetComponent<Image>().sprite = selectImg;

        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;

        Bee = skinName.Substring(0, skinName.Length - 1);
    }

    public void beeSkinBuy(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().sprite.name;
        skinName = skinName.Substring(0, skinName.Length - 4);
        int skinNum = 0;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("beeSkin");
        for (int i = 0; i < btnList.Length; i++)
        {
            string check = btnList[i].GetComponent<SpriteRenderer>().sprite.name;
            check = check.Substring(0, check.Length - 4);

            if (check == skinName)
            {
                skinNum = i;
            }
        }

        long money = beeSkinPriceList[skinNum];
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

                skin.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + skinName + "5.png", typeof(Sprite));
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

                skin.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + skinName + "5.png", typeof(Sprite));
                beeSkinList[skinNum] = true;
            }
        }
    }

    public void StartEnterSkin()
    {
        Bee = GameManager.Bee;
        Queen = GameManager.Queen;
    }

    public void StartExitSkin()
    {
        GameManager.Queen = Queen;
    }

    void ShowSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");

        for (int i = 1; i < btnList.Length; i++)
        {
            if (!queenSkinList[i])
            {
                Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buySkin");
                if (queenSkinJellyList[i])
                {
                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyImg;
                }
                buyBtn.Find("Text").GameObject().GetComponent<Text>().text = DivideNumber(queenSkinPriceList[i]);
            }
        }

        GameObject[] beeBtnList = GameObject.FindGameObjectsWithTag("beeSkin");

        for (int i = 1; i < beeBtnList.Length; i++)
        {
            if (!beeSkinList[i])
            {
                Transform buyBtn = beeBtnList[i].transform.Find("Canvas").Find("buySkin");
                if (beeSkinJellyList[i])
                {
                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyImg;
                }
                buyBtn.Find("Text").GameObject().GetComponent<Text>().text = DivideNumber(beeSkinPriceList[i]);
            }
        }
    }

    void ActiveSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("queenSkin");
        Color color = new Color32(112, 59, 0, 255);

        for (int i = 1; i < btnList.Length; i++)
        { 
            if (!queenSkinList[i])
            {
                if (queenSkinJellyList[i])
                {
                    if (GameManager.royalJelly < queenSkinPriceList[i])
                    {
                        Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
                else
                {
                    if (GameManager.honey < queenSkinPriceList[i])
                    {
                        Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
            }
        }

        GameObject[] beeBtnList = GameObject.FindGameObjectsWithTag("beeSkin");

        for (int i = 1; i < beeBtnList.Length; i++)
        {
            if (!beeSkinList[i])
            {
                if (beeSkinJellyList[i])
                {
                    if (GameManager.royalJelly < beeSkinPriceList[i])
                    {
                        Transform buyBtn = beeBtnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = beeBtnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = jellyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
                else
                {
                    if (GameManager.honey < beeSkinPriceList[i])
                    {
                        Transform buyBtn = beeBtnList[i].transform.Find("Canvas").Find("buySkin");
                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyLockImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                    }
                    else
                    {
                        Transform buyBtn = beeBtnList[i].transform.Find("Canvas").Find("buySkin");

                        Image image = buyBtn.Find("Image").GetComponent<Image>();
                        image.sprite = honeyImg;
                        buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                    }
                }
            }
        }
    }

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