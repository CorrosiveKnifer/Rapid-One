using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : Interactable
{
    public bool isComplete = false;
    public string objectiveName = "Objective";
    private string goalText;
    public bool hasAnimation = false;
    public MeshRenderer togglableMesh;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        goalText = "[  ] " + objectiveName;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Activate(Interactor other)
    {
        if (!isComplete)
        {
            // Change bool
            isComplete = true;

            // Play sound
            if (GetComponent<AudioAgent>())
            {
                AudioAgent agent = GetComponent<AudioAgent>();
                if (agent.AudioClips.Length != 0)
                    agent.PlaySoundEffect(agent.AudioClips[0].name);
            }

            // Change model / mat
            if (togglableMesh != null)
            {
                togglableMesh.enabled = false;
            }
            if (animator != null)
            {
                animator.SetTrigger("Start");
            }

            // Debug log
            goalText = "[X] " + objectiveName;
            Debug.Log(goalText);
        }
    }
}
