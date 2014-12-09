using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	protected enum Facing{
		Left,
		Right
	};
	
	protected enum Mode{
		Approach,
		Wait,
		Attack,
		Retreat,
		Dying
	};

	float droppingDeadSpeed = 15.0f;
	float waitingUntil;

	protected Mode _mode;


	protected void MonsterUpdate (){
		if (_mode == Mode.Dying)
			Die();
		else if (_mode == Mode.Wait)
			Wait();
	}

	void Wait(){

		if (Time.time >= waitingUntil){
			_mode = Mode.Attack;
		}
	}

	void Die(){
		float step = droppingDeadSpeed * Time.deltaTime;
		transform.position = transform.position - new Vector3(0.0f, step, 0.0f);

		if (transform.position.y < -5.0f)
			Destroy(this.gameObject);
	}

	protected void WaitUntil( float time ){
		waitingUntil = time;
		_mode = Mode.Wait;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//	if (coll.gameObject.tag == "Enemy")
		//		coll.gameObject.SendMessage("ApplyDamage", 10);
		_mode = Mode.Dying;
		Destroy( coll.gameObject );
		
	}



}
