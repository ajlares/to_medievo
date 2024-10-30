
using UnityEngine;

public class EventsCreates : MonoBehaviour
{
    
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
        if(boxSelected.GetComponent<BoxController>().IsEmpty)
        {
            Debug.Log("esta vacia vamos a hacer algo");
        }
        else
        {
            Debug.Log("la caja tiene algo vamos a descubirilos");
        }
    }
}
