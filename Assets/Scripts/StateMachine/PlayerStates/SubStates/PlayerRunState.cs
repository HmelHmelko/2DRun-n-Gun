public class PlayerRunState : PlayerGroundedState
{   private bool dashInput;
    public PlayerRunState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dashInput = _player.playerInputHandler.dashInput;

        if (dashInput && _player.dashState.CanDash())
        {
            _player.playerInputHandler.UseDashInput();
            _playerStateMachine.ChangeState(_player.dashState);
        }
        else if(!dashInput)
        {
            _player.SetVelocityX(_playerData.movementVelocity);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
