using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Michael Jordan
/// </summary>
public class NumberPadScript : MonoBehaviour
{
    public bool isShowing;
    public Text displayText;
    public string TargetText;
    public GameObject player;
    public GameObject otherObject;
    
    private string number;
    private Canvas display;
    private Interactable interactable;
    private AudioAgent audio;
    // Start is called before the first frame update
    void Start()
    {
        interactable = otherObject.GetComponent<Interactable>();
        audio = GetComponent<AudioAgent>();
        number = "";
        display = GetComponent<Canvas>();
        int temp;
        if(!int.TryParse(TargetText, out temp))
        {
            Debug.LogError("Target Number for Numberpad has failed to parse!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = number;
        isShowing = display.enabled;

        for(int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0 + i))
            {
                AddDigit(i);
            }
        }

        if(Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            Hide();
        }

        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            Delete();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Enter();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }

    public void Show()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        display.enabled = true;
        PlayerController.instance.SetCameraFreeze(true);
    }

    public void Hide()
    {
        if(display.enabled)
        {
            PlayerController.instance.SetCameraFreeze(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //player.GetComponent<PlayerController>()?.EnableCameraMovement();
            display.enabled = false;
            number = "";
        }
    }

    public void AddDigit(int digit)
    {
        number += digit.ToString();
    }

    public void Enter()
    {
        if(TargetText == number)
        {
            //Got Correct code
            if (interactable is DoorScript)
                (interactable as DoorScript).isLocked = !(interactable as DoorScript).isLocked;

            interactable.Activate();
            audio.PlaySoundEffect("KeypadSuccess");
            Hide();
        }
        else
        {
            StartCoroutine(ErrorFlash());
            audio.PlaySoundEffect("KeypadError");
        }
    }

    public void Delete()
    {
        if(number.Length > 0)
            number = number.Substring(0, number.Length - 1);
    }

    IEnumerator ErrorFlash()
    {
        Color errorColor = Color.red;
        Color baseColor = displayText.color;
        number = "ERROR";
        float t = 0;
        float dt = 0.05f;
        float dt2 = 0.01f;
        while (displayText.color != errorColor)
        {
            displayText.color = Color.Lerp(baseColor, errorColor, t);
            t += dt;
            dt += dt2;

            yield return new WaitForEndOfFrame();
        }

        t = 0;
        dt = 0.05f;
        dt2 = 0.01f;
        while (displayText.color != baseColor)
        {
            displayText.color = Color.Lerp(errorColor, baseColor, t);
            t += dt;
            dt += dt2;

            yield return new WaitForEndOfFrame();
        }

        t = 0;
        dt = 0.05f;
        dt2 = 0.01f;
        while (displayText.color != errorColor)
        {
            displayText.color = Color.Lerp(baseColor, errorColor, t);
            t += dt;
            dt += dt2;

            yield return new WaitForEndOfFrame();
        }

        t = 0;
        dt = 0.05f;
        dt2 = 0.01f;
        while (displayText.color != baseColor)
        {
            displayText.color = Color.Lerp(errorColor, baseColor, t);
            t += dt;
            dt += dt2;

            yield return new WaitForEndOfFrame();
        }

        displayText.color = baseColor;
        number = "";
        yield return null;
    }
}