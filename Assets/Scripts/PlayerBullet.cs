using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    float speed;

	// Use this for initialization
	void Start () {
        speed = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        //Get the bullet's current position
        Vector2 position = transform.position;
        //Compute the bullet's new position
        position = new Vector2(position.x + speed * Time.deltaTime, position.y);



        //Update the bullet's position
        transform.position = position;

        //Destroy the bullet when it leaves the screen.

        //Get the right hand side of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.x > max.x){
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D col){
        if((col.tag == "EnemySleekTag") || (col.tag == "EnemyJavelinTag") 
            || (col.tag == "EnemyCircleTag") || (col.tag == "EnemySmallTag") 
            || (col.tag == "EnemyTriangleTag") || (col.tag == "EnemyTriangleTag")
            || (col.tag == "EnemyBossTag")){
                Destroy(gameObject);
            }
        
    }
}
