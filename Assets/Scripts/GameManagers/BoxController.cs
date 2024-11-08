using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private bool isEmpty;
    [SerializeField] private GameObject obstacle;
    [SerializeField] int managerListValeu;
    [SerializeField] GameObject objectHere;
    [SerializeField] public GameObject mapWayPoint;

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
            return objectHere;
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
            Instantiate(obstacle, mapWayPoint.transform);
            isEmpty = false;
        }
    }
    public void CastleCreate(GameObject castle)
    {
        //spawnea un castillo
        Instantiate(castle, mapWayPoint.transform);
        isEmpty = false;
    }
    public void saveObject(GameObject ObjectToSave)
    {
        objectHere = ObjectToSave;
        isEmpty = false;
    }
    public void removeObject()
    {
        objectHere = null;
        isEmpty = true;
    }
}
