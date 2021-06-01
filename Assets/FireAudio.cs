using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudio : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<AudioAgent>().Play3DSoundEffect(GetComponent<AudioAgent>().AudioClips[0].name, true);
    }
    private void OnDisable()
    {
        GetComponent<AudioAgent>().StopAllAudio();
    }
}
