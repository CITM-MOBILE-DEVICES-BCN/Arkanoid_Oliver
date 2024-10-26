using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private Ball ball;
    public bool IsActiveAuto;
    private bool IsActivePause;
    public TMPro.TextMeshProUGUI livestext;



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
    public int Avaiblelives = 3;

    public bool IsRunning { get; set; }
    public  int lives { get; set; }

    public GameObject gameOverScreen;

    public GameObject gameWinScreen;

    public GameObject isActiveScreen;

    public GameObject Pause;

    public GameObject highscore;



    private void Start()
    {

        IsActiveAuto = false;
        IsActiveAuto = false;
       

        gameOverScreen.SetActive(false);

        gameWinScreen.SetActive(false);

        isActiveScreen.SetActive(false);

        Pause.SetActive(false);

        highscore.SetActive(false);


        if (Menu.Instance.Continue)
        {


            Loadlives();



        }
        else
        {


            this.lives = this.Avaiblelives;

        }

    }


    private void Update()
    {
        OnBallDeath();
        OnGameWon();
        AutoModeButton();
        PauseActivation();
        livestext.text = "Lives: " + lives.ToString();
        Savelives();

    }

    private void OnBallDeath()
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
           
            this.lives--;
           
            if (this.lives < 1) {
                IsRunning = false;
                gameOverScreen.SetActive(true);
                highscore.SetActive(true);
                lives = 0;
            }
            else
            {

                BallsManager.Instance.ResetBalls();
                IsRunning = false;


            }

        }


    }
    private void Savelives()
    {
        PlayerPrefs.SetInt("lives", lives);
        PlayerPrefs.Save();
    }

    private void Loadlives()
    {
        lives = PlayerPrefs.GetInt("lives", 0);


    }

    private void OnGameWon()
    {
       
        if (LevelManager.Instance.DidWeFinishedThegame){

            IsRunning = false;
            gameWinScreen.SetActive(true);

            highscore.SetActive(true);

            Debug.Log("yourwinning");

        }
            


    }
    public void MenuScreenActivated()
    {
        IsRunning = false;

        SceneManager.LoadScene("Menu");


    }


    public  void RestartGame()
    {

        SceneManager.LoadScene(1);


    }

    public void PauseActivation()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsActivePause)
            {
                IsActivePause = false;
                Pause.SetActive(false);
                Time.timeScale = 1.0f;

            }
            else
            {

                IsActivePause = true;
              
                Pause.SetActive(true);
                Time.timeScale = 0.0f;

            }




        }
    }


    public void AutoModeButton()
    {


       

        if ( Input.GetKeyDown(KeyCode.T))
        {
            if (IsActiveAuto)
            {
                IsActiveAuto = false;
                isActiveScreen.SetActive(false);

            }
            else
            {
              
                IsActiveAuto = true;
                Debug.Log("Siguiendo");
                isActiveScreen.SetActive(true);
            }
          

        }
        if(IsActiveAuto && GameManager.Instance.IsRunning)
        {

           Vector3 ballposition = BallsManager.Instance.initialBall.Position;
                Paddle.Instance.transform.position = new Vector3(ballposition.x, Paddle.Instance.transform.position.y, Paddle.Instance.transform.position.z);



            


        }

    }





}
