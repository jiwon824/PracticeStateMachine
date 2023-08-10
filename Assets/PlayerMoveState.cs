using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
	public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
		Flip();

		player.SetVelocity(xInput *player.moveSpeed, rb.velocity.y);

		if (xInput == 0)
		{
			player.stateMachine.ChangeState(player.idleState);
		}
	}
	private void Flip()
	{
		// ¿ÞÂÊ
		if (xInput < 0)
		{
			player.facingDir = -1;
			if (!player.facingRight)
			{
				player.transform.Rotate(0, 180, 0);
				player.facingRight = !player.facingRight;
			}
		}
		if (xInput > 0)
		{
			player.facingDir = 1;
			if (player.facingRight)
			{
				player.transform.Rotate(0, 180, 0);
				player.facingRight = !player.facingRight;
			}
		}
	}
}
