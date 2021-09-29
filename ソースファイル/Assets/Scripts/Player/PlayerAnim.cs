using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private PlayerDodge playerDodge;

    [SerializeField] 
    private Animator animator;

    private void FixedUpdate()
    {
        MoveAnimSetting();　// Animationの切り替えは、移動がFixedUpdateの為、合わせている
        DodgeAnimSetting();
    }
    private void MoveAnimSetting()
    {
        // 歩行アニメ
        animator.SetFloat("X", playerManager.playerDir.x);
        animator.SetFloat("Y", playerManager.playerDir.y);
        animator.SetFloat("Speed", playerManager.rb2d.velocity.magnitude);

        // アイドルアニメ
        if (playerManager.rb2d.velocity.magnitude == 0)
        {
            animator.SetFloat("IdleX", playerManager.playerDir.x);
            animator.SetFloat("IdleY", playerManager.playerDir.y);
        }
    }
    private void DodgeAnimSetting()
    {
        if(playerManager.isDodge)
        {
            animator.SetBool("isDodge", true);
        }
        else
        {
            animator.SetBool("isDodge", false);
        }  
    }
  
}
