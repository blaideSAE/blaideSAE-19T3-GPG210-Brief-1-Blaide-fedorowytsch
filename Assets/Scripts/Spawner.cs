using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
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
        GameObject newObject = Instantiate(prefab, transform.position, transform.rotation);
        Debug.Log("spawned" + newObject.name);
    }
}
