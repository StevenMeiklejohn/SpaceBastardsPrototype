# Unity 1 Day Course.


## 1. Create a new 2d project.
Create a new project in unity.
Inside the assets folder create new folders for;
Animations
Scenes
Sprites
Scripts
Sound
Prefabs

Next, set the camera size;
Select the main camera. In the inspector, select orthographic and size 3.2.
Save the scene as GameEngine.




## 2. Player Animation and importing player.
Unity has its own sprite animation tools, but I prefer to use Aseprite, which is a third party tool. I then use the attached animation importer to import the results into Unity.
https://blog.redbluegames.com/unity-tutorial-animate-pixel-art-using-aseprite-and-animation-importer-5c4fe1e06985

Download Animation importer tool.
Import the package into unity using Assets > Import Package > Custom Package...
Open the Importer through Window > Animation Importer

Create the sprite file using aseprite. Remember to tag the animation frames (all associated frames use the same tag name).
Click and drag the aseprite into the box titled "Animator Controller + Animations" in the animation importer.
This should create;
 a sprite sheet (all the animation frames)
 an animation controller (indicates which animation can trigger or switch to another i.e. run to jump, jump to run). You may want to rename the animation controller to something more descriptive (i.e PlayerAnimController).
 An animation clip or clips.

 Drag the PlayerAnimController to the game scene.
 Call it playerGO and hit play. The sprite should animate.

 The next assignment is to create a GameObject that can use these animations and a script to control it.



## 3. Player Input movement.
Next we will get input from the keyboard and use it to move the player accordingly.
Create a new C# script and call it PlayerControl
Drag the sript onto the PlayerGO object in the scene.

Script;
![](assets/UnityCodeClanCourseNotes-d5f78c0d.png)

![](assets/UnityCodeClanCourseNotes-ea34e045.png)

Set the speed of the ship in the inspector.
Run the game and check movement (WASD or arrows).


## 4. Player Bullet & Player Shooting.

 Next import the bullet sprite in the same way as the player sprite.
Drag the player bullet to the game scene and rename to PlayerBulletGO.
Create a script called PlayerBullet and drag it onto the PlayerBulletGO object.
Script;
```
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
    ```


Run the game. The bullet should travel to the top of the screen and be removed from the list of game objects.
Drag the PlayerGO object to the Prefabs folder. This will create a prefab of ready made bullets we can instantiate.
Once we have a prefab we can delete the bullet object from the scene.
Next, lets make the player fire when the space bar is hit. Since the bullet will fire from a position offset from the center of the player, lets create a new game object to represent it.
Create an object called BulletPosition1 then drag it to the position on the ship from which you want to fire.
Drag the BulletPosition1 object onto the PlayerGO object (in the side panel). This makes it a child of PlayerGo.

Add this to the PlayerControl script;

```
public GameObject PlayerBulletGo;
public GameObject BulletPosition01;
```


Select the playerGO object in the scene and drag the BulletPosition1 object onto the PlayerBulletGO definition in the inspector.
Save the scene.
Add the following to PlayerControl script (update function);
```
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
```

Save the scene and run the game. Pressing space should fire a bullet from the front of the playership.


## 5. Enemy Animation and Enemy Spawning.

