using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CharactersBase : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int health;
    public int movementRange;
    public int attackRange;
    public int attackPower;
    public int defenseBonus;

    public Animator anim;

    protected UnitStateManager stateManager;

    public MovementPattern MovementPattern { get; protected set; }
    public MovementPattern AttackPattern { get; protected set; }
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
        stateManager = GetComponent<UnitStateManager>();
        health = maxHealth;
    }

    public abstract void SpecialAbility();

    public void TakeDamage(int damage)
    {
        damage -= defenseBonus;
        health -= damage;
        anim.SetInteger("C",3);
        if (health <= 0)
        {
            anim.SetInteger("C",4);
        }
        else
        {
            Debug.Log("Vida restante" + health);
        }
    }

    public void Heling(int heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log($"{characterName} curado. Salud actual: {health}");
    }

    protected void Die()
    {
        Debug.Log(characterName + " ha muerto.");
        AllyCastle.instance.UnitsToUse = +1;
        Destroy(gameObject);
    }
}
