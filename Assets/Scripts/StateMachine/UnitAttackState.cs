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

        // Revisar si se presiona el botón derecho (Fire2) para curar si es un Cleric
        if(character is Builder && Input.GetButtonDown("Fire2"))
        {
            UnityEngine.Object.Destroy(unit.gameObject);

            // Instanciar la torre en la posición del constructor
            UnityEngine.Object.Instantiate(unit.tower, unit.transform.position, Quaternion.identity);

            // Cambiar el estado a un estado adecuado, como Idle, después de destruir el Builder
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

                // Verificar si el objeto clicado está dentro del rango iluminado y es una casilla válida
                if (highlightedMapCubes.Contains(clickedObject) && boxController != null)
                {
                    ObjectDetector objectDetector = clickedObject.GetComponentInChildren<ObjectDetector>();

                    // Verificar si el objeto es una unidad aliada
                    if (objectDetector != null && objectDetector.IsUnit)
                    {
                        GameObject united = objectDetector.DetectedUnit;
                        CharactersBase targetCharacter = united.GetComponentInParent<CharactersBase>();

                        if (targetCharacter != null && targetCharacter != character)
                        {
                            // Realizar la curación en la unidad aliada
                            int healAmount = character.attackPower; // Puede usarse un valor de curación específico
                            targetCharacter.Heling(healAmount);

                            Debug.Log($"Unidad aliada curada. Curación aplicada: {healAmount}");

                            unit.canAttack = false;

                            // Cambiar a un nuevo estado si el ataque se realiza correctamente
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
        // ataque para unidades que no son Constructor o Cleric, o al hacer clic izquierdo
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

                    // Comprobar si el objeto es un enemigo
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

                            // Cambiar a un nuevo estado si el ataque se realiza correctamente
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