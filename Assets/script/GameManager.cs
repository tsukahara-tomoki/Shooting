using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体を管理するクラス。
/// EnemyGenerator と同じ GameObject にアタッチする必要がある。
/// </summary>
public class GameManager : MonoBehaviour 
{
    /// <summary>
    /// ライフ
    /// </summary>
    [SerializeField] int[] m_Playerlife = new int[10];
    //[SerializeField] int m_Playerlife1P;
    //[SerializeField] int m_Optionlife1P1;
    //[SerializeField] int m_Optionlife1P2;
    //[SerializeField] int m_Optionlife1P3;
    //[SerializeField] int m_Optionlife1P4;
    //[SerializeField] int m_Optionlife1P5;
    //[SerializeField] int m_Playerlife2P ;
    //[SerializeField] int m_Optionlife2P1;
    //[SerializeField] int m_Optionlife2P2;
    //[SerializeField] int m_Optionlife2P3;
    //[SerializeField] int m_Optionlife2P4;
    //[SerializeField] int m_Optionlife2P5;
    /// <summary> オブジェクト </summary>
    [SerializeField] GameObject[] playerObject = new GameObject[10];
    /*[SerializeField] GameObject Player1P;
    [SerializeField] GameObject option1p1;
    [SerializeField] GameObject option1p2;
    [SerializeField] GameObject option1p3;
    [SerializeField] GameObject option1p4;
    [SerializeField] GameObject Player2P;
    [SerializeField] GameObject option2p1;
    [SerializeField] GameObject option2p2;
    [SerializeField] GameObject option2p3;
    [SerializeField] GameObject option2p4;*/
    /// <summary>
    /// ライフテキスト
    /// </summary>
    [SerializeField] Text m_lifebar1PText;
    [SerializeField] Text m_optionLifebar1p1Text;
    [SerializeField] Text m_optionLifebar1p2Text;
    [SerializeField] Text m_optionLifebar1p3Text;
    [SerializeField] Text m_optionLifebar1p4Text;
    [SerializeField] Text m_optionLifebar1p5Text;
    [SerializeField] Text m_lifebar2PText;
    [SerializeField] Text m_optionLifebar2p1Text;
    [SerializeField] Text m_optionLifebar2p2Text;
    [SerializeField] Text m_optionLifebar2p3Text;
    [SerializeField] Text m_optionLifebar2p4Text;

    /// <summary>
    /// ライフバー
    /// </summary>
    [SerializeField] Slider m_lifebar1P;
    [SerializeField] Slider m_optionLifebar1p1;
    [SerializeField] Slider m_optionLifebar1p2;
    [SerializeField] Slider m_optionLifebar1p3;
    [SerializeField] Slider m_optionLifebar1p4;
    [SerializeField] Slider m_optionLifebar1p5;
    [SerializeField] Slider m_lifebar2P;
    [SerializeField] Slider m_optionLifebar2p1;
    [SerializeField] Slider m_optionLifebar2p2;
    [SerializeField] Slider m_optionLifebar2p3;
    [SerializeField] Slider m_optionLifebar2p4;

    public float dNam1 = 1;
    public float dNam2 = 1;
    public bool Ready1P;
    public bool Ready2P;

    [SerializeField] float fireDelay;
    [SerializeField] float shotDelay;
    [SerializeField] float laserDelay;

    [SerializeField] Slider[] m_CT = new Slider[6];
    //[SerializeField] Slider CT1;
    //[SerializeField] Slider CT2;
    //[SerializeField] Slider CT3;
    //[SerializeField] Slider CT4;
    //[SerializeField] Slider CT5;
    //[SerializeField] Slider CT6;
    [SerializeField] GameObject countObject;
    [SerializeField] GameObject countObject1;
    [SerializeField] Text countText;
    [SerializeField] Text countText1;
    [SerializeField] GameObject reloadButton;
    [SerializeField] GameObject titleButton;
    public bool start = false;
    int count = 3;

    /// <summary>タイマー</summary>
    float m_timer;
    /// <summary>ゲームの状態</summary>
    int m_status = 0;    // 0: ゲーム初期化前, 1: ゲーム初期化済み、ゲーム開始前, 2: ゲーム中, 3: プレイヤーがやられた

