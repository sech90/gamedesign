using UnityEngine;
using System.Collections;

public class FlyingLion : Monster 
{
	public AudioClip AttackSound = null;

	float _attackSpeed = 5.0f;
	float _approachSpeed = 1.0f;
	float _retreatSpeed = 2.0f;
	float _waitTime = 6.0f;



	GameObject _shipAttackSpot; //GameObject towards which the monster is attacking
	Vector3 _target;			// Position towards which the monster is moving

	public float droppingDeadAccelleration = 0.5f;
	private float droppingDeadSpeed = 0.0f;
//	private float waitingUntil;


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

		Monster._headCount++;
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
		float x = Ship.instance.transform.position.x;
		if (_facing == MonsterFacing.Right)
			x += 15.0f;
		else
			x -= 15.0f;

		float y = Random.Range (5.0f, 8.0f);
		return new Vector3(x, y, 0.0f);
	}

	Vector3 RandomWaitPosition(){
		float x = Ship.instance.transform.position.x;
		if (_facing == MonsterFacing.Right)
		     x += Random.Range (3.0f, 6.0f);
		else
			x  -= Random.Range (3.0f, 6.0f);

		float y = Random.Range (6.0f, 10.0f);
		return new Vector3(x, y, 0.0f);
	}






}
