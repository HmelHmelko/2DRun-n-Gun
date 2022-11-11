using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float dashTime;
    private float dashZeroYVelocity = 0f;
    private float dashCooldown;
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        dashTime = playerData.dashTime; 
    }
    public override void Enter()
    {
        base.Enter();
        _player.rb2D.gravityScale = 0;
        _player.SetVelocity(_player.currentVelocity.x * _playerData.dashVelocityMultiplier, dashZeroYVelocity);
        dashCooldown = _playerData.dashCooldown;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dashTime -= Time.deltaTime;
        dashCooldown -= Time.deltaTime;
        
        if (dashTime <= 0.0f)
        {
            isAbilityDone = true;
            dashTime = _playerData.dashTime;
            _player.rb2D.gravityScale = 2;
        }
    }
    public bool CanDash()
    {
        if(Time.time >= startTime + dashCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
