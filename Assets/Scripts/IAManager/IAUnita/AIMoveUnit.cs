using UnityEngine;
using UnityEngine.UIElements;


public class AIMoveUnit : MonoBehaviour
{
    [SerializeField] public Vector2 actualPlace;
    public void newPlace(int x, int z)
    {
        actualPlace = new Vector2(x,z); 
    }
    public void moveUnit()
    {
        int actionDistance = GetComponent<EnemyStats>().actionDistance;
        bool enemyDetecte;
        bool allyDetecte;
        Vector2 enemyPlace;
        Vector2 allyPlace;
        //south
        if((actualPlace.y - actionDistance) > 0)
        {
            for(int i = 0; i <= actionDistance; i ++)
            {
                int cubePlace = (int)((((actualPlace.x * 10 ) + actualPlace.y ) -i));
                GameObject tempCube = GameManager.instance.getCube(cubePlace);
                if(!tempCube.GetComponent<BoxController>().IsEmpty)
                {
                    
                }
            }
        }
        //west
        if((actualPlace.x - actionDistance) < 0)
        {
            
        }
        // east
        if((actualPlace.x + actionDistance) < 0)
        {

        }
        // north
        if((actualPlace.y + actionDistance) < 0)
        {

        }
    }
}
