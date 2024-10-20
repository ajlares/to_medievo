using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelector : MonoBehaviour
{
    public GameObject mapWaipont;

    private void OnMouseDown()
    {
        if (CompareTag("MapCube"))
        {
            MoveUnits selectedUnit = UnitListController.uLController.GetSelectedUnit(); 

            if (selectedUnit != null)
            {
                selectedUnit.UpdateFinalPosition(mapWaipont); 
            }
        }
    }
}

