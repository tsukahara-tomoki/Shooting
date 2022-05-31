//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

///// <summary>
///// ゲーム全体を管理するクラス。
///// EnemyGenerator と同じ GameObject にアタッチする必要がある。
///// </summary>
//[RequireComponent(typeof(EnemyGenerator), typeof(PlayerLifebarCounter1P), typeof(OptionLifebarCounter1P1))]
//[RequireComponent(typeof(OptionLifebarCounter1P2), typeof(OptionLifebarCounter1P3), typeof(OptionLifebarCounter1P4))]
//[RequireComponent(typeof(OptionLifebarCounter1P5), typeof(PlayerLifebarCounter2P1), typeof(OptionLifebarCounter2P1))]
//[RequireComponent(typeof(OptionLifebarCounter2P2), typeof(OptionLifebarCounter2P3), typeof(OptionLifebarCounter2P4))]
//public class GameManager : MonoBehaviour 
//{
//    /// <summary>残機数</summary>
//    [SerializeField] int m_Playerlife1P = 300;
//    [SerializeField] int m_Optionlife1P1 = 100;
//    [SerializeField] int m_Optionlife1P2 = 3;
//    [SerializeField] int m_Optionlife1P3 = 3;
//    [SerializeField] int m_Optionlife1P4 = 3;
//    [SerializeField] int m_Optionlife1P5 = 3;
//    [SerializeField] int m_Playerlife2P = 3;
//    [SerializeField] int m_Optionlife2P1 = 3;
//    [SerializeField] int m_Optionlife2P2 = 3;
//    [SerializeField] int m_Optionlife2P3 = 3;
//    [SerializeField] int m_Optionlife2P4 = 3;
//    [SerializeField] int m_Optionlife2P5 = 3;

//    /// <summary>得点</summary>
//    int m_score;
//    /// <summary>得点を表示する Text</summary>
//    [SerializeField] Text m_scoreText;
//    [SerializeField] Text m_lifebar1PText;
//    [SerializeField] Text m_optionLifebar1p1Text;
//    [SerializeField] Text m_optionLifebar1p2Text;
//    [SerializeField] Text m_optionLifebar1p3Text;
//    [SerializeField] Text m_optionLifebar1p4Text;
//    [SerializeField] Text m_optionLifebar1p5Text;
//    [SerializeField] Text m_lifebar2PText;
//    [SerializeField] Text m_optionLifebar2p1Text;
//    [SerializeField] Text m_optionLifebar2p2Text;
//    [SerializeField] Text m_optionLifebar2p3Text;
//    [SerializeField] Text m_optionLifebar2p4Text;
//    /// <summary>自機のプレハブを指定する</summary>
//    [SerializeField] GameObject m_playerPrefab;
//    /// <summary>ゲームの初期化が終わってからゲームが始まるまでの待ち時間</summary>
//    [SerializeField] float m_waitTimeUntilGameStarts = 5f;
//    /// <summary>自機がやられてからゲームの再初期化をするまでの待ち時間</summary>
//    [SerializeField] float m_waitTimeAfterPlayerDeath = 5f;
//    /// <summary>ゲームオーバー後に遷移するシーン（タイトル画面）のシーン名</summary>
//    [SerializeField] string m_titleSceneName = "Title";
//    /// <summary>EnemyGenerator を保持しておく変数</summary>
//    EnemyGenerator m_enemyGenerator;
//    /// <summary>残機表示をする PlayerCounter を保持しておく変数</summary>
//    PlayerLifebarCounter1P m_lifebar1P;
//    OptionLifebarCounter1P1 m_optionLifebar1p1;
//    OptionLifebarCounter1P2 m_optionLifebar1p2;
//    OptionLifebarCounter1P3 m_optionLifebar1p3;
//    OptionLifebarCounter1P4 m_optionLifebar1p4;
//    OptionLifebarCounter1P5 m_optionLifebar1p5;
//    PlayerLifebarCounter2P1 m_lifebar2P;
//    OptionLifebarCounter2P1 m_optionLifebar2p1;
//    OptionLifebarCounter2P2 m_optionLifebar2p2;
//    OptionLifebarCounter2P3 m_optionLifebar2p3;
//    OptionLifebarCounter2P4 m_optionLifebar2p4;

