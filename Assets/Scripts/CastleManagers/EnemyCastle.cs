using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : MonoBehaviour
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

    #region intance
    public static EnemyCastle instance;
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
        int randomUnit = Random.Range(0,units.Count);
        Instantiate(units[randomUnit],spaw.transform.position,Quaternion.identity);
    }
}
