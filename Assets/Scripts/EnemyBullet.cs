using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    //Bullet speed
    public float speed;
    //Direction of the bullet
    Vector2 _direction;
    //Know when bullet is facing the right way.
    bool isReady;

    //Set default valuesin awaken functio.
    void Awake(){
        speed = 5f;
        isReady = false;
    }

    //Function to set bullet's direction
    public void SetDirection(Vector2 directionToSet){
        //Set the direction normalised to get a unit vector
        _direction = directionToSet.normalized;
        //Set flag to true
        isReady = true;

    }

	// Use this for initialization
	void Start () {

        if(isReady){
            //Get the bullet's current position
            Vector2 CurrentPosition = transform.position;

            //Compute the bullet's new position
            Vector2 NewPosition = CurrentPosition + _direction * speed * Time.deltaTime;

            //Update the bullet's position
            transform.position = NewPosition; 

        }
		
	}
	
	// Update is called once per frame
	void Update () {
        //Get the bullet's current position
        Vector2 CurrentPosition = transform.position;
        //Compute the bullet's new position
        Vector2 NewPosition = CurrentPosition + _direction * speed * Time.deltaTime;

        //Update the bullet's position
        transform.position = NewPosition;

        //Get bottom left point of screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //Get top left
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Destroy the bullet if it leaves the screen
        if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
            (transform.position.y < min.y) || (transform.position.y > max.y))
        {
            Destroy(gameObject);
        }
		
	}

    private void OnTriggerEnter2D(Collider2D col){
        //Detect collision with an enemy's bullet and the playerShiop
        if(col.tag == "PlayerShipTag"){
            //Destroy this bullet
            Destroy(gameObject);
        }
    }

}
