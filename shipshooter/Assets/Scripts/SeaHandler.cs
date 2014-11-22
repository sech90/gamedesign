using UnityEngine;
using System.Collections;

public class SeaHandler : MonoBehaviour {
	
	EditableMesh mesh;
	
	void Start () 
	{
		mesh = new EditableMesh();
		
		mesh.Create();
		mesh.SetColor(Color.blue);
	}
	
	void Update () 
	{
		
		int i = 0;
		
		float time = Time.time;
		
		while (i < 81) 
		{
			Vector3 position = mesh.GetVertex(i,1); 
			Vector3 newPosition = new Vector3( position.x, 
			                                  GetSurfaceY( position.x, time ), 
			                                  position.z );
			
			mesh.SetVertex(i,1, newPosition);
			i++;
		}
		
		mesh.UpdateVertices();
		
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
