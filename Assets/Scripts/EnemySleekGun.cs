using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySleekGun : MonoBehaviour {

    //Enemy bullet prefab
    public GameObject EnemyBulletGo;

	// Use this for initialization
	void Start () {
        //Fire an enemy bullet after 1 second
        //Invoke("FireEnemyBullet", 1f);
        InvokeRepeating("FireEnemyBullet", 1f, 5f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Function to fire an enemy bullet
    void FireEnemyBullet(){
        //Get a reference to the player's location.
        GameObject playerShip = GameObject.Find("playerGo");

        if(playerShip != null){
            //Instantiate an enemy bullet
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGo);
            //Set the bullet's initial position.
            bullet.transform.position = transform.position;
            //Compute the bullet's direction towards the player's ship.
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            //UnitySystemConsoleRedirector.Redirect();
            //Debug.Log(direction);
            //Set the bullet's direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
