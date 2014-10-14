using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour 
{


	static public bool Left()
	{
		if (Input.GetAxis("Horizontal") < 0 )
			return true;

		return false;
	}

	static public bool Right()
	{
		if (Input.GetAxis("Horizontal") > 0)
			return true;

		return false;
	}

	static public bool Jump()
	{
		if (Input.GetAxis("Vertical") > 0)
			return true;

		return false;
	}


}
