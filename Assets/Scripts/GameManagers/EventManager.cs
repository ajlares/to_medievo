using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private int naturalDisasterMaxTurns;
    [SerializeField] private int naturalDisasterTurns;
    [SerializeField] private int allyEventMaxTurns;
    [SerializeField] private int allyEventTurns;
    [SerializeField] private int enemyEventMaxTurns;
    [SerializeField] private int enemyEventTurns;

    #region intance
    public static EventManager instance;
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
        naturalDisasterMaxTurns = Random.Range(0,6);
        naturalDisasterTurns = 0;
        allyEventMaxTurns = Random.Range(0,6);
        allyEventTurns = 0;
        enemyEventMaxTurns = Random.Range(0,6);
        enemyEventTurns = 0;
    }
    public void turnUpdate()
    {
        naturalDisasterTurns++;
        allyEventTurns++;
        enemyEventTurns++;
        eventValidation();
    }
    private void eventValidation()
    {
        if(naturalDisasterMaxTurns == naturalDisasterTurns)
        {
            Disaster();
            naturalDisasterMaxTurns = Random.Range(1,6);
            naturalDisasterTurns = 0;
        }
        if(allyEventMaxTurns == allyEventTurns)
        {
            AllyCastle.instance.UnitsToUse = 1;
            allyEventMaxTurns = Random.Range(1,6);
            allyEventTurns = 0;
        }
        if(enemyEventMaxTurns == enemyEventTurns)
        {
            EnemyCastle.instance.UnitsToUse = 1;
            enemyEventMaxTurns = Random.Range(1,6);
            enemyEventTurns = 0;
        }
    }
    private void Disaster()
    {
        int temMapBoxes = MapManager.instance.height * MapManager.instance.width;
        GameObject boxSelected = GameManager.instance.getCube(temMapBoxes);
        Debug.Log(boxSelected);
    }
}
