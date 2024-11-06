
using UnityEngine;

public class EventsCreates : MonoBehaviour
{
    [SerializeField] private GameObject stone;
    
    #region intance
    public static EventsCreates instance;
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

    public void SelectBox()
    {
        int totalTiles = MapManager.instance.height * MapManager.instance.width;
        int tempTileSelect = Random.Range(1, totalTiles-1); 
        GameObject boxSelected = GameManager.instance.getCube(tempTileSelect);
        if(boxSelected != null)
        {
            eventSelect(boxSelected);
        }
    }

    private void eventSelect(GameObject boxSelected)
    {
        Vector3 temporalvector = boxSelected.GetComponent<BoxController>().mapWayPoint.transform.position;
        Vector3 spawPoint = new Vector3(temporalvector.x,temporalvector.y+10,temporalvector.z);
        Instantiate(stone,spawPoint,Quaternion.identity);
    }
}
