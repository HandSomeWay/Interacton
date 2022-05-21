using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transparency : MonoBehaviour
{

    //渐变时长
    public float time = 2f;
    public float speed = 0.5f;
    //定时器
    public float timer = 0;
    //透明度 范围是0-1
    float alpha = 1;
    int add = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (time < timer)
        {
            timer = 0;
            if (add == 1)
                add = 0;
            else
                add = 1;
        }
        if (add == 1)
            alpha += Time.deltaTime * speed;
        else
            alpha -= Time.deltaTime * speed;
        Debug.Log(alpha);
        GetComponent<Image>().color = new Color(255, 255, 255, alpha);
    }

}
