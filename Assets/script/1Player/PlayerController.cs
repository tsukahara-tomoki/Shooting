using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject m_laserPrefab;
    [SerializeField] Transform[] m_muzzle = new Transform[8];
    [SerializeField] Vector3[] pos = new Vector3[30];
    Vector3 pos1;
    private int i = 0;
    [SerializeField] bool moving = true;
    [SerializeField] GameObject nextObject;
    AudioSource m_audio;
    Rigidbody2D m_rb2d;
    float m_timer = 0;
    [SerializeField] float fireDelay;
    [SerializeField] float shotDelay;
    [SerializeField] float laserDelay;
    float fireTimer = 0;
    bool fireNow = true;
    float shotTimer = 0;
    bool shotNow = true;
    float laserTimer = 0;
    bool laserNow = true;
    [SerializeField] bool firstPlayer;
    int j = 0;
    OptionController optionController;
    [SerializeField] int m_delay = 30;
    /// <summary>爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionEffect;
    public　bool dead = false;
    GameManager gameManager;
    [SerializeField] GameObject winnerText;
    [SerializeField] Slider CT1;
    [SerializeField] Slider CT2;
    [SerializeField] Slider CT3;
    [SerializeField] Slider CT4;
    [SerializeField] Slider CT5;
    [SerializeField] Slider CT6;
    bool gameStart = true;
    Vector2 dir;
    float beingHitTimer = 0;
    bool beingHit = false;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (dead) return;
        float h;
        float v;
        if (gameStart)
        {
            //GameStart();
        }
        // 自機を移動させる]
        //if (!lasernow)]
        else
        {
            
            if (firstPlayer)
            {
                h = Input.GetAxisRaw("Horizontal1P");   // 垂直方向の入力を取得する
                v = Input.GetAxisRaw("Vertical1P");     // 水平方向の入力を取得する
                dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
                m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
            }
            else
            {
                h = Input.GetAxisRaw("Horizontal2P");   // 垂直方向の入力を取得する
                v = Input.GetAxisRaw("Vertical2P");// 水平方向の入力を取得する
                dir = new Vector2(-h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
                m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
            }
            if (gameManager.start)
            {
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
                    if (Input.GetButton("Fire3"))
                    {
                        if (!laserNow)
                        {
                            Laser();
                            laserNow = true;
                            laserTimer = 0f;   // タイマーをリセットする
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
                    if (Input.GetButton("2PFire3"))
                    {
                        if (!laserNow)
                        {
                            Laser();
                            laserNow = true;
                            laserTimer = 0f;   // タイマーをリセットする
                        }

                    }
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

        fireTimer += Time.deltaTime;
        shotTimer += Time.deltaTime;
        laserTimer += Time.deltaTime;
        beingHitTimer += Time.deltaTime;
        if (beingHit)
        {
            if (beingHitTimer > 0.2)
            {
                beingHit = false;
                Thin(1f);
            }
        }
        if (firstPlayer)
        {
            if (fireTimer > fireDelay / (float)gameManager.dNam1)    // 待つ
            {
                fireNow = false;
                Ctime1(1, fireDelay / (float)gameManager.dNam1);
            }
            else
            {
                Ctime1(1, fireTimer);
            }
            if (shotTimer > shotDelay/gameManager.dNam1)    // 待つ
            {
                shotNow = false;
                Ctime1(2, shotDelay / (float)gameManager.dNam1);
            }
            else
            {
                Ctime1(2, shotTimer);
            }
            if (laserTimer > laserDelay / gameManager.dNam1)    // 待つ
            {
                laserNow = false;
                Ctime1(3, laserDelay / (float)gameManager.dNam1);
            }
            else
            {
                Ctime1(3, laserTimer);
            }
        }
        else
        {
            if (fireTimer > fireDelay/ gameManager.dNam2)    // 待つ
            {
                fireNow = false;
                Ctime1(4, fireDelay / (float)gameManager.dNam2);
            }
            else
            {
                Ctime1(4, fireTimer);
            }
            if (shotTimer > shotDelay/ gameManager.dNam2)    // 待つ
            {
                shotNow = false;
                Ctime1(5, shotDelay / (float)gameManager.dNam2);
            }
            else
            {
                Ctime1(5, shotTimer);
            }
            if (laserTimer > laserDelay / gameManager.dNam2)    // 待つ
            {
                laserNow = false;
                Ctime1(6, laserDelay / (float)gameManager.dNam2);
            }
            else
            {
                Ctime1(6, laserTimer);
            }
        }

    }
    private void FixedUpdate()
    {
        if (gameStart)
        {
            GameStart();
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
    void Laser()
    {
        GameObject go;
        if (m_laserPrefab) // m_bulletPrefab にプレハブが設定されている時
        {
            //for (int i = 0; i < m_muzzle.Length; i++)
            {
                go = Instantiate(m_laserPrefab, m_muzzle[0].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
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
        if (dead) return;
        // GameManager にやられたことを知らせる
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject)
        {
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager)
            {
                //if (j == 0){ j++;return; }
                //j = 0;
                switch (i)
                {
                    case 1:
                        gameManager.PlayerHit1P(5);
                        break;
                    case 2:
                        gameManager.PlayerHit2P(5);
                        break;
                    default:
                        break;
                }

                
            }
        }
    }
    void LaserHit(int i)
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject)
        {
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager)
            {
                //if (j == 0){ j++;return; }
                //j = 0;
                switch (i)
                {
                    case 1:
                        gameManager.PlayerHit1P(1);
                        break;
                    case 2:
                        gameManager.PlayerHit2P(1);
                        break;
                    default:
                        break;
                }


            }
        }
    }
    public void PlayerDestroy()
    {
        GetComponent<Renderer>().material.color = new Color32(0, 0, 0, 0);
        // 爆発エフェクトを置く
        dead = true;
        if (m_explosionEffect)
        {
            Instantiate(m_explosionEffect, this.transform.position, Quaternion.identity);
        }
        winnerText.SetActive(true);
        Text text = winnerText.GetComponent<Text>();
        if (firstPlayer)
        {
            text.text = "2P  WIN";
        }
        else
        {
            text.text = "1P  WIN";
        }

    }
    void Ctime1(int i, float f)
    {
        switch (i)
        {
            case 1:
                CT1.value = f * -1;
                break;
            case 2:
                CT2.value = f * -1;
                break;
            case 3:
                CT3.value = f * -1;
                break;
            case 4:
                CT4.value = f * -1;
                break;
            case 5:
                CT5.value = f * -1;
                break;
            case 6:
                CT6.value = f * -1;
                break;
            default:
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (beingHit) return;
        beingHit = true;
        beingHitTimer = 0;
        Thin(0.5f);
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2Pbullet")
            {
                Hit(1);
            }
            if (collision.gameObject.tag == "2PsubBullet")
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
            if (collision.gameObject.tag == "1PsubBullet")
            {
                Hit(2);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2Plaser")
            {
                LaserHit(1);
                //Debug.Log("l");
            }

        }
        else
        {
            if (collision.gameObject.tag == "laser")
            {
                LaserHit(2);
                //Debug.Log("l");
            }
        }
        
    }
    void Thin(float f)
    {
        spriteRenderer.color = new Color(1, 1, 1, f);
    }
    void GameStart()
    {

        if (firstPlayer)
        {
            if (this.gameObject.transform.position.x > -4.5)
            {
                gameObject.transform.position -= new Vector3(0.15f,0);
            }
            else
            {
                gameStart = false;
                gameManager.GameStart();
            }
        }
        else
        {
            if (this.gameObject.transform.position.x < 4.5)
            {
                gameObject.transform.position += new Vector3(0.15f, 0);
            }
            else
            {
                gameStart = false;
                gameManager.GameStart();
            }
        }
    }
}

