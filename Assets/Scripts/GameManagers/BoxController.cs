using Unity.Mathematics;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private bool isEmpty;
    [SerializeField] private GameObject obstacle;
    [SerializeField] int managerListValeu;
    [SerializeField] int tipeBlock;
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
            Vector3 temporalvector =  new Vector3 (transform.position.x,transform.position.y+.5f,transform.position.z);
            Instantiate(obstacle, temporalvector,quaternion.identity);
            isEmpty = false;
        }
    }
    public void CastleCreate(GameObject castle)
    {
        //spawnea un castillo
        Vector3 temporalvector =  new Vector3 (transform.position.x,transform.position.y+.5f,transform.position.z);
        Instantiate(castle,  temporalvector,quaternion.identity);
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
