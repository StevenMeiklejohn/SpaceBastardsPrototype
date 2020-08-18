using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetController : MonoBehaviour {

    //An array of planetGO prefabs
    public GameObject[] Planets;

    //Queue to hold planets
    Queue<GameObject> availablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () {

        //Add the planets to the queue (Enqueue them).
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        //Call the move planet left function one every 20 seconds.
        InvokeRepeating("MovePlanetLeft", 0, 20);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Function to de-queue a planet and set its isMoving flag to true.
    //So that the planet starts scrolling along the screen.
    void MovePlanetLeft(){

        EnqueuePlanets();
        
        //If the queue is empty, then return
        if (availablePlanets.Count == 0)
            return;

        //Get a planet from the queue
        GameObject aPlanet = availablePlanets.Dequeue();

        //Set the planet isMoving flag to true
        aPlanet.GetComponent<Planet>().isMoving = true;

    }

    //Function to enqueue planets which are left of the screen and not moving.
    void EnqueuePlanets(){

        foreach(GameObject aPlanet in Planets){
            
            //If the planet id left of the screen and not moving.
            if((aPlanet.transform.position.x < 0 ) && (!aPlanet.GetComponent<Planet>().isMoving)){
                
                //Reset the planet position
                aPlanet.GetComponent<Planet>().ResetPosition();

                //Enqueue the planet
                availablePlanets.Enqueue(aPlanet);
            }
        }
        
    }
}
