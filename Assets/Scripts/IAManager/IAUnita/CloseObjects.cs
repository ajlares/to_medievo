using System.Collections.Generic;
using UnityEngine;

public class CloseObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesClose;
    [SerializeField] List<GameObject> allyClose;
    [SerializeField] List<GameObject> obstaclesClose;

    public void UpdateCLoseObjects()
    {
        sourthUpdate();
        northUpdate();
        eastUpdate();
        weastUpdate();
    }

    private void updatepos()
    {

    }
    private void sourthUpdate()
    {
        
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
}
