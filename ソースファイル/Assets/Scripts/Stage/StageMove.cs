using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageMove : MonoBehaviour
{
    [SerializeField]
    private GameObject[] notReturnWalls; // 帰させない壁

    [SerializeField]
    private GameObject[] stopWalls; // 進めない壁

    [SerializeField]
    private GameObject[] stageEmemys;

    // Start is called before the first frame update
    void Start()
    {
        SetActive(notReturnWalls,false);
        SetActive(stageEmemys,false);
    }
    void Update() 
    {
        // ステージの敵を全て倒したら
        if(stageEmemys.All(enemy => !enemy.activeSelf))
        {
            SetActive(stopWalls,false);
        }
    }
    void SetActive(GameObject[] objs,bool isActive)
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(isActive);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "isDodgePlayer")
        {
            for(int i = 0; i < notReturnWalls.Length; i++)
            {
                SetActive(stopWalls,true);
                SetActive(notReturnWalls,true);
                SetActive(stageEmemys,true);     
            }
        }
    }
}
