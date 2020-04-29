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

    [Header("Debug")] 
    [SerializeField] private bool startWithGravity;

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

                    savedGravityState = IsSubjectToGravity;
                    IsSubjectToGravity = false;
                }
                else
                {
                    _rigidbody.velocity = savedVelocity;
                    savedVelocity = Vector2.zero;

                    IsSubjectToGravity = savedGravityState;
                    savedGravityState = false;
                }
            }
            isFreezed = value;
        }
    }
    public bool IsGrabbed { get; private set; }
    public Character Grabber { get; private set; }
    public bool IsEmpowered { get; private set; }
    public TeamEnum TeamEmpowerement { get; private set; }
    public bool IsSubjectToGravity { get; private set; }
    public float GravityCurrentlyApplied { get => (IsGrabbed || !IsSubjectToGravity) ? 0f : baseGravity; }

    private bool savedGravityState = false;
    private Vector2 savedVelocity = Vector2.zero;
    private Vector2 previousVelocity = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private Coroutine empowerementFadeCoroutine;


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
            else
                player.CatchBall();
        }
        else
            Bounce(collision.GetContact(0).normal);
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

    public void ThrowBall(Vector2 throwDirection, float throwMagnitude , TeamEnum throwerTeam)
    {
        //Enable Good Trail
        _rigidbody.velocity = throwMagnitude * throwDirection.normalized;           //throwMagnitude could be processed by the player (throwDirection => throwVelocity)
        TeamEmpowerement = throwerTeam;

        if (TeamEmpowerement >= 0)
        {
            IsEmpowered = true;
            empowerementFadeCoroutine = StartCoroutine(EmpowerFadeCoroutine());
        }
    }

    private void Bounce(Vector2 normal)
    {
        Vector2 normalVelocity = Vector2.Dot(previousVelocity, normal) * normal;
        Vector2 tangentialVelocity = previousVelocity - normalVelocity;
        _rigidbody.velocity = - bounciness * normalVelocity + (1f - friction) * tangentialVelocity;
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
}
