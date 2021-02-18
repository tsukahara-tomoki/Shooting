using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム全体を管理するクラス。
/// EnemyGenerator と同じ GameObject にアタッチする必要がある。
/// </summary>
public class GameManager : MonoBehaviour 
{
    /// <summary>ライフ/summary>
    [SerializeField] int m_Playerlife1P = 300;
    [SerializeField] int m_Optionlife1P1 = 100;
    [SerializeField] int m_Optionlife1P2 = 3;
    [SerializeField] int m_Optionlife1P3 = 3;
    [SerializeField] int m_Optionlife1P4 = 3;
    [SerializeField] int m_Optionlife1P5 = 3;
    [SerializeField] int m_Playerlife2P = 3;
    [SerializeField] int m_Optionlife2P1 = 3;
    [SerializeField] int m_Optionlife2P2 = 3;
    [SerializeField] int m_Optionlife2P3 = 3;
    [SerializeField] int m_Optionlife2P4 = 3;
    [SerializeField] int m_Optionlife2P5 = 3;
    /// <summary> オブジェクト </summary>
    [SerializeField] GameObject Player1P;
    [SerializeField] GameObject option1p1;
    [SerializeField] GameObject option1p2;
    [SerializeField] GameObject option1p3;
    [SerializeField] GameObject option1p4;
    [SerializeField] GameObject Player2P;
    [SerializeField] GameObject option2p1;
    [SerializeField] GameObject option2p2;
    [SerializeField] GameObject option2p3;
    [SerializeField] GameObject option2p4;
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
    bool Ready1P;
    bool Ready2P;


    /// <summary>タイマー</summary>
    float m_timer;
    /// <summary>ゲームの状態</summary>
    int m_status = 0;    // 0: ゲーム初期化前, 1: ゲーム初期化済み、ゲーム開始前, 2: ゲーム中, 3: プレイヤーがやられた

    void Start()
    {
        m_lifebar1PText.text = m_Playerlife1P.ToString();
        m_optionLifebar1p1Text.text = m_Optionlife1P1.ToString();
        m_optionLifebar1p2Text.text = m_Optionlife1P2.ToString();
        m_optionLifebar1p3Text.text = m_Optionlife1P3.ToString();
        m_optionLifebar1p4Text.text = m_Optionlife1P4.ToString();
        m_lifebar2PText.text = m_Playerlife2P.ToString();
        m_optionLifebar2p1Text.text = m_Optionlife2P1.ToString();
        m_optionLifebar2p2Text.text = m_Optionlife2P2.ToString();
        m_optionLifebar2p3Text.text = m_Optionlife2P3.ToString();
        m_optionLifebar2p4Text.text = m_Optionlife2P4.ToString();
        m_lifebar1P.maxValue = m_Playerlife1P;
        m_optionLifebar1p1.maxValue = m_Optionlife1P1;
        m_optionLifebar1p2.maxValue = m_Optionlife1P2;
        m_optionLifebar1p3.maxValue = m_Optionlife1P3;
        m_optionLifebar1p4.maxValue = m_Optionlife1P4;
        m_lifebar2P.maxValue = m_Playerlife2P;
        m_optionLifebar2p1.maxValue = m_Optionlife2P1;
        m_optionLifebar2p2.maxValue = m_Optionlife2P2;
        m_optionLifebar2p3.maxValue = m_Optionlife2P3;
        m_optionLifebar2p4.maxValue = m_Optionlife2P4;
        m_lifebar1P.value = m_Playerlife1P;
        m_optionLifebar1p1.value = m_Optionlife1P1;
        m_optionLifebar1p2.value = m_Optionlife1P2;
        m_optionLifebar1p3.value = m_Optionlife1P3;
        m_optionLifebar1p4.value = m_Optionlife1P4;
        m_lifebar2P.value = m_Playerlife2P;
        m_optionLifebar2p1.value = m_Optionlife2P1;
        m_optionLifebar2p2.value = m_Optionlife2P2;
        m_optionLifebar2p3.value = m_Optionlife2P3;
        m_optionLifebar2p4.value = m_Optionlife2P4;
    }

