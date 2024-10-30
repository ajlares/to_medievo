using UnityEngine;

public class UnitSelectedState : UnitBaseState
{
    private Material originalMaterial;
    private Material outlineMaterial;
    public override void EnterState(UnitStateManager unit)
    {
        Debug.Log("Entrando en SELECTED");
        outlineMaterial = Resources.Load<Material>("OutlineMaterial1");
        ApplyOutline(unit, true);
    }

    public override void ExitState(UnitStateManager unit)
    {
        Debug.Log("Saliendo de SELECTED");
        ApplyOutline(unit, false);
    }

    public override void UpdateState(UnitStateManager unit)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnitBaseState newState = new UnitAttackState();
            unit.ChangeState(newState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CharactersBase character = unit.GetComponent<CharactersBase>();
            if (character != null && unit.canMove)
            {
                unit.ChangeState(new UnitMovementState());
            }
            else
            {
                Debug.Log("La unidad no puede moverse.");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnitBaseState newState = new UnitIdleState();
            unit.ChangeState(newState);
        }
    }

    private void ApplyOutline(UnitStateManager unit, bool applyOutline)
    {
        Renderer renderer = unit.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (applyOutline)
            {
                originalMaterial = renderer.material;
                renderer.material = outlineMaterial;
            }
            else if (originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
        }
    }
}
