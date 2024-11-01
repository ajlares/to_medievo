using UnityEngine;

public abstract class UnitBaseState
{
    public abstract void EnterState(UnitStateManager unit);

    public abstract void ExitState(UnitStateManager unit);

    public abstract void UpdateState(UnitStateManager unit);

}
