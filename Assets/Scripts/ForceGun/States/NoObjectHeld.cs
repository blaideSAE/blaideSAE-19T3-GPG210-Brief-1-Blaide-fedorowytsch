using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoObjectHeld : StateBase
{
    public float maxDistance;
    public GameObject lastFocused;
    public GameObject currentFocused;
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
            Use();
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
