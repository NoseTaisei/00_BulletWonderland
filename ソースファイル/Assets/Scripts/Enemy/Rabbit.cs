using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rabbit : EnemyManager
{
    [SerializeField]
    private EnemyData enemyData; // ScriptableObject

    [SerializeField]
    private GameObject enemyBullet;

    [SerializeField]
    private GameObject firePoint;

    private bool isChase = false;

    private float rangeToChase = 6f; // プレイヤー感知距離

    private enum RabbitState　// Rabbitの行動ステート
    {
        Walk,
        Shot,
        AllShot,
        AllShot2,
    }
    private RabbitState rabbitState;

    private void Awake()
    {
        HP = enemyData.HP;
        SPD = enemyData.SPD;
        bulletATK = enemyData.bulletATK;
        bulletSPD = enemyData.bulletSPD;
    }

    private void FixedUpdate()
    {
        if(HP <= 0)
        {
            gameObject.SetActive(false);
            rb2d.simulated = false; // 物理演算を切る
        }

        base.Move();
        base.MoveAnim();
        Attack();

        // プレイヤーが一定の距離に入ったら
        if(Vector2.Distance(enemyPos.position , target.position) < rangeToChase)
        {
            isChase = true;
        }
    }
    [SerializeField]
    private float moveTime = 0; // どの行動から始まるか設定

    private void Update()
    {
        base.DamageColorChange();

        // 時間ごとにステートが変わる
        moveTime += Time.deltaTime;
        if(moveTime <= 4)
        {
            rabbitState = RabbitState.Shot;
        }
        else if(moveTime <= 10f)
        {
            rabbitState = RabbitState.AllShot;
        }
        else if(moveTime <= 12f)
        {
            rabbitState = RabbitState.Walk;
        }
        else if(moveTime <= 15f)
        {
            rabbitState = RabbitState.AllShot2;
        }
        else if(moveTime <= 20f)
        {
            rabbitState = RabbitState.Walk;
        }
        else if(moveTime > 20f)
        {
            moveTime = 0;
        }
    }
    
    private void Attack()
    {
        if(isChase)
        {
            switch (rabbitState)
            {
                case RabbitState.AllShot:
                DanmakuEdit.instance.AllShot(Random.Range (2f, 4f),bulletATK,firePoint,Random.Range (10f,20f),
                1,bulletType);
                // SoundManager.instance.PlaySe(3,0.3f);
                break; 
                case RabbitState.AllShot2:
                DanmakuEdit.instance.AllShot(5f,bulletATK,firePoint,15f,1,bulletType);
                SoundManager.instance.PlaySe(3,0.3f);
                break; 
                case RabbitState.Shot:
                animator.SetBool("isAttack",true);
                break; 
            }
        }
    }
    private void Shot()
    {   
        DanmakuEdit.instance.OneShot(8f,bulletATK,firePoint,target.position - enemyPos.position,bulletType);

        SoundManager.instance.PlaySe(3,1f);
    }
    private void AllShot()
    {
        DanmakuEdit.instance.AllShot(5f,bulletATK,firePoint,30f,1,bulletType);
    }

    /// <summary>
    /// Animationで攻撃を止める関数
    /// </summary>
    public void IsAttackFalse()
    {
        animator.SetBool("isAttack",false);
    }

    /*
    private float shotAngle = 0f; //　発射角度

    private float shotAngleRate = 1f; // 発射角速度
    private float shotSpeed; //  発射速度

    private float shotInterval = 1f;
    private float maxShotInterval = 1f;

    private int shotCount = 1;
    
    Vector2 testDir;

    public void Shot1()
    {
        shotInterval -= Time.deltaTime;
        if (shotInterval <= 0)
        {
            var bullet = ObjectPool.instance.GetBullet(1);
                bullet.GetComponent<EnemyBullet>().SettingShotAbility(target.position - enemyPos.position, 5f, bulletATK);

                // bullet.transform.position = firePoint.position; // どこから発射されるか
                shotInterval = maxShotInterval;
            /*for(int i = 0; i < shotCount; i++)
            {
                test2(); // 先に向きを決めておかないと弾を発射する際にnullになる
                /*
                var bullet = ObjectPool.instance.GetBullet(1);
                bullet.GetComponent<EnemyBullet>().SettingShotAbility((Vector3)target.position, 5f, bulletATK);

                bullet.transform.position = this.gameObject.transform.position; // どこから発射されるか
   
                //shotAngle += 45f;
                //shotAngle += shotAngleRate;
                //shotInterval = maxShotInterval;

            }
            
        }
        
    }
    */

    public override void DamageHealth(float amount)
    {
        base.DamageHealth(amount);

        if(!isChase)
        {
            isChase = true;
        }
    }
}
