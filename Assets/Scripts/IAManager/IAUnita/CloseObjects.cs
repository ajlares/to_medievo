using System.Collections.Generic;
using UnityEngine;

public class CloseObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesClose;
    [SerializeField] List<GameObject> allyClose;
    [SerializeField] List<GameObject> obstaclesClose;
    [SerializeField] List<GameObject> placeEmptys;
    [SerializeField] EnemyStats ES;

    public void UpdateCLoseObjects()
    {
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
            Debug.Log(((y-1)*10)+x);
            if(GameManager.instance.tilemap[((y-1)*10)+x].gameObject.GetComponent<BoxController>().IsEmpty)
            {
                Debug.Log( ((y-1)*10)+x + "is empty");
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
