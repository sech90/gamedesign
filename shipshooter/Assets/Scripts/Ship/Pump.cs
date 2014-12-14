using UnityEngine;
using System.Collections;

public delegate void PumpEvent(Pump p);

public class Pump : InteractiveObject {
	
	public float PumpPower = 1.0f;
	public float LevelOfActivation = 0.0f; //this should stay from 0 to 1

	public PumpEvent OnPump;
	private bool _pumpUp = false;
	
	
	Pump(){
		_keyList = new KeyCode[]{ KeyCode.W, KeyCode.S };
	}
	
	override protected void OnButtonPressed(KeyCode key){
		switch(key){
		case KeyCode.W:
			_pumpUp = true;
			break;
		case KeyCode.S:
			if(_pumpUp){
				OnPump(this);
				_pumpUp = false;
			}
			break;
		default:
			break;
		}
	}
	
	//empty bodies
	override protected void OnButtonHold(KeyCode key){}
	override protected void OnButtonRelease(KeyCode key){}
}
