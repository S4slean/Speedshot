using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Gravity Settings")]
    [SerializeField] private float baseGravity;
    [Header("Standard Bounce")]
    [SerializeField] private float bounciness;
    [SerializeField] private float friction;
    [Header("PlayerHit Bounce")]
    [SerializeField] private float horizontalPlayerHitBounceVelocity;
    [SerializeField] private float verticalPlayerHitBounceVelocity;
    [Header("Empowerement Settings")]
    [SerializeField] private float empoweredStateDuration;
    [Header("Throw Settings")]
    [SerializeField] private float throwerCollisionIgnoredDuration;
    [Header("Bumper Settings")]
    [SerializeField] [Range(0, 1)] private float bumperForceRatio;

    [Header("Debug")] 
    [SerializeField] private bool startWithGravity;

    [Header("UtilityReferences")]
    [SerializeField] private Collider2D physicsCollider;
    [SerializeField] private Collider2D catchCollider;

    private bool isFreezed;
    public bool IsFreezed 
    {
        get
        {
            return isFreezed;
        }
        set
        {
            if(value != isFreezed)
            {
                if (value == true)
                {
                    savedVelocity = _rigidbody.velocity;
                    _rigidbody.velocity = Vector2.zero; 
                }
                else
                {
                    _rigidbody.velocity = savedVelocity;
                    savedVelocity = Vector2.zero;
                }
            }
            isFreezed = value;
        }
    }
    public bool IsGrabbed { get; private set; }
    public Character Grabber { get; private set; }
    public bool IsEmpowered { get; private set; }
    public TeamEnum TeamEmpowerement { get; private set; }
    public bool IsSubjectToGravity { get; set; }
    public float GravityCurrentlyApplied { get => (IsGrabbed || !IsSubjectToGravity) ? 0f : baseGravity; }
    private Collider2D ignoredCollider;
    public Collider2D IgnoredCollider
    {
        get => ignoredCollider;
        private set
        {
            if(ignoreCollisionCoroutine != null)
            {
                StopCoroutine(IgnoreCollisionCoroutine(ignoredCollider));
            }

            if(ignoredCollider != null)
                IgnoreCollision(ignoredCollider, false);

            if (value != null)
                IgnoreCollision(value, true);

            ignoredCollider = value;
        }
    }


    private Vector2 savedVelocity = Vector2.zero;
    private Vector2 previousVelocity = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private Coroutine empowerementFadeCoroutine;
    private Coroutine ignoreCollisionCoroutine;




    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        IsSubjectToGravity = startWithGravity;
    }

    private void FixedUpdate()
    {
        previousVelocity = _rigidbody.velocity;
        _rigidbody.AddForce(new Vector2(0f, -GravityCurrentlyApplied));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character player))
        {
            if(IsEmpowered && player.team != TeamEmpowerement)
            {
                player.ReceiveDamage((int)Mathf.Sign(player.transform.position.x - transform.position.x));
                PlayerHitBounce();
            }
            //else
            //    player.CatchBall();
        }
        else
            Bounce(collision.GetContact(0).normal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character player))
        {
            if (!(IsEmpowered && player.team != TeamEmpowerement))
            {
                player.CatchBall();
            }  
        }
    }

    public void Pause(bool isPaused)
    {
        isFreezed = isPaused;
    }

    public void Restart()
    {
        _rigidbody.velocity = Vector2.zero;

        if(IsGrabbed)
        {
            IsGrabbed = false;
            Grabber = null;
        }

        if(IsEmpowered)
            StopEmpowerementState();

        if(IgnoredCollider != null)
            IgnoredCollider = null;
    }

    public void SetAsGrabbed(Character grabber)
    {
        Grabber = grabber;
        gameObject.SetActive(false);
    }

    public void SetAsNotGrabbed(Vector2 releasePosition)
    {
        Grabber = null;
        transform.position = releasePosition;
        gameObject.SetActive(true);
    }

    public void ThrowBall(Vector2 throwDirection, float throwMagnitude , Character thrower, bool shouldBeEmpowered)
    {
        //Enable Good Trail
        _rigidbody.velocity = throwMagnitude * throwDirection.normalized;
        TeamEmpowerement = (thrower!= null && shouldBeEmpowered)? thrower.team : TeamEnum.NONE;

        if (TeamEmpowerement >= 0)
        {
            IsEmpowered = true;
            empowerementFadeCoroutine = StartCoroutine(EmpowerFadeCoroutine());
        }

        if(thrower != null && thrower.gameObject.TryGetComponent<Collider2D>(out Collider2D playerCollider))
        {
            StartCoroutine(IgnoreCollisionCoroutine(playerCollider));
        }
    }

    private void Bounce(Vector2 normal)
    {
        Vector2 normalVelocity = Vector2.Dot(previousVelocity, normal) * normal;
        Vector2 tangentialVelocity = previousVelocity - normalVelocity;
        _rigidbody.velocity = - bounciness * normalVelocity + (1f - friction) * tangentialVelocity;
    }

    public void Bump(Bumper bumper)
    {
        _rigidbody.velocity += (Vector2)bumper.transform.up * bumper.bumpForce * bumperForceRatio;
    }

    private void PlayerHitBounce()
    {
        _rigidbody.velocity = new Vector2(-Mathf.Sign(previousVelocity.x) * horizontalPlayerHitBounceVelocity, verticalPlayerHitBounceVelocity);
    }

    private IEnumerator EmpowerFadeCoroutine()
    {
        yield return new WaitForSeconds(empoweredStateDuration);
        StopEmpowerementState();
    }

    private void StopEmpowerementState()
    {
        StopCoroutine(empowerementFadeCoroutine);
        IsEmpowered = false;
        TeamEmpowerement = TeamEnum.NONE;
    }

    private IEnumerator IgnoreCollisionCoroutine(Collider2D otherCollider)
    {
        IgnoredCollider = otherCollider;

        yield return new WaitForSeconds(throwerCollisionIgnoredDuration);

        IgnoredCollider = null;
    }

    private void IgnoreCollision(Collider2D otherCollider, bool shouldBeIgnored)
    {
        Physics2D.IgnoreCollision(physicsCollider, otherCollider, shouldBeIgnored);
        Physics2D.IgnoreCollision(catchCollider, otherCollider, shouldBeIgnored);
    }
}
