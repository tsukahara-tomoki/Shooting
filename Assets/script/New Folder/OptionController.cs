using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 10f;
    readonly Vector3[] pos = new Vector3[60];
    Vector3 pos1;
    [SerializeField] GameObject nextObject;
    Rigidbody2D m_rb2d;
    [SerializeField] int m_delay = 30;
    bool  moving = true;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

        m_rb2d = GetComponent<Rigidbody2D>();
        if (nextObject) Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextObject)
        {
            //if (pos[0] != transform.position)
            //{
            //    moving = true;
            //    OptionController optionController = nextObject.GetComponent<OptionController>();
            //    optionController.Move(pos[1], moving);
            //    Buffer();
            //}
            //else
            //{
            //    moving = false;
            //    OptionController optionController = nextObject.GetComponent<OptionController>();
            //    optionController.Move(pos[1], moving);
            //}}
        
            if (pos[0] != transform.position)
            {
                moving = true;
                OptionController optionController = nextObject.GetComponent<OptionController>();
                optionController.Move(pos[m_delay], moving);
                Buffer();
            }
            else
            {
                moving = false;
                OptionController optionController = nextObject.GetComponent<OptionController>();
                optionController.Move(pos[m_delay], moving);
            }

        }
    }
    void Initialize()
    {
        for (int i = 59; i >= 0; i--)
        {
            pos[i] = transform.position;
        }
    }
    void Buffer()
    {
        //switch (i)
        //{
        //    case m_delay:
        //        for (int i = 59; i > 0; i--)
        //        {
        //            pos[i] = pos[i - 1];
        //        }
        //        pos[0] = transform.position;
        //        break;
        //    default:
        //        i++;
        //        break;
        //}

        for (int i = 59; i > 0; i--)
        {
            pos[i] = pos[i - 1];
        }
        pos[0] = transform.position;
    }
    public void Move(Vector3 pos, bool moving)
    {
        Vector3 pos1 = pos - transform.position;
        float h = pos1[0];
        float v = pos1[1];
        //if (!lasernow)
        //{
            if (moving)
            {

                //transform.position = pos;


                Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
                m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする

            }
            else
            {
                h = 0;
                v = 0;
                Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
                m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする

            }
        }
        //else
        //{
        //    h = 0;
        //    v = 0;
        //    Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
        //    m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
        //}
    }
