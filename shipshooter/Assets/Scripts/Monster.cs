using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public GameObject sea;

	protected static GameObject ship = null;
	
	// Sets the ship as target for all monsters
	public static void SetTargetShip( GameObject shipIn )
	{
		ship = shipIn;
	}
	/*
	// Update is called once per frame
	void Update () 
	{
		UpdateTransformation();
	
	}

	void UpdateTransformation()
	{
		
		SeaHandler sh = sea.GetComponent<SeaHandler>();


		float time = Time.time;
		
		transform.position = new Vector3(transform.position.x, 
		                                 sh.GetSurfaceY(transform.position.x, time) + 0.3f, 
		                                 transform.position.z );
		
	}*/
}
