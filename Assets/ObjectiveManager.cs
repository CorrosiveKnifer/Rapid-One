using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public Objective[] Objectives;
    public Text childObjectiveText;
    public Text adultObjectiveText;
    public ObjectiveRadius bedObjective;

    private string objectiveList;
    private bool finalGoalVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        Objectives = FindObjectsOfType<Objective>();

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
            childObjectiveText.text = "Board the pirate ship";
            adultObjectiveText.text = "Go to bed";
            finalGoalVisible = true;

            if (bedObjective != null)
            {
                bedObjective.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No final objective in ObjectiveManager");
            }
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
