using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public StateBase currentState;
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
    public void ChangeState( StateBase state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
