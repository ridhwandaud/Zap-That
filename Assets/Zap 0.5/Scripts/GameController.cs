using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	private int score = 0; //For count score
	public Text scoreText; //For show score
	public Text newHighScoreText; //For show when player got high score
	public Text gameoverScoreText; //For show score when gameover
	public Text gameoverHighscoreText; //For show highscore when gameover
	public Canvas gameoverGUI; //For show GUI of gameover
	public Canvas ingameGUI;  //For show GUI when play (pause button ,scoreText)
	public Canvas pauseGUI; //For show GUI when pause
	public static GameController instance; //Instance

	//Ads
	public int i;
	public int adsShow;
	public string gameId; // Set this value from the inspector.
	public bool enableTestMode = true;
	public bool adsShowing = true;
	public UnityAdsExample ads;

	//Level
	public GameObject enemyPrefab;
	public Vector2 enemySpawnPosition;
	float difficulty;

	void Awake(){
		instance = this;
		difficulty = 0;
		CheckPlayTime ();
		SpawnEnemy ();
	}

	void Update()
	{
		if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
			SpawnEnemy ();
		}
	}

	void SpawnEnemy()
	{
		if (score > 10) {
			difficulty = score / 20;

		}

		if (difficulty > enemySpawnPosition.y) {
			difficulty = enemySpawnPosition.y;
		}
		Vector3 spawnPosition = new Vector3 (Random.Range(-enemySpawnPosition.x,enemySpawnPosition.x), Random.Range (difficulty,enemySpawnPosition.y), 0.0f); //Position for spawn
		Instantiate (enemyPrefab,spawnPosition,Quaternion.identity);
	}

	void CheckPlayTime()
	{
		if (PlayerPrefs.HasKey ("PlayTime") == false) {
			PlayerPrefs.SetInt("PlayTime",1);
			i=1;
		}

		i = PlayerPrefs.GetInt ("PlayTime");

		i++;

		PlayerPrefs.SetInt("PlayTime",i);

	}

	public void addScore(){
		print ("score" + score);
		score++; //Plus score
		scoreText.text = score.ToString (); //Change scoreText to current score
	}

	public void GameOver(){

		CheckHighScore ();
		gameoverScoreText.text = score.ToString(); //Set score to gameoverScoreText
		gameoverHighscoreText.text = PlayerPrefs.GetInt ("highscore", 0).ToString(); //Set high score to gameoverHighscoreText
		gameoverGUI.gameObject.SetActive(true); //Show gameover's GUI
		ingameGUI.gameObject.SetActive(false); //Hide ingame's GUI

		//ads here
		//interstitial.ShowInterstitial ();
		adsShow = PlayerPrefs.GetInt ("PlayTime");
		if (adsShow > 5 && adsShowing) 
		{
			PlayerPrefs.SetInt("PlayTime",1);
			adsShowing = false;
			ads.ShowAd ();
			return;
			// Wait until Unity Ads is initialized,
			//  and the default ad placement is ready.
		}

	}

	public void CheckHighScore(){
		if( score > PlayerPrefs.GetInt("highscore",0) ){ //If score > highscore
			PlayerPrefs.SetInt("highscore",score); //Save a new highscore
			newHighScoreText.gameObject.SetActive(true); //Enable newHighScoreText
		}
	}

	public void Pause(){
		Time.timeScale = 0; //Change timeScale to 0
		pauseGUI.gameObject.SetActive (true); //Show pauseGUI
	}

	public void Resume(){
		Time.timeScale = 1; //Change timeScale to 1
		pauseGUI.gameObject.SetActive (false); //Hide pauseGUI
	}
}
