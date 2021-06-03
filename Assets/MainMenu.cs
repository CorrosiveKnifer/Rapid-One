using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioAgent>().PlayBackground(GetComponent<AudioAgent>().AudioClips[0].name, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        LevelLoader.GetInstance().LoadNextLevel();
    }
    public void QuitGame()
    {
        LevelLoader.GetInstance().QuitGame();
    }
}
