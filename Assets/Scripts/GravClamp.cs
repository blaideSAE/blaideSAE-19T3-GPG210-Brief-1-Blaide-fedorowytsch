using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravClamp : MonoBehaviour , IGrabber<IGrabbable>
{
    public int obstructions = 0;
    public List<GameObject> objectsInField;
    public List<GameObject> grabbedObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject g in objectsInField)
        {
            if (!g.GetComponent<IGrabbable>().IsHeld())
            {
                Grab(g.GetComponent<IGrabbable>());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IGrabbable>() != null)
        {
            objectsInField.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IGrabbable>() != null && other.gameObject.GetComponent<IGrabbable>().IsHeld() != true)
        {
            Drop(other.gameObject.GetComponent<IGrabbable>());
        }

        objectsInField.Remove(other.gameObject);
    }

    public void Grab(IGrabbable grabbable)
    {
        MonoBehaviour mb = grabbable as MonoBehaviour;
        if (mb != null)
        {
            GameObject g = mb.gameObject;
            // [...]
            grabbedObjects.Add(g);
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        
    }

    public void Drop(IGrabbable grabbable)
    {
        MonoBehaviour mb = grabbable as MonoBehaviour;
        if (mb != null)
        {
            GameObject g = mb.gameObject;
            // [...]
            grabbedObjects.Remove(g);
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.useGravity = true;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
