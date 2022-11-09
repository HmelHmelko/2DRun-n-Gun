using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    protected bool inputs;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool glidingInput;
    
    private bool isGrounded;
    private bool isJumping; 
    private bool coyoteTime;

    public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckCoyoteTime();

        jumpInput = _player.playerInputHandler.jumpInput;
        jumpInputStop = _player.playerInputHandler.jumpInputStop;

        CheckJumpMultiplier();

        if (isGrounded && _player.currentVelocity.y < 0.01f)
        {
            _playerStateMachine.ChangeState(_player.runState);
        }
        else if(jumpInput && _player.jumpState.CanJump())
        {
            _playerStateMachine.ChangeState(_player.jumpState);
        }
        else
        {
            _player.SetVelocityX(_playerData.movementVelocity);

            _player.animator.SetFloat("yVelocity", _player.currentVelocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                _player.SetVelocityY(_player.currentVelocity.y * _playerData.jumpVelocityMultiplier);
                isJumping = false;
            }
            else if (_player.currentVelocity.y <= 0.0f)
            {
                isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + _playerData.coyoteTime)
        {
            coyoteTime = false;
            _player.jumpState.DecreaseNumberOfJumpsLeft();
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;
    public void SetIsJumping() => isJumping = true;

}
