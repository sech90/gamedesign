using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public float speed = 5.0f;
	public float wobbling = 1.0f;
	private Vector3 pos = new Vector3(); 

	// Use this for initialization
	void Start () {
		Debug.Log("Molecules are excited!");
	}
	
	// Update is called once per frame
	void Update () {

		//pos.x = Mathf.Sin(Time.time * speed);
		//pos.y = Mathf.Cos(Time.time * speed);
		//transform.position = pos;
		transform.Rotate(
			Mathf.Sin(Time.time * speed)* wobbling,
			Mathf.Cos(Time.time * speed)* wobbling,
			Mathf.Sin(Time.time * speed)* wobbling);
	}
}
