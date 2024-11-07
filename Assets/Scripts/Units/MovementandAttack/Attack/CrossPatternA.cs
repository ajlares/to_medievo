using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPatternA : MovementPattern
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

            // Verificar si está dentro del rango y alineado en X o Z
            if (distance <= range &&
                (Mathf.Approximately(cubePosition.x, unitPosition.x) ||
                Mathf.Approximately(cubePosition.z, unitPosition.z)))
            {
                BoxController boxController = cube.GetComponent<BoxController>();

                // Si el BoxController es nulo o está vacío, verificamos el ObjectDetector
                if (boxController != null && (boxController.IsEmpty || HasEnemyInCube(cube)))
                {
                    validCubes.Add(cube); // Agregar cubo válido a la lista
                }
            }
        }

        return validCubes;
    }   

    // Método que verifica si hay un enemigo en el ObjectDetector dentro del cubo
    private bool HasEnemyInCube(GameObject cube)
    {
        ObjectDetector objectDetector = cube.GetComponentInChildren<ObjectDetector>();
        return objectDetector != null && objectDetector.IsEnemy;
    }
}

