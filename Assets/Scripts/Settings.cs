using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// William de Beer
/// </summary>
public class Settings : MonoBehaviour
{
    [Header("Controls")]
    public InputField m_sensitivityField;
    public Slider m_sensitivitySlider;

    [Header("Sounds")]
    public Slider m_masterVolume;
    public Slider m_soundEffectsVolume;
    public Slider m_backgroundVolume;

    void Start()
    {
        m_sensitivityField.text = GameManager.PlayerSensitivity.ToString();
    }

    void Update()
    {
        GameManager.MasterVolume = m_masterVolume.value;
        GameManager.SoundEffectVolume = m_soundEffectsVolume.value;
        GameManager.BackGroundVolume = m_backgroundVolume.value;

        if (m_sensitivitySlider.value != GameManager.PlayerSensitivity)
        {
            GameManager.PlayerSensitivity = m_sensitivitySlider.value;
            m_sensitivityField.text = GameManager.PlayerSensitivity.ToString();
        }
    }
    public void SetSensitivity()
    {
        string newSensitivity = m_sensitivityField.text;
        if (int.TryParse(newSensitivity, out int i))
        {
            GameManager.PlayerSensitivity = i;
            m_sensitivitySlider.value = i;
        }
        else
        {
            Debug.Log("Wrong data type used for sensitivity.");
        }
    }
    public void ToggleSettings()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void ShowSettings()
    {
        gameObject.SetActive(true);
    }
    public void HideSettings()
    {
        gameObject.SetActive(false);
    }
}
