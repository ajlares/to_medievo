using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] public int actionDistance;

    #region geterysetters
    int Health
    {
        get
        {
            return health;
        }
        set
        {
            health += value;
            death();
        }
    }
    int Damage
    {
        get
        {
            return damage;
        }
    }
    #endregion

    private void Start()
    {
        health = maxHealth;
        transform.Rotate(new Vector3(0, 180, 0));
    }
    void death()
    {

    }
    
    public void destroy()
    {

    }

}
