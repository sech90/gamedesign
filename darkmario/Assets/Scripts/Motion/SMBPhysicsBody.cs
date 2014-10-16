﻿using UnityEngine;
using System.Collections;


// This script does basic Super Mario Bros physics:
// It stores objects position and velocity and does
// basic integration between them and applies gravity.


public class SMBPhysicsBody : MonoBehaviour 
{
	
	// Default gravity
	private float gravity = 500.5F; // pixels / sec ^ 2
	
	public Vector2 velocity;
	
	
	void Awake()
	{	
		velocity = new Vector2 (0.0f, 0.0f);
	}	
	
	
	void Update () 
	{
		// Integrate velocity into position
		transform.position = new Vector2( transform.position.x + velocity.x * Time.deltaTime, 
		                                 transform.position.y + velocity.y * Time.deltaTime );
		
		
		// Gravity pulls object down
		velocity.y -= gravity * Time.deltaTime;
		
		
	}
	
	
	
	
}