using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{   
    private Renderer objRenderer;
    private Material originalMaterial;
    public Material highlightMaterial;

    public GameObject cameraUnit;

     private void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;
    }

    private void OnMouseDown()
    {
        if (UnitManager.unitManager.GetSelectedUnit() == this.gameObject)
        {
            UnitManager.unitManager.DeselectUnit();
            ResetMaterial();
        }
        else
        {
            MoveCameraToUnit();
            UnitManager.unitManager.SelectUnit(this.gameObject);
            objRenderer.material = highlightMaterial;
        }
    }

    private void OnMouseEnter()
    {
        if (UnitManager.unitManager.GetSelectedUnit() != this.gameObject)
        {
            objRenderer.material = highlightMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (UnitManager.unitManager.GetSelectedUnit() != this.gameObject)
        {
            ResetMaterial();
        }
    }

    public void ResetMaterial()
    {
        objRenderer.material = originalMaterial;
    }

    private void MoveCameraToUnit()
    {
        if (cameraUnit != null)
        {
            Camera.main.transform.position = new Vector3(cameraUnit.transform.position.x, cameraUnit.transform.position.y, cameraUnit.transform.position.z);
            Camera.main.transform.LookAt(transform.position);
        }
    }
}