using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

/// <summary>
/// Michael Jordan
/// </summary>
public class HUDScript : MonoBehaviour
{
    #region Singleton

    public static HUDScript instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Second Instance of HUDScript was created, this instance was destroyed.");
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    #endregion

    public GameObject Cursor;
    public GameObject Shift;
    public GameObject Hand;
    public GameObject Interact;

    public Sprite AdultShift;
    public Sprite ChildShift;
    public Sprite HandOpen;
    public Sprite HandClose;

    //public Image LightDisplay;

    public Text m_TextDisplay;

    public Volume m_damageVolume;
    public float m_damage = 0.0f;

    //private float m_prevlightVal = 0.0f;
    //private float m_lightVal = 0.0f;
    public bool isHandOpen { get; set; }

    private CameraAgent agent;
    private float m_displayTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Hand.SetActive(false);
        Interact.SetActive(false);
        Shift.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Hand.GetComponent<Image>().sprite = (isHandOpen) ? HandOpen : HandClose;

        m_damage -= Time.deltaTime * 0.5f;
        m_damageVolume.weight = m_damage;
        if (m_damage < 0 || PlayerController.instance.m_isAdultForm)
        {
            m_damage = 0;
        }

        if (PlayerController.instance.m_isAdultForm)
        {
            Shift.SetActive(false);
            Shift.GetComponent<Image>().sprite = AdultShift;
        }
        else
        {
            Shift.SetActive(true);
            Shift.GetComponent<Image>().sprite = ChildShift;
        }

        /*if (LightDisplay != null)
            LightDisplay.color = new Color(m_lightVal, m_lightVal, m_lightVal); //Note: Light val is 0.0f to 1.0f
        
        m_prevlightVal = m_lightVal;
        Debug.Log(m_prevlightVal);
        m_lightVal = 0.0f;*/
    }

    public void ApplyDamage(float _damage)
    {
        m_damage += _damage;
        if (m_damage > 1)
        {
            m_damage = 1;
            PlayerController.instance.Switch();
        }
    }
    public void ShowHand()
    {
        if(!Hand.activeSelf)
            StartCoroutine(FadeIn(Hand));
        Cursor.SetActive(false);
        Interact.SetActive(false);
    }
    public void ShowCursor()
    {
        Hand.SetActive(false);
        Interact.SetActive(false);
        Cursor.SetActive(true);
    }
    public void ShowInteract()
    {
        if (!Interact.activeSelf)
            StartCoroutine(FadeIn(Interact));
        Hand.SetActive(false);
        Cursor.SetActive(false);
    }

    IEnumerator FadeIn(GameObject toFadeIn)
    {
        Image item = toFadeIn.GetComponent<Image>();

        if(item == null)
            yield return null;

        toFadeIn.SetActive(true);
        item.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        float step = 0.05f;
        float stepSquared = 0.01f;
        while(item.color.a < 1.0f)
        {
            float newColor = item.color.a + step;
            step += stepSquared;

            item.color = new Color(1.0f, 1.0f, 1.0f, newColor);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    public void DisplayText(string text)
    {
        m_TextDisplay.text = text;
        m_TextDisplay.enabled = true;
        StartCoroutine(StartDisplayingText(2.0f));
    }

    private IEnumerator StartDisplayingText(float time)
    {
        if(m_displayTimer != 0.0)
        {
            m_displayTimer = time;
            yield return null;
        }

        while (m_displayTimer >= 0.0)
        {
            yield return new WaitForEndOfFrame();
            m_displayTimer -= Time.deltaTime;
        }

        m_TextDisplay.enabled = false;
    }

    /*public void SetLight(float light)
    {
        m_lightVal = Mathf.Max(light, m_lightVal);
    }

    public float GetLight()
    {
        return m_prevlightVal;
    }*/
}
