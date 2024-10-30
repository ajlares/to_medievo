using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePatternM : MovementPattern
{
    public override List<GameObject> GetValidCubes(GameObject unit, int range)
    {
        List<GameObject> validCubes = new List<GameObject>();
        Vector3 unitPosition = unit.transform.position;

        GameObject[] mapCubes = GameObject.FindGameObjectsWithTag("MapCube");

        foreach (var cube in mapCubes)
        {
            float distance = Vector3.Distance(unitPosition, cube.transform.position);
            if (distance <= range)
            {
                BoxController boxController = cube.GetComponent<BoxController>();
                if (boxController == null || boxController.IsEmpty)
                {
                    validCubes.Add(cube);
                }
            }
        }
        return validCubes;
    }
}