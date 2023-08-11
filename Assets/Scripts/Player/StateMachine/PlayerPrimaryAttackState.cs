using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
	private int comboCounter;
	private float lastTimeAttacked;
	private float comboWindow = 2;

	public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
	{
	}

	public override void Enter()
	{
		base.Enter();
		stateTimer = 0.15f;

		if (comboCounter > 2 || Time.deltaTime >= lastTimeAttacked+comboWindow)
		{
			comboCounter = 0;
		}

		Debug.Log(comboCounter);

		player.anim.SetInteger("ComboCounter", comboCounter);

		// 콤보 공격할 때 방향전환이 빨리 잘 됨
		float attackDir = player.facingDir;
		if(xInput != 0)
		{
			attackDir = xInput;
		}

		player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
	}

	public override void Exit()
	{
		base.Exit();

		player.StartCoroutine("BusyFor", 0.1f);

		comboCounter++;
		lastTimeAttacked =  Time.deltaTime;
		Debug.Log(lastTimeAttacked);

	}

	public override void Update()
	{
		base.Update();
		
		if (stateTimer < 0)
		{
			rb.velocity = new Vector2(0, 0);
		}

		if (triggerCalled)
		{
			stateMachine.ChangeState(player.idleState);
		}
	}
}
