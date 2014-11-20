using UnityEngine;
using System.Collections;

public class SeaBottom : MonoBehaviour {

	EditableMesh mesh1;
	EditableMesh mesh2;
	EditableMesh mesh3;

	void Start () 
	{
		mesh1 = new EditableMesh();
		mesh1.Create();
		mesh1.SetColor(new Color(0.9f, 0.5f, 0.0F, 1.0F) );

		mesh2 = new EditableMesh();
		mesh2.Create();
		mesh2.SetColor(new Color(0.9f, 0.5f, 0.0F, 0.3F) );

	}
	
	void Update () 
	{
		float time = Time.time;

		UpdateMesh( mesh1, time );		
		UpdateMesh( mesh2, time + 2.0f );		

	}

	void UpdateMesh( EditableMesh mesh, float time)
	{
		int i = 0;
		

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
		return 2.0f + 0.3f * Mathf.Sin(time*0.4f + x*0.7f) ;

		/*return 4.0f + 0.3f * Mathf.Sin(time*0.4f + x*0.7f) 
			- 0.1f * Mathf.Cos(time*0.13f + x*2.0f); */
	}

}
