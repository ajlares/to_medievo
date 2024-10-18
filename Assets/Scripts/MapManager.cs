
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
                GameObject boxMapTemp = Instantiate(mapPeefabs[randomRange],new Vector3(worldPosx , .5f , worldPosy), Quaternion.identity);
                if(randomRange !=5 && Random.Range(0,4)==1)
                {
                    boxMapTemp.GetComponent<BoxController>().ObstacleSpawn();
                }
                worldPosx++;
                
            }
            worldPosy++;
            worldPosx=0;
       }
    }
}
