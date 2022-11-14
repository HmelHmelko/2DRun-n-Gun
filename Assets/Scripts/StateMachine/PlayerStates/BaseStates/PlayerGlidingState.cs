using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGlidingState : PlayerState
{
    //Inputs
    private bool glidingInput;
    private bool dashInput;

    //Checks
    private bool isGlide;
    private bool isGrounded;
    private float glideVelocity;
    public PlayerGlidingState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        glideVelocity = playerData.glideVelocity;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = _player.isGrounded();
        isGlide = _player.airState.isGliding;
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
        glidingInput = _player.playerInputHandler.glidingInput;

        if (isGrounded && _player.currentVelocity.y < 0.01f)
        {
            _playerStateMachine.ChangeState(_player.runState);
        }
        else if (!glidingInput && _player.currentVelocity.y <= 0.0f)
        {
            _playerStateMachine.ChangeState(_player.airState);
        }
        else if (dashInput && _player.dashState.CanDash())
        {
            _player.playerInputHandler.UseDashInput();
            _playerStateMachine.ChangeState(_player.dashState);
        }
        else
        {
            _player.SetVelocityX(_playerData.movementVelocity);
            _player.SetVelocityY(-(glideVelocity));
            _player.animator.SetBool("Glide",isGlide);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
