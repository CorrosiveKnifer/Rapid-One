﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MenuScreen")
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                StartCoroutine(LoadLevel(0));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings <= SceneManager.GetActiveScene().buildIndex + 1)
        {
            StartCoroutine(LoadLevel(0));
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play Animation
        transition.SetTrigger("Start");

        // Wait to let animation finish playing
        yield return new WaitForSeconds(transitionTime);

        if (levelIndex == 0 || levelIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
