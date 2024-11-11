using System.Collections.Generic;
using UnityEngine;

public class CloseObjects : MonoBehaviour
{
    [SerializeField] private List<Vector2> results;
    [SerializeField] EnemyStats ES;

    private void Start() 
    {
        results = new List<Vector2>();
    }
    public void UpdateCLoseObjects()
    {
        sourthUpdate();
        northUpdate();
        eastUpdate();
        weastUpdate();
    }
    private void sourthUpdate()
    {
        int x = ES.PoX;
        int y = ES.PoY;
        for(int i = 0; i<ES.actionDistance;i++)
        {
            int cubeSelect = ((y-(i+1))*10)+x;
            if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
            {
                int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                Debug.Log(cubeSelect + " " + " " + indexInt);
                addlist(cubeSelect,indexInt);
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
    private void addlist(int x, int y)
    {
        results.Add(new Vector2(x,y));
    }
}
