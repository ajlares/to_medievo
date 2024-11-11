using System.Collections.Generic;
using UnityEngine;

public class CloseObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesClose;
    [SerializeField] List<GameObject> allyClose;
    [SerializeField] List<GameObject> obstaclesClose;
    [SerializeField] List<GameObject> metheoriteClose;
    [SerializeField] List<int> placeEmptys;
    [SerializeField] EnemyStats ES;

    public void UpdateCLoseObjects()
    {
        enemiesClose.Clear();
        allyClose.Clear();
        obstaclesClose.Clear();
        metheoriteClose.Clear();
        sourthUpdate();
        northUpdate();
        eastUpdate();
        weastUpdate();
    }

    private void updatepos()
    {

    }
    private void sourthUpdate()
    {
        int x = ES.PoX;
        int y = ES.PoY;
        for(int i =0; i<ES.actionDistance;i++)
        {
            int cubeSelect = ((y-(i+1))*10)+x;
            if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
            {
                GameObject tempCube = GameManager.instance.tilemap[cubeSelect];
                if(tempCube.gameObject.GetComponent<BoxController>().UnitHere.CompareTag("blue"))
                {
                    enemiesClose.Add(tempCube.gameObject.GetComponent<BoxController>().UnitHere);
                }
                if(tempCube.gameObject.GetComponent<BoxController>().UnitHere.CompareTag("Red"))
                {
                    allyClose.Add(tempCube.gameObject.GetComponent<BoxController>().UnitHere);
                }
                if(tempCube.gameObject.GetComponent<BoxController>().UnitHere.CompareTag("Obstacle"))
                {
                    obstaclesClose.Add(tempCube.gameObject.GetComponent<BoxController>().UnitHere);
                }
                if(tempCube.gameObject.GetComponent<BoxController>().UnitHere.CompareTag("meteriorite"))
                {
                    metheoriteClose.Add(tempCube.gameObject.GetComponent<BoxController>().UnitHere);
                }
            }
        }
    }
    private void northUpdate()
    {
        
    }
    private void eastUpdate()
    {
        
    }
    private void weastUpdate()
    {
        
    }
}
