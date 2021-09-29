using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;

    private bool isInvincible = false;

    private float invincibleTime = 2f;
    private float invincibleMaxTime = 2f;
 
    private SpriteRenderer sp;

    public void DamageHealth(float amount)
    {
        if(!isInvincible)
        {
            playerManager.currentHP -= amount;
            Debug.Log("プレイヤーの" + playerManager.currentHP);

            isInvincible = true; // 無敵状態                           
        }
        
    }
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        // sp.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }
    private void Update()
    {
        if(isInvincible)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 50f));
            sp.color = new Color(1f, 1f, 1f, level); // 点滅処理

            invincibleTime -= Time.deltaTime;
            if (invincibleTime < 0)
            {
                isInvincible = false;
                invincibleTime = invincibleMaxTime;

                sp.color = new Color(1f, 1f, 1f, 1f);　//　α値を元に戻す
            }
        }
    }
}
