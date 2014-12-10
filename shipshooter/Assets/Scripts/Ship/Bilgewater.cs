using UnityEngine;
using System;
using System.Collections.Generic;

public class Bilgewater : MonoBehaviour {


	public GameObject ship;


	EditableMesh mesh;
	


	Vector2 hullBottom;
	Vector2 hullLeftMiddle;
	Vector2 hullLeftTop;
	Vector2 hullRightMiddle;
	Vector2 hullRightTop;

	//should be from 0 (no water) to 1 (completely full)
	public void SetWaterLevel(float level){}

	//should return values from 0..1
	public float GetWaterHeight()
	{
		return -0.7f;
	}

	void Start () 
	{
		/** /
		PolygonCollider2D coll = ship.collider2D as PolygonCollider2D;
		Vector2[] sortedX = coll.points;
		Vector2[] sortedY = coll.points;

		Array.Sort(sortedX,delegate(Vector2 a, Vector2 b) {
			return a.x.CompareTo(b.x);
		});

		Array.Sort(sortedY,delegate(Vector2 a, Vector2 b) {
			return a.y.CompareTo(b.y);
		});

		for(int i=0;i<coll.GetTotalPointCount(); i++){
			Debug.Log("X "+i+": "+sortedX[i]);
		}

		for(int i=0;i<coll.GetTotalPointCount(); i++){
			Debug.Log("Y "+i+": "+sortedY[i]);
		}

		hullBottom = 		sortedY[0];
		hullLeftTop =  		sortedX[0];
		hullRightTop = 		sortedX[6];
		hullLeftMiddle = 	sortedX[1].x < sortedX[2].x ? sortedX[1] : sortedX[2];
		hullRightMiddle = 	sortedX[4].x < sortedX[5].x ? sortedX[4] : sortedX[5];

		Debug.Log(hullBottom);
		Debug.Log(hullLeftTop);
		Debug.Log(hullRightTop);
		Debug.Log(hullLeftMiddle);
		Debug.Log(hullRightMiddle);
		/**/

		hullBottom = 		new Vector2( 0.0f,  -1.25f);
		hullLeftTop =  		new Vector2(-1.45f, 0.18f);
		hullRightTop = 		new Vector2( 1.45f, 0.18f);
		hullLeftMiddle = 	new Vector2(-1.07f,  -0.85f);
		hullRightMiddle = 	new Vector2( 1.07f,  -0.85f);
		/**/
		mesh = new EditableMesh();
		mesh.Create(10.0f, 10.0f, 1, 2);

		mesh.SetParent(this.gameObject);
		mesh.SetLocalPosition( new Vector3(0.0f, 0.0f, 0.0f) );

		mesh.SetSortingOrder(30);

		Color waterColor = Util.CreateColor(182, 172, 147, 120);
		mesh.SetColor(waterColor);

		mesh.SetVertex(0,0, hullBottom);
		mesh.SetVertex(1,0, hullBottom);
		mesh.SetVertex(0,1, hullLeftMiddle);
		mesh.SetVertex(1,1, hullRightMiddle);
		mesh.SetVertex(0,2, hullLeftTop);
		mesh.SetVertex(1,2, hullRightTop);

		
		mesh.UpdateMesh();
	}
	
	void Update () 
	{

		LineSegment2d upperLeftSide  = new LineSegment2d( hullLeftTop,  hullLeftMiddle );
		LineSegment2d upperRightSide = new LineSegment2d( hullRightTop, hullRightMiddle );
		LineSegment2d lowerLeftSide  = new LineSegment2d( hullBottom,   hullLeftMiddle );
		LineSegment2d lowerRightSide = new LineSegment2d( hullBottom,   hullRightMiddle );


		Vector2 isect = new Vector2(0.0f,0.0f);
	 	LineSegment2d surface = GetWaterSurfaceLine();

		if( surface.Intersection(lowerLeftSide, ref isect))
		{	
			mesh.SetVertex(0, 1, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(0, 2, new Vector3(isect.x, isect.y, 0.0f) );
		}
		else if( surface.Intersection(upperLeftSide, ref isect))
		{	
			mesh.SetVertex(0, 2, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(0,1, hullLeftMiddle);
		}
		else
		{
			mesh.SetVertex(0,1, hullBottom);
			mesh.SetVertex(0,2, hullBottom);
		}

		if( surface.Intersection(lowerRightSide, ref isect))
		{	
			mesh.SetVertex(1, 1, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(1, 2, new Vector3(isect.x, isect.y, 0.0f) );
		}
		else if( surface.Intersection(upperRightSide, ref isect))
		{	
			mesh.SetVertex(1, 2, new Vector3(isect.x, isect.y, 0.0f) );
			mesh.SetVertex(1,1, hullRightMiddle);
		}
		else
		{
			mesh.SetVertex(1,1, hullBottom);
			mesh.SetVertex(1,2, hullBottom);
		}


		mesh.UpdateMesh();

		//int i = 0;
		
/*
		while (i < numberOfSegments+1) 
		{
			Vector3 position = mesh.GetVertex(i,1); 
			float surfaceY = GetSurfaceY( position.x, time );
			
			Vector3 newPosition = new Vector3( position.x, surfaceY, position.z );
			
			mesh.SetVertex(i,1, newPosition);
			
			Vector2 uvSurface = new Vector2( i*0.1f, 1.0f ); 
			Vector2 uvBottom = new Vector2( i*0.1f, 1.0f-surfaceY*0.5f ); 
			mesh.SetUv(i, 1, uvSurface);
			mesh.SetUv(i, 0, uvBottom);
			
			i++;
		}
		
		mesh.UpdateMesh();
		*/
		
	}

	LineSegment2d GetWaterSurfaceLine()
	{
		float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		float lineLenght = 100.0f; // arbitrary

		float x = Mathf.Cos(-angle) * lineLenght;
		float y = Mathf.Sin(-angle) * lineLenght;

		//Vector2 a =  new Vector2(10.0f, GetWaterHeight() );
		//Vector2 eb =     new Vector2( 4.0f, GetWaterHeight() );
		Vector2 a =  new Vector2(x, y + GetWaterHeight() );
		Vector2 b =  new Vector2( -x, -y + GetWaterHeight() );

	

		LineSegment2d surface = new LineSegment2d( a, b );
		return surface;
	}

}
