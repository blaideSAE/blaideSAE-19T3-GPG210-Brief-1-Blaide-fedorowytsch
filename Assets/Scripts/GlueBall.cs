using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueBall : MonoBehaviour,IGrabbable
{
    public GameObject parent;
    private Rigidbody pRB;
    
    public GameObject child;
    private Rigidbody cRB;

    private Rigidbody rB;
    
    public Joint mainJoint;
    
    public Joint selfJoint;
    
    public float strength;
    public bool isHeld;
    
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        //selfJoint = gameObject.AddComponent<FixedJoint>();
       // selfJoint.enableCollision = false;
       // selfJoint.breakForce = strength;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other, true);
            if (parent == null)
            { 
                parent = other.gameObject;
                pRB = parent.GetComponent<Rigidbody>();
                
                
            }
            else if (child == null && other.gameObject != parent)
            {
                child = other.gameObject;
                cRB = child.GetComponent<Rigidbody>();
                Join();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), other, false);
        if (other.gameObject == parent)
        {
            parent = null;
            Destroy(mainJoint);
        }
        else if (other.gameObject == child)
        {
            parent = child;
            child = null;
            Destroy(mainJoint);
        }
        
    }

    public void Join()
    {
        mainJoint = parent.AddComponent<FixedJoint>();
        mainJoint.connectedBody = cRB;
        mainJoint.enableCollision = false;
        mainJoint.breakForce = strength;
    }

    public void AttatchGlue()
    {
        selfJoint = gameObject.AddComponent<FixedJoint>();
        selfJoint.connectedBody = pRB;
        selfJoint.enableCollision = false;
        rB.useGravity = false;
    }

    public void DetatchGlue()
    {
        Destroy(selfJoint);
        rB.useGravity = true;
    }

    public void Grabbed()
    { 
        DetatchGlue();
        isHeld = true;
    }

    public void Dropped()
    {
        if (parent != null)
        { 
            AttatchGlue();
        }
        isHeld = false;
    }

    public bool IsHeld()
    {
        return isHeld;

    }
}
