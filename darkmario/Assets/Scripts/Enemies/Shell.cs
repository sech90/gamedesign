using UnityEngine;
using System.Collections;

public class Shell : Enemy {

	private AISimpleMovement _movement;
	private DIRECTION _direction;
	private Mario _mario;
	private float respawnTime = 5.00f;
	private float startTime;

	void Start(){
		_movement = GetComponent<AISimpleMovement>();
		startTime = Time.time;
	}

	void Update(){
		if(_movement.enabled == false && Time.time - startTime >= respawnTime)
			Respawn();
	}


	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log("Shell collided with "+coll.gameObject.name);
		Enemy enemy = coll.gameObject.GetComponent<Enemy>();
		if(_movement.enabled && enemy != null)
			enemy.Kill();
	}

	public override void OnMarioContact (Mario mario,  Collision2D coll = null)
	{
		if(_movement.enabled == true)
			base.OnMarioContact (mario);
		else{
			gameObject.layer = LayerMask.NameToLayer("RunningEnemies");
			if(coll.contacts[0].normal.x == -1)
				_direction = DIRECTION.RIGHT;
			else
				_direction = DIRECTION.LEFT;

			_movement.enabled = true;
			if(_movement.direction != _direction)
				_movement.Flip();
		}
	}

	override public int Stomped(Mario mario){
		_movement.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("Enemies");
		startTime = Time.time;
		return 0;
	}

	private void Respawn(){
		GameObject turtle = Resources.Load("Turtle", typeof(GameObject)) as GameObject;
		Vector3 pos = transform.position;
		Quaternion rot = transform.rotation;
		Instantiate(turtle,pos,rot);
		Destroy(this.gameObject);
	}
}
