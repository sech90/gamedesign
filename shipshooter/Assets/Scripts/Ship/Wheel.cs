using UnityEngine;
using System.Collections;

public class Wheel : InteractiveObject {

	public float turnSpeed = 0.25f;
	public float counterSteering = 0.1f;
	public AudioClip steerRightSound = null;
	public AudioClip steerLeftSound = null;


	private float _steeringAmount = 0;
	public float SteeringAmount{ get{return _steeringAmount;} }


	void FixedUpdate(){
		if(_steeringAmount != 0)
			_steeringAmount = Mathf.Lerp(_steeringAmount,0,counterSteering*Time.deltaTime);
	}


	override protected void OnButtonHold(ButtonDir key, Animator _anim){
		switch(key){
		case ButtonDir.LEFT:
				_steeringAmount -= turnSpeed*Time.deltaTime;
				if(_steeringAmount < -1.0f)
					_steeringAmount = -1.0f;
				break;
		case ButtonDir.RIGHT:
				_steeringAmount += turnSpeed*Time.deltaTime;
				if(_steeringAmount > 1.0f)
					_steeringAmount = 1.0f;
				break;
			default:
				break;
		}
	}

	override protected void OnButtonPressed(ButtonDir key, Animator _anim){
		switch(key){
		case ButtonDir.LEFT:
			AudioSource.PlayClipAtPoint(steerLeftSound, transform.position);
			break;
		case ButtonDir.RIGHT:
			AudioSource.PlayClipAtPoint(steerRightSound, transform.position);
			break;
		default:
			break;
		}
	}

	
	//empty bodies
	override protected void OnButtonRelease(ButtonDir key, Animator _anim){}
}
