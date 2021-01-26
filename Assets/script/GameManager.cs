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
    /// <summary>残機数</summary>
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


    /// <summary>タイマー</summary>
    float m_timer;
    /// <summary>ゲームの状態</summary>
    int m_status = 0;    // 0: ゲーム初期化前, 1: ゲーム初期化済み、ゲーム開始前, 2: ゲーム中, 3: プレイヤーがやられた

    void Start()
    {
  
    }

    void Update()
    {
        if (m_status == 0)  // 初期化前
        {
     

        }
        else if (m_status == 1) // 初期化済み、開始前
        {

            
        }
        else if (m_status == 3) // プレイヤーがやられた
        {
            
        }
    }


    /// <summary>
    /// プレイヤーがやられた時、外部から呼ばれる関数
    /// </summary>
    public void PlayerHit1P()
    {
        Debug.Log("Hit.");
        m_Playerlife1P--;    // 残機を減らす
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

    public void OptionHit1P1()
    {
        
        m_Optionlife1P1--;    // 残機を減らす
        Debug.Log(m_Optionlife1P1);

    }

    public void OptionHit1P2()
    {
        m_Optionlife1P2--;
        Debug.Log(m_Optionlife1P2);

    }

    public void OptionHit1P3()
    {
        m_Optionlife1P3--;
        Debug.Log(m_Optionlife1P3);

    }

    public void OptionHit1P4()
    {
        m_Optionlife1P4--;
        Debug.Log(m_Optionlife1P4);

    }

    public void PlayerHit2P()
    {
        m_Playerlife2P--;
        Debug.Log(m_Playerlife2P);
    }


    public void OptionHit2P1()
    {
        m_Optionlife2P1--;
        Debug.Log(m_Optionlife2P1);
    }
    public void OptionHit2P2()
    {
        m_Optionlife2P2--;
        Debug.Log(m_Optionlife2P2);
    }

    public void OptionHit2P3()
    {
        m_Optionlife2P3--;
        Debug.Log(m_Optionlife2P3);
    }

    public void OptionHit2P4()
    {
        m_Optionlife2P4--;
        Debug.Log(m_Optionlife2P4);
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

    /// <summary>
    /// ゲームオーバー時に呼び出す
    /// </summary>
    void GameOver()
    {
        
    }
}
