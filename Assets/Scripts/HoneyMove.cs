using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoneyMove : MonoBehaviour
{
    public Vector2 point;   // ²ÜÀÌ ¹ú·Á¼­ µé¾î¿À´Â À§Ä¡

    Text txt;               // ¹ú¾î¿À´Â ²Ü ±Ý¾×

    // Start is called before the first frame update
    void Start()
    {
        txt = transform.GetComponentInChildren<Text>();
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        long gainHoney = gm.upgrade["queenHealth"] + gm.upgrade["queenStorage"] + gm.upgrade["royalQueenHealth"] + gm.upgrade["royalQueenStorage"];
        if (ItemManager.itemList[13] > 0)
        {
            gainHoney += gm.upgrade["queenHealth"] * (ItemManager.itemList[13] + 1) / 1000;
        }
        if (ItemManager.itemList[12] > 0)
        {
            gainHoney += gm.upgrade["queenStorage"] * (ItemManager.itemList[12] + 1) / 1000;
        }
        if (ItemManager.itemList[0] > 0)
        {
            gainHoney += gainHoney * (ItemManager.itemList[0] + 1) / 1000;
        }
        if (ItemManager.itemList[18] > 0)
        {
            gainHoney += gainHoney * (ItemManager.itemList[18] + 1) / 1000;
        }
        txt.text = "+" + DivideNumber(gainHoney);

        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * 10f);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.01f);

        txt = transform.GetComponentInChildren <Text>();
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - 0.01f);
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
