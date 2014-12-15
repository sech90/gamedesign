using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameHandler : MonoBehaviour {

	private float lastSpawn = 0.0f;
	private float spawnRate = 3.0f;
	private int maxFlyingMonsters = 4;
	private GameObject flyingLionPrefab;
	private GameObject _gameOverPanel;
	private bool _gameover = false;


	private static float _score = 0;

	public int ScorePerSecond = 50;
	public Text ScoreText;

	public float Score{get{return _score;}}

	// Use this for initialization
	void Awake () 
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Sailorman"), LayerMask.NameToLayer("WaterInteract"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("Sailorman"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("InteractiveObj"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("WaterInteract"), true);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("Monsters"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("InteractiveObj"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("WaterInteract"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Markers"), LayerMask.NameToLayer("InteractiveObj"), true); 

		flyingLionPrefab = Resources.Load<GameObject>("FlyingLion");
		_gameOverPanel = GameObject.Find("/InGameUI/GameOverPanel");
		_score = 0;
	}

	public static void AddScore(int amount){
		_score += amount;
	}

	void Update()
	{


		if (UserInput.Exit()){
			Application.Quit();
		}

		if(Ship.instance.CurrentHp == 0 && !_gameover){
			setGameOver();
			return;
		}
		else if(_gameover){
			if(Input.GetKeyDown(KeyCode.R))
				Application.LoadLevel("MainScene");
			return;
		}

		_score += ScorePerSecond * Time.deltaTime;
		ScoreText.text = ((int)_score).ToString();
		
		if (Time.time > lastSpawn + spawnRate && Monster.GetNumberOf() < maxFlyingMonsters)
		{
			Instantiate (flyingLionPrefab);
			lastSpawn = Time.time;
		}
	}

	private void setGameOver(){
		_gameover = true;
		Monster[] monsters = Transform.FindObjectsOfType<Monster>();
		for(int i=0; i<monsters.Length; i++)
			monsters[i].StopAttacking();

		Monster._headCount = 0; //HACK!!!!!
		_gameOverPanel.GetComponent<FadeEffect>().Fade();
		AudioSource audio = GameObject.FindWithTag("SoundTrack").GetComponent<AudioSource>();
		audio.Stop();
	}
	
}
