using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DanmakuEdit : MonoBehaviour
{
    public static DanmakuEdit instance;　//　シングルトン化

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OneShot(float shotSpeed,float bulletATK,GameObject firePoint,Vector3 dir,string type)
    {
        var bullet = ObjectPool.instance.GetBullet(1,type);
        bullet.GetComponent<EnemyBullet>().SettingShotAbility(dir, shotSpeed, bulletATK);

        bullet.transform.position = firePoint.transform.position; // どこから発射されるか
    }

    private float shotAngle = 0f; //　発射角度

    public Vector3 shotDir;

    public void AllShot(float shotSpeed,float bulletATK,GameObject firePoint,float plusAngle,int shotCount,string type)
    {
        for(int i = 0; i < shotCount; i++)
        {
            ShotDir();
            var bullet = ObjectPool.instance.GetBullet(1,type);
            bullet.GetComponent<EnemyBullet>().SettingShotAbility(shotDir, shotSpeed, bulletATK);

            bullet.transform.position = firePoint.transform.position; // どこから発射されるか

            shotAngle += plusAngle;　// 角度を足す
        }  
    }
    void ShotDir()
    {
        float rad = shotAngle * Mathf.Deg2Rad;

        shotDir = new Vector3(Mathf.Cos(rad),Mathf.Sin(rad),0);
    }
}