    void Start()
    {
        m_lifebar1PText.text = m_Playerlife[0].ToString();
        m_optionLifebar1p1Text.text = m_Playerlife[1].ToString();
        m_optionLifebar1p2Text.text = m_Playerlife[2].ToString();
        m_optionLifebar1p3Text.text = m_Playerlife[3].ToString();
        m_optionLifebar1p4Text.text = m_Playerlife[4].ToString();
        m_lifebar2PText.text = m_Playerlife[5].ToString();
        m_optionLifebar2p1Text.text = m_Playerlife[6].ToString();
        m_optionLifebar2p2Text.text = m_Playerlife[7].ToString();
        m_optionLifebar2p3Text.text = m_Playerlife[8].ToString();
        m_optionLifebar2p4Text.text = m_Playerlife[9].ToString();
        m_lifebar1P.maxValue = m_Playerlife[0];
        m_optionLifebar1p1.maxValue = m_Playerlife[1];
        m_optionLifebar1p2.maxValue = m_Playerlife[2];
        m_optionLifebar1p3.maxValue = m_Playerlife[3];
        m_optionLifebar1p4.maxValue = m_Playerlife[4];
        m_lifebar2P.maxValue = m_Playerlife[5];
        m_optionLifebar2p1.maxValue = m_Playerlife[6];
        m_optionLifebar2p2.maxValue = m_Playerlife[7];
        m_optionLifebar2p3.maxValue = m_Playerlife[8];
        m_optionLifebar2p4.maxValue = m_Playerlife[9];
        m_lifebar1P.value = m_Playerlife[0];
        m_optionLifebar1p1.value = m_Playerlife[1];
        m_optionLifebar1p2.value = m_Playerlife[2];
        m_optionLifebar1p3.value = m_Playerlife[3];
        m_optionLifebar1p4.value = m_Playerlife[4];
        m_lifebar2P.value = m_Playerlife[5];
        m_optionLifebar2p1.value = m_Playerlife[6];
        m_optionLifebar2p2.value = m_Playerlife[7];
        m_optionLifebar2p3.value = m_Playerlife[8];
        m_optionLifebar2p4.value = m_Playerlife[9];
        SetCool(1);
        SetCool(2);
        countText = countObject.GetComponent<Text>();
        countText1 = countObject1.GetComponent<Text>();

    }

    void Update()
    {
        if (m_status == 0)  // 初期化前
        {

            if (Ready1P && Ready2P)
            {
                m_status = 1;
                countObject.SetActive(true);
                countObject1.SetActive(true);
            }

        }
        else if (m_status == 1) // カウントダウン
        {
            m_timer += Time.deltaTime;
            if (m_timer > 1)
            {
                Count();
                m_timer = 0;
            }
            
        }
        else if (m_status == 3) // プレイヤーがやられた
        {
            
        }
    }


    /// <summary>
    /// 被弾時、外部から呼ばれる関数
    /// </summary>
    public void PlayerHit1P(int i)
    {
        m_Playerlife[0] -= i;    // 残機を減らす
        LifebarRenewal("1P");
        if (m_Playerlife[0] < 1)
            {
             
            //Debug.Log("PlayerDestroy.");
            var playerController1 = playerObject[0].GetComponent<PlayerController>();
            var playerController2 = playerObject[5].GetComponent<PlayerController>();
            playerController1.dead = true;
            playerController2.dead = true;
            //if (PlayerObject)
            //{
            GameOver();
            //m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する

            //}
        }
    }


    public void OptionHit1P1(int i)
    {

        m_Playerlife[1] -= i;    // 残機を減らす
        LifebarRenewal("1PO1");

    }

    public void OptionHit1P2(int i)
    {
        m_Playerlife[2] -= i;
        LifebarRenewal("1PO2");
        //Debug.Log(m_Optionlife1P2);

    }

    public void OptionHit1P3(int i)
    {
        m_Playerlife[3] -= i;
        LifebarRenewal("1PO3");
        //Debug.Log(m_Optionlife1P3);

    }

    public void OptionHit1P4(int i)
    {
        m_Playerlife[4] -= i;
        LifebarRenewal("1PO4");
        //Debug.Log(m_Optionlife1P4);

    }

