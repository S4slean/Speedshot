using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
	[Header("References")]
	public Transform self;
	public Rigidbody2D rb2D;
	public BoxCollider2D box2D;
	public Animator anim;
	public Ball ball;

	[Header("Movement")]
	public float runSpeed = 10;
	public AnimationCurve accelerationCurve;
	public float timeToMaxSpeed = 1;
	[Range(0, 1)] public float airControl = .7f;
	private float accelerationTracker;
	private int dir = 1;

	[Header("Deceleration")]
	[Range(0.05f, 1f)] public float decelerationTime = .3f;
	public float airDecelerationTime = 1f;

	[Header("Dodge")]
	public float dodgeDuration = 1;
	private int dodgeDirection;
	public float dodgeLength = 5f;
	public AnimationCurve dodgeCurve;
	private float dodgeTracker;
	public float bufferDuration = .4f;
	private bool leftBuffer = false;
	private bool rightBuffer = false;
	private float bufferTracker = 0;

	[Header("Jump")]
	public AnimationCurve jumpCurve;
	public float jumpForce = 5;
	public float jumpMaxDuration;
	public float jumpReleaseFactor = 2f;
	private float jumpTracker;
	private Bumper triggeredBumper;

	[Header("WallJump")]
	public float wallJumpDuration = 1;
	public AnimationCurve wallJumpVerticalCurve;
	[Range(0, 1)] public float wallRejectionMaxSpeedRatio = 1;
	public float walljumpUpForce = 1.5f;
	private float wallJumpTracker;

	[Header("Gravity")]
	public float gravity = 9.81f;
	public float maxFallSpeed = 50f;

	[Header("Collisions")]
	public LayerMask collisionMask;
	public LayerMask wallSlideMask;
	public float shellThickness = 0.1f;

	[Header("Shoot")]
	public float shootForce = 5;
	public float ballDistanceFromPlayer = 2;

	[Header("Slide")]
	public float slideSpeed = 5;
	public float slideDuration = 1.3f;
	public AnimationCurve slideCurve;
	private float slideTracker;
	private float attackDir = 1;

	[Header("AirDash")]
	public float airDashSpeed = 5;
	public float airDashDuration = 1.2f;
	public AnimationCurve airDashCurve;
	private float airDashTracker;

	public enum WallRide { None, Right, Left };
	public enum Attack { None, Slide, AirDash }

	[Header("Damage")]
	public float invulnerabiltyDuration = .7f;
	public AnimationCurve knockBackCurve;
	public float knockBackDuration = .5f;
	[Range(0, 1)] public float knockBackForceMaxSpeedRatio = 1f;
	public LayerMask damageMask;
	public float damageRange = .1f;
	private float knockbackTracker;

	[Header("States")]
	public bool hasTheBall = false;
	public bool grounded = false;
	public bool jumping = false;
	public bool wallJumping = false;
	private int wallJumpDir = 1;
	public bool canAttack = true;
	public WallRide wallRide = WallRide.None;
	public Attack attack = Attack.None;
	public bool attacking = false;
	public bool dodging = false;
	public bool damaged = false;
	public bool catching = false;
	public bool bumping = false;
	public TeamEnum team = TeamEnum.TEAM1;

	[Header("Debug")]
	public bool debugMode;

	private Vector3 movementAxis;
	private CharacterInputHandler characterInputHandler;

	public void Start()
	{
		ball = GameObject.FindObjectOfType<Ball>();

		characterInputHandler = GetComponent<CharacterInputHandler>();

		//Debug
		if(debugMode)
			characterInputHandler.SetPlayerInput(GetComponent<PlayerInput>());
	}

	public void Update()
	{
		//HandleInputs();
		NewHandleInputs();
		HandleCollisions();

		MoveCharacter();
		UpdateAnims();
	}

	//private void HandleInputs()
	//{
	//	movementAxis = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
	//	movementAxis.Normalize();

	//	HandleDodgeBuffers();

	//	if (Input.GetButtonDown("Jump"))
	//	{
	//		if (grounded)
	//			Jump();
	//		else if (!grounded && wallRide != WallRide.None)
	//			WallJump();
	//	}

	//	if (Input.GetButtonDown("Fire1"))
	//	{
	//		if (hasTheBall)
	//			Shoot();
	//		else if (canAttack)
	//		{
	//			if (grounded)
	//				Slide();

	//			else
	//				Tackle();
	//		}

	//	}
	//}

	//private void HandleDodgeBuffers()
	//{
	//	if (Input.GetButtonDown("Left"))
	//	{
	//		rightBuffer = false;
	//		if (leftBuffer)
	//		{
	//			bufferTracker = 0;
	//			leftBuffer = false;
	//			Dodge(-1);
	//		}
	//		else
	//		{
	//			leftBuffer = true;
	//			bufferTracker = bufferDuration;
	//		}
	//	}
	//	if (Input.GetButtonDown("Right"))
	//	{
	//		leftBuffer = false;
	//		if (rightBuffer)
	//		{
	//			bufferTracker = 0;
	//			rightBuffer = false;
	//			Dodge(1);
	//		}
	//		else
	//		{
	//			rightBuffer = true;
	//			bufferTracker = bufferDuration;
	//		}
	//	}

	//	if (bufferTracker > 0)
	//	{
	//		bufferTracker -= Time.deltaTime;
	//	}
	//	if (bufferTracker <= 0)
	//	{
	//		bufferTracker = 0;
	//		rightBuffer = false;
	//		leftBuffer = false;
	//	}
	//}

	private void NewHandleInputs()
	{
		movementAxis = characterInputHandler.MovementAxis;
		movementAxis.Normalize();

		NewHandleDodgeBuffers();

		if (characterInputHandler.JumpButtonDown)
		{
			if (grounded)
				Jump();
			else if (!grounded && wallRide != WallRide.None)
				WallJump();
		}

		if (characterInputHandler.ActionButtonDown)
		{
			if (hasTheBall)
				Shoot();
			else if (canAttack)
			{
				if (grounded)
					Slide();

				else
					Tackle();
			}

		}
	}

	private void NewHandleDodgeBuffers()
	{
		if (characterInputHandler.LeftButtonDown)
		{
			rightBuffer = false;
			if (leftBuffer)
			{
				bufferTracker = 0;
				leftBuffer = false;
				Dodge(-1);
			}
			else
			{
				leftBuffer = true;
				bufferTracker = bufferDuration;
			}
		}
		if (characterInputHandler.RightButtonDown)
		{
			leftBuffer = false;
			if (rightBuffer)
			{
				bufferTracker = 0;
				rightBuffer = false;
				Dodge(1);
			}
			else
			{
				rightBuffer = true;
				bufferTracker = bufferDuration;
			}
		}

		if (bufferTracker > 0)
		{
			bufferTracker -= Time.deltaTime;
		}
		if (bufferTracker <= 0)
		{
			bufferTracker = 0;
			rightBuffer = false;
			leftBuffer = false;
		}
	}

	private void HandleCollisions()
	{
		DetectGround();
		DetectWall();

		if (attacking)
		{
			RaycastHit2D hit =  Physics2D.BoxCast(self.position + new Vector3(box2D.size.x / 2, box2D.size.y / 2, 0), new Vector2(shellThickness * 1.1f, box2D.size.y * .85f), 0, Vector3.right, shellThickness, damageMask);
		}
	}

	private bool DetectGround()
	{
		if (rb2D.velocity.y > 0)
		{
			grounded = false;
		}
		else
		{
			grounded = Physics2D.BoxCast(self.position, new Vector2(box2D.size.x * .85f, shellThickness), 0, Vector2.down, shellThickness, collisionMask);
		}

		if (grounded && !attacking)
		{
			canAttack = true;
			rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
		}

		return grounded;
	}

	private WallRide DetectWall()
	{
		wallRide = WallRide.None;

		if (rb2D.velocity.x >= 0)
		{
			if (Physics2D.BoxCast(self.position + new Vector3(box2D.size.x / 2, box2D.size.y / 2, 0), new Vector2(shellThickness * 1.1f, box2D.size.y * .85f), 0, Vector3.right, shellThickness, wallSlideMask))
			{
				wallRide = WallRide.Right;
				canAttack = true;
			}
		}
		if (rb2D.velocity.x <= 0)
		{

			if (Physics2D.BoxCast(self.position + new Vector3((-box2D.size.x / 2), box2D.size.y / 2, 0), new Vector2(shellThickness * 1.1f, box2D.size.y * .85f), 0, Vector3.left, shellThickness, wallSlideMask))
			{
				wallRide = WallRide.Left;
				canAttack = true;
			}
		}


		return wallRide;
	}

	public float HorizontalMovement(Vector3 axis)
	{
		float horizontalMovement = 0;


		if (axis.x > 0 && wallRide == WallRide.Right || axis.x < 0 && wallRide == WallRide.Left)
		{
			if (!wallJumping)
				accelerationTracker = 0;
		}
		else if (axis.x == 0 || damaged)
		{
			if (accelerationTracker > 0)
			{
				if (grounded)
				{
					accelerationTracker -= Time.deltaTime / decelerationTime;
				}
				else
				{
					accelerationTracker -= Time.deltaTime / airDecelerationTime;
				}
				accelerationTracker = Mathf.Clamp(accelerationTracker, 0, 1);
			}
			else if (accelerationTracker < 0)
			{
				if (grounded)
				{
					accelerationTracker += Time.deltaTime / decelerationTime;
				}
				else
				{
					accelerationTracker += Time.deltaTime / airDecelerationTime;
				}
				accelerationTracker = Mathf.Clamp(accelerationTracker, -1, 0);
			}
		}
		else
		{
			if (grounded)
			{
				accelerationTracker = Mathf.Abs(accelerationTracker) * axis.x;
				accelerationTracker += Time.deltaTime * Mathf.Sign(axis.x) / timeToMaxSpeed;
			}
			else
			{
				accelerationTracker += Time.deltaTime * axis.x * airControl / timeToMaxSpeed;
			}
		}

		if (attacking)
		{
			switch (attack)
			{
				case Attack.AirDash:
					airDashTracker += Time.deltaTime / airDashDuration;
					horizontalMovement = airDashCurve.Evaluate(airDashTracker) * airDashSpeed * attackDir;

					if (airDashTracker >= 1)
					{
						attacking = false;
						attack = Attack.None;
						airDashTracker = 0;
					}
					break;

				case Attack.Slide:
					slideTracker += Time.deltaTime / slideDuration;
					horizontalMovement = slideCurve.Evaluate(slideTracker) * slideSpeed * attackDir;

					if (slideTracker >= 1)
					{
						attacking = false;
						attack = Attack.None;
						slideTracker = 0;
					}
					break;

			}
		}
		else if (dodging)
		{
			dodgeTracker += Time.deltaTime / dodgeDuration;
			horizontalMovement = dodgeCurve.Evaluate(dodgeTracker) * dodgeDirection * dodgeLength;
			if (dodgeTracker >= 1)
			{
				dodging = false;
				dodgeTracker = 0;
			}
		}
		else
		{
			accelerationTracker = Mathf.Clamp(accelerationTracker, -1, 1);
			horizontalMovement = accelerationCurve.Evaluate(Mathf.Abs(accelerationTracker)) * Mathf.Sign(accelerationTracker) * runSpeed;
		}

		if (horizontalMovement > 0) dir = 1;
		else if (horizontalMovement < 0) dir = -1;


		return horizontalMovement;
	}

	public float VerticalMovement()
	{
		float verticalMovement;

		if (jumping)
		{
			jumpTracker += Time.deltaTime * (Input.GetButton("Jump") ? 1 : jumpReleaseFactor) / jumpMaxDuration;
			if (jumpTracker >= 1)
			{
				jumpTracker = 0;
				jumping = false;
				verticalMovement = rb2D.velocity.y;
			}
			else
			{
				verticalMovement = jumpCurve.Evaluate(jumpTracker) * jumpForce;
			}
		}
		else if (wallJumping)
		{
			wallJumpTracker += Time.deltaTime * (Input.GetButton("Jump") ? 1 : jumpReleaseFactor) / wallJumpDuration;
			if (wallJumpTracker >= 1)
			{
				wallJumpTracker = 0;
				wallJumping = false;
				verticalMovement = rb2D.velocity.y;
			}
			else
			{

				verticalMovement = wallJumpVerticalCurve.Evaluate(wallJumpTracker) * walljumpUpForce;
			}
		}
		else if (attacking || dodging)
		{
			verticalMovement = 0;
		}
		else if (damaged)
		{
			knockbackTracker += Time.deltaTime / knockBackDuration;
			verticalMovement = knockBackCurve.Evaluate(knockbackTracker);
			if (knockbackTracker >= 1)
			{
				damaged = false;
				knockbackTracker = 0;
			}
		}
		else if (bumping)
		{
			triggeredBumper.bumpTracker += Time.deltaTime * triggeredBumper.bumpReleaseFactor / triggeredBumper.bumpMaxDuration;
			if (triggeredBumper.bumpTracker >= 1)
			{
				triggeredBumper.bumpTracker = 0;
				bumping = false;
				triggeredBumper = null;
				verticalMovement = rb2D.velocity.y;
			}
			else
			{
				verticalMovement = triggeredBumper.bumpCurve.Evaluate(triggeredBumper.bumpTracker) * triggeredBumper.bumpForce;
			}
		}
		else
		{
			verticalMovement = rb2D.velocity.y;
			verticalMovement -= gravity * Time.deltaTime;
			verticalMovement = Mathf.Clamp(verticalMovement, -maxFallSpeed, jumpForce);
		}

		if (grounded && verticalMovement < 0)
		{
			verticalMovement = 0;
		}

		return verticalMovement;
	}

	public void Jump()
	{
		jumping = true;
		jumpTracker = 0;
		anim.Play("Jump");
	}

	public void WallJump()
	{
		jumping = false;
		wallJumping = true;
		wallJumpTracker = 0;

		if (wallRide == WallRide.Right)
			wallJumpDir = -1;
		else if (wallRide == WallRide.Left)
			wallJumpDir = 1;
		else
			wallJumpDir = 0;

		accelerationTracker = wallJumpDir * wallRejectionMaxSpeedRatio;

	}

	public void InterruptJumps()
	{
		jumping = false;
		jumpTracker = 0;
		wallJumping = false;
		wallJumpTracker = 0;
	}
	public void MoveCharacter()
	{
		rb2D.velocity = new Vector2(HorizontalMovement(movementAxis), VerticalMovement());
	}

	public void Shoot()
	{
		if (Physics2D.Raycast((Vector2)transform.position + new Vector2(0, box2D.size.y / 2), ((movementAxis == Vector3.zero) ? Vector2.right * dir : (Vector2)movementAxis),ballDistanceFromPlayer, collisionMask)) return;
		ball.SetAsNotGrabbed((Vector2)transform.position + new Vector2(0, box2D.size.y / 2) + ((movementAxis == Vector3.zero) ? Vector2.right * dir : (Vector2)movementAxis) * ballDistanceFromPlayer);
		ball.ThrowBall(((movementAxis == Vector3.zero) ? Vector2.right * dir : (Vector2)movementAxis),shootForce, this, true);
		hasTheBall = false;
	}

	public void Tackle()
	{
		canAttack = false;
		jumping = false;
		wallJumping = false;
		attacking = true;
		airDashTracker = 0;
		attack = Attack.AirDash;
		anim.Play("AirDash");
		attackDir = movementAxis.x != 0 ? Mathf.Sign(movementAxis.x) : dir;
		if (wallRide == WallRide.Right) attackDir = -1;
		else if (wallRide == WallRide.Left) attackDir = 1;

	}

	public void Slide()
	{
		canAttack = false;
		jumping = false;
		wallJumping = false;
		attacking = true;
		slideTracker = 0;
		attack = Attack.Slide;
		anim.Play("Slide");
		attackDir = movementAxis.x != 0 ? Mathf.Sign(movementAxis.x) : dir;
	}

	public void Dodge(int dodgeDir)
	{
		dodging = true;
		dodgeTracker = 0;
		dodgeDirection = dodgeDir;
	}

	public void ReceiveDamage(int dmgDir)
	{
		if (damaged || dodging) return;

		damaged = true;
		knockbackTracker = 0;
		accelerationTracker = dmgDir * knockBackForceMaxSpeedRatio;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		InterruptJumps();
	}

	public void CatchBall()
	{
		hasTheBall = true;
		ball.SetAsGrabbed(this);
	}

	public void Bump(Bumper bumper)
	{
		triggeredBumper = bumper;
		triggeredBumper.bumpTracker = 0f;
		bumping = true;
	}

	public void UpdateAnims()
	{
		anim.SetBool("grounded", grounded);
		anim.SetBool("running", rb2D.velocity.x != 0 ? true: false);
		anim.SetBool("wallSliding", wallRide == WallRide.None ? false : true);
		transform.localScale = new Vector3(dir, 1, 1);

		if (wallRide == WallRide.Right)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if(wallRide == WallRide.Left)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
}

