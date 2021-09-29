using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : MonoBehaviour
{
    [SerializeField]
    private WeaponManager weaponManager;

    private bool isReLoad = false;
    public bool IsReLoad { get => isReLoad;}

    public float reloadTime; // UIBulletReloadで使う用

    public enum PlayerWeaponState
    {
        Handgun,
        Shotgun,
        SniperRifle
    }

    public PlayerWeaponState playerWeaponState;

    private void Start()
    {
        playerWeaponState = PlayerWeaponState.Handgun; // 初期はハンドガン
        InitialSetting();
    }
    private void Update()
    {
        if(!isReLoad)
        {
            int index = (int)playerWeaponState;
            reloadTime = weaponManager.currrentReloadTime[index];
        }
        


        WeaponChange();
        BulletReload();
    }
    private void InitialSetting()
    {
        for(int i = 0; i < 3; i++)
        {
            weaponManager.StartSettingMagazineCount(i);
            weaponManager.SettingReloadTime(i);
        }
    }
    private void WeaponSelect()
    {
        int index = (int)playerWeaponState;
        weaponManager.SettingWeponAbility(index); // 現在の武器の能力値をセット       
    }
    private void WeaponChange()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(!isReLoad) // リロード中は武器の切り替えが出来ない
        {
            if (scroll < 0) // 下にホイール
            {
                switch (playerWeaponState)
                {
                    case PlayerWeaponState.Handgun:
                        playerWeaponState = PlayerWeaponState.Shotgun;
                        //Debug.Log("現在の武器はショットガンです");
                        break;
                    case PlayerWeaponState.Shotgun:
                        playerWeaponState = PlayerWeaponState.SniperRifle;
                        //Debug.Log("現在の武器はスナイパーライフルです");
                        break;
                    case PlayerWeaponState.SniperRifle:
                        playerWeaponState = PlayerWeaponState.Handgun;
                        //Debug.Log("現在の武器はハンドガンです");
                        break;
                }

            }
            else if (scroll > 0) // 上にホイール
            {
                switch (playerWeaponState)
                {
                    case PlayerWeaponState.Handgun:
                        playerWeaponState = PlayerWeaponState.SniperRifle;
                        //Debug.Log("現在の武器はスナイパーライフルです");
                        break;
                    case PlayerWeaponState.Shotgun:
                        playerWeaponState = PlayerWeaponState.Handgun;
                        //Debug.Log("現在の武器はハンドガンです");
                        break;
                    case PlayerWeaponState.SniperRifle:
                        playerWeaponState = PlayerWeaponState.Shotgun;
                        //Debug.Log("現在の武器はショットガンです");
                        break;
                }
            }
            WeaponSelect(); // 現在の武器の能力値を入れる
        }
        
    }
    public void ReduceMagazineCount()
    {
        int index = (int)playerWeaponState;

        weaponManager.currentManazineCounts[index]--;
        if (weaponManager.currentManazineCounts[index] <= 0)
        {
            isReLoad = true;
        }
    }
    public void ReduceReloadTime()
    {
        if (isReLoad)
        {
            int index = (int)playerWeaponState;

            // タイムを減らしていく
            weaponManager.currrentReloadTime[index] -= Time.deltaTime;

            if (weaponManager.currrentReloadTime[index] <= 0)
            {
                isReLoad = false;
                weaponManager.SettingReloadTime(index); // タイムを初期化
                weaponManager.StartSettingMagazineCount(index);// マガジンを初期化
                reloadTime = 0;
            }
        }   
    }
    public void BulletReload()
    {
        int index = (int)playerWeaponState;

        // 弾が最大だとリロード出来ない
        if (Input.GetKeyDown(KeyCode.R) 
            && weaponManager.currentManazineCounts[index] != weaponManager.maxMagazineCounts[index])
        {
            isReLoad = true;
        }
        else
        {
            ReduceReloadTime();
        }
    　　
    }
}
