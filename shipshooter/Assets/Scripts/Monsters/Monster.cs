using UnityEngine;
using System.Collections;

public enum MonsterFacing{
	Left,
	Right
};

public enum MonsterMode{
	Approach,
	Wait,
	Attack,
	Retreat,
	Dying
};

public class Monster : MonoBehaviour {
	
	public int AttackPower;
	public int PointsWhenKilled;
	public int MaxHp;
	
	protected int _currentHp;
	protected MonsterMode _mode;
	protected MonsterFacing _facing;				// Is monster facing left or right

	private float droppingDeadSpeed = 15.0f;
	private float waitingUntil;

	public MonsterMode Mode{get{return _mode;}}
	public MonsterFacing Direction{get{return _facing;}}

	static protected int _headCount = 0;

	static public int GetNumberOf()
	{
		return _headCount;
	}

	protected void MonsterUpdate (){
		if (_mode == MonsterMode.Dying)
			Die();
		else if (_mode == MonsterMode.Wait)
			Wait();
	}

	void Wait(){

		if (Time.time >= waitingUntil){
			_mode = MonsterMode.Attack;
		}
	}

	virtual protected void Die(){
		float step = droppingDeadSpeed * Time.deltaTime;
		transform.position = transform.position - new Vector3(0.0f, step, 0.0f);

		if (transform.position.y < -5.0f)
		{
			Destroy(this.gameObject);
			_headCount--;
		}
	}

	protected void WaitUntil( float time ){
		waitingUntil = time;
		_mode = MonsterMode.Wait;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		_mode = MonsterMode.Dying;
		Destroy( coll.gameObject );
		GameHandler.AddScore(PointsWhenKilled);
	}



}
