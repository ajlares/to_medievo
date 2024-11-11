using System.Collections;
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

    private Vector3 targetPosition;
    private float moveSpeed = 2f; // Velocidad de movimiento
    private bool isMoving = false;
    private Quaternion originalRotation;

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
                    targetPosition = new Vector3(
                        clickedObject.transform.position.x,
                        clickedObject.transform.position.y + .5f,
                        clickedObject.transform.position.z
                    );

                    isMoving = true;

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

        if (isMoving)
    {
        unit.Anim.SetInteger("C", 1);

        Vector3 direction = targetPosition - unit.transform.position;

        unit.transform.position = Vector3.MoveTowards(unit.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotar la unidad en la dirección del movimiento
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Comprobar si la unidad ha llegado a la posición objetivo
        if (Vector3.Distance(unit.transform.position, targetPosition) < 0.1f)
        {
            unit.transform.position = targetPosition;

            unit.Anim.SetInteger("C", 0);
            
            unit.canMove = false;
            isMoving = false;

            unit.transform.rotation = originalRotation;

            UnitBaseState newState = new UnitSelectedState();
            unit.ChangeState(newState);
        }
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