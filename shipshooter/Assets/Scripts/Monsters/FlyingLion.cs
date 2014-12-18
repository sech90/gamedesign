using UnityEngine;
using System.Collections;

public class FlyingLion : Monster 
{
	public AudioClip AttackSound = null;

	static public int _headCount = 0;

	public float _attackSpeed = 4.0f;
	public float _approachSpeed = 0.4f;
	public float _retreatSpeed = 2.0f;
	float _waitTime = 6.0f;



	GameObject _shipAttackSpot; //GameObject towards which the monster is attacking
	Vector3 _target;			// Position towards which the monster is moving

	public float droppingDeadAccelleration = 0.5f;
	private float droppingDeadSpeed = 0.0f;
//	private float waitingUntil;

	static public int GetNumberOf()
	{
		return _headCount;
	}


	// Use this for initialization
	void Start () {
		// Select at random whether monster comes from left or right
		if (Random.value > 0.5f){
			SetFacing( MonsterFacing.Right);
			//_facing = MonsterFacing.Right;
		}
		else{
				SetFacing( MonsterFacing.Left);

			/*
			_facing = MonsterFacing.Left;
			// Mirror the sprite
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;*/
		}

		transform.position = RandomStartPosition();
		_mode = MonsterMode.Approach;
		_target = RandomWaitPosition();

		if (_facing == MonsterFacing.Right)
			_shipAttackSpot = GameObject.Find("FlyingLionAttackSpotLeft");
		else
			_shipAttackSpot = GameObject.Find("FlyingLionAttackSpotRight");

		if (_shipAttackSpot == null)
			Debug.LogError( "ERROR: Attack spot for Flying Lion not found");

		_headCount++;
	}

	void Update () {

		if (_mode == MonsterMode.Approach)
			Approach();
		else if (_mode == MonsterMode.Attack)
			Attack();
		else if (_mode == MonsterMode.Retreat)
			Retreat();
		else if (_mode == MonsterMode.Dying)
			Die();

		if (_mode == MonsterMode.Wait && FinishedWaiting()){
			_mode = MonsterMode.Attack;
			AudioSource.PlayClipAtPoint(AttackSound,transform.position);
		}
	}

	override protected void Die(){
	
		droppingDeadSpeed += droppingDeadAccelleration * Time.deltaTime;
		transform.position += new Vector3(0.0f, -droppingDeadSpeed, 0.0f );
		//sinkingRollSpeed += sinkingRollAcceleration * Time.deltaTime;
		

		float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 180.0f, 5.0f);
		transform.eulerAngles = new Vector3(0, 0, angle);
	
		
		if (transform.position.y < -5.0f)
		{
			Destroy(this.gameObject);
			_headCount--;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		_mode = MonsterMode.Dying;
		Destroy( coll.gameObject );
		GameHandler.AddScore(PointsWhenKilled);
	}


	void Approach(){
//		MoveStraightTowards(_target, _approachSpeed);

		float x = Mathf.MoveTowards( transform.position.x, Ship.instance.transform.position.x, 
		                            _approachSpeed * Time.deltaTime );
		transform.position = new Vector3(x, transform.position.y, transform.position.z );

		if ( Mathf.Abs( Ship.instance.transform.position.x - x) < 4.0f )
		{
			_mode = MonsterMode.Attack;
			AudioSource.PlayClipAtPoint(AttackSound,transform.position);
		}

//		if (transform.position == _target){
//			WaitUntil( Time.time + _waitTime );
	//	}
	}

	void Retreat(){
		MoveStraightTowards(_target, _retreatSpeed);
		if (transform.position == _target){
			//WaitUntil( Time.time + _waitTime );
			Destroy(this.gameObject);
			_headCount--;
		}
	}

	void Attack()
	{
		_target = _shipAttackSpot.transform.position;

		MoveStraightTowards(_target, _attackSpeed);
		
		if (transform.position == _target){
			_mode = MonsterMode.Retreat;
			_target = RetreatPosition();
			if (_facing == MonsterFacing.Right ) {
				SetFacing( MonsterFacing.Left);
				Ship.instance.TakeDamage( AttackPower, 0);
			}
			else {
				SetFacing( MonsterFacing.Right);
				Ship.instance.TakeDamage( AttackPower, 1);
			}
		}
	}
	

	void MoveStraightTowards(Vector3 position, float speed){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, position, step);
	}


	Vector3 RandomStartPosition(){
		float x = Ship.instance.transform.position.x;
		if (_facing == MonsterFacing.Left)
			x += 15.0f;
		else
			x -= 15.0f;

		float y = Random.Range (5.0f, 9.0f);
		return new Vector3(x, y, 0.0f);
	}

	Vector3 RetreatPosition(){
		float x = Ship.instance.transform.position.x;

		if (_facing == MonsterFacing.Right)
			return new Vector3(x-15.0f, 15.0f, 0.0f);
		else
			return new Vector3(x+15.0f, 15.0f, 0.0f);
	}

	Vector3 RandomWaitPosition(){
		float x = Ship.instance.transform.position.x;
		if (_facing == MonsterFacing.Left)
		     x += Random.Range (3.0f, 6.0f);
		else
			x  -= Random.Range (3.0f, 6.0f);

		float y = Random.Range (6.0f, 10.0f);
		return new Vector3(x, y, 0.0f);
	}






}
