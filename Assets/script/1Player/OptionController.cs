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
    bool  moving = true;
    float fireTimer = 0;
    bool fireNow = false;
    float shotTimer = 0;
    bool shotNow = false;
    float laserTimer = 0;
    bool laserNow = false;
    [SerializeField] float fireDelay;
    [SerializeField] float shotDelay;
    [SerializeField] float laserDelay;
    [SerializeField] bool firstPlayer;
    /// <summary>爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionEffect;
    int i = 0;
    OptionController optionController;
    bool dead = false;
    GameManager gameManager;
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
        if (nextObject) Initialize();
    }

    // Update is called once per frame
    void Update()
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

        fireTimer += Time.deltaTime;
        shotTimer += Time.deltaTime;
        laserTimer += Time.deltaTime;
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
        if (dead){ return; }
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
        Vector3 pos1 = pos - transform.position;
        float h = pos1[0];
        float v = pos1[1];
        //if (!lasernow)
        //{
            if (moving)
            {

            //transform.position = pos;

                //Debug.Log("うごく");
                Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
                //m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
                m_rb2d.position = pos;
                //Debug.Log(pos);
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
    void Hit(string name)
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
                        gameManager.OptionHit1P1(5);
                        break;
                    case "Option (1)":
                        gameManager.OptionHit1P2(5);
                        break;
                    case "Option (2)":
                        gameManager.OptionHit1P3(5);
                        break;
                    case "Option (3)":
                        gameManager.OptionHit1P4(5);
                        break;
                    case "Option2P (1)":
                        gameManager.OptionHit2P1(5);
                        break;
                    case "Option2P (2)":
                        gameManager.OptionHit2P2(5);
                        break;
                    case "Option2P (3)":
                        gameManager.OptionHit2P3(5);
                        break;
                    case "Option2P (4)":
                        gameManager.OptionHit2P4(5);
                        break;
                    default:
                        break;
                }


            }
        }
    }
    void LaserHit(string name)
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
        }
        else
        {
            gameManager.dNam2 *= 2;
        }
        
        dead = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead) { return; }
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2Pbullet")
            {
                Hit(this.gameObject.name);
            }
            if (collision.gameObject.tag == "2PsubBullet")
            {
                Hit(this.gameObject.name);
            }
        }
        else
        {
            if (collision.gameObject.tag == "1Pbullet")
            {
                Hit(this.gameObject.name);
            }
            if (collision.gameObject.tag == "1PsubBullet")
            {
                Hit(this.gameObject.name);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dead) { return; }
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2Plaser")
            {
                LaserHit(this.gameObject.name);
                Debug.Log("l");
            }

        }
        else
        {
            if (collision.gameObject.tag == "laser")
            {
                LaserHit(this.gameObject.name);
                Debug.Log("l");
            }
        }

    }
}
