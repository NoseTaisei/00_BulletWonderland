using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Cursor cursor;

    [SerializeField]
    private GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(cursor.IsPlayerDie)
        {
            gameOver.SetActive(true);
        }
    }
}
