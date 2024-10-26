using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class ScoreManager : MonoBehaviour
{
    #region Singleton
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

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

    public int score;
    public int highscore;

    public TMPro.TextMeshProUGUI scoreText;


    public TMPro.TextMeshProUGUI highScore;








    private void Start()
    {


        UpdateHighScoreText();
        LoadHighScore();
        if (Menu.Instance.Continue)
        {

           
            LoadScore();
           


        }
        else
        {
          
            ResetScore();
           

        }
        

    }


    private void Update()
    {
        GetHighScore();

    }


    public void AddScore(int points)
    {
        score += points; 
        UpdateScoreText(); 
         SaveScore();
    }

    public void ResetScore()
    {
        score = 0; 
        UpdateScoreText();
       
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();

      
    }


    private void SaveScore()
    {
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.Save(); 
    }
  
    private void LoadScore()
    {
        score = PlayerPrefs.GetInt("PlayerScore", 0);

        
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("PlayerHighScore", score);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highscore = PlayerPrefs.GetInt("PlayerHighScore", 0);


    }


    private void GetHighScore()
    {
        if (highscore <= score)
        {

            highscore = score;
        }

        UpdateHighScoreText();
        SaveHighScore();
    }

    private void UpdateHighScoreText()
    {
        highScore.text = "Highscore: " + highscore.ToString();


    }




}