//    [SerializeField] GameObject Player1P;
//    [SerializeField] GameObject option1p1;
//    [SerializeField] GameObject option1p2;
//    [SerializeField] GameObject option1p3;
//    [SerializeField] GameObject option1p4;
//    [SerializeField] GameObject Player2P;
//    [SerializeField] GameObject option2p1;
//    [SerializeField] GameObject option2p2;
//    [SerializeField] GameObject option2p3;
//    [SerializeField] GameObject option2p4;


//    /// <summary>タイマー</summary>
//    float m_timer;
//    /// <summary>ゲームの状態</summary>
//    int m_status = 0;    // 0: ゲーム初期化前, 1: ゲーム初期化済み、ゲーム開始前, 2: ゲーム中, 3: プレイヤーがやられた

//    void Start()
//    {
//        // EnemyGenerator を取得しておき、まずは敵の生成をしない。
//        m_enemyGenerator = GetComponent<EnemyGenerator>();
//        m_enemyGenerator.StopGeneration();

//        m_lifebar1P = GetComponent<PlayerLifebarCounter1P>();
//        m_lifebar1P.Refresh(m_Playerlife1P);
//        m_optionLifebar1p1 = GetComponent<OptionLifebarCounter1P1>();
//        m_optionLifebar1p1.Refresh(m_Optionlife1P1);
//        m_optionLifebar1p2 = GetComponent<OptionLifebarCounter1P2>();
//        m_optionLifebar1p2.Refresh(m_Optionlife1P2);
//        m_optionLifebar1p3 = GetComponent<OptionLifebarCounter1P3>();
//        m_optionLifebar1p3.Refresh(m_Optionlife1P3);
//        m_optionLifebar1p4 = GetComponent<OptionLifebarCounter1P4>();
//        m_optionLifebar1p4.Refresh(m_Optionlife1P4);
//        m_optionLifebar1p5 = GetComponent<OptionLifebarCounter1P5>();
//        m_optionLifebar1p5.Refresh(m_Optionlife1P5);
//        m_lifebar2P = GetComponent<PlayerLifebarCounter2P1>();
//        m_lifebar2P.Refresh(m_Playerlife2P);
//        m_optionLifebar2p1 = GetComponent<OptionLifebarCounter2P1>();
//        m_optionLifebar2p1.Refresh(m_Optionlife2P1);
//        m_optionLifebar2p2 = GetComponent<OptionLifebarCounter2P2>();
//        m_optionLifebar2p2.Refresh(m_Optionlife2P2);
//        m_optionLifebar2p3 = GetComponent<OptionLifebarCounter2P3>();
//        m_optionLifebar2p3.Refresh(m_Optionlife2P3);
//        m_optionLifebar2p4 = GetComponent<OptionLifebarCounter2P4>();
//        m_optionLifebar2p4.Refresh(m_Optionlife2P4);
//        AddScore(0);
//    }

//    void Update()
//    {
//        if (m_status == 0)  // 初期化前
//        {
//            Debug.Log("Initialize.");
//            //Instantiate(m_playerPrefab);    // プレイヤーを生成する
//            m_status = 1;   // ステータスを初期化済みにする
//            m_lifebar1P.Refresh(m_Playerlife1P);    // 残機表示を更新する
//            m_optionLifebar1p1.Refresh(m_Optionlife1P1);
//            m_optionLifebar1p2.Refresh(m_Optionlife1P2);
//            m_optionLifebar1p3.Refresh(m_Optionlife1P3);
//            m_optionLifebar1p4.Refresh(m_Optionlife1P4);
//            m_optionLifebar1p5.Refresh(m_Optionlife1P5);

