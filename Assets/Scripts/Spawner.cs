using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    public int obstructions = 0;
    public List<GameObject> spawnedObjects; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPrefab()
    {
        if (obstructions == 0)
        {
            GameObject newObject = Instantiate(prefab, transform.position, transform.rotation);
            Debug.Log("spawned" + newObject.name);
            spawnedObjects.Add(newObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        obstructions++;
    }

    private void OnTriggerExit(Collider other)
    {
        obstructions--;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
