using System.Collections;
using UnityEditor;
using UnityEngine;

public class beeSleep : MonoBehaviour
{
    private bool beeBool = true;    // 잠에서 꺠어났는지 확인
    private Vector2 home;           // 일벌 위치 화긴

    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.temperature >= 0 && beeBool)
        {
            StartCoroutine(BeeAwake());
            beeBool = false;
        }
    }

    // 서로 다른 타이밍에 일벌 기상
    IEnumerator BeeAwake()
    {
        int rand = Random.Range(0, 11);
        yield return new WaitForSeconds(rand / 2f);

        GameObject prefabBee = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/" + GameManager.Bee + ".prefab", typeof(GameObject));
        home.y += 0.3f;
        home.x += 0.1f;
        Instantiate(prefabBee, home, Quaternion.identity);
        Destroy(gameObject);

        yield break;

    }
}
