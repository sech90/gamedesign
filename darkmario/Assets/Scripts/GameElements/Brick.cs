using UnityEngine;
using System.Collections;

public abstract class Brick : MonoBehaviour {
	
	public abstract void OnHit(GameObject hitter);

	/*void OnCollisionEnter2D(Collision2D coll){
		Debug.Log("collision enter "+coll.gameObject.name);
		if(coll != null && coll.gameObject.tag == "MarioHead")
			OnHit();
	}*/

}
