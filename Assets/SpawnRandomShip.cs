using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomShip : MonoBehaviour
{
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject asteroid4;
    public GameObject asteroid5;
    public GameObject asteroid6;
    public GameObject asteroid7;
    public GameObject asteroid8;
    public GameObject asteroid9;
    public GameObject asteroid10;

    public GameObject enemy1;
    public GameObject enemy2;

    public GameObject ship_1;
    public GameObject ship_2;
    public GameObject mothership;
    
    private int choice;
    private Vector3 rocklocation;

    private static int asteroidnum = 2000;
    private static int asteroidspread = 1000;

    private static int enemyspread = 500;
    private int enemyCount = 2;
    private int waveCount;
    private Vector3 rotate;

    public GameObject IndicatorHandler;

    private GameObject enemy;
    private GameObject player;

    public void NewWave()
    {
        Debug.Log("trying to spawn");
        for (int i = 0; i < enemyCount; i++)
        {
            enemy = Instantiate(enemy1, Random.insideUnitSphere * enemyspread, Quaternion.identity);
            enemy.name = "enemy1 " + i.ToString();
            enemy = Instantiate(enemy2, Random.insideUnitSphere * enemyspread, Quaternion.identity);
            enemy.name = "enemy2 " + i.ToString();
        }
        enemyCount += 1;
        IndicatorHandler.GetComponent<EnemyIndicatorHandler>().LogNewEnemies();
    }

    void Start()
    {
        
        player =  Instantiate(ship_1, new Vector3(500, 200, 0), Quaternion.Euler(new Vector3(0,180,0)));
        player.name = "player";
        Instantiate(mothership, new Vector3(10, 10, -2), Quaternion.identity);
        NewWave();

       


       
     

        rotate.Set(0.1f, 0.1f, 0.1f);

            for (int i = 0; i < asteroidnum; i++)
        {
            choice = Random.Range(0, 9);

            if (choice == 0)
            {
                Instantiate(asteroid1, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 1)
            {
                Instantiate(asteroid2, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 2)
            {
                Instantiate(asteroid3, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 3)
            {
                Instantiate(asteroid4, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 4)
            {
                Instantiate(asteroid5, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 5)
            {
                Instantiate(asteroid6, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 6)
            {
                Instantiate(asteroid7, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 7)
            {
                Instantiate(asteroid8, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 8)
            {
                Instantiate(asteroid9, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }
            if (choice == 9)
            {
                Instantiate(asteroid10, Random.insideUnitSphere * asteroidspread, Quaternion.identity);
            }

            }

        }

      
    }
