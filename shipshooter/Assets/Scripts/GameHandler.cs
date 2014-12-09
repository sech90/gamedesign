using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		GameObject cameraPrefab = Resources.Load<GameObject>("Main Camera");

		GameObject shipPrefab = Resources.Load<GameObject>("Ship");
		GameObject sailorPrefab = Resources.Load<GameObject>("Sailor");
		GameObject flyingLionPrefab = Resources.Load<GameObject>("FlyingLion");

		GameObject ship = Instantiate (shipPrefab) as GameObject;

		GameObject sailor = Instantiate (sailorPrefab) as GameObject;
		GameObject camera = Instantiate (cameraPrefab) as GameObject;
		camera.GetComponent<CameraFollow2d>().targetGameObject = ship;

		GameObject flyingLion1 = Instantiate (flyingLionPrefab) as GameObject;
		GameObject flyingLion2 = Instantiate (flyingLionPrefab) as GameObject;
		GameObject flyingLion3 = Instantiate (flyingLionPrefab) as GameObject;


		ship.transform.position = new Vector3(7.0f, 0.0f, 0.0f);
		sailor.transform.parent = ship.transform;
		sailor.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);

		//flyingLion.transform.position = new Vector3(9.0f, 2.0f, 0.0f);

	
	}
	
}
