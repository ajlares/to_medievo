using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private UnitStateManager currentSelectedUnit;

    public void SelectUnit(UnitStateManager newUnit)
    {
        if (currentSelectedUnit != null && currentSelectedUnit != newUnit)
        {
            currentSelectedUnit.ChangeState(new UnitIdleState());
        }

        currentSelectedUnit = newUnit;

        if (currentSelectedUnit != null)
        {
            currentSelectedUnit.ChangeState(new UnitSelectedState());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                UnitStateManager clickedUnit = hit.collider.GetComponent<UnitStateManager>();
                if (clickedUnit != null)
                {
                    SelectUnit(clickedUnit);
                }
            }
        }
    }
}
