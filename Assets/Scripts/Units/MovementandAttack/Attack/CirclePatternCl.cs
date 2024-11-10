using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CirclePatternCl : MovementPattern
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
                if (boxController == null || boxController.IsEmpty || HasUnitInCube(cube))
                {
                    validCubes.Add(cube);
                }
            }
        }
        return validCubes;
    }   

    // Método que verifica si hay un aliado en el ObjectDetector dentro del cubo
    private bool HasUnitInCube(GameObject cube)
    {
        ObjectDetector objectDetector = cube.GetComponentInChildren<ObjectDetector>();
        return objectDetector != null && objectDetector.IsUnit;
    }
}
