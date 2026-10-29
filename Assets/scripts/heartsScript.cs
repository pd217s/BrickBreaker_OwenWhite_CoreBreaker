using System;
using Unity.VisualScripting;
using UnityEngine;

public class heartsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    GameObject heart1;
    [SerializeField]
    GameObject heart2;
    [SerializeField]
    GameObject heart3;
    [SerializeField]
    private Animator animator1;

    [SerializeField]
    private Animator animator2;

    [SerializeField]
    private Animator animator3;
    [SerializeField]
    private int health;

    [SerializeField]
    paddelControler Pad;

    private bool hurtTimerActivate;
    private GameObject heartToHurt;
    private float timer;

    [SerializeField]
    ScoreManager scoreManager;

    [SerializeField] float collisionDelay;

    void Start()
    {
        health = 3;
        Pad = GameObject.FindWithTag("paddel").GetComponent<paddelControler>();
    }

    // Update is called once per frame
    void Update()
    {
        collisionDelay -= Time.deltaTime;
        if (hurtTimerActivate == true)
        {
            timer = 0.30f;
            hurtTimerActivate = false;
        }
        
        if (timer < 0)
        {
            heartToHurt.gameObject.SetActive(false);
            timer = 0;
        }
        else if (timer > 0)
            timer -= Time.deltaTime;

        if (Pad == null)
        {
            Pad = GameObject.FindWithTag("paddel").GetComponent<paddelControler>();
        }
    }

    public void hurtPlayer()
    {
        if (collisionDelay <= 0)
        {
            if (health == 3)
            {
                animator1.SetBool("isHurt", true);
                heartToHurt = heart1;
                hurtTimerActivate = true;
                Pad.gameRestart();
            }
            else if (health == 2)
            {
                animator2.SetBool("isHurt", true);
                heartToHurt = heart2;
                hurtTimerActivate = true;
                Pad.gameRestart();
            }
            else if (health == 1)
            {
                //animator3.SetBool("isHurt", true);
                //heartToHurt = heart3;
                //hurtTimerActivate = true;

                heart3.gameObject.SetActive(false);
                Debug.Log("before end game");
                scoreManager.endGame();
            }
            collisionDelay = 0.01f;
        }

        health--;

        Debug.Log("here");
    }

    public void healPlayer() //this is limited to max health but this is helpful becuase it makes the player have to aim away from this until they need to heal
    {
        if (health == 2)
        {
            heart1.SetActive(true);
            health++;
        }
        else if(health == 1) //this else if is important becuase it checks if the health is 3 if this wasnt here the health could go above
        {
            heart2.SetActive(true);
            health++;
        }
    }
   

    public void nextLevel() //saving for maby future use
    {
        health = 3;
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
    }
    public void gameStart()
    {
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
    }
}
