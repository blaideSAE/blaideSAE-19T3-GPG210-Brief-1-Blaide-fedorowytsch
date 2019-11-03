using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spawner : MonoBehaviour , IGrabbable
{
    public GameObject prefab;

    private MeshCollider spawnCollider;
    private MeshFilter spawnMeshFilter;
    private MeshRenderer spawnMeshRenderer;
    private Material spawnMaterial;
    public float spawnAlpha;
    public ForceGunMain lastGrabber;
    public int obstructions = 0;
    public List<GameObject> spawnedObjects;
    public GameObject lastSpawned;
    // Start is called before the first frame update
    void Start()
    {
        spawnCollider = GetComponent<MeshCollider>();
        spawnMeshFilter = GetComponent<MeshFilter>();
        spawnMeshRenderer = GetComponent<MeshRenderer>();
        spawnMaterial = GetComponent<Renderer>().material;
        
        UpdateMesh();
        
    }

    public void Grabbed()
    {
        SpawnPrefab();
        lastSpawned.GetComponent<IGrabbable>().Grabbed();
        

    }

    public void Dropped()
    {
        
    }

    public bool IsHeld()
    {
        return false;
    }

    void UpdateMesh()
    {
        spawnCollider.sharedMesh = prefab.GetComponent<MeshFilter>().sharedMesh;
        spawnMeshFilter.mesh = prefab.GetComponent<MeshFilter>().sharedMesh;
        spawnMaterial.color = prefab.GetComponent<Renderer>().sharedMaterial.color;
        spawnMaterial.color = new Color(spawnMaterial.color.r,spawnMaterial.color.g,spawnMaterial.color.b,spawnAlpha);
        transform.localScale = prefab.transform.localScale; //- new Vector3(0.04f, 0.04f, 0.1f);
    }

    
    public void SpawnPrefab()
    {
        if (obstructions == 0)
        {
            lastSpawned = Instantiate(prefab, transform.position, transform.rotation);
            Debug.Log("spawned" + lastSpawned.name);
            spawnedObjects.Add(lastSpawned);
            
        }
    }

    public Rigidbody HoldRigidBody()
    {
        return lastSpawned.GetComponent<Rigidbody>();
    }

    public GameObject HoldObject()
    {
        return lastSpawned;
    }

    private void OnTriggerEnter(Collider other)
    {
        obstructions++;
    }
    private void OnTriggerExit(Collider other)
    {
        obstructions--;
    }
}
