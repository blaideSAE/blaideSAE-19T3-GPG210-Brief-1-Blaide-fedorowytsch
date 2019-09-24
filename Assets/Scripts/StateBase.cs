using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    public StateManager sM;
    public StateBase nextState;
    
    public virtual void Enter()
    {
        
    }

    public virtual void Execute()
    {
        
    }

    public virtual void Exit()
    {
        
    }
    
}
