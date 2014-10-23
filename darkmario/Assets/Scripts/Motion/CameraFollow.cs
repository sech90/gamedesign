using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{

	public Transform target;

	void Start(){
		if(target == null)
			target = GameObject.FindObjectOfType<Mario>().transform;
	}
	void Update () 
	{
		transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
	}
}
