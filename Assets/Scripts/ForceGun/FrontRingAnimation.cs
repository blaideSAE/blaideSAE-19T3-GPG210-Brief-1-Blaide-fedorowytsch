using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontRingAnimation : MonoBehaviour
{
    public float baseRotationSpeed;
    public float speedChangeSpeed;
    public float speedChangeAmount;
    public bool rotationEnabled;

    public bool pulsateEnabled;
    public float pulseSpeed;
    public float pulseScale;
    public float currentScale;
    public float minimumScale;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationEnabled)
        {
            Rotate();
        }

        if (pulsateEnabled)
        {
            Pulsate();
        }

    }
    void Rotate()
    {
        transform.Rotate(0, 0, Mathf.PerlinNoise(1,Time.time *speedChangeSpeed ) *speedChangeAmount+ baseRotationSpeed);

    }

    void Pulsate()
    {
        currentScale = Mathf.PerlinNoise(0.1f, Time.time * pulseSpeed)*pulseScale +minimumScale;
        transform.localScale = new Vector3(currentScale,currentScale,currentScale);
    }
}
