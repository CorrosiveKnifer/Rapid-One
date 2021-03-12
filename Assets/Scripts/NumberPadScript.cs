using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPadScript : MonoBehaviour
{
    public bool isShowing;
    public Text displayText;
    public string TargetText;

    private string number;
    private Canvas display;
    
    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        display.enabled = false;
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
        }
    }

    public void Delete()
    {
        if(number.Length > 0)
            number = number.Substring(0, number.Length - 1);
    }
}