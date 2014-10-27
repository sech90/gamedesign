﻿using UnityEngine;
using System.Collections;

public class BigPower : SuperPower {



	override protected void Activate(){
//		Vector3 scale = transform.localScale;
//		scale.y *= 2;
		
	//	transform.localScale = scale;

	 
		GetComponent<Animator> ().SetTrigger ("TransformToBig");
		BoxCollider2D bc = GetComponent<BoxCollider2D> ();
		bc.center = new Vector2 (0.0f, 50.0f);
		bc.size = new Vector2 (65.0f, 155.0f);


	}

	override public void Remove(){
//		Vector3 scale = transform.localScale;
//		scale.y /= 2;
		GetComponent<Animator> ().SetTrigger ("TransformToSmall");
		BoxCollider2D bc = GetComponent<BoxCollider2D> ();
		bc.center = new Vector2 (0.0f, 2.0f);
		bc.size = new Vector2 (65.0f, 76.0f);
		
		//		transform.localScale = scale;
		Destroy(this);
	}
}
