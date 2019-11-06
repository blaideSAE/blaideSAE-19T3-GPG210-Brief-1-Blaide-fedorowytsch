using System;
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
    private Vector3 heldPoint;

    public bool holdByCentre;
    private float oldDrag;
    public float newDrag;
    private float distance;
    public float differenceInRotation;
    public bool objectHeld = false;

    public KeyCode rotateKey;
    private LineRenderer lineRenderer;
    public Transform muzzle;
    public LayerMask layerMask;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

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
        //int layerMask = 1 << 2;
        //layerMask = ~layerMask;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            currentFocused = hit.collider.gameObject;
            
            heldPoint = currentFocused.transform.InverseTransformPoint(hit.point);
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
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentFocused != null && (currentFocused.GetComponent<IGrabbable>() != null))
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
        
        Vector3 deltaPosition = target.transform.position - heldObject.transform.position;
        heldObjectRB.drag =  0.5f +   1/distance + newDrag * 1/Mathf.Pow(distance,2);


        if (holdByCentre)
        {
            heldObjectRB.AddForce(deltaPosition.normalized * holdCurve.Evaluate(distance) * heldObjectRB.mass * holdForceMultiplier);
        }
        else
        {
            heldObjectRB.AddForceAtPosition((deltaPosition.normalized * holdCurve.Evaluate(distance) * heldObjectRB.mass * holdForceMultiplier),heldObject.transform.TransformPoint(heldPoint));
 
        }

        // drop object;
        if (Input.GetKeyDown(KeyCode.Mouse0)|| !heldObject.GetComponent<IGrabbable>().IsHeld())
        {
            Drop(heldObject.GetComponent<IGrabbable>());
            transform.parent.parent.GetComponent<CameraControlFirstPerson>().rotationEnabled = true;
        }
        
   



        // Move object in and out;
        if ( Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) >= 0.01f)
        {
            Vector3 targetNewPos = target.transform.position + (target.transform.position-this.transform.position).normalized  * Input.GetAxis("Mouse ScrollWheel") * 5;
            if (Vector3.Distance(targetNewPos, this.transform.position) >= 2 && Vector3.Distance(targetNewPos, this.transform.position) <= 14)
            {
                target.transform.position += (target.transform.position-this.transform.position).normalized * Input.GetAxis("Mouse ScrollWheel") * 5;
            }
        }
        
        
        
        // Rotate
        if (Input.GetKeyDown(rotateKey))
        {
            target.transform.rotation = heldObject.transform.rotation;
            transform.parent.parent.GetComponent<CameraControlFirstPerson>().rotationEnabled = false;
        }
        else if (Input.GetKeyUp(rotateKey))
        {
            transform.parent.parent.GetComponent<CameraControlFirstPerson>().rotationEnabled = true;
        }
        else if (Input.GetKey(rotateKey))
        {
            
           // target.transform.Rotate(transform.TransformDirection(new Vector3(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0)),Space.World);
            target.transform.Rotate(transform.up, Input.GetAxis("Mouse X"),Space.World);
            target.transform.Rotate(transform.right, Input.GetAxis("Mouse Y"),Space.World);

            differenceInRotation = Quaternion.Angle(target.transform.rotation,heldObject.transform.rotation);
           

            
            //This drops a component of the Quaternion, it's stable but it has edge cases where the rotation is weird.
            Quaternion rot = Quaternion.FromToRotation(heldObject.transform.TransformDirection(Vector3.one), target.transform.TransformDirection(Vector3.one));
            Vector3 deltaRotation = new Vector3(rot.x,rot.y,rot.z);
            
         //  Vector3 deltaRotation = (target.transform.TransformDirection(Vector3.one) - heldObject.transform.TransformDirection(Vector3.one));
           // Vector3 deltaRotation = Vector3.RotateTowards(heldObject.transform.eulerAngles,target.transform.eulerAngles,0.1f,0.1f);
           
           
           heldObjectRB.MoveRotation(target.transform.rotation);
           
           //heldObjectRB.angularVelocity = deltaRotation * (differenceInRotation) * 100;
        }

        
        
        //Drawing a beam


        Vector3 lineStart = transform.InverseTransformPoint(muzzle.position);
        Vector3 lineEnd;
        if (!holdByCentre)
        {
            lineEnd = transform.InverseTransformPoint(heldObject.transform.TransformPoint(heldPoint));
        }
        else
        {
             lineEnd = transform.InverseTransformPoint(heldObject.transform.position);
        }
        Vector3 targetEnd = transform.InverseTransformPoint(target.transform.position);
        //lineRenderer.positionCount = 25;
        float perC = 1.0f / lineRenderer.positionCount;
        lineRenderer.SetPosition(0,lineStart);
        
        for (int i = 1; i < lineRenderer.positionCount -1; i++)
        {
            float fractionofDistance = (Vector3.Distance(lineStart,lineEnd)/lineRenderer.positionCount)*i;
            
            Vector3 pointA = Vector3.MoveTowards(lineStart,lineEnd,fractionofDistance);
            Vector3 pointB = Vector3.MoveTowards(lineStart,targetEnd,fractionofDistance);
            Vector3 pointC = Vector3.Lerp(pointB, pointA, i * perC );

            lineRenderer.SetPosition(i, pointC);
        }
        lineRenderer.SetPosition(lineRenderer.positionCount-1,lineEnd);
        //lineRenderer.Simplify(0.01f);

    }

    public void Grab(IGrabbable grabbable)
    {
        grabbable.Grabbed();
        if ( grabbable.HoldRigidBody() != null)
        {
            heldObject = grabbable.HoldObject();
            currentFocused = heldObject;
            heldObjectRB = grabbable.HoldRigidBody(); 
            oldDrag = heldObjectRB.drag;
            target.transform.rotation = heldObject.transform.rotation;

            if (holdByCentre)
            {
                target.transform.position = heldObject.transform.position;
            }
            else
            {
                target.transform.position = heldObject.transform.TransformPoint(heldPoint);
            }

            heldObjectRB.useGravity = false;
            objectHeld = true;
            lineRenderer.enabled = true;
        }
    }

    public void Drop(IGrabbable grabbable)
    {
        grabbable.Dropped();
        heldObjectRB.drag = oldDrag;
        heldObjectRB.useGravity = true;
        heldObject = null;
        objectHeld = false;
        lineRenderer.enabled = false;
    }
    
}

