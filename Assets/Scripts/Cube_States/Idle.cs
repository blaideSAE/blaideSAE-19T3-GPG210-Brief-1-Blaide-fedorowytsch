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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sM.ChangeState(nextState);
        }
    }
    public override void Exit()
    {
    
    }
}