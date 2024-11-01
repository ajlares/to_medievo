using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRangeAttack : MonoBehaviour
{
    public float rangeAttack = 5f;
    private List<GameObject> cellsInRange = new List<GameObject>();

    public void SetRangeCells(List<GameObject> cells)
    {
        cellsInRange = cells;
    }

    public bool CanMoveCell(GameObject cells)
    {
        return cellsInRange.Contains(cells);
    }

    public void LimpiarRangoCeldas()
    {
        cellsInRange.Clear();
    }
    
    public void ActualizarRangoMovimiento(float newRange)
    {
        rangeAttack = newRange;
    }
}
