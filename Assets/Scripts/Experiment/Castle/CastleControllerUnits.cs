using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleControllerUnits : MonoBehaviour
{
    [SerializeField] private int spawnRange;
    [SerializeField] private int numberOfUnitsToSpawn;
    [SerializeField] private Material highlightMaterial;

    private List<GameObject> highlightedCubes = new List<GameObject>();
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
    private CirclePatternM circlePattern;

    private void Start()
    {
        circlePattern = new CirclePatternM();
    }

    private void OnMouseEnter()
    {
        HighlightCubesInRange();
    }

    private void OnMouseExit()
    {
        ResetHighlightedCubes();
    }

    private void OnMouseDown()
    {
        SpwanButton();
    }

    public void SpwanButton()
    {
        HighlightCubesInRange();
        SpawnUnitsInRange();
    }

    private void HighlightCubesInRange()
    {
        // Obtener las celdas válidas dentro del rango usando el patrón CirclePatternM
        List<GameObject> validCubes = circlePattern.GetValidCubes(gameObject, spawnRange);

        foreach (var cube in validCubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                if (!originalMaterials.ContainsKey(cube))
                {
                    originalMaterials[cube] = cubeRenderer.material;
                }

                cubeRenderer.material = highlightMaterial;

                highlightedCubes.Add(cube);
            }
        }
    }

    private void ResetHighlightedCubes()
    {
        foreach (var cube in highlightedCubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();
            if (cubeRenderer != null && originalMaterials.ContainsKey(cube))
            {
                cubeRenderer.material = originalMaterials[cube];
            }
        }

        highlightedCubes.Clear();
        originalMaterials.Clear();
    }

    private void SpawnUnitsInRange()
    {
        List<GameObject> validCubes = circlePattern.GetValidCubes(gameObject, spawnRange);

        if (validCubes.Count == 0)
        {
            Debug.LogWarning("No hay celdas válidas para generar unidades.");
            return;
        }

        for (int i = 0; i < numberOfUnitsToSpawn && validCubes.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, validCubes.Count);
            GameObject selectedCube = validCubes[randomIndex];

            // Instanciar una unidad aleatoria usando AllyCastle
            AllyCastle.instance.instanceUnit(selectedCube);

            validCubes.RemoveAt(randomIndex);
        }

        ResetHighlightedCubes();
    }
}
