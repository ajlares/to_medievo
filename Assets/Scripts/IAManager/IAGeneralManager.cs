using System.Collections;
using UnityEngine;
public class IAGeneralManager : MonoBehaviour
{
    [SerializeField] public bool turnComplete;
    [SerializeField] private bool cantMoveUnit;
    [SerializeField] private bool canSpawnUnit;

     #region intance
    public static IAGeneralManager instance;
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

    void Start()
    {
        
    }

    void Update()
    {
        if(!turnComplete)
        {
            if(canSpawnUnit && EnemyCastle.instance.UnitsToUse > 0)
            {
                Debug.Log("spawmUnit");
                SpawnUnit();
            }
            else if(GameManager.instance.enemyUnits.Count>0)
            {
                
            }
            else
            {
                cantMoveUnit = true;
            }
            if(cantMoveUnit)
            {
                turnComplete = true;
            }

        }
    }
    private void SpawnUnit()
    {
        canSpawnUnit = false;
        // instance unit
    }
    private void moveUnit(GameObject unit)
    {

    }
    public void Newturn()
    {
        StartCoroutine(WaitTime());
        turnComplete = false;
        canSpawnUnit = true;
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        yield return null;
    }
}
