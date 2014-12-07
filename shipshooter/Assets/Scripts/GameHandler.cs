using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GameObject cameraPrefab = Resources.Load<GameObject>("Main Camera");

		GameObject shipPrefab = Resources.Load<GameObject>("Ship");
		GameObject sailorPrefab = Resources.Load<GameObject>("Sailor");

		GameObject ship = Instantiate (shipPrefab) as GameObject;
		GameObject sailor = Instantiate (sailorPrefab) as GameObject;
		GameObject camera = Instantiate (cameraPrefab) as GameObject;
		camera.GetComponent<CameraFollow2d>().targetGameObject = ship;

		ship.transform.position = new Vector3(7.0f, 0.0f, 0.0f);
		sailor.transform.parent = ship.transform;
		sailor.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);

	
	}
	
}
