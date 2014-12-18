using UnityEngine;
using System.Collections;

public class LizardMonster : Monster {

	public float xSpeed = 1.0f;
	static public int _headCount = 0;

	static public int GetNumberOf()
	{
		return _headCount;
	}
	// Use this for initialization
	void Start () {
		_mode = MonsterMode.Approach;
		_headCount++;

		float x = 15.0f;
		if (Random.Range(0.0f, 2.0f) > 1.0f)
			x = -15;

		transform.position = new Vector3(Ship.instance.transform.position.x + x, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x > Ship.instance.transform.position.x)
			SetFacing(MonsterFacing.Left);
		else
			SetFacing(MonsterFacing.Right);


		if (_mode == MonsterMode.Approach)
			Approach();

		MonsterUpdate();
	}

	override protected void Die(){
	
		FloatingObject fo = this.GetComponent<FloatingObject>();
		
			fo.isFloating = false;
			fo.isSinking = true;

		if (transform.position.y < -5.0f)
		{
			Destroy(this.gameObject);
			_headCount--;
		}


	}


	void Approach() {
		Vector3 shipPos = Ship.instance.transform.position;
		float x = Mathf.MoveTowards( transform.position.x, shipPos.x, xSpeed * Time.deltaTime );
		transform.position = new Vector3( x, transform.position.y, transform.position.z); 
	}

	void OnTriggerEnter2D(Collider2D coll){
		Ship ship = coll.GetComponent<Ship>();
		if (ship!=null) {
			if (_facing == MonsterFacing.Right )
				Ship.instance.TakeDamage( AttackPower, 2);
			else
				Ship.instance.TakeDamage( AttackPower, 3);

			//ship.CurrentHp -= AttackPower;
			_mode = MonsterMode.Dying;
		}
	}
}
