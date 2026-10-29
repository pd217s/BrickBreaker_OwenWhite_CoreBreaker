using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float playerScore;
    [SerializeField] private float stageScore;

    [SerializeField] LevelManager levelManager;

    [SerializeField]private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI stageScoreDisplay;

    private void Start()
    {
        scoreDisplay = GameObject.FindWithTag("scoreDisplay").GetComponent<TextMeshProUGUI>();
        scoreDisplay.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (scoreDisplay == null) //when a scene loads it
        {
            Debug.Log("no known scoreDisplay");
            scoreDisplay = GameObject.FindWithTag("scoreDisplay").GetComponent<TextMeshProUGUI>();
            displayScore();
        }
    }

    //mainscoresection
    public float getScore()
    {
        return playerScore;
    }

    public void addToScore()
    {
        playerScore++;
        displayScore();
    }
    public void addToScore (int num)
    {
        playerScore += num;
        displayScore();
    }
    public void multiplyScore(float num) //dont know if I should use this
    {
        playerScore *= num;
        displayScore();
    }

    //stageScore section
    public float getStageScore()
    {
        return playerScore;
    }

    public void addToStageScore()
    {
        stageScore++;
        displayStageScore();
    }
    public void addToStageScore(int num)
    {
        stageScore += num;
        displayStageScore();
    }
    public void multiplyStageScore(float num)
    {
        stageScore *= num;
        displayStageScore();
    }

    //=========================================================================
    //other
    //=========================================================================

    public void endGame()
    {
        nextStage();
        Debug.Log("EndGame attempt");
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("endOfGame");
        levelManager.endOfGame();
        scoreDisplay.gameObject.SetActive(false);
        stageScoreDisplay.gameObject.SetActive(false);
        scoreDisplay = null;
    }

    private void displayScore()
    {
        scoreDisplay.text = "Score: " + playerScore.ToString();
    }

    private void displayStageScore()
    {
        stageScoreDisplay.text = stageScore.ToString();
    }

    public void gameStart()
    {
        scoreDisplay.gameObject.SetActive(true);
        stageScoreDisplay.gameObject.SetActive(true);
        displayScore();
    }

    public void nextStage()
    {
        playerScore += stageScore;
        stageScore = 0;
        displayStageScore();
        displayScore();
    }

    public void restart()
    {
        scoreDisplay = null;
        stageScoreDisplay.gameObject.SetActive(true);
        playerScore = 0;
    }
}
