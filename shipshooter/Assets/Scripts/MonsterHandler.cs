using UnityEngine;
using System.Collections;

public class MonsterHandler : MonoBehaviour {

	public GameObject sea;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateTransformation();
	
	}

	void UpdateTransformation()
	{
		
		SeaHandler sh = sea.GetComponent<SeaHandler>();
		

		float time = Time.time;
		
		transform.position = new Vector3(transform.position.x, 
		                                 sh.GetSurfaceY(transform.position.x, time) + 0.2f, 
		                                 transform.position.z );
		
	}
}
