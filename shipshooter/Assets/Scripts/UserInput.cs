using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour 
{


	static public bool Player1Left()
	{
		if (Input.GetKey (KeyCode.A))
				return true;

		return false;
	}

	static public bool Player1Right()
	{
		if (Input.GetKey (KeyCode.D))
			return true;

		return false;
	}


	static public bool Player1Up()
	{
		if (Input.GetKey (KeyCode.W))
			return true;
		
		return false;
	}

	static public bool Player1Down()
	{
		if (Input.GetKey (KeyCode.S))
			return true;
		
		return false;
	}


	/*
	static public bool Player1Fire()
	{
		if (Input.GetKey(KeyCode.Space))
			return true;

		return false;
	}*/

	static public bool Exit()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			return true;

		return false;
	}


}
