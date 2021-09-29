using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private CameraManager cameraManager;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private WeaponManager weaponManager;

    [SerializeField]
    private WeaponState weaponState;

    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private float timeCount;

    private const float handgunInterval = 0.2f;

    private const float shotgunInterval = 0.3f;

    private const float sniperRiflInterval = 0.2f;

    [SerializeField]
    private string[] bulletType;

    private void Start()
    {
        timeCount = 0.4f;
    }

    void Update()
    {   
        Shot();
    }
    
    private void Shot()
    {
        if (Input.GetMouseButton(0))
        {
            switch (weaponState.playerWeaponState)
            {
                case WeaponState.PlayerWeaponState.Handgun:
                    BulletShot(handgunInterval,0.02f,false);
                    break;
                case WeaponState.PlayerWeaponState.Shotgun:
                    BulletShot(shotgunInterval, 0.03f, true);
                    break;
                case WeaponState.PlayerWeaponState.SniperRifle:
                    BulletShot(sniperRiflInterval, 0.05f, false);
                    break;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            timeCount = 0.4f;
        }
    }
    
    private void BulletShot(float interval,float shakePower,bool shotgun)
    {
        
        // リロード中か回避中は弾を撃てない
        if (!weaponState.IsReLoad && !playerManager.isDodge)
        {

            timeCount += Time.deltaTime;

            if (timeCount > interval)
            {
                timeCount = 0;

                var burret = GetBullet();

                burret.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);

                burret.transform.position = gameObject.transform.position; // どこから発射されるか

                SoundManager.instance.PlaySe(0,1f);

                if(shotgun) // ショットガンの時
                {
                    var burret1 = GetBullet();
                    var burret2 = GetBullet();

                    burret1.GetComponent<PlayerBullet>().SettingShotAbility 
                        (Quaternion.Euler(0, 0, 20) * playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                    burret2.GetComponent<PlayerBullet>().SettingShotAbility 
                        (Quaternion.Euler(0, 0, -20) * playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);

                    burret1.transform.position = gameObject.transform.position; 
                    burret2.transform.position = gameObject.transform.position;
                }

                weaponState.ReduceMagazineCount(); // マガジンカウントを減らす
            }
        }
    }
    GameObject GetBullet()
    {
        switch (weaponState.playerWeaponState)
            {
                default:
                case WeaponState.PlayerWeaponState.Handgun:
                    return ObjectPool.instance.GetBullet(0,bulletType[0]);
                case WeaponState.PlayerWeaponState.Shotgun:
                    return ObjectPool.instance.GetBullet(0,bulletType[1]);
                case WeaponState.PlayerWeaponState.SniperRifle:
                    return ObjectPool.instance.GetBullet(0,bulletType[2]);
            }
    }

    /*
 
    private void BulletShot()
    {
        switch (weaponState.playerWeaponState)
        {
            case WeaponState.PlayerWeaponState.Handgun:
                if (Input.GetMouseButton(0))
                {
                    handgunShot();       
                }
                if(Input.GetMouseButtonDown(0))
                {
                    isFarstShot = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isFarstShot = false;
                    handgunInterval = handgunMaxInterval;// 弾発射ののインターバル初期化
                }
                break;
            case WeaponState.PlayerWeaponState.Shotgun:
                if (Input.GetMouseButton(0))
                {
                    shotgunShot();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    isFarstShot = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isFarstShot = false;
                    shotgunInterval = shotgunMaxInterval; // 弾発射ののインターバル初期化
                }
                break;
            case WeaponState.PlayerWeaponState.SniperRifle:
                if (Input.GetMouseButton(0))
                {
                    sniperRifleShot();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    isFarstShot = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isFarstShot = false;
                    sniperRiflInterval = sniperRiflMaxInterval;// 弾発射ののインターバル初期化
                }
                break;
        }
    }
    
    private void handgunShot()
    {
        // リロード中か回避中は弾を撃てない
        if (!weaponState.IsReLoad && !playerManager.isDodge)
        {
            if (isFarstShot)
            {
                var burret = ObjectPool.instance.GetBullet(0);

                burret.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                burret.transform.position = gameObject.transform.position; // どこから発射されるか

                weaponState.ReduceMagazineCount(); // マガジンカウントを減らす

                StartCoroutine(CameraManager.instance.Shake(0.05f));

                Debug.Log("ikka");
            }
            isFarstShot = false;

            handgunInterval -= Time.deltaTime;

            if(handgunInterval <= 0)
            {
                var burret = ObjectPool.instance.GetBullet(0);

                burret.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                burret.transform.position = gameObject.transform.position; // どこから発射されるか

                StartCoroutine(CameraManager.instance.Shake(0.05f));

                Debug.Log("あ");
                weaponState.ReduceMagazineCount(); // マガジンカウントを減らす

                handgunInterval = handgunMaxInterval;　 // 弾発射ののインターバル初期化
            }
        }
        else if (weaponState.IsReLoad)
        {
            isFarstShot = false;

            handgunInterval = handgunMaxInterval;　 // 弾発射ののインターバル初期化
        }



    }
    
    private void shotgunShot()
    {
        // リロード中か回避中は弾を撃てない
        if (!weaponState.IsReLoad && !playerManager.isDodge)
        {
            if (isFarstShot)
            {
                var burret = ObjectPool.instance.GetBullet(0);
                var burret1 = ObjectPool.instance.GetBullet(0);
                var burret2 = ObjectPool.instance.GetBullet(0);

                burret2.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                burret1.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (Quaternion.Euler(0, 0, 20) * playerManager.playerDir , weaponManager.bulletSPD, weaponManager.bulletATK);
                burret.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (Quaternion.Euler(0, 0, -20) * playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);

                burret.transform.position = gameObject.transform.position; // どこから発射されるか
                burret1.transform.position = gameObject.transform.position; // どこから発射されるか
                burret2.transform.position = gameObject.transform.position; // どこから発射されるか


                weaponState.ReduceMagazineCount(); // マガジンカウントを減らす


               
            }
            isFarstShot = false;

            shotgunInterval -= Time.deltaTime;

            if (shotgunInterval <= 0)
            {
                var burret = ObjectPool.instance.GetBullet(0);
                var burret1 = ObjectPool.instance.GetBullet(0);
                var burret2 = ObjectPool.instance.GetBullet(0);

                burret2.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                burret1.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (Quaternion.Euler(0, 0, 20) * playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                burret.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (Quaternion.Euler(0, 0, -20) * playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);

                burret.transform.position = gameObject.transform.position; // どこから発射されるか
                burret1.transform.position = gameObject.transform.position; // どこから発射されるか
                burret2.transform.position = gameObject.transform.position; // どこから発射されるか


                weaponState.ReduceMagazineCount(); // マガジンカウントを減らす
            }
        }
        else if (weaponState.IsReLoad)
        {
            isFarstShot = false;

            shotgunInterval = shotgunMaxInterval;　 // 弾発射ののインターバル初期化
        }



    }
    private void sniperRifleShot()
    {
        // リロード中か回避中は弾を撃てない
        if (!weaponState.IsReLoad && !playerManager.isDodge)
        {
            if (isFarstShot)
            {
                var burret = ObjectPool.instance.GetBullet(0);

                burret.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                burret.transform.position = gameObject.transform.position; // どこから発射されるか

                weaponState.ReduceMagazineCount(); // マガジンカウントを減らす


            }
            isFarstShot = false;

            sniperRiflInterval -= Time.deltaTime;

            if (sniperRiflInterval <= 0)
            {
                var burret = ObjectPool.instance.GetBullet(0);

                burret.GetComponent<PlayerBullet>().SettingShotAbility // 弾の能力値をセット
                    (playerManager.playerDir, weaponManager.bulletSPD, weaponManager.bulletATK);
                burret.transform.position = gameObject.transform.position; // どこから発射されるか


                
                weaponState.ReduceMagazineCount(); // マガジンカウントを減らす

                sniperRiflInterval = sniperRiflMaxInterval;　 // 弾発射ののインターバル初期化
            }
        }
        else if (weaponState.IsReLoad)
        {
            isFarstShot = false;

            sniperRiflInterval = sniperRiflMaxInterval; // 弾発射ののインターバル初期化
        }



    }*/
}
