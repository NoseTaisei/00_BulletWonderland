using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーカーソル")]
    private GameObject targetCursor;

    private Vector2 targetCursorPos; // カーソルのポジション

    public bool IsPlayerDie = false;

    private Vector2 playerDir;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerDie = false;
    }

    // Update is called once per frame
    void Update()
    { 
        // プレイヤーが死んだら
        if(IsPlayerDie)
        {
            targetCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);　// カメラの位置をカーソルの位置にする
            targetCursor.transform.position = targetCursorPos;
        }
    }
    public Vector2 PlayerDirectionCheck(GameObject player)
    {
        targetCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);　// カメラの位置をカーソルの位置にする
        targetCursor.transform.position = targetCursorPos;

        // ターゲットカーソルの座標からプレイヤーの座標を引いて、プレイヤーの向きを求める
        playerDir =  (targetCursor.transform.position - player.transform.position).normalized;
        
        return playerDir;     
    }
}
