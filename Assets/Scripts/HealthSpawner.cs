using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour {

    public GameObject HealthPickupGo;

    float maxSpawnRateInNSeconds = 35f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    //Function will create and enemy prefab position on the right hand side of the screen and randomly between the top and bottom.
    void SpawnHealth(){

        //Get bottom left of screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //UnitySystemConsoleRedirector.Redirect();
        //Debug.Log(min);
        //Get top right of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //UnitySystemConsoleRedirector.Redirect();
        //Debug.Log(min);


        //Get random object from enemies array.
        //Instantiate an enemy
        GameObject HealthPickUp = (GameObject)Instantiate(HealthPickupGo);
        float randomY = Random.Range(min.y + HealthPickUp.GetComponent<SpriteRenderer>().sprite.bounds.extents.y , max.y - (HealthPickUp.GetComponent<SpriteRenderer>().sprite.bounds.extents.y *2));
        //UnitySystemConsoleRedirector.Redirect();
        //Debug.Log(randomY);
        HealthPickUp.transform.position = new Vector2 (max.x, randomY);

        //Schedule when to spawn next enemy.
        ScheduleNextHealthSpawn();
    }

    void ScheduleNextHealthSpawn(){

        Invoke("SpawnHealth", maxSpawnRateInNSeconds);
        
    }



    //Function to start health spawner
    public void ScheduleHealthSpawner(){
        
        Invoke("SpawnHealth", maxSpawnRateInNSeconds);

    }


    //Function to stop health spawner.
    public void UnscheduleHealthSpawner(){
        CancelInvoke("SpawnHealth");
}

}
