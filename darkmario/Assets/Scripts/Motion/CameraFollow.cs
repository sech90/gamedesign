using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{

	public Transform target;
	public float xDistanceTarget = -150.0f;

	void Start(){
		if(target == null)
			target = GameObject.FindObjectOfType<Mario>().transform;
	}
	void Update () 
	{
		float xDistance = target.position.x - transform.position.x;

		if (xDistance > xDistanceTarget) 
		{
			transform.position = new Vector3 (target.position.x - xDistanceTarget, 
			                                  transform.position.y, transform.position.z);
		}

	}
}
