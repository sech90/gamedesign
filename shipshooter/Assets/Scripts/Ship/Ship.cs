using UnityEngine;
using System.Collections.Generic;

public class Ship : FloatingObject {

	public int MaxHp = 100;
	public float SteerMaxSpeed = 10.0f;

	//components of the ship
//	private Sailorman 	_player;
	private Hole[]		_holes;
	private Pump[] 		_pumps;
	private Wheel 		_wheel;
	private Bilgewater 	_water;

	//data of the ship
	private float _currentHp;
	public  float CurrentHp {
		get{return _currentHp;} 
		set{_currentHp = Mathf.Clamp(value,0,MaxHp);}
	}

	void Start () {
		_currentHp = MaxHp; 
//		_player = transform.GetComponentInChildren<Sailorman>();
		_water  = transform.GetComponentInChildren<Bilgewater>();
		_pumps  = transform.FindChild("Interactive").GetComponentsInChildren<Pump>();
		_wheel  = transform.FindChild("Interactive").GetComponentInChildren<Wheel>();
		_holes  = new Hole[4];//transform.FindChild("Interactive/Holes").GetComponentsInChildren<Hole>();

		_holes[0] = transform.FindChild("Interactive/Holes/MidLeft").GetComponent<Hole>();
		_holes[1] = transform.FindChild("Interactive/Holes/MidRight").GetComponent<Hole>();
		_holes[2] = transform.FindChild("Interactive/Holes/BotLeft").GetComponent<Hole>();
		_holes[3] = transform.FindChild("Interactive/Holes/BotRight").GetComponent<Hole>();

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

	//create holes depending by the strength of the monster
	void OnTriggerEnter2D(Collider2D coll){
		Monster monster = coll.GetComponent<Monster>();
		if(monster != null && monster.Mode == MonsterMode.Attack){

			if(monster.Direction == MonsterFacing.Left)
				_holes[0].TakeDamage(monster.AttackPower);
			else
				_holes[1].TakeDamage(monster.AttackPower);
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
		if(pump.LevelOfActivation < _water.GetWaterLevel())
			_currentHp = _currentHp + pump.PumpPower;
	}
}
















