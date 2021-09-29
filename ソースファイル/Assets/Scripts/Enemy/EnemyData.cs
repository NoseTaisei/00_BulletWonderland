using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public float HP;

    public float SPD;

    public float bulletATK;
    public float bulletSPD;
}