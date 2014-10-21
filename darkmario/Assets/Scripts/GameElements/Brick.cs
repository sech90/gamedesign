using UnityEngine;
using System.Collections;

public abstract class Brick : MonoBehaviour {
	
	protected abstract void OnHit();

	void OnCollisionEnter2D(Collision2D coll){

	}

}
