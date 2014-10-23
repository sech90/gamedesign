using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour {
	
	private int _score;
	private int _coins;
	private int _lives = 3;
	
	private ItemCollector _collector = null;
	private MarioMovement _motion = null;
	private SuperPower big, fire, star;
	private LayerMask bricksLayer, enemyLayer;

	public int Score{get{return _score;}}
	public int Coins{get{return _coins;}}
	public int Lives{get{return _lives;}}
	
	public float DelayAfterHit = 3.0f;

	void Start(){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Player"),true);
		big = star = fire = null;
		bricksLayer = LayerMask.NameToLayer("Obstacles");
		enemyLayer  = LayerMask.NameToLayer("Enemies");

		_motion = GetComponent<MarioMovement>();
		_collector = GetComponent<ItemCollector>();
		_collector.OnItemCollision += pickItem;
		Debug.Log((int)bricksLayer);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll == null)
			return;

		//Hit an enemy
		if(coll.gameObject.layer == enemyLayer){
			//Stomped an enemy with feet
			Debug.Log("**** Enemy! Contacts: "+coll.contacts.Length);
			bool isStomped = false;
			int i = 0;
			while(i < coll.contacts.Length && !isStomped){
				Debug.Log(coll.contacts[i].otherCollider.name);
				Debug.Log(coll.contacts[i].normal);

				if(coll.contacts[i].otherCollider.tag == "MarioFeet" || coll.contacts[i].normal.y == 1)
					isStomped = true;
				i++;
			}
			Debug.Log("**** Stomped: "+isStomped);
			if(isStomped){
			//if(coll.contacts[0].normal.y == 1){
				Enemy enemy = coll.contacts[0].collider.gameObject.GetComponent<Enemy>();
				if(enemy != null){
					_motion.Jump();
					_score += enemy.Hit();
				}
			}
			//Hit by an enemy
			else
				hitByEnemy();
		}
			

		//Hit a brick 
		else if(coll.gameObject.layer == bricksLayer){
			if(coll.contacts[0].otherCollider.tag == "MarioHead"){
			//if(coll.contacts[0].normal.y == -1){
				Brick brick = coll.contacts[0].collider.gameObject.GetComponent<Brick>();
				if(brick != null){
					brick.OnHit(gameObject);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll != null)
			if(coll.tag == "DeathPit")
				Die();
	}

	public void Die(){
		Debug.Log("Mario Died");
		//Destroy(gameObject);
		gameObject.SetActive(false);
	}

	private void hitByEnemy(){
		if(star == null){
			if(fire != null){
				fire.Remove();
				fire = null;
			}

			if(big != null){
				big.Remove();
				big = null;
			}
			else{
				Die();
				return;
			}
			StartCoroutine(ApplyInvulnerability());
		}
	}

	private void pickItem(Item item){

		if(item.ItemType == ITEM.COIN)
			_coins++;
		else{
			switch(item.superpower){
				case POWER.BIG:
					if(big == null)
						big = gameObject.AddComponent<BigPower>();
					break;
				case POWER.FIRE:
					if(big == null)
						big = gameObject.AddComponent<BigPower>();
					else if(fire == null)
						fire = gameObject.AddComponent<FirePower>();
					break;
				case POWER.INVINCIBLE:
					
					if(star == null)
						star = gameObject.AddComponent<StarPower>();
					else
						(star as StarPower).Reload();
					break;
			}
		}

		_score += item.points;
		Destroy(item.gameObject);
	}

	//Needed when hitting a brick and gets a coin from it
	//HACK: Coin's score must not be hardcoded like this....
	public void GetCoin(){
		_coins++;
		_score+=200;
	}
	
	IEnumerator ApplyInvulnerability()
	{
		Debug.Log("Inulnerable");
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Enemies"),true);
		yield return new WaitForSeconds(DelayAfterHit);
		Debug.Log("Vulnerable again");
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Enemies"),false);
	}
}
