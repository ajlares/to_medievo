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

    public bool canSpawnUnit = true;

    public static CastleControllerUnits instance;
    public AudioClip OpenDoor;
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy( this);
        }
    }

    private void Start()
    {
        circlePattern = new CirclePatternM();
    }

    private void OnMouseEnter()
    {
        HighlightCubesInRange();
        SfxController.instance.PlaySound(OpenDoor);
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
        if (!canSpawnUnit)
        {
            Debug.LogWarning("Ya se ha generado una unidad este turno.");
            return;
        }

        if (EnemyCastle.instance.UnitsToUse <= 0)
        {
            Debug.LogWarning("No se puede generar mas unidades");
            return;
        }

        List<GameObject> validCubes = circlePattern.GetValidCubes(gameObject, spawnRange);

        if (validCubes.Count == 0)
        {
            Debug.LogWarning("No hay celdas válidas para generar unidades.");
            return;
        }

        int randomIndex = Random.Range(0, validCubes.Count);
        GameObject selectedCube = validCubes[randomIndex];

        AllyCastle.instance.instanceUnit(selectedCube);
        AllyCastle.instance.UnitsToUse = -1;

        canSpawnUnit = false;

        ResetHighlightedCubes();
    }

    public void EnableUnitSpawn()
    {
        canSpawnUnit = true;
    }
}
