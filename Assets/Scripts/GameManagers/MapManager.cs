using System.Collections.Generic;
using UnityEngine;

// script enfocado en la generacion del mundo en un grid de de tamaño variable 
public class MapManager : MonoBehaviour
{
        // tamañano del mapa
    [SerializeField] public int height;
    [SerializeField] public int width;
    // lista de los prefabs del mapa
    [SerializeField] private List<GameObject> mapPeefabs;
    [SerializeField] private List<GameObject> castles;

    #region intance
    public static MapManager instance;
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
    private void Start() 
    {
    }

    #region world Generate
    // region enfocada en la generacion del mundo
    public void worldGenerate()
    {
        float worldPosx = 0;
        float worldPosy = 0;
       for(int i = 0; i < width ; i++)
       { 
            for(int j = 0 ; j < height ; j++)
            {
                int randomRange = UnityEngine.Random.Range(0,6);
                if(i == 0 && j == 0)
                {
                    GameObject boxMapTemp = Instantiate(mapPeefabs[4],new Vector3(worldPosx , .5f , worldPosy), Quaternion.identity);
                    boxMapTemp.GetComponent<BoxController>().CastleCreate(castles[0]);
                }
                else if(i == (width-1)  && j == (height-1))
                {
                    GameObject boxMapTemp = Instantiate(mapPeefabs[4],new Vector3(worldPosx , .5f , worldPosy), Quaternion.identity);
                    boxMapTemp.GetComponent<BoxController>().CastleCreate(castles[1]);
                }
                else
                {
                    GameObject boxMapTemp = Instantiate(mapPeefabs[randomRange],new Vector3(worldPosx , .5f , worldPosy), Quaternion.identity);
                    if(randomRange !=5 && UnityEngine.Random.Range(0,4)==1)
                    {
                        boxMapTemp.GetComponent<BoxController>().ObstacleSpawn();
                    }
                }
                worldPosx++;
            }
            worldPosy++;
            worldPosx=0;
       }
    }
    #endregion
}
