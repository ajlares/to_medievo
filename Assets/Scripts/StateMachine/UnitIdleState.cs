using UnityEditor;
using UnityEngine;

public class UnitIdleState : UnitBaseState
{
     public override void EnterState(UnitStateManager unit)
    {
        unit.Anim.SetInteger("C", 0);
    }

    public override void ExitState(UnitStateManager unit)
    {
        Debug.Log("Saliendo de IDLE");
    }

    public override void UpdateState(UnitStateManager unit)
    {

    }
}