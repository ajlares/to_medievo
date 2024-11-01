using System.Collections.Generic;
using UnityEngine;

public class CrossPatternM : MovementPattern
{
    public override List<GameObject> GetValidCubes(GameObject unit, int range)
    {
        List<GameObject> validCubes = new List<GameObject>();
        Vector3 unitPosition = unit.transform.position;

        GameObject[] mapCubes = GameObject.FindGameObjectsWithTag("MapCube");

        foreach (var cube in mapCubes)
        {
            Vector3 cubePosition = cube.transform.position;
            float distance = Vector3.Distance(unitPosition, cubePosition);

            
            if (distance <= range &&
                (Mathf.Approximately(cubePosition.x, unitPosition.x) ||
                 Mathf.Approximately(cubePosition.z, unitPosition.z)))
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