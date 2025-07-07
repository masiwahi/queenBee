using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Text startBtnText;
    private int blink = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startBtnText.color = new Color(startBtnText.color.r, startBtnText.color.g, startBtnText.color.b, startBtnText.color.a + (Time.deltaTime)*blink);
        if(startBtnText.color.a <= 0.0f)
        {
            blink = 1;
        }
        if(startBtnText.color.a >= 1.0f)
        {
            blink = -1;
        }
    }


    public void StartButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
