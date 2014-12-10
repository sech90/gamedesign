using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameHandler : MonoBehaviour {

	private static float _score = 0;

	public int ScorePerSecond = 50;
	public Text ScoreText;
	public AudioClip MainSoundtrack;
	public AudioClip StartScreenAudio;

	public float Score{get{return _score;}}

	// Use this for initialization
	void Awake () 
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("Sailorman"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("InteractiveObj"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ship"), LayerMask.NameToLayer("WaterInteract"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("Monsters"), true); 
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monsters"), LayerMask.NameToLayer("InteractiveObj"), true); 



		GameObject flyingLionPrefab = Resources.Load<GameObject>("FlyingLion");
		Instantiate (flyingLionPrefab);
		Instantiate (flyingLionPrefab);
		Instantiate (flyingLionPrefab);
		//flyingLion.transform.position = new Vector3(9.0f, 2.0f, 0.0f);

		/** /
		GameObject cameraPrefab = Resources.Load<GameObject>("Main Camera");
		GameObject shipPrefab = Resources.Load<GameObject>("Ship");
		GameObject sailorPrefab = Resources.Load<GameObject>("Sailor");

		GameObject ship = Instantiate (shipPrefab) as GameObject;
		GameObject sailor = Instantiate (sailorPrefab) as GameObject;
		GameObject camera = Instantiate (cameraPrefab) as GameObject;
		camera.GetComponent<CameraFollow2d>().targetGameObject = ship;

		ship.transform.position = new Vector3(7.0f, 0.0f, 0.0f);
		sailor.transform.parent = ship.transform;
		sailor.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);
		/**/
		AudioSource.PlayClipAtPoint(MainSoundtrack,transform.position);
	}

	void Update(){
		_score += ScorePerSecond * Time.deltaTime;
		ScoreText.text = ((int)_score).ToString();
	}

	public static void AddScore(int amount){
		_score += amount;
	}
	
}
