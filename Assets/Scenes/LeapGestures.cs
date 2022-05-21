using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public class LeapGestures : MonoBehaviour
{
    private LeapProvider mProvider;
    private Frame mFrame;
    private Hand mHand;

    private Vector leftPosition;
    private Vector rightPosition;
    public static float zoom = 1.0f;
    public float dis;

    //�����ƶ�����С�ٶ�
    [Tooltip("Velocity (m/s) of Palm ")]
    public float smallestVelocity = 0.5f;

    //�������������ƶ����ٶ�
    [Tooltip("Velocity (m/s) of Single Direction ")]
    [Range(0, 1)]
    public float deltaVelocity = 1.0f;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    public Text text7;
    public Text text8;


    void Start()
    {
        mProvider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update()
    {
        //��ȡ��ǰ֡
        mFrame = mProvider.CurrentFrame;


/*        for (int i = 0; i < mProvider.Hands.Count; i++)
        {
            Hand _hand = mProvider.Hands[i];
        }*/

        //����ֵĸ���
        if (mFrame.Hands.Count > 0)
        {
            if (mFrame.Hands.Count == 1)
            {
                Gestures(mFrame);
            }
            if (mFrame.Hands.Count == 2)
            {
                Gestures(mFrame);
                CalcuateDistance(mFrame);
            }
        }
        text1.text = "ʶ�����Ƶĸ�����" + mFrame.Hands.Count.ToString();
    }

    void CalcuateDistance(Frame mFrame)
    {
        float distance = 0f;
        foreach (var itemHands in mFrame.Hands)
        {
            if (itemHands.IsLeft)
            {
                leftPosition = itemHands.PalmPosition;
            }
            if (itemHands.IsRight)
            {
                rightPosition = itemHands.PalmPosition;
            }
        }

        if (leftPosition != Vector.Zero && rightPosition != Vector.Zero)
        {
            Vector3 leftPos = new Vector3(leftPosition.x, leftPosition.y, leftPosition.z);
            Vector3 rightPos = new Vector3(rightPosition.x, rightPosition.y, rightPosition.z);

            distance = 10 * Vector3.Distance(leftPos, rightPos);
            //print("distance" + distance);
            text6.text = "˫�ֵľ���Ϊ��" + distance.ToString();
        }
    }

    void Gestures(Frame mFrame)
    {
        //Gesture_zoom = false;
        foreach (var itemHands in mFrame.Hands)
        {
            Finger _thumb = itemHands.GetThumb();
            Finger _index = itemHands.GetIndex();
            Finger _middle = itemHands.GetMiddle();
            Finger _ring = itemHands.GetRing();
            Finger _pinky = itemHands.GetPinky();



            if (itemHands.IsLeft)
            {
                text7.text = "������ָλ�ã�\t" + (new Vector3(_thumb.TipPosition.x, _thumb.TipPosition.y, _thumb.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_index.TipPosition.x, _index.TipPosition.y, _index.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_middle.TipPosition.x, _middle.TipPosition.y, _middle.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_ring.TipPosition.x, _ring.TipPosition.y, _ring.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_pinky.TipPosition.x, _pinky.TipPosition.y, _pinky.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(itemHands.PalmPosition.x, itemHands.PalmPosition.y, itemHands.PalmPosition.z)).ToString();

                if (isMoveLeft(itemHands))
                {
                    text2.text = "���ֵ��ƶ�Ϊ������";
                }
                else if (isMoveRight(itemHands))
                {
                    text2.text = "���ֵ��ƶ�Ϊ������";
                }
                else if (isMoveUp(itemHands))
                {
                    text2.text = "���ֵ��ƶ�Ϊ������";
                }
                else if (isMoveDown(itemHands))
                {
                    text2.text = "���ֵ��ƶ�Ϊ������";

                }
                else if (isMoveForward(itemHands))
                {
                    text2.text = "���ֵ��ƶ�Ϊ����ǰ";
                }
                else if (isMoveBack(itemHands))
                {
                    text2.text = "���ֵ��ƶ�Ϊ�����";
                }
                else if (isStationary(itemHands))
                {
                    //text2.text = "���ֵ��ƶ�Ϊ������";
                }
                /*                if (isCloseFullHand(itemHands))
                                {
                                    text3.text = "���ֵ�����Ϊ����ȭ";
                                }
                                else */
                if (isGrabHand(itemHands))
                {
                    text3.text = "���ֵ�����Ϊ��ץȡ";
                    //transform.position = new Vector3(leftPosition.x, leftPosition.y, leftPosition.z);
                    //Debug.Log("aaaaaa"+((new Vector3(leftPosition.x, leftPosition.y, leftPosition.z)) - transform.position).magnitude);
                    //Debug.Log("bbbbbb"+dis);
                    if (((new Vector3(itemHands.PalmPosition.x, itemHands.PalmPosition.y, itemHands.PalmPosition.z)) - transform.position).magnitude < dis)
                    {
                        transform.position = new Vector3(itemHands.PalmPosition.x + 0.04f, itemHands.PalmPosition.y, itemHands.PalmPosition.z);
                        transform.rotation = Quaternion.Euler(0f, 0f, 25f);
                    }
                }
                else if (isOpenFullHand(itemHands))
                {
                    text3.text = "���ֵ�����Ϊ���ſ�";
                }
                //isRotation(itemHands);
            }
            else if (itemHands.IsRight)
            {
                text8.text = "������ָλ�ã�\t" + (new Vector3(_thumb.TipPosition.x, _thumb.TipPosition.y, _thumb.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_index.TipPosition.x, _index.TipPosition.y, _index.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_middle.TipPosition.x, _middle.TipPosition.y, _middle.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_ring.TipPosition.x, _ring.TipPosition.y, _ring.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_pinky.TipPosition.x, _pinky.TipPosition.y, _pinky.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(itemHands.PalmPosition.x, itemHands.PalmPosition.y, itemHands.PalmPosition.z)).ToString();

                if (isMoveLeft(itemHands))
                {
                    text4.text = "���ֵ��ƶ�Ϊ������";
                }
                else if (isMoveRight(itemHands))
                {
                    text4.text = "���ֵ��ƶ�Ϊ������";
                }
                else if (isMoveUp(itemHands))
                {
                    text4.text = "���ֵ��ƶ�Ϊ������";
                }
                else if (isMoveDown(itemHands))
                {
                    text4.text = "���ֵ��ƶ�Ϊ������";
                }
                else if (isMoveForward(itemHands))
                {
                    text4.text = "���ֵ��ƶ�Ϊ����ǰ";
                }
                else if (isMoveBack(itemHands))
                {
                    text4.text = "���ֵ��ƶ�Ϊ�����";
                }
                else if (isStationary(itemHands))
                {
                    //text4.text = "���ֵ��ƶ�Ϊ������";
                }
                /*                if (isCloseFullHand(itemHands))
                                {
                                    text5.text = "���ֵ�����Ϊ����ȭ";
                                }
                                else */
                if (isGrabHand(itemHands))
                {
                    text5.text = "���ֵ�����Ϊ��ץȡ";
                    if (((new Vector3(itemHands.PalmPosition.x, itemHands.PalmPosition.y, itemHands.PalmPosition.z)) - transform.position).magnitude < dis)
                    {
                        transform.position = new Vector3(itemHands.PalmPosition.x - 0.04f, itemHands.PalmPosition.y, itemHands.PalmPosition.z);
                        transform.rotation = Quaternion.Euler(0f, 0f, -25f);
                    }
                }
                else if (isOpenFullHand(itemHands))
                {
                    text5.text = "���ֵ�����Ϊ���ſ�";
                }
                //isRotation(itemHands);
            }
        }
    }

    // �Ŵ����ƣ�����ȫչ��
    public bool isOpenFullHand(Hand hand)
    {
        return hand.GrabStrength == 0;
    }

    // ��С���ƣ�������ȭ
    public bool isCloseFullHand(Hand hand)
    {
        return hand.GrabAngle > 2.0f;
    }

    // �Ƿ���ȭ
    public bool isStone(Hand hand)
    {
        return hand.GrabAngle > 2.0f;
    }

    // �Ƿ�ץȡ
    public bool isGrabHand(Hand hand)
    {
        return hand.GrabStrength > 0.1f;  
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

    // �ֻ����
    public bool isMoveBack(Hand hand)
    {
        return hand.PalmVelocity.z < -deltaVelocity && !isStationary(hand);
    }

    // �̶�������
    public bool isStationary(Hand hand)
    {
        return hand.PalmVelocity.Magnitude < smallestVelocity;      //Vector3.Magnitude���������ĳ���
    }

}

