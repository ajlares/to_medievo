using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBetaStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Vida restante" + health);
        }
    }

    public void Die()
    {
        Debug.Log(" ha muerto.");
        Destroy(gameObject);
    }
}
