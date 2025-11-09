using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static int[] itemList = new int[32];
    public static string[] itemName = new string[32];
    private string[] itemEffect = new string[32];

    private float spotX = -1.7f;
    private float spotY = 3;

    public GameObject itemScreen;
    public GameObject prefabItemUnlock;
    public GameObject prefabItemLock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (itemList[6] == 0)
        {
            itemList[6] = 1;
        }
        UpdateItemEffect();

        int i = 0;
        GameObject[] mapList = GameObject.FindGameObjectsWithTag("mapSkin");
        List<GameObject> sortedMapList = mapList.OrderBy(go => go.transform.position.y).ToList();
        sortedMapList.RemoveAt(0);

        foreach (GameObject map in sortedMapList)
        {
            itemName[i++] = "map/" + map.name + "1";
            itemName[i++] = "map/" + map.name + "2";
        }

        GameObject[] queenList = GameObject.FindGameObjectsWithTag("queenSkin");
        List<GameObject> sortedQueenList = queenList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();
        sortedQueenList.RemoveAt(0);

        foreach (GameObject queen in sortedQueenList)
        {
            itemName[i++] = "skin/queen/" + queen.name[..^4];
        }

        GameObject[] beeList = GameObject.FindGameObjectsWithTag("beeSkin");
        List<GameObject> sortedbeeList = beeList.OrderByDescending(go => go.transform.position.y).ThenBy(go => go.transform.position.x).ToList();
        sortedbeeList.RemoveAt(0);

        foreach (GameObject bee in sortedbeeList)
        {
            itemName[i++] = "skin/bee/" + bee.name[..^4];
        }

        UnlockItemSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UnlockItemSetting()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] > 0)
            {
                GameObject itemUnlock = Instantiate(prefabItemUnlock, new Vector2(spotX, spotY), Quaternion.identity);
                itemUnlock.transform.parent = itemScreen.transform;
                itemUnlock.transform.localPosition = new Vector2(spotX, spotY);

                spotX += 1.7f;
                if (spotX > 1.7f)
                {
                    spotX = -1.7f;
                    spotY -= 2;
                }

                itemUnlock.transform.Find("itemImage").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/collect/icon/" + itemName[i]);
                itemUnlock.transform.Find("Canvas").Find("AmountText").GetComponent<Text>().text = itemList[i].ToString();

                string[] abilityText = itemEffect[i].Split("%");

                switch (i)
                {
                    case 4:
                    case 5:
                    case 25:
                        if (itemList[i] < 9)
                        {
                            itemUnlock.transform.Find("Canvas").Find("AbilityText").GetComponent<Text>().text = abilityText[0] + "1 " + abilityText[1];
                        }
                        else
                        {
                            itemUnlock.transform.Find("Canvas").Find("AbilityText").GetComponent<Text>().text = abilityText[0] + ((itemList[i] + 1) / 10) + " " + abilityText[1];
                        }
                        break;
                    default:
                        itemUnlock.transform.Find("Canvas").Find("AbilityText").GetComponent<Text>().text = abilityText[0] + ((float)itemList[i] + 1) / 10 + "% " + abilityText[1];
                        break;
                }
            }
        }
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == 0)
            {
                GameObject itemLock = Instantiate(prefabItemLock, new Vector2(spotX, spotY), Quaternion.identity);
                itemLock.transform.parent = itemScreen.transform;
                itemLock.transform.localPosition = new Vector2(spotX, spotY);

                spotX += 1.7f;
                if (spotX > 1.7f)
                {
                    spotX = -1.7f;
                    spotY -= 2;
                }
            }
        }
    }

    void UpdateItemEffect()
    {
        itemEffect[0] = "여왕벌 채밀 보상\n%증가";
        itemEffect[1] = "일벌 채밀 보상\n%증가";
        itemEffect[2] = "오프라인 시간\n%증가";
        itemEffect[3] = "오프라인 보상\n%증가";
        itemEffect[4] = "최대 벌집 개수\n%증가";
        itemEffect[5] = "벌집 개수\n%증가";
        itemEffect[6] = "광고 시청 보상\n%증가";
        itemEffect[7] = "분봉하기 보상\n%증가";
        itemEffect[8] = "로얄젤리 강화 비용\n%감소";
        itemEffect[9] = "전체 스킨 비용\n%감소";
        itemEffect[10] = "모험하기 시간\n%감소";
        itemEffect[11] = "아이템 획득 확률\n%증가";
        itemEffect[12] = "여왕벌 꿀주머니\n%증가";
        itemEffect[13] = "여왕벌 체력\n%증가";
        itemEffect[14] = "여왕벌 스킨 비용\n%감소";
        itemEffect[15] = "분봉하기 보상\n%증가";
        itemEffect[16] = "오프라인 시간\n%증가";
        itemEffect[17] = "여왕벌 체력 비용\n%감소";
        itemEffect[18] = "여왕벌 채밀 보상\n%증가";
        itemEffect[19] = "여왕벌 꿀주머니 비용\n%감소";    
        itemEffect[20] = "모험하기 시간\n%감소";
        itemEffect[21] = "오프라인 보상\n%증가";
        itemEffect[22] = "일벌 꿀주머니\n%증가";
        itemEffect[23] = "일벌 체력\n%증가";
        itemEffect[24] = "일벌 스킨 가격\n%감소";
        itemEffect[25] = "일벌 수\n%증가";
        itemEffect[26] = "오프라인 시간\n%증가";
        itemEffect[27] = "일벌 체력 비용\n%감소";
        itemEffect[28] = "일벌 채밀 보상\n%증가";
        itemEffect[29] = "일벌 꿀주머니 비용\n%감소";
        itemEffect[30] = "모험하기 시간\n%감소";
        itemEffect[31] = "오프라인 보상\n%증가";
    }
}
