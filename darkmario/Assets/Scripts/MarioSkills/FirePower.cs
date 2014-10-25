using UnityEngine;
using System.Collections;

public class FirePower : SuperPower {
		
	override protected void Activate(){
		Debug.Log("Mario is on fire");
	}
	
	override public void Remove(){
		Destroy(this);
		Debug.Log("Mario not on fire anymore");
	}

}
