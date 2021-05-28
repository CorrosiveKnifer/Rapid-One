using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// William de Beer
/// </summary>
public class LevelLoader : MonoBehaviour
{
    #region Singleton

    public static LevelLoader GetInstance()
    {
        if (instance == null)
        {
            GameObject loader = GameObject.Instantiate(Resources.Load("LevelLoader.prefab") as GameObject, Vector3.zero, Quaternion.identity);
            return loader.GetComponent<LevelLoader>();
        }

        return instance;
    }

    private static LevelLoader instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Second Instance of LevelLoader was created, this instance was destroyed.");
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
    #endregion

    public Animator transition;

    public float transitionTime = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MenuScreen")
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                StartCoroutine(LoadLevel(0));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings <= SceneManager.GetActiveScene().buildIndex + 1) // Check if index exceeds scene count
        {
            StartCoroutine(LoadLevel(0)); // Load menu
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); // Loade next scene
        }
    }
    public void ResetScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play Animation
        transition.SetTrigger("Start");

        // Wait to let animation finish playing
        yield return new WaitForSeconds(transitionTime);

        if (levelIndex == 0 || levelIndex == SceneManager.sceneCountInBuildSettings - 1) // Check if either in menu or end screen
        {
            Cursor.lockState = CursorLockMode.None; // Make cursor usable.
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Make cursor unusable.
            Cursor.visible = false;
        }

        // Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
