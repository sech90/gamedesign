using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Enemies"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"),LayerMask.NameToLayer("Items"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
