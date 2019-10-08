using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is just a script for  the force gun to interact with to show which object is selected.
/// It may end up not being necessary.
/// </summary>
public class OutlineController : MonoBehaviour
{
    private float selectionTimer;
    public float selectionRefreshTime;
    public bool refreshTimerEnabled;
    
    public bool isSelected;
    public Outline outlineScript;
    
    // Start is called before the first frame update
    void Start()
    {
        outlineScript = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (refreshTimerEnabled)
        {
            selectionTimer += Time.deltaTime;

            if (selectionTimer >= selectionRefreshTime)
            {
                isSelected = !isSelected;
                outlineScript.enabled = isSelected;
                selectionTimer = 0;
            }
        }
    }
}