using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoObjectHeld : StateBase, IGrabber<IGrabbable>
{
    public float maxDistance;
    public GameObject lastFocused;
    public GameObject currentFocused;
    public BasicHold basicHold;
    void Start()
    {
        basicHold = gameObject.GetComponent<BasicHold>();
    }
    public override void Enter()
    {

    }

    public override void Execute()
    {
        int layerMask = 1 << 2;
       layerMask = ~layerMask;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
           // if (hit.collider.gameObject.GetComponent<Outline>() != null)
           // {
              currentFocused = hit.collider.gameObject;  
           // }
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            currentFocused = null;
            if (lastFocused != null)
            {
                if (lastFocused != currentFocused && lastFocused.GetComponent<Outline>() != null) 
                {
                    lastFocused.GetComponent<Outline>().enabled = false;
                }
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

        if (  currentFocused != null)
        {
           if (lastFocused != null)
           {
               if (lastFocused != currentFocused && lastFocused.GetComponent<Outline>() != null) 
               {
                   lastFocused.GetComponent<Outline>().enabled = false;
               }
           }

           if (currentFocused.GetComponent<Outline>() != null)
           {
                currentFocused.GetComponent<Outline>().enabled = true;

           }
           lastFocused = currentFocused;
        }
    }

    private void Fire()
    {
        if(currentFocused.GetComponent<IGrabbable>() != null)
        {
           Grab(currentFocused.GetComponent<IGrabbable>());

        }

    }

    public void Grab(IGrabbable grabbable)
    {
        grabbable.Grabbed();
        if (currentFocused.GetComponent<Rigidbody>() != null)
        {
            basicHold.heldObject = currentFocused.gameObject;
            sM.ChangeState(nextState); 
        }

    }

    public void Drop(IGrabbable grabbable)
    {
        grabbable.Dropped();
    }

    public override void Exit()
    {
    
    }
}
