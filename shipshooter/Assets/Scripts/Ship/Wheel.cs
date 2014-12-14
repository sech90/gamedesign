using UnityEngine;
using System.Collections;

public class Wheel : InteractiveObject {

	public float turnSpeed = 0.25f;
	public float counterSteering = 0.1f;

	private float _steeringAmount = 0;
	public float SteeringAmount{ get{return _steeringAmount;} }


	Wheel(){
		_keyList = new KeyCode[]{ KeyCode.A, KeyCode.D };
	}

	void FixedUpdate(){
		if(_steeringAmount != 0)
			_steeringAmount = Mathf.Lerp(_steeringAmount,0,counterSteering*Time.deltaTime);
	}


	override protected void OnButtonHold(KeyCode key){
		switch(key){
			case KeyCode.A:
				_steeringAmount -= turnSpeed*Time.deltaTime;
				if(_steeringAmount < -1.0f)
					_steeringAmount = -1.0f;
				break;
			case KeyCode.D:
				_steeringAmount += turnSpeed*Time.deltaTime;
				if(_steeringAmount > 1.0f)
					_steeringAmount = 1.0f;
				break;
			default:
				break;
		}
	}

	//empty bodies
	override protected void OnButtonPressed(KeyCode key){}
	override protected void OnButtonRelease(KeyCode key){}
}
