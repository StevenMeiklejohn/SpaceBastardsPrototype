using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public float flashSpeed;
    PolygonCollider2D polyColl;
    SpriteRenderer spRndrer;


    //Player's bullet Prefab
    public GameObject PlayerBulletGo;
    public GameObject BulletPosition01;
    //Animation for when ship is destroyed
    public GameObject ExplosionLargeGo;
    //References Game Manager
    public GameObject GameManagerGO;


    private Animator an;



    //Reference to the lives ui text
    public Text LivesUIText;
    //Maximum player lives;
    const int MaxLives = 3;
    //Current number of lives;
    int lives;

    //Movement Speed
    public float speed;

    public void Init(){
        lives = MaxLives;
        //Update the lives UI text
        LivesUIText.text = lives.ToString();
        //Reset the player position to the center of the screen
        transform.position = new Vector2(0, 0);
        //Set this player game object to active
        gameObject.SetActive(true);

        polyColl = GetComponent<PolygonCollider2D>();
        spRndrer = GetComponent<SpriteRenderer>();
        spRndrer.enabled = true;
        polyColl.enabled = true;
    }

	//// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	void Update () {

        //_____________
        //Bullets
        //_____________

        //Fire bullets when the spacebar is pressed.
        if(Input.GetKeyDown("space")){

            //Play the laser sound effect
            gameObject.GetComponent<AudioSource>().Play();
            
            //Instantiate bullet.
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGo);

            //Set the bullet initial position
            bullet01.transform.position = BulletPosition01.transform.position;
        }

        //____________
        //Player
        //____________



        //The value will be -1, 0, or 1 (for left, no input and right)
        float x = Input.GetAxisRaw("Horizontal");
        //The value will be -1, 0, or 1 (for down, no input and up)
        float y = Input.GetAxisRaw("Vertical");

        //Based on the inpue, compute a direction vector, then normalise it to get a unit vector.
        Vector2 direction = new Vector2(x, y).normalized;

        //Now call the function that computes and sets the player's position.
        Move(direction);
                                             
	}

    void Move(Vector2 direction)
    {
        //Find the screen limits to restrict the players movement
        //Get the bottom left corner of the screen.
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //This is the top right point of the screen.
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Subtract half the player width
        max.x = max.x - 0.5f;
        //Add hald the player width
        min.x = min.x + 0.5f;

        //Subtract half the player height
        max.y = max.y - 0.85f;
        //Add half the player height
        min.y = min.y - 0.25f;

        //Get the player's current position.
        Vector2 pos = transform.position;

        //Calculate the new position
        pos += direction * speed * Time.deltaTime;

        //Make sure the new position is not outside the screen.
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update the player's position
        transform.position = pos;

        UnitySystemConsoleRedirector.Redirect();
        Debug.Log(transform.position);

    }

    void OnTriggerEnter2D(Collider2D col){
        
        //Get player's animation controller
        an = GetComponent<Animator>();


        //Detect collision of the player ship with an enemy ship or an enemy bullet.
        if((col.tag == "EnemySleekTag") || (col.tag == "EnemyJavelinTag")
            || (col.tag == "EnemyCircleTag") || (col.tag == "EnemySmallTag")
            || (col.tag == "EnemyTriangleTag") || (col.tag == "EnemyTriangleTag")
           || (col.tag == "EnemyBossTag") || (col.tag == "EnemyBulletTag")){
            PlayExplosion();

            //Make sprite flash
            StartCoroutine(Flash(flashSpeed));

            //Remove 1 life
            lives--;
            //Update lives in the UI
            LivesUIText.text = lives.ToString();

            //if(lives >= 3){
            //    an.SetInteger("AnimState", 0);
            //}
            if(lives < 3){
                an.SetInteger("AnimState", 1);
            }

            if (lives == 0)
            {
                //Change game manager state to game over state
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                //Hide the player's ship if out of lives
                gameObject.SetActive(false);
            }
        }


        if(col.tag == "Health"){
            lives++;
            //Update lives in the UI
            LivesUIText.text = lives.ToString();
            an.SetInteger("AnimState", 0);
        }
    }

    //Instantiate an explosion
    void PlayExplosion(){
        GameObject explosion = (GameObject)Instantiate(ExplosionLargeGo);
        //Set the position of the explosion
        explosion.transform.position = transform.position;
    }

    IEnumerator Flash(float x)
    {
        polyColl.enabled = false;
        for (int i = 0; i < 10; i++)
        {
            spRndrer.enabled = false;
            yield return new WaitForSeconds(x);
            spRndrer.enabled = true;
            yield return new WaitForSeconds(x);
        }
        polyColl.enabled = true;
    }


}
