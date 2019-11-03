using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    Mesh SpawnerMesh();
    Material SpawnerMaterial();
}
