using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureReplace : MonoBehaviour
{

    public static Sprite PictureOnClicked;
    
    // Start is called before the first frame update
    void Start()
    {
       PictureOnClicked = gameObject.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().sprite = PictureOnClicked;
    }
}
