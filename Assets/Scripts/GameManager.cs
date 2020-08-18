using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Reference to game objects
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject title;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject healthSpawner;
    public GameObject TimeCounterGO;

 

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


    //Function to update the GameManager State.
    void UpdateGameManagerState(){
        switch(GMState){
            case GameManagerState.Opening:
                
                //Hide game over
                GameOverGO.SetActive(false);

                //Set title active
                title.SetActive(true);

                //Set play button visible active
                playButton.SetActive(true);
                
                break;
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

                //Start health spawner
                healthSpawner.GetComponent<HealthSpawner>().ScheduleHealthSpawner();

                //Start the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;

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
                
        }
    }

    //Function to set the GameManagerState
    public void SetGameManagerState(GameManagerState state){
        GMState = state;
        UpdateGameManagerState();
    }
    //Play button will call this function when clicked
    public void StartGamePlay(){
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();

    }

    //Function to change game manager to opening state
    public void ChangeToOpeningState(){
        SetGameManagerState(GameManagerState.Opening);
    }


}
