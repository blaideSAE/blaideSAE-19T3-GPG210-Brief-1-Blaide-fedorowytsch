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
    
    public GlueBall otherGlueBall;
    private Rigidbody rB;
    
    public FixedJoint mainJoint;
    public Vector3 attatchmentPoint;

    

    public float strength;
    public bool isHeld;
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (otherGlueBall != null && otherGlueBall.parent != null && parent != null && mainJoint == null)
        {
            
            child = otherGlueBall.parent;
            cRB = child.GetComponent<Rigidbody>();


            Vector3 deltaPosition = (otherGlueBall.transform.position - transform.position);
            float distance = Vector3.Distance(transform.position, otherGlueBall.transform.position);
            
            pRB.AddForceAtPosition( deltaPosition * (1f/distance) * 20f,transform.position);
            
            
            
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), child.GetComponent<Collider>(), true);
           
            
            if ( distance < 0.02f)
            {
                Join();
            }
        }
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
            else if (otherGlueBall == null && other.gameObject.GetComponent<GlueBall>()!= null)
            {
                otherGlueBall = other.gameObject.GetComponent<GlueBall>();
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
        else if (other.GetComponent<GlueBall>() == otherGlueBall)
        {
            otherGlueBall = null;
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

