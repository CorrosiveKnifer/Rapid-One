using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Michael Jordan
/// </summary>
public class BackgroundMusicManager : MonoBehaviour
{
    public AudioAgent adultAgent;
    public AudioAgent childAgent;

    bool isAdult = true;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        adultAgent.QueueArchive(true);
        childAgent.QueueArchive(true);
        childAgent.PauseQueue();
        childAgent.AgentBGVolume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            Destroy(gameObject);
        }

        if(isAdult != PlayerController.instance.m_isAdultForm)
        {
            if (PlayerController.instance.m_isAdultForm)
            {
                StartCoroutine(FadeToAdult());
            }
            if (!PlayerController.instance.m_isAdultForm)
            {
                StartCoroutine(FadeToChild());
            }
            isAdult = PlayerController.instance.m_isAdultForm;
        }
    }

    IEnumerator FadeToChild()
    {
        float ratio = 0.0f;
        float ratioStep = 0.035f;

        childAgent.StartQueue();

        while (ratio < 1.0f)
        {
            ratio += ratioStep;

            adultAgent.AgentBGVolume = ratio;
            childAgent.AgentBGVolume = 1.0f - ratio;
        
            yield return new WaitForEndOfFrame();
        }

        adultAgent.AgentBGVolume = 0.0f;
        childAgent.AgentBGVolume = 1.0f;
        adultAgent.PauseQueue();
        yield return null;
    }
    IEnumerator FadeToAdult()
    {
        float ratio = 0.0f;
        float ratioStep = 0.05f;

        adultAgent.StartQueue();

        childAgent.AgentBGVolume = 0.0f;
        adultAgent.AgentBGVolume = 1.0f;

        childAgent.PauseQueue();
        yield return null;
    }
}
