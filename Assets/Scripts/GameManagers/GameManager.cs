using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int turnCount;
    [SerializeField] public List<GameObject> allyUnits;
    [SerializeField] public List<GameObject> enemyUnits;
    [SerializeField] public List<GameObject> tilemap;
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
        allyUnits = new List<GameObject>();
        enemyUnits = new List<GameObject>();
        tilemap = new List<GameObject>();
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextTurn();
        }
    }
    public void NextTurn()
    {
        Debug.Log("Next Turn");
        //setea todo para el siguiente turno
        EventManager.instance.turnUpdate();
        //cambia el turno
        turnCount++;
        // hace los seteos de turno respectivos
        IAGeneralManager.instance.Newturn();
        // Llama al metodo para permitir el movimeinto y ataque de las unidades
        UnitTurnManager.instance.EnableMovementAndAttackForAllies();
        CastleControllerUnits.instance.canSpawnUnit = true;

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
