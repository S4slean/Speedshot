using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float baseGravity;
    [SerializeField] private float[] chargeLevelBaseSpeeds;
    [SerializeField] private float chargeLevelDuration;
    
    public bool IsGrabbed { get; private set; }
    public Character Grabber { get; private set; }
    private int chargeLevel = 0;
    public int ChargeLevel
    {
        get => chargeLevel;
        private set
        {
            chargeLevel = value > 0? value : 0;

            if (chargeLevelDecreaseCoroutine != null)
                StopCoroutine(chargeLevelDecreaseCoroutine);

            if (chargeLevel > 0)
            {
                chargeLevelDecreaseCoroutine = StartCoroutine(ChargeLevelDecreaseCoroutine());
            }
        }
    }
    public float GravityCurrentlyApplied { get => IsGrabbed? 0f : baseGravity; }

    private Rigidbody2D _rigidbody;
    private Coroutine chargeLevelDecreaseCoroutine;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.AddForce(new Vector2(0f, -GravityCurrentlyApplied));
    }

    private void OnCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.LogError("BallPlayerHit (NotImplemented)");
        else
            Bounce(collision.GetContact(0).normal);
    }

    public void Pause(bool isPaused)
    {
        
    }

    public void SetHasGrabbed(Character grabber)
    {
        Grabber = grabber;
        gameObject.SetActive(false);
    }

    public void SetHasNotGrabbed(Vector2 releasePosition)
    {
        Grabber = null;
        transform.position = releasePosition;
        gameObject.SetActive(true);
    }

    public void ThrowBall(Vector2 throwDirection, int throwLevel)
    {
        ChargeLevel = throwLevel;
        _rigidbody.velocity = chargeLevelBaseSpeeds[throwLevel] * throwDirection;
    }

    private void Bounce(Vector2 normal)
    {
        Vector2 normalVelocity = Vector2.Dot(_rigidbody.velocity, normal) * normal;
        Vector2 tangentialVelocity = _rigidbody.velocity - normalVelocity;
        _rigidbody.velocity = - normalVelocity + tangentialVelocity;
    }

    private IEnumerator ChargeLevelDecreaseCoroutine()
    {
        yield return new WaitForSeconds(chargeLevelDuration);
        ChargeLevel -= 1;
    }
}
