using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager unitManager;
    public Material highlightMaterial;
    public Material ObstacleMaterial;
    private GameObject selectedUnit;
    private List<GameObject> highlightedTiles = new List<GameObject>();

    public float highlightRange;

    private void Awake()
    {
        if (unitManager == null) unitManager = this;
        else Destroy(gameObject);
    }

    public void SelectUnit(GameObject unit)
    {
        ClearHighlight(); // Limpiar las celdas iluminadas anteriores.
        selectedUnit = unit;
        print("Unidad seleccionada: " + unit.name);
        HighlightRange(selectedUnit.transform.position, highlightRange);
    }

    public void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            selectedUnit.GetComponent<SelectUnit>().ResetMaterial(); // Restaurar material.
            selectedUnit = null;  // Deseleccionar la unidad.
            print("Unidad deseleccionada.");
            ClearHighlight();  // Limpiar las celdas iluminadas.
        }
    }

    public GameObject GetSelectedUnit()
    {
        return selectedUnit;
    }

    public void HighlightRange(Vector3 unitPosition, float range)
    {
        Vector3 boxSize = new Vector3(range * 2, 1, range * highlightRange);
        Collider[] tilesInRange = Physics.OverlapBox(unitPosition, boxSize / 2, Quaternion.identity);

        foreach (var tile in tilesInRange)
    {
        Select selectScript = tile.GetComponent<Select>();

        bool hasObstacle = false;
        foreach (Transform child in tile.transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                hasObstacle = true;
                tile.tag = "Obstacle";
                break;
            }
        }

        if (!hasObstacle && tile.CompareTag("MapCube"))
        {
            tile.GetComponent<Renderer>().material = selectScript.GetHighlightMaterial();
            highlightedTiles.Add(tile.gameObject);
        }
    }
    }

    public void ClearHighlight()
    {
        foreach (var tile in highlightedTiles)
        {
            tile.GetComponent<Select>().ResetMaterial();
        }
        highlightedTiles.Clear();
    }

    private void OnDrawGizmos()
    {
        if (selectedUnit != null)
        {
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Vector3 boxSize = new Vector3(highlightRange * 2, 0.1f, highlightRange * 2);
            Gizmos.DrawCube(selectedUnit.transform.position, boxSize);
        }
    }
}
