//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Realtime;

/// <summary>
/// 敵の弾を発射するためのコンポーネント
/// Player タグが付いているオブジェクトに向かってまっすぐ飛ぶ
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶスピード</summary>
    [SerializeField] float m_speed = 1f;
    GameObject player;
    [SerializeField] string objectName;
    Rigidbody2D m_rb;
    [SerializeField] bool firstPlayer;
    //PhotonView m_view;

    void Start()
    {
        player = GameObject.Find(objectName); 
        m_rb = GetComponent<Rigidbody2D>();
        Vector2 dir = player.transform.position - this.transform.position;
        dir = dir.normalized;

        // プレイヤーに向かって飛ばす
        m_rb.velocity = dir * m_speed * -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstPlayer)
        {
            if (collision.gameObject.tag == "2P")
            {
                Destroy(this.gameObject);
            }
            if (collision.gameObject.tag == "2Pbullet")
            {
                Destroy(this.gameObject);
            }
            if (collision.gameObject.tag == "2PsubBullet")
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag == "1P")
            {
                Destroy(this.gameObject);
            }
            if (collision.gameObject.tag == "1Pbullet")
            {
                Destroy(this.gameObject);
            }
            if (collision.gameObject.tag == "1PsubBullet")
            {
                Destroy(this.gameObject);
            }
        }
        // Finish タグのついた Trigger に接触したら弾を消す
        if (collision.gameObject.tag == "laser")
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "laser")
        {
            Destroy(this.gameObject);
        }
    }

}
