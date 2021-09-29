using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Cursor cursor;

    [SerializeField]
    private GameObject target; // カーソル
    [SerializeField]
    private GameObject player;

    private float moveStartDis = 3f; // カメラが移動し始める開始距離
    private float cameraMoveSpeed = 3f;

    void LateUpdate()
    {
        // プレイヤーが生きている時
        if(!cursor.IsPlayerDie)
        {
            // プレイヤーとカーソル間の距離を求める
            float dis = Vector2.Distance(target.transform.position, player.transform.position);

            if(dis < moveStartDis)
            {
                Vector3 pos = player.transform.position;
                pos.z = -10;
                // 一定の速度でプレイヤーの座標までカメラを移動
                transform.position = Vector3.MoveTowards(transform.position, pos, cameraMoveSpeed * 2f * Time.deltaTime) ;
            }
            else
            {
                // 2点の中心点を求めて、カメラを移動
                Vector3 center = Vector3.Lerp(player.transform.position, target.transform.position, 0.4f); // 0.4fは若干プレイヤー寄りの位置になる
                transform.position = Vector3.MoveTowards(transform.position, center + Vector3.forward * -10, cameraMoveSpeed  * Time.deltaTime);
            }

            
        }
        
        
    }
    /*
    public IEnumerator Shake(float magnitude)
    {
        var pos = transform.position;
        float time = 0f;

        var transformPos = transform.position;
        while (time < 1f)
        {
            transform.position = new Vector3(transformPos.x, transformPos.y +
                Mathf.Sin(magnitude), transformPos.z);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = pos;
    }
    */
}
