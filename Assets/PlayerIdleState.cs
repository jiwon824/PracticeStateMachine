using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
	// 생성자 만들어야 함                                                                                                                                                                                                                                                                                                                                                                                                                   →→→→→→→→→→→→→         
	// 여기서 만든 게 부모의 생성자로 타고 올라감 <-?
	public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
	{
		
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		base.Update();
		if(xInput != 0)
		{
			player.stateMachine.ChangeState(player.moveState);
		}
	}
}
