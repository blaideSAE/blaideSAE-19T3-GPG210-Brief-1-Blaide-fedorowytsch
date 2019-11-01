using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabber<T>
{

    void Grab(T grabbable);
    void Drop(T grabbable);
}
