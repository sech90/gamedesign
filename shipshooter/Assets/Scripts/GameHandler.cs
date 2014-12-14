using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameHandler : MonoBehaviour {

	float lastSpawn = 0.0f;
	float spawnRate = 3.0f;
	GameObject flyingLionPrefab;
	int maxFlyingMonsters = 4;

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
	}

	public static void AddScore(int amount){
		_score += amount;
	}

	void Update()
	{
		_score += ScorePerSecond * Time.deltaTime;
		ScoreText.text = ((int)_score).ToString();

		if (Time.time > lastSpawn + spawnRate && Monster.GetNumberOf() < maxFlyingMonsters)
		{
			GameObject flyingLion = Instantiate (flyingLionPrefab) as GameObject;
			lastSpawn = Time.time;
		}

		if (UserInput.Exit())
			Application.Quit();

	

	}
	
}
