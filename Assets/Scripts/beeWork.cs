using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class beeWork : MonoBehaviour
{
    public static long beeHealth = 0;           // 일벌(오토) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public static long beeStorage = 1;          // 일벌(오토) 저장공간이 많으면 꿀을 더 많이 저장해서 운반 가능
    public static long beeSpeed = 0;            // 일벌(오토) 속도가 오르면 꿀을 더 빨리 가져옴

    public static long royalBeeHealth = 0;      // 일벌(오토) 체력이 높으면 더 멀리 날아가서 많은 꽃을 운반 가능
    public static long royalBeeStorage = 0;     // 일벌(오토) 저장공간이 많으면 꿀을 더 많이 저장해서 운반 가능

    private Animator anim;                      // 일벌 움직이는 애니메이션
    private bool halftime = true;               // 일벌이 꿀 가져오는 시간
    public Vector2 point;                       // 일벌이 꿀 가져오는 위치
    private Vector3 home;                       // 일벌이 집으로 돌아오는 위치

    // Start is called before the first frame update
    void Start()
    {
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
        long dueTime = (12 - beeSpeed / 5) / 2;

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
            GameObject prefabBeeSleep = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/beeSleep.prefab", typeof(GameObject));
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
                GameManager.honey += beeStorage + beeHealth + royalBeeHealth + royalBeeStorage;
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

            yield return new WaitForSeconds(12f - beeSpeed/5);
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

            yield return new WaitForSeconds((12f - beeSpeed / 5) / 2);
        }
    }
}
