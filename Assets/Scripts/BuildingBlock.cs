﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour , IGrabbable
{
    private bool isHeld;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Grabbed()
    {
        isHeld = true;
    }

    public void Dropped()
    {
        isHeld = false;
    }

    public bool IsHeld()
    {
        return isHeld;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
