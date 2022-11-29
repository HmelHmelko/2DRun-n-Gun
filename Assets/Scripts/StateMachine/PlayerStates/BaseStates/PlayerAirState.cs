using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerAirState : PlayerState
{
    //Inputs
    protected bool inputs;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool glidingInput;
    private bool dashInput;

    //Checks
    private bool isGrounded;
    private bool isJumping;
    public bool isGliding { get; private set; }
    private bool coyoteTime;

    public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = _player.IsGrounded;
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
        dashInput = _player.playerInputHandler.dashInput;
        glidingInput = _player.playerInputHandler.glidingInput;

        CheckJumpMultiplier();


        if (isGrounded && _player.currentVelocity.y < 0f)
        {
            _playerStateMachine.ChangeState(_player.runState);
        }
        else if(isGrounded && jumpInput && _player.jumpState.CanJump())
        {
            _player.playerInputHandler.UseJumpInput();
            _playerStateMachine.ChangeState(_player.jumpState);
        }
        else if(dashInput && _player.dashState.CanDash())
        {
            _player.playerInputHandler.UseDashInput();
            _playerStateMachine.ChangeState(_player.dashState);
        }
        else if(glidingInput && !isGrounded && _player.currentVelocity.y < 0.01f)
        {
            SetIsGliding();
            _playerStateMachine.ChangeState(_player.glidingState);
        }
        else
        {
            isGliding = false;
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
    public void SetIsGliding() => isGliding = true;

}
