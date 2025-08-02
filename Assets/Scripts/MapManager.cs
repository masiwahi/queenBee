using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MapManager : MonoBehaviour
{

    public string Map;              // 선택한 맵 스킨

    public Text RoyalJellyText;     // 로얄젤리 양 확인하는 글씨

    public Sprite jellyImg;         // 로얄젤리 이미지
    public Sprite jellyLockImg;     // 빈 로얄 젤리 이미지

    public static bool[] mapSkinList = { true, false, false, false, false, false, false };  // 맵 스킨 구매 여부
    private long[] mapSkinPriceList = { 0, 1000, 10000, 15000, 30000, 50000, 60000 };       // 맵 스킨 구매 가격
    public static float[] minTempList = { -5, -6, -2, -3, -4, 0, -1 };                      // 맵 스킨 별 최저 온도
    public static float[] maxTempList = { 15, 10, 12, 27, 16, 25, 20 };                     // 맵 스킨 별 최고 온도

    // Start is called before the first frame update
    void Start()
    {
        ShowSkinMoney();
        ShowSkinTemperature();
    }

    // Update is called once per frame
    void Update()
    {
        ShowJelly();
        ActiveSkinMoney();
    }

    // 현재 보유한 로얄 젤리 개수 업데이트
    void ShowJelly()
    {
        RoyalJellyText.text = DivideNumber(GameManager.royalJelly);
    }

    // 맵 스킨 선택
    public void mapSkinSelect(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().name;
        GameManager.Map = skinName.Substring(3);
        Map = skinName.Substring(3);

        int skinNum = 0;
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedbtnList = btnList.OrderBy(go => go.transform.position.y).ToList();
        for (int i = 1; i < sortedbtnList.Count; i++)
        {
            string check = sortedbtnList[i].GetComponent<SpriteRenderer>().name;

            if (check == skinName)
            {
                skinNum = i;
            }
        }
        GameManager.maxTemp = maxTempList[skinNum];
        GameManager.minTemp = minTempList[skinNum];
    }

    //맵 스킨 구매
    public void mapSkinBuy(GameObject skin)
    {
        string skinName = skin.GetComponent<SpriteRenderer>().name;
        int skinNum = 0;

        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedbtnList = btnList.OrderBy(go => go.transform.position.y).ToList();
        for (int i = 1; i < sortedbtnList.Count; i++)
        {
            string check = sortedbtnList[i].GetComponent<SpriteRenderer>().name;

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

    // 맵 스킨 온도 표시
    void ShowSkinTemperature()
    {
        GameObject[] imageList = GameObject.FindGameObjectsWithTag("tempBottle");
        List<GameObject> sortedimageList = imageList.OrderBy(go => go.transform.position.y).ToList();
        for (int i = 0; i < sortedimageList.Count; i++)
        {
            Transform bottleText = sortedimageList[i].transform.Find("Text");
            bottleText.GameObject().GetComponent<Text>().text = minTempList[i] + " ~ " + maxTempList[i];

        }
    }

    // 맵 스킨 구매 가격 표시
    void ShowSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedbtnList = btnList.OrderBy(go => go.transform.position.y).ToList();
        for (int i = 1; i < sortedbtnList.Count; i++)
        {
            if (!mapSkinList[i])
            {
                Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buyMap");
                buyBtn.Find("Text").GameObject().GetComponent<Text>().text = DivideNumber(mapSkinPriceList[i]);
            }
        }
    }

    // 맵 스킨 구매 가격 미달시 비활성화
    void ActiveSkinMoney()
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedbtnList = btnList.OrderBy(go => go.transform.position.y).ToList();
        Color color = new Color32(112, 59, 0, 255);

        for (int i = 1; i < sortedbtnList.Count; i++)
        {
            if (!mapSkinList[i])
            {
                if (GameManager.royalJelly < mapSkinPriceList[i])
                {
                    Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buyMap");
                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyLockImg;
                    buyBtn.Find("Text").GameObject().GetComponent<Text>().color = Color.grey;
                }
                else
                {
                    Transform buyBtn = sortedbtnList[i].transform.Find("Canvas").Find("buyMap");

                    Image image = buyBtn.Find("Image").GetComponent<Image>();
                    image.sprite = jellyImg;
                    buyBtn.Find("Text").GameObject().GetComponent<Text>().color = color;
                }
            }
        }
    }

    // 맵 스킨 선택 페이지 들어가기
    public void StartEnterMap()
    {
        Map = GameManager.Map;
    }

    // 맵 스킨 선택 페이지 나가기
    public void StartExitMap()
    {
        GameManager.Map = Map;
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
