using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void DebugReset2()
    {
        SceneManager.LoadScene (1);
        Time.timeScale = 1.0f;
    }
}
