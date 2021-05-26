using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuScreen : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        { 
            if (GameIsPaused)
            {
                Resume();
            }    
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pMenu.SetActive(false);
        //PlayerController.instance.SetCameraFreeze(false); 
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pMenu.SetActive(true);
        //PlayerController.instance.SetCameraFreeze(true); 
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("ass");
    }

    public void QuitGame()
    {
        Debug.Log("ass");
    }
}
       