using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UnitAttackState : UnitBaseState
{
    private List<GameObject> highlightedMapCubes = new List<GameObject>();
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
    private Material outlineMaterial;
    private bool isOutlineMaterialLoaded = false;
    private int attackRange;
    private int damage;
    private MovementRangeCalculator rangeCalculator;
    
    private bool isRotating = false;
    private Quaternion originalRotation;

    public override void EnterState(UnitStateManager unit)
    {
        Debug.Log("Entrando en ATTACK");
        if (!isOutlineMaterialLoaded)
        {
            outlineMaterial = Resources.Load<Material>("OutlineMaterial2");
            isOutlineMaterialLoaded = true;
        }

        CharactersBase character = unit.GetComponent<CharactersBase>();
        if (character != null)
        {
            attackRange = character.GetAttackRange();
            Debug.Log($"Rango de movimiento: {attackRange}");

            rangeCalculator = new MovementRangeCalculator(character.AttackPattern);
            highlightedMapCubes = rangeCalculator.CalculateRange(unit.gameObject, attackRange);
            HighlightMapCubes(true);
        }
    }

    public override void ExitState(UnitStateManager unit)
    {
        Debug.Log("Saliendo de ATTACK");
        HighlightMapCubes(false);
    }

    public override void UpdateState(UnitStateManager unit)
    {
        if (!unit.canAttack) return;
        
        CharactersBase character = unit.GetComponent<CharactersBase>();

        if(character is Builder && Input.GetButtonDown("Fire2"))
        {
            unit.Anim.SetInteger("C", 2);

            unit.StartCoroutine(PerformBuildAction(unit));
            return;
        }
        else if (character is Cleric && Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                BoxController boxController = clickedObject.GetComponent<BoxController>();

                if (highlightedMapCubes.Contains(clickedObject) && boxController != null)
                {
                    ObjectDetector objectDetector = clickedObject.GetComponentInChildren<ObjectDetector>();

                    if (objectDetector != null && objectDetector.IsUnit)
                    {
                        GameObject united = objectDetector.DetectedUnit;
                        CharactersBase targetCharacter = united.GetComponentInParent<CharactersBase>();

                        if (targetCharacter != null && targetCharacter != character)
                        {
                            int healAmount = character.attackPower;
                            Vector3 direction = targetCharacter.transform.position - unit.transform.position;
                            direction.y = 0; 
                            Quaternion targetRotation = Quaternion.LookRotation(direction);

                            originalRotation = unit.transform.rotation;
                            unit.StartCoroutine(RotateToTarget(unit, targetRotation, () =>
                            {
                                // Realizar la curación
                                targetCharacter.Heling(healAmount);
                                Debug.Log($"Unidad aliada curada. Curación aplicada: {healAmount}");

                                unit.Anim.SetInteger("C", 2);

                                unit.StartCoroutine(WaitAndChangeStateAfterHealing(unit));
                            }));
                        }
                        else
                        {
                            Debug.Log("No hay una unidad aliada en este espacio para curar.");
                        }
                    }
                    else
                    {
                        Debug.Log("No hay una unidad aliada en este espacio para curar.");
                    }
                }
                else
                {
                    Debug.Log("Fuera de rango para curar.");
                }
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                BoxController boxController = clickedObject.GetComponent<BoxController>();

                if (highlightedMapCubes.Contains(clickedObject) && boxController != null)
                {
                    ObjectDetector objectDetector = clickedObject.GetComponentInChildren<ObjectDetector>();

                    if (objectDetector != null && objectDetector.IsEnemy)
                    {
                        GameObject enemy = objectDetector.DetectedEnemy;
                        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

                        if (enemyStats != null)
                        {
                            originalRotation = unit.transform.rotation;

                            Vector3 direction = enemy.transform.position - unit.transform.position;
                            direction.y = 0; 
                            Quaternion targetRotation = Quaternion.LookRotation(direction);

                            isRotating = true;
                            unit.StartCoroutine(RotateToTarget(unit, targetRotation, () =>
                            {
                                unit.Anim.SetInteger("C", 2);
                                unit.StartCoroutine(WaitForAttackAnimation(unit, enemyStats, character.attackPower, .7f));
                            }));
                        }
                        else
                        {
                            Debug.LogWarning("No se encontró el componente EnemyStats en el enemigo.");
                        }
                    }
                    else
                    {
                        Debug.Log("No hay enemigo en esta casilla.");
                    }
                }
                else
                {
                    Debug.Log("Fuera de rango.");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnitBaseState newState = new UnitSelectedState();
            unit.ChangeState(newState);
        }
    }

    private System.Collections.IEnumerator RotateToTarget(UnitStateManager unit, Quaternion targetRotation, System.Action onComplete)
    {
        while (Quaternion.Angle(unit.transform.rotation, targetRotation) > 0.5f)
        {
            unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, targetRotation, Time.deltaTime * 5f);
            yield return null;
        }

        unit.transform.rotation = targetRotation; 
        onComplete?.Invoke(); 
    }

    private System.Collections.IEnumerator RotateBackToOriginal(UnitStateManager unit)
    {
        while (Quaternion.Angle(unit.transform.rotation, originalRotation) > 0.5f) 
        {
            unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, originalRotation, Time.deltaTime * 5f);
            yield return null;
        }

        unit.transform.rotation = originalRotation;
    }
    private System.Collections.IEnumerator WaitForAttackAnimation(UnitStateManager unit, EnemyStats enemyStats, int attackPower, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        enemyStats.Health -= attackPower;
        Debug.Log($"Enemigo atacado. Daño aplicado: {attackPower}");

        unit.StartCoroutine(RotateBackToOriginal(unit));
    }

    private System.Collections.IEnumerator WaitAndChangeStateAfterHealing(UnitStateManager unit)
    {
        yield return new WaitForSeconds(3f);

        unit.StartCoroutine(RotateBackToOriginal(unit));

        unit.canAttack = false;
        UnitBaseState newState = new UnitSelectedState();
        unit.ChangeState(newState);
    }

    private System.Collections.IEnumerator PerformBuildAction(UnitStateManager unit)
    {
        yield return new WaitForSeconds(1f);

        Vector3 towerPosition = unit.transform.position;
        towerPosition.y += .5f;

        UnityEngine.Object.Instantiate(unit.tower, towerPosition, Quaternion.identity);

        UnityEngine.Object.Destroy(unit.gameObject);

        UnitBaseState newState = new UnitIdleState();
        unit.ChangeState(newState);
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
