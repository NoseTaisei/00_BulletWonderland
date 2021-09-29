using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector2 shotDir; //　弾が飛んでいく方向
    private float shotSpeed; //　弾が飛んでいく速さ
    private float shotPower;//  弾の攻撃力

    public float ShotPower { get => shotPower;}

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
    {
        if (rb2d.simulated == false)
             return;   
            rb2d.velocity = shotDir.normalized * shotSpeed;
    }
    /// <summary>
    /// dirにプレイヤーの向きを入れる為の関数
    /// </summary>
    /// <param name="dir"></param>
    public void SettingShotAbility(Vector2 dir, float SPD, float ATK)
    {
        shotDir = dir;
        shotSpeed = SPD;
        shotPower = ATK;
        // 弾の回転を決める
        transform.rotation = Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up, shotDir));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            rb2d.simulated = false;
            ObjectPool.instance.OnReturnedToPool(this.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            // Debug.Log("当たったよ");
            rb2d.simulated = false;
            ObjectPool.instance.OnReturnedToPool(this.gameObject);

            EnemyManager enemyManager = collision.gameObject.GetComponent<EnemyManager>();

            if (enemyManager != null)
            {
                // プレイヤーの攻撃力分、エネミーにダメージを与える
                enemyManager.DamageHealth(shotPower);
            }
        }
    }
   
}
