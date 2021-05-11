﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Jordan
/// </summary>

public class Interactor : MonoBehaviour
{
    public float rayDistance = 5.0f;
    public GameObject heldLocation;

    public HUDScript HUD;
    private Camera camera;
    private float strength;
    private float intellegence;

    protected struct HeldObject
    {
        public GameObject item;
        public Transform itemParent;
    }
    private HeldObject myHeldObject;

    private enum ItemType
    {
        EMPTY = 0x0000,
        ACTION = 0x0001,
        LIFT = 0x0002,
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        strength = GetComponentInChildren<Player>().m_strength;
        intellegence = GetComponentInChildren<Player>().m_intellegence;

        if (HUD != null)
            HUD.isHandOpen = true;

    }

    // Update is called once per frame
    void Update()
    {
        //Get current game object infront of the interactor.
        GameObject item = null;
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
        ushort itemType = RaycastToObjectInfront(ray, out item);

        UpdateHUD(itemType);
        MoveHeldItem();

        if (Input.GetKeyDown(KeyCode.E) && ResolveBitwise(itemType, (ushort)ItemType.ACTION))
        {
            Activate(item);
            return;
        }
        if (Input.GetMouseButton(0) && ResolveBitwise(itemType, (ushort)ItemType.LIFT))
        {
            HoldObject(item);
            return;
        }
        else if(!Input.GetMouseButton(0))
        {
            DropObject();
        }
    }
    private void UpdateHUD(ushort itemType)
    {
        if (ResolveBitwise(itemType, (ushort)ItemType.ACTION))
        {
            HUD.ShowInteract();
            return;
        }
        if (ResolveBitwise(itemType, (ushort)ItemType.LIFT))
        {
            HUD.ShowHand();
            
            return;
        }

        //else
        HUD.ShowCursor();
    }
    
    private void OnDisable()
    {
        DropObject();
    }

    private void Activate(GameObject item)
    {
        if (item.GetComponentInChildren<Interactable>().m_brainRequirement < intellegence)
        {
            item.GetComponentInChildren<Interactable>().Activate();
        }
        else
        {
            HUD.DisplayText("You aren't smart enough to interact with this item.");
        }
    }

    private void MoveHeldItem()
    {
        if (myHeldObject.item != null)
        {
            if (HUD != null)
                HUD.isHandOpen = false;

            myHeldObject.item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            myHeldObject.item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private void HoldObject(GameObject item)
    {
        if(myHeldObject.item == null)
        {
            if(item.GetComponentInChildren<Liftable>().m_myMass < strength)
            {
                myHeldObject.item = item;
                myHeldObject.itemParent = item.transform.parent;
                myHeldObject.item.GetComponent<Rigidbody>().useGravity = false;
                myHeldObject.item.GetComponent<Rigidbody>().detectCollisions = true;
                myHeldObject.item.transform.parent = heldLocation.transform;
            }
            else
            {
                HUD.DisplayText("This item is too heavy");
            }
        }
    }

    private void DropObject()
    {
        if (myHeldObject.item != null)
        {
            if (HUD != null)
                HUD.isHandOpen = true;

            Vector3 itemPos = myHeldObject.item.transform.position;
            myHeldObject.item.GetComponent<Rigidbody>().useGravity = true;
            myHeldObject.item.transform.parent = myHeldObject.itemParent;
            myHeldObject.item.transform.position = itemPos;
            myHeldObject.itemParent = null;
            myHeldObject.item = null;
        }
    }

    private bool ResolveBitwise(ushort currentType, ushort requiredType)
    {
        return (currentType & requiredType) != 0;
    }

    private ushort RaycastToObjectInfront(Ray ray, out GameObject item)
    {
        ushort result = (ushort)ItemType.EMPTY;

        RaycastHit[] hits;
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 0.5f);

        hits = Physics.RaycastAll(ray.origin, ray.direction, rayDistance);

        if(hits.Length == 0)
        {
            item = null;
            return (ushort)ItemType.EMPTY;
        }
            
        RaycastHit closestHit = hits[0];
        for (int i = 1; i < hits.Length; i++)
        {
            if (closestHit.distance > hits[i].distance)
                closestHit = hits[i];
        }

        if(closestHit.collider.gameObject.GetComponentInChildren<Interactable>())
        {
            result = (ushort)(result | (int)ItemType.ACTION);
        }
        if (closestHit.collider.gameObject.GetComponentInChildren<Liftable>())
        {
            result = (ushort)(result | (int)ItemType.LIFT);
        }
        //Note: Add more for different results.

        item = closestHit.collider.gameObject;
        return result;
    }
}