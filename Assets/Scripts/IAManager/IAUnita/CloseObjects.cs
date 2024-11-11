using System.Collections.Generic;
using UnityEngine;

public class CloseObjects : MonoBehaviour
{
    [SerializeField] private List<Vector2> results;
    [SerializeField] EnemyStats ES;

    private void Start() 
    {
        results = new List<Vector2>();
        Debug.Log("Tamaño de lista inicial: " + results.Count);
    }
    public void UpdateCLoseObjects()
    {
        results.Clear();
        sourthUpdate();
        northUpdate();
        eastUpdate();
        weastUpdate();
    }
    private void sourthUpdate()
    {
        Debug.Log("Tamaño de lista: " + results.Count);
        int x = ES.PoX;
        int y = ES.PoY;
        for(int i = 0; i<ES.actionDistance;i++)
        {
            int cubeSelect = ((y-(i+1))*10)+x;
            if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
            {
                int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                Debug.Log(cubeSelect + " " + " " + indexInt);
                addlist(new Vector2(cubeSelect, indexInt));
                Debug.Log(results[0]);
                // Debug.Log(results[1]);
            }
        }
        Debug.Log("Tamaño de lista final: " + results.Count);
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
    private void addlist(Vector2 _vector)
    {
        results.Add(_vector);
    }
}
