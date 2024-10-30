using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private bool isEmpty;
    [SerializeField] private GameObject obstacle;
    [SerializeField] int managerListValeu;
    [SerializeField] GameObject unitHere;

    public int ManagerListValeu
    {
        get
        {
            return managerListValeu;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }
        set
        {
            isEmpty = value;
        }
    }
    public GameObject UnitHere
    {
        get
        {
            return unitHere;
        }
    }
    private void Start() 
    {
       managerListValeu = GameManager.instance.addlistCube(gameObject);
    }

    public void ObstacleSpawn()
    {
        //spawnea un obstaculo
        if(isEmpty)
        {
            Instantiate(obstacle, transform);
            isEmpty = false;
        }
        //Instantiate(mapPeefabs[randomRange],new Vector3(worldPosx , .5f , worldPosy), Quaternion.identity);
    }
    public void CastleCreate(GameObject castle)
    {
        //spawnea un castillo
            Instantiate(castle, transform);
            isEmpty = false;
    }
    public void saveObject()
    {
        
    }
}
