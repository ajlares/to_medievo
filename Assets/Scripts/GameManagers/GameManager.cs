using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int turnCount;
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

    private void NextTurn()
    {
        //setea todo para el siguiente turno
        
        //cambia el turno
        turnCount++;
    }
}
