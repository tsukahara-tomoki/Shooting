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
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Transform[] m_muzzle = new Transform[8];
    [SerializeField] Vector3[] pos = new Vector3[60];
    Vector3 pos1;
    private int i = 0;
    [SerializeField] bool moving = true;
    [SerializeField] GameObject nextObject;
    AudioSource m_audio;
    Rigidbody2D m_rb2d;
    float fireTimer = 0;
    bool fireDelay = false;

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
            // 左クリックまたは左 Ctrl で弾を発射する（単発）
            if (Input.GetButton("Fire1"))
            {
                //if (this.GetComponentsInChildren<BulletController>().Length < m_bulletLimit)    // 画面内の弾数を制限する
                {
                    if (!fireDelay)
                    {
                        Fire();
                        fireDelay = true;
                        fireTimer = 0f;   // タイマーをリセットする
                    }
                    //Fire();
                }
            }
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
        fireTimer += Time.deltaTime;
        if (fireTimer > 0.3)    // 待つ
        {
            fireDelay = false;
        }

    }
    void Fire()
    {
        if (m_bulletPrefab) // m_bulletPrefab にプレハブが設定されている時
        {
            GameObject go = Instantiate(m_bulletPrefab, m_muzzle[0].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            //m_audio.Play();
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
