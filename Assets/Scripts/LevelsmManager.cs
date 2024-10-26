using UnityEngine;
using UnityEngine.SceneManagement; // Only if you're using scenes for levels

public class LevelManager : MonoBehaviour
{


    #region Singleton
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

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

    public GameObject[] Levels; // Assign your first level GameObject in the Inspector

    public int levelActual = 0;
    private GameObject currentLevel;
    public GameObject brickPrefab;

    public bool DidWeFinishedThegame = false;

    void Start()
    {
      

        //if (Menu.Instance.Continue)
        //{
        //    LoadBricks();
        //}
        currentLevel = Instantiate(Levels[levelActual]);
        if (Menu.Instance.Continue)
        {
            LoadLevel();
            Destroy(currentLevel);
            currentLevel = Instantiate((Levels[levelActual]));


        }



    }

    void Update()
    {
        CheckBricks();

        SaveLevel();

    }

    public void CheckBricks()
    {
        // Find all active bricks with the tag "Brick"
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        // If no bricks remain, move to the next level
        if (bricks.Length == 0)
        {
            AdvanceToNextLevel();
        }

     

    }

    private void AdvanceToNextLevel()
    {


        if (levelActual >= Levels.Length - 1)
        { 

            DidWeFinishedThegame = true;
            ScoreManager.Instance.ResetScore();

            return; 

        }




        BallsManager.Instance.ResetBalls();
        levelActual++;



        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }


        currentLevel = Instantiate((Levels[levelActual]));


    }
    private void SaveLevel()
    {
        PlayerPrefs.SetInt("ActualLevel", levelActual);
        PlayerPrefs.Save();
    }

    private void LoadLevel()
    {
        levelActual = PlayerPrefs.GetInt("ActualLevel", 0);

    }



        //public void CheckBricksToSave()
        //{

        //    GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");


        //    foreach (GameObject brickObject in bricks)
        //    {
        //        Brick brick = brickObject.GetComponent<Brick>();
        //        if (brick != null)
        //        {
        //            SaveBrickState(brick); 
        //        }
        //    }
        //}


        //private void SaveBrickState(Brick brick)
        //{
        //    string brickID = brick.BrickID; // Ensure each brick has a unique ID

        //    PlayerPrefs.SetInt(brickID + "_Hitpoints", brick.Hitpoints);
        //    PlayerPrefs.SetFloat(brickID + "_PosX", brick.transform.position.x);
        //    PlayerPrefs.SetFloat(brickID + "_PosY", brick.transform.position.y);
        //    PlayerPrefs.SetFloat(brickID + "_PosZ", brick.transform.position.z);

        //    PlayerPrefs.Save();
        //}


        //public void LoadBricks()
        //{


        //    string savedBrickIDs = PlayerPrefs.GetString("SavedBrickIDs", "");
        //    if (string.IsNullOrEmpty(savedBrickIDs))
        //    {
        //        Debug.Log("No saved bricks to load.");
        //        return;
        //    }


        //    string[] brickIDs = savedBrickIDs.Split(',');

        //    foreach (string brickID in brickIDs)
        //    {


        //        int hitpoints = PlayerPrefs.GetInt(brickID + "_Hitpoints", 1);
        //        float posX = PlayerPrefs.GetFloat(brickID + "_PosX", 0);
        //        float posY = PlayerPrefs.GetFloat(brickID + "_PosY", 0);
        //        float posZ = PlayerPrefs.GetFloat(brickID + "_PosZ", 0);



        //        GameObject newBrick = Instantiate(brickPrefab, new Vector3(posX, posY, posZ), Quaternion.identity);
        //        Brick brick = newBrick.GetComponent<Brick>();


        //        brick.BrickID = brickID;
        //        brick.Hitpoints = hitpoints;

        //    }
        //}





        //public void ReloadLevels()
        //{
        //    Destroy(currentLevel);
        //    levelActual= 0;
        //    currentLevel = Instantiate((Levels[levelActual]));

        //}

    }
