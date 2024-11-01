using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int turnCount;

    [SerializeField] private List<GameObject> allyUnits;
    [SerializeField] private List<GameObject> enemyUnits;
    [SerializeField] private List<GameObject> tilemap;
    #region intance
    public static GameManager instance;
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy( this);
        }
    }
    #endregion

    private void Start() 
    {
        turnCount = 1;
        MapManager.instance.worldGenerate();   
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextTurn();
        }
    }
    private void NextTurn()
    {
        //setea todo para el siguiente turno
        EventManager.instance.turnUpdate();
        //cambia el turno
        turnCount++;
    }
    
    private void canSelect()
    {

    }
    
    public int addlistCube(GameObject valeu)
    {
        tilemap.Add(valeu);
        return tilemap.Count - 1;
    }
    public GameObject getCube(int valeu)
    {
        if(valeu < tilemap.Count)
        {
            return tilemap[valeu];
        }
        else
        {
        return null;
        }
    }
}
