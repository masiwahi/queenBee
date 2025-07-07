using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static long royalJelly = 1000000;             // ȯ��(�к�) ��ȭ
    public static long honey = 10000000;                  // �⺻ ��ȭ

    public static float temperature = 1f;             // �µ�(�µ��� ���� Ȱ���Ⱑ ����)
    public float temperatureChange = 0.5f;
    public static float minTemp = -5f;
    public static float maxTemp = 15f;

    public long queenHealth;            // ����(Ŭ��Ŀ) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public long queenHealthPrice;       // ����(Ŭ��Ŀ) ü���� ��ȭ ���
    public long queenStorage;           // ����(Ŭ��Ŀ) ���ָӴ�(�������)�� ������ ���� �� ���� �����ؼ� ��� ����
    public long queenStoragePrice;      // ����(Ŭ��Ŀ) ���ָӴ�(�������) ��ȭ ���

    public long beeHealthPrice;         // �Ϲ�(����) ü���� ��ȭ ���
    public long beeStoragePrice;        // �Ϲ�(����) ���ָӴ�(�������) ��ȭ ���
    public long beeSpeedPrice;          // �Ϲ�(����) ���ָӴ�(�������) ��ȭ ���

    public long beeCount;               // �ܹ�(����) ���� ��
    public long beeCountPrice;          // �ܹ�(����) ���� ����
    public long honeyComb;              // �ܹ��� ���� �� �ִ� ����(����)
    public long honeyCombPrice;         // �ܹ��� ���� �� �ִ� ����(����) ���� ����

    public int honeyCombWidth;          // ���� ���� �ִ� ��ġ ��
    public float honeyCombX;            // ���� ���� ����
    public float honeyCombY;            // ���� ���� ����

    public long royalQueenHealth;            // ����(Ŭ��Ŀ) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public long royalQueenHealthPrice;       // ����(Ŭ��Ŀ) ü���� ��ȭ ���
    public long royalQueenStorage;           // ����(Ŭ��Ŀ) ���ָӴ�(�������)�� ������ ���� �� ���� �����ؼ� ��� ����
    public long royalQueenStoragePrice;      // ����(Ŭ��Ŀ) ���ָӴ�(�������) ��ȭ ���
    public long royalBeeHealthPrice;         // �Ϲ�(����) ü���� ��ȭ ���
    public long royalBeeStoragePrice;        // �Ϲ�(����) ���ָӴ�(�������) ��ȭ ���

    public Text HoneyText;              // �ܹ� �� üũ�ϴ� �۾�
    public Text RoyalJellyText;         // �ξ����� �� Ȯ���ϴ� �۾�
    public Text temperatureText;

    public Text queenHealthText;        // ���չ� ü�� ��ȭȮ�� �۾�
    public Text queenStorageText;       // ���չ� ���ָӴ� ��ȭȮ�� �۾�
    public Text honeyCombText;          // ���� ��ȭȮ�� �۾�
    public Text queenHealthLevel;        // ���չ� ü�� ��ȭȮ�� �۾�
    public Text queenStorageLevel;       // ���չ� ���ָӴ� ��ȭȮ�� �۾�
    public Text honeyCombLevel;          // ���� ��ȭȮ�� �۾�

    public Text beeHealthText;          // �Ϲ� ü�� ��ȭȮ�� �۾�
    public Text beeStorageText;         // �Ϲ� ���ָӴ� ��ȭȮ�� �۾�
    public Text beeSpeedText;           // �Ϲ� ���ָӴ� ��ȭȮ�� �۾�
    public Text beeCountText;           // �Ϲ� �� ��ȭȮ�� �۾�
    public Text beeHealthLevel;          // �Ϲ� ü�� ��ȭȮ�� �۾�
    public Text beeStorageLevel;         // �Ϲ� ���ָӴ� ��ȭȮ�� �۾�
    public Text beeSpeedLevel;           // �Ϲ� ���ָӴ� ��ȭȮ�� �۾�
    public Text beeCountLevel;           // �Ϲ� �� ��ȭȮ�� �۾�

    public Text royalQueenHealthText;        // ���չ� ü�� ��ȭȮ�� �۾�
    public Text royalQueenStorageText;       // ���չ� ���ָӴ� ��ȭȮ�� �۾�
    public Text royalBeeHealthText;          // �Ϲ� ü�� ��ȭȮ�� �۾�
    public Text royalBeeStorageText;         // �Ϲ� ���ָӴ� ��ȭȮ�� �۾�
    public Text royalQueenHealthLevel;        // ���չ� ü�� ��ȭȮ�� �۾�
    public Text royalQueenStorageLevel;       // ���չ� ���ָӴ� ��ȭȮ�� �۾�
    public Text royalBeeHealthLevel;          // �Ϲ� ü�� ��ȭȮ�� �۾�
    public Text royalBeeStorageLevel;         // �Ϲ� ���ָӴ� ��ȭȮ�� �۾�

    public Button queenHealthBtn;       // ���չ� ü�� ��ȭ ��ư
    public Button queenStorageBtn;      // ���չ� ���ָӴ� ��ȭ ��ư
    public Button honeyCombBtn;         // ���� ��ȭ ��ư
    public Button swarmingBtn;

    public Button beeHealthBtn;         // �Ϲ� ü�� ��ȭ ��ư
    public Button beeStorageBtn;        // �Ϲ� ���ָӴ� ��ȭ ��ư
    public Button beeSpeedBtn;          // �Ϲ� ���ָӴ� ��ȭ ��ư
    public Button beeCountBtn;          // �Ϲ� �� ��ȭ ��ư

    public Button royalQueenHealthBtn;       // ���չ� ü�� ��ȭ ��ư
    public Button royalQueenStorageBtn;      // ���չ� ���ָӴ� ��ȭ ��ư
    public Button royalBeeHealthBtn;         // �Ϲ� ü�� ��ȭ ��ư
    public Button royalBeeStorageBtn;        // �Ϲ� ���ָӴ� ��ȭ ��ư

    public GameObject prefabHoney;      // Ŭ���� ���� �þ�� ���
    public GameObject prefabHoneyComb;  // ���� ���Ž� �����ϴ� ����
    public GameObject prefabHoneyCombBG;

    public static string Queen = "queenBee";
    public static string Bee = "beeBasic";

    public static string Map = "Basic";

    public Image queenSelectSkinImage;
    public Image beeSelectSkinImage;
    public GameObject backgroundImage;
    public GameObject hiveImage;

    private int upgradeGraph = 17;
    private int combGraph = 57;
    private int beeGraph = 37;
    private int speedGraph = 137;

    private Vector2 queenSpot;

    // Start is called before the first frame update
    void Start()
    {
        queenSpot = GameObject.FindGameObjectWithTag("queen").transform.position;
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path))
        {
            Load();
            FillHoneyComb();
            FillBee();
        }
        else
        {

        }
        FillHoneyCombBG();
        SettingSkin();
        MapSelect();
        QueenSkinSelect();
        StartCoroutine(TemperatureChangeWork());
    }

    // Update is called once per frame
    void Update()
    {
        ShowHoney();

        UpdateQueenHealthText();
        UpdateQueenStorageText();
        UpdateHoneyCombText();

        QueenHealthButtonActiveCheck();
        QueenStorageButtonActiveCheck();
        HoneyCombButtonActiveCheck();
        SwarmingButtonActiveCheck();

        UpdateBeeHealthText();
        UpdateBeeStorageText();
        UpdateBeeSpeedText();
        UpdateBeeCountText();

        BeeHealthButtonActiveCheck();
        BeeStorageButtonActiveCheck();
        BeeSpeedButtonActiveCheck();
        BeeCountButtonActiveCheck();

        UpdateRoyalQueenHealthText();
        UpdateRoyalQueenStorageText();
        UpdateRoyalBeeHealthText();
        UpdateRoyalBeeStorageText();

        RoyalQueenHealthButtonActiveCheck();
        RoyalQueenStorageButtonActiveCheck();
        RoyalBeeHealthButtonActiveCheck();
        RoyalBeeStorageButtonActiveCheck();

        HoneyIncrease();
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    IEnumerator TemperatureChangeWork()
    {
        while (true)
        {
            if (temperature <= minTemp)
            {
                temperatureChange = 0.5f;
            }
            else if (temperature >= maxTemp)
            {
                temperatureChange = -0.5f;
            }
            temperature += temperatureChange;

            yield return new WaitForSeconds(5f);
        }
    }

    // ȭ�� Ŭ�� �� �� ȹ�� (���չ��� ���� ���� ��)
    void HoneyIncrease()
    {
        if (Input.GetMouseButtonDown(0))                                                         // ȭ���� ��ġ�ߴ��� Ȯ��
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if (temperature >= 0 && EventSystem.current.IsPointerOverGameObject() == false)
                {
                    honey += queenHealth + queenStorage + royalQueenHealth + royalQueenStorage;     // ��ȭ�� ���չ� ���忡 ���� �� ȹ��

                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // ���콺 Ŭ���� �κ��� ���� �� ��ġ ��������
                    Instantiate(prefabHoney, mousePosition, Quaternion.identity);                   // �� ȹ�� �̹��� ����
                }
            }
        }
    }

    // ��� ��ȭ ǥ�� ������Ʈ
    void ShowHoney()
    {
        if (honey == 0)                                                     // ������ �� ������ 0������ Ȯ��
        {
            HoneyText.text = "0";                                         // �� 0 ���� ǥ��
        }
        else
        {
            HoneyText.text = DivideNumber(honey);              // ������ �� ���� ǥ��
        }

        if (royalJelly == 0)                                                // ������ �ξ����� ������ 0������ Ȯ��
        {
            RoyalJellyText.text = "0";                                    // �ξ����� 0 ���� ǥ��
        }
        else
        {
            RoyalJellyText.text = DivideNumber(royalJelly);    // ������ �ξ����� ���� ǥ��
        }
        temperatureText.text = temperature + "��";
    }

    // ���չ� ü�� ���� ǥ�� ������Ʈ
    void UpdateQueenHealthText()
    {
        queenHealthLevel.text = "Lv." + queenHealth;             // ���չ� ü�� ��ȭ�� ǥ��
        queenHealthText.text = DivideNumber(queenHealthPrice);   // ���չ� ü�� ��ȭ ���� ǥ�� ����
    }

    // ���չ� ü�� ��ȭ ����
    public void UpgradeQueenHealth()
    {
        if (honey >= queenHealthPrice)              // ���չ� ü�� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honey -= queenHealthPrice;              // ���չ� ü�� ��ȭ �ݾ׸�ŭ �� ����
            queenHealth += 1;                       // ���չ� ü�� ��ȭ
            queenHealthPrice += queenHealth * upgradeGraph;    // ���չ� ü�� ��ȭ ��� ����
        }
    }

    // ���չ� ü�� ��ư Ȱ��ȭ
    void QueenHealthButtonActiveCheck()
    {
        if (honey >= queenHealthPrice)              // ���չ� ü�� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            queenHealthBtn.interactable = true;     // ���չ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            queenHealthBtn.interactable = false;    // ���չ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // ���չ� ���ָӴ� ���� ǥ�� ������Ʈ
    void UpdateQueenStorageText()
    {
        queenStorageLevel.text = "Lv." + queenStorage;             // ���չ� ���ָӴ� ��ȭ�� ǥ��
        queenStorageText.text = DivideNumber(queenStoragePrice);   // ���չ� ���ָӴ� ��ȭ ���� ǥ�� ����
    }

    // ���չ� ���ָӴ� ��ȭ ����
    public void UpgradeQueenStorage()
    {
        if (honey >= queenStoragePrice)              // ���չ� ���ָӴ� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honey -= queenStoragePrice;              // ���չ� ���ָӴ� ��ȭ �ݾ׸�ŭ �� ����
            queenStorage += 1;                       // ���չ� ���ָӴ� ��ȭ
            queenStoragePrice += queenStorage * upgradeGraph;    // ���չ� ���ָӴ� ��ȭ ��� ����
        }
    }

    // ���չ� ���ָӴ� ��ư Ȱ��ȭ
    void QueenStorageButtonActiveCheck()
    {
        if (honey >= queenStoragePrice)              // ���չ� ���ָӴ� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            queenStorageBtn.interactable = true;     // ���չ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            queenStorageBtn.interactable = false;    // ���չ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // ���� ���� ǥ�� ������Ʈ
    void UpdateHoneyCombText()
    {
        honeyCombLevel.text = "Lv." + honeyComb;             // ���� ��ȭ�� ǥ��
        honeyCombText.text = DivideNumber(honeyCombPrice);   // ���� ��ȭ ���� ǥ�� ����
    }

    // ���� ��ȭ ����
    public void UpgradeHoneyComb()
    {
        if (honey >= honeyCombPrice)            // ���� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honey -= honeyCombPrice;            // ���� ��ȭ �ݾ׸�ŭ �� ����
            honeyComb += 1;                     // ���� ��ȭ
            honeyCombPrice += honeyComb * combGraph;   // ���� ��ȭ ��� ����

            CreateHoneyComb();                  //���� ����
            if(honeyComb%5 == 0 && honeyComb > 14)
            {
                CreateHoneyCombBG();
            }
        }
    }

    // ���� ��ȭ ��ư Ȱ��ȭ
    void HoneyCombButtonActiveCheck()
    {
        if (honey >= honeyCombPrice)              // ���� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honeyCombBtn.interactable = true;     // ���� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            honeyCombBtn.interactable = false;    // ���� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // ���� ����
    void CreateHoneyComb()
    {
        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;                                                                        // �ʱ� ���� X, Y ��ǥ�� ��������
        float spotX = honeyCombSpot.x + ((honeyComb - 1) % honeyCombWidth) * honeyCombX;                                                                // ������ ���� X ��ǥ�� ���
        float spotY = honeyCombSpot.y - ((honeyComb - 1) / honeyCombWidth) * honeyCombY - (((honeyComb - 1) % honeyCombWidth) % 2) * (honeyCombY / 2);  // ������ ���� Y ��ǥ�� ���

        Instantiate(prefabHoneyComb, new Vector2(spotX, spotY), Quaternion.identity);                                                                   // �����س��� ���� �̹��� ����
    }

    void CreateHoneyCombBG()
    {
        Vector2 honeyCombBGSpot = GameObject.Find("honeycombBackground").transform.position;
        float spotX = honeyCombBGSpot.x;
        float spotY = honeyCombBGSpot.y - (1.2f * (honeyComb / 5));

        Instantiate(prefabHoneyCombBG, new Vector2(spotX, spotY), Quaternion.identity);

        if (honeyComb / 5 > 2)
        {
            mainCameraDrag.mainLimitMinY -= 1.2f;
            mainCameraDrag.limitMinY = mainCameraDrag.mainLimitMinY;
        }
    }

    // �Ϲ� ü�� ���� ǥ�� ������Ʈ
    void UpdateBeeHealthText()
    {
        beeHealthLevel.text = "Lv." + beeWork.beeHealth;             // �Ϲ� ü�� ��ȭ�� ǥ��
        beeHealthText.text = DivideNumber(beeHealthPrice);   // �Ϲ� ü�� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �Ϲ� ü�� ��ȭ ����
    public void UpgradeBeeHealth()
    {
        if (honey >= beeHealthPrice)              // �Ϲ� ü�� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honey -= beeHealthPrice;              // �Ϲ� ü�� ��ȭ �ݾ׸�ŭ �� ����
            beeWork.beeHealth += 1;                       // �Ϲ� ü�� ��ȭ
            beeHealthPrice += beeWork.beeHealth * upgradeGraph;    // �Ϲ� ü�� ��ȭ ��� ����
        }
    }

    // �Ϲ� ü�� ��ư Ȱ��ȭ
    void BeeHealthButtonActiveCheck()
    {
        if (honey >= beeHealthPrice)              // �Ϲ� ü�� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            beeHealthBtn.interactable = true;     // �Ϲ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            beeHealthBtn.interactable = false;    // �Ϲ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // �Ϲ� ���ָӴ� ���� ǥ�� ������Ʈ
    void UpdateBeeStorageText()
    {
        beeStorageLevel.text = "Lv." + beeWork.beeStorage;             // �Ϲ� ���ָӴ� ��ȭ�� ǥ��
        beeStorageText.text = DivideNumber(beeStoragePrice);   // �Ϲ� ���ָӴ� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �Ϲ� ���ָӴ� ��ȭ ����
    public void UpgradeBeeStorage()
    {
        if (honey >= beeStoragePrice)              // �Ϲ� ���ָӴ� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honey -= beeStoragePrice;              // �Ϲ� ���ָӴ� ��ȭ �ݾ׸�ŭ �� ����
            beeWork.beeStorage += 1;                       // �Ϲ� ���ָӴ� ��ȭ
            beeStoragePrice += beeWork.beeStorage * upgradeGraph;    // �Ϲ� ���ָӴ� ��ȭ ��� ����
        }
    }

    // �Ϲ� ���ָӴ� ��ư Ȱ��ȭ
    void BeeStorageButtonActiveCheck()
    {
        if (honey >= beeStoragePrice)              // �Ϲ� ���ָӴ� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            beeStorageBtn.interactable = true;     // �Ϲ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            beeStorageBtn.interactable = false;    // �Ϲ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }
    
    // �Ϲ� �ӵ� ���� ǥ�� ������Ʈ
    void UpdateBeeSpeedText()
    {
        beeSpeedLevel.text = "Lv." + beeWork.beeSpeed;             // �Ϲ� �ӵ� ��ȭ�� ǥ��
        beeSpeedText.text = DivideNumber(beeSpeedPrice);   // �Ϲ� �ӵ� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �Ϲ� �ӵ� ��ȭ ����
    public void UpgradeBeeSpeed()
    {
        if (honey >= beeSpeedPrice)              // �Ϲ� �ӵ� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honey -= beeSpeedPrice;              // �Ϲ� �ӵ� ��ȭ �ݾ׸�ŭ �� ����
            beeWork.beeSpeed += 1;                       // �Ϲ� �ӵ� ��ȭ
            beeSpeedPrice += beeWork.beeSpeed * speedGraph;    // �Ϲ� �ӵ� ��ȭ ��� ����
        }
    }

    // �Ϲ� �ӵ� ��ư Ȱ��ȭ
    void BeeSpeedButtonActiveCheck()
    {
        if (honey >= beeSpeedPrice && beeWork.beeSpeed < 50)              // �Ϲ� �ӵ� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            beeSpeedBtn.interactable = true;     // �Ϲ� �ӵ� ��ư Ȱ��ȭ
        }
        else
        {
            beeSpeedBtn.interactable = false;    // �Ϲ� �ӵ� ��ư ��Ȱ��ȭ
        }
    }

    // �Ϲ� �� ���� ǥ�� ������Ʈ
    void UpdateBeeCountText()
    {
        beeCountLevel.text = "Lv." + beeCount;                 // �Ϲ� �� ��ȭ�� ǥ��
        beeCountText.text = DivideNumber(beeCountPrice);     // �Ϲ� �� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �Ϲ� �� ��ȭ ����
    public void UpgradeBeeCount()
    {
        if (honey >= beeCountPrice && beeCount < honeyComb)            // �Ϲ� �� ��ȭ�� ���� ����� �ִ��� Ȯ��
        {
            honey -= beeCountPrice;            // �Ϲ� �� ��ȭ �ݾ׸�ŭ �� ����
            beeCount += 1;                     // �Ϲ� �� ��ȭ
            beeCountPrice += beeCount * beeGraph;   // �Ϲ� �� ��ȭ ��� ����

            CreateBeeCount();                  //�Ϲ� ����
        }
    }

    // �Ϲ� �� ���� ��ư Ȱ��ȭ
    void BeeCountButtonActiveCheck()
    {
        if (honey >= beeCountPrice && beeCount < honeyComb)              // �Ϲ� �� ��ȭ�� ���� ����� �ִ����� �Ϲ��� ������ ������ �ִ��� Ȯ��
        {
            beeCountBtn.interactable = true;     // �Ϲ� �� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            beeCountBtn.interactable = false;    // �Ϲ� �� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // �Ϲ� ����
    void CreateBeeCount()
    {
        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;                                                                        // �ʱ� ���� X, Y ��ǥ�� ��������
        float spotX = honeyCombSpot.x + ((beeCount - 1) % honeyCombWidth) * honeyCombX;                                                                 // ������ �Ϲ� X ��ǥ�� ���
        float spotY = honeyCombSpot.y - ((beeCount - 1) / honeyCombWidth) * honeyCombY - (((beeCount - 1) % honeyCombWidth) % 2) * (honeyCombY / 2);    // ������ �Ϲ� Y ��ǥ�� ���


        GameObject prefabBee = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/" + Bee + ".prefab", typeof(GameObject));
        Instantiate(prefabBee, new Vector2(spotX, spotY), Quaternion.identity);                                                                         // �����س��� �Ϲ� �̹��� ����
    }

    // �ξ����� ���չ� ü�� ���� ǥ�� ������Ʈ
    void UpdateRoyalQueenHealthText()
    {
        royalQueenHealthLevel.text = "Lv." + royalQueenHealth;             // ���չ� ü�� ��ȭ�� ǥ��
        royalQueenHealthText.text = DivideNumber(royalQueenHealthPrice);   // ���չ� ü�� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �ξ����� ���չ� ü�� ��ȭ ����
    public void UpgradeRoyalQueenHealth()
    {
        if (royalJelly >= royalQueenHealthPrice)              // ���չ� ü�� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalJelly -= royalQueenHealthPrice;              // ���չ� ü�� ��ȭ �ݾ׸�ŭ �� ����
            royalQueenHealth += 1;                       // ���չ� ü�� ��ȭ
            royalQueenHealthPrice += royalQueenHealth * upgradeGraph;    // ���չ� ü�� ��ȭ ��� ����
        }
    }

    // �ξ����� ���չ� ü�� ��ư Ȱ��ȭ
    void RoyalQueenHealthButtonActiveCheck()
    {
        if (royalJelly >= royalQueenHealthPrice)              // ���չ� ü�� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalQueenHealthBtn.interactable = true;     // ���չ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            royalQueenHealthBtn.interactable = false;    // ���չ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // �ξ����� ���չ� ���ָӴ� ���� ǥ�� ������Ʈ
    void UpdateRoyalQueenStorageText()
    {
        royalQueenStorageLevel.text = "Lv." + royalQueenStorage;             // ���չ� ���ָӴ� ��ȭ�� ǥ��
        royalQueenStorageText.text = DivideNumber(royalQueenStoragePrice);   // ���չ� ���ָӴ� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �ξ����� ���չ� ���ָӴ� ��ȭ ����
    public void UpgradeRoyalQueenStorage()
    {
        if (royalJelly >= royalQueenStoragePrice)              // ���չ� ���ָӴ� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalJelly -= royalQueenStoragePrice;              // ���չ� ���ָӴ� ��ȭ �ݾ׸�ŭ �� ����
            royalQueenStorage += 1;                       // ���չ� ���ָӴ� ��ȭ
            royalQueenStoragePrice += royalQueenStorage * upgradeGraph;    // ���չ� ���ָӴ� ��ȭ ��� ����
        }
    }

    // �ξ����� ���չ� ���ָӴ� ��ư Ȱ��ȭ
    void  RoyalQueenStorageButtonActiveCheck()
    {
        if (royalJelly >= royalQueenStoragePrice)              // ���չ� ���ָӴ� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalQueenStorageBtn.interactable = true;     // ���չ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            royalQueenStorageBtn.interactable = false;    // ���չ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // �ξ����� �Ϲ� ü�� ���� ǥ�� ������Ʈ
    void UpdateRoyalBeeHealthText()
    {
        royalBeeHealthLevel.text = "Lv." + beeWork.royalBeeHealth;             // �Ϲ� ü�� ��ȭ�� ǥ��
        royalBeeHealthText.text = DivideNumber(royalBeeHealthPrice);   // �Ϲ� ü�� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �ξ����� �Ϲ� ü�� ��ȭ ����
    public void UpgradeRoyalBeeHealth()
    {
        if (royalJelly >= royalBeeHealthPrice)              // �Ϲ� ü�� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalJelly -= royalBeeHealthPrice;              // �Ϲ� ü�� ��ȭ �ݾ׸�ŭ �� ����
            beeWork.royalBeeHealth += 1;                       // �Ϲ� ü�� ��ȭ
            royalBeeHealthPrice += beeWork.royalBeeHealth * upgradeGraph;    // �Ϲ� ü�� ��ȭ ��� ����
        }
    }

    // �ξ����� �Ϲ� ü�� ��ư Ȱ��ȭ
    void RoyalBeeHealthButtonActiveCheck()
    {
        if (royalJelly >= royalBeeHealthPrice)              // �Ϲ� ü�� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalBeeHealthBtn.interactable = true;     // �Ϲ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            royalBeeHealthBtn.interactable = false;    // �Ϲ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // �ξ����� �Ϲ� ���ָӴ� ���� ǥ�� ������Ʈ
    void UpdateRoyalBeeStorageText()
    {
        royalBeeStorageLevel.text = "Lv." + beeWork.royalBeeStorage;             // �Ϲ� ���ָӴ� ��ȭ�� ǥ��
        royalBeeStorageText.text = DivideNumber(royalBeeStoragePrice);   // �Ϲ� ���ָӴ� ��ȭ ���� ǥ�� ���� + �ޱ���
    }

    // �ξ����� �Ϲ� ���ָӴ� ��ȭ ����
    public void UpgradeRoyalBeeStorage()
    {
        if (royalJelly >= royalBeeStoragePrice)              // �Ϲ� ���ָӴ� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalJelly -= royalBeeStoragePrice;              // �Ϲ� ���ָӴ� ��ȭ �ݾ׸�ŭ �� ����
            beeWork.royalBeeStorage += 1;                       // �Ϲ� ���ָӴ� ��ȭ
            royalBeeStoragePrice += beeWork.royalBeeStorage * upgradeGraph;    // �Ϲ� ���ָӴ� ��ȭ ��� ����
        }
    }

    // �ξ����� �Ϲ� ���ָӴ� ��ư Ȱ��ȭ
    void RoyalBeeStorageButtonActiveCheck()
    {
        if (royalJelly >= royalBeeStoragePrice)              // �Ϲ� ���ָӴ� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            royalBeeStorageBtn.interactable = true;     // �Ϲ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            royalBeeStorageBtn.interactable = false;    // �Ϲ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    void SwarmingButtonActiveCheck()
    {
        if (queenHealth + queenStorage + beeWork.beeHealth + beeWork.beeStorage >= 100)              // �Ϲ� ���ָӴ� ��ȭ�� �ξ������� ����� �ִ��� Ȯ��
        {
            swarmingBtn.interactable = true;     // �Ϲ� ��ȭ�ϱ� ��ư Ȱ��ȭ
        }
        else
        {
            swarmingBtn.interactable = false;    // �Ϲ� ��ȭ�ϱ� ��ư ��Ȱ��ȭ
        }
    }

    // �к� ��� ��ȭ ������ �ʱ�ȭ �ϰ� �ξ������� ����
    public void Swarming()
    {
        honey = 0;

        if (queenHealth + queenStorage + beeWork.beeHealth + beeWork.beeStorage >= 100)
        {
            royalJelly += (queenHealth + queenStorage + beeWork.beeHealth + beeWork.beeStorage - 89) / 10;
        }

        queenHealth = 0;
        queenHealthPrice = 5;
        queenStorage = 1;
        queenStoragePrice = 5;
        honeyComb = 1;
        honeyCombPrice = 5;

        beeWork.beeHealth = 0;
        beeHealthPrice = 5;
        beeWork.beeStorage = 1;
        beeStoragePrice = 5;
        beeWork.beeSpeed = 0;
        beeSpeedPrice = 5;
        beeCount = 0;
        beeCountPrice = 5;

        GameObject[] bees = GameObject.FindGameObjectsWithTag("bee");
        for(int i = 0; i < bees.Length; i++)
        {
            Destroy(bees[i]);
        }

        GameObject[] honeycombs = GameObject.FindGameObjectsWithTag("honeycomb");
        for (int j = 1; j < honeycombs.Length; j++)
        {
            Destroy(honeycombs[j]);
        }
    }

    void Save()
    {
        SaveData saveData = new SaveData();

        saveData.royalJelly = royalJelly;
        saveData.honey = honey;

        saveData.queenHealth = queenHealth;
        saveData.queenHealthPrice = queenHealthPrice;
        saveData.queenStorage = queenStorage;
        saveData.queenStoragePrice = queenStoragePrice;

        saveData.beeHealthPrice = beeHealthPrice;
        saveData.beeStoragePrice = beeStoragePrice;
        saveData.beeSpeedPrice = beeSpeedPrice;

        saveData.beeCount = beeCount;
        saveData.beeCountPrice = beeCountPrice;
        saveData.honeyComb = honeyComb;
        saveData.honeyCombPrice = honeyCombPrice;

        saveData.royalQueenHealth = royalQueenHealth;
        saveData.royalQueenHealthPrice = royalQueenHealthPrice;
        saveData.royalQueenStorage = royalQueenStorage;
        saveData.royalQueenStoragePrice = royalQueenStoragePrice;
        saveData.royalBeeHealthPrice = royalBeeHealthPrice;
        saveData.royalBeeStoragePrice = royalBeeStoragePrice;

        saveData.beeHealth = beeWork.beeHealth;
        saveData.beeStorage = beeWork.beeStorage;
        saveData.beeSpeed = beeWork.beeSpeed;

        saveData.royalBeeHealth = beeWork.royalBeeHealth;
        saveData.royalBeeStorage = beeWork.royalBeeStorage;

        saveData.Queen = Queen;
        saveData.Bee = Bee;

        saveData.Map = Map;

        saveData.queenSkinList = SkinManager.queenSkinList;
        saveData.beeSkinList = SkinManager.beeSkinList;
        saveData.mapSkinList = MapManager.mapSkinList;

        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData, path);
    }

    void Load()
    {
        SaveData saveData = new SaveData();

        string path = Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<SaveData>(path);

        royalJelly = saveData.royalJelly;
        honey = saveData.honey;

        queenHealth = saveData.queenHealth;
        queenHealthPrice = saveData.queenHealthPrice;
        queenStorage = saveData.queenStorage;
        queenStoragePrice = saveData.queenStoragePrice;

        beeHealthPrice = saveData.beeHealthPrice;
        beeStoragePrice = saveData.beeStoragePrice;
        beeSpeedPrice = saveData.beeSpeedPrice;

        beeCount = saveData.beeCount;
        beeCountPrice = saveData.beeCountPrice;
        honeyComb = saveData.honeyComb;
        honeyCombPrice = saveData.honeyCombPrice;

        royalQueenHealth = saveData.royalQueenHealth;
        royalQueenHealthPrice = saveData.royalQueenHealthPrice;
        royalQueenStorage = saveData.royalQueenStorage;
        royalQueenStoragePrice = saveData.royalQueenStoragePrice;
        royalBeeHealthPrice = saveData.royalBeeHealthPrice;
        royalBeeStoragePrice = saveData.royalBeeStoragePrice;

        beeWork.beeHealth = saveData.beeHealth;
        beeWork.beeStorage = saveData.beeStorage;
        beeWork.beeSpeed = saveData.beeSpeed;

        beeWork.royalBeeHealth = saveData.royalBeeHealth;
        beeWork.royalBeeStorage = saveData.royalBeeStorage;

        Queen = saveData.Queen;
        Bee = saveData.Bee;

        Map = saveData.Map;

        SkinManager.queenSkinList = saveData.queenSkinList;
        SkinManager.beeSkinList = saveData.beeSkinList;
        MapManager.mapSkinList = saveData.mapSkinList;
    }

    void FillHoneyComb()
    {
        GameObject[] honeycombs = GameObject.FindGameObjectsWithTag("honeycomb");

        if (honeyComb != honeycombs.Length)
        {
            for (int i = honeycombs.Length; i < honeyComb; i++)
            {
                Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;                                                                        // �ʱ� ���� X, Y ��ǥ�� ��������
                float spotX = honeyCombSpot.x + (i % honeyCombWidth) * honeyCombX;                                                                // ������ ���� X ��ǥ�� ���
                float spotY = honeyCombSpot.y - (i / honeyCombWidth) * honeyCombY - ((i % honeyCombWidth) % 2) * (honeyCombY / 2);  // ������ ���� Y ��ǥ�� ���

                Instantiate(prefabHoneyComb, new Vector2(spotX, spotY), Quaternion.identity);                                                                   // �����س��� ���� �̹��� ����
            }
        }
    }

    void FillHoneyCombBG()
    {
        GameObject[] honeycombBGs = GameObject.FindGameObjectsWithTag("honeycombBackground");

        if (honeyComb / 5 > honeycombBGs.Length || 3 > honeycombBGs.Length)
        {
            for (int i = honeycombBGs.Length; i <= honeyComb / 5 || i < 3; i++)
            {
                Vector2 honeyCombBGSpot = GameObject.Find("honeycombBackground").transform.position;  // �ʱ� ���� X, Y ��ǥ�� ��������
                float spotX = honeyCombBGSpot.x;                                                             // ������ ���� X ��ǥ�� ���
                float spotY = honeyCombBGSpot.y - (1.2f * i);  // ������ ���� Y ��ǥ�� ���

                Instantiate(prefabHoneyCombBG, new Vector2(spotX, spotY), Quaternion.identity);                                                                   // �����س��� ���� �̹��� ����
                if(i > 2)
                {
                    mainCameraDrag.mainLimitMinY -= 1.2f;
                    mainCameraDrag.limitMinY = mainCameraDrag.mainLimitMinY;

                }
            }
        }
    }

    void FillBee()
    {
        GameObject[] bees = GameObject.FindGameObjectsWithTag("bee");
        for (int i = 0; i < bees.Length; i++)
        {
            Destroy(bees[i]);
        }

        for (int i = 0; i < beeCount; i++)
        {
            StartCoroutine(BeeDelay(i));                                                                        // �����س��� �Ϲ� �̹��� ����
        }
    }

    public void ChangeBee()
    {
        if (Bee != SkinManager.Bee)
        {
            Bee = SkinManager.Bee;
            FillBee();
        }
    }

    IEnumerator BeeDelay(int i)
    {
        GameObject prefabBeeStop = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/beeStop.prefab", typeof(GameObject));
        prefabBeeStop.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + Bee + "5.png", typeof(Sprite));

        GameObject prefabBee = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/bee/" + Bee + ".prefab", typeof(GameObject));

        Vector2 honeyCombSpot = GameObject.Find("honeycomb").transform.position;                                                                        // �ʱ� ���� X, Y ��ǥ�� ��������
        float spotX = honeyCombSpot.x + (i % honeyCombWidth) * honeyCombX;                                                                 // ������ �Ϲ� X ��ǥ�� ���
        float spotY = honeyCombSpot.y - (i / honeyCombWidth) * honeyCombY - ((i % honeyCombWidth) % 2) * (honeyCombY / 2);    // ������ �Ϲ� Y ��ǥ�� ���

        GameObject Beestop = Instantiate(prefabBeeStop, new Vector2(spotX, spotY), Quaternion.identity);


        int rand = Random.Range(0, 11);

        if(temperature < 0)
        {
            rand = 0;
        }

        yield return new WaitForSeconds(rand / 2f);

        Instantiate(prefabBee, new Vector2(spotX, spotY), Quaternion.identity);
        Destroy(Beestop);

        yield break;

    }

    public void QueenSkinSelect()
    {
        float spotX = queenSpot.x;
        float spotY = queenSpot.y;

        GameObject prefabQueen = GameObject.FindGameObjectWithTag("queen");
        Destroy(prefabQueen);

        prefabQueen = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/queen/" + Queen + ".prefab", typeof(GameObject));
        queenSelectSkinImage.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/queen/" + Queen + "1.png", typeof(Sprite));
        beeSelectSkinImage.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + Bee + "5.png", typeof(Sprite));

        Instantiate(prefabQueen, new Vector2(spotX, spotY), Quaternion.identity);
    }

    public void MapSelect()
    {
        backgroundImage.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/background/background" + Map + ".jpg", typeof(Sprite));
        hiveImage.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/hive/hive" + Map + ".png", typeof(Sprite));

        int skinNum = 0;
        GameObject[] btnList = GameObject.FindGameObjectsWithTag("mapSkin");
        for (int i = 1; i < btnList.Length; i++)
        {
            string check = btnList[i].GetComponent<SpriteRenderer>().name;
            check = check.Substring(3);

            if (check == Map)
            {
                skinNum = i;
            }
        }
        maxTemp = MapManager.maxTempList[skinNum];
        minTemp = MapManager.minTempList[skinNum];
    }

    void SettingSkin()
    {
        GameObject[] skinList = GameObject.FindGameObjectsWithTag("queenSkin");

        for (int i = 1; i < SkinManager.queenSkinList.Length; i++)
        {
            if (SkinManager.queenSkinList[i])
            {
                Transform buyBtn = skinList[i].transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = skinList[i].transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                string skinName = skinList[i].GetComponent<SpriteRenderer>().sprite.name;
                skinName = skinName.Substring(0, skinName.Length - 4) + "1";

                skinList[i].GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/queen/" + skinName + ".png", typeof(Sprite));
            }
        }

        GameObject[] beeSkinList = GameObject.FindGameObjectsWithTag("beeSkin");

        for (int i = 1; i < SkinManager.beeSkinList.Length; i++)
        {
            if (SkinManager.beeSkinList[i])
            {
                Transform buyBtn = beeSkinList[i].transform.Find("Canvas").Find("buySkin");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = beeSkinList[i].transform.Find("Canvas").Find("selectSkin");
                selectBtn.gameObject.SetActive(true);

                string skinName = beeSkinList[i].GetComponent<SpriteRenderer>().sprite.name;
                skinName = skinName.Substring(0, skinName.Length - 4) + "5";

                beeSkinList[i].GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/bee/" + skinName + ".png", typeof(Sprite));
            }
        }

        GameObject[] mapSkinList = GameObject.FindGameObjectsWithTag("mapSkin");

        for (int i = 1; i < MapManager.mapSkinList.Length; i++)
        {
            if (MapManager.mapSkinList[i])
            {
                Transform buyBtn = mapSkinList[i].transform.Find("Canvas").Find("buyMap");
                buyBtn.gameObject.SetActive(false);

                Transform selectBtn = mapSkinList[i].transform.Find("Canvas").Find("selectMap");
                selectBtn.gameObject.SetActive(true);
            }
        }
    }



    string DivideNumber(long num)
    {
        long divineNum = num;
        int divisionNumber = 0;
        while (divineNum / 1000  > 0)
        {
            divineNum = divineNum / 1000;
            divisionNumber++;
        }
        char divisionWord = (char) (divisionNumber + 64);


        if (divisionNumber == 0)
        {
            return divineNum.ToString();
        }
        else 
        {
            if(divisionNumber == 1)
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
