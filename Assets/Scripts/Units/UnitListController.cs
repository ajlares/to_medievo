using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitListController : MonoBehaviour
{
    public static UnitListController uLController;
    public List<MoveUnits> units; 
    private MoveUnits selectedUnit;

    private void Awake()
    {
        if (uLController == null)
        {
            uLController = this;
            units = new List<MoveUnits>(FindObjectsOfType<MoveUnits>());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SelectUnit(MoveUnits unit)
    {
        selectedUnit = unit;
    }

    public MoveUnits GetSelectedUnit()
    {
        return selectedUnit;
    }
}
