using System.Collections.Generic;
using UnityEngine;

public class decisionController : MonoBehaviour
{
    [SerializeField] private CloseObjects CO;
    [SerializeField] private EnemyStats ES;
    [SerializeField] private List<int> heights;
    [SerializeField] private Transform cubeplace;
    [SerializeField] private bool canmove = false;

    private void Start() 
    {
        CO = GetComponent<CloseObjects>();
        ES = GetComponent<EnemyStats>();    
        canmove = false;
    }

    private void Update() 
    {
        if(canmove)
        {
            Debug.Log("move");
            float step = ES.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, cubeplace.position, step);
        }
    }

    public void takeDesicion()
    {
        canmove = false;
        if(CO.results.Count != 0)
        {
            for(int i = 0;i < heights.Count;i++)
            {
                for(int j = 0; j < CO.results.Count;j++)
                {
                    if(heights[i]==CO.results[j][1])
                    {
                        if(heights[i] == 2)
                        {
                            attack((int)CO.results[j][0]);
                            break;
                        }
                        else if(heights[i] == 1)
                        {
                            attack((int)CO.results[j][0]);
                            break;
                        }
                        else if(heights[i] == 3)
                        {
                            destroy((int)CO.results[j][0]);
                            break;
                        }
                        else if(heights[i] == 5)
                        {
                            attackStructure((int)CO.results[j][0]);
                            break;
                        }
                        else if(heights[i] == 7)
                        {
                            attackStructure((int)CO.results[j][0]);
                            break;
                        }
                    }
                }
            }
            randomMove();
        }
        else
        {
            randomMove();
        }
    }
    private void attack(int cube)
    {
        if(GameManager.instance.tilemap[cube].GetComponent<BoxController>().UnitHere.CompareTag("Red"))
        {
            
        }
        if(GameManager.instance.tilemap[cube].GetComponent<BoxController>().UnitHere.CompareTag("blue"))
        {
            
        }
    }
    private void attackStructure(int cube)
    {
        if(GameManager.instance.tilemap[cube].GetComponent<BoxController>().UnitHere.CompareTag("BCastle"))
        {

        }
        if(GameManager.instance.tilemap[cube].GetComponent<BoxController>().UnitHere.CompareTag("BTorret"))
        {

        }
    }
    private void destroy(int cube)
    {
        // destroy obstacle
    }   
    private void randomMove()
    {
        Vector2 vectorPos = new Vector2(ES.PoX,ES.PoY);
        List<int> emptys = new List<int>();
        int cubeSelect;
        emptys.Clear();
        if(vectorPos.x < 10)
        {
            cubeSelect = (int)((vectorPos.y * 10) + vectorPos.x +1);
            if (GameManager.instance.tilemap[cubeSelect].GetComponent<BoxController>().IsEmpty)
            {
                emptys.Add(cubeSelect);
            }
        }
        if(vectorPos.x > -1)
        {
            cubeSelect = (int)((vectorPos.y * 10) + vectorPos.x -1);
            if (GameManager.instance.tilemap[cubeSelect].GetComponent<BoxController>().IsEmpty)
            {
                emptys.Add(cubeSelect);
            }
        }
        if(vectorPos.y < 10&& vectorPos.y >0)
        {
            cubeSelect = (int)(((vectorPos.y-1)*10)+vectorPos.x);
            if (GameManager.instance.tilemap[cubeSelect].GetComponent<BoxController>().IsEmpty)
            {
                emptys.Add(cubeSelect);
            }
        }
        if(vectorPos.y > -1 && vectorPos.y <9)
        {
            cubeSelect = (int)(((vectorPos.y+1)*10)+vectorPos.x);
            if (GameManager.instance.tilemap[cubeSelect].GetComponent<BoxController>().IsEmpty)
            {
                emptys.Add(cubeSelect);
            }
        }

        if(emptys.Count > 0)
        {
            int placeInt = Random.Range(0,emptys.Count);
            Debug.Log(emptys[placeInt]);
            cubeplace = GameManager.instance.tilemap[placeInt].GetComponent<BoxController>().setPoint.transform;
            canmove = true;
        }
    }
}
