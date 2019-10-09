using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHold : StateBase
{
    public Transform target;
    //public Vector3 velocity;
    public float holdingForce;
    public float rotationalFriction;
    
    public GameObject heldObject;
    private Rigidbody heldObjectRB;
    private StateManager heldStateManager;
    private float oldDrag;
    public float newDrag;
    private float distance;
    public override void Enter()
    {
        
        heldObjectRB = heldObject.GetComponent<Rigidbody>();
        heldStateManager = heldObject.GetComponent<StateManager>();
        heldObjectRB.useGravity = false;
        oldDrag = heldObjectRB.drag;

    }

    public override void Execute()
    {
        distance = Vector3.Distance(target.position, heldObject.transform.position);
        
        HoldPosition();
        heldObjectRB.drag =  0.3f + newDrag * 1/Mathf.Pow(distance,2);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Use();
        }
    }

    public void HoldPosition()
    {
        //heldObjectRB.AddForce(;
        heldObjectRB.AddForce((target.position - heldObject.transform.position) * holdingForce * distance *Time.deltaTime );
    }

    private void Use()
    {
        heldObjectRB.drag = oldDrag;
        heldObjectRB.useGravity = true;
        heldStateManager.ChangeState(heldStateManager.currentState.nextState);
        sM.ChangeState(nextState);
    }

    public override void Exit()
    {
    
    }
}
