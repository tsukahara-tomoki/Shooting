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
    [SerializeField] GameObject m_bulletPrefab2;
    [SerializeField] Transform[] m_muzzle = new Transform[8];
    [SerializeField] Vector3[] pos = new Vector3[30];
    Vector3 pos1;
    private int i = 0;
    [SerializeField] bool moving = true;
    [SerializeField] GameObject nextObject;
    AudioSource m_audio;
    Rigidbody2D m_rb2d;
    [SerializeField] float fireDelay;
    [SerializeField] float shotDelay;
    float fireTimer = 0;
    bool fireNow = false;
    float shotTimer = 0;
    bool shotNow = false;
    [SerializeField] bool firstPlayer;
    int j = 0;
    OptionController optionController;
    [SerializeField] int m_delay = 30;
    /// <summary>爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionEffect;
    bool dead = false;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audio = GetComponent<AudioSource>();
        if (nextObject)
        {
            optionController = nextObject.GetComponent<OptionController>();
        }
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        if (nextObject)Initialize();
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
            if (firstPlayer)
            {
                h = Input.GetAxisRaw("Horizontal1P");   // 垂直方向の入力を取得する
                v = Input.GetAxisRaw("Vertical1P");     // 水平方向の入力を取得する
            }
            else
            {
                h = Input.GetAxisRaw("Horizontal2P");   // 垂直方向の入力を取得する
                v = Input.GetAxisRaw("Vertical2P");
                v *= -1;// 水平方向の入力を取得する
            }
            dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
            m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
            // 左クリックまたは左 Ctrl で弾を発射する（単発）
            if (firstPlayer)
            {
                if (Input.GetButton("Fire1"))
                {
                    //if (this.GetComponentsInChildren<BulletController>().Length < m_bulletLimit)    // 画面内の弾数を制限する
                    {
                        if (!fireNow)
                        {
                            Fire();
                            fireNow = true;
                            fireTimer = 0f;   // タイマーをリセットする
                        }
                        //Fire();
                    }
                }
                if (Input.GetButton("Fire2"))
                {
                    if (!shotNow)
                    {
                        Shot();
                        shotNow = true;
                        shotTimer = 0f;   // タイマーをリセットする
                    }

                }
            }
            else
            {
                if (Input.GetButton("2PFire1"))
                {
                    { 
                        if (!fireNow)
                        {
                            Fire();
                            fireNow = true;
                            fireTimer = 0f;   // タイマーをリセットする
                        }
                        //Fire();
                    }
                }
                if (Input.GetButton("2PFire2"))
                {
                    if (!shotNow)
                    {
                        Shot();
                        shotNow = true;
                        shotTimer = 0f;   // タイマーをリセットする
                    }

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
                    optionController.Move(pos[m_delay], moving);

                    Buffer();
                }
                else
                {
                moving = false;
                optionController.Move(pos[m_delay], moving);
                }

            }
        }
        fireTimer += Time.deltaTime;
        shotTimer += Time.deltaTime;
        if (firstPlayer)
        {
            if (fireTimer > fireDelay / (float)gameManager.dNam1)    // 待つ
            {
                fireNow = false;
                
            }
            if (shotTimer > shotDelay/gameManager.dNam1)    // 待つ
            {
                shotNow = false;
            }
        }
        else
        {
            if (fireTimer > fireDelay/ gameManager.dNam2)    // 待つ
            {
                fireNow = false;
            }
            if (shotTimer > shotDelay/ gameManager.dNam2)    // 待つ
            {
                shotNow = false;
            }
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
    void Shot()
    {
        GameObject go;
        if (m_bulletPrefab2) // m_bulletPrefab にプレハブが設定されている時
        {
            for (int i = 0; i < m_muzzle.Length; i++)
            {
                go = Instantiate(m_bulletPrefab2, m_muzzle[i].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                go.transform.SetParent(this.transform);
            }
        }
    }
    void Initialize()
    {
        for (int i = 29; i >= 0; i--)
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
        for (int i = 29; i > 0; i--)
        {
            pos[i] = pos[i - 1];
        }
        pos[0] = transform.position;
    }
    void Hit(int i)
    {

        // GameManager にやられたことを知らせる
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject)
        {
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager)
            {
                if (j == 0){ j++;return; }
                j = 0;
                switch (i)
                {
                    case 1:
                        gameManager.PlayerHit1P();
                        break;
                    case 2:
                        gameManager.PlayerHit2P();
                        Debug.Log("は?");
                        break;
                    default:
                        break;
                }

                
            }
        }
    }
    public void PlayerDestroy()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2Pbullet")
            {
                Hit(1);
            }
        }
        else
        {
            if (collision.gameObject.tag == "1Pbullet")
            {
                Hit(2);
            }
        }
    }
}

