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
        for(int i = 1; i<ES.actionDistance + 1;i++)
        {   
            // sorth seearch
            if((y - i) > -1)
            {
                Debug.Log("abajo");
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
                Debug.Log("arriba");
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
                Debug.Log("izquierda");
            }
            // east search
            if(x + i < 10)
            {
                Debug.Log("derecha");
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
