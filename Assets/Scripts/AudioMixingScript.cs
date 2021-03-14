using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMixingScript : MonoBehaviour
{
    public AudioClip slowClip;
    public AudioClip fastClip;

    private AudioSource slowSource;
    private AudioSource fastSource;
    private bool isSlow = true;

    public float TIME_CONSTANT = 1.665f;
    // Start is called before the first frame update
    void Awake()
    {
        slowSource = gameObject.AddComponent<AudioSource>();
        slowSource.clip = slowClip;
        fastSource = gameObject.AddComponent<AudioSource>();
        fastSource.clip = fastClip;

        slowSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSlow = !isSlow;

            if(isSlow)
            {
                //Convert larger time based on a smaller time.
                slowSource.time = fastSource.time * TIME_CONSTANT + (fastSource.time / 60.0f)*1.0f;
                //Debug.Log($"slow = {slowSource.time} fast = {fastSource.time}");
                StartCoroutine(Fade());
            }
            else
            {
                //Convert smaller time based on a larger time.
                fastSource.time = slowSource.time / TIME_CONSTANT - (slowSource.time/60.0f)*1.0f;
                //Debug.Log($"slow = {slowSource.time} fast = {fastSource.time}");
                StartCoroutine(Fade());
            }
        }
    }
    IEnumerator Fade()
    {
        fastSource.Play();
        slowSource.Play();

        float ratio = 0.0f;
        float ratioStep = 0.025f;

        while(ratio < 1.0f)
        {
            ratio += ratioStep;

            if (isSlow)
            {
                fastSource.volume = 1 - ratio;
                slowSource.volume = ratio;
            }
            else
            {
                fastSource.volume = ratio;
                slowSource.volume = 1 - ratio;
            }

            yield return new WaitForEndOfFrame();
        }

        if (isSlow)
        {
            fastSource.Pause();
        }
        else
        {
            slowSource.Pause();
        }

        yield return null;
    }
}
