using UnityEditor;
using UnityEngine;

public class queenWork : MonoBehaviour
{
    private Animator anim;              // 여왕벌 애니메이션 효과
    private bool isClicked = false;     // 화면 클릭 여부
    private Vector3 home;               // 여왕벌 복귀 위치

    private float lastClickTime;        // 마지막 클릭 시간
    private float comebackTime = 0.5f;  // 마지막 클릭 시간 후 여왕벌 복귀까지 남은 시간

    float randX;                        // 여왕벌이 날아갈 랜덤 X 좌표
    float randY;                        // 여왕벌이 날아갈 래덤 Y 좌표

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
            GameObject prefabQueen = Resources.Load<GameObject>("Prefabs/queen/queenSleep");
            home.x -= 0.2f;
            home.y -= 0.35f;
            Instantiate(prefabQueen, home, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // 클릭 중단 시 애니메이션 초기화
    void ResetAnimation()
    {
        isClicked = false;
        anim.SetBool("isClicked", false);
    }
}