    void Update()
    {
        if (m_status == 0)  // 初期化前
        {

            if (Ready1P && Ready2P)
            {
                m_status = 1;
            }

        }
        else if (m_status == 1) // カウントダウン
        {

            
        }
        else if (m_status == 3) // プレイヤーがやられた
        {
            
        }
    }


    /// <summary>
    /// プレイヤーがやられた時、外部から呼ばれる関数
    /// </summary>
    public void PlayerHit1P(int i)
    {
        Debug.Log("Hit.");
        m_Playerlife1P -= i;    // 残機を減らす
        LifebarRenewal("1P");
        if (m_Playerlife1P < 1)
            {
                //Debug.Log("PlayerDestroy.");
                GameObject PlayerObject = GameObject.Find("Player(Clone)");
                if (PlayerObject)
                {
                    m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する
                }
            }
    }


    public void OptionHit1P1(int i)
    {

        m_Optionlife1P1 -= i;    // 残機を減らす
        LifebarRenewal("1PO1");
        //sDebug.Log(m_Optionlife1P1);

    }

    public void OptionHit1P2(int i)
    {
        m_Optionlife1P2 -= i;
        LifebarRenewal("1PO2");
        //Debug.Log(m_Optionlife1P2);

    }

    public void OptionHit1P3(int i)
    {
        m_Optionlife1P3 -= i;
        LifebarRenewal("1PO3");
        //Debug.Log(m_Optionlife1P3);

    }

    public void OptionHit1P4(int i)
    {
        m_Optionlife1P4 -= i;
        LifebarRenewal("1PO4");
        //Debug.Log(m_Optionlife1P4);

    }

    public void PlayerHit2P(int i)
    {
        m_Playerlife2P -= i;
        LifebarRenewal("2P");
        //Debug.Log(m_Playerlife2P);
    }


    public void OptionHit2P1(int i)
    {
        m_Optionlife2P1 -= i;
        LifebarRenewal("2PO1");
        //Debug.Log(m_Optionlife2P1);
    }
    public void OptionHit2P2(int i)
    {
        m_Optionlife2P2 -= i;
        LifebarRenewal("2PO2");
        //Debug.Log(m_Optionlife2P2);
    }

    public void OptionHit2P3(int i)
    {
        m_Optionlife2P3--;
        LifebarRenewal("2PO3");
        //Debug.Log(m_Optionlife2P3);
    }

