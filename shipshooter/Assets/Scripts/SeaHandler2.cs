using UnityEngine;
using System.Collections;

public class SeaHandler2 : MonoBehaviour {
	
	EditableMesh mesh;

	GameObject[] wave;
	int numberOfWaves = 20;

	void Start () 
	{
		mesh = new EditableMesh();
		
		mesh.Create();
		
	//	Texture2D seaTex = Resources.Load<Texture2D>("seaTest");
		//mesh.SetTexture(seaTex);

		Color seaColor = CreateColor(182, 172,147);


		mesh.SetColor(seaColor);

		wave = new GameObject[numberOfWaves];

		GameObject wavePrefab = Resources.Load<GameObject>("Wave");

		for (int i=0; i<numberOfWaves; i++)
		   wave[i] = Instantiate (wavePrefab) as GameObject;
	
	}
	
	void Update () 
	{
		
		int i = 0;
		
		float time = Time.time;
		
		while (i < 81) 
		{
			Vector3 position = mesh.GetVertex(i,1); 
			float surfaceY = GetSurfaceY( position.x, time );
			float z = 0.5f;

			Vector3 newPosition = new Vector3( position.x, surfaceY, z );
			
			mesh.SetVertex(i,1, newPosition);
			
			Vector2 uvSurface = new Vector2( i*0.1f, 1.0f ); 
			Vector2 uvBottom = new Vector2( i*0.1f, 1.0f-surfaceY*0.5f ); 
			mesh.SetUv(i, 1, uvSurface);
			mesh.SetUv(i, 0, uvBottom);
			
			i++;
		}
		
		mesh.UpdateMesh();


		bool even=true;

		for (i=0; i<numberOfWaves; i++)
		{
			float x = i* 20.0f/numberOfWaves;
			float y = GetSurfaceY( x, time );
			float z = 0.1f;
			if (even)
				z= 0.2f;
			wave[i].transform.position = new Vector3(x,y,z);

			even = !even;
		}


	}
	
	
	public float GetSurfaceY( float x, float time)
	{
		//		return 4.0f + 0.8f * Mathf.Sin(time*0.4f + x*0.7f) 
		//			- 0.1f * Mathf.Cos(time*2 + x*2.0f);
		return 4.0f + 0.38f * Mathf.Sin(time*0.4f + x*0.7f) 
			- 0.1f * Mathf.Cos(time*2 + x*2.0f);
		/*return 4.0f + 0.3f * Mathf.Sin(time*0.4f + x*0.7f) 
			- 0.1f * Mathf.Cos(time*0.13f + x*2.0f); */
	}

	private Color CreateColor(int red, int green, int blue)
	{
		Color color = new Color (red/256.0f, green/256.0f, blue/256.0f);
		return color;
	}
	
}
