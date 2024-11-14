using System.Collections.Generic;
using UnityEngine;

public class CloseObjects : MonoBehaviour
{
    [SerializeField] public List<Vector2> results;
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
        for(int i = 1; i<ES.actionDistance + 1;i++)
        {   
            // sorth seearch
            if((y - i) > -1)
            {
                cubeSelect = (( y - i )*10)+x;
                if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
                {
                    int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                    addlist(new Vector2(cubeSelect, indexInt));
                    cubeSelect =0;
                }
            }
            // north search
            if((y + i) < 10 )
            {   
                cubeSelect = (( y + i ) * 10 ) + x;
                if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
                {
                    int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                    addlist(new Vector2(cubeSelect, indexInt));
                    cubeSelect = 0;
                }     
            }
            // weast search
            if(x - i > -1)
            {
                cubeSelect = (y*10) + x-i;
                if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
                {
                    int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                    addlist(new Vector2(cubeSelect, indexInt));
                    cubeSelect = 0;
                }   
            }
            // east search
            if(x + i < 10)
            {
                cubeSelect = (y*10) + x+i;
                if(!GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().IsEmpty)
                {
                    int indexInt = GameManager.instance.tilemap[cubeSelect].gameObject.GetComponent<BoxController>().UnitHere.GetComponent<IAID>().ID;
                    addlist(new Vector2(cubeSelect, indexInt));
                    cubeSelect = 0;
                }  
            }

        }
    }   
    private void addlist(Vector2 _vector)
    {
        results.Add(_vector);
    }
}
