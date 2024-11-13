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
            move();
        }
        else
        {
            move();
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
    private void move()
    {
        // move to random cube if all are clean
    }
}
