using UnityEditor;
using UnityEngine;

public class queenWork : MonoBehaviour
{
    private Animator anim;
    private bool isClicked = false;
    private Vector3 home;

    private float lastClickTime;
    private float comebackTime = 0.5f;

    private GameObject[] UpgradePanel;

    float randX;
    float randY;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        home = transform.position;
        randX = Random.Range(-2.2f, 1.6f);
        randY = Random.Range(0.8f, 3.6f);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (!isClicked)
                {
                    isClicked = true;
                    anim.SetBool("isClicked", true);
                    
                }

                lastClickTime = Time.time;

                UpgradePanel = GameObject.FindGameObjectsWithTag("upgradePanel");
                for (int i = 0; i < UpgradePanel.Length; i++)
                {
                    UpgradePanel[i].SetActive(false);
                }
            }
        }
        if (isClicked)
        {
            while (transform.position == new Vector3(randX, randY, 0))
            {
                randX = Random.Range(-2.2f, 1.6f);
                randY = Random.Range(0.8f, 3.6f);
            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(randX, randY), 1f * Time.deltaTime);
        }
        if(Time.time - lastClickTime > comebackTime)
        {
            isClicked = false;
            transform.position = Vector2.MoveTowards(transform.position, home, 1f * Time.deltaTime);
        }

        if (transform.position == home)
        {
            Invoke("ResetAnimation",0f);
        }
        if (GameManager.temperature < 0)
        {
            GameObject prefabQueen = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/queen/queenSleep.prefab", typeof(GameObject));
            home.x -= 0.2f;
            home.y -= 0.35f;
            Instantiate(prefabQueen, home, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void ResetAnimation()
    {
        isClicked = false;
        anim.SetBool("isClicked", false);
    }
}
