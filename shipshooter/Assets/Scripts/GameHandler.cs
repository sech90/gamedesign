using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

		GameObject shipPrefab = Resources.Load<GameObject>("Ship");

		GameObject ship = Instantiate (shipPrefab) as GameObject;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
