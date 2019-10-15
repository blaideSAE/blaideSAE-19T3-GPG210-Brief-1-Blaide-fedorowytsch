using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    public UnityEvent triggerableEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggered()
    {
        triggerableEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
