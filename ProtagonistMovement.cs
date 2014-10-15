using UnityEngine;
using System.Collections;

public class ProtagonistMovement : MonoBehaviour 
{
	// pixels / s
	float maxWalkSpeed = 468.75F;
	float walkAccelleration = 2000.0F;
	float gravity = 300.5F;


	float jumpSpeed = 250.0f;

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

		if (!isOnGround)
				velocity.y -= gravity * Time.deltaTime;

		rigidbody2D.velocity = velocity;
	}


	void OnTriggerStay2D(Collider2D collider)
	{
		BoxCollider2D mariosBox = gameObject.GetComponent<BoxCollider2D>();
		BoxCollider2D otherBox = (BoxCollider2D)collider; //Should check if successful


		while (IsPointIntBoxCollider (new Vector2 (0.0f, -40.0f), otherBox)) 
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + 1.0f);
		}

		if (IsPointIntBoxCollider (new Vector2 (0.0f, -42.0f), otherBox)) 
		{
			isOnGround = true;
		} 
		else 
		{
			isOnGround = false;
		}


		
		//	Vector2 intersection = GetBoxCollIntersectionDepth (mariosBox, otherBox);

	}

	bool IsPointIntBoxCollider( Vector2 pointIn, BoxCollider2D box )
	{
		// Transform to world coordinates
		Vector2 boxCenter = box.transform.TransformPoint (box.center);
		Vector2 boxSize = box.size; //box.transform.TransformPoint (box.size);
		Vector2 point = transform.TransformPoint (pointIn);

		Vector2 min = new Vector2(boxCenter.x - boxSize.x/2.0f, boxCenter.y - boxSize.y/2.0f);
		Vector2 max = new Vector2(boxCenter.x + boxSize.x/2.0f, boxCenter.y + boxSize.y/2.0f);

		Debug.Log (boxSize);
		//Debug.Log ("JEE");
		//Debug.Log (min.y);
		//Debug.Log (point.y);
		//Debug.Log (max.y);

//		Debug.Log ( point.x >= min.x && point.x <= max.x );
//		Debug.Log (point.y >= min.y && point.y <= max.y);

		if (point.x >= min.x && point.x <= max.x && 
		    point.y >= min.y && point.y <= max.y)
				return true;

		return false;
	}


	Vector2 GetBoxCollIntersectionDepth( BoxCollider2D a, BoxCollider2D b )
	{
		// Transform to world coordinates
		Vector2 aCenter = a.transform.TransformPoint (a.center);
		Vector2 aSize = a.transform.TransformPoint (a.size);
		Vector2 bCenter = b.transform.TransformPoint (b.center);
		Vector2 bSize = b.transform.TransformPoint (b.size);

		Debug.Log (aSize );
		Debug.Log (bSize );

		
		return aSize;

	}

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
