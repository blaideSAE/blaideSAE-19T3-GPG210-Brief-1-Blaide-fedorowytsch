using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueBall : MonoBehaviour,IGrabbable, IDestroyedByLava, ISpawnable
{
    public GameObject parent;
    private Rigidbody pRB;
    
    public GameObject child;
    private Rigidbody cRB;

    private Rigidbody rB;
    
    public Joint mainJoint;
    public Vector3 attatchmentPoint;

    public GlueBall otherGlueBall;

    public float strength;
    public bool isHeld;
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        //selfJoint = gameObject.AddComponent<FixedJoint>();
       // selfJoint.enableCollision = false;
       // selfJoint.breakForce = strength;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
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
       // Physics.IgnoreCollision(this.GetComponent<Collider>(), other, false);
        if (other.gameObject == parent)
        {
            parent = null;
            
        }
        else if (other.gameObject == child)
        {
            //parent = child;
            child = null;
            
        }
        
    }

    public void Join()
    {
        mainJoint = parent.AddComponent<FixedJoint>();
        mainJoint.connectedBody = cRB;
        mainJoint.enableCollision = false;
        mainJoint.breakForce = strength;
    }

    //public void OnMainJointBreak()
    //{
    //}

    public void AttatchGlue()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), parent.GetComponent<Collider>(), true);
        transform.position = parent.GetComponent<Collider>().ClosestPoint(transform.position);
        this.transform.SetParent(parent.transform);
        rB.useGravity = false;
        rB.isKinematic = true;
    }

    public void DetatchGlue()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), parent.GetComponent<Collider>(), false); 
        this.transform.SetParent(null);
        rB.isKinematic = false;
        if (mainJoint != null)
        {
            Destroy(mainJoint);
        }
    }

    public void Grabbed()
    { 
        if (parent != null)
        { 
            DetatchGlue();
            
        }
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
    public Rigidbody HoldRigidBody()
    {
        return GetComponent<Rigidbody>();
    }
    public GameObject HoldObject()
    {
        return gameObject;
    }
    public Mesh SpawnerMesh()
    {
        return GetComponent<MeshFilter>().sharedMesh;
    }
    public Material SpawnerMaterial()
    {
        return GetComponent<Renderer>().sharedMaterial;
    }
}

