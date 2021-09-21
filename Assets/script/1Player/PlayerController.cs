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
    [SerializeField] GameObject[] optionObject = new GameObject[4];

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
    float intervalTimer = 0;
    [SerializeField] bool firstPlayer;
    int j = 0;
    OptionController[] optionController = new OptionController[4];
    [SerializeField] int m_delay = 30;
    /// <summary>爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionEffect;
    public　bool dead = false;
    GameManager gameManager;
    [SerializeField] GameObject winnerText;
    [SerializeField] Slider[] m_CoolBar = new Slider[3];
    //[SerializeField] Slider CT1;
    //[SerializeField] Slider CT2;
    //[SerializeField] Slider CT3;
    //[SerializeField] Slider CT4;
    //[SerializeField] Slider CT5;
    //[SerializeField] Slider CT6;
    bool gameStart = true;
    Vector2 dir;
    float beingHitTimer = 0;
    bool beingHit = false;
    SpriteRenderer spriteRenderer;
    int m_ShotPoint = 0;
    //[SerializeField]Slider[] spSlider = new Slider[2];
    [SerializeField] GameObject spSlider;
    MpRegeneration mpRegeneration;
    /// <summary>SP自然回復の初期値    /// </summary>
    //[SerializeField] float m_RecoveryIntervalSP;
    [SerializeField] int m_RecoverylSP;
    [SerializeField] int[] m_;
    // Start is called before the first frame update

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //if (nextObject)
        //{
        //    optionController = nextObject.GetComponent<OptionController>();
        //}
        for (int i = 0; i < optionObject.Length; i++)
        {
            optionController[i] = optionObject[i].GetComponent<OptionController>(); ;
        }
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        mpRegeneration = spSlider.GetComponent<MpRegeneration>();
        if (nextObject)Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //if (dead) return;
        float h;
        float v;

        // 自機を移動させる]
        //if (!lasernow)]
        if (!gameStart)
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
                dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
                m_rb2d.velocity = dir * -m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
            }
            if (dead) return;
            if (gameManager.start)
            {
                // 左クリックまたは左 Ctrl で弾を発射する（単発）
                if (firstPlayer)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        //if (this.GetComponentsInChildren<BulletController>().Length < m_bulletLimit)    // 画面内の弾数を制限する
                        {
                            if (!fireNow && mpRegeneration.m_Mp > 50)
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
                        if (!shotNow && mpRegeneration.m_Mp > 200)
                        {
                            Shot();
                            shotNow = true;
                            shotTimer = 0f;   // タイマーをリセットする
                        }

                    }
                    if (Input.GetButton("Fire3"))
                    {
                        if (!laserNow && mpRegeneration.m_Mp > 2000)
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
                            if (!fireNow && mpRegeneration.m_Mp > 50)
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
                        if (!shotNow && mpRegeneration.m_Mp > 200)
                        {
                            Shot();
                            shotNow = true;
                            shotTimer = 0f;   // タイマーをリセットする
                        }

                    }
                    if (Input.GetButton("2PFire3"))
                    {
                        if (!laserNow && mpRegeneration.m_Mp > 2000)
                        {
                            Laser();
                            laserNow = true;
                            laserTimer = 0f;   // タイマーをリセットする
                        }

                    }
                }
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
            if (fireTimer > fireDelay / gameManager.dNam2)    // 待つ
            {
                fireNow = false;
                Ctime1(1, fireDelay / (float)gameManager.dNam2);
            }
            else
            {
                Ctime1(1, fireTimer);
            }
            if (shotTimer > shotDelay / gameManager.dNam2)    // 待つ
            {
                shotNow = false;
                Ctime1(2, shotDelay / (float)gameManager.dNam2);
            }
            else
            {
                Ctime1(2, shotTimer);
            }
            if (laserTimer > laserDelay / gameManager.dNam2)    // 待つ
            {
                laserNow = false;
                Ctime1(3, laserDelay / (float)gameManager.dNam2);
            }
            else
            {
                Ctime1(3, laserTimer);
            }

        }
        intervalTimer += Time.deltaTime;
        if (0.5 < intervalTimer)
        {
            m_ShotPoint+= m_RecoverylSP;
            intervalTimer = 0;
            //Debug.Log(m_ShotPoint);
        }

    }
    private void FixedUpdate()
    {
        if (gameStart)
        {
            GameStart();
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
                optionController[0].Move(pos[m_delay], moving);

                Buffer();
            }
            else
            {
                moving = false;
                optionController[0].Move(pos[m_delay], moving);
            }

        }
    }
    void Fire()
    {
        if (mpRegeneration.m_sliderR.value < mpRegeneration.m_Mp) mpRegeneration.m_sliderR.value = mpRegeneration.m_Mp;
        mpRegeneration.m_Mp -= 50;
        if (m_bulletPrefab) // m_bulletPrefab にプレハブが設定されている時
        {
            GameObject go = Instantiate(m_bulletPrefab, m_muzzle[0].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            foreach (var item in optionController)
            {
                item.FireCall(0);
            }
            
            //m_audio.Play();
        }
    }
    void Shot()
    {
        if (mpRegeneration.m_sliderR.value < mpRegeneration.m_Mp) mpRegeneration.m_sliderR.value = mpRegeneration.m_Mp;
        mpRegeneration.m_Mp -= 200;
        GameObject go;
        if (m_bulletPrefab2) // m_bulletPrefab にプレハブが設定されている時
        {
            for (int i = 0; i < m_muzzle.Length; i++)
            {
                go = Instantiate(m_bulletPrefab2, m_muzzle[i].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                go.transform.SetParent(this.transform);
            }
            foreach (var item in optionController)
            {
                item.FireCall(1);
            }
        }
    }
    void Laser()
    {
        if (mpRegeneration.m_sliderR.value < mpRegeneration.m_Mp) mpRegeneration.m_sliderR.value = mpRegeneration.m_Mp;
        mpRegeneration.m_Mp -= 2000;
        GameObject go;
        if (m_laserPrefab) // m_bulletPrefab にプレハブが設定されている時
        {
            //for (int i = 0; i < m_muzzle.Length; i++)
            {
                go = Instantiate(m_laserPrefab, m_muzzle[0].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                go.transform.SetParent(this.transform);
                foreach (var item in optionController)
                {
                    item.FireCall(2);
                }
            }
        }
    }
    void Initialize()
    {
        for (float i = 29; i >= 0; i--)
        {
            if (firstPlayer)
            {
                pos[(int)i] = transform.position + new Vector3(i * 0.15f, 0, 0);
            }
            else
            {
                pos[(int)i] = transform.position - new Vector3(i * 0.15f, 0, 0);
            }
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
    void Hit(int i ,bool b)
    {
        //Debug.Log("Hit");
        if (dead) return;
        Thin(0.5f);
        
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
                        if (b)
                        {
                            gameManager.PlayerHit1P(10);
                        }
                        else
                        {
                            gameManager.PlayerHit1P(7);
                        }
                        break;
                    case 2:
                        if (b)
                        {
                            gameManager.PlayerHit2P(10);
                        }
                        else
                        {
                            gameManager.PlayerHit2P(7);
                        }
                        
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
                m_CoolBar[0].value = f * -1;
                break;
            case 2:
                m_CoolBar[1].value = f * -1;
                break;
            case 3:
                m_CoolBar[2].value = f * -1;
                break;
            //case 4:
            //    CT4.value = f * -1;
            //    break;
            //case 5:
            //    CT5.value = f * -1;
            //    break;
            //case 6:
            //    CT6.value = f * -1;
            //    break;
            default:
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Hit");
        if (beingHit) return;
        beingHit = true;
        beingHitTimer = 0;
        //Thin(0.5f);
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2Pbullet")
            {
                Hit(1 ,true);
            }
            else if (collision.gameObject.tag == "2PsubBullet")
            {
                Hit(1 ,false);
            }
            else
            {
                beingHit = false;
            }
            
        }
        else
        {
            if (collision.gameObject.tag == "1Pbullet")
            {
                //Debug.Log("Hit");
                Hit(2, true);
            }
            else if (collision.gameObject.tag == "1PsubBullet")
            {
                Hit(2, false);
            }
            else
            {
                beingHit = false;
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "laser")
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
                gameManager.GameStartBefore();
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
                gameManager.GameStartBefore();
            }
        }
    }
}

