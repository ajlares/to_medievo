using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] public int actionDistance;
    [SerializeField] public int moveDistance;
    [SerializeField] private  int poX;
    [SerializeField] private int poY;
    [SerializeField] private float speed;

    #region geterysetters
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health += value;
            // reproduce hit
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
    public int PoX
    {
        get
        {
            return poX;
        }
        set
        {
            poX = value;
        }
    }
    public int PoY
    {
        get
        {
            return poY;
        }
        set
        {
            poY = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
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
        if(health < 1)
        {
           
        }

    }
    
    //private  IEnumerator death()
    //{
    //    yield return null;
    //}
    
}
