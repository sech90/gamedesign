using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Timers;

public delegate void Callback();

public class GameState : MonoBehaviour {

	private Timer _clockTimer;
	private int timeLeft = 400;
	private float _reloadTimer = 0.002f;
	private bool _marioDied = false;
	private float _timeAfterDeath;
	private float endTime;
	private Mario _mario;
	public Text score;
	public Text coins;
	public Text timer;

	void Awake(){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Player"),true);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Items"),LayerMask.NameToLayer("Items"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Enemies"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Items"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("RunningEnemies"),LayerMask.NameToLayer("Items"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("RunningEnemies"),LayerMask.NameToLayer("RunningEnemies"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("CameraWall"),LayerMask.NameToLayer("Items"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("CameraWall"),LayerMask.NameToLayer("Enemies"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("CameraWall"),LayerMask.NameToLayer("RunningEnemies"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("CameraWall"),LayerMask.NameToLayer("Player"),false);
	}

	void Start () {
		_mario = FindObjectOfType<Mario>();
		_mario.OnMarioDie += MarioDied;
		endTime = Time.time + 400;
		_timeAfterDeath = -1;
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
		if(_marioDied){
			if(_timeAfterDeath == -1)
				_timeAfterDeath = Time.time;
			Debug.Log(Time.time - _timeAfterDeath);
			if(Time.time - _timeAfterDeath >= _reloadTimer){
				Time.timeScale = 1;
				Application.LoadLevel(Application.loadedLevel);

			}

		}
	}

	private void MarioDied(){
		Time.timeScale = 0.001f;
		_marioDied = true;
		Destroy(Camera.main.GetComponent<CameraFollow>());
	}

	void ReloadLevel(object source, ElapsedEventArgs e){
		Debug.Log("Reload level");
		_clockTimer.Stop();
		Time.timeScale = 1;
		//Application.LoadLevel(Application.loadedLevel);
	}
}