//        }
//        else if (m_status == 1) // 初期化済み、開始前
//        {
//            m_timer += Time.deltaTime;
//            if (m_timer > m_waitTimeUntilGameStarts)    // 待つ
//            {
//                Debug.Log("Game Start.");
//                m_timer = 0f;   // タイマーをリセットする
//                m_status = 2;   // ステータスをゲーム中にする
//                m_enemyGenerator.StartGeneration(); // 敵の生成を開始する
//                m_lifebar1P.Refresh(m_Playerlife1P);    // 残機表示を更新する
//            }
//        }
//        else if (m_status == 3) // プレイヤーがやられた
//        {
//            m_timer += Time.deltaTime;
//            if (m_timer > m_waitTimeAfterPlayerDeath)   // 待つ
//            {
//                if (m_Playerlife1P > 0) // 残機がまだある場合
//                {
//                    Debug.Log("Restart Game.");
//                    m_timer = 0f;   // タイマーをリセットする
//                    m_status = 0;   // 初期化するためにステータスを更新する
//                    ClearScene();
//                }
//                else
//                {
//                    GameOver(); // 残機がもうない場合はゲームオーバーにする
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// スコアを加算する
//    /// </summary>
//    /// <param name="score"></param>
//    public void AddScore(int score)
//    {
//        m_score += score;   // 点数を足す
//        Debug.Log("Score: " + m_score.ToString());  // Console に出力する
//            if (m_scoreText)    // m_scoreText が設定されていたら
//            {
//                m_scoreText.text = "Score: " + m_score.ToString();  // 表示を更新する
//            }
//    }

//    /// <summary>
//    /// プレイヤーがやられた時、外部から呼ばれる関数
//    /// </summary>
//    public void PlayerHit1P()
//    {
        
//        //Debug.Log("Player Dead.");
//        /*m_life--;
//        if (m_life == 0)
//        {
//            //m_enemyGenerator.StopGeneration();  // 敵の生成を止める
//            if (m_lifeText)    // m_scoreText が設定されていたら
//            {
//                m_lifeText.text = "残機: " + m_life.ToString();  // 表示を更新する
//            }
//            m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する
//        }
//        else
//        {
//            if (m_lifeText)    // m_scoreText が設定されていたら
//            {
//                m_lifeText.text = "残機: " + m_life.ToString();  // 表示を更新する
//            }
//        }*/
//        //m_enemyGenerator.StopGeneration();  // 敵の生成を止める
//        Debug.Log("Hit.");
//        m_Playerlife1P--;    // 残機を減らす
//        if (m_lifebar1PText)    // m_scoreText が設定されていたら
//        {
//            m_lifebar1PText.text = m_Playerlife1P.ToString();  // 表示を更新する
//            /*if (m_optionLifebar1p1Text)
//            {
//                m_optionLifebar1p1Text.text = m_optionLifebar1p1.ToString();  // 表示を更新する
//            }
//            if (m_optionLifebar1p2Text)
//            {
//                m_optionLifebar1p2Text.text = m_optionLifebar1p2.ToString();  // 表示を更新する
//            }
//            if (m_optionLifebar1p3Text)
//            {
//                m_optionLifebar1p3Text.text = m_optionLifebar1p3.ToString();  // 表示を更新する
//            }
//            if (m_optionLifebar1p4Text)
//            {
//                m_optionLifebar1p4Text.text = m_optionLifebar1p4.ToString();  // 表示を更新する
//            }
//            if (m_lifebar2PText)
//            {
//                m_lifebar2PText.text = m_lifebar2P.ToString();  // 表示を更新する
//            }
//            if (m_optionLifebar2p1Text)
//            {
//                m_optionLifebar2p1Text.text = m_optionLifebar2p1.ToString();  // 表示を更新する
//            }
//            if (m_optionLifebar2p2Text)
//            {
//                m_optionLifebar2p2Text.text = m_optionLifebar2p2.ToString();  // 表示を更新する
//            }
//            if (m_optionLifebar2p3Text)
//            {
//                m_optionLifebar2p3Text.text = m_optionLifebar2p3.ToString();  // 表示を更新する
//            }
//            if (m_optionLifebar2p4Text)
//            {
//                m_optionLifebar2p4Text.text = m_optionLifebar2p4.ToString();  // 表示を更新する
//            }*/
//            m_lifebar1P = GetComponent<PlayerLifebarCounter1P>();
//            m_lifebar1P.Refresh(m_Playerlife1P);
//            /*m_optionLifebar1p1.Refresh(m_Optionlife1P1);
//            m_optionLifebar1p2.Refresh(m_Optionlife1P2);
//            m_optionLifebar1p3.Refresh(m_Optionlife1P3);
//            m_optionLifebar1p4.Refresh(m_Optionlife1P4);
//            m_lifebar2P.Refresh(m_Playerlife2P);
//            m_optionLifebar2p1.Refresh(m_Optionlife2P1);
//            m_optionLifebar2p2.Refresh(m_Optionlife2P2);
//            m_optionLifebar2p3.Refresh(m_Optionlife2P3);
//            m_optionLifebar2p4.Refresh(m_Optionlife2P4);*/
//            if (m_Playerlife1P < 1)
//            {
//                //Debug.Log("PlayerDestroy.");
//                GameObject PlayerObject = GameObject.Find("Player(Clone)");
//                if (PlayerObject)
//                {
//                    Debug.Log("PlayerDestroy.");
//                    PlayerController1P playerController = PlayerObject.GetComponent<PlayerController1P>();
//                    playerController.PlayerDestroy();
//                    m_enemyGenerator.StopGeneration();  // 敵の生成を止める

