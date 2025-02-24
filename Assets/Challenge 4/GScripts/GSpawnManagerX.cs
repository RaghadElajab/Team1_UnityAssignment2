using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GSpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnX = 0;
    private float spawnZ = 11.866f; 

    public static int enemyCount;
    public static int waveCount = 1;

    public UIDocument UIDoc;
    public Label counter;

    public GameObject player;

    private int enemiesToSpawn=0;
    private bool spawningrn=false;

    public HomePageHandler homepage;

    void Start()
    {
        counter = UIDoc.rootVisualElement.Q<Label>("counter");
        counter.style.display = DisplayStyle.None;
    }
                                     
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; //part of challenge: used to say "powerup" i changed it to enemy

        if (enemyCount == 0 && !spawningrn)
        {
            spawningrn = true;
            enemiesToSpawn = waveCount;
            StartCoroutine(SpawnEnemyWave());
        }

    }



    private IEnumerator SpawnEnemyWave()
    {
        StartCoroutine(showCount(enemiesToSpawn));

        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end


        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, new Vector3(spawnX, -0.64f, spawnZ) + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float delay = (3+(i * 2f));
            yield return new WaitForSeconds(delay);
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(spawnX, -0.64f, spawnZ), enemyPrefab.transform.rotation);
            GEnemyX enemyscript = newEnemy.GetComponent<GEnemyX>();
            enemyscript.spawner = this;
        }

        waveCount++;
        spawningrn = false;

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

    private IEnumerator showCount(int enemiesToSpawn)
    {
        
        counter.text = ""+enemiesToSpawn;
        counter.style.display = DisplayStyle.Flex;
        
        yield return new WaitForSeconds(3);

        counter.style.display = DisplayStyle.None;


    }

    public void reset()
    {
        enemyCount = 0;
        waveCount = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
