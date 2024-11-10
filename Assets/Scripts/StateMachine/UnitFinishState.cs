using UnityEngine;

public class UnitFinishState : UnitBaseState
{
    public override void EnterState(UnitStateManager unit)
    {
        Debug.Log("Entrando en FINISH");
    }

    public override void ExitState(UnitStateManager unit)
    {
        Debug.Log("Saliendo de FINISH");
    }

    public override void UpdateState(UnitStateManager unit)
    {
        
    }
}