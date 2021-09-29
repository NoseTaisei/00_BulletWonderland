using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector3 shotDir; //　弾が飛んでいく方向

    private float shotSpeed; //　弾が飛んでいく速さ
    private float shotPower;//  弾の攻撃力

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb2d.simulated == false)
            return;
    }
    private void FixedUpdate()
    {
        // 弾の速さは継承先で決める
        rb2d.velocity = shotDir.normalized * shotSpeed;
    }
    public void SettingShotAbility(Vector3 dir, float SPD,float ATK)
    {
        shotDir = dir;
        shotSpeed = SPD;
        shotPower = ATK;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            rb2d.simulated = false;
            ObjectPool.instance.OnReturnedToPool(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            rb2d.simulated = false;
            ObjectPool.instance.OnReturnedToPool(this.gameObject);

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // エネミーの攻撃力分、プレイヤーにダメージを与える
                playerHealth.DamageHealth(shotPower);
            }
        }
    }
}
