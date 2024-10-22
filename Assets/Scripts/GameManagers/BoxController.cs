using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private int internalValeu;
    [SerializeField] private bool isEmpty;
    [SerializeField] private GameObject obstacle;
    [SerializeField] int managerListValeu;

    public int InternalValeu
    {
        get
        {
            return internalValeu;
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
    private void Start() 
    {
       managerListValeu = MapManager.instance.addlistCube(gameObject);
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
}
