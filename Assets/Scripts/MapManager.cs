using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

// script enfocado en la generacion del mundo en un grid de 5x5
public class MapManager : MonoBehaviour
{
    // lista de los prefabs del mapa
    [SerializeField] private List<GameObject> mapPeefabs;
    // arreglo del mapa para poder trabajar con el
    [SerializeField] private GameObject[][] mapgrid;
    // tamañano del mapa
    [SerializeField] private int height;
    [SerializeField] private int width;

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
                GameObject boxMapTemp = Instantiate(mapPeefabs[randomRange],new Vector3(worldPosx , 0 , worldPosy), Quaternion.identity);
                worldPosx++;
                //mapgrid[i][j] = boxMapTemp;
            }
            worldPosy++;
            worldPosx=0;
       }
    }
}
