using UnityEngine;

public class PlayerRunState : PlayerState
{   
    //Inputs
    private bool dashInput;
    private bool jumpInput;
    
    //Checks
    private bool isGrounded;
    public PlayerRunState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = _player.isGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        _player.jumpState.ResetNumberOfJumps();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dashInput = _player.playerInputHandler.dashInput;
        jumpInput = _player.playerInputHandler.jumpInput;


        if (jumpInput && _player.jumpState.CanJump())
        {
            _player.playerInputHandler.UseJumpInput();
            _playerStateMachine.ChangeState(_player.jumpState);
        }
        else if (dashInput && _player.dashState.CanDash())
        {
            _player.playerInputHandler.UseDashInput();
            _playerStateMachine.ChangeState(_player.dashState);
        }
        else if(!isGrounded)
        {
            _player.airState.StartCoyoteTime();
            _playerStateMachine.ChangeState(_player.airState);
        }
        else
        {
            _player.SetVelocityX(_playerData.movementVelocity);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
