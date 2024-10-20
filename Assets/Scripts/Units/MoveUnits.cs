using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para el movmiento de cada unidad 
public class MoveUnits : MonoBehaviour
{
    public GameObject unit;
    public GameObject finalPosition;
    public float speed;

    private void OnMouseDown()
    {
        UnitListController.uLController.SelectUnit(this);
    }

    private void Update()
    {
        if (finalPosition != null && IsPositionInRange(finalPosition.transform.position))
        {
            unit.transform.position = Vector3.MoveTowards(unit.transform.position, finalPosition.transform.position, speed * Time.deltaTime);

            if (unit.transform.position == finalPosition.transform.position)
            {
                UnitManager.unitManager.ClearHighlight(); 
                UnitManager.unitManager.DeselectUnit(); 
                finalPosition = null;  
            }
        }
    }

    public void UpdateFinalPosition(GameObject newPosition)
    {
        finalPosition = newPosition;
    }

    private bool IsPositionInRange(Vector3 targetPosition)
    {
        Vector3 currentPosition = unit.transform.position;
        float distance = Vector3.Distance(currentPosition, targetPosition);
        return distance <= UnitManager.unitManager.highlightRange;
    }
}