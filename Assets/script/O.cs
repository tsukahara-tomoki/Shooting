using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 10f;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] GameObject m_bulletPrefab2;
    [SerializeField] GameObject m_laserPrefab;
    /// <summary>弾の発射位置</summary>
    [SerializeField] Transform m_muzzle;
    [SerializeField] Transform m_muzzle1;
    [SerializeField] Transform m_muzzle2;
    [SerializeField] Transform m_muzzle3;
    [SerializeField] Transform m_muzzle4;
    [SerializeField] Transform m_muzzle5;
    [SerializeField] Transform m_muzzle6;
    [SerializeField] Transform m_muzzle7;
    [SerializeField] Transform m_muzzle8;
    [SerializeField] GameObject m_explosionEffect;
    [SerializeField] bool m_godMode;    // ← このメンバ変数を追加する
    bool lasernow = false;
    bool laserwait = false;
    bool ammunition = false;
    //bool fire = true;
    //bool shot = true;
    Vector3[] pos = new Vector3[60];
    Vector3 pos1;
    /// <summary>タイマー</summary>
    //float m_timer;
    //float m_timer1;
    //float m_timer2;
    //float m_timer3;
    [SerializeField] bool moving = true;
    [SerializeField] int m_ammunitionTime = 1;
    [SerializeField] GameObject nextObject;
    AudioSource m_audio;
    Rigidbody2D m_rb2d;
    [SerializeField] int m_delay;
    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audio = GetComponent<AudioSource>();
        if (nextObject) Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir;
        float h;
        float v;
        if (!lasernow)
        {
            //m_timer2 += Time.deltaTime;
            //if (m_timer2 > 0.5)    // 待つ
            //{
            //    //fire = true;
            //}
            // 左クリックまたは左 Ctrl で弾を発射する（単発）
            if (Input.GetButton("Fire1"))
            {
                //if (this.GetComponentsInChildren<BulletController>().Length < m_bulletLimit)    // 画面内の弾数を制限する
                {
                    //if (fire)
                    {
                        Fire();
                        //fire = false;
                        //m_timer2 = 0f;   // タイマーをリセットする
                    }
                }
            }

            
            // 右クリックまたは左 Alt で弾を発射する（散弾）
            if (Input.GetButton("Fire2"))
            {
                //if (fire)
                {
                    Shot();
                    //fire = false;
                    //m_timer2 = 0f;   // タイマーをリセットする
                }
            }
        }
        //else
        //{
        //    h = 0;
        //    v = 0;
        //    dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
        //    m_rb2d.velocity = dir * m_moveSpeed;
        //}

        //m_timer += Time.deltaTime;
        //if (m_timer > m_ammunitionTime)    // 待つ
        //{
        //    ammunition = false;
        //    m_timer = 0f;   // タイマーをリセットする
        //}
        //m_timer3 += Time.deltaTime;
        //if (m_timer3 > 2)    // 待つ
        //{
        //    lasernow = false;
        //    m_timer3 = 0f;   // タイマーをリセットする
        //}
        //if (Input.GetButton("Fire3"))
        //{

        //    if (fire)
        //    {
        //        lasernow = true;
        //        laserwait = true;
        //        //Fire3();
        //        fire = false;
        //        m_timer3 = 0f;   // タイマーをリセットする
        //    }


        //}
        //if (laserwait)
        //{
        //    LaserFire();
        //}
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
    /// <summary>
    /// 弾を発射して、発射音を鳴らす
    /// </summary>
    void Fire()
    {
        if (m_bulletPrefab) // m_bulletPrefab にプレハブが設定されている時
        {
            GameObject go = Instantiate(m_bulletPrefab, m_muzzle.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            //m_audio.Play();
        }
    }
    void Shot()
    {
        if (m_bulletPrefab2) // m_bulletPrefab にプレハブが設定されている時
        {
            GameObject go = Instantiate(m_bulletPrefab2, m_muzzle1.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            go = Instantiate(m_bulletPrefab2, m_muzzle2.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            go = Instantiate(m_bulletPrefab2, m_muzzle3.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            go = Instantiate(m_bulletPrefab2, m_muzzle4.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            go = Instantiate(m_bulletPrefab2, m_muzzle5.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            go = Instantiate(m_bulletPrefab2, m_muzzle6.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            go = Instantiate(m_bulletPrefab2, m_muzzle7.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            go = Instantiate(m_bulletPrefab2, m_muzzle8.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.SetParent(this.transform);
            //m_audio.Play();
        }
    }
    //void LaserFire()
    //{
    //    if (m_laserPrefab) // m_bulletPrefab にプレハブが設定されている時
    //    {
    //        //if (m_timer3 > 1)
    //        {
    //            GameObject go = Instantiate(m_laserPrefab, m_muzzle.position, m_bulletPrefab.transform.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
    //            go.transform.SetParent(this.transform);
    //            //m_audio.Play();
    //            //laserwait = false;
    //        }

    //    }
    //}
    void Initialize()
    {
        for (int i = 59; i >= 0; i--)
        {
            pos[i] = transform.position;
        }
    }
    void Buffer()
    {
        for (int i = 59; i > 0; i--)
        {
            pos[i] = pos[i - 1];
        }
        pos[0] = transform.position;
    }
    public void move(Vector3 pos, bool moving)
    {
        Vector3 pos1 = pos - transform.position;
        float h = pos1[0];
        float v = pos1[1];
        if (!lasernow)
        {
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
        else
        {
            h = 0;
            v = 0;
            Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction)*/
            m_rb2d.velocity = dir * m_moveSpeed; // 単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
        }
    }
}
