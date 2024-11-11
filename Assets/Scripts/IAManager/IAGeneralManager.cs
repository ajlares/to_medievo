using System.Collections;
using System.Data;
using UnityEngine;
public class IAGeneralManager : MonoBehaviour
{
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
        if(cantMoveUnit && GameManager.instance.enemyUnits.Count > 0)
        {
            moveUnit();
        }
        if(canSpawnUnit && EnemyCastle.instance.UnitsToUse > 0)
        {
            SpawnUnit();
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
                int tempint = Random.Range(0,4);
                tempunit = EnemyCastle.instance.units[tempint];
                tempunit.GetComponent<AIMoveUnit>().newPlace(rw/10,rh);
                Vector3 newSpawn = new Vector3 (tempCube.transform.position.x, tempCube.transform.position.y + .5f, tempCube.transform.position.z);
                Instantiate(tempunit,newSpawn,Quaternion.identity);
                tempCube.GetComponent<BoxController>().saveObject(tempunit);
                GameManager.instance.enemyUnits.Add(tempunit);
                EnemyCastle.instance.UnitsToUse = -1;
                break;
            }
        }
    }
    private void moveUnit()
    {
        cantMoveUnit = false;
        for(int i =0;i < GameManager.instance.enemyUnits.Count;i++)
        {
            GameManager.instance.enemyUnits[i].GetComponent<AIMoveUnit>().moveUnit();
        }
    }
    public void Newturn()
    {
        StartCoroutine(WaitTime());
        canSpawnUnit = true;
        cantMoveUnit = true;
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        yield return null;
    }
}
