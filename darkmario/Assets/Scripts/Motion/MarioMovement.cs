using UnityEngine;
using System.Collections;


public class MarioMovement : MonoBehaviour 
{
	// pixels / s
	float maxWalkSpeed 			= 468.75F;
	float jumpSpeed 			= 1200.0f;

	// pixels / s^2
	float walkAccelleration 	= 668.0F;
	float releaseDecelleration 	= 703.0F;
	float skiddingDecelleration = 1828.0F;


	private SMBPhysicsBody body;
	private ObstacleCollision obsColls;
	
	void Start () 
	{
		
	}
	
	void Awake()
	{
		body = gameObject.GetComponent<SMBPhysicsBody>();
		obsColls = gameObject.GetComponent<ObstacleCollision>();
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		// If Jump-button is just pressed down and Mario is standing on ground
		if (UserInput.JumpDown() && obsColls.IsGrounded() ) 
		{
			body.velocity.y = jumpSpeed;
		}


		// If jump-button is being held down, gravity is lower making jump higher
		if (UserInput.Jump() ) 
		{
			body.marioGravity = 2250.0f;
		}
		else
		{
			body.marioGravity = 7875.0f;
		}
		


		if ( UserInput.Right() && body.velocity.x >= 0.0f )
		{
			body.velocity.x += walkAccelleration * Time.deltaTime;
			
			if (body.velocity.x > maxWalkSpeed)
				body.velocity.x = maxWalkSpeed;
			
		}
		
		if ( UserInput.Left() && body.velocity.x <= 0.0f)
		{
			body.velocity.x -= walkAccelleration * Time.deltaTime;
			
			if (body.velocity.x < -maxWalkSpeed)
				body.velocity.x = -maxWalkSpeed;
		}


		// If player let's go of controller, Mario decellerates slowly
		if (!UserInput.Left () && !UserInput.Right ()) 
		{
			Decellerate( releaseDecelleration * Time.deltaTime );
		}


		// If Mario is moving other way and plahyer steeres other way, Mario skids and decellerates faster
		if ( UserInput.Right() && body.velocity.x < 0.0f )
		{
			body.velocity.x += skiddingDecelleration * Time.deltaTime;
		}
		
		if ( UserInput.Left() && body.velocity.x > 0.0f)
		{
			body.velocity.x -= skiddingDecelleration * Time.deltaTime;
		}


	

	}





	void Decellerate( float deltaV )
	{
		if ( Mathf.Abs(body.velocity.x) < deltaV )
		{
			body.velocity.x = 0;
		}
		else
		{
			if (body.velocity.x < 0.0f)
			{
				body.velocity.x += deltaV;
			}
			else
			{
				body.velocity.x -= deltaV;
			}	
		}
	}
	
	
	
	
}