//                    m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する
//                }
//            }
//        }
//        /*if (m_life < 1)
//        {
//            Debug.Log("PlayerDestroy.");
//            GameObject PlayerObject = GameObject.Find("Player");
//            if (PlayerObject)
//            {
//                //Debug.Log("PlayerDestroy.");
//                PlayerController playerController = PlayerObject.GetComponent<PlayerController>();
//                playerController.PlayerDestroy();
//                m_enemyGenerator.StopGeneration();  // 敵の生成を止める
//               // Debug.Log("PlayerDestroy.");

//                m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する
//            }
//        }*/
        
//        //m_status = 3;   // ステータスをプレイヤーがやられた状態に更新する
//    }

//    public void OptionHit1P1()
//    {
        
//        m_Optionlife1P1--;    // 残機を減らす
//        if (m_Optionlife1P1 > 0)
//        {
//            if (m_optionLifebar1p1Text)
//            {
//                m_optionLifebar1p1Text.text = m_Optionlife1P1.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p1 = GetComponent<OptionLifebarCounter1P1>();
//            m_optionLifebar1p1.Refresh(m_Optionlife1P1);
//        }
//        else
//        {
//            OptionController optionController = option1p1.GetComponent<OptionController>();
//            optionController.PlayerDestroy();
//            m_Optionlife1P1 = 0;
//            if (m_optionLifebar1p1Text)
//            {
//                m_optionLifebar1p1Text.text = m_Optionlife1P1.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p1 = GetComponent<OptionLifebarCounter1P1>();
//            m_optionLifebar1p1.Refresh(m_Optionlife1P1);
//        }
//    }

//    public void OptionHit1P2()
//    {
//        m_Optionlife1P2--;
//        if (m_Optionlife1P2 > 0)
//        {
//            if (m_optionLifebar1p2Text)
//            {
//                m_optionLifebar1p2Text.text = m_Optionlife1P2.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p2 = GetComponent<OptionLifebarCounter1P2>();
//            m_optionLifebar1p2.Refresh(m_Optionlife1P2);
//        }
//        else
//        {
//            OptionController2 optionController2 = option1p2.GetComponent<OptionController2>();
//            optionController2.PlayerDestroy();
//            m_Optionlife1P2 = 0;
//            if (m_optionLifebar1p2Text)
//            {
//                m_optionLifebar1p2Text.text = m_Optionlife1P2.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p2 = GetComponent<OptionLifebarCounter1P2>();
//            m_optionLifebar1p2.Refresh(m_Optionlife1P2);
//        }
//    }

