using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGameScript : MonoBehaviour
{
    public int enemyTracker;
    public int enemySpawnRate;

    public Laser projectile1;
    public Bomb projectile2;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldsText;
    public TextMeshProUGUI primaryAmmoText;
    public TextMeshProUGUI secondaryAmmoText;

    public PlayerShip playerShip;
    public GameObject[] enemies;

    private float gameStartTime;
    private float gameCurrentTime;

    private int minutes;
    private int seconds;


    // Start is called before the first frame update
    void Start()
    {
        Globals.score = 0;
        enemySpawnRate = 2;
        scoreText.text = "Score:" + Globals.score;
        gameStartTime = Time.timeSinceLevelLoad;
        timeText.text = "Time: " + gameStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + Globals.score;

        gameCurrentTime = Time.timeSinceLevelLoad - gameStartTime;

        seconds = ((int) gameCurrentTime % 60);
        minutes = ((int) gameCurrentTime / 60);

        if ((seconds % enemySpawnRate == 0) && enemyTracker < ((((minutes * 60) + seconds) / enemySpawnRate) - 1))
        {
            var enemySpawn = Instantiate(enemies[(int)UnityEngine.Random.Range(0f, 6f)]) as GameObject;
            var enemySpawn1 = Instantiate(enemies[(int)UnityEngine.Random.Range(0f, 6f)]) as GameObject;
            enemyTracker++;
        }

        if (enemySpawnRate > 1)
        {
            enemySpawnRate = enemySpawnRate - minutes;
            if (enemySpawnRate < 1)
            {
                enemySpawnRate = 1;
            }
        }

        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        healthText.text = "Health: " + playerShip.health.ToString("0");
        shieldsText.text = "Shields: " + playerShip.shields.ToString("0");
        primaryAmmoText.text = "Primary: " + projectile1.getCurrentAmmo().ToString() + "/" + projectile1.getAmmoCapacity().ToString();
        secondaryAmmoText.text = "Secondary: " + projectile2.getCurrentAmmo().ToString() + "/" + projectile2.getAmmoCapacity().ToString();
    }
}