﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossControl : MonoBehaviour {

    //Reference to the text UI game object
    GameObject scoreUITextGO;

    public GameObject ExplosionSmallGo;
    public GameObject ExplosionLargeGo;
    public GameObject BlueExplosionGO;
    //Enemy Speed
    float speed;

    int lives = 10;

	// Use this for initialization
	void Start () {
        //Set speed
        speed = 1f;
        //Get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
		
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

        //If enemy leaves the screen to the left, destroy the object
        if (transform.position.x < min.x){
            Destroy(gameObject);
        }
		
	}

    private void OnTriggerEnter2D(Collider2D col){
        if((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")){
            
            PlayExplosionSmall();
            lives--;

            if (lives == 0)
            {
                //Add 100 points to the score
                scoreUITextGO.GetComponent<GameScore>().Score += 300;
                //Destroy enemy ship.
                Destroy(gameObject);
                PlayExplosionLarge();

            }
        }
        
    }

    //Instantiate a small explosion
    void PlayExplosionSmall()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionSmallGo);
        //Set the position of the explosion
        explosion.transform.position = transform.position;
    }

    //Instantiate a small explosion
    void PlayExplosionLarge()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionLargeGo);
        //Set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
