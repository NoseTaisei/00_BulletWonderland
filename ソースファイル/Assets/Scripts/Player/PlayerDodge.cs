using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;

    private SpriteRenderer sp;

    private void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetMouseButtonDown(1))
        {
            playerManager.isDodge = true;
        }   
        if(playerManager.isDodge) 
        {
            gameObject.tag = "isDodgePlayer";  // 回避中タグを変えることで、敵の弾が貫通するようになる

            playerManager.SPD = playerManager.dodgeSPD;　// 回避中はスピードを速くする

            sp.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            gameObject.tag = "Player";

            playerManager.SPD = playerManager.normalSPD; // 元のスピードに戻している

            
        }
    }
    public void DodgeBoolFalse() // Animationで呼びだす用
    {
        playerManager.isDodge = false;

        Color tempColor = sp.color;
        tempColor.a = 1f;
        sp.color = tempColor;
        
    }
}
