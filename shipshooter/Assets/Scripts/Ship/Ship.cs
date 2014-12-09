using UnityEngine;
using System.Collections.Generic;

public class Ship : FloatingObject {

	public int MaxHp = 100;
	public float SteerMaxSpeed = 10.0f;

	//components of the ship
	private Sailorman 	_player;
	private List<Hole> 	_holes;
	private Pump[] 		_pumps;
	private Wheel 		_wheel;
	private Bilgewater 	_water;

	//data of the ship
	private float _currentHp;
	public  float CurrentHp {
		get{return _currentHp;} 
		set{
			if(value < 0) _currentHp = 0;
			else if (value > MaxHp) _currentHp = MaxHp;
			else _currentHp = value;
		}
	}

	void Start () {
		_currentHp = MaxHp;
		_player = transform.GetComponentInChildren<Sailorman>();
		_pumps  = transform.GetComponentsInChildren<Pump>();
		_wheel  = transform.GetComponentInChildren<Wheel>();
		_holes  = new List<Hole>();
		_water  = transform.GetComponentInChildren<Bilgewater>();
	}

	void Update () {
		doSteering();
		_water.SetWaterLevel(1-_currentHp/MaxHp);
	}

	//create holes depending by the force
	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log("Hit by enemy!");
	}

	private void doSteering(){
		if(_wheel.SteeringAmount != 0){
			Vector3 pos = transform.position;
			pos.x += _wheel.SteeringAmount * SteerMaxSpeed * Time.deltaTime;
			transform.position = pos;
		}
	}
}
