using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : StateBase
{
    private Rigidbody rB;

    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    public override void Enter()
    {
        rB.useGravity = false;
        rB.isKinematic = false;
    }

    public override void Execute()
    {

        //sM.ChangeState(nextState);
    }

    public override void Exit()
    {
        rB.useGravity = true;
        rB.isKinematic = true;

    }

}
