using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 10f;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] GameObject m_bulletPrefab2;
    [SerializeField] GameObject m_laserPrefab;
    [SerializeField] Transform[] m_muzzle = new Transform[8];
    readonly Vector3[] pos = new Vector3[30];
    Vector3 pos1;
    [SerializeField] GameObject nextObject;
    Rigidbody2D m_rb2d;
    [SerializeField] int m_delay = 30;
    bool moving = true;
    float fireTimer = 0;
    bool fireNow = true;
    float shotTimer = 0;
    bool shotNow = true;
    float laserTimer = 0;
    bool laserNow = true;
    [SerializeField] float fireDelay;
    [SerializeField] float shotDelay;
    [SerializeField] float laserDelay;
    [SerializeField] bool firstPlayer;
    float beingHitTimer = 0;
    bool beingHit = false;
    /// <summary>爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionEffect;
    int i = 0;
    OptionController optionController;
    bool dead = false;
    GameManager gameManager;
    SpriteRenderer spriteRenderer;
    /// <summary>
    /// 追従パターンを変える
    /// </summary>
    static int movePattern = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (nextObject)
        {
            optionController = nextObject.GetComponent<OptionController>();
        }
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        m_rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (nextObject) Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.start)
        {
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

            }
            if (shotTimer > shotDelay / gameManager.dNam1)    // 待つ
            {
                shotNow = false;
            }
            if (laserTimer > laserDelay / gameManager.dNam1)    // 待つ
            {
                laserNow = false;
            }
        }
        else
        {
            if (fireTimer > fireDelay / gameManager.dNam2)    // 待つ
            {
                fireNow = false;
            }
            if (shotTimer > shotDelay / gameManager.dNam2)    // 待つ
            {
                shotNow = false;
            }
            if (laserTimer > laserDelay / gameManager.dNam2)    // 待つ
            {
                laserNow = false;
            }
        }
        
    }
    private void FixedUpdate()
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
    void Fire()
    {
        if (dead) { return; }
        if (m_bulletPrefab) // m_bulletPrefab にプレハブが設定されている時
        {
            GameObject go = Instantiate(m_bulletPrefab, m_muzzle[0].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            //go.transform.SetParent(this.transform);
            //m_audio.Play();
        }
    }
    void Shot()
    {
        if (dead) { return; }
        GameObject go;
        if (m_bulletPrefab2) // m_bulletPrefab にプレハブが設定されている時
        {
            for (int i = 0; i < m_muzzle.Length; i++)
            {
                go = Instantiate(m_bulletPrefab2, m_muzzle[i].position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                //go.transform.SetParent(this.transform);
            }
        }
    }
    void Laser()
    {
        if (dead) { return; }
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
    public void Move(Vector3 pos, bool moving)
    {
        Vector2 pos1 = pos - transform.position;
        float h = pos1[0];
        float v = pos1[1];
        //if (!lasernow)
        //{
        switch (movePattern)
        {
            //パターン0 position
            case 0:
                if (moving)
                {
                    Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
                    m_rb2d.position = pos;                      //○○フレーム前の場所に直接移動
                }
                else
                {

                    m_rb2d.velocity = new Vector2(0, 0); // その場から動かない

                }
                break;
            //パターン1 velocity
            case 1:
                if (moving)
                {
                    Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
                    m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする

                }
                else
                {
                    //h = 0;
                    //v = 0;
                    //Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
                    //m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
                    m_rb2d.velocity = new Vector2(0, 0); // その場から動かない
                }
                break;

            default:
                break;
        }
        //if (moving)
        //{

        //    //transform.position = pos;

        //    //Debug.Log("うごく");
        //    Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
        //    m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
        //    //m_rb2d.position = pos;
        //    //Debug.Log(pos);
        //}
        //else
        //{
        //    h = 0;
        //    v = 0;
        //    Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
        //    m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする

        //}
    }
    //else
    //{
    //    h = 0;
    //    v = 0;
    //    Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
    //    m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
    //}
    void Hit(string name, string bulletName)
    {

        // GameManager にやられたことを知らせる
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject)
        {
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager)
            {
                if (bulletName == "2Pbullet" || bulletName == "1Pbullet")
                {
                    switch (name)
                    {
                        case "Option":
                            gameManager.OptionHit1P1(10);
                            break;
                        case "Option (1)":
                            gameManager.OptionHit1P2(10);
                            break;
                        case "Option (2)":
                            gameManager.OptionHit1P3(10);
                            break;
                        case "Option (3)":
                            gameManager.OptionHit1P4(10);
                            break;
                        case "Option2P (1)":
                            gameManager.OptionHit2P1(10);
                            break;
                        case "Option2P (2)":
                            gameManager.OptionHit2P2(10);
                            break;
                        case "Option2P (3)":
                            gameManager.OptionHit2P3(10);
                            break;
                        case "Option2P (4)":
                            gameManager.OptionHit2P4(10);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Option":
                            gameManager.OptionHit1P1(7);
                            break;
                        case "Option (1)":
                            gameManager.OptionHit1P2(7);
                            break;
                        case "Option (2)":
                            gameManager.OptionHit1P3(7);
                            break;
                        case "Option (3)":
                            gameManager.OptionHit1P4(7);
                            break;
                        case "Option2P (1)":
                            gameManager.OptionHit2P1(7);
                            break;
                        case "Option2P (2)":
                            gameManager.OptionHit2P2(7);
                            break;
                        case "Option2P (3)":
                            gameManager.OptionHit2P3(7);
                            break;
                        case "Option2P (4)":
                            gameManager.OptionHit2P4(7);
                            break;
                        default:
                            break;
                    }
                }
                


            }
        }
    }
    void LaserHit(string name )
    {

        // GameManager にやられたことを知らせる
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject)
        {
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager)
            {
                switch (name)
                {
                    case "Option":
                        gameManager.OptionHit1P1(1);
                        break;
                    case "Option (1)":
                        gameManager.OptionHit1P2(1);
                        break;
                    case "Option (2)":
                        gameManager.OptionHit1P3(1);
                        break;
                    case "Option (3)":
                        gameManager.OptionHit1P4(1);
                        break;
                    case "Option2P (1)":
                        gameManager.OptionHit2P1(1);
                        break;
                    case "Option2P (2)":
                        gameManager.OptionHit2P2(1);
                        break;
                    case "Option2P (3)":
                        gameManager.OptionHit2P3(1);
                        break;
                    case "Option2P (4)":
                        gameManager.OptionHit2P4(1);
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

        if (m_explosionEffect)
        {
            Instantiate(m_explosionEffect, this.transform.position, Quaternion.identity);
        }
        //CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        //circleCollider2D.enabled = false;
        this.gameObject.tag = "Die";
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        if (firstPlayer)
        {
            gameManager.dNam1 *= 2;
            gameManager.SetCool(1);
        }
        else
        {
            gameManager.dNam2 *= 2;
            gameManager.SetCool(2);
        }

        dead = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead) { return; }
        if (beingHit) return;
        beingHit = true;
        beingHitTimer = 0;
        Thin(0.5f);
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2Pbullet")
            {
                Hit(this.gameObject.name, collision.gameObject.tag);
            }
            if (collision.gameObject.tag == "2PsubBullet")
            {
                Hit(this.gameObject.name, collision.gameObject.tag);
            }
        }
        else
        {
            if (collision.gameObject.tag == "1Pbullet")
            {
                Hit(this.gameObject.name, collision.gameObject.tag);
            }
            if (collision.gameObject.tag == "1PsubBullet")
            {
                Hit(this.gameObject.name, collision.gameObject.tag);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dead) { return; }
        //if (firstPlayer)
        //{
        //    if (collision.gameObject.tag == "2Plaser")
        //    {
        //        LaserHit(this.gameObject.name);
        //    }
        //}
        //else
        {
            if (collision.gameObject.tag == "laser")
            {
                LaserHit(this.gameObject.name);
            }
        }

    }
    void Thin(float f)
    {
        spriteRenderer.color = new Color(1, 1, 1, f);
    }
}
