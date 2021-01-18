﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シューティングゲームで自機から発射される弾を制御するコンポーネント
/// 弾はインスタンス化されたら自ら飛んでいく
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    /// <summary>弾の発射方向</summary>
    [SerializeField] Vector2 m_direction = Vector2.up;
    /// <summary>弾の飛ぶ速度</summary>
    [SerializeField] float m_bulletSpeed = 10f;
    Rigidbody2D m_rb2d;
    [SerializeField] float m_life= 2;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        Vector3 v = m_direction.normalized * m_bulletSpeed;   // 弾が飛ぶ速度ベクトルを計算する
        m_rb2d.velocity = v;                      // 速度ベクトルを弾にセットする

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Finish タグのついた Trigger に接触したら弾を消す
        if (collision.gameObject.tag == "killzoneTag")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "laser")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            m_life--;
        }
        if (collision.gameObject.tag == "2Player")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "2POption")
        {
            Destroy(this.gameObject);
        }
        if (m_life < 0)
        {
            Destroy(this.gameObject);
        }


    }
}