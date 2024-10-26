using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{

    #region Singleton
    private static BallsManager _instance;
    public static BallsManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {

            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

    }







    #endregion

    [SerializeField]
    private Ball ballPrefab;
    public List<Ball> Balls { get; set; }
    public Ball initialBall;
    private Rigidbody2D initiallBallRb;
    public float forceinitial = 250;


    private void Start()
    {
        InitBall();

    }

    private void Update()
    {
        if(!GameManager.Instance.IsRunning && initialBall != null)
        {
            Vector3 PaddlePosition= Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(PaddlePosition.x , PaddlePosition.y+ 1f,0);
            initialBall.transform.position = ballPosition;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                initiallBallRb.isKinematic = false;
                initiallBallRb.AddForce(new Vector2(0, forceinitial));
                GameManager.Instance.IsRunning = true;
            }


        }
       
  

    }


    private void InitBall()
    {


        Vector3 startingPosition = new(Paddle.Instance.gameObject.transform.position.x, Paddle.Instance.gameObject.transform.position.y+ 1f,0);
        initialBall = Instantiate(ballPrefab, startingPosition,Quaternion.identity);
        initiallBallRb = initialBall.GetComponent<Rigidbody2D>();


        this.Balls = new List<Ball>
        {
            initialBall


        };

    }
   
    


    public void ResetBalls()
    {
        foreach(var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }
        GameManager.Instance.IsRunning= false;
        InitBall();
    }
}
