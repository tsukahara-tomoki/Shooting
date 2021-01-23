using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// シューティングゲームの自機を操作するためのコンポーネント
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 5f;
    [SerializeField] Vector3[] pos = new Vector3[60];
    Vector3 pos1;
    private int i = 0;
    [SerializeField] bool moving = true;
    [SerializeField] GameObject nextObject;
    AudioSource m_audio;
    Rigidbody2D m_rb2d;

    [SerializeField] int m_delay = 30;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audio = GetComponent<AudioSource>();
        if(nextObject)Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir;
        float h;
        float v;
        // 自機を移動させる]
        //if (!lasernow)
        {
            h = Input.GetAxisRaw("Horizontal");   // 垂直方向の入力を取得する
            v = Input.GetAxisRaw("Vertical");     // 水平方向の入力を取得する
            dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
            m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
            if (nextObject)
            {

                if (pos[0] != transform.position)
                {
                    moving = true;
                    //if (lasernow)
                    //{
                    //    moving = false;
                    //}
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
}
