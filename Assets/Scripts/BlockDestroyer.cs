using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IDestroyedByLava>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
