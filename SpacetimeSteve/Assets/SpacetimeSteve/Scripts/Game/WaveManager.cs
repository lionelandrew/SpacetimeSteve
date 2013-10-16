using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    public EraManager eraManager;
    public EnemyConstructor enemyConstructor;
    public GameObject enemySpawners;

    public UILabel waveAmountLabel;
    public UILabel EnemiesAmountLabel;

    public GameObject nextWavePanel;

    public static int waveNumber = 1;
    public int waveIterationNumber = 0;
    public int gameDifficulty = 1;

    public float spawnMaxTimer = 1.0f;
    public float nextWaveMaxTimer = 5.0f;

    int remainingCount = 0;
    int totalWaveCount = 0;
    int enemiesSpawned = 0;

    bool hasRoundStarted = false;

    float nextWaveTimer = 0.0f;
    float spawnTimer = 0.0f;


    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.running)
        {
            if (remainingCount <= 0 && hasRoundStarted == true)
                hasRoundStarted = false;

            if (hasRoundStarted == false)
            {
                nextWaveTimer += Time.deltaTime;

                nextWavePanel.transform.localPosition = new Vector3(0, -220, 0);
                nextWavePanel.transform.GetChild(0).GetComponent<UILabel>().text = "Next Wave in... " + ((int)nextWaveMaxTimer - (int)nextWaveTimer);

                if (nextWaveTimer >= nextWaveMaxTimer)
                {
                    waveNumber++;
                    SpawnWave();
                    nextWavePanel.transform.localPosition = new Vector3(1000, -220, 0);
                    nextWaveTimer = 0.0f;
                }
            }

            if (hasRoundStarted == true && enemiesSpawned < totalWaveCount)
            {
                spawnTimer += Time.deltaTime;

                if (spawnTimer >= spawnMaxTimer)
                {
                    SpawnEnemy();
                    spawnTimer = 0.0f;
                }
            }

            EnemiesAmountLabel.text = "Enemies Remaining: " + remainingCount + " / " + totalWaveCount;
        }
    }

    public void SpawnWave()
    {
        if (waveNumber > (10 * eraManager.wavePerEraAmount) - 1)
        {
            waveNumber = 0;
            waveIterationNumber += 10 * eraManager.wavePerEraAmount;
        }

        eraManager.SetCurrentEra(waveNumber);
        waveAmountLabel.text = "Wave: " + ((waveNumber + waveIterationNumber) + 1);
        remainingCount = (int)((((waveNumber + waveIterationNumber + 1)/ 2.0) + ((gameDifficulty + 1) / 1.5)) * 3);
        totalWaveCount = remainingCount;
        enemiesSpawned = 0;

        hasRoundStarted = true;
    }

    void SpawnEnemy()
    {
        enemiesSpawned++;

        int randomSpawnpoint = Random.Range(0, enemySpawners.transform.GetChildCount());

        enemyConstructor.ContructEnemy(waveNumber, (int)eraManager.currentEra, enemySpawners.transform.GetChild(randomSpawnpoint).transform.position);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        remainingCount--;
    }
}
