using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateHover : MonoBehaviour
{
    public AnimationCurve hoverMotionCurve;

    public float distance;
    public float speedMultiplier;
    
    private float startingheight;
    public float maxHeight;
    public float minHeight;
    public bool goingUp = true;

    public bool rotate = false; 
    public Vector3 rotateDirection;

    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startingheight = this.transform.localPosition.y;
    }
    //[ExecuteInEditMode]
    void OnValidate()
    {
        rotateDirection = rotateDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        maxHeight = startingheight + distance / 2;
        minHeight = startingheight - distance / 2;

        if (goingUp)
        {
            transform.localPosition += new Vector3(0, hoverMotionCurve.Evaluate((maxHeight - transform.localPosition.y)/ distance)*speedMultiplier, 0);

            if (transform.localPosition.y >= maxHeight - distance/20)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.localPosition -= new Vector3(0, hoverMotionCurve.Evaluate((transform.localPosition.y - minHeight)/ distance)* speedMultiplier, 0);

            if (transform.localPosition.y <= minHeight + distance/20)
            {
                goingUp = true;
            }
        }

        if (rotate)
        {
            this.transform.Rotate(rotateDirection * rotateSpeed);
        }


    }
}
