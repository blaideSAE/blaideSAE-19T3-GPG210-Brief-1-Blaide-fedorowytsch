using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    public Spawner parentSpawner;

    private void OnDestroy()
    {
        parentSpawner.spawnedObjects.Remove(gameObject);
    }
}
