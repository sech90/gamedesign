using UnityEngine;
using System.Collections;

public class ProtagonistMovement : MonoBehaviour 
{
	public float maxSpeed = 400.0F;
	public float accelleration = 20.0F;
	public float jumpSpeed = 800.0f;


	bool isOnGround = true;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 velocity = rigidbody2D.velocity;


		if (UserInput.Jump() && isOnGround ) 
		{
			velocity.y = jumpSpeed;
		}
		

	
		if ( UserInput.Right() )
		{
			velocity.x += accelleration;

			if (velocity.x > maxSpeed)
				velocity.x = maxSpeed;

		}

		if ( UserInput.Left() )
		{
			velocity.x -= accelleration;
			
			if (velocity.x < -maxSpeed)
				velocity.x = -maxSpeed;
			
		}

		rigidbody2D.velocity = velocity;
	}




	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground") 
		{
			isOnGround = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground") 
		{
			isOnGround = false;
		}
	}


}
