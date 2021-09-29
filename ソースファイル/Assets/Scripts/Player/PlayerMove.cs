using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private PlayerManager playerManager;
    private float horizontal;
    private float vertical;
    public float Horizontal { get => horizontal; }
    public float Vertical { get => vertical; }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        // 移動させる
        playerManager.rb2d.velocity = new Vector2(horizontal, vertical).normalized * playerManager.SPD;
    }
}
