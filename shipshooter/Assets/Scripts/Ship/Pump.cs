using UnityEngine;
using System.Collections;

public delegate void PumpEvent(Pump p);

public class Pump : InteractiveObject {
	
	public float PumpPower = 1.0f;
	public float LevelOfActivation = 0.0f; //this should stay from 0 to 1
	public AudioClip PullSound;
	public AudioClip PushSound;

	public PumpEvent OnPump;
	private bool _pumpUp = false;

	
	override protected void OnButtonPressed(ButtonDir key, Animator _anim){
		switch(key){
		case ButtonDir.UP:
			if(!_pumpUp){
				_pumpUp = true;
				AudioSource.PlayClipAtPoint(PullSound,transform.position);
			}
			break;
		case ButtonDir.DOWN:
			if(_pumpUp){
				AudioSource.PlayClipAtPoint(PushSound,transform.position);
				OnPump(this);
				_pumpUp = false;
			}
			break;
		default:
			break;
		}
	}
	
	//empty bodies
	override protected void OnButtonHold(ButtonDir key, Animator _anim){}
	override protected void OnButtonRelease(ButtonDir key, Animator _anim){}
}
