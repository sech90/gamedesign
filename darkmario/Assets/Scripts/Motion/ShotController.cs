using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	Rigidbody2D rb = null;

	// Use this for initialization
	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2( 800.0f, 0.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{

		// If shot hits an enemy, it dies
		if (coll.gameObject.GetComponent<Enemy> () != null)
						coll.gameObject.GetComponent<Enemy> ().Kill ();

		// Shot is destroyed on all collisions
		Destroy (gameObject);
	}
}
