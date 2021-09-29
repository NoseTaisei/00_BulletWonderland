using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBulletReload : MonoBehaviour
{
    [SerializeField]
    private WeaponState weaponState;

    [SerializeField]
    private GameObject reloadBar,reloadCursor;

    [SerializeField]
    private Transform reloadUIPoint,startPoint,endPoint;

    private float time = 0;
     

    // Start is called before the first frame update
    void Start()
    {
        reloadBar.transform.position = reloadUIPoint.transform.position;
        reloadCursor.transform.position = startPoint.transform.position;

     
        //journeyLength = Vector2.Distance(startPoint.position,endPoint.position);

        reloadBar.SetActive(false);
        reloadCursor.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(weaponState.IsReLoad)
        {
            reloadBar.SetActive(true);
            reloadCursor.SetActive(true);
            time += Time.deltaTime;
            reloadCursor.transform.position = Vector3.Lerp(startPoint.transform.position,endPoint.position,time / weaponState.reloadTime);
            
            //float newPos = Mathf.SmoothDamp(reloadCursor.transform.position.x,endPoint.position.x,ref _currentVelocity,weaponState.reloadTime);
            //reloadCursor.transform.position = new Vector2(newPos,reloadCursor.transform.position.y);
            // Debug.Log(time / weaponState.reloadTime);
            // reloadCursor.transform.position = Vector3.Lerp(reloadCursor.transform.position,endPoint.position,weaponState.reloadTime * 0.01f );
            // reloadCursor.transform.position = pos;
        }
        else
        {
            time = 0;
            reloadBar.SetActive(false);
            reloadCursor.SetActive(false);
            reloadCursor.transform.position = startPoint.transform.position;
        }
    }
}
