using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
public class VoiceRecognizer : MonoBehaviour
{
    #region ʶ����
    //��Ҫʶ��Ĺؼ���
    public string[] keyWords = { "������ָ�������", "�ǻ�����������", "�вƽ���" ,"��غ��侫���º�����"};
    //ʶ����
    private PhraseRecognizer phraseRecognizer;
    //���ɳ̶�
    private static int doubt = 0;
    //���Ŷ�
    public ConfidenceLevel confidenceLevel = ConfidenceLevel.Low;

    public Text SuccessText;

    #endregion


    void Start()
    {
        if (phraseRecognizer == null && keyWords != null)
        {
            //��ʼ��һ��ʶ����
            phraseRecognizer = new KeywordRecognizer(keyWords, confidenceLevel);
            //ʶ���ķ���
            phraseRecognizer.OnPhraseRecognized += PhraseRecognizer_OnPhraseRecognized;

            phraseRecognizer.Start();
            Debug.Log(phraseRecognizer.IsRunning);
        }
    }
    /// <summary>
    /// ��ʶ�𵽹ؼ���ʱ���ô˷���
    /// </summary>
    /// <param name="args"></param>
    private void PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        SuccessText.gameObject.SetActive(true);
    }

}
