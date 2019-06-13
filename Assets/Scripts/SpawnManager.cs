using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _astreoid;

    [SerializeField]
    private GameObject enemy;
    private float enemySpawnTimeGaps = 5.0f;
    private float lastSpawnedEnemy = 0.0f;

    private float gameLevelUpTime = 5.0f;

    [SerializeField]
    private GameObject[] powerUps;
    private float powerUpSpawnTimeGaps = 5.0f;
    private float lastSpawnedPowerUp = 0.0f;

    private bool levelUpdated = true;


    // Use this for initialization
    void Start () {
        float randomX = Random.Range(-6.5f, -6.5f);
        Vector3 spawnPos = new Vector3(randomX, 7.0f, 0.0f);
        Instantiate(enemy, spawnPos, Quaternion.identity);
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

            StartCoroutine(SpawnAstreoidCoRoutine());
        }

    }

    IEnumerator SpawnAstreoidCoRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        float randomX = Random.Range(-6.5f, -6.5f);
        Vector3 spawnPos = new Vector3(randomX, 7.0f, 0.0f);
        Instantiate(_astreoid, spawnPos, Quaternion.identity);
    }

    void PowerUpSpawnController()
    {
        int powerUpId = (int) Random.Range(0, 3);
        int now = (int)Time.time;
        if (now - lastSpawnedPowerUp >= powerUpSpawnTimeGaps)
        {
            lastSpawnedPowerUp = now;
            float randomX = Random.Range(-6.5f, 6.5f);
            Vector3 spawnPos = new Vector3(randomX, 7.0f, 0.0f);
            Instantiate(powerUps[powerUpId], spawnPos, Quaternion.identity);
        }
    }

    void SpawnRateController()
    {
        int gameTime = (int)Time.time;

        if (gameTime%gameLevelUpTime == 0 && levelUpdated == false)
        {
            levelUpdated = true;
            enemySpawnTimeGaps -= 0.5f;
            powerUpSpawnTimeGaps -= 0.5f;
            gameLevelUpTime -= .50f;
            
        }
        else if (gameTime%gameLevelUpTime != 0)
        {
            levelUpdated = false;
        }

        enemySpawnTimeGaps = System.Math.Max(0.5f, enemySpawnTimeGaps);
        powerUpSpawnTimeGaps = System.Math.Max(0.5f, powerUpSpawnTimeGaps);

    }
}
