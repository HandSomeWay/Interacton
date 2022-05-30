using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
public class VoiceRecognizer : MonoBehaviour
{
    #region 识别器
    //所要识别的关键字
    public string[] keyWords = { "金光速现覆护吾身", "智慧明镜心神安宁", "招财进宝" ,"天地合其精日月合其明"};
    //识别器
    private PhraseRecognizer phraseRecognizer;
    //怀疑程度
    private static int doubt = 0;
    //可信度
    public ConfidenceLevel confidenceLevel = ConfidenceLevel.Low;

    public Text SuccessText;

    #endregion


    void Start()
    {
        if (phraseRecognizer == null && keyWords != null)
        {
            //初始化一个识别器
            phraseRecognizer = new KeywordRecognizer(keyWords, confidenceLevel);
            //识别后的方法
            phraseRecognizer.OnPhraseRecognized += PhraseRecognizer_OnPhraseRecognized;

            phraseRecognizer.Start();
            Debug.Log(phraseRecognizer.IsRunning);
        }
    }
    /// <summary>
    /// 当识别到关键字时调用此方法
    /// </summary>
    /// <param name="args"></param>
    private void PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        SuccessText.gameObject.SetActive(true);
    }

}
