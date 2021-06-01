using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// William de Beer, rachael colaco
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
            Debug.Log("Second Instance of LevelLoader was created, this instance was destroyed.");
            Destroy(gameObject);
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

    public GameObject loadingscreen;

    bool canShowLoadingScreen = false;
    bool isLoading = false;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MenuScreen")
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                StartCoroutine(LoadLevel(0));
                canShowLoadingScreen = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
        //For testing purposes
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
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
            canShowLoadingScreen = true;
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); // Loade next scene
            canShowLoadingScreen = true;
            

        }
    }
    public void ResetScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        canShowLoadingScreen = true;
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

        if (canShowLoadingScreen)
        {
            canShowLoadingScreen = false;
            StartCoroutine(LoadAsychronously(levelIndex));
        }
        // Load Scene
        //SceneManager.LoadScene(levelIndex);
        //if(levelIndex == 2)
        //   StartCoroutine(LoadAsychronously(levelIndex));
        //else
        //SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadAsychronously(int sceneIndex)
    {
        if(isLoading)
            yield return null;

        isLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingscreen.SetActive(true);
        canShowLoadingScreen = false;
        Slider slider = loadingscreen.GetComponentInChildren<Slider>();
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            slider.value = progress;

            
            yield return null;
        }

        isLoading = false;
        loadingscreen.SetActive(false);
        transition.SetTrigger("Blink");
    }
}
