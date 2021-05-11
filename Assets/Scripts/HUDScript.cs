using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Michael Jordan
/// </summary>
public class HUDScript : MonoBehaviour
{
    public GameObject Cursor;
    public GameObject Shift;
    public GameObject Hand;
    public GameObject Interact;

    public Sprite AdultShift;
    public Sprite ChildShift;
    public Sprite HandOpen;
    public Sprite HandClose;

    public Text m_TextDisplay;

    public bool isHandOpen { get; set; }

    private CameraAgent agent;
    private float m_displayTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = CameraController.instance.agent;
        Hand.SetActive(false);
        Interact.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Hand.GetComponent<Image>().sprite = (isHandOpen) ? HandOpen : HandClose;

        switch (agent.currentState)
        {
            case CameraAgent.AgentState.FOLLOW_ADULT:
                Shift.SetActive(true);
                Shift.GetComponent<Image>().sprite = AdultShift;
                break;
            case CameraAgent.AgentState.FOLLOW_CHILD:
                Shift.SetActive(true);
                Shift.GetComponent<Image>().sprite = ChildShift;
                break;
            default:
            case CameraAgent.AgentState.SHIFTTING:
                Shift.SetActive(false);
                break;
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
}
