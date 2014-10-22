using UnityEngine;
using System.Collections;

public class Destructible : Brick {

	override public void OnHit(GameObject hitter){
		if(hitter.GetComponent<BigPower>() != null){
			Debug.Log("Destroyed brick");
			Destroy(gameObject);
		}
	}

}
