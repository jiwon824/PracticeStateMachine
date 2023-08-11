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

	// ������
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

	// ���� ���¿��� �ٸ� ���·� ��ȯ�� ��
	// currentState.Exit ���� ���� currentState = state �������ְ� currentState.Enter
	// => PlayerState.ChangeState()�� �����Ǿ� ����
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
