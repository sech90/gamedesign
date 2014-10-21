using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : MonoBehaviour {

	// Use this for initialization
	private int timeLeft = 400;
	private float endTime;
	private Mario _mario;
	public Text score;
	public Text coins;
	public Text timer;


	void Start () {
		_mario = FindObjectOfType<Mario>();
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
}
