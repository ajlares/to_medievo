using UnityEngine;

public class AllyCastle : MonoBehaviour
{
    [SerializeField] private int unitsToUse;
    public int UnitsToUse
    {
        get
        {
            return unitsToUse;
        }
        set
        {
            unitsToUse += value;
        }
    }

        #region intance
    public static AllyCastle instance;
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
}
