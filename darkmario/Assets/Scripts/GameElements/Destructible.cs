using UnityEngine;
using System.Collections;

public class Destructible : Brick {

	override public void SpecialEffect(GameObject hitter){
		Debug.Log("Destructible ");
		if(hitter.GetComponent<BigPower>() != null){
			Debug.Log("Destroyed brick");
			Destroy(GetComponent<SpriteRenderer>());
			Destroy(gameObject,0.2f);
		}
	}

}
