using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadImage : MonoBehaviour
{
    //������Ҫչʾ�ķ���ͼƬ
    List<Texture2D> allTex2d = new List<Texture2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Load()
    {
        List<string> filePaths = new List<string>();
        string imgtpye = "*.BMP|*.JPG|*.PNG";
        string[] imageType = imgtpye.Split('|');

    }
}
