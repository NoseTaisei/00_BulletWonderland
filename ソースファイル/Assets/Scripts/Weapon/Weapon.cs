using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;

    public float bulletATK;
    public float bulletSPD;

    public int magazineCount;
    public float reloadSPD;
}

