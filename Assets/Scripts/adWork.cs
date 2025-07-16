using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adWork : MonoBehaviour
{
    private Transform tr;
    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        Destroy(this.gameObject, 14f);
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt < 200)
        {
            tr.Translate(Vector2.down * 0.5f * Time.deltaTime);
            cnt++;
        }
        else if (cnt < 250)
        {
            cnt++;
        }
        else if (cnt < 450)
        {
            tr.Translate(Vector2.up * 0.5f * Time.deltaTime);
            cnt++;
        }
        else if (cnt < 500)
        {
            cnt++;
        }
        else
        {
            cnt = 0;
        }
        tr.Translate(Vector2.right * 0.5f * Time.deltaTime);
    }

    public void ckickAd()
    {
        Destroy(this.gameObject);
    }
}
