using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rachael Calaco
/// </summary>
public class GameObjectViewChanger: MonoBehaviour
{
    public GameObject m_adultGameObject = null; //the object the adult will see
    public GameObject m_childGameObject = null; //the object the child will see

    public bool KeepLocationSync = true;
    // Start is called before the first frame update
    void Start()
    {
        if(m_adultGameObject != null)
            m_adultGameObject.SetActive(PlayerController.instance.m_isAdultForm);
        if (m_childGameObject != null)
            m_childGameObject.SetActive(!PlayerController.instance.m_isAdultForm);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_adultGameObject != null)
            m_adultGameObject.SetActive(PlayerController.instance.m_isAdultForm);

        if (m_childGameObject != null)
            m_childGameObject.SetActive(!PlayerController.instance.m_isAdultForm);

        if (KeepLocationSync && m_adultGameObject != null && m_childGameObject != null)
        {
            if  (m_adultGameObject.activeSelf)
            {
                m_childGameObject.transform.position = m_adultGameObject.transform.position;
                m_childGameObject.transform.rotation = m_adultGameObject.transform.rotation;
            }
            else
            {

                m_adultGameObject.transform.position = m_childGameObject.transform.position;
                m_adultGameObject.transform.rotation = m_childGameObject.transform.rotation;
            }
        }
    }
}
