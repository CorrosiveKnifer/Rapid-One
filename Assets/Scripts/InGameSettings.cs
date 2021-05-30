using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// William de Beer
/// </summary>
public class InGameSettings : MonoBehaviour
{
    [Header("Controls")]
    public InputField m_sensitivityField;
    public Slider m_sensitivitySlider;
    public Image m_sensitivityShiftAdult;
    public Text m_sensitivityText;

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

        if (!PlayerController.instance.m_isAdultForm)
        {
            m_sensitivityShiftAdult.GetComponentInChildren<Image>().color = new Color32(36, 36, 36, 100);
            m_sensitivityText.GetComponentInChildren<Text>().color = Color.white;
        }
        else if (PlayerController.instance.m_isAdultForm)
        {
            m_sensitivityShiftAdult.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 100);
            m_sensitivityText.GetComponentInChildren<Text>().color = Color.black;
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
