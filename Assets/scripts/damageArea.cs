using System;
using Unity.VisualScripting;
using UnityEngine;

public class damageArea : MonoBehaviour
{
    [SerializeField]
    heartsScript h;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        h = GameObject.FindWithTag("healthManager").GetComponent<heartsScript>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        collision.gameObject.SetActive(false);
        GameObject foundBall = GameObject.FindWithTag("ball");
        if (!foundBall)
        {
            h.hurtPlayer();
        }
        
    }
}
