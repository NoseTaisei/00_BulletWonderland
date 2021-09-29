using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    private bool isCheck = false;
    public bool IsCheck { get => isCheck; }

    // Update is called once per frame
    void Update()
    {
        OnCheck();
    }
    void OnCheck()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            isCheck = true;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            isCheck = false;
        }
    }
}
