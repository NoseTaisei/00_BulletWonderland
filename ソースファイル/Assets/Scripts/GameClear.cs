using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    private GameObject gameClear;
    void Start()
    {
        gameClear.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameClear.SetActive(true);
        }
    }
}
