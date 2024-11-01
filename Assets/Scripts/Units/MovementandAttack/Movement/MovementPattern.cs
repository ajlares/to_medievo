using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementPattern
{
    public abstract List<GameObject> GetValidCubes(GameObject unit, int range);
}

