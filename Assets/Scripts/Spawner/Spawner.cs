﻿using System;
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
        ISpawnable spawnable = prefab.GetComponent<ISpawnable>();
        spawnCollider.sharedMesh = spawnable.SpawnerMesh();
        spawnMeshFilter.mesh = spawnable.SpawnerMesh();
        spawnMaterial.color = spawnable.SpawnerMaterial().color;
        spawnMaterial.color = new Color(spawnable.SpawnerMaterial().color.r,spawnable.SpawnerMaterial().color.g,spawnable.SpawnerMaterial().color.b,spawnAlpha);
        transform.localScale = prefab.transform.localScale;
    }

    
    public void SpawnPrefab()
    {
        if (obstructions == 0)
        {
            lastSpawned = Instantiate(prefab, transform.position, transform.rotation);
            Debug.Log("spawned" + lastSpawned.name);
            spawnedObjects.Add(lastSpawned);
            lastSpawned.AddComponent<SpawnedObject>().parentSpawner = this;
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
