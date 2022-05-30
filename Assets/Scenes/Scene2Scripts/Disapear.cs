using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapear : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DIS());
    }

    IEnumerator DIS()
    {
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("Print");
        gameObject.SetActive(false);
    }
}
