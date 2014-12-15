using UnityEngine;
using System.Collections;

public class Kraken : Monster {

	public AudioClip AttackSound = null;
	public float xSpeed = 0.5f;
	public float attackYSpeed  = 2.0f;
	public float attackHeight = -2.0f;
	public float swimHeight = -3.0f;
	public float upWaitTime = 2.0f;

	private Transform _tentacle1 = null;
	private Transform _tentacle2 = null;
	private Transform _tentacle3 = null;

	private float _height;

	// Use this for initialization
	void Start () {
		_tentacle1 = transform.FindChild("tentacle_1_1");
		_tentacle2 = transform.FindChild("tentacle_2_1");
		_tentacle3 = transform.FindChild("tentacle_3_1");
		_mode = MonsterMode.Approach;
		_height = swimHeight;
		SetTentacleHeight(_height);
	}
	
	// Update is called once per frame
	void Update () {
		if (_mode == MonsterMode.Approach)
			Approach();
		if (_mode == MonsterMode.Attack)
			Attack();
		//if (_mode == MonsterMode.Wait)
		//	Wait();

		if ( IsUnderShip() && _mode == MonsterMode.Approach) {
			_mode = MonsterMode.Attack;
			AudioSource.PlayClipAtPoint(AttackSound,transform.position);
		}
		/*
		if ( _height == attackHeight && _mode == MonsterMode.Attack ){
			WaitUntil( Time.time + upWaitTime );
		}*/

	}

	void Attack() {
		_height = Mathf.MoveTowards( _height, attackHeight, attackYSpeed * Time.deltaTime );
		SetTentacleHeight( _height );
	}

	void Retreat() {
		_height = Mathf.MoveTowards( _height, swimHeight, attackYSpeed * Time.deltaTime );
		SetTentacleHeight( _height );
	}

	void Approach() {
		Vector3 shipPos = Ship.instance.transform.position;
		float xKraken = Mathf.MoveTowards( transform.position.x, shipPos.x, xSpeed * Time.deltaTime );
		transform.position = new Vector3( xKraken, transform.position.y, transform.position.z); 
	}

	bool IsUnderShip(){
		Vector3 shipPos = Ship.instance.transform.position;
		return ( Mathf.Abs( shipPos.x - transform.position.x ) < 0.1f );
	}


	void SetTentacleHeight( float height )
	{
		_tentacle1.GetComponent<FloatingObject>().yAdjustment = height;
		_tentacle2.GetComponent<FloatingObject>().yAdjustment = height;
		_tentacle3.GetComponent<FloatingObject>().yAdjustment = height + 0.2f;
	}
}
