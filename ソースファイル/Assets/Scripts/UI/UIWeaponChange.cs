using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIWeaponChange : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private WeaponState  WeaponState;

    [SerializeField]
    private Text weaponName;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if((int)WeaponState.playerWeaponState == 0)
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            
            weaponName.text = "ハンドガン";
        }
        else if((int)WeaponState.playerWeaponState == 1)
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);

            weaponName.text = "ショットガン";
        }
        else if((int)WeaponState.playerWeaponState == 2)
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);

            weaponName.text = "ライフル";
        }
    }  
}
