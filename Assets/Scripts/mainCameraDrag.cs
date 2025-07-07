using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class mainCameraDrag : MonoBehaviour
{
    private Transform tr;
    private Vector2 firstTouch;

    public GameObject skinBackground;

    public static float limitMinY = -0.4f;
    public static float limitMaxY = 0;

    public static float skinLimitMinY = -0.48f;

    public static float mainLimitMinY = -0.4f;

    public float dragSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(firstTouch, currentTouch) > 1f)
            {
                if (firstTouch.y < currentTouch.y)
                {
                    if (tr.position.y > limitMinY)
                    {
                        tr.Translate(Vector2.down * dragSpeed);
                        skinBackground.GetComponent<Transform>().Translate(Vector2.down * dragSpeed);
                    }
                }
                else if (firstTouch.y > currentTouch.y)
                {
                    if (tr.position.y < limitMaxY)
                    {
                        tr.Translate(Vector2.up * dragSpeed);
                        skinBackground.GetComponent<Transform>().Translate(Vector2.up * dragSpeed);
                    }
                }
            }
        }
    }

    public void EnterSkinSelect()
    {
        tr.position = new Vector3(-24, 0, -10);
        skinBackground.GetComponent<Transform>().position = new Vector3(-24, 0, 0);
        limitMinY = skinLimitMinY;
    }

    public void QueenSkinSelect()
    {
        tr.position = new Vector3(-24, 0, -10);
        skinBackground.GetComponent<Transform>().position = new Vector3(-24, 0, 0);
    }

    public void BeeSkinSelect()
    {
        tr.position = new Vector3(-16, 0, -10);
        skinBackground.GetComponent<Transform>().position = new Vector3(-16, 0, 0);
    }

    public void ExitSkinSelect()
    {
        tr.position = new Vector3(0, 0, -10);
        limitMinY = mainLimitMinY;
    }

    public void EnterMapSelect()
    {
        tr.position = new Vector3(16, 0, -10);
        limitMinY = 0;
    }

    public void ExitMapSelect()
    {
        tr.position = new Vector3(0, 0, -10);
        limitMinY = mainLimitMinY;
    }
}
