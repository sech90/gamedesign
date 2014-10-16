using UnityEngine;
using System.Collections;


public class MarioMovement : MonoBehaviour 
{
	// pixels / s
	float maxWalkSpeed = 468.75F;
	float walkAccelleration = 2000.0F;
	float jumpSpeed = 450.0f;
	
	private SMBPhysicsBody body;
	//private ObstacleCollision collisionHandler;
	
	void Start () 
	{
		
	}
	
	void Awake()
	{
		body = gameObject.GetComponent<SMBPhysicsBody>();
		//collisionHandler = gameObject.GetComponent<CollisionHandler>();
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		
		if (UserInput.JumpDown() )//&& collisionHandler.IsGrounded() ) 
		{
			body.velocity.y = jumpSpeed;
		}
		
		
		if ( UserInput.Right() )
		{
			body.velocity.x += walkAccelleration * Time.deltaTime;
			
			if (body.velocity.x > maxWalkSpeed)
				body.velocity.x = maxWalkSpeed;
			
		}
		
		if ( UserInput.Left() )
		{
			body.velocity.x -= walkAccelleration * Time.deltaTime;
			
			if (body.velocity.x < -maxWalkSpeed)
				body.velocity.x = -maxWalkSpeed;
			
		}
		
		
	}
	
	
	
	
}