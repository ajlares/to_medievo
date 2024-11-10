using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int health;
    public int movementRange;
    public int attackRange;
    public int attackPower;
    public int defenseBonus;
    public bool canAttack = true;
    private CirclePatternA movementPattern;  // Referencia al patrón de movimiento

    public int GetMovementRange()
    {
        return movementRange;
    }

    public int GetAttackRange()
    {
        return attackRange;
    }

    protected virtual void Start()
    {
        health = maxHealth;
        movementPattern = new CirclePatternA();  // Inicializar el patrón de movimiento
    }

    protected virtual void Update()
    {
        // Si la torre no puede atacar, no hace nada
        if (!canAttack)
        {
            return;
        }

        // Si la torre puede atacar, busca enemigos en su rango de ataque
        List<GameObject> attackCubes = movementPattern.GetValidCubes(gameObject, attackRange);

        foreach (GameObject cube in attackCubes)
        {
            ObjectDetector objectDetector = cube.GetComponentInChildren<ObjectDetector>();

            // Si hay un enemigo en el cubo, aplica daño
            if (objectDetector != null && objectDetector.IsEnemy)
            {
                GameObject enemy = objectDetector.DetectedEnemy;
                EnemyBetaStats enemyStats = enemy.GetComponent<EnemyBetaStats>();

                if (enemyStats != null)
                {
                    int damage = attackPower;
                    Debug.Log($"{characterName} atacó a un enemigo. Daño aplicado: {damage}");
                    enemyStats.TakeDamage(damage);

                    // Desactivar la capacidad de atacar después de un ataque
                    canAttack = false;
                }
                else
                {
                    Debug.LogWarning("No se encontró el componente EnemyBetaStats en el enemigo.");
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        damage -= defenseBonus;
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log($"Vida restante de {characterName}: {health}");
        }
    }

    protected void Die()
    {
        Debug.Log($"{characterName} ha muerto.");
        Destroy(gameObject);
    }
    private void ResetAttack()
    {
        canAttack = true;
        Debug.Log($"{characterName} puede atacar nuevamente.");
    }
}
