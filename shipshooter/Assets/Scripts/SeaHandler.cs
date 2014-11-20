using UnityEngine;
using System.Collections;

public class SeaHandler : EditableMesh {



	void Start () 
	{
		Create();
	}
	
	void Update () 
	{
		
		int i = 0;
		
		float time = Time.time;
		
		while (i < 81) 
		{
			Vector3 position = GetVertex(i,1); 
			Vector3 newPosition = new Vector3( position.x, 
			                                  GetSurfaceY( position.x, time ), 
			                                  position.z );
			
			SetVertex(i,1, newPosition);
			i++;
		}

		UpdateVertices();
		
	}


	public float GetSurfaceY( float x, float time)
	{
//		return 4.0f + 0.8f * Mathf.Sin(time*0.4f + x*0.7f) 
//			- 0.1f * Mathf.Cos(time*2 + x*2.0f);
		return 4.0f + 0.3f * Mathf.Sin(time*0.4f + x*0.7f) 
			- 0.1f * Mathf.Cos(time*2 + x*2.0f);
		/*return 4.0f + 0.3f * Mathf.Sin(time*0.4f + x*0.7f) 
			- 0.1f * Mathf.Cos(time*0.13f + x*2.0f); */
	}

}
