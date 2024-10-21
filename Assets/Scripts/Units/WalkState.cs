public class WalkState : State
{
    Unit UnitController;
    public WalkState(Unit _unitController)
    {
        UnitController = _unitController;
    }
    public override void Enter()
    {
        base.Enter();
        UnitController.unitController.ChangeState(UnitController.walkState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}