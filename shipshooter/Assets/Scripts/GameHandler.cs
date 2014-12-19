﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameHandler : MonoBehaviour {

	private float lastSpawn = 0.0f;
	private float spawnRate = 3.0f;
	private float lastLizSpawn = 0.0f;
	private float lizSpawnRate = 6.0f;
	private int maxFlyingMonsters = 4;
	private int maxSwimmingMonsters = 4;

	private GameObject flyingLionPrefab;
	private GameObject lizPrefab;
	private FadeEffect _gameOverEffect;
	private FadeEffect _blackPanelEffect;
	private bool _gameover = false;


	private static float _score = 0;

	public int ScorePerSecond = 50;
	public Text ScoreText;
	public Text SecondScoreText;
	public GameObject TextDecor;

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
		lizPrefab = Resources.Load<GameObject>("Lizard_monster");
		_gameOverEffect = GameObject.Find("/InGameUI/GameOverPanel").GetComponent<FadeEffect>();
		_blackPanelEffect = GameObject.Find("/InGameUI/BlackEndPanel").GetComponent<FadeEffect>();
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
			if(Input.GetKeyDown(KeyCode.Return))
				Application.LoadLevel("StartScreen");
			return;
		}

		_score += ScorePerSecond * Time.deltaTime;
		ScoreText.text = "Score: "+((int)_score).ToString("D6");
		
		if (Time.time > lastSpawn + spawnRate && FlyingLion.GetNumberOf() < maxFlyingMonsters)
		{
			FlyingLion lion = Instantiate (flyingLionPrefab) as FlyingLion;
			lastSpawn = Time.time;

			if (Time.time > 180.0f) {
				lion.GetComponent<FlyingLion>()._approachSpeed *= 1.5f;
			}
			if (Time.time > 360.0f) {
				lion.GetComponent<FlyingLion>()._approachSpeed *= 1.5f;
			}

		}

		if (Time.time > lastLizSpawn + lizSpawnRate && LizardMonster.GetNumberOf() < maxSwimmingMonsters)
		{
			LizardMonster liz = Instantiate (lizPrefab) as LizardMonster;
			lastLizSpawn = Time.time;
			
			if (Time.time > 240.0f) {
				liz.xSpeed *= 2.0f;
			}
		}



	}

	private void setGameOver(){
		_gameover = true;
		Monster[] monsters = Transform.FindObjectsOfType<Monster>();
		for(int i=0; i<monsters.Length; i++)
			monsters[i].StopAttacking();

		FlyingLion	._headCount = 0; //HACK!!!!!
		LizardMonster._headCount = 0;

		float loadAfter = _gameOverEffect.FadeTime + _gameOverEffect.Delay + _blackPanelEffect.FadeTime + _blackPanelEffect.Delay;
		Invoke("ToTitleScreen",13);
		SecondScoreText.text = ScoreText.text;
		//TextDecor.SetActive(false);

		_gameOverEffect.Fade();
		_blackPanelEffect.Fade();

		AudioSource audio = GameObject.FindWithTag("SoundTrack").GetComponent<AudioSource>();
		audio.Stop();
	}

	private void ToTitleScreen(){
		Application.LoadLevel("StartScreen");
	}
}
