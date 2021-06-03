using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{ 
   //public static bool GameIsPaused = false;
    public bool GameIsPaused;

    public GameObject pMenu;
    public Image resume;

        // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        PlayerController.instance.SetCameraFreeze(false);
        PlayerController.instance.IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
        pMenu.SetActive(false);
    }

    public void Pause()
    {
        PlayerController.instance.SetCameraFreeze(true);
        PlayerController.instance.IsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        GameIsPaused = true;
        pMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}

    

