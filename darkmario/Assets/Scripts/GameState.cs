using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void Callback();

public class GameState : MonoBehaviour {

	// Use this for initialization
	private int timeLeft = 400;
	private float endTime;
	private Mario _mario;
	public Text score;
	public Text coins;
	public Text timer;

	void Awake(){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Player"),true);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Enemies"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Items"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("RunningEnemies"),LayerMask.NameToLayer("Items"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("RunningEnemies"),LayerMask.NameToLayer("RunningEnemies"));
	}

	void Start () {
		_mario = FindObjectOfType<Mario>();
		_mario.OnMarioDie += MarioDied;
		endTime = Time.time + 400;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeLeft > 0){
			timeLeft = (int)(endTime - Time.time);
			score.text = _mario.Score.ToString();
			coins.text = _mario.Coins.ToString();
			timer.text = timeLeft.ToString();
		}
		else if(timeLeft == 0)
		{
			_mario.Die();
			timeLeft--;
		}
	}

	private void MarioDied(){
		//Time.timeScale = 0;
		Destroy(Camera.main.GetComponent<CameraFollow>());
		StartCoroutine(ReloadLevel());
	}

	IEnumerator ReloadLevel(){
		yield return new WaitForSeconds(6.0f);
		Application.LoadLevel(Application.loadedLevel);
	}
}
