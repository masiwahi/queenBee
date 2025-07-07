using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class queenSleep : MonoBehaviour
{
    private Vector2 home;
    private GameObject[] UpgradePanel;


    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
            {
                UpgradePanel = GameObject.FindGameObjectsWithTag("upgradePanel");
                for (int i = 0; i < UpgradePanel.Length; i++)
                {
                    UpgradePanel[i].SetActive(false);
                }
            }
        }
        if (GameManager.temperature >= 0)
        {
            GameObject prefabQueen = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/queen/" + GameManager.Queen + ".prefab", typeof(GameObject));
            home.x += 0.2f;
            home.y += 0.35f;
            Instantiate(prefabQueen, home, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
