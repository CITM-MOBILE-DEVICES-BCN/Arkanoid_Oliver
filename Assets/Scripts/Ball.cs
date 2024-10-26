using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private float limiter = 20f;
    [SerializeField] private Rigidbody2D BallRb;

    [SerializeField] private AudioSource bounceSound;

    public static event Action<Ball> OnballDeath;

    public Vector3 Position;

    public Vector2 Velocity;

    private void Update()
    {
        VelocityLimiter();


        Position = BallRb.position;   
        Velocity = BallRb.velocity;

    }
    private void VelocityLimiter()
    {


        if (Mathf.Abs(BallRb.velocity.y) > limiter)
        {
            float limitedY = Mathf.Clamp(BallRb.velocity.y, -limiter, limiter);

            float limitedX= Mathf.Clamp(BallRb.velocity.x, -limiter, limiter);


            BallRb.velocity = Vector2.zero;
            BallRb.velocity = new Vector2(limitedX, limitedY);


    

        }

        if (Mathf.Abs(BallRb.velocity.x) > limiter)
        {
            float limitedY = Mathf.Clamp(BallRb.velocity.y, -limiter, limiter);

            float limitedX = Mathf.Clamp(BallRb.velocity.x, -limiter, limiter);


            BallRb.velocity = Vector2.zero;
            BallRb.velocity = new Vector2(limitedX, limitedY);


        
        }

    }
    public void Die()
    {
        OnballDeath?.Invoke(this);
        Destroy(gameObject, 0.5f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            bounceSound.Play(); 
        
    }

}
