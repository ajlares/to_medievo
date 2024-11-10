using UnityEngine;


public class AIMoveUnit : MonoBehaviour
{
    [SerializeField] public Vector2 actualPlace;
    private void Start()
    {
        actualPlace = new Vector2(transform.position.x,transform.position.z);    
    }
    public void moveUnit()
    {
        int close = closeObjects(); 
    }
    private int closeObjects()
    {
        Debug.Log("seachMovement");
        int actionDistance = GetComponent<EnemyStats>().actionDistance;
        int valeuS, valeuW, valeuE, valeuN,search;
        //south
        if((actualPlace.x - actionDistance) > 0)
        {
            search = (int)((actualPlace.x * 10)+actualPlace.y);
            Debug.Log(search);
        }
        //west
        if((actualPlace.y - actionDistance) < 0)
        {
            
        }
        // east
        if((actualPlace.y + actionDistance) < 0)
        {

        }
        // north
        if((actualPlace.x + actionDistance) < 0)
        {

        }
        return 0;
    }
}
