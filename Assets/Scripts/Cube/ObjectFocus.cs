using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFocus : MonoBehaviour
{
    private float timeOut;
    public float timeOutLimit;
    private Outline outlineScript;
    // Start is called before the first frame update
    void Start()
    {
        outlineScript = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        timeOut += Time.deltaTime;
        if (timeOut >= timeOutLimit)
        {
            LostFocus();
        }

    }

    public void GotFocus()
    {
        outlineScript.enabled = true;
        timeOut = 0;
    }

    public void LostFocus()
    {
        outlineScript.enabled = false;
    }
}
