using UnityEngine;
using System.Collections;

public class FloatingObject : MonoBehaviour {
	
	private bool _isFloating = true;
	protected bool IsFloating {get{return _isFloating;} set{_isFloating = value;}}
	
	private SeaHandler _sea;  
	
	void Start(){
		_sea = GameObject.FindObjectOfType<SeaHandler>();
	}
	void Update () {
		if(_isFloating){
			transform.position = new Vector3(transform.position.x, _sea.GetSurfaceY(transform.position.x), transform.position.z );
		}
	}
}
