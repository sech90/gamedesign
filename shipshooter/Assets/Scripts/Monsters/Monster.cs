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
	protected float waitingUntil;

	public MonsterMode Mode{get{return _mode;}}
	public MonsterFacing Direction{get{return _facing;}}

	static public int _headCount = 0;

	static public int GetNumberOf()
	{
		return _headCount;
	}

	protected void MonsterUpdate (){
		if (_mode == MonsterMode.Dying)
			Die();
	}

	/*virtual protected void Wait(){

		if (Time.time >= waitingUntil){
			_mode = MonsterMode.Attack;
		}
	}*/

	protected bool FinishedWaiting(){
		return (Time.time >= waitingUntil);
	}

	virtual protected void Die(){


		FloatingObject fo = this.GetComponent<FloatingObject>();

		if (fo==null){
			float step = droppingDeadSpeed * Time.deltaTime;
			transform.position = transform.position - new Vector3(0.0f, step, 0.0f);
		}
		else{
			fo.isFloating = false;
			fo.isSinking = true;
		}

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

	public void StopAttacking(){
		if (GetComponent<Kraken>() == null)
			WaitUntil(Time.time + 9999);
	}



	protected void SetFacing( MonsterFacing facing) {
		_facing = facing;
		Vector3 scale = transform.localScale;

		if (_facing == MonsterFacing.Left) {
			scale.x = Mathf.Abs( scale.x );
		}else {
			scale.x = -Mathf.Abs( scale.x );
		}
		transform.localScale = scale;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		_mode = MonsterMode.Dying;
		Destroy( coll.gameObject );
		GameHandler.AddScore(PointsWhenKilled);
	}



}
