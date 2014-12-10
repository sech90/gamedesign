using UnityEngine;
using System.Collections;

public class Hole : InteractiveObject {

	public float MinWaterSec = 1.0f;
	public float MaxWaterSec = 10.0f;
	public int MaxHP = 20;
	public int DamageBuffer = 15;

	private int _currentHp;
	private int _buffer = 0;
	public int HP{get{return _currentHp;}}

	//settings for particle system
	private ParticleSystem _jet;
	private ParticleSystem _spray;
	private ParticleSystem _halo;
	//private int _minJet = 50;
	private int _maxJet = 400;
	//private int _minSpray = 100;
	private int _maxSpray = 400;


	void Start(){
		_currentHp = MaxHP;
		_keyList = new KeyCode[]{ KeyCode.A, KeyCode.D};
		_jet = transform.FindChild("WaterJet").GetComponent<ParticleSystem>();
		_halo = transform.FindChild("HoleHalo").GetComponent<ParticleSystem>();
		_spray = transform.FindChild("WaterSpry").GetComponent<ParticleSystem>();

		_jet.renderer.sortingLayerName = "Particles";
		_halo.renderer.sortingLayerName = "Particles";
		_spray.renderer.sortingLayerName = "Particles";
	}

	public float GetWaterPerSec(){
		if(_currentHp < MaxHP)
			return Mathf.Lerp(MaxWaterSec,MinWaterSec,_currentHp/MaxHP);
		return 0;
	}

	public void TakeDamage(int amount){

		if(_buffer < DamageBuffer){
			_buffer += amount;
			amount = _buffer > DamageBuffer ? DamageBuffer - _buffer : 0;
		}
		_currentHp = Mathf.Clamp(_currentHp-amount, 0, _currentHp);
		Debug.Log(name+" take damage "+amount+" buffer: "+_buffer);
		UpdateParticle();
	}

	override protected void OnButtonPressed(KeyCode key){
		switch(key){
		case KeyCode.A:
			if(transform.localRotation.eulerAngles.z <= 90 || transform.localRotation.eulerAngles.z >= 270){
				if(_currentHp < MaxHP){
					_currentHp++;
					if(_currentHp == MaxHP)
						_buffer = 0;
					UpdateParticle();
				}
			}
			break;
		case KeyCode.D:
			if(transform.localRotation.eulerAngles.z > 90 && transform.localRotation.eulerAngles.z < 270){
				if(_currentHp < MaxHP){
					_currentHp++;
					if(_currentHp == MaxHP)
						_buffer = 0;
					UpdateParticle();
				}
			}
			break;
		default:
			break;
		}


	}

	private void UpdateParticle(){
		if(_currentHp == MaxHP){
			_jet.Stop();
			_halo.Stop();
			_spray.Stop();
		}
		else{
			if(!_halo.isPlaying){
				_halo.Play();
				_jet.Play();
				_spray.Play();
			}
			_jet.emissionRate = Mathf.Lerp(_maxJet, 0, (float)_currentHp/MaxHP);
			_spray.emissionRate = Mathf.Lerp(_maxSpray, 0, (float)_currentHp/MaxHP);
		}
	}
	
	//empty bodies
	override protected void OnButtonHold(KeyCode key){}
	override protected void OnButtonRelease(KeyCode key){}


}
