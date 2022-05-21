using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1 : MonoBehaviour
{
    public GameObject GameObject1;
    public GameObject GameObject2;
    public GameObject GameObject3;
    private GameObject gameObject;
    public Text text1;

    private LeapProvider mProvider;
    private Frame mFrame;
    private Hand mHand;
    private int n;

    //�����ƶ�����С�ٶ�
    [Tooltip("Velocity (m/s) of Palm ")]
    public float smallestVelocity = 0.5f;

    //�������������ƶ����ٶ�
    [Tooltip("Velocity (m/s) of Single Direction ")]
    [Range(0, 1)]
    public float deltaVelocity = 1.0f;

    void Start()
    {
        mProvider = FindObjectOfType<LeapProvider>() as LeapProvider;
        gameObject = GameObject1;
        n = 1;
    }

    void Update()
    {
        if (n == 1)
        {
            gameObject = GameObject1;
        }
        else if(n == 2)
        {
            gameObject = GameObject2;
        }
        else if (n == 3)
        {
            gameObject = GameObject3;
        }
        
        mFrame = mProvider.CurrentFrame;
        if (mFrame.Hands.Count > 0)
        {
            Gestures(mFrame);
        }
    }
    void Gestures(Frame mFrame)
    {
        //Gesture_zoom = false;
        foreach (var itemHands in mFrame.Hands)
        {
            if (itemHands.IsLeft)
            {
                if (isMoveRight(itemHands))
                {
                    gameObject.transform.rotation= Quaternion.Euler(0, 0, 0);
                }
            }
            else if (itemHands.IsRight)
            {

                if (isMoveLeft(itemHands))
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
            if (isMoveUp(itemHands))
            {
                if (n > 1)
                {
                    n--;
                }
            }
            else if (isMoveDown(itemHands))
            {
                if (n < 3)
                {
                    n++;
                }
            }
            else if (isMoveForward(itemHands))
            {
                text1.text = "��ǰ���������";
            }
        }
    }


    // �ֻ����ұ�
    public bool isMoveRight(Hand hand)
    {
        return hand.PalmVelocity.x > deltaVelocity && !isStationary(hand);
    }

    // �ֻ������
    public bool isMoveLeft(Hand hand)
    {
        return hand.PalmVelocity.x < -deltaVelocity && !isStationary(hand);
    }

    // �ֻ����Ϸ�
    public bool isMoveUp(Hand hand)
    {
        return hand.PalmVelocity.y > deltaVelocity && !isStationary(hand);
    }

    // �ֻ����·�
    public bool isMoveDown(Hand hand)
    {
        return hand.PalmVelocity.y < -deltaVelocity && !isStationary(hand);
    }

    // �ֻ���ǰ��
    public bool isMoveForward(Hand hand)
    {
        return hand.PalmVelocity.z > deltaVelocity && !isStationary(hand);
    }

    // �̶�������
    public bool isStationary(Hand hand)
    {
        return hand.PalmVelocity.Magnitude < smallestVelocity;      //Vector3.Magnitude���������ĳ���
    }
}
