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
        if (!unit.canAttack) return;

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                BoxController boxController = clickedObject.GetComponent<BoxController>();

                // Verificar si está dentro del rango y la casilla es válida
                if (highlightedMapCubes.Contains(clickedObject) && boxController != null)
                {
                    // Buscar el detector de enemigos
                    ObjectDetector objectDetector = clickedObject.GetComponentInChildren<ObjectDetector>();

                    // Comprobar si el objeto es un enemigo
                    if (objectDetector != null && objectDetector.IsEnemy)
                    {
                        GameObject enemy = objectDetector.DetectedEnemy; // Obtener el enemigo detectado
                        EnemyBetaStats enemyStats = enemy.GetComponent<EnemyBetaStats>();

                        if (enemyStats != null)
                        {
                            // Obtener el poder de ataque del personaje y aplicar daño
                            CharactersBase character = unit.GetComponent<CharactersBase>();
                            int damage = character.attackPower;

                            Debug.Log($"Enemigo atacado. Daño aplicado: {damage}");
                            enemyStats.TakeDamage(damage);

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