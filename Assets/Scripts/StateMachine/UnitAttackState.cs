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
        CharactersBase character = unit.GetComponent<CharactersBase>();

        if(character is Builder && Input.GetButtonDown("Fire2"))
        {
            UnityEngine.Object.Destroy(unit.gameObject);

            UnityEngine.Object.Instantiate(unit.tower, unit.transform.position, Quaternion.identity);

            UnitBaseState newState = new UnitIdleState();
            unit.ChangeState(newState);

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
                            targetCharacter.Heling(healAmount);

                            Debug.Log($"Unidad aliada curada. Curación aplicada: {healAmount}");

                            unit.canAttack = false;

                            UnitBaseState newState = new UnitSelectedState();
                            unit.ChangeState(newState);
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
            if (!unit.canAttack) return;

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
                        EnemyBetaStats enemyStats = enemy.GetComponent<EnemyBetaStats>();

                        if (enemyStats != null)
                        {
                            int damage = character.attackPower;
                            Debug.Log($"Enemigo atacado. Daño aplicado: {damage}");
                            enemyStats.TakeDamage(damage);
                            unit.canAttack = false;
                            unit.anim.SetInteger("Count",2);

                            UnitBaseState newState = new UnitSelectedState();
                            unit.ChangeState(newState);
                        }
                        else
                        {
                            Debug.LogWarning("No se encontró el componente EnemyBetaStats en el enemigo.");
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