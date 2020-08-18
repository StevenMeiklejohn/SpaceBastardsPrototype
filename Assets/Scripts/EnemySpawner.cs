﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemySleekGo;
    public GameObject EnemyCircleGo;
    public GameObject EnemyJavelinGo;
    public GameObject EnemyTriangleGo;
    public GameObject EnemySmallGo;
    public GameObject EnemyBossGo;

    public GameObject[] Enemies;


    float maxSpawnRateInNSeconds = 5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    //Function will create and enemy prefab position on the right hand side of the screen and randomly between the top and bottom.
    void SpawnEnemy(){

        //Get bottom left of screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //UnitySystemConsoleRedirector.Redirect();
        //Debug.Log(min);
        //Get top right of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //UnitySystemConsoleRedirector.Redirect();
        //Debug.Log(min);

        //Create random number for array selection
        int randomSelection = Random.Range(0, 6);

        //Get random object from enemies array.
        //Instantiate an enemy
        GameObject anEnemy = (GameObject)Instantiate(Enemies[randomSelection]);
        float randomY = Random.Range(min.y + anEnemy.GetComponent<SpriteRenderer>().sprite.bounds.extents.y , max.y - (anEnemy.GetComponent<SpriteRenderer>().sprite.bounds.extents.y)*4);
        UnitySystemConsoleRedirector.Redirect();
        Debug.Log(randomY);
        anEnemy.transform.position = new Vector2 (max.x, randomY);

        //Schedule when to spawn next enemy.
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn(){
        float spawnInNSeconds;

        if (maxSpawnRateInNSeconds > 1f)
        {
            //Pick a number between 1 and maxSpawnRateInSeconds
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInNSeconds);
        }
        else
            spawnInNSeconds = 1f;
        Invoke("SpawnEnemy", spawnInNSeconds);
        
    }

    //Function to increase the difficuklty of the game
    void IncreaseSpawnRate(){
        if (maxSpawnRateInNSeconds > 1f)
            maxSpawnRateInNSeconds--;
        if (maxSpawnRateInNSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    //Function to start enemy spawner
    public void ScheduleEnemySpawner(){

        //Reset max spawn rate
        maxSpawnRateInNSeconds = 5f;


        Invoke("SpawnEnemy", maxSpawnRateInNSeconds);

        //Increase spawn rate every 30 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }


    //Function to stop enemy spawner.
    public void UnscheduleEnemySpawner(){
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

}
