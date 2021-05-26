using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{
    public bool GameIsPaused;

    public GameObject pMenu;

    // Update is called once per frame
    void Update()
    {
        
        if (GameIsPaused)
        {
                pMenu.SetActive(true);
                Time.timeScale = 0f;
        }
        else
        {
                pMenu.SetActive(false);
                Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameIsPaused = !GameIsPaused;
        }


    }

    public void Resume()
    {
        GameIsPaused = !GameIsPaused;
    }


   /* public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScreen");
    }
}
       