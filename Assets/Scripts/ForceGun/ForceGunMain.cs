using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGunMain : MonoBehaviour, IGrabber<IGrabbable>
{
    // Start is called before the first frame update
    public float maxDistance;
    public GameObject lastFocused;
    public GameObject currentFocused;
    
    public GameObject target;
    public AnimationCurve holdCurve;
    public float holdForceMultiplier;
    
    public GameObject heldObject;
    private Rigidbody heldObjectRB;
    
    private float oldDrag;
    public float newDrag;
    private float distance;

    public bool objectHeld = false;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectHeld)
        {
            HoldObject();
        }
        else
        {
            CheckForGrabbables();
        }
    }

    private void CheckForGrabbables()
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
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && (currentFocused.GetComponent<IGrabbable>() != null))
        {
            Grab(currentFocused.GetComponent<IGrabbable>());
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

    private void HoldObject()
    {
        distance = Vector3.Distance(target.transform.position, heldObject.transform.position);
        
        heldObjectRB.AddForce(Vector3.Normalize(target.transform.position - heldObject.transform.position) * holdCurve.Evaluate(distance) * heldObjectRB.mass * holdForceMultiplier) ;
        heldObjectRB.drag =  0.5f +   1/distance + newDrag * 1/Mathf.Pow(distance,2);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Drop(heldObject.GetComponent<IGrabbable>());
        }

        if ( Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) >= 0.01f)
        {
            Vector3 targetNewPos = target.transform.position + (target.transform.position-this.transform.position).normalized  * Input.GetAxis("Mouse ScrollWheel") * 5;
            if (Vector3.Distance(targetNewPos, this.transform.position) >= 2 && Vector3.Distance(targetNewPos, this.transform.position) <= 14)
            {
                target.transform.position += (target.transform.position-this.transform.position).normalized * Input.GetAxis("Mouse ScrollWheel") * 5;
            }

        }
    }

    public void Grab(IGrabbable grabbable)
    {
        grabbable.Grabbed();
        if (currentFocused.GetComponent<Rigidbody>() != null)
        {
            heldObject = currentFocused.gameObject;
            heldObjectRB = heldObject.GetComponent<Rigidbody>(); 
            oldDrag = heldObjectRB.drag;
            target.transform.rotation = heldObject.transform.rotation;
            target.transform.position = heldObject.transform.position;
            heldObjectRB.useGravity = false;
            objectHeld = true;
        }

       
        
        
        
        

        
        
        
        
    }

    public void Drop(IGrabbable grabbable)
    {
        grabbable.Dropped();
        heldObjectRB.drag = oldDrag;
        heldObjectRB.useGravity = true;
        heldObject = null;
        objectHeld = false;
    }
    
}

