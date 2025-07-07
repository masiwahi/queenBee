using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class beeWork : MonoBehaviour
{
    public static long beeHealth = 0;           // �ܹ�(����) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public static long beeStorage = 1;          // �ܹ�(����) ��������� ������ ���� �� ���� �����ؼ� ��� ����
    public static long beeSpeed = 0;            // �ܹ�(����) �ӵ��� ������ ���� �� ���� ������

    public static long royalBeeHealth = 0;      // �ܹ�(����) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public static long royalBeeStorage = 0;     // �ܹ�(����) ��������� ������ ���� �� ���� �����ؼ� ��� ����

    private Animator anim;
    private bool halftime = true;
    public Vector2 point;
    private Vector3 home;

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

    // beeSpeed�� �ӵ��� ���� ������� �Լ�
    IEnumerator Work()
    {
        while (true)                                                                        // �Ϲ��� �������� ���� �����
        {

            if (GameManager.temperature >= 0)
            {
                GameManager.honey += beeStorage + beeHealth + royalBeeHealth + royalBeeStorage;          //���� �Ŵ����� �ִ� �� �� ������Ʈ
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

            yield return new WaitForSeconds(12f - beeSpeed/5);                              //beeSpeed ��ŭ�� �ð����� �Ͻ����� �� �ٽ� �� �������
        }
    }

    IEnumerator CarryHoney()
    {
        while (true)                                                                        // �Ϲ��� �������� ���� �����
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

            yield return new WaitForSeconds((12f - beeSpeed / 5) / 2);                              //beeSpeed ��ŭ�� �ð����� �Ͻ����� �� �ٽ� �� �������
        }
    }
}
