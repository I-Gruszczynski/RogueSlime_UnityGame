using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPause = true;
    public GameObject pauseBox;

    private void Start()
    {
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        pauseBox.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void Resume()
    {
        pauseBox.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        Debug.Log("Resume...");
    }
}
