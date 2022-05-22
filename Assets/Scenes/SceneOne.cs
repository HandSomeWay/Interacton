using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public class SceneOne : MonoBehaviour
{
    public GameObject GameObject1;
    public GameObject GameObject2;
    public GameObject GameObject3;
    //private GameObject gameObject;
    public Text text1;
    public string text = "��";

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;
    private Animator animator;

    private LeapProvider mProvider;
    private Frame mFrame;
    private Hand mHand;
    private int n;
    public int[] x = new int[3]{ 0, 0, 0 };

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
        animator1 = GameObject1.transform.GetComponent<Animator>();
        animator2 = GameObject2.transform.GetComponent<Animator>();
        animator3 = GameObject3.transform.GetComponent<Animator>();
        n = 1;
        animator = animator1;
    }

    void Update()
    {
        if (n == 1)
        {
            animator = animator1;
        }
        else if(n == 2)
        {
            animator = animator2;
        }
        else if (n == 3)
        {
            animator = animator3;
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
                    x[n - 1] = 1;
                    animator.ResetTrigger("yang2");
                    animator.ResetTrigger("yin1");
                    animator.SetTrigger("yin2");
                    animator.SetTrigger("yang1");
                }
            }
            else if (itemHands.IsRight)
            {

                if (isMoveLeft(itemHands))
                {
                    x[n - 1] = -1;
                    animator.ResetTrigger("yin2");
                    animator.ResetTrigger("yang1");
                    animator.SetTrigger("yang2");
                    animator.SetTrigger("yin1");
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
                if (x[0] == 1)
                {
                    if (x[1] == 1)
                    {
                        if (x[2] == 1)
                        {
                            text = "Ǭ��";
                        }
                        else if (x[2] == -1)
                        {
                            text = "����";
                        }
                    }
                    else if(x[1] == -1)
                    {
                        if (x[2] == 1)
                        {
                            text = "����";
                        }
                        else if (x[2] == -1)
                        {
                            text = "����";
                        }
                    }
                }
                else if(x[0] == -1){
                    if (x[1] == 1)
                    {
                        if (x[2] == 1)
                        {
                            text = "����";
                        }
                        else if (x[2] == -1)
                        {
                            text = "����";
                        }
                    }
                    else if (x[1] == -1)
                    {
                        if (x[2] == 1)
                        {
                            text = "����";
                        }
                        else if (x[2] == -1)
                        {
                            text = "����";
                        }
                    }
                }
                text1.text = "�����������" + text;
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
