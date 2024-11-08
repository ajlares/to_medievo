using System.Collections;
using UnityEngine;
public class IAGeneralManager : MonoBehaviour
{
    [SerializeField] private bool turnComplete;
    [SerializeField] private bool nextMove;
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
            if(nextMove)
            {
                if(canSpawnUnit && EnemyCastle.instance.UnitsToUse > 0)
                {
                    Debug.Log("spawmUnit");
                    SpawnUnit();
                }
                if(GameManager.instance.enemyUnits.Count>0)
                {
            
                }

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
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(3);
        canSpawnUnit = true;
        yield return null;
    }
    IEnumerator turnDelay()
    {
        yield return new WaitForSeconds(.25f);
    }

}
