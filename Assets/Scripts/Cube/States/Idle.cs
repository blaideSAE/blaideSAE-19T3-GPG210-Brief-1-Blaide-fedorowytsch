using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateBase
{
    private Rigidbody rB;
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }
    public override void Enter()
    {
        rB.useGravity = true;

    }

    public override void Execute()
    {

    }
    public override void Exit()
    {
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            sM.ChangeState(nextState);
        }
        //throw new NotImplementedException();
    }
}
