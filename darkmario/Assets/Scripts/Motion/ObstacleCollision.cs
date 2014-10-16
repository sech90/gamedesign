using UnityEngine;
using System.Collections;

public class ObstacleCollision : MonoBehaviour 
{
	
	private enum COLLISIONPOINT { GROUND1, GROUND2, GROUNDED1, GROUNDED2, 
		RIGHT1, RIGHT2, LEFT1, LEFT2, HEAD1, HEAD2 };
	
	private Transform[] collisionPoints;
	private Collider2D[] hits;
	
	private SMBPhysicsBody body;
	
	public bool IsGrounded()
	{
		return (ObstacleCollisionAt (COLLISIONPOINT.GROUNDED1) || ObstacleCollisionAt (COLLISIONPOINT.GROUNDED2));
	}
	
	
	void Awake () 
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
		
		hits = new Collider2D[5];
		
		body = gameObject.GetComponent<SMBPhysicsBody>();
		
		if (body == null) 
		{
			Debug.LogError("Collisions need BasicMovement to work");
		}
		
	}
	
	void Update () 
	{
		
		// Check collision from different directions and move Mario accordingly
		while (ObstacleCollisionAt(COLLISIONPOINT.HEAD1) || ObstacleCollisionAt(COLLISIONPOINT.HEAD2) )
		{
			transform.position = new Vector2(transform.position.x, transform.position.y - 1.0f);
			body.velocity.y = 0.0f;
		}
		
		
		while (ObstacleCollisionAt(COLLISIONPOINT.RIGHT1) || ObstacleCollisionAt(COLLISIONPOINT.RIGHT2) )
		{
			transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y);
			body.velocity.x = 0.0f;
		}
		
		while (ObstacleCollisionAt(COLLISIONPOINT.LEFT1) || ObstacleCollisionAt(COLLISIONPOINT.LEFT2) )
		{
			transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y);
			body.velocity.x = 0.0f;
		}
		
		while ( ObstacleCollisionAt(COLLISIONPOINT.GROUND1) || ObstacleCollisionAt(COLLISIONPOINT.GROUND2) )
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + 1.0F);
			body.velocity.y = 0.0f;
		}   
	}
	
	
	// Check if given collision point is in collision with ANYTHING (to be changed)
	bool ObstacleCollisionAt(COLLISIONPOINT cp)
	{
		if ( Physics2D.OverlapPointNonAlloc (collisionPoints [(int)cp].position, hits) > 0)
			return true;
		
		return false;
	}
	
	
	
}