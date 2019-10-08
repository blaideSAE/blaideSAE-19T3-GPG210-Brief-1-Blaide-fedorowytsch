using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoObjectHeld : StateBase
{
    public float maxDistance;
    //private GameObject lastFocused;
    private GameObject currentFocused;
    void Start()
    {
        
    }
    public override void Enter()
    {

    }

    public override void Execute()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            currentFocused = hit.collider.gameObject;
            ObjectFocus focusScript = currentFocused.GetComponent<ObjectFocus>();
            if (focusScript != null)
            {
                focusScript.GotFocus();
            }

        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Use();
        }

    }

    private void Use()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    public override void Exit()
    {
    
    }
}
