using UnityEngine;
using System.Collections;

public class ProtagonistMovement : MonoBehaviour 
{
	// pixels / s
	float maxWalkSpeed = 468.75F;
	float walkAccelleration = 2000.0F;
	float gravity = 500.5F;


	private Transform groundCheck;
	private Collider2D[] hits;

	float jumpSpeed = 450.0f;


	private Vector3 velocity;

	private enum COLLISIONPOINT {GROUND1, GROUND2,  GROUNDED1, GROUNDED2, RIGHT1, RIGHT2, LEFT1, LEFT2, HEAD1, HEAD2};
	private Transform[] collisionPoints;

	bool isOnGround = false;

	void Start () 
	{
	
	}

	void Awake()
	{
		// Initialize array of collision points
		collisionPoints = new Transform[10];
		collisionPoints[(int)COLLISIONPOINT.GROUND1]   	 = transform.Find ("groundCheck1").transform;
		collisionPoints[(int)COLLISIONPOINT.GROUND2]   	 = transform.Find ("groundCheck2").transform;
		collisionPoints[(int)COLLISIONPOINT.GROUNDED1]   = transform.Find ("groundedCheck1").transform;
		collisionPoints[(int)COLLISIONPOINT.GROUNDED2]   = transform.Find ("groundedCheck2").transform;
		collisionPoints[(int)COLLISIONPOINT.LEFT1]   	 = transform.Find ("leftCheck1").transform;
		collisionPoints[(int)COLLISIONPOINT.LEFT2]   	 = transform.Find ("leftCheck2").transform;
		collisionPoints[(int)COLLISIONPOINT.RIGHT1]   	 = transform.Find ("rightCheck1").transform;
		collisionPoints[(int)COLLISIONPOINT.RIGHT2]   	 = transform.Find ("rightCheck2").transform;
		collisionPoints[(int)COLLISIONPOINT.HEAD1]    	 = transform.Find ("headCheck1").transform;
		collisionPoints[(int)COLLISIONPOINT.HEAD2]  	 = transform.Find ("headCheck2").transform;


		velocity = new Vector3 (0.0f, 0.0f, 0.0f);

		hits = new Collider2D[5];

	}



	// Update is called once per frame
	void Update () 
	{
		// Update Mario's position
		transform.position = new Vector2(transform.position.x + velocity.x * Time.deltaTime, transform.position.y + velocity.y * Time.deltaTime);



		if (UserInput.JumpDown() && IsGrounded() ) 
		{
			velocity.y = jumpSpeed;
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



		// Gravity pulls Mario down
		velocity.y -= gravity * Time.deltaTime;



		// Check collision from different direction and move Mario accordingly
		while (ObstacleCollision(COLLISIONPOINT.HEAD1) || ObstacleCollision(COLLISIONPOINT.HEAD2) )
		{
			transform.position = new Vector2(transform.position.x, transform.position.y - 1.0f);
			velocity.y = 0.0f;
		}

		while (ObstacleCollision(COLLISIONPOINT.RIGHT1) || ObstacleCollision(COLLISIONPOINT.RIGHT2) )
		{
			transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y);
			velocity.x = 0.0f;
		}

		while (ObstacleCollision(COLLISIONPOINT.LEFT1) || ObstacleCollision(COLLISIONPOINT.LEFT2) )
		{
			transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y);
			velocity.x = 0.0f;
		}

		while ( ObstacleCollision(COLLISIONPOINT.GROUND1) || ObstacleCollision(COLLISIONPOINT.GROUND2) )
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + 1.0F);
			velocity.y = 0.0f;
		}
	}


	// Check if given collision point is in collision with ANYTHING (to be changed)
	bool ObstacleCollision(COLLISIONPOINT cp)
	{
		if ( Physics2D.OverlapPointNonAlloc (collisionPoints [(int)cp].position, hits) > 0)
			return true;

		return false;
	}


	bool IsGrounded()
	{
		return (ObstacleCollision (COLLISIONPOINT.GROUNDED1) || ObstacleCollision (COLLISIONPOINT.GROUNDED2));
	}


	// Transform world 2d coordinates to local coordinates
	Vector2 WorldToLocal2D( Vector2 worldCoords )
	{
		Vector2 localCoords = transform.InverseTransformPoint(worldCoords.x, worldCoords.y, 0);
		return localCoords;
	}


}
