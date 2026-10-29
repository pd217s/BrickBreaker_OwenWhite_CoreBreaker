//using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public class balls_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float speed;
    [SerializeField] private float right;
    [SerializeField] private float left;
    [SerializeField] private float up;
    [SerializeField] private float down;
    [SerializeField] private GameObject middleOfPaddel;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private heartsScript heartsScript;
    [SerializeField] private paddelControler paddelControler;
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private float directionX;
    private float directionY;
    private Rigidbody2D RB;

    private float collisionDelay;


    void Start()
    {
        if (right > 0)
            directionX = right;
        else
            directionX = left * -1;

        if (up > 0)
            directionY = up;
        else
            directionY = down * -1;
        RB = GetComponent<Rigidbody2D>();
        RB.linearVelocity = new Vector2(speed * directionX, speed * directionY);
        middleOfPaddel = GameObject.FindWithTag("middle");
        scoreManager = GameObject.FindWithTag("healthManager").GetComponent<ScoreManager>();
        levelManager = GameObject.FindWithTag("ParentOfHearts").GetComponent<LevelManager>();
        heartsScript = GameObject.FindWithTag("healthManager").GetComponent<heartsScript>();
        paddelControler = GameObject.FindWithTag("paddel").GetComponent<paddelControler>();
        audioSource = GetComponent<AudioSource>();

        maxSpeed = speed * 6f;
        minSpeed = 6f / speed;

    }

    // Update is called once per frame
    void Update()
    {
        speedControler();
        if (collisionDelay > 0)
            collisionDelay -= Time.deltaTime;
        
        if (middleOfPaddel != null)
            middleOfPaddel = GameObject.FindWithTag("middle");
    }

    private void speedControler()
    {
        float absSpeedX;
        float absSpeedY;

        if (RB.linearVelocityX >= 0)
            absSpeedX = RB.linearVelocityX;
        else
            absSpeedX = RB.linearVelocityX * -1;

        if (RB.linearVelocityY >= 0)
            absSpeedY = RB.linearVelocityY;
        else
            absSpeedY = RB.linearVelocityY * -1;

        if (absSpeedX > maxSpeed)
            RB.linearVelocityX = RB.linearVelocityX / 1.2f;
        if (absSpeedY > maxSpeed)
            RB.linearVelocityY = RB.linearVelocityY / 1.2f;

        if (absSpeedX < minSpeed)
            RB.linearVelocityX = RB.linearVelocityX * 1.2f;
        if (absSpeedY < minSpeed)
            RB.linearVelocityY = RB.linearVelocityY * 1.2f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionDelay  <= 0)
        {
            if (collision.gameObject.CompareTag("paddel"))
            {
                directionY = down;

                directionX = RB.position.x - middleOfPaddel.gameObject.transform.position.x;

                RB.linearVelocity = RB.linearVelocity = new Vector2(speed * directionX, speed * directionY);
            }


            //if (collision.gameObject.CompareTag("wallSide"))
            //    directionX *= -1;
            //else if (collision.gameObject.CompareTag("wallTop"))
            //    directionY *= -1;
            else if (collision.gameObject.CompareTag("BrickSide"))
            {
                //directionX *= -1;

                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.CompareTag("BrickEnd"))
            {
                //directionY *= -1;
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("core"))
            {
                scoreManager.addToStageScore();
                colidedWithACore(collision);
            }
            else if (collision.gameObject.CompareTag("blueCore"))
            {
                paddelControler.addBalls(3);
                Debug.Log("balls to add");
                colidedWithACore(collision);
            }
            else if (collision.gameObject.CompareTag("orangeCore"))
            {
                scoreManager.multiplyStageScore(4);
                colidedWithACore(collision);
            }
            else if (collision.gameObject.CompareTag("redCore"))
            {
                heartsScript.healPlayer();
                colidedWithACore(collision);
            }

            audioSource.clip = hitSound;
            audioSource.Play();
        }
        collisionDelay = 0.01f;
    }

    private void colidedWithACore(Collision2D collision) //useful logic so I can have multiple difrent cores
    {
        bool coreChecker = false;
        Destroy(collision.gameObject.transform.parent.gameObject);
        collision.gameObject.SetActive(false); //so it does not false flag as still in game
        if (GameObject.FindWithTag("core"))
            coreChecker = true;


        //else if (GameObject.FindWithTag("blueCore"))   //I decided to end the level when the last point block is broke
        //    coreChecker= true;
        //else if (GameObject.FindWithTag("orangeCore"))
        //    coreChecker = true;
        //else if (GameObject.FindWithTag("redCore"))
        //    coreChecker = true;

        if (!coreChecker) //if no core type is found then go to next level
        {
            levelManager.nextLevel();
            scoreManager.nextStage();
            Debug.Log("nextLevelAttempt");
        }
        
    }
}
