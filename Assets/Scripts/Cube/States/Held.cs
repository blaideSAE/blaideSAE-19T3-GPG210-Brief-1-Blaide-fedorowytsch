using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Held : StateBase
{
    void Start()
    {

    }
    public override void Enter()
    {
        
    }

    public override void Execute()
    {

    }

    public void Dropped()
    {
        sM.ChangeState(nextState);
    }

    public void Thrown()
    {
        sM.ChangeState(nextState);
    }

    public override void Exit()
    {
    
    }

 }