using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public class SceneOne : MonoBehaviour
{
    public GameObject GameObject1;
    public GameObject GameObject2;
    public GameObject GameObject3;

    public GameObject GameObject4;
    public GameObject GameObject5;

    public GameObject GameObject6;

    public GameObject GameObject7;
    public GameObject GameObject8;
    public GameObject GameObject9;

    public GameObject GameObject0;

    public GameObject GameObject11;
    public GameObject GameObject12;
    public GameObject GameObject13;
    public GameObject GameObject14;
    public GameObject GameObject15;
    public GameObject GameObject16;
    public GameObject GameObject17;
    public GameObject GameObject18;
    public GameObject GameObject19;
    public GameObject GameObject20;

    public GameObject GameObject21;
    public GameObject GameObject22;
    public GameObject GameObject23;
    public GameObject GameObject24;
    public GameObject GameObject25;
    public GameObject GameObject26;
    public GameObject GameObject27;

    //private GameObject gameObject;
    public Text text1;
    private bool flag = true;
    private bool flag1 = true;

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;
    private Animator animator;

    private Animator animator6;
    private Animator animator7;
    private Animator animator8;
    private Animator animator9;

    private Animator animator11;
    private Animator animator12;
    private Animator animator13;
    private Animator animator14;
    private Animator animator15;
    private Animator animator16;
    private Animator animator17;
    private Animator animator18;
    private Animator animator19;
    private Animator animator20;

    private LeapProvider mProvider;
    private Frame mFrame;
    private Hand mHand;
    private int n;
    public int[] x = new int[3]{ 0, 0, 0 };
    public bool[] f = new bool[8]{ false, false, false, false, false, false, false, false };
    public float dis;
    public float distance = 0f;
    private Vector leftPosition;
    private Vector rightPosition;

    //手掌移动的最小速度
    [Tooltip("Velocity (m/s) of Palm ")]
    public float smallestVelocity = 0.5f;

    //单方向上手掌移动的速度
    [Tooltip("Velocity (m/s) of Single Direction ")]
    [Range(0, 1)]
    public float deltaVelocity = 1.0f;

    void Start()
    {
        mProvider = FindObjectOfType<LeapProvider>() as LeapProvider;
        animator1 = GameObject1.transform.GetComponent<Animator>();
        animator2 = GameObject2.transform.GetComponent<Animator>();
        animator3 = GameObject3.transform.GetComponent<Animator>();
        animator6 = GameObject6.transform.GetComponent<Animator>();
        animator7 = GameObject7.transform.GetComponent<Animator>();
        animator8 = GameObject8.transform.GetComponent<Animator>();
        animator9 = GameObject9.transform.GetComponent<Animator>();

        animator11 = GameObject11.transform.GetComponent<Animator>();
        animator12 = GameObject12.transform.GetComponent<Animator>();
        animator13 = GameObject13.transform.GetComponent<Animator>();
        animator14 = GameObject14.transform.GetComponent<Animator>();
        animator15 = GameObject15.transform.GetComponent<Animator>();
        animator16 = GameObject16.transform.GetComponent<Animator>();
        animator17 = GameObject17.transform.GetComponent<Animator>();
        animator18 = GameObject18.transform.GetComponent<Animator>();
        animator19 = GameObject19.transform.GetComponent<Animator>();
        animator20 = GameObject20.transform.GetComponent<Animator>();

        n = 1;
        animator = animator1;
    }

    void Update()
    {
        if (n == 1)
        {
            animator = animator1;
            GameObject0.transform.position = new Vector3(-1f, 4.35f, -15f);
        }
        else if(n == 2)
        {
            animator = animator2;
            GameObject0.transform.position = new Vector3(-1f, 3.95f, -15f);
        }
        else if (n == 3)
        {
            animator = animator3;
            GameObject0.transform.position = new Vector3(-1f, 3.55f, -15f);
        }

        if(f[0] && f[1] && f[2] && f[3] && f[4] && f[5] && f[6] && f[7])
        {
            StartCoroutine(Opendoor());
        }
        
        mFrame = mProvider.CurrentFrame;
        if (mFrame.Hands.Count > 0)
        {
            if (flag1 && mFrame.Hands.Count == 2)
            {
                CalcuateDistance(mFrame);
            }
            Gestures(mFrame);
        }
    }

    void CalcuateDistance(Frame mFrame)
    {
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
            if (distance < dis)
            {
                StartCoroutine(Show());
            }
        }
    }

    void Gestures(Frame mFrame)
    {
        //Gesture_zoom = false;
        foreach (var itemHands in mFrame.Hands)
        {
            if (itemHands.IsLeft)
            {
                GameObject4.transform.position = new Vector3(itemHands.PalmPosition.x + 0.04f, itemHands.PalmPosition.y, itemHands.PalmPosition.z + 0.1f);
                if (!flag1 && isMoveRight(itemHands))
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
                GameObject5.transform.position = new Vector3(itemHands.PalmPosition.x + 0.04f, itemHands.PalmPosition.y, itemHands.PalmPosition.z + 0.1f);
                if (!flag1 && isMoveLeft(itemHands))
                {
                    x[n - 1] = -1;
                    animator.ResetTrigger("yin2");
                    animator.ResetTrigger("yang1");
                    animator.SetTrigger("yang2");
                    animator.SetTrigger("yin1");
                }
            }

            if (!flag1)
            {
                if (flag == true)
                {
                    if (isMoveUp(itemHands))
                    {
                        if (n > 1)
                        {
                            StartCoroutine(Reduce());
                        }
                    }
                    else if (isMoveDown(itemHands))
                    {
                        if (n < 3)
                        {
                            StartCoroutine(Add());
                        }
                    }
                }

                if (isMoveForward(itemHands))
                {
                    if (x[0] == 1)
                    {
                        if (x[1] == 1)
                        {
                            if (x[2] == 1)
                            {
                                text1.text = "绘制完成这是乾卦";
                                if (f[0] == false)
                                {
                                    animator11.SetTrigger("乾");
                                    StartCoroutine(Disappear());
                                    f[0] = true;
                                }
                            }
                            else if (x[2] == -1)
                            {
                                text1.text = "绘制完成这是巽卦";
                                if (f[1] == false)
                                {
                                    animator12.SetTrigger("巽");
                                    StartCoroutine(Disappear());
                                    f[1] = true;
                                }
                            }
                        }
                        else if (x[1] == -1)
                        {
                            if (x[2] == 1)
                            {
                                text1.text = "绘制完成这是离卦";
                                if (f[2] == false)
                                {
                                    animator13.SetTrigger("离");
                                    StartCoroutine(Disappear());
                                    f[2] = true;
                                }
                            }
                            else if (x[2] == -1)
                            {
                                text1.text = "绘制完成这是艮卦";
                                if (f[3] == false)
                                {
                                    animator14.SetTrigger("艮");
                                    StartCoroutine(Disappear());
                                    f[3] = true;
                                }
                            }
                        }
                    }
                    else if (x[0] == -1)
                    {
                        if (x[1] == 1)
                        {
                            if (x[2] == 1)
                            {
                                text1.text = "绘制完成这是兑卦";
                                if (f[4] == false)
                                {
                                    animator15.SetTrigger("兑");
                                    StartCoroutine(Disappear());
                                    f[4] = true;
                                }
                            }
                            else if (x[2] == -1)
                            {
                                text1.text = "绘制完成这是坎卦";
                                if (f[5] == false)
                                {
                                    animator16.SetTrigger("坎");
                                    StartCoroutine(Disappear());
                                    f[5] = true;
                                }
                            }
                        }
                        else if (x[1] == -1)
                        {
                            if (x[2] == 1)
                            {
                                text1.text = "绘制完成这是震卦";
                                if (f[6] == false)
                                {
                                    animator17.SetTrigger("震");
                                    StartCoroutine(Disappear());
                                    f[6] = true;
                                }
                            }
                            else if (x[2] == -1)
                            {
                                text1.text = "绘制完成这是坤卦";
                                if (f[7] == false)
                                {
                                    animator18.SetTrigger("坤");
                                    StartCoroutine(Disappear());
                                    f[7] = true;
                                }
                            }
                        }
                    }
                }
            }
        }
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

    // 固定不动的
    public bool isStationary(Hand hand)
    {
        return hand.PalmVelocity.Magnitude < smallestVelocity;      //Vector3.Magnitude返回向量的长度
    }

    IEnumerator Add()
    {
        flag = false;
        n++;        
        yield return new WaitForSeconds(0.5f);
        flag = true;
    }

    IEnumerator Reduce()
    {
        flag = false; 
        n--;
        yield return new WaitForSeconds(0.5f);
        flag = true;
    }

    IEnumerator Disappear()
    {
        GameObject0.SetActive(false);
        //GameObject1.SetActive(false);
        //GameObject2.SetActive(false);
        //GameObject3.SetActive(false);
        GameObject1.transform.position = new Vector3(0f, 4.4f, 15f);
        GameObject2.transform.position = new Vector3(0f, 4f, 15f);
        GameObject3.transform.position = new Vector3(0f, 3.6f, 15f);
        yield return new WaitForSeconds(5f);
        GameObject0.SetActive(true);
        GameObject1.transform.position = new Vector3(0f, 4.4f, -15f);
        GameObject2.transform.position = new Vector3(0f, 4f, -15f);
        GameObject3.transform.position = new Vector3(0f, 3.6f, -15f);
        //GameObject1.SetActive(true);
        //GameObject2.SetActive(true);
        //GameObject3.SetActive(true);
    }

    IEnumerator Opendoor()
    {
        text1.text = "绘制完成";
        yield return new WaitForSeconds(4f);

        animator19.SetTrigger("bagua");

        GameObject0.SetActive(false);
        GameObject1.SetActive(false);
        GameObject2.SetActive(false);
        GameObject3.SetActive(false);

        GameObject11.SetActive(false);
        GameObject12.SetActive(false);
        GameObject13.SetActive(false);
        GameObject14.SetActive(false);
        GameObject15.SetActive(false);
        GameObject16.SetActive(false);
        GameObject17.SetActive(false);
        GameObject18.SetActive(false);

        GameObject21.SetActive(false);
        GameObject22.SetActive(false);
        GameObject23.SetActive(false);
        GameObject24.SetActive(false);
        GameObject25.SetActive(false);
        GameObject26.SetActive(false);
        GameObject27.SetActive(false);

        text1.gameObject.SetActive(false);

        yield return new WaitForSeconds(11f);
        GameObject19.SetActive(false);
        animator6.SetTrigger("door");

        yield return new WaitForSeconds(2f);
        animator7.SetTrigger("move");
        yield return new WaitForSeconds(2f);
        animator8.SetTrigger("open");
        animator9.SetTrigger("open");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }


    IEnumerator Show()
    {
        animator20.SetTrigger("yinyang");
        yield return new WaitForSeconds(5f);
        GameObject0.SetActive(true);
        GameObject20.SetActive(false);
        flag1 = false;
        GameObject21.SetActive(true);
        GameObject22.SetActive(true);
        GameObject23.SetActive(true);
        GameObject24.SetActive(true);
        GameObject25.SetActive(true);
        GameObject26.SetActive(true);
        GameObject27.SetActive(true);
        text1.text = "向前推，完成绘制";
    }
}
