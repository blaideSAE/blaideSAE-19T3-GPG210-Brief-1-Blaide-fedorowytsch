using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateBase
{
    private Rigidbody rB;
    private float LastHeight;
    public float force;
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }
    
    public override void Enter()
    {
        LastHeight = 0;
        rB.AddForce(Vector3.up * force);
    }

    public override void Execute()
    {

        if (LastHeight > transform.position.y)
        {
            sM.ChangeState(nextState);
        }
        else
        {
            LastHeight = transform.position.y;
        }

    }

    public override void Exit()
    {
        
    }

}
