using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
    }

    public void OnPauseButtun()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }   
        else if(Time.timeScale == 0)
        {
            Time.timeScale = 1.0f;
            pauseUI.SetActive(false);
        }     
    }
    public void DebugReset()
    {
        SceneManager.LoadScene (0);
        Time.timeScale = 1.0f;
    }
}
