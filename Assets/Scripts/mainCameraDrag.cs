using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class mainCameraDrag : MonoBehaviour
{
    private Transform tr;                       // 화면의 현재 위치
    private Vector2 nowPos;
    private Vector2 prePos;

    public GameObject skinBackground;           // 스킨 페이지 배경 화면

    public static float limitMinY = -0.4f;      // 카메라 최소 위치 값
    public static float limitMaxY = 0;          // 카메라 최대 위치 값

    public static float skinLimitMinY = -0.48f; // 스킨페이지 최소 위치 값

    public static float mainLimitMinY = -0.4f;  // 메인 페이지 최소 위치 값

    public float dragSpeed = 0.05f;             // 화면 내리는 속도

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        SetResolution();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution()
    {
        int setWidth = 1440; // 사용자 설정 너비
        int setHeight = 2560; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }

    // 화면 드래그 시 화면 상하 이동
    void Move()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                prePos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                nowPos = touch.position - touch.deltaPosition;

                if (prePos.y < nowPos.y && nowPos.y - prePos.y > 0.1f)
                {
                    if (tr.position.y > limitMinY)
                    {
                        tr.Translate(Vector2.down * dragSpeed);
                        skinBackground.GetComponent<Transform>().Translate(Vector2.down * dragSpeed * Time.deltaTime);
                    }
                }
                else if (prePos.y > nowPos.y && prePos.y - nowPos.y > 0.1f)
                {
                    if (tr.position.y < limitMaxY)
                    {
                        tr.Translate(Vector2.up * dragSpeed);
                        skinBackground.GetComponent<Transform>().Translate(Vector2.up * dragSpeed * Time.deltaTime);
                    }
                }
                prePos = touch.position - touch.deltaPosition;
            }
        }
    }

    // 스킨 페이지 들어가기
    public void EnterSkinSelect()
    {
        tr.position = new Vector3(-24, 0, -10);
        skinBackground.GetComponent<Transform>().position = new Vector3(-24, 0, 0);
        limitMinY = skinLimitMinY;
    }

    // 여왕 스킨 선택 페이지 이동
    public void QueenSkinSelect()
    {
        tr.position = new Vector3(-24, 0, -10);
        skinBackground.GetComponent<Transform>().position = new Vector3(-24, 0, 0);
    }

    // 일벌 스킨 선택 페이지 이동
    public void BeeSkinSelect()
    {
        tr.position = new Vector3(-16, 0, -10);
        skinBackground.GetComponent<Transform>().position = new Vector3(-16, 0, 0);
    }

    // 스킨 페이지 나가기
    public void ExitSkinSelect()
    {
        tr.position = new Vector3(0, 0, -10);
        limitMinY = mainLimitMinY;
    }

    // 맵 스킨 페이지 들어가기
    public void EnterMapSelect()
    {
        tr.position = new Vector3(16, 0, -10);
        limitMinY = 0;
    }

    // 맵 스킨 페이지 나가기
    public void ExitMapSelect()
    {
        tr.position = new Vector3(0, 0, -10);
        limitMinY = mainLimitMinY;
    }

    // 모험 스킨 페이지 들어가기
    public void EnterCollectSelect()
    {
        tr.position = new Vector3(-16, 15, -10);
        limitMinY = 0;
    }

    // 모험 스킨 페이지 나가기
    public void ExitCollectSelect()
    {
        tr.position = new Vector3(0, 0, -10);
        limitMinY = mainLimitMinY;
    }

    public void EnterItemSelect()
    {
        tr.position = new Vector3(24, 15, -10);
        limitMinY = 1.7f;
    }

    // 모험 스킨 페이지 나가기
    public void ExitItemSelect()
    {
        tr.position = new Vector3(0, 0, -10);
        limitMinY = mainLimitMinY;
    }
}
