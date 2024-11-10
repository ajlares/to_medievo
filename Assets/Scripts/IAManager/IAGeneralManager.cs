using System.Collections;
using UnityEngine;
public class IAGeneralManager : MonoBehaviour
{
    [SerializeField] public bool turnComplete;
    [SerializeField] public GameObject tempunit;
    [SerializeField] private bool cantMoveUnit;
    [SerializeField] private bool canSpawnUnit;
    [SerializeField] private GameObject tempCube;
    public int rw,rh;

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
                SpawnUnit();
            }
            else if(GameManager.instance.enemyUnits.Count>0)
            {
                //moveUnit();
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
        for(int i = 0;i < 9 ; i++)
        {

            rw = Random.Range(7,10);
            rh = Random.Range(7,10);
            rw *= 10;
            tempCube = GameManager.instance.getCube(rw+rh);
            if(tempCube.GetComponent<BoxController>().IsEmpty)
            {   
                Vector3 newSpawn = new Vector3 (tempCube.transform.position.x, tempCube.transform.position.y + .5f, tempCube.transform.position.z);
                int tempint = Random.Range(0,4);
                tempunit = EnemyCastle.instance.units[tempint];
                Instantiate(tempunit,newSpawn,Quaternion.identity);
                tempCube.GetComponent<BoxController>().saveObject(tempunit);
                EnemyCastle.instance.UnitsToUse =-1;
                break;
            }
        }
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
