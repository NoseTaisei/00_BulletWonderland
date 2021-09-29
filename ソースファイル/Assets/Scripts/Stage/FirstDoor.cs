using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
     [SerializeField]
    private GameObject[] firstDoor;

    public void OpenFirstDoor()
    {
        firstDoor[0].SetActive(false);
        firstDoor[1].SetActive(true);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerCheck player = other.GetComponent<PlayerCheck>();
            if(player.IsCheck && player != null)
            {
                OpenFirstDoor();
            }
        }
    }
   
}
