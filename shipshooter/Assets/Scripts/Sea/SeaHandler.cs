using UnityEngine;
using System.Collections;

public class SeaHandler : MonoBehaviour {
	
	EditableMesh mesh;

	GameObject[] wave;
	int numberOfWaves = 70;
	int numberOfSegments = 80;

	float width = 40.0f;
	float avgDepth = 4.0f;

	void Start () 
	{
		mesh = new EditableMesh();
		
		mesh.Create(width, avgDepth, numberOfSegments, 1);
		
	//	Texture2D seaTex = Resources.Load<Texture2D>("seaTest");
		//mesh.SetTexture(seaTex);

		Color seaColor = Util.CreateColor(182, 172,147);


		mesh.SetColor(seaColor);

		wave = new GameObject[numberOfWaves];

		GameObject wavePrefab = Resources.Load<GameObject>("Wave");

		for (int i=0; i<numberOfWaves; i++)
		{
		   wave[i] = Instantiate (wavePrefab) as GameObject;
			SpriteRenderer sprite = wave[i].GetComponent<SpriteRenderer>();
			if (Util.IsOdd(i))
				sprite.sortingOrder = 4;
			else
				sprite.sortingOrder = 8;
		}
	
	}
	
	void Update () 
	{
		
		int i = 0;
		
		while (i < numberOfSegments+1) 
		{
			Vector3 position = mesh.GetVertex(i,1); 
			float surfaceY = GetSurfaceY( position.x );

			Vector3 newPosition = new Vector3( position.x, surfaceY, position.z );
			
			mesh.SetVertex(i,1, newPosition);
			
			Vector2 uvSurface = new Vector2( i*0.1f, 1.0f ); 
			Vector2 uvBottom = new Vector2( i*0.1f, 1.0f-surfaceY*0.5f ); 
			mesh.SetUv(i, 1, uvSurface);
			mesh.SetUv(i, 0, uvBottom);
			
			i++;
		}
		
		mesh.UpdateMesh();


//		bool even=true;

		for (i=0; i<numberOfWaves; i++)
		{
			float x = i* width/numberOfWaves;
			float y = GetSurfaceY( x );
			if (Util.IsOdd(i))
				y=y+0.15f;
			float z = 0.0f;
 
		    wave[i].transform.position = new Vector3(x,y,z);

		}


	}
	
	
	public float GetSurfaceY( float x)
	{
		float time = Time.time;
		return avgDepth + 0.38f * Mathf.Sin(time*0.1f + x*0.7f) 
			- 0.45f * Mathf.Cos(time*0.8f + x*0.4f);

//		return avgDepth + 3.0f * Mathf.PerlinNoise(0.2f*x, 0.25f*time);

		//		return 4.0f + 0.8f * Mathf.Sin(time*0.4f + x*0.7f) 
		//			- 0.1f * Mathf.Cos(time*2 + x*2.0f);

		/*return 4.0f + 0.3f * Mathf.Sin(time*0.4f + x*0.7f) 
			- 0.1f * Mathf.Cos(time*0.13f + x*2.0f); */
	}

}
