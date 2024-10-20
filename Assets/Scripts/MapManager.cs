
using System.Collections.Generic;
using UnityEngine;

// script enfocado en la generacion del mundo en un grid de de tamaño variable 
public class MapManager : MonoBehaviour
{
        // tamañano del mapa
    [SerializeField] private int height;
    [SerializeField] private int width;
    // lista de los prefabs del mapa
    [SerializeField] private List<GameObject> mapPeefabs;
    [SerializeField] private List<GameObject> castles;
    [SerializeField] private List<GameObject> tilemap;


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
    private void Start() 
    {
        wordGenerate();
    }

    private void wordGenerate()
    {
        float worldPosx = 0;
        float worldPosy = 0;
       for(int i = 0; i < width ; i++)
       { 
            for(int j = 0 ; j < height ; j++)
            {
                int randomRange = Random.Range(0,6);
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
                    if(randomRange !=5 && Random.Range(0,4)==1)
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

    public int addlistCube(GameObject valeu)
    {
        tilemap.Add(valeu);
        return tilemap.Count - 1;
    }
}
