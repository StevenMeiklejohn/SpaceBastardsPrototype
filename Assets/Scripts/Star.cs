using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    //Speed of the star.
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Get the current position of the star.
        Vector2 position = transform.position;

        //Compute the star's new position.
        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        //Update the star's position
        transform.position = position;

        //Bottom left point of the screen.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //Top right of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //if the star goes outside the screen on the bottom,
        //then position the star on the top edge of the screen
        //and randomly between the left and right side of the screen.
        if (transform.position.x < min.x){
            transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
        }
		
	}
}
