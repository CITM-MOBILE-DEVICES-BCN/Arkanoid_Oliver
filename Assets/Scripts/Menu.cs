using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Singleton
    private static Menu _instance;
    public static Menu Instance => _instance;

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

    public bool Continue= false;
    public void NewGame()
    {

        SceneManager.LoadScene(1);
        Continue = false;
    }
    public void MenuScreenContinue()
    {

        SceneManager.LoadScene(1);

        Continue = true; 
    }


}
