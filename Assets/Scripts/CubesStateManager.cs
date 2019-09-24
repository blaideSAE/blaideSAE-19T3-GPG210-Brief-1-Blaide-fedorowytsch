using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesStateManager : StateManager
{
   public StateBase currentState;
   public StateBase jumping;
   public StateBase floating;
   public StateBase idle;
   public StateBase charge;
   // Start is called before the first frame update
    void Start()
    {
        currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();
    }

    public override void ChangeState(StateBase state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }


}
