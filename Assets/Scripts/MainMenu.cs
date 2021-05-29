using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// William de Beer
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Useless now, use LevelLoader.cs instead

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MenuScreen")
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                SceneManager.LoadScene(0);
            }
        }
    }

}
