using System.Collections.Generic;
using UnityEngine;

public class AllyCastle : MonoBehaviour
{
    [SerializeField] private int unitsToUse;
    [SerializeField] private List<GameObject> units;
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
        int randomUnit = Random.Range(0,units.Count);
        Instantiate(units[randomUnit],spaw.transform.position,Quaternion.identity);
    }
}
