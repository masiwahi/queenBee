using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class beeWork : MonoBehaviour
{
    private Animator anim;                      // 일벌 움직이는 애니메이션
    private bool halftime = true;               // 일벌이 꿀 가져오는 시간
    public Vector2 point;                       // 일벌이 꿀 가져오는 위치
    private Vector3 home;                       // 일벌이 집으로 돌아오는 위치
    
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        home = transform.position;
        float spotX = home.x;
        if (home.x < 0)
        {
            spotX = home.x - Random.Range(3, 6);
        }
        else
        {
            spotX = home.x + Random.Range(4, 7);
        }
        float spotY = home.y + Random.Range(1, 4);

        point = new Vector2(spotX, spotY);

        anim = GetComponent<Animator>();
        StartCoroutine(Work());
        StartCoroutine(CarryHoney());
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (home.x - point.x) * (home.x - point.x) + (home.y - point.y) * (home.y - point.y);
        distance = Mathf.Sqrt(distance);
        long dueTime = (12 - gm.upgrade["beeSpeed"] / 5) / 2;

        if (!halftime && GameManager.temperature >= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, distance / dueTime * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, home, distance / dueTime * Time.deltaTime);
        }

        if (transform.position == home && GameManager.temperature < 0)
        {
            GameObject prefabBeeSleep = Resources.Load<GameObject>("Prefabs/bee/beeSleep");
            home.y -= 0.3f;
            home.x -= 0.1f;
            Instantiate(prefabBeeSleep, home, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // 일벌 속도 만큼 꿀을 벌어오는 함수
    IEnumerator Work()
    {
        while (true)
        {

            if (GameManager.temperature >= 0)
            {
                long gainHoney = gm.upgrade["beeHealth"] + gm.upgrade["beeStorage"] + gm.upgrade["royalBeeHealth"] + gm.upgrade["royalBeeStorage"];
                if (ItemManager.itemList[23] > 0)
                {
                    gainHoney += gm.upgrade["beeHealth"] * (ItemManager.itemList[23] + 1) / 1000;
                }
                if (ItemManager.itemList[22] > 0)
                {
                    gainHoney += gm.upgrade["beeStorage"] * (ItemManager.itemList[22] + 1) / 1000;
                }
                if (ItemManager.itemList[1] > 0)
                {
                    gainHoney += gainHoney * (ItemManager.itemList[1] + 1) / 1000;
                }
                if (ItemManager.itemList[28] > 0)
                {
                    gainHoney += gainHoney * (ItemManager.itemList[28] + 1) / 1000;
                }

                GameManager.honey += gainHoney;
            }

            float spotX = home.x;
            if (home.x < 0)
            {
                spotX = home.x - Random.Range(3, 6);
            }
            else
            {
                spotX = home.x + Random.Range(4, 7);
            }
            float spotY = home.y + Random.Range(1, 4);
            point = new Vector2(spotX, spotY);

            yield return new WaitForSeconds(12f - gm.upgrade["beeSpeed"] / 5);
        }
    }

    // 일정 시간마다 꿀 가져오는 애니메이션 적용
    IEnumerator CarryHoney()
    {
        while (true)
        {
            if (!halftime )
            {
                halftime = true;
                anim.SetBool("halftime", true);
            }
            else
            {
                halftime = false;
                anim.SetBool("halftime", false);
            }

            yield return new WaitForSeconds((12f - gm.upgrade["beeSpeed"] / 5) / 2);
        }
    }
}
