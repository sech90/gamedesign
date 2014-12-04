using UnityEngine;
using System.Collections;

public class ShipHandler : MonoBehaviour {

	public GameObject sea;

	public float hullWidth = 3.0f;
	public float rollFactor = 0.5f;
	public float maxSpeed = 0.5f;
	public float wheelTurnSpeed = 0.25f;
	public float maxRotationFromTurning = 15.0f; 

	// How much the wheel has been turned
	private float wheel = 0.0f; // -1 = max to left, 1 = max to right




	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Input.GetKey (KeyCode.E) )
		{
			TurnWheel( wheelTurnSpeed * Time.deltaTime );
		}

		if ( Input.GetKey (KeyCode.Q) )
		{
			TurnWheel( -wheelTurnSpeed * Time.deltaTime );
		}

		TurnShip();
		UpdateTransformation();
	}

	void TurnWheel( float amount )
	{
		wheel += amount;

		if (wheel < -1.0f)
			wheel = -1.0f;

		if (wheel > 1.0f)
			wheel = 1.0f;
	}

	void TurnShip()
	{
		Vector3 change = new Vector3(maxSpeed * wheel * Time.deltaTime, 0.0f, 0.0f);
		transform.position = transform.position + change;
	}


	// Updates ships position and rotation so it floats on the waves
	void UpdateTransformation()
	{

		SeaHandler sh = sea.GetComponent<SeaHandler>();
			
			
		// Height of the sea at hull's middle and it's edges
		float seaLeft   = sh.GetSurfaceY(transform.position.x - hullWidth/2.0f, Time.time);
		float seaMiddle = sh.GetSurfaceY(transform.position.x,                  Time.time);
		float seaRight  = sh.GetSurfaceY(transform.position.x + hullWidth/2.0f, Time.time);
			
		// Ship floats at the avg of the three points
		float shipHeight = ( seaLeft + seaMiddle + seaRight ) / 3.0f;
			
			
			
		transform.position = new Vector3(transform.position.x, 
			                             shipHeight, 
			                             transform.position.z );

		// Calculate ships roll (rotation caused by waves)
		float roll = Mathf.Atan( (seaRight-seaLeft)/hullWidth ) * Mathf.Rad2Deg;
		roll = roll * rollFactor; // dampen the roll to make it more tolerable
			
		transform.rotation =  Quaternion.AngleAxis(roll - wheel* maxRotationFromTurning, Vector3.forward);
	}

}
