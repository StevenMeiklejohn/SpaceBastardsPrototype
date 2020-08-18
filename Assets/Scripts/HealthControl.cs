using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour {



    float speed;

	// Use this for initialization
	void Start () {
        //Set speed
        speed = 5f;
		
	}
	
	// Update is called once per frame
	void Update () {

        //Get current position
        Vector2 position = transform.position;

        //Compute the new enemy position
        position = new Vector2(position.x - speed * Time.deltaTime, position.y);

        //Update the enemy position
        transform.position = position;

        //Get the left hand side of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //If object leaves the screen to the left, destroy the object
        if (transform.position.x < min.x){
            Destroy(gameObject);
        }
		
	}

    private void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "PlayerShipTag"){
            gameObject.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }
        
    }

}
