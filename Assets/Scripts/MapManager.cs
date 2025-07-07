using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    public string Map;

    public Text RoyalJellyText;         // ·Î¾âÁ©¸® ¾ç È®ÀÎÇÏ´Â ±Û¾¾

    public Sprite jellyImg;
    public Sprite jellyLockImg;

    public static bool[] mapSkinList = { true, false, false, false, false, false, false };
    private long[] mapSkinPriceList = { 0, 1000, 10000, 15000, 30000, 50000, 60000 };
    public static float[] minTempList = { -5, -6, -2, -3, -4, 0, -1 };
    public static float[] maxTempList = { 15, 10, 12, 27, 16, 25, 20 };

    // Start is called before the first frame update
    void Start()
    {
        ShowSkinMoney();
    }

    // Update is called once per frame
    void Update()
    {
        ShowJelly();
        ActiveSkinMoney();
    }

    void ShowJelly()
    {
        if (GameManager.royalJelly == 0)                                                // º¸À¯ÇÑ ·Î¾âÁ©¸® °³¼ö°¡ 0°³ÀÎÁö È®ÀÎ
        {
            RoyalJellyText.text = "0";                                    // ·Î¾âÁ©¸® 0 °³·Î Ç¥½Ã
        }
        else
        {
            RoyalJellyText.text = DivideNumber(GameManager.royalJelly);    // º¸À¯ÇÑ ·Î¾âÁ©¸® °³¼ö Ç¥½Ã
        }
    }

    public void mapSkinSelect(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().name;
        GameManager.Map = skinName.Substring(3);
        Map = skinName.Substring(3);

        int skinNum = 0;
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        for (int i = 1; i < btnList.Length; i++)
        {
            string check = btnList[i].GetComponent<SpriteRenderer>().name;

            if (check == skinName)
            {
                skinNum = i;
            }
        }
        GameManager.maxTemp = maxTempList[skinNum];
        GameManager.minTemp = minTempList[skinNum];
    }

    public void mapSkinBuy(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().name;
        int skinNum = 0;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        for (int i = 1; i < btnList.Length; i++)
        {
            string check = btnList[i].GetComponent<SpriteRenderer>().name;

            if (check == skinName)
            {
                skinNum = i;
            }
        }

        long money = mapSkinPriceList[skinNum];

        if (GameManager.royalJelly >= money)
        {
            GameManager.royalJelly -= money;

            Transform buyBtn = skin.transform.Find("Canvas").Find("buyMap");
            buyBtn.gameObject.SetActive(false);

            Transform selectBtn = skin.transform.Find("Canvas").Find("selectMap");
            selectBtn.gameObject.SetActive(true);

            mapSkinList[skinNum] = true;
        }
    }

    void ShowSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");

        for (int i = 1; i < btnList.Length; i++)
        {
            if (!mapSkinList[i])
            {
                Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buyMap");
                buyBtn.Find("Text").GameObject().GetComponent<Text>().text = DivideNumber(mapSkinPriceList[i]);
            }
        }
    }

    void ActiveSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        Color color = new Color32(112, 59, 0, 255);

        for (int i = 1; i < btnList.Length; i++)
        {
            if (!mapSkinList[i])
            {
                if (GameManager.royalJelly < mapSkinPriceList[i])
                {
                    Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buyMap");
                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyLockImg;
                    buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                }
                else
                {
                    Transform buyBtn = btnList[i].transform.Find("Canvas").Find("buyMap");

                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyImg;
                    buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                }
            }
        }
    }

    public void StartEnterMap()
    {
        Map = GameManager.Map;
    }

    public void StartExitMap()
    {
        GameManager.Map = Map;
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