//    public void OptionHit1P3()
//    {
//        m_Optionlife1P3--;
//        if (m_Optionlife1P3 > 0)
//        {
//            if (m_optionLifebar1p3Text)
//            {
//                m_optionLifebar1p3Text.text = m_Optionlife1P3.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p3 = GetComponent<OptionLifebarCounter1P3>();
//            m_optionLifebar1p3.Refresh(m_Optionlife1P3);
//        }
//        else
//        {
//            OptionController3 optionController3 = option1p3.GetComponent<OptionController3>();
//            optionController3.PlayerDestroy();
//            m_Optionlife1P3 = 0;
//            if (m_optionLifebar1p3Text)
//            {
//                m_optionLifebar1p3Text.text = m_Optionlife1P3.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p3 = GetComponent<OptionLifebarCounter1P3>();
//            m_optionLifebar1p3.Refresh(m_Optionlife1P3);
//        }
//    }

//    public void OptionHit1P4()
//    {
//        m_Optionlife1P4--;
//        if (m_Optionlife1P4 > 0)
//        {
//            if (m_optionLifebar1p4Text)
//            {
//                m_optionLifebar1p4Text.text = m_Optionlife1P4.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p4 = GetComponent<OptionLifebarCounter1P4>();
//            m_optionLifebar1p4.Refresh(m_Optionlife1P4);
//        }
//        else
//        {
//            OptionController4 optionController4 = option1p4.GetComponent<OptionController4>();
//            optionController4.PlayerDestroy();
//            m_Optionlife1P4 = 0;
//            if (m_optionLifebar1p4Text)
//            {
//                m_optionLifebar1p4Text.text = m_Optionlife1P4.ToString();  // 表示を更新する
//            }
//            m_optionLifebar1p4 = GetComponent<OptionLifebarCounter1P4>();
//            m_optionLifebar1p4.Refresh(m_Optionlife1P4);
//        }
//    }

//    public void PlayerHit2P()
//    {
//        if (m_Playerlife2P > 0)
//        {
//            m_Playerlife2P--;    // 残機を減らす
//            if (m_lifebar2PText)
//            {
//                m_lifebar2PText.text = m_Playerlife2P.ToString();  // 表示を更新する
//            }
//            m_lifebar2P = GetComponent<PlayerLifebarCounter2P1>();
//            m_lifebar2P.Refresh(m_Playerlife2P);
//            //m_lifebar2P.hpSlider.value = m_Playerlife2P;
            
//        }
//        else
//        {
//            m_Playerlife2P--;    // 残機を減らす
//            if (m_lifebar2PText)
//            {
//                m_lifebar2PText.text = m_Playerlife2P.ToString();  // 表示を更新する
//            }
//            m_lifebar2P = GetComponent<PlayerLifebarCounter2P1>();
//            m_lifebar2P.Refresh(m_Playerlife2P);
//            GameObject PlayerObject = GameObject.Find("Player(Clone)");
//            PlayerController2P playerController2p = Player2P.GetComponent<PlayerController2P>();
//            playerController2p.PlayerDestroy();
//            //m_lifebar2P.hpSlider.value = m_Playerlife2P;
//        }

//    }
    

//    public void OptionHit2P1()
//    {
//        m_Optionlife2P1--;
//        if (m_Optionlife2P1 > 0)
//        {
//            if (m_optionLifebar2p1Text)
//            {
//                m_optionLifebar2p1Text.text = m_Optionlife2P1.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p1 = GetComponent<OptionLifebarCounter2P1>();
//            m_optionLifebar2p1.Refresh(m_Optionlife2P1);
//        }
//        else
//        {
//            OptionController2P optionController2P = option2p1.GetComponent<OptionController2P>();
//            optionController2P.PlayerDestroy();
//            m_Optionlife2P1 = 0;
//            if (m_optionLifebar2p1Text)
//            {
//                m_optionLifebar2p1Text.text = m_Optionlife2P1.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p1 = GetComponent<OptionLifebarCounter2P1>();
//            m_optionLifebar2p1.Refresh(m_Optionlife2P1);
//        }
        
