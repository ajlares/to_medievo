
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
        int tempTileSelect = Random.Range(0, totalTiles); 
        GameObject boxSelected = GameManager.instance.getCube(tempTileSelect);
        if(boxSelected != null)
        {
            eventSelect(boxSelected);
        }
    }

    private void eventSelect(GameObject boxSelected)
    {
        Instantiate(stone,boxSelected.GetComponent<BoxController>().mapWayPoint.transform);
    }
}
