using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour , IGrabbable, IDestroyedByLava, ISpawnable, IGlueable
{
    private bool isHeld;
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
    public Rigidbody HoldRigidBody()
    {
        return GetComponent<Rigidbody>();
    }
    public GameObject HoldObject()
    {
        return gameObject;
    }
    public Mesh SpawnerMesh()
    {
        return GetComponent<MeshFilter>().sharedMesh;
    }
    public Material SpawnerMaterial()
    {
        return GetComponent<Renderer>().sharedMaterial;
    }
}
