using System.Collections.Generic;
using UnityEngine;

public class UnitMovementState : UnitBaseState
{
    private List<GameObject> highlightedMapCubes = new List<GameObject>();
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
    private Material outlineMaterial;
    private bool isOutlineMaterialLoaded = false;
    private int movementRange;
    private MovementRangeCalculator rangeCalculator;

    public override void EnterState(UnitStateManager unit)
    {
        Debug.Log("Entrando en MOVEMENT");
        
        if (!isOutlineMaterialLoaded)
        {
            outlineMaterial = Resources.Load<Material>("OutlineMaterial");
            isOutlineMaterialLoaded = true;
        }

        CharactersBase character = unit.GetComponent<CharactersBase>();
        if (character != null)
        {
            movementRange = character.GetMovementRange();
            Debug.Log($"Rango de movimiento: {movementRange}");

            rangeCalculator = new MovementRangeCalculator(character.MovementPattern);
            highlightedMapCubes = rangeCalculator.CalculateRange(unit.gameObject, movementRange);
            HighlightMapCubes(true);
        }
    }

    public override void ExitState(UnitStateManager unit)
    {
        Debug.Log("Saliendo de MOVEMENT");
        HighlightMapCubes(false);
    }

    public override void UpdateState(UnitStateManager unit)
    {
        if (!unit.canMove) return;

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                BoxController boxController = clickedObject.GetComponent<BoxController>();

                if (highlightedMapCubes.Contains(clickedObject) && boxController != null && boxController.IsEmpty)
                {
                    Vector3 targetPosition = new Vector3(
                        clickedObject.transform.position.x,
                        clickedObject.transform.position.y + 1,
                        clickedObject.transform.position.z
                    );
                    unit.transform.position = targetPosition;
                    unit.canMove = false;
                    UnitBaseState newState = new UnitSelectedState();
                    unit.ChangeState(newState);
                }
                else
                {
                    Debug.Log("No se puede mover a este objeto.");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnitBaseState newState = new UnitSelectedState();
            unit.ChangeState(newState);
        }
    }

    private void HighlightMapCubes(bool highlight)
    {
        foreach (var mapCube in highlightedMapCubes)
        {
            Renderer renderer = mapCube.GetComponent<Renderer>();

            if (renderer != null)
            {
            
                if (highlight && !originalMaterials.ContainsKey(mapCube))
                {
                    originalMaterials[mapCube] = renderer.material;
                }

                renderer.material = highlight ? outlineMaterial : originalMaterials.GetValueOrDefault(mapCube, renderer.material);
            }
            else
            {
                Debug.LogWarning($"No se encontró Renderer para {mapCube.name}.");
            }
        }

        if (!highlight)
        {
            originalMaterials.Clear();
            highlightedMapCubes.Clear();
        }
    }
}


