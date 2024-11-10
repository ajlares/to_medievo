using UnityEditor;
using UnityEngine;

public class UnitIdleState : UnitBaseState
{
     public override void EnterState(UnitStateManager unit)
    {
        Debug.Log("Entrando en IDLE");
    }

    public override void ExitState(UnitStateManager unit)
    {
        Debug.Log("Saliendo de IDLE");
    }

    public override void UpdateState(UnitStateManager unit)
    {

    }
}