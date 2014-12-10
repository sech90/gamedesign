using UnityEngine;
using System.Collections.Generic;

public class Ship : FloatingObject {

	public int MaxHp = 100;
	public float SteerMaxSpeed = 10.0f;

	//components of the ship
	private Sailorman 	_player;
	private Hole[]		_holes;
	private Pump[] 		_pumps;
	private Wheel 		_wheel;
	private Bilgewater 	_water;
	private int 		_attackBuffer = 0;

	//data of the ship
	private float _currentHp;
	public  float CurrentHp {
		get{return _currentHp;} 
		set{_currentHp = Mathf.Clamp(value,0,MaxHp);}
	}

	void Start () {
		_currentHp = MaxHp; 
		_player = transform.GetComponentInChildren<Sailorman>();
		_water  = transform.GetComponentInChildren<Bilgewater>();
		_pumps  = transform.FindChild("Interactive").GetComponentsInChildren<Pump>();
		_wheel  = transform.FindChild("Interactive").GetComponentInChildren<Wheel>();
		_holes  = transform.FindChild("Interactive/Holes").GetComponentsInChildren<Hole>();



		for(int i=0;i<_pumps.Length;i++)
			_pumps[i].OnPump = Pumped;

	}

	void Update () {
		doSteering();

		for(int i=0;i<_holes.Length; i++){
			CurrentHp = _currentHp - _holes[i].GetWaterPerSec() * Time.deltaTime;
		}

		_water.SetWaterLevel(1-_currentHp/MaxHp);
	}

	//create holes depending by the force
	void OnTriggerEnter2D(Collider2D coll){
		Monster monster = coll.GetComponent<Monster>();
		if(monster != null){
			_attackBuffer += monster.AttackPower;
		}
	}

	private void doSteering(){
		if(_wheel.SteeringAmount != 0){
			Vector3 pos = transform.position;
			pos.x += _wheel.SteeringAmount * SteerMaxSpeed * Time.deltaTime;
			transform.position = pos;
		}
	}

	private void Pumped(Pump pump){
		if(pump.LevelOfActivation < _water.GetWaterHeight())
			_currentHp = _currentHp + pump.PumpPower;
	}
}
















