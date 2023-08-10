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
	public bool facingRight; // �������� ���� �ִ���?

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
	// ĳ������ ���¸� �����ϰ�, ����
	public PlayerStateMachine stateMachine { get; private set; }
    
    // ĳ������ ����
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
		airState = new PlayerAirState(this, stateMachine, "Jump"); //���� Ʈ�� �� �Ŷ� ���� Jump�� ��

	}

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		stateMachine.Initialize(idleState); // ó�� ���¸� idle ���·� �������

	}
	private void Update()
	{
		// idle ���°� �Ǹ� idle�� Update�� ���ư���, move ���°� �Ǹ� move�� Update�� ���ư�
		stateMachine.currentState.Update();

	}

	// �Լ��� �� ���� ���� '=>'�� �̿��ؼ� �ڵ带 © ���� �ִ�. 
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
