using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
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

        //Proverka razvorota na 180 na buduwee, neobhodimo peredat parametr
        //_player.CheckIfShouldFlip();

        _player.SetVelocityX(_playerData.movementVelocity);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
