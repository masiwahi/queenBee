using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CollectManager : MonoBehaviour
{
    private int selectCount = 0;
    public static bool collecting = false;

    public string selectMapSkin = "mapBasic";
    public string selectQueenSkin = "queenBee";
    public string selectBeeSkin = "beeBasic";

    public static string collectStartTime = "";

    public GameObject selectMapImage;
    public GameObject selectQueenImage;
    public GameObject selectBeeImage;

    public Button rightButton;
    public Button leftButton;
    public Button tripButton;

    public GameObject queenPage;
    public GameObject beePage;

    public GameObject EndGamePanel;

    private Vector2 firstTouch;
    private Vector2 currentTouch;
    private float queenLimitMinX = 7.19f;
    private float beeLimitMinX = 7.19f;
    private float limitMaxX = 7.19f;
    public float dragSpeed = 0.05f;

    private List<GameObject> queenList = new List<GameObject>();
    private List<GameObject> beeList = new List<GameObject>();
    private List<GameObject> itemList = new List<GameObject>();
    public static List<string> itemName = new List<string>();

    void Start()
    {
        GameObject[] TagQueenSelect = GameObject.FindGameObjectsWithTag("queenSelect");
        queenList = TagQueenSelect.OrderBy(go => go.transform.position.x).ToList();
        GameObject[] TagBeeSelect = GameObject.FindGameObjectsWithTag("beeSelect");
        beeList = TagBeeSelect.OrderBy(go => go.transform.position.x).ToList();
        GameObject[] TagItemList = GameObject.FindGameObjectsWithTag("itemList");
        itemList = TagItemList.OrderBy(go => go.transform.position.x).ToList();

        if (!itemName.Contains("honey"))
        {
            itemName.Add("honey");
        }
        if (!itemName.Contains("royalJelly"))
        {
            itemName.Add("royalJelly");
        }

        for (int i = 2; i < itemName.Count; i++)
        {
            string[] item = itemName[i].Split("/");
            itemName[i] = itemName[i][..^1];

            if (item[^1][..5] == "queen")
            {
                selectQueenImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/queen/" + item[^1] + "1");
                selectQueenImage.SetActive(true);
            }
            else if (item[^1][..3] == "bee")
            {
                selectBeeImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/bee/" + item[^1] + "5");
                selectBeeImage.SetActive(true);
            }
            else
            {
                selectMapImage.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/collect/map/" + item[^1][..^1]);
            }
        }

        ItemListChange();

        queenPage.SetActive(false);
        beePage.SetActive(false);

        if (collecting)
        {
            BlockCollect();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(firstTouch, currentTouch) > 0.1f)
            {
                if (firstTouch.x < currentTouch.x)
                {
                    if (queenPage.transform.localPosition.x < limitMaxX)
                    {
                        queenPage.transform.Translate(Vector2.right * dragSpeed);
                    }
                    if (beePage.transform.localPosition.x < limitMaxX)
                    {
                        beePage.transform.Translate(Vector2.right * dragSpeed);
                    }
                }
                else if (firstTouch.x > currentTouch.x)
                {
                    if (queenPage.transform.localPosition.x > queenLimitMinX)
                    {
                        queenPage.transform.Translate(Vector2.left * dragSpeed);
                    }
                    if (beePage.transform.localPosition.x > beeLimitMinX)
                    {
                        beePage.transform.Translate(Vector2.left * dragSpeed);
                    }
                }
            }
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstTouch = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouch = touch.position - touch.deltaPosition;

                if (firstTouch.x < currentTouch.x && currentTouch.x - firstTouch.x > 0.1f)
                {
                    if (queenPage.transform.localPosition.x < limitMaxX)
                    {
                        queenPage.transform.Translate(Vector2.right * dragSpeed);
                    }
                    if (beePage.transform.localPosition.x < limitMaxX)
                    {
                        beePage.transform.Translate(Vector2.right * dragSpeed);
                    }
                }
                else if (firstTouch.x > currentTouch.x && firstTouch.x - currentTouch.x > 0.1f)
                {
                    if (queenPage.transform.localPosition.x > queenLimitMinX)
                    {
                        queenPage.transform.Translate(Vector2.left * dragSpeed);
                    }
                    if (beePage.transform.localPosition.x > beeLimitMinX)
                    {
                        beePage.transform.Translate(Vector2.left * dragSpeed);
                    }
                }
                firstTouch = touch.position - touch.deltaPosition;
            }
        }

        if (collecting)
        {
            string[] collectTimeList = collectStartTime.Split("/");
            int[] collectTimeInt = new int[collectTimeList.Length];
            for (int i = 0; i < collectTimeList.Length; i++)
            {
                collectTimeInt[i] = int.Parse(collectTimeList[i]);
            }

            if (DateTime.Now.Year > collectTimeInt[0])
            {
                collectTimeInt[1] -= 12;
            }
            if (DateTime.Now.Month > collectTimeInt[1])
            {
                collectTimeInt[2] -= 30;
            }
            if (DateTime.Now.Day > collectTimeInt[2])
            {
                collectTimeInt[3] -= 24;
            }
            if (DateTime.Now.Hour > collectTimeInt[3])
            {
                collectTimeInt[4] -= 60;
            }

            int emptyMinute = DateTime.Now.Minute - collectTimeInt[4];

            int discount = ItemManager.itemList[10] + ItemManager.itemList[20] + ItemManager.itemList[30];
            if (ItemManager.itemList[10] > 0) { discount += 1; }
            if (ItemManager.itemList[20] > 0) { discount += 1; }
            if (ItemManager.itemList[30] > 0) { discount += 1; }

            if (emptyMinute > (240 - (discount) * 0.24))
            {
                collecting = false;
                EndCollect();
            }
        }
    }

    public void MapSelect(int direction)
    {
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedbtnList = btnList.OrderBy(go => go.transform.position.y).ToList();

        itemName.Remove("map/" + selectMapSkin + "1");
        itemName.Remove("map/" + selectMapSkin + "2");

        do
        {
            selectCount += direction;
            if (selectCount < 0)
            {
                selectCount = sortedbtnList.Count - 1;
            }
            else if (selectCount == sortedbtnList.Count)
            {
                selectCount = 0;
            }
        } while (!MapManager.mapSkinList[selectCount]);

        selectMapSkin = sortedbtnList[selectCount].GetComponent<SpriteRenderer>().name;
        selectMapImage.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/collect/map/" + selectMapSkin);

        if (selectMapSkin != "mapBasic")
        {
            itemName.Add("map/" + selectMapSkin + "1");
            itemName.Add("map/" + selectMapSkin + "2");
        }
        ItemListChange();
    }

    public void OpenSkinSelect(string kind)
    {
        if (kind == "queen" && queenPage.transform.localPosition.y == -7)
        {
            queenPage.transform.localPosition = new Vector2(7.19f, -3.5f);
            beePage.transform.localPosition = new Vector2(7.19f, -7);

            queenPage.SetActive(true);
            beePage.SetActive(false);


            float spotX = -0.45f;

            int skinCount = 0;

            for (int i = 0; i < SkinManager.queenSkinList.Length; i++)
            {
                if (SkinManager.queenSkinList[i])
                {
                    queenList[i].SetActive(true);
                    queenList[i].transform.localPosition = new Vector2(spotX, 0);
                    spotX += 0.09f;
                    skinCount++;
                }
                else
                {
                    queenList[i].SetActive(false);
                }
            }
            queenLimitMinX = 7.19f;
            if (skinCount > 3)
            {
                queenLimitMinX -= 1.8f * (skinCount - 3);
            }
        }
        else if (kind == "bee" && beePage.transform.localPosition.y == -7)
        {
            queenPage.transform.localPosition = new Vector2(7.19f, -7);
            beePage.transform.localPosition = new Vector2(7.19f, -3.5f);

            queenPage.SetActive(false);
            beePage.SetActive(true);


            float spotX = -0.45f;

            int skinCount = 0;

            for (int i = 0; i < SkinManager.beeSkinList.Length; i++)
            {
                if (SkinManager.beeSkinList[i])
                {
                    beeList[i].SetActive(true);
                    beeList[i].transform.localPosition = new Vector2(spotX, 0);
                    spotX += 0.09f;
                    skinCount++;
                }
                else
                {
                    beeList[i].SetActive(false);
                }
            }
            beeLimitMinX = 7.19f;
            if (skinCount > 3)
            {
                beeLimitMinX -= 1.8f * (skinCount - 3);
            }
        }
        else
        {
            queenPage.transform.localPosition = new Vector2(7.19f, -7);
            beePage.transform.localPosition = new Vector2(7.19f, -7);

            queenPage.SetActive(false);
            beePage.SetActive(false);
        }
    }

    public void SelectSkin(GameObject select)
    {
        string selectName = select.name;
        selectName = selectName[..5];

        if (selectName == "queen")
        {
            itemName.Remove("skin/queen/" + selectQueenSkin);

            selectQueenSkin = select.name;
            selectQueenSkin = selectQueenSkin[..^6];

            selectQueenImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/queen/" + selectQueenSkin + "1");
            selectQueenImage.SetActive(true);
            queenPage.transform.localPosition = new Vector2(0, -7);
            if (selectQueenSkin != "queenBee")
            {
                itemName.Add("skin/queen/" + selectQueenSkin);
            }
            queenPage.SetActive(false);
        }
        else
        {
            itemName.Remove("skin/bee/" + selectBeeSkin);

            selectBeeSkin = select.name;
            selectBeeSkin = selectBeeSkin[..^6];

            selectBeeImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/bee/" + selectBeeSkin + "5");
            selectBeeImage.SetActive(true);
            beePage.transform.localPosition = new Vector2(0, -7);
            if (selectBeeSkin != "beeBasic")
            {
                itemName.Add("skin/bee/" + selectBeeSkin);
            }
            beePage.SetActive(false);
        }
        ItemListChange();
    }

    public void ItemListChange()
    {
        for (int i = 2; i < itemName.Count; i++)
        {
            itemList[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/collect/icon/" + itemName[i]);
            itemList[i].SetActive(true);
        }
        for (int i = itemName.Count; i < itemList.Count; i++)
        {
            itemList[i].SetActive(false);
        }
    }

    public void StartCollect()
    {
        collectStartTime = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Hour + "/" + DateTime.Now.Minute;
        collecting = true;
        BlockCollect();
    }

    void BlockCollect()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);

        selectQueenImage.transform.parent.GetComponent<Button>().interactable = false;
        selectBeeImage.transform.parent.GetComponent<Button>().interactable = false;

        if (!selectBeeImage.activeSelf)
        {
            selectBeeImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/bee/beeBasic5");
            selectBeeImage.gameObject.SetActive(true);
        }

        if (!selectQueenImage.activeSelf)
        {
            selectQueenImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/queen/queenBee1");
            selectQueenImage.gameObject.SetActive(true);
        }

        tripButton.interactable = false;
        tripButton.transform.Find("tripText").GetComponent<Text>().text = "모험 중";
    }

    void EndCollect()
    {
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);

        selectQueenImage.transform.parent.GetComponent<Button>().interactable = true;
        selectBeeImage.transform.parent.GetComponent<Button>().interactable = true;

        tripButton.interactable = true;
        tripButton.transform.Find("tripText").GetComponent<Text>().text = "모험 보내기";   

        EndGamePanel.SetActive(true);


        int rand = UnityEngine.Random.Range(0, 1000);

        int reward = -1;

        int luck = ItemManager.itemList[11];

        if (luck > 0) { luck += 1; }

        luck = (itemName.Count - 2) * 5 + ((itemName.Count - 2) * luck);

        if (rand < luck)
        {
            luck = UnityEngine.Random.Range(0, itemName.Count - 2);
            for (int i = 0; i < ItemManager.itemName.Length; i++)
            {
                if (ItemManager.itemName[i] == itemName[luck + 2])
                {
                    reward = i;
                }
            }
            if (reward != -1)
            {
                ItemManager.itemList[reward] += 1;
            }
            EndGamePanel.transform.Find("RewardItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/collect/icon/" + itemName[rand + 2]);
            EndGamePanel.transform.Find("RewardCount").GetComponent<Text>().text = "1개";
        }
        else
        {
            rand = UnityEngine.Random.Range(0, 5);
            if (rand < 2)
            {
                reward = UnityEngine.Random.Range(1, 101);
                GameManager.royalJelly += reward;

                EndGamePanel.transform.Find("RewardItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/collect/icon/" + itemName[1]);
            }
            else
            {
                reward = UnityEngine.Random.Range(100, 10001);
                GameManager.honey += reward;

                EndGamePanel.transform.Find("RewardItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/collect/icon/" + itemName[0]);
            }
            EndGamePanel.transform.Find("RewardCount").GetComponent<Text>().text = reward + "개";
        }
            collectStartTime = "";
    }

}
