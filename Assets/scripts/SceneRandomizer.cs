using UnityEngine;

public class SceneRandomizer : MonoBehaviour
{

    [SerializeField] GameObject foundBrick;
    [SerializeField] GameObject brick1;
    [SerializeField] GameObject brick2;
    [SerializeField] GameObject brick3;
    [SerializeField] GameObject brick4;
    [SerializeField] float powerUpChance;
    private bool isBrickFound;

    void Start()
    {
        isBrickFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        foundBrick = GameObject.FindWithTag("UndefinedBrick");
        float randomBrickNumber;
        if (foundBrick != null)
        {
            randomBrickNumber = Random.Range(0, 100);
            GameObject spawnedBrick;

            if (randomBrickNumber > (100 - powerUpChance) && randomBrickNumber < 100) //doesnt need that and just doesnt feel right without it
            {
                int randomIntBrickNum = Random.Range(1, 4);

                if (randomIntBrickNum == 1)
                    spawnedBrick = Instantiate(brick2);
                else if (randomIntBrickNum == 2)
                    spawnedBrick = Instantiate(brick3);
                else
                    spawnedBrick = Instantiate(brick4);

                powerUpChance -= 10;
            }
            else
            {
                spawnedBrick = Instantiate(brick1);
                powerUpChance += 5;
            }
               


            spawnedBrick.gameObject.transform.position = foundBrick.transform.position;
            Destroy(foundBrick);
            foundBrick.SetActive(false);
            isBrickFound = true;
        }
        else if (isBrickFound) //this assumes that there was no found brick but requires at least one brick to already be spawned in
        {
            isBrickFound = false;
            gameObject.SetActive(false);//if above is true it tuns itself off so it doesnt use up ram
        }


    }
}
