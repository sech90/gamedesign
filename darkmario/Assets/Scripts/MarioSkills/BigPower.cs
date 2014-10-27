using UnityEngine;
using System.Collections;

public class BigPower : SuperPower {



	override protected void Activate(){
		Vector3 scale = transform.localScale;
		scale.y *= 2;
		
		transform.localScale = scale;
	}

	override public void Remove(){
		Vector3 scale = transform.localScale;
		scale.y /= 2;
		
		transform.localScale = scale;
		Destroy(this);
	}
}
