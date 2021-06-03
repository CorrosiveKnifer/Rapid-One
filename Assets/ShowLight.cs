using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>
public class ShowLight : MonoBehaviour
{
    public GameObject Light;

    public void Show()
    {
        Light.SetActive(true);
    }
    public void Hide()
    {
        Light.SetActive(false);
    }
}
