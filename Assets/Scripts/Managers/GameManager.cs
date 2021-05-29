using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Michael Jordan, William de Beer
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Second Instance of GameManager was created, this instance was destroyed.");
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    #endregion

    //Volume Settings
    public static float MasterVolume { get; set; } = 1.0f;
    public static float SoundEffectVolume { get; set; } = 1.0f;
    public static float BackGroundVolume { get; set; } = 1.0f;
    public static float HighScore { get; set; } = 0.0f;
    public static float PlayerSensitivity { get; set; } = 150f;

    public double GameTime = 0.0;


    private void Start()
    {
        GetComponent<AudioAgent>()?.PlayBackground(GetComponent<AudioAgent>().AudioClips[0].name, true);
    }

    // Update is called once per frame
    void Update()
    {
        GameTime += Time.deltaTime;
    }
}