    public void PlayerHit2P(int i)
    {
        m_Playerlife[5] -= i;
        LifebarRenewal("2P");
        if (m_Playerlife[5] < 1)
        {

            //Debug.Log("PlayerDestroy.");
            var playerController1 = playerObject[0].GetComponent<PlayerController>();
            var playerController2 = playerObject[5].GetComponent<PlayerController>();
            playerController1.dead = true;
            playerController2.dead = true;
            //if (PlayerObject)
            //{
            GameOver();
            //m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する

            //}
        }
        //Debug.Log(m_Playerlife2P);
    }


    public void OptionHit2P1(int i)
    {
        m_Playerlife[6] -= i;
        LifebarRenewal("2PO1");
        //Debug.Log(m_Optionlife2P1);
    }
    public void OptionHit2P2(int i)
    {
        m_Playerlife[7] -= i;
        LifebarRenewal("2PO2");
        //Debug.Log(m_Optionlife2P2);
    }

    public void OptionHit2P3(int i)
    {
        m_Playerlife[8]-= i;
        LifebarRenewal("2PO3");
        //Debug.Log(m_Optionlife2P3);
    }

    public void OptionHit2P4(int i)
    {
        m_Playerlife[9] -= i;
        LifebarRenewal("2PO4");
        //Debug.Log(m_Optionlife2P4);
    }

    /// <summary>
    /// シーン上にある敵と敵の弾を消す
    /// </summary>
    void ClearScene()
    {
        // 敵を消す
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var go in goArray)
        {
            Destroy(go);
        }

