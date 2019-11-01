using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHold : StateBase
{
    public GameObject target;
    //public Vector3 velocity;

    
    public AnimationCurve holdCurve;
    public GameObject heldObject;
    private Rigidbody heldObjectRB;
    private StateManager heldStateManager;
    private float oldDrag;
    public float newDrag;
    private float distance;
    //private Quaternion heldObjectTargetRotation;
    public override void Enter()
    {
        
        heldObjectRB = heldObject.GetComponent<Rigidbody>();
        heldStateManager = heldObject.GetComponent<StateManager>();
        heldObjectRB.useGravity = false;
        oldDrag = heldObjectRB.drag;
        target.transform.rotation = heldObject.transform.rotation;
        target.transform.position = heldObject.transform.position;


    }

    public override void Execute()
    {
        distance = Vector3.Distance(target.transform.position, heldObject.transform.position);
        
        HoldPosition();
        heldObjectRB.drag =  0.5f +   1/distance + newDrag * 1/Mathf.Pow(distance,2);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
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

    public void HoldPosition()
    {
        heldObjectRB.AddForce(Vector3.Normalize(target.transform.position - heldObject.transform.position) * holdCurve.Evaluate(distance) * heldObjectRB.mass) ;
    }

    private void Fire()
    {
        heldObjectRB.drag = oldDrag;
        heldObjectRB.useGravity = true;
       // heldStateManager.ChangeState(heldStateManager.currentState.nextState);
        sM.ChangeState(nextState);
    }

    public override void Exit()
    {
    }
}
