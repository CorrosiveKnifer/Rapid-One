using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public Objective[] Objectives;
    public Text childObjectiveText;
    public Text adultObjectiveText;

    private string objectiveList;
    private bool finalGoalVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }
    // Update is called once per frame
    void Update()
    {
        bool allComplete = true;
        foreach (var obj in Objectives)
        {
            if (!obj.isComplete)
                allComplete = false;
        }

        if (allComplete && !finalGoalVisible)
        {
            // show bed objective
            childObjectiveText.text = "Go to bed";
            adultObjectiveText.text = "Go to bed";
            finalGoalVisible = true;
        }
    }

    public void UpdateText()
    {
        objectiveList = "";
        foreach (var obj in Objectives)
        {
            objectiveList += obj.goalText + "\n";
        }
        childObjectiveText.text = objectiveList;
        adultObjectiveText.text = objectiveList;
    }
}