//    }
//    public void OptionHit2P2()
//    {
//        m_Optionlife2P2--;
//        if (m_Optionlife2P2 > 0)
//        {
//            if (m_optionLifebar2p2Text)
//            {
//                m_optionLifebar2p2Text.text = m_Optionlife2P2.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p2 = GetComponent<OptionLifebarCounter2P2>();
//            m_optionLifebar2p2.Refresh(m_Optionlife2P2);
//        }
//        else
//        {
//            OptionController2P2 optionController2P2 = option2p2.GetComponent<OptionController2P2>();
//            optionController2P2.PlayerDestroy();
//            m_Optionlife2P2 = 0;
//            if (m_optionLifebar2p2Text)
//            {
//                m_optionLifebar2p2Text.text = m_Optionlife2P2.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p2 = GetComponent<OptionLifebarCounter2P2>();
//            m_optionLifebar2p2.Refresh(m_Optionlife2P2);
//        }
        
//    }

//    public void OptionHit2P3()
//    {
//        m_Optionlife2P3--;
//        if (m_Optionlife2P3 > 0)
//        {
//            if (m_optionLifebar2p3Text)
//            {
//                m_optionLifebar2p3Text.text = m_Optionlife2P3.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p3 = GetComponent<OptionLifebarCounter2P3>();
//            m_optionLifebar2p3.Refresh(m_Optionlife2P3);
//        }
//        else
//        {
//            OptionController2P3 optionController2P3 = option2p3.GetComponent<OptionController2P3>();
//            optionController2P3.PlayerDestroy();
//            m_Optionlife2P3 = 0;
//            if (m_optionLifebar2p3Text)
//            {
//                m_optionLifebar2p3Text.text = m_Optionlife2P3.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p3 = GetComponent<OptionLifebarCounter2P3>();
//            m_optionLifebar2p3.Refresh(m_Optionlife2P3);
//        }
//    }

//    public void OptionHit2P4()
//    {
//        m_Optionlife2P4--;
//        if (m_Optionlife2P4 > 0)
//        {
//            if (m_optionLifebar2p4Text)
//            {
//                m_optionLifebar2p4Text.text = m_Optionlife2P4.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p4 = GetComponent<OptionLifebarCounter2P4>();
//            m_optionLifebar2p4.Refresh(m_Optionlife2P4);
//        }
//        else
//        {
//            OptionController2P4 optionController2P4 = option2p4.GetComponent<OptionController2P4>();
//            optionController2P4.PlayerDestroy();
//            m_Optionlife2P4 = 0;
//            if (m_optionLifebar2p4Text)
//            {
//                m_optionLifebar2p4Text.text = m_Optionlife2P4.ToString();  // 表示を更新する
//            }
//            m_optionLifebar2p4 = GetComponent<OptionLifebarCounter2P4>();
//            m_optionLifebar2p4.Refresh(m_Optionlife2P4);
//        }
//    }

//    /// <summary>
//    /// シーン上にある敵と敵の弾を消す
//    /// </summary>
//    void ClearScene()
//    {
//        // 敵を消す
//        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Enemy");
//        foreach (var go in goArray)
//        {
//            Destroy(go);
//        }

//        // 敵の弾を消す
//        goArray = GameObject.FindGameObjectsWithTag("EnemyBullet");
//        foreach (var go in goArray)
//        {
//            Destroy(go);
//        }
//    }

//    /// <summary>
//    /// ゲームオーバー時に呼び出す
//    /// </summary>
//    void GameOver()
//    {
//        Debug.Log("Game over. Return to title scene.");
//        Initiate.Fade(m_titleSceneName, Color.black, 1.0f); // タイトル画面に戻る
//    }
//}
