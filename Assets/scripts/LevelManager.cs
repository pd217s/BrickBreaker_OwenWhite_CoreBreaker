using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int level; //didnt realize I could format like this till 11:40pm 10/15/2026 do modt of the code wont look like this
    [SerializeField] heartsScript heartsScript;
    [SerializeField] string levelOneName;
    [SerializeField] string levelTwoName;
    [SerializeField] SceneRandomizer sceneRandomizer;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject scoreDisplay;
    [SerializeField] private TextMeshProUGUI levelDisplay;
    [SerializeField] private TextMeshProUGUI endOfGameLevelDisplay;
    private bool isGameStarted;
    private bool isGameFinished;

    private void Start()
    {
        isGameStarted = false;
        isGameFinished = false;
        endOfGameLevelDisplay = null;
    }

    private void Update()
    {
        if (isGameFinished) 
        {
            if (endOfGameLevelDisplay == null)
            {
                endOfGameLevelDisplay = GameObject.FindWithTag("endLevelDisplay").GetComponent<TextMeshProUGUI>();
                displayEndLevel();
            }
            else
                isGameFinished = true;
        }
    }
    public void endOfGame()
    {
        isGameFinished = true;
        levelDisplay.gameObject.SetActive(false);
    }

    public void nextLevel()
    {
        //heartsScript.nextLevel();  //unsure if I like this or not
        int levelSelctionRandomizer = Random.Range(1, 3);
        if (!isGameStarted)
        {
            
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(levelOneName);
            heartsScript.gameStart();
            scoreManager.gameStart();
            isGameStarted = true;
            levelDisplay.gameObject.SetActive(true);
        }
        else if (levelSelctionRandomizer == 1 )
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(levelOneName);
        }
        else if (levelSelctionRandomizer == 2)
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(levelTwoName);
        }
        sceneRandomizer.gameObject.SetActive(true);
        level++;
        displayLevel();
    }


    public void restart()
    {
        heartsScript.gameObject.SetActive(true);
        heartsScript.nextLevel();
        sceneRandomizer.gameObject.SetActive(true);
        isGameStarted = false;
        isGameFinished=false;
        level = 0;
        nextLevel();
        scoreDisplay.SetActive(true);
        scoreManager.restart();
        displayLevel();
    }

    public void displayLevel()
    {
        levelDisplay.text = "Level: " + level.ToString();
    }
    public void displayEndLevel()
    {
        endOfGameLevelDisplay.text = "Final Level Count: " + level.ToString();
    }
}
