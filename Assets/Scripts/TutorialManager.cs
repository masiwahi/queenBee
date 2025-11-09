using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private string[] tutorialMessage = new string[58];
    private float[] buttonX = new float[58];
    private float[] buttonY = new float[58];

    public static int Count = 0;
    private float blink = 0.005f;

    public Text messageText;
    public Button clickPoint;
    public Image clickImage;

    public Button queenMenu;
    public Button beeMenu;
    public Button royalMenu;
    public Button hambugerMenu;
    public Button skinMenu;
    public Button mapMenu;
    public Button collectMenu;
    public Button itemMenu;

    public Button royalExit;
    public Button skinExit;
    public Button mapExit;
    public Button collectExit;
    public Button itemExit;

    public GameObject canvasTutorial;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingTutorial();

        Count--;
        NextTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        clickImage.transform.localScale = new Vector3(clickImage.transform.localScale.x-blink , clickImage.transform.localScale.y-blink);
        if (clickImage.transform.localScale.x <= 0.0f)
        {
            blink = -0.005f;
        }
        if (clickImage.transform.localScale.x >= 1.0f)
        {
            blink = 0.005f;
        }
    }

    public void NextTutorial()
    {
        Count++;

        if(Count == 57)
        {
            itemExit.onClick.Invoke();
            canvasTutorial.SetActive(false);
        }

        messageText.text = tutorialMessage[Count];
        if(tutorialMessage[Count] == "")
        {
            messageText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            messageText.transform.parent.gameObject.SetActive(true);
        }
        clickPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(buttonX[Count], buttonY[Count]);

        switch (Count) {
            case 7: queenMenu.onClick.Invoke(); break;
            case 21: beeMenu.onClick.Invoke(); break;
            case 31: royalMenu.onClick.Invoke(); break;
            case 32: royalExit.onClick.Invoke(); break;
            case 33: hambugerMenu.onClick.Invoke(); break;
            case 38: skinMenu.onClick.Invoke(); break;
            case 43: skinExit.onClick.Invoke(); mapMenu.onClick.Invoke(); break;
            case 48: mapExit.onClick.Invoke(); collectMenu.onClick.Invoke(); break;
            case 54: collectExit.onClick.Invoke(); itemMenu.onClick.Invoke(); break;
        }
    }

    void SettingTutorial()
    {
        tutorialMessage[0] = "손모양이 가리키는 곳을 터치하면 튜토리얼이 진행돼요";
        tutorialMessage[1] = "여왕벌인 저는 화면을 터치해야 일을 해요";
        tutorialMessage[2] = "일벌은 알아서 일을 해요 일벌 강화 탭에서 일벌을 구매할 수 있어요";
        tutorialMessage[3] = "온도가 영하로 내려가면 벌들이 잠을 자요 잠을 자는 동안에는 꿀을 채밀할 수 없어요";
        tutorialMessage[4] = "꿀로 벌을 강화하거나, 옷을 구매할 수 있어요";
        tutorialMessage[5] = "로얄젤리로 벌을 영구 강화하거나, 옷, 새로운 지역을 구매할 수 있어요";
        tutorialMessage[6] = "";
        tutorialMessage[7] = "여왕벌 강화 메뉴를 누르면 여왕벌을 강화할 수 있어요";
        tutorialMessage[8] = "";
        tutorialMessage[9] = "구매 시 체력이 올라 여왕벌이 채밀하는 꿀의 양이 늘어나요";
        tutorialMessage[10] = "";
        tutorialMessage[11] = "구매 시 한번에 더 많은 꿀을 담아서 여왕벌이 채밀하는 꿀의 양이 늘어나요";
        tutorialMessage[12] = "";
        tutorialMessage[13] = "일벌이 지낼 수 있는 벌집을 늘릴 수 있어요";
        tutorialMessage[14] = "벌집은 지역 개수마다 5개씩 최대치를 늘릴 수 있어요";
        tutorialMessage[15] = "";
        tutorialMessage[16] = "분봉을 하면 새로운 여왕벌이 로얄젤리만 가지고 새출발을 해요";
        tutorialMessage[17] = "새출발인 만큼 꿀과 꿀로 강화한 내용이 초기화 돼요";
        tutorialMessage[18] = "구매한 옷, 지역은 사라지지 않아요 로얄젤리로 강화한 내용도 사라지지 않아요";
        tutorialMessage[19] = "여왕벌과 일벌의 체력, 꿀주머니 레벨이 100 을 넘으면 분봉을 할 수 있어요";
        tutorialMessage[20] = "";
        tutorialMessage[21] = "일벌 강화 메뉴를 누르면 여왕벌을 강화할 수 있어요";
        tutorialMessage[22] = "";
        tutorialMessage[23] = "구매 시 체력이 올라 일벌이 채밀하는 꿀의 양이 늘어나요";
        tutorialMessage[24] = "";
        tutorialMessage[25] = "구매 시 한번에 더 많은 꿀을 담아서 일벌이 채밀하는 꿀의 양이 늘어나요";
        tutorialMessage[26] = "";
        tutorialMessage[27] = "구매 시 일벌이 더 빠르게 꿀을 채밀해와요";
        tutorialMessage[28] = "";
        tutorialMessage[29] = "구매 시 일벌이 더 늘어나요 벌집보다 많은 양의 일벌을 구매할 순 없어요";
        tutorialMessage[30] = "";
        tutorialMessage[31] = "로얄젤리 강화 메뉴를 누르면 로얄젤리로 여왕벌과 일벌을 강화할 수 있어요";
        tutorialMessage[32] = "메뉴바를 누르면 다양한 다양한 메뉴를 확인 할 수 있어요";
        tutorialMessage[33] = "매일 하면 로얄젤리를 보상으로 주는 퀘스트를 볼 수 있어요";
        tutorialMessage[34] = "옷을 구매하고 갈아입을 수 있는 옷장페이지로 이동해요";
        tutorialMessage[35] = "새로운 지역을 구매하고 이동할 수 있는 지역페이지로 이동해요";
        tutorialMessage[36] = "벌들에게 모험을 보낼 수 있는 모험페이지로 이동해요";
        tutorialMessage[37] = "벌들이 모험으로 가져온 물건들을 확인할 수 있는 아이템페이지로 이동해요";
        tutorialMessage[38] = "입힌 옷을 확인 할 수 있어요";
        tutorialMessage[39] = "터치하면 일벌과 여왕벌 옷장을 변경할 수 있어요";
        tutorialMessage[40] = "옷 가격이 표시 돼요";
        tutorialMessage[41] = "터치하면 구매할 수 있어요";
        tutorialMessage[42] = "구매한 옷을 터치하면 옷을 갈아입을 수 있어요";
        tutorialMessage[43] = "지역 해금 가격이 표시돼요";
        tutorialMessage[44] = "터치해서 구매하면 출입금지 표지판이 사라져요";
        tutorialMessage[45] = "지역마다 최고기온과 최저기온이 달라요";
        tutorialMessage[46] = "";
        tutorialMessage[47] = "출입 가능한 지역을 클릭하면 지역을 이동할 수 있어요";
        tutorialMessage[48] = "터치하면 모험할 지역을 선택 할 수 있어요";
        tutorialMessage[49] = "터치하면 모험 복장을 고를 수 있어요";
        tutorialMessage[50] = "";
        tutorialMessage[51] = "모험에서 돌아올 때 받을 수 있는 보상들을 확인 할 수 있어요";
        tutorialMessage[52] = "";
        tutorialMessage[53] = "터치하면 모험을 출발해요";
        tutorialMessage[54] = "모험에서 획득한 아이템을 볼 수 있어요";
        tutorialMessage[55] = "아이템의 갯수를 볼 수 있어요";
        tutorialMessage[56] = "아이템의 효과를 볼 수 있어요";

        buttonX[0] = 500;
        buttonY[0] = -1050;
        buttonX[1] = -150;
        buttonY[1] = 550;
        buttonX[2] = -500;
        buttonY[2] = -200;
        buttonX[3] = -450;
        buttonY[3] = 1150;
        buttonX[4] = 50;
        buttonY[4] = 1150;
        buttonX[5] = 550;
        buttonY[5] = 1150;
        buttonX[6] = -370;
        buttonY[6] = -1130;
        buttonX[7] = 500;
        buttonY[7] = -1050;
        buttonX[8] = 400;
        buttonY[8] = -550;
        buttonX[9] = 500;
        buttonY[9] = -1050;
        buttonX[10] = 400;
        buttonY[10] = -800;
        buttonX[11] = 500;
        buttonY[11] = -1050;
        buttonX[12] = 400;
        buttonY[12] = -1000;
        buttonX[13] = 500;
        buttonY[13] = -1050;
        buttonX[14] = 500;
        buttonY[14] = -1050;
        buttonX[15] = 400;
        buttonY[15] = -1150;
        buttonX[16] = 500;
        buttonY[16] = -1050;
        buttonX[17] = 500;
        buttonY[17] = -1050;
        buttonX[18] = 500;
        buttonY[18] = -1050;
        buttonX[19] = 500;
        buttonY[19] = -1050;
        buttonX[20] = 0;
        buttonY[20] = -350;
        buttonX[21] = 500;
        buttonY[21] = -1050;
        buttonX[22] = 400;
        buttonY[22] = -550;
        buttonX[23] = 500;
        buttonY[23] = -1050;
        buttonX[24] = 400;
        buttonY[24] = -800;
        buttonX[25] = 500;
        buttonY[25] = -1050;
        buttonX[26] = 400;
        buttonY[26] = -1000;
        buttonX[27] = 500;
        buttonY[27] = -1050;
        buttonX[28] = 400;
        buttonY[28] = -1150;
        buttonX[29] = 500;
        buttonY[29] = -1050;
        buttonX[30] = 430;
        buttonY[30] = -350;
        buttonX[31] = 500;
        buttonY[31] = -1050;
        buttonX[32] = 580;
        buttonY[32] = 970;
        buttonX[33] = 580;
        buttonY[33] = 770;
        buttonX[34] = 580;
        buttonY[34] = 570;
        buttonX[35] = 580;
        buttonY[35] = 350;
        buttonX[36] = 380;
        buttonY[36] = 770;
        buttonX[37] = 380;
        buttonY[37] = 570;
        buttonX[38] = 100;
        buttonY[38] = 500;
        buttonX[39] = 570;
        buttonY[39] = 650;
        buttonX[40] = -120;
        buttonY[40] = -220;
        buttonX[41] = -150;
        buttonY[41] = -100;
        buttonX[42] = -450;
        buttonY[42] = -100;
        buttonX[43] = 380;
        buttonY[43] = 390;
        buttonX[44] = 400;
        buttonY[44] = 330;
        buttonX[45] = 550;
        buttonY[45] = 150;
        buttonX[46] = 380;
        buttonY[46] = -950;
        buttonX[47] = 500;
        buttonY[47] = -1050;
        buttonX[48] = 550;
        buttonY[48] = 650;
        buttonX[49] = -300;
        buttonY[49] = -200;
        buttonX[50] = -300;
        buttonY[50] = -600;
        buttonX[51] = 500;
        buttonY[51] = -1050;
        buttonX[52] = 170;
        buttonY[52] = -970;
        buttonX[53] = 500;
        buttonY[53] = -1050;
        buttonX[54] = -450;
        buttonY[54] = 750;
        buttonX[55] = -380;
        buttonY[55] = 680;
        buttonX[56] = -400;
        buttonY[56] = 550;

    }
}
