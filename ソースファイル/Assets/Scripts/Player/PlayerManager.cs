using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private PlayerData playerData;　// ScriptableObject

    [SerializeField]
    private Cursor cursor;

    public string playerName; // playerDataの値を代入する
    public float currentHP;
    public float maxHP;
    public float SPD;
    public float bulletATK;
    public float bulletSPD;

    public bool isDodge　= false; // 回避中か
    public float dodgeSPD; // 回避中のスピード
    public float normalSPD; // 元のスピード

    public Rigidbody2D rb2d;

    public Vector2 playerDir; // プレイヤーの向き

    private void Awake()
    {
        playerName = playerData.playerName;
        currentHP = playerData.currentHP;
        maxHP = playerData.maxHP;
        SPD = playerData.SPD;
        bulletATK = playerData.bulletATK;
        bulletSPD = playerData.bulletSPD;

        normalSPD = SPD;
        dodgeSPD = SPD * 1.5f;

        // playerDir = gameObject.GetComponent<Cursor>().PlayerDirectionCheck(this.gameObject);
    }
    private void Update()
    {
        if(currentHP <= 0)
        {
            cursor.IsPlayerDie = true;
            gameObject.SetActive(false);
        }
        else if(currentHP >= 0)
        {
            playerDir =  cursor.PlayerDirectionCheck(gameObject);
        }

        // PlayerDirectionCheck();
    }
    /*
    private void PlayerDirectionCheck()
    {
        targetCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);　// カメラの位置をカーソルの位置にする
        targetCursor.transform.position = targetCursorPos;

        // ターゲットカーソルの座標からプレイヤーの座標を引いて、プレイヤーの向きを求める
        playerDir = (targetCursor.transform.position - transform.position).normalized;     
    }*/
}
