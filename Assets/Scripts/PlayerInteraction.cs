using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance;
   // public Trigger currentFocused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
    }

     void Use()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            /*if (hit.collider.gameObject.GetComponent<Trigger>() != null)
            { 
                currentFocused = hit.collider.gameObject.GetComponent<Trigger>();
                currentFocused.OnTriggered();
            }*/
        }
        
    }
}
