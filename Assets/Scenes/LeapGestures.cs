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

    //手掌移动的最小速度
    [Tooltip("Velocity (m/s) of Palm ")]
    public float smallestVelocity = 0.5f;

    //单方向上手掌移动的速度
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
        //获取当前帧
        mFrame = mProvider.CurrentFrame;


/*        for (int i = 0; i < mProvider.Hands.Count; i++)
        {
            Hand _hand = mProvider.Hands[i];
        }*/

        //获得手的个数
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
        text1.text = "识别手掌的个数：" + mFrame.Hands.Count.ToString();
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
            text6.text = "双手的距离为：" + distance.ToString();
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
                text7.text = "左手手指位置：\t" + (new Vector3(_thumb.TipPosition.x, _thumb.TipPosition.y, _thumb.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_index.TipPosition.x, _index.TipPosition.y, _index.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_middle.TipPosition.x, _middle.TipPosition.y, _middle.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_ring.TipPosition.x, _ring.TipPosition.y, _ring.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_pinky.TipPosition.x, _pinky.TipPosition.y, _pinky.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(itemHands.PalmPosition.x, itemHands.PalmPosition.y, itemHands.PalmPosition.z)).ToString();

                if (isMoveLeft(itemHands))
                {
                    text2.text = "左手的移动为：向左";
                }
                else if (isMoveRight(itemHands))
                {
                    text2.text = "左手的移动为：向右";
                }
                else if (isMoveUp(itemHands))
                {
                    text2.text = "左手的移动为：向上";
                }
                else if (isMoveDown(itemHands))
                {
                    text2.text = "左手的移动为：向下";

                }
                else if (isMoveForward(itemHands))
                {
                    text2.text = "左手的移动为：向前";
                }
                else if (isMoveBack(itemHands))
                {
                    text2.text = "左手的移动为：向后";
                }
                else if (isStationary(itemHands))
                {
                    //text2.text = "左手的移动为：不动";
                }
                /*                if (isCloseFullHand(itemHands))
                                {
                                    text3.text = "左手的手势为：握拳";
                                }
                                else */
                if (isGrabHand(itemHands))
                {
                    text3.text = "左手的手势为：抓取";
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
                    text3.text = "左手的手势为：张开";
                }
                //isRotation(itemHands);
            }
            else if (itemHands.IsRight)
            {
                text8.text = "右手手指位置：\t" + (new Vector3(_thumb.TipPosition.x, _thumb.TipPosition.y, _thumb.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_index.TipPosition.x, _index.TipPosition.y, _index.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_middle.TipPosition.x, _middle.TipPosition.y, _middle.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_ring.TipPosition.x, _ring.TipPosition.y, _ring.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(_pinky.TipPosition.x, _pinky.TipPosition.y, _pinky.TipPosition.z)).ToString() + "\t"
                    + (new Vector3(itemHands.PalmPosition.x, itemHands.PalmPosition.y, itemHands.PalmPosition.z)).ToString();

                if (isMoveLeft(itemHands))
                {
                    text4.text = "右手的移动为：向左";
                }
                else if (isMoveRight(itemHands))
                {
                    text4.text = "右手的移动为：向右";
                }
                else if (isMoveUp(itemHands))
                {
                    text4.text = "右手的移动为：向上";
                }
                else if (isMoveDown(itemHands))
                {
                    text4.text = "右手的移动为：向下";
                }
                else if (isMoveForward(itemHands))
                {
                    text4.text = "右手的移动为：向前";
                }
                else if (isMoveBack(itemHands))
                {
                    text4.text = "右手的移动为：向后";
                }
                else if (isStationary(itemHands))
                {
                    //text4.text = "右手的移动为：不动";
                }
                /*                if (isCloseFullHand(itemHands))
                                {
                                    text5.text = "右手的手势为：握拳";
                                }
                                else */
                if (isGrabHand(itemHands))
                {
                    text5.text = "右手的手势为：抓取";
                    if (((new Vector3(itemHands.PalmPosition.x, itemHands.PalmPosition.y, itemHands.PalmPosition.z)) - transform.position).magnitude < dis)
                    {
                        transform.position = new Vector3(itemHands.PalmPosition.x - 0.04f, itemHands.PalmPosition.y, itemHands.PalmPosition.z);
                        transform.rotation = Quaternion.Euler(0f, 0f, -25f);
                    }
                }
                else if (isOpenFullHand(itemHands))
                {
                    text5.text = "右手的手势为：张开";
                }
                //isRotation(itemHands);
            }
        }
    }

    // 放大手势，手掌全展开
    public bool isOpenFullHand(Hand hand)
    {
        return hand.GrabStrength == 0;
    }

    // 缩小手势，手掌握拳
    public bool isCloseFullHand(Hand hand)
    {
        return hand.GrabAngle > 2.0f;
    }

    // 是否握拳
    public bool isStone(Hand hand)
    {
        return hand.GrabAngle > 2.0f;
    }

    // 是否抓取
    public bool isGrabHand(Hand hand)
    {
        return hand.GrabStrength > 0.1f;  
    }

    // 手划向右边
    public bool isMoveRight(Hand hand)
    {
        return hand.PalmVelocity.x > deltaVelocity && !isStationary(hand);
    }

    // 手划向左边
    public bool isMoveLeft(Hand hand)
    {
        return hand.PalmVelocity.x < -deltaVelocity && !isStationary(hand);
    }

    // 手划向上方
    public bool isMoveUp(Hand hand)
    {
        return hand.PalmVelocity.y > deltaVelocity && !isStationary(hand);
    }

    // 手划向下方
    public bool isMoveDown(Hand hand)
    {
        return hand.PalmVelocity.y < -deltaVelocity && !isStationary(hand);
    }

    // 手划向前方
    public bool isMoveForward(Hand hand)
    {
        return hand.PalmVelocity.z > deltaVelocity && !isStationary(hand);
    }

    // 手划向后方
    public bool isMoveBack(Hand hand)
    {
        return hand.PalmVelocity.z < -deltaVelocity && !isStationary(hand);
    }

    // 固定不动的
    public bool isStationary(Hand hand)
    {
        return hand.PalmVelocity.Magnitude < smallestVelocity;      //Vector3.Magnitude返回向量的长度
    }

}

