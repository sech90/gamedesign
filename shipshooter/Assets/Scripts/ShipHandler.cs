using UnityEngine;
using System.Collections;

public class ShipHandler : MonoBehaviour {

	public GameObject sea;

	public float hullWidth = 3.0f;
	public float rollFactor = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		SeaHandler sh = sea.GetComponent<SeaHandler>();


		// Height of the sea 
		float seaLeft   = sh.GetSurfaceY(7.5f - hullWidth/2.0f, Time.time);
		float seaMiddle = sh.GetSurfaceY(7.5f,                  Time.time);
		float seaRight  = sh.GetSurfaceY(7.5f + hullWidth/2.0f, Time.time);

		// Ship floats at the avg of sea's surface at the three points
		float shipHeight = ( seaLeft + seaMiddle + seaRight ) / 3.0f;



		transform.position = new Vector3(transform.position.x, 
		                                 shipHeight, 
		                                 transform.position.z );


		transform.rotation =  Quaternion.AngleAxis(Mathf.Sin(Time.time) * 5.0f, Vector3.forward);

	*/
		UpdateTransformation();
	}


	// Updates ships position and rotation so it floats on the waves
	void UpdateTransformation()
	{

		SeaHandler sh = sea.GetComponent<SeaHandler>();
			
			
		// Height of the sea at hull's middle and it's edges
		float seaLeft   = sh.GetSurfaceY(7.5f - hullWidth/2.0f, Time.time);
		float seaMiddle = sh.GetSurfaceY(7.5f,                  Time.time);
		float seaRight  = sh.GetSurfaceY(7.5f + hullWidth/2.0f, Time.time);
			
		// Ship floats at the avg of the three points
		float shipHeight = ( seaLeft + seaMiddle + seaRight ) / 3.0f;
			
			
			
		transform.position = new Vector3(transform.position.x, 
			                             shipHeight, 
			                             transform.position.z );

		// Calculate ships roll (rotation caused by waves)
		float roll = Mathf.Atan( (seaRight-seaLeft)/hullWidth ) * Mathf.Rad2Deg;
		roll = roll * rollFactor; // dampen the roll to make it more tolerable
			
		transform.rotation =  Quaternion.AngleAxis(roll, Vector3.forward);
	}

}
