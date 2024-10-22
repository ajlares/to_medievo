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
}
