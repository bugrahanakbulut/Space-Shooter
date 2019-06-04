using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemy;
    private float enemySpawnTimeGaps = 5.0f;
    private float lastSpawnedEnemy = 0.0f;

    [SerializeField]
    private GameObject[] powerUps;
    private float powerUpSpawnTimeGaps = 5.0f;
    private float lastSpawnedPowerUp = 0.0f;

    private bool levelUpdated = true;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        EnemySpawnController();
        PowerUpSpawnController();
        SpawnRateController();

	}

    void EnemySpawnController()
    {
        int now = (int) Time.time;
        Debug.Log(now - lastSpawnedEnemy);
        if (now - lastSpawnedEnemy >= enemySpawnTimeGaps)
        {
            lastSpawnedEnemy = now;
            float randomX = Random.Range(-6.5f, -6.5f);
            Vector3 spawnPos = new Vector3(randomX, 7.0f, 0.0f);
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
        
    }

    void PowerUpSpawnController()
    {
        int powerUpId = (int) Random.Range(0, 3);
        int now = (int)Time.time;
        if (now - lastSpawnedPowerUp >= powerUpSpawnTimeGaps)
        {
            lastSpawnedPowerUp = now;
            float randomX = Random.Range(-6.5f, -6.5f);
            Vector3 spawnPos = new Vector3(randomX, 7.0f, 0.0f);
            Instantiate(powerUps[powerUpId], spawnPos, Quaternion.identity);
        }
    }

    void SpawnRateController()
    {
        int gameTime = (int)Time.time;

        if (gameTime%10 == 0 && levelUpdated == false)
        {
            levelUpdated = true;
            enemySpawnTimeGaps -= 0.5f;
            powerUpSpawnTimeGaps -= 0.3f;
            
        }
        else if (gameTime%10 != 0)
        {
            levelUpdated = false;
        }

        enemySpawnTimeGaps = System.Math.Max(0.5f, enemySpawnTimeGaps);
        powerUpSpawnTimeGaps = System.Math.Max(0.5f, powerUpSpawnTimeGaps);

    }
}
