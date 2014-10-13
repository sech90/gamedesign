using UnityEngine;
using System.Collections;

public class ProtagonistMovement : MonoBehaviour 
{
	// pixels / s
	float maxWalkSpeed = 468.75F;
	float walkAccelleration = 2000.0F;
	float gravity = 112.5F;


	float jumpSpeed = 800.0f;

	// if there is a collision higher than this coordinate, it's a head collision
	private float headCollisionY = 35.0f;
	// if there is a collision lower than this coordinate, it's a ground collision
	private float feetCollisionY = -35.0f;


	bool isOnGround = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 velocity = rigidbody2D.velocity;


		if (UserInput.JumpDown() && isOnGround ) 
		{
			velocity.y = jumpSpeed;
			Debug.Log("Jump");
		}
		

		if ( UserInput.Right() )
		{
			velocity.x += walkAccelleration * Time.deltaTime;

			if (velocity.x > maxWalkSpeed)
				velocity.x = maxWalkSpeed;

		}

		if ( UserInput.Left() )
		{
			velocity.x -= walkAccelleration * Time.deltaTime;
			
			if (velocity.x < -maxWalkSpeed)
				velocity.x = -maxWalkSpeed;
			
		}

		//if (!isOnGround)
		//		velocity.y -= gravity * Time.deltaTime;

		rigidbody2D.velocity = velocity;
	}

/*
	void OnTriggerEnter2D(Collider2D collider)
	{
		BoxCollider2D box = (BoxCollider2D)collider;
		Debug.Log("Pum!");

	}
*/
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground") 
		{
			Debug.Log("Pam!");
			if ( IsFeetCollision(collision) )
			{
				isOnGround = true;
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0.0f);
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground") 
		{
			isOnGround = false;
		}
	}

	// Check if collision is to the lower part of character
	bool IsFeetCollision(Collision2D collision)
	{
		for (int i=0; i<collision.contacts.Length; i++) 
		{
			Vector2 localCollPoint = WorldToLocal2D( collision.contacts[i].point );
 			
			if (localCollPoint.y <= feetCollisionY )
				return true;
		}
	
		return false;
	}

	// Check if collision is to the upper part of character
	bool IsHeadCollision(Collision2D collision)
	{
		for (int i=0; i<collision.contacts.Length; i++) 
		{
			Vector2 localCollPoint = WorldToLocal2D( collision.contacts[i].point );

			if (localCollPoint.y >= headCollisionY )
				return true;
		}
		
		return false;
	}


	// Transform world 2d coordinates to local coordinates
	Vector2 WorldToLocal2D( Vector2 worldCoords )
	{
		Vector2 localCoords = transform.InverseTransformPoint(worldCoords.x, worldCoords.y, 0);
		return localCoords;
	}


}
