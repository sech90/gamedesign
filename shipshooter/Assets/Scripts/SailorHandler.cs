using UnityEngine;
using System.Collections;

public class SailorHandler : MonoBehaviour {

	public float sidewaysSpeed = 0.2f;
	public float climbSpeed = 0.1f;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{

		if ( UserInput.Player1Left() )
		{
			TryToMove( new Vector3(-sidewaysSpeed * Time.deltaTime, 0.0f) );
//			transform.localPosition +=  new Vector3(-sidewaysSpeed * Time.deltaTime, 0.0f);
		}

		if ( UserInput.Player1Right() )
		{
			TryToMove( new Vector3(sidewaysSpeed * Time.deltaTime, 0.0f) );
//			transform.localPosition += new Vector3(-sidewaysSpeed * Time.deltaTime, 0.0f);
		}

		if ( UserInput.Player1Up() )
		{
			TryToMove( new Vector3(0.0f, climbSpeed * Time.deltaTime ) );
//			transform.localPosition +=  new Vector3(0.0f, climbSpeed * Time.deltaTime );
		}

		if ( UserInput.Player1Down() )
		{
			TryToMove( new Vector3(0.0f, -climbSpeed * Time.deltaTime ) );
//			transform.localPosition += new Vector3(0.0f, -climbSpeed * Time.deltaTime );
		}
		/*
		if ( IsColliderAt(transform.localPosition) )
		{
			transform.localPosition += new Vector3( 0.0f, -2.0f );
		}*/


	}

	void TryToMove( Vector3 movement )
	{
		Vector3 newLocalPos = transform.localPosition + movement;
		if ( IsColliderAt(newLocalPos, "Floor"))
			transform.localPosition = newLocalPos;
	}



	// Is there a collider with given name at given local position
	bool IsColliderAt(Vector3 localPosition, string name)
	{
		Vector3 position = transform.TransformPoint(localPosition);

		Collider2D[] hits = Physics2D.OverlapPointAll (position);

		if (hits.Length > 0)
			if (hits[0].name == name )
				return true;
		
		return false;
	}

}
