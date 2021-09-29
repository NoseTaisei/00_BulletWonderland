using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName;

    public float currentHP;
    public float maxHP;

    public float SPD;

    public float bulletATK;
    public float bulletSPD;
}
