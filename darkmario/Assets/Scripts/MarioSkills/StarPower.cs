using UnityEngine;
using System.Collections;

public class StarPower : SuperPower {

	override protected void Activate(){
		Debug.Log("Mario is invincible");
	}
	
	override public void Remove(){
		Debug.Log("Mario not invincible anymore");
	}

	public void Reload(){
		Debug.Log("Invincible again");
	}
}
