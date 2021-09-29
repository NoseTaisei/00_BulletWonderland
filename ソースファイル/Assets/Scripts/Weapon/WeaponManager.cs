using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Weapon[] weaponType; // ScriptableObject

    public float bulletATK;
    public float bulletSPD;

    public int[] maxMagazineCounts;
    public int[] currentManazineCounts;

    public float[] reloadSPD;
    public float[] currrentReloadTime;

    private void Awake()
    {
        // 配列を初期化
        currentManazineCounts = new int[3];　
        maxMagazineCounts = new int[3];

        reloadSPD = new float[3];
        currrentReloadTime = new float[3];
    }

    public void SettingWeponAbility(int x)　// 武器の能力値をセット
    {
        bulletATK = weaponType[x].bulletATK;
        bulletSPD = weaponType[x].bulletSPD;     
    }
    public void StartSettingMagazineCount(int x) //　全ての弾の量をセット
    {
        maxMagazineCounts[x] = weaponType[x].magazineCount;
        currentManazineCounts[x] = maxMagazineCounts[x];
    } 
    public void SettingReloadTime(int x) //　武器ごとのリロードの時間をセット
    {
        reloadSPD[x] = weaponType[x].reloadSPD;
        currrentReloadTime[x] = reloadSPD[x];
    }
}
