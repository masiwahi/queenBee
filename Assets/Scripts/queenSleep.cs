using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class queenSleep : MonoBehaviour
{
    private Vector2 home;               // ¿©¿Õ¹ú º¹±Í À§Ä¡


    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.temperature >= 0)
        {
            GameObject prefabQueen = Resources.Load<GameObject>("Prefabs/queen/" + GameManager.Queen);
            home.x += 0.2f;
            home.y += 0.35f;
            Instantiate(prefabQueen, home, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
