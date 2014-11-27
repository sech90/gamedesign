using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

		GameObject shipPrefab = Resources.Load<GameObject>("Ship");
		GameObject sailorPrefab = Resources.Load<GameObject>("Sailor");

		GameObject ship = Instantiate (shipPrefab) as GameObject;
		GameObject sailor = Instantiate (sailorPrefab) as GameObject;
		ship.transform.position = new Vector3(7.0f, 0.0f, 0.0f);
		sailor.transform.parent = ship.transform;
		sailor.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
