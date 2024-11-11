using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private UnitStateManager currentSelectedUnit;
    [SerializeField] private List<UnitStateManager> selectedUnits = new List<UnitStateManager>();

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

        if (!selectedUnits.Contains(newUnit))
        {
            selectedUnits.Add(newUnit);
            Debug.Log("Unidad añadida a la lista de unidades seleccionadas. Total unidades: " + selectedUnits.Count);

            // Añadir a la lista de allyUnits en el GameManager
            if (!GameManager.instance.allyUnits.Contains(newUnit.gameObject))
            {
                GameManager.instance.allyUnits.Add(newUnit.gameObject);
                Debug.Log("Unidad añadida a allyUnits en GameManager. Total unidades aliadas: " + GameManager.instance.allyUnits.Count);
            }
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

    public List<UnitStateManager> GetSelectedUnits()
    {
        return selectedUnits;
    }

    public void ClearSelectedUnits()
    {
        selectedUnits.Clear();
        Debug.Log("Lista de unidades seleccionadas limpia.");
    }
}
