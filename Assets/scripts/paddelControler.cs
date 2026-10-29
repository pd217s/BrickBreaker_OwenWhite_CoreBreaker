using UnityEngine;

public class paddelControler : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private int ballsRemaining;
    private bool gameStart;

    [SerializeField]
    private GameObject wallLeft;

    [SerializeField]
    private GameObject wallRight;

    [SerializeField] float maxTimeWait;
    private float timer;

    private void Start()
    {
        gameStart = true;
        timer = 0;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > (wallLeft.gameObject.transform.position.x + 2) && mousePosition.x< (wallRight.gameObject.transform.position.x - 2))
            transform.position = new Vector2(mousePosition.x, transform.position.y);
        if (gameStart)
            ball.SetActive(true);

        if (timer <= 0)
        {
            if ((Input.GetKeyDown(KeyCode.Space) && gameStart) || (Input.GetMouseButtonDown(0) && gameStart))
            {
                ballsRemaining--;
                GameObject spawnedBall;
                spawnedBall = Instantiate(ballPrefab);
                spawnedBall.transform.position = new Vector3(transform.position.x, transform.position.y + 2);
                gameStart = false;
                timer = maxTimeWait;
            }
            else if (ballsRemaining > 0 && !gameStart)
            {
                ballsRemaining--;
                GameObject spawnedBall;
                spawnedBall = Instantiate(ballPrefab);
                spawnedBall.transform.position = new Vector3(transform.position.x, transform.position.y + 2);
                timer = maxTimeWait;
            }
        }
        if (ballsRemaining == 0)
        {
            ball.SetActive(false);
        }
        timer -= Time.deltaTime;
    }
    public void gameRestart()
    {
        gameStart = true;
        ballsRemaining = 1;
    }

    public void addBalls()
    {
        ballsRemaining++;
    }

    public void addBalls(int num)
    {
        ballsRemaining += num;
    }
}