Import your enemy sprite in the same manner as before.
Rename the animation controller to EnemyAnimController. Drag it to the scene and rename to EnemyGO.
Create a new script called EnemyControl and drag it over EnemyGO.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    //Enemy Speed
    float speed;

    int lives = 3;

	// Use this for initialization
	void Start () {
        //Set speed
        speed = 2f;
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
}
```
Save and run the game. The enemy should move from right to left and be destroyed after leaving the screen on the left.
Create a prefab from the enemy object.
Drag the EnemyGO object to the prefabs folder.
Delete the instance from the scene.


Create an empty game object and call in EnemySpawner.
Create a new script called EnemySpawner and drag it onto the EnemySpawner game object.

Open the script and declare GameObject called EnemyGO.
In the inspection window drag an enemy game object onto the newly created option.
Script;
(Note. This script is set up to choose from multiple enemy sprites and instantiate randomly).

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemySleekGo;
    public GameObject EnemyCircleGo;
    public GameObject EnemyJavelinGo;
    public GameObject EnemyTriangleGo;
    public GameObject EnemySmallGo;
    public GameObject EnemyBossGo;

    public GameObject[] Enemies;


    float maxSpawnRateInNSeconds = 5f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


	}

    //Function will create and enemy prefab position on the right hand side of the screen and randomly between the top and bottom.
    void SpawnEnemy(){

        //Get bottom left of screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //UnitySystemConsoleRedirector.Redirect();
        //Debug.Log(min);
        //Get top right of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //UnitySystemConsoleRedirector.Redirect();
        //Debug.Log(min);

        //Create random number for array selection
        int randomSelection = Random.Range(0, 6);

        //Get random object from enemies array.
        //Instantiate an enemy
        GameObject anEnemy = (GameObject)Instantiate(Enemies[randomSelection]);
        float randomY = Random.Range(min.y + anEnemy.GetComponent<SpriteRenderer>().sprite.bounds.extents.y , max.y - (anEnemy.GetComponent<SpriteRenderer>().sprite.bounds.extents.y)*4);
        UnitySystemConsoleRedirector.Redirect();
        Debug.Log(randomY);
        anEnemy.transform.position = new Vector2 (max.x, randomY);

        //Schedule when to spawn next enemy.
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn(){
        float spawnInNSeconds;

        if (maxSpawnRateInNSeconds > 1f)
        {
            //Pick a number between 1 and maxSpawnRateInSeconds
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInNSeconds);
        }
        else
            spawnInNSeconds = 1f;
        Invoke("SpawnEnemy", spawnInNSeconds);

    }

    //Function to increase the difficuklty of the game
    void IncreaseSpawnRate(){
        if (maxSpawnRateInNSeconds > 1f)
            maxSpawnRateInNSeconds--;
        if (maxSpawnRateInNSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    //Function to start enemy spawner
    public void ScheduleEnemySpawner(){

        //Reset max spawn rate
        maxSpawnRateInNSeconds = 5f;


        Invoke("SpawnEnemy", maxSpawnRateInNSeconds);

        //Increase spawn rate every 30 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }


    //Function to stop enemy spawner.
    public void UnscheduleEnemySpawner(){
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

}
```

## 6. Enemy Bullets and Enemy Shooting.
Drag an enemy bullet sprite to the game scene and rename it EnemeyBulletGO.
Create a C# script called EnemyBullet.
Drag the script onto the EnemyBulletGO.

Script;
```
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

```
Run the game to check for errors then create the prefab. Delete the instance of EnemeyBulletGO from the scene.

(Hide the camera to see better, gizmos - camera.)
Create a new game object called EnemeyGunGO.
Position this game object where the enemy's gun should be.
Create a new script. Call it EnemyGun. Add the script to the EnemyGunGO instance.
EnemyGun script;

```
//Enemy bullet prefab
public GameObject EnemyBulletGo;
```
Grab the EnemyBullet prefab and drag it to here.
Next drag the EnemyGunGO onto the EnemyGO.
Click on the EnemyGO and click apply to save these changes to the prefab.



```
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
        GameObject playerShip = GameObject.Find("PlayerGo");

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

```

After updating the script remember to click apply on the EnemyGO/EnemeyGunGO etc to ensure changes apply to the prefab.

Run the game and you should see an enemy spawn and fire at the player.



## 7. Collision detection and explosion animation.
Import the explosion animation and do the usual steps.
Rename the controller to ExplosionAnimController.
Create an instance of it in the game scene and rename to ExplosionGO.
Create a prefab.

If you add this to the scene you may notice a couple of issues.
1. The animation keeps looping.
This can be switched off within the inspector.
2. The last frame of the animation stays on the screen. To remove this we will create a script called Destroyer. (Dramatic!).

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    void DestroyGameObject(){
        Destroy(gameObject);
    }

}
```

Add the destroyer script to ExplosionGO.
The explosion animation will call the function in Destroyer.
Click on ExplosionGO then click the animations tab.
Add an animation event after all the animation frames.
Right click on the gray area and select Add Animation Event.
A window should appear. In the dropdown there should be the option to call the DestroyGameObject function.
Run the game again to test the explosion.
If all is working ok, remember to update/create the ExplosionGO prefab and remove from the scene.

Next we're going to implement
# Collision Detection.

First lets add tags to our game objects (this will help us identify them in our scripts).
In the inspector click tags & layers.
Add the following tags;
PlayerShipTag
EnemyShipTag
PlayerBulletTag
EnemyBulletTag

Click on the playerGO and assign it the PlayerShipTag.
Assign the appropriate tags to our prefabs.

Next we'll add physics components to our game objects.
Click on the PlayerGo object then in the inspector click add Component - Physics 2D - box collider2d.
Next resize the collider to fit round the edges of the game object. (Use the sliders in the inspector).
Check the 'is trigger' box.
Now, still clicked on the PlayerGO add a rigid body 2d.
Set the gravity scale to 0.
click 'fixed angle' on.

Repeat the process for the enemy sprite(s), bullets and player bullets.


Ok, lets head over to the PlayerControl Script.
This function will trigger on collision;
```
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
```

open the EnemyControl script and add the following;
```
private void OnTriggerEnter2D(Collider2D col){
    if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
    {
        lives--;
        PlayExplosion();

        if (lives <= 0)
        {
            //Add 100 points to the score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            //Destroy enemy ship.
            Destroy(gameObject);
        }

    }
}
```
Open the PlayerBullet script and add the following;
```
private void OnTriggerEnter2D(Collider2D col){
    if((col.tag == "EnemySleekTag") || (col.tag == "EnemyJavelinTag")
        || (col.tag == "EnemyCircleTag") || (col.tag == "EnemySmallTag")
        || (col.tag == "EnemyTriangleTag") || (col.tag == "EnemyTriangleTag")
        || (col.tag == "EnemyBossTag")){
            Destroy(gameObject);
        }

}
```

Open the EnemyBullet script and add the following;
```
private void OnTriggerEnter2D(Collider2D col){
    //Detect collision with an enemy's bullet and the playerShiop
    if(col.tag == "PlayerShipTag"){
        //Destroy this bullet
        Destroy(gameObject);
    }
}
```

Build and the play game. Collisions should be working.

## Adding Explosions.

In the PlayerControl script add;
```
public GameObject ExplosionLargeGo;
```
In the inspector drag the explosion prefab onto the new field in the inspector.
Next add;
```
//Instantiate an explosion
void PlayExplosion(){
    GameObject explosion = (GameObject)Instantiate(ExplosionLargeGo);
    //Set the position of the explosion
    explosion.transform.position = transform.position;
}
```
Inside the OnTriggerEnter2D function add;
```
PlayExplosion();
```

Next, follow the same procedure in the EnemyControl script.


## 8. Game Manager (Lives & UI)
Lets create some UI components like a play button to start the game and lives to show the player's remaining life.


Import the player lives and score sprites.
Create a UI Image component for the lives.
New Game Object - UI - Image
Notice unity creates a canvas for our image ui and an event system game object.

Click on the canvas and place it on the desired location in the scene.
Drag the sprite onto the source image field in the inspector.
For sizing options, in the inspector go to Canvas scaler - UI Scale Mode - scale with screen size.
Our game screen reference resolution is 1136 x 640 (iphone landscape).
When happy, click Set Native Size in the inspector.
Set the anchor point of the image to the top right of the screen.
Rename the image object to ImageLivesUI
Right click on the canvas to add another UI for the score and follow the same procedure.


Right click on the canvas and create a UI Text. Drag this onto the lives sprite. This will represent the digits.
In the inspector set the font size and color. Set the text to 0.
Rename to textLives.
Make the textLives a child of ImageLivesUI.

Repeat the process for the score digits calling it TextScore.

Right click on the canvas and create a UI button for the play button.
Name the button ButtonPlay.
In the inspector click transition - sprite swap.
Delete the text object from our button.
Import the play button and play button highlighted sprites.
Make the play button source image and the highlighted button the pressed sprite (under transition).
When we run the game, the play button should highlight when clicked. We'll implement the functionality a bit later.
Set the anchor point of the play button to the bottom centre.


Now lets create the Game Manager.
Create an empty game object and call it GameManagerGO.
Create a script called GameManager and attach it to the game object.
Inside the script create references to our game objects and this enum;
```
public GameObject playButton;
public GameObject playerShip;

public enum GameManagerState{
    Opening,
    Gameplay,
    GameOver
}

GameManagerState GMState;