    public void OptionHit2P4(int i)
    {
        m_Optionlife2P4 -= i;
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

    void LifebarRenewal(string n)
    {
        switch (n)
        {
            case "1P":
                if (m_Playerlife1P > 0)
                {
                    if (m_lifebar1PText)
                    {
                        m_lifebar1PText.text = m_Playerlife1P.ToString();  // 表示を更新する
                    }
                    m_lifebar1P.value = m_Playerlife1P;
                }
                else
                {
                    PlayerController playerController = Player1P.GetComponent<PlayerController>();
                    playerController.PlayerDestroy();
                    m_Playerlife1P = 0;
                    if (m_lifebar1PText)
                    {
                        m_lifebar1PText.text = m_Playerlife1P.ToString();  // 表示を更新する
                    }
                    m_lifebar1P.value = m_Playerlife1P;
                }
                break;
            case "1PO1":
                if(m_Optionlife1P1 > 0)
                {
                    if (m_optionLifebar1p1Text)
                    {
                        m_optionLifebar1p1Text.text = m_Optionlife1P1.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p1.value = m_Optionlife1P1;
                }
                else
                {
                    OptionController optionController = option1p1.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife1P1 = 0;
                    if (m_optionLifebar1p1Text)
                    {
                        m_optionLifebar1p1Text.text = m_Optionlife1P1.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p1.value = m_Optionlife1P1;
                }
                break;
            case "1PO2":
                if (m_Optionlife1P2 > 0)
                {
                    if (m_optionLifebar1p2Text)
                    {
                        m_optionLifebar1p2Text.text = m_Optionlife1P2.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p2.value = m_Optionlife1P2;
                }
                else
                {
                    OptionController optionController = option1p2.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife1P2 = 0;
                    if (m_optionLifebar1p2Text)
                    {
                        m_optionLifebar1p2Text.text = m_Optionlife1P2.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p2.value = m_Optionlife1P2;
                }
                break;
            case "1PO3":
                if (m_Optionlife1P3 > 0)
                {
                    if (m_optionLifebar1p3Text)
                    {
                        m_optionLifebar1p3Text.text = m_Optionlife1P3.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p3.value = m_Optionlife1P3;
                }
                else
                {
                    OptionController optionController = option1p3.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife1P3 = 0;
                    if (m_optionLifebar1p3Text)
                    {
                        m_optionLifebar1p3Text.text = m_Optionlife1P3.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p3.value = m_Optionlife1P3;
                }
                break;
            case "1PO4":
                if (m_Optionlife1P4 > 0)
                {
                    if (m_optionLifebar1p4Text)
                    {
                        m_optionLifebar1p4Text.text = m_Optionlife1P4.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p4.value = m_Optionlife1P4;
                }
                else
                {
                    OptionController optionController = option1p4.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife1P4 = 0;
                    if (m_optionLifebar1p4Text)
                    {
                        m_optionLifebar1p4Text.text = m_Optionlife1P4.ToString();  // 表示を更新する
                    }
                    m_optionLifebar1p4.value = m_Optionlife1P4;
                }
                break;
            case "2P":
                if (m_Playerlife2P > 0)
                {
                    if (m_lifebar2PText)
                    {
                        m_lifebar2PText.text = m_Playerlife2P.ToString();  // 表示を更新する
                    }
                    m_lifebar2P.value = m_Playerlife2P;
                }
                else
                {
                    PlayerController playerController = Player2P.GetComponent<PlayerController>();
                    playerController.PlayerDestroy();
                    m_Playerlife2P = 0;
                    if (m_lifebar2PText)
                    {
                        m_lifebar2PText.text = m_Playerlife2P.ToString();  // 表示を更新する
                    }
                    m_lifebar2P.value = m_Playerlife2P;
                }
                break;
            case "2PO1":
                if (m_Optionlife2P1 > 0)
                {
                    if (m_optionLifebar2p1Text)
                    {
                        m_optionLifebar2p1Text.text = m_Optionlife2P1.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p1.value = m_Optionlife2P1;
                }
                else
                {
                    OptionController optionController = option2p1.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife2P1 = 0;
                    if (m_optionLifebar2p1Text)
                    {
                        m_optionLifebar2p1Text.text = m_Optionlife2P1.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p1.value = m_Optionlife2P1;
                }
                break;
            case "2PO2":
                if (m_Optionlife2P2 > 0)
                {
                    if (m_optionLifebar2p2Text)
                    {
                        m_optionLifebar2p2Text.text = m_Optionlife2P2.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p2.value = m_Optionlife2P2;
                }
                else
                {
                    OptionController optionController = option2p2.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife2P2 = 0;
                    if (m_optionLifebar2p2Text)
                    {
                        m_optionLifebar2p2Text.text = m_Optionlife2P2.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p2.value = m_Optionlife2P2;
                }
                break;
            case "2PO3":
                if (m_Optionlife2P3 > 0)
                {
                    if (m_optionLifebar2p3Text)
                    {
                        m_optionLifebar2p3Text.text = m_Optionlife2P3.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p3.value = m_Optionlife2P3;
                }
                else
                {
                    OptionController optionController = option2p3.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife2P3= 0;
                    if (m_optionLifebar2p1Text)
                    {
                        m_optionLifebar2p3Text.text = m_Optionlife2P3.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p3.value = m_Optionlife2P3;
                }
                break;
            case "2PO4":
                if (m_Optionlife2P4 > 0)
                {
                    if (m_optionLifebar2p4Text)
                    {
                        m_optionLifebar2p4Text.text = m_Optionlife2P4.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p4.value = m_Optionlife2P4;
                }
                else
                {
                    OptionController optionController = option2p4.GetComponent<OptionController>();
                    optionController.PlayerDestroy();
                    m_Optionlife2P4 = 0;
                    if (m_optionLifebar2p4Text)
                    {
                        m_optionLifebar2p4Text.text = m_Optionlife2P4.ToString();  // 表示を更新する
                    }
                    m_optionLifebar2p4.value = m_Optionlife2P4;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ゲームオーバー時に呼び出す
    /// </summary>
    void GameOver()
    {
        
    }
}
