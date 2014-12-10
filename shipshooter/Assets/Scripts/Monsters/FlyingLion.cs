using UnityEngine;
using System.Collections;

public class FlyingLion : Monster 
{
	float _attackSpeed = 5.0f;
	float _approachSpeed = 2.0f;
	float _retreatSpeed = 2.0f;
	float _waitTime = 3.0f;

	GameObject _shipAttackSpot; //GameObject towards which the monster is attacking
	Vector3 _target;			// Position towards which the monster is moving
	MonsterFacing _facing;				// Is monster facing left or right


	// Use this for initialization
	void Start () {
		// Select at random whether monster comes from left or right
		if (Random.value > 0.5f){
			_facing = MonsterFacing.Right;
		}
		else{
			_facing = MonsterFacing.Left;
			// Mirror the sprite
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		transform.position = RandomStartPosition();
		_mode = MonsterMode.Approach;
		_target = RandomWaitPosition();

		if (_facing == MonsterFacing.Right)
			_shipAttackSpot = GameObject.Find("FlyingLionAttackSpotRight");
		else
			_shipAttackSpot = GameObject.Find("FlyingLionAttackSpotLeft");

		if (_shipAttackSpot == null)
			Debug.LogError( "ERROR: Attack spot for Flying Lion not found");
	}

	void Update () {

		if (_mode == MonsterMode.Approach)
			Approach();
		else if (_mode == MonsterMode.Attack)
			Attack();
		else if (_mode == MonsterMode.Retreat)
			Retreat();
	
		MonsterUpdate();
	
	}

	void Approach(){
		MoveStraightTowards(_target, _approachSpeed);
		if (transform.position == _target){
			WaitUntil( Time.time + _waitTime );
		}
	}

	void Retreat(){
		MoveStraightTowards(_target, _retreatSpeed);
		if (transform.position == _target){
			WaitUntil( Time.time + _waitTime );
		}
	}

	void Attack()
	{
		_target = _shipAttackSpot.transform.position;

		MoveStraightTowards(_target, _attackSpeed);
		
		if (transform.position == _target){
			_mode = MonsterMode.Retreat;
			_target = RandomWaitPosition();
		}
	}
	

	void MoveStraightTowards(Vector3 position, float speed){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, position, step);
	}


	Vector3 RandomStartPosition(){
		float x = ShipHandler.instance.transform.position.x;
		if (_facing == MonsterFacing.Right)
			x += 15.0f;
		else
			x -= 15.0f;

		float y = Random.Range (5.0f, 8.0f);
		return new Vector3(x, y, 0.0f);
	}

	Vector3 RandomWaitPosition(){
		float x = ShipHandler.instance.transform.position.x;
		if (_facing == MonsterFacing.Right)
		     x += Random.Range (3.0f, 6.0f);
		else
			x  -= Random.Range (3.0f, 6.0f);

		float y = Random.Range (6.0f, 10.0f);
		return new Vector3(x, y, 0.0f);
	}





}