        // 敵の弾を消す
        goArray = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (var go in goArray)
        {
            Destroy(go);
        }
    }

    void LifebarRenewal(string objectName)
    {
        if (m_status == 3) return;
        switch (objectName)
        {
            case "1P":
                if (m_Playerlife[0] > 0)
                {
                    if (m_lifebar1PText)
                    {
                        m_lifebar1PText.text = m_Playerlife[0].ToString();  // 表示を更新する
                    }
                    m_lifebar1P.value = m_Playerlife[0];
                }
                else
                {
                    PlayerController playerController = playerObject[0].GetComponent<PlayerController>();
                    playerController.PlayerDestroy();
                    m_Playerlife[0] = 0;
                    if (m_lifebar1PText)
                    {
                        m_lifebar1PText.text = m_Playerlife[0].ToString();  // 表示を更新する
                    }
                    m_lifebar1P.value = m_Playerlife[0];
                }
                break;
            case "1PO1":
                if(m_Playerlife[1] > 0)
                {
                    if (m_optionLifebar1p1Text)
                    {
                        m_optionLifebar1p1Text.text = m_Playerlife[1].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p1.value = m_Playerlife[1];
                }
                else
                {
                    OptionController optionController = playerObject[1].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[1] = 0;
                    if (m_optionLifebar1p1Text)
                    {
                        m_optionLifebar1p1Text.text = m_Playerlife[1].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p1.value = m_Playerlife[1];
                }
                break;
            case "1PO2":
                if (m_Playerlife[2] > 0)
                {
                    if (m_optionLifebar1p2Text)
                    {
                        m_optionLifebar1p2Text.text = m_Playerlife[2].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p2.value = m_Playerlife[2];
                }
                else
                {
                    OptionController optionController = playerObject[2].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[2] = 0;
                    if (m_optionLifebar1p2Text)
                    {
                        m_optionLifebar1p2Text.text = m_Playerlife[2].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p2.value = m_Playerlife[2];
                }
                break;
            case "1PO3":
                if (m_Playerlife[3] > 0)
                {
                    if (m_optionLifebar1p3Text)
                    {
                        m_optionLifebar1p3Text.text = m_Playerlife[3].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p3.value = m_Playerlife[3];
                }
                else
                {
                    OptionController optionController = playerObject[3].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[3] = 0;
                    if (m_optionLifebar1p3Text)
                    {
                        m_optionLifebar1p3Text.text = m_Playerlife[3].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p3.value = m_Playerlife[3];
                }
                break;
            case "1PO4":
                if (m_Playerlife[4] > 0)
                {
                    if (m_optionLifebar1p4Text)
                    {
                        m_optionLifebar1p4Text.text = m_Playerlife[4].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p4.value = m_Playerlife[4];
                }
                else
                {
                    OptionController optionController = playerObject[4].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[4] = 0;
                    if (m_optionLifebar1p4Text)
                    {
                        m_optionLifebar1p4Text.text = m_Playerlife[4].ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p4.value = m_Playerlife[4];
                }
                break;
            case "2P":
                if (m_Playerlife[5] > 0)
                {
                    if (m_lifebar2PText)
                    {
                        m_lifebar2PText.text = m_Playerlife[5].ToString();  // 表示を更新する
                    }
                    m_lifebar2P.value = m_Playerlife[5];
                }
                else
                {
                    PlayerController playerController = playerObject[5].GetComponent<PlayerController>();
                    playerController.PlayerDestroy();
                    m_Playerlife[5] = 0;
                    if (m_lifebar2PText)
                    {
                        m_lifebar2PText.text = m_Playerlife[5].ToString();  // 表示を更新する
                    }
                    m_lifebar2P.value = m_Playerlife[5];
                }
                break;
            case "2PO1":
                if (m_Playerlife[6] > 0)
                {
                    if (m_optionLifebar2p1Text)
                    {
                        m_optionLifebar2p1Text.text = m_Playerlife[6].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p1.value = m_Playerlife[6];
                }
                else
                {
                    OptionController optionController = playerObject[6].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[6] = 0;
                    if (m_optionLifebar2p1Text)
                    {
                        m_optionLifebar2p1Text.text = m_Playerlife[6].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p1.value = m_Playerlife[6];
                }
                break;
            case "2PO2":
                if (m_Playerlife[7] > 0)
                {
                    if (m_optionLifebar2p2Text)
                    {
                        m_optionLifebar2p2Text.text = m_Playerlife[7].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p2.value = m_Playerlife[7];
                }
                else
                {
                    OptionController optionController = playerObject[7].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[7] = 0;
                    if (m_optionLifebar2p2Text)
                    {
                        m_optionLifebar2p2Text.text = m_Playerlife[7].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p2.value = m_Playerlife[7];
                }
                break;
            case "2PO3":
                if (m_Playerlife[8] > 0)
                {
                    if (m_optionLifebar2p3Text)
                    {
                        m_optionLifebar2p3Text.text = m_Playerlife[8].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p3.value = m_Playerlife[8];
                }
                else
                {
                    OptionController optionController = playerObject[8].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[8] = 0;
                    if (m_optionLifebar2p1Text)
                    {
                        m_optionLifebar2p3Text.text = m_Playerlife[8].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p3.value = m_Playerlife[8];
                }
                break;
            case "2PO4":
                if (m_Playerlife[9] > 0)
                {
                    if (m_optionLifebar2p4Text)
                    {
                        m_optionLifebar2p4Text.text = m_Playerlife[9].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p4.value = m_Playerlife[9];
                }
                else
                {
                    OptionController optionController = playerObject[9].GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Playerlife[9] = 0;
                    if (m_optionLifebar2p4Text)
                    {
                        m_optionLifebar2p4Text.text = m_Playerlife[9].ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p4.value = m_Playerlife[9];
                }
                break;
            default:
                break;
        }
    }
    public void SetCool(int i)
    {
        if (i == 1)
        {
            m_CT[0].minValue = fireDelay / -dNam1;
            m_CT[1].minValue = shotDelay / -dNam1;
            m_CT[2].minValue = laserDelay / -dNam1;

        }
        else
        {
            m_CT[3].minValue = fireDelay / -dNam2;
            m_CT[4].minValue = shotDelay / -dNam2;
            m_CT[5].minValue = laserDelay / -dNam2;
        }
    }
    public void GameStart()
    {
        if (!Ready1P)
        {
            Ready1P = true;
        }
        else
        {
            Ready2P = true;
        }
        
    }
    void Count()
    {
        
        count--;
        if (count < 0)
        {
            
            countObject.SetActive(false);
            countObject1.SetActive(false);
            start = true;
        }
        else if (count != 0)
        {
            countText.text = count.ToString();
            countText1.text = count.ToString();
        }
        else
        {
            countText.text = "GO";
            countText1.text = "FIGHT";
        }
    }
    
    public void SceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Titleload()
    {
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary>
    /// ゲームオーバー時に呼び出す
    /// </summary>
    void GameOver()
    {
        titleButton.SetActive(true);
        reloadButton.SetActive(true);
    }
}
