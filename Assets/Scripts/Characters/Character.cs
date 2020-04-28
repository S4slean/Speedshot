using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Header("References")]
	public Transform self;
	public Rigidbody2D rb2D;
	public BoxCollider2D box2D;
	public Animator anim;

	[Header("Movement")]
	public float runSpeed = 10;
	public AnimationCurve accelerationCurve;
	public float timeToMaxSpeed = 1;
	[Range(0, 1)] public float airControl = .7f;
	private float accelerationTracker;
	private int dir;

	[Header("Deceleration")]
	[Range(0.05f, 1f)] public float decelerationTime = .3f;
	public float airDecelerationTime = 1f;

	[Header("Dodge")]
	public float dodgeDuration = 1;
	private Vector3 dodgeDirection;
	public float dodgeLength = 5f;
	public AnimationCurve dodgeCurve;
	private float dodgeTracker;

	[Header("Jump")]
	public AnimationCurve jumpCurve;
	public float jumpForce = 5;
	public float jumpMaxDuration;
	public float jumpReleaseFactor = 2f;
	private float jumpTracker;

	[Header("WallJump")]
	public float wallJumpDuration = 1;
	public AnimationCurve wallJumpVerticalCurve;
	[Range(0,1)] public float wallRejectionMaxSpeedRatio = 1;
	public float walljumpUpForce = 1.5f;
	private float wallJumpTracker;

	[Header("Gravity")]
	public float gravity = 9.81f;
	public float maxFallSpeed = 50f;

	[Header("Collisions")]
	public LayerMask collisionMask;
	public float shellThickness = 0.1f;

	[Header("Shoot")]
	public float timeToMaxShoot = 1;
	public float minShootForce = 1;
	public float maxShootForce = 10f;
	private float currentShootForce = 0;

	[Header("Slide")]
	public float slideDuration = 1.3f;
	public AnimationCurve slideCurve;
	private float slideTracker;

	[Header("AirDash")]
	public float airDashDuration = 1.2f;
	public AnimationCurve airDashCurve;
	private float airDashTracker;

	public enum WallRide { None, Right, Left };

	[Header("States")]
	public bool hasTheBall = false;
	public bool grounded = false;
	public bool jumping = false;
	public bool wallJumping = false;
	private int wallJumpDir = 1;
	public WallRide wallRide = WallRide.None;
	public bool attacking = false;
	public bool damaged = false;

	private Vector3 movementAxis;

	public void Update()
	{
		HandleInputs();
		HandleCollisions();

		MoveCharacter();
	}

	private void HandleInputs()
	{
		movementAxis = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		movementAxis.Normalize();

		if (Input.GetButtonDown("Jump"))
		{
			if (grounded)
				Jump();
			else if (!grounded && wallRide != WallRide.None)
				WallJump();
		}

		if (Input.GetButtonDown("Fire1"))
		{
			if (hasTheBall)
				Shoot();
			else
				Tackle();

		}
	}

	private void HandleCollisions()
	{
		DetectGround();
		DetectWall();
	}

	private bool DetectGround()
	{
		if (rb2D.velocity.y > 0)
		{
			grounded = false;
		}
		else
		{
			grounded = Physics2D.BoxCast(self.position, new Vector2(box2D.size.x * .95f, shellThickness), 0, Vector2.down, shellThickness, collisionMask);
		}

		return grounded;
	}

	private WallRide DetectWall()
	{
		wallRide = WallRide.None;

		if (rb2D.velocity.x >= 0)
		{
			if (Physics2D.BoxCast(self.position + new Vector3(box2D.size.x / 2, box2D.size.y / 2, 0), new Vector2(shellThickness * 1.1f, box2D.size.y * .85f), 0, Vector3.right, shellThickness, collisionMask))
			{
				wallRide = WallRide.Right;
			}
		}
		if (rb2D.velocity.x <= 0)
		{

			if (Physics2D.BoxCast(self.position + new Vector3((-box2D.size.x / 2), box2D.size.y / 2, 0), new Vector2(shellThickness * 1.1f, box2D.size.y * .85f), 0, Vector3.left, shellThickness, collisionMask))
			{
				wallRide = WallRide.Left;
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
		else if (axis.x == 0)
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
		else if (attacking)
		{
			if (!grounded)
			{
				airDashTracker += Time.deltaTime / airDashDuration;
				horizontalMovement = airDashCurve.Evaluate(airDashTracker);

				if (airDashTracker >= 1)
				{
					attacking = false;
				}
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


		accelerationTracker = Mathf.Clamp(accelerationTracker, -1, 1);
		horizontalMovement = accelerationCurve.Evaluate(Mathf.Abs(accelerationTracker)) * Mathf.Sign(accelerationTracker) * runSpeed;


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

	public void MoveCharacter()
	{
		rb2D.velocity = new Vector2(HorizontalMovement(movementAxis), VerticalMovement());
	}

	public void Shoot()
	{

	}

	public void Tackle()
	{
		attacking = true;
		airDashTracker = 0;
	}
}

