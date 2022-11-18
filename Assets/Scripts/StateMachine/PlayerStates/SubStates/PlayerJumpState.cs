using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int numberOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        numberOfJumpsLeft = playerData.numberOfJumps;
    }
    public override void Enter()
    {
        base.Enter();
        _player.SetVelocityY(_playerData.jumpVelocity);
        _player.airState.SetIsJumping();
        isAbilityDone = true;
        numberOfJumpsLeft--;
    }
    public override void Exit()
    {
        base.Exit();
    }

    public bool CanJump()
    {
        if(numberOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetNumberOfJumps() => numberOfJumpsLeft = _playerData.numberOfJumps;

    public void DecreaseNumberOfJumpsLeft() => numberOfJumpsLeft--;
}
