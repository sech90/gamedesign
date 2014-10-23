using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int lives = 1;
	public int pointsWhenKilled = 100;

	// Use this for initialization
	void Awake () {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Enemies"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Items"));
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "DeathPit")
			Kill();
	}

	public int Hit(){
		lives--;
		if(lives == 0){
			Kill();
			return pointsWhenKilled;
		}
		return 0;
	}

	public void Kill(){
		Destroy(gameObject);
	}
}
