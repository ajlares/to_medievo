using System.Collections.Generic;
using UnityEngine;

public class decisionController : MonoBehaviour
{
    [SerializeField] private CloseObjects CO;
    [SerializeField] private List<int> heights;
    private void Start() 
    {
        CO = GetComponent<CloseObjects>();    
    }

    public void takeDesicion()
    {
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
                            Debug.Log("attack enemy");
                            break;
                        }
                        else if(heights[i] == 1)
                        {
                            attack((int)CO.results[j][0]);
                            Debug.Log("attack ally");
                            break;
                        }
                        else if(heights[i] == 3)
                        {
                            destroy((int)CO.results[j][0]);
                            Debug.Log("destroy obstacle");
                            break;
                        }
                        else if(heights[i] == 5)
                        {
                            attackStructure((int)CO.results[j][0]);
                            Debug.Log("attack enemy castle");
                            break;
                        }
                        else if(heights[i] == 7)
                        {
                            attackStructure((int)CO.results[j][0]);
                            Debug.Log("attack enemy torret");
                            break;
                        }
                    }
                }
            }
            move();
            Debug.Log("move 2");
        }
        else
        {
            move();
                    Debug.Log("move 1");
        }
    }
    private void attack(int cube)
    {
    
    }
    private void attackStructure(int cube)
    {

    }
    private void destroy(int cube)
    {

    }
    private void move()
    {
    }
}
