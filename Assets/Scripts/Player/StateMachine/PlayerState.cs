using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
	protected PlayerStateMachine stateMachine;
	protected Player player;

	protected Rigidbody2D rb;

	protected float xInput;
	protected float yInput;

	private string animBoolName;

	protected float stateTimer;
	protected bool triggerCalled;

	// 생성자
	public PlayerState (Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
	{
		player = _player;
		stateMachine = _stateMachine;
		animBoolName = _animBoolName;
	}

	public virtual void Update()
	{
		xInput = Input.GetAxisRaw("Horizontal");
		yInput = Input.GetAxisRaw("Vertical");

		stateTimer -= Time.deltaTime;

		player.anim.SetFloat("yVelocity", rb.velocity.y);
		// Debug.Log("PlayerState.Update(): " + animBoolName);
	}

	// 현재 상태에서 다른 상태로 전환할 때
	// currentState.Exit 해준 다음 currentState = state 변경해주고 currentState.Enter
	// => PlayerState.ChangeState()에 구현되어 있음
	public virtual void Enter()
	{
		//Debug.Log("PlayerState.Enter(): " + animBoolName);
		player.anim.SetBool(animBoolName, true);
		rb = player.rb;
		triggerCalled = false;
	}
	public virtual void Exit () 
	{
		//Debug.Log("PlayerState.Exit(): " + animBoolName);
		player.anim.SetBool(animBoolName, false);
	}
	public virtual void AnimationFinishTrigger()
	{
		triggerCalled = true;

	}
}
