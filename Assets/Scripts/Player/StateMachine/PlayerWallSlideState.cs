using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
	public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

		// 스페이스바를 누르면 wallJump가 되도록
		if(Input.GetKeyDown(KeyCode.Space))
		{
			stateMachine.ChangeState(player.wallJumpState);
			return;
		}

		if ((xInput != 0) && (player.facingDir != xInput))
		{
			stateMachine.ChangeState(player.idleState);
		}
		
		// 아래 방향키가 눌렸으면 빨리 내려가도록
		if(yInput < 0)
			rb.velocity = new Vector2 (0, rb.velocity.y);
		// 그게 아니면 좀 천천히 내려가도록
		else
			rb.velocity = new Vector2 (0, rb.velocity.y*0.7f); // 70%의 속도로 내려오도록 설정

		if (player.IsGroundDetected())
		{
			stateMachine.ChangeState(player.idleState);
		}
	}

}
