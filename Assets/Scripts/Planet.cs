using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    //Speed of planet
    public float speed;

    //flag to make planet scroll
    public bool isMoving;

    //Bottom left point of screen
    Vector2 min;
    //Top right of screen
    Vector2 max;

    void Awake(){

        isMoving = false;

        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //add the planet sprite half width to max x
        max.x = max.x + (GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2);

        //subtract the planet sprite half width to min x
        min.x = min.x - (GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2);

    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isMoving)
            return;

        //Get the current position of the planet
        Vector2 position = transform.position;

        //Compute the planet's new position.
        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        //Update the planet's position
        transform.position = position;

        //If the planet gets to the minimium x position, stop moving the planet.
        if (transform.position.x < min.x - GetComponent<SpriteRenderer>().sprite.bounds.extents.x){
            isMoving = false;
        }

	}

    //Function to reset the planet's position
    public void ResetPosition(){

        //Reset the position of the planet to max x and random y
        transform.position = new Vector2(max.x + GetComponent<SpriteRenderer>().sprite.bounds.extents.x, Random.Range(min.y, max.y));
    }
}
