using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Move Info")]
	public float moveSpeed = 12f;
	public float jumpForce = 12f;

	[Header("Dash Info")]
	public float dashSpeed = 18f;
	public float dashTime;
	public float dashTimer;

	public int facingDir; // 1, -1
	public bool facingRight; // 오른쪽을 보고 있는지?

	[Header("Collision Info")]
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundCheckDist;
//	[SerializeField] private LayerMask whatIsWall;
	[SerializeField] private Transform wallCheck;
	[SerializeField] private float wallCheckDist;

	
	#region Component
	public Animator anim { get; private set; }
	public Rigidbody2D rb { get; private set; }
	#endregion

	#region State
	// 캐릭터의 상태를 설정하고, 저장
	public PlayerStateMachine stateMachine { get; private set; }
    
    // 캐릭터의 상태
	public PlayerGroundedState groundedState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

	public PlayerAirState airState { get; private set; }
	public PlayerJumpState jumpState { get; private set; }
	#endregion

	private void Awake()
	{
		stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
		jumpState = new PlayerJumpState(this, stateMachine, "Jump");
		airState = new PlayerAirState(this, stateMachine, "Jump"); //블랜드 트리 쓸 거라서 같은 Jump로 씀

	}

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		stateMachine.Initialize(idleState); // 처음 상태를 idle 상태로 만들어줌

	}
	private void Update()
	{
		// idle 상태가 되면 idle의 Update가 돌아가고, move 상태가 되면 move의 Update가 돌아감
		stateMachine.currentState.Update();

	}

	// 함수가 한 줄일 때는 '=>'를 이용해서 코드를 짤 수도 있다. 
	public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDist, whatIsGround);


	public void SetVelocity(float _xVelocity, float _yVelocity)
	{
		rb.velocity = new Vector2(_xVelocity, _yVelocity);
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(groundCheck.position,
			new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDist));
		
		Gizmos.DrawLine(wallCheck.position,
			new Vector3(wallCheck.position.x + wallCheckDist, wallCheck.position.y));
	}
}