// Use this for initialization
void Start () {
      GMState = GameManagerState.Opening;
}
```

Drag the gameManager script inside the GameManagerGO.
Drag the play button and the player ship to the new fields in the inspector created by the script.
Add the update game manager update function.
```
//Function to update the GameManager State.
void UpdateGameManagerState(){
    switch(GMState){
        case GameManagerState.Opening:
            break;
        case GameManagerState.Gameplay:
            break;
        case GameManagerState.GameOver:
            break;
    }
}
```

IN the player control script create a reference to the lives UI.
```
//Reference to the lives ui text
public Text LivesUIText;
```
Then create the following;
```
//Reference to the lives ui text
public Text LivesUIText;
//Maximum player lives;
const int MaxLives = 3;
//Current number of lives;
int lives;
```

Update the OnTriggerEnter2D
```
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
```



    Back in the GameManager script, add a function to set the game manager state.
```
//Function to set the GameManagerState
public void SetGameManagerState(GameManagerState state){
    GMState = state;
    UpdateGameManagerState();
}
```

In the player control script, add a reference to the game manager.
```
//References Game Manager
public GameObject GameManagerGO;
```
Using the new field created in the inspector, drag the game manager to the corresponding field in the inspector.

Update the OnTriggerEnter2D to switch the game manager state.
```
if (lives == 0)
{
    //Change game manager state to game over state
    GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
    //Hide the player's ship if out of lives
    gameObject.SetActive(false);
}
```
In the inspector set the PlayerGO to inactive to hide it when the game starts.


In game manager, lets write a function to handle the play button being clicked.
```
//Play button will call this function when clicked
public void StartGamePlay(){
    GMState = GameManagerState.Gameplay;
    UpdateGameManagerState();

}
```
Update the GameManager state .gameplay to hide the button during game play.
```
//Hide play button on game play state
playButton.SetActive(false);
//Set the player visible active and init playe
playerShip.GetComponent<PlayerControl>().Init();
```
Now lets make the button call this function;
select the button and in the inspector click on-click. drag the game manager object in.
Select the function from the drop down.

Build and run to test.
(Might need to check the player control script (in the inspector) has a LivesUIText dragged into position.)
On clicking play, the button should dissapear and the player ship appear.


Now, lets stop the enemy spawner when the player dies.
In the game manager script update the GameManagerState.GameOver;
```
case GameManagerState.GameOver:

    //Stop the time counter
    TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

    //Stop enemy spawner
    enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

    //Display game over
    GameOverGO.SetActive(true);


    //Change game manager state to opening state after 8 seconds
    Invoke("ChangeToOpeningState", 8f);
    break;
```

Updagte the game manager state.opening.
```
case GameManagerState.Opening:

    //Hide game over
    GameOverGO.SetActive(false);

    //Set title active
    title.SetActive(true);

    //Set play button visible active
    playButton.SetActive(true);

    break;
```
In the EnemeySpawner script add the following;
```
//Function to stop enemy spawner.
public void UnscheduleEnemySpawner(){
    CancelInvoke("SpawnEnemy");
    CancelInvoke("IncreaseSpawnRate");
}
```
Then add a function to start the enemy spawner.
```
//Function to start enemy spawner
public void ScheduleEnemySpawner(){

    //Reset max spawn rate
    maxSpawnRateInNSeconds = 5f;


    Invoke("SpawnEnemy", maxSpawnRateInNSeconds);

    //Increase spawn rate every 30 seconds
    InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
}
```
Note: May need to remove these lines from the start function.

In the GameManager script, declare an EnemySpawner and add;
```
    public GameObject enemySpawner;

    case GameManagerState.Gameplay:

    //Reset the score
    scoreUITextGO.GetComponent<GameScore>().Score = 0;

    //Hide play button on game play state
    playButton.SetActive(false);

    //Hide title
    title.SetActive(false);

    //Set the player visible active and init playe
    playerShip.GetComponent<PlayerControl>().Init();

    //Start enemy spawner
    enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

    break;
  ```
  Update the GameManagerState.Gameover.
  ```
  case GameManagerState.GameOver:

    //Stop the time counter
    TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

    //Stop enemy spawner
    enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

    //Display game over
    GameOverGO.SetActive(true);


    //Change game manager state to opening state after 8 seconds
    Invoke("ChangeToOpeningState", 8f);
    break;
  ```

  Add a function to change the gameManager state to opening.
```
  //Function to change game manager to opening state
public void ChangeToOpeningState(){
    SetGameManagerState(GameManagerState.Opening);
}
```






















 8. UI & play button.
 9. Sound Effects and music.
 10. Scrolling Background.
 11. Timer & Game Tiles.
