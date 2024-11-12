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
        results.Clear();
        searchObjects();
    }
    private void searchObjects()
    {
        int x = ES.PoX;
        int y = ES.PoY;
        int cubeSelect;
        for(int i = 0; i<ES.actionDistance;i++)
        {   
            // sorth seearch
            if((y - 1) >= 0)
            {
                cubeSelect = ((y-(i+1))*10)+x;
                Debug.Log("cubeselect" + cubeSelect);
                if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
                {
                    int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                    addlist(new Vector2(cubeSelect, indexInt));
                    cubeSelect =0;
                }
            }
            // north search
            if((y + 1) <= 9)
            {
                cubeSelect = (( y + i + 1 ) * 10 ) + x;
                Debug.Log("cubeselect" + cubeSelect);
                if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
                {
                    int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                    addlist(new Vector2(cubeSelect, indexInt));
                    cubeSelect = 0;
                }
            }
        }

        for(int i =0; i < results.Count;i++)
        {
            Debug.Log(results[i]);
        }
    }   
    private void addlist(Vector2 _vector)
    {
        results.Add(_vector);
    }
}
