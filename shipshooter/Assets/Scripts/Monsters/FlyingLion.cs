using UnityEngine;
using System.Collections;

public class FlyingLion : Monster 
{
	enum Side
	{
		Left,
		Right
	};
	
	enum Mode
	{
		Approach,
		Wait,
		Attack,
		Retreat
	};


	float attackSpeed = 5.0f;
	float approachSpeed = 2.0f;
	float retreatSpeed = 2.0f;
	float waitTime = 3.0f;

	GameObject shipAttackSpot; 
	Vector3 target;
	Side side;
	Mode mode;
	float waitingUntil;

	// Use this for initialization
	void Start () 
	{
		// Select at random whether monster comes from left or right
		if (Random.value > 0.5f)
		{
			side = Side.Right;
		}
		else
		{
			side = Side.Left;
			// Mirror the sprite
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		transform.position = RandomStartPosition();
		mode = Mode.Approach;
		target = RandomWaitPosition();

		if (side == Side.Right)
			shipAttackSpot = GameObject.Find("FlyingLionAttackSpotRight");
		else
			shipAttackSpot = GameObject.Find("FlyingLionAttackSpotLeft");

		if (shipAttackSpot == null)
			Debug.LogError( "ERROR: Attack spot for Flying Lion not found");
	}

	// Update is called once per frame
	void Update () 
	{
		if (mode == Mode.Approach)
			Approach();
		else if (mode == Mode.Attack)
			Attack();
		else if (mode == Mode.Retreat)
			Retreat();
		else if (mode == Mode.Wait)
			Wait();
	}

	void Approach()
	{
		MoveStraightTowards(target, approachSpeed);
		if (transform.position == target)
		{
			mode = Mode.Wait;
			waitingUntil = Time.time + waitTime;
		}
	}

	void Retreat()
	{
		MoveStraightTowards(target, retreatSpeed);
		if (transform.position == target)
		{
			mode = Mode.Wait;
			waitingUntil = Time.time + waitTime;
		}
	}

	void Attack()
	{
		target = shipAttackSpot.transform.position;

		MoveStraightTowards(target, attackSpeed);
		
		if (transform.position == target)
		{
			mode = Mode.Retreat;
			target = RandomWaitPosition();
		}
	}

	void Wait()
	{
		if (Time.time >= waitingUntil)
		{
			mode = Mode.Attack;
		}
	}

	void MoveStraightTowards(Vector3 position, float speed)
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, position, step);
	}


	Vector3 RandomStartPosition()
	{
		float x = ship.transform.position.x;
		if (side == Side.Right)
			x += 15.0f;
		else
			x -= 15.0f;

		float y = Random.Range (5.0f, 8.0f);
		return new Vector3(x, y, 0.0f);
	}

	Vector3 RandomWaitPosition()
	{
		float x = ship.transform.position.x;
		if (side == Side.Right)
		     x += Random.Range (3.0f, 6.0f);
		else
			x  -= Random.Range (3.0f, 6.0f);

		float y = Random.Range (6.0f, 10.0f);
		return new Vector3(x, y, 0.0f);
	}




}
