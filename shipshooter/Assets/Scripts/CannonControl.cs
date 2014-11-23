using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {

	public float CannonPower = 10.0f;
	public float RotateSpeed = 5.0f;
	public int ShootAngle = 90;

	private Transform 	_hinge;
	private Transform 	_spawner;
	private GameObject 	_cannonBall;
	private float 		_angleLimit;
	private float 		_curAngle = 0;

	void Start () {
		_hinge = 		transform.Find("CannonHinge");
		_spawner = 		_hinge.Find("CannonChamber/BulletSpawner");
		_cannonBall = 	Resources.Load<GameObject>("CannonBall");

		_angleLimit = 	ShootAngle/2;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot ();
		}

		if(Input.GetKey("up")){
			_curAngle -= RotateSpeed * Time.deltaTime;
			_curAngle = Mathf.Clamp(_curAngle, -_angleLimit, +_angleLimit); // update the object rotation: 
			_hinge.localRotation = Quaternion.Euler(0,0,_curAngle); 
			//Debug.Log(_hinge.rotation.z);
		}
		else if(Input.GetKey("down")){
			_curAngle += RotateSpeed * Time.deltaTime;
			_curAngle = Mathf.Clamp(_curAngle, -_angleLimit, +_angleLimit); // update the object rotation: 
			_hinge.localRotation = Quaternion.Euler(0,0,_curAngle); 
			//Debug.Log(_hinge.rotation.z);
		}
	}

	private void Shoot(){
		GameObject bullet = Instantiate(_cannonBall,_spawner.position,_hinge.localRotation) as GameObject;

		Vector3 distance = _spawner.position - _hinge.position;
		Vector3 forceDirection = distance / distance.magnitude;
		bullet.rigidbody.AddForce(CannonPower * forceDirection, ForceMode.Impulse);
		Destroy(bullet,1.0f);
	}
}
