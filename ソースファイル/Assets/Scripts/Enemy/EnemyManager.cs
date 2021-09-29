using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public Animator animator;

    public float HP;
    public float SPD;
    public float bulletATK;
    public float bulletSPD;

    [System.NonSerialized]
    public Transform target; 
    [System.NonSerialized]
    public Transform enemyPos;
    [System.NonSerialized]
    public Vector2 targetDir; // ターゲットの方向


    [System.NonSerialized]
    public Vector2 moveDir; // 自身が動く方向
    
    public float waitCounter;
    public float waitTime;
    public float moveCounter;

    public SpriteRenderer sp;

    public bool isDamage = false;

    [SerializeField]
    protected string bulletType;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        
        enemyPos = gameObject.transform;
        // 全てのエネミーはプレイヤーの位置情報を持つ
        target = GameObject.Find("Player").transform;
    }
 
    public void Move()　// ランダムに動く
    {
        if (waitCounter > 0)
            {
                waitCounter -= Time.deltaTime;
                rb2d.velocity = Vector2.zero;

                if (waitCounter <= 0)
                {
                    moveCounter = waitTime;

                    moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    moveDir.Normalize();
                }
            }
        else
        {
            moveCounter -= Time.deltaTime;

            rb2d.velocity = moveDir * SPD;

            if (moveCounter <= 0)
            {
                waitCounter += waitTime;
            }
        }
        
    }
    public void MoveAnim()
    {
        animator.SetFloat("X", moveDir.x);
        animator.SetFloat("Y", moveDir.y);
    }

    /// <summary>
    /// エネミーの弾の攻撃力、スピード、方向を決める
    /// </summary>
    public void EnemyShotAbilityCheck(float bulletSpeed,float bulletAttack)
    {
        var burret = ObjectPool.instance.GetBullet(1,bulletType);

        targetDir = target.position - enemyPos.position;// プレイヤーの方向を求める

        burret.GetComponent<EnemyBullet>().SettingShotAbility(targetDir, bulletSpeed, bulletAttack);
        

        burret.transform.position = this.gameObject.transform.position; // どこから発射されるか
    }
    public virtual void DamageHealth(float amount)
    {
        HP -= amount;
        // Debug.Log(gameObject.name + HP);

        isDamage = true;
    }

    public float damageTime = 0;
    public virtual void DamageColorChange()
    {
        if(isDamage)
        {
            sp.color = Color.red; // 点滅処理
                
            damageTime += Time.deltaTime;
            if(damageTime >= 0.1f)
            {
                sp.color = new Color(1f, 1f, 1f, 1f);　
                isDamage = false;
                damageTime = 0;
            }        
        }       
    }
}
