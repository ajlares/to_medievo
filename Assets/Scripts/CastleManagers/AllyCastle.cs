using System.Collections.Generic;
using UnityEngine;

public class AllyCastle : MonoBehaviour
{
    [SerializeField] private int unitsToUse;
    [SerializeField] private List<GameObject> units;
    [SerializeField] private int maxLife;
    [SerializeField] private int life;
    public int UnitsToUse
    {
        get
        {
            return unitsToUse;
        }
        set
        {
            unitsToUse += value;
        }
    }
    public int Life
    {
        get
        {
            return life;
        }
    }
        #region intance
    public static AllyCastle instance;
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy( this);
        }
    }
    #endregion
    public void instanceUnit(GameObject spaw)
    {
        int randomUnit = Random.Range(0, units.Count);

        Vector3 spawnPosition = spaw.transform.position;
        spawnPosition.y = 1f;
        
        Instantiate(units[randomUnit], spawnPosition, Quaternion.identity);
    }
}
