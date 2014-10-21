using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour {
	
	private int _score;
	private int _coins;
	private int _lives = 3;

	public int Score{get{return _score;}}
	public int Coins{get{return _coins;}}
	public int Lives{get{return _lives;}}
	
	private ItemCollector _collector = null;

	void Start(){
		_collector = GetComponent<ItemCollector>();
		_collector.OnItemCollision += pickItem;
	}

	public void Die(){
		Debug.Log("Mario Died");
		Destroy(this);
	}

	private void pickItem(Item item){

		if(item.ItemType == ITEM.COIN)
			_coins++;
		else{
			switch(item.superpower){
				case POWER.BIG:
					if((GetComponent<BigPower>() as SuperPower) == null)
						gameObject.AddComponent<BigPower>();
					break;
				case POWER.FIRE:
					if((GetComponent<BigPower>() as SuperPower) == null)
						gameObject.AddComponent<BigPower>();
					else if((GetComponent<FirePower>() as SuperPower) == null)
						gameObject.AddComponent<FirePower>();
					break;
				case POWER.INVINCIBLE:
					StarPower power = (GetComponent<StarPower>() as StarPower);
					if(power == null)
						gameObject.AddComponent<StarPower>();
					else
						power.Reload();
					break;
			}
		}

		_score += item.points;
		Debug.Log("Now scorea is "+_score);
		Destroy(item.gameObject);
	}
}
