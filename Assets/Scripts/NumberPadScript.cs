using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        interactable = otherObject.GetComponent<Interactable>();
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
        display.enabled = true;
        player.GetComponent<Player>().DisableControl();
    }

    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<Player>().EnableControl();
        display.enabled = false;
        number = "";
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
            Hide();
            interactable.Activate();
        }
    }

    public void Delete()
    {
        if(number.Length > 0)
            number = number.Substring(0, number.Length - 1);
    }
}