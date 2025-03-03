using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupPrefabs;
    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public UIDocument UIDoc;
    private static Label hscounter;
    private static Label counter;
    private static int highScore = 0;
    private static int Score = 0;

    public int enemyCount;
    public int waveCount = 1;

    public GameObject player;

    void Start()
    {
        counter = UIDoc.rootVisualElement.Q<Label>("PlayerScore");
        hscounter = UIDoc.rootVisualElement.Q<Label>("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
     
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; //part of challenge: used to say "powerup" i changed it to enemy

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }
        counter.text = "" + Score;
        hscounter.text = "" + highScore;
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a random number (1 to 5) of powerups
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0 && GameObject.FindGameObjectsWithTag("PushPowerup").Length == 0)
        {
            int powerupsToSpawn = Random.Range(1, 4); // Spawn between 1 and 3 powerups
            for (int i = 0; i < powerupsToSpawn; i++)
            {
                int randomIndex = Random.Range(0, powerupPrefabs.Length); // Pick a random powerup
                Instantiate(powerupPrefabs[randomIndex], GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefabs[randomIndex].transform.rotation);
            }
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            if (waveCount == 1) randomIndex = 0;//i only spawn normal enemy on the first wave after that its will be randomized
            Instantiate(enemyPrefab[randomIndex], GenerateSpawnPosition(), enemyPrefab[randomIndex].transform.rotation);
        }

        waveCount++;
        ResetPlayerPosition(); // put player back at start
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 0, 2);
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void incScore()
    {
        Score++;
    }
    public void decScore()
    {
        Score--;
    }
}
