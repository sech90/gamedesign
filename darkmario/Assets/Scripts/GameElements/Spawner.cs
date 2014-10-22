using UnityEngine;
using System.Collections;

public class Spawner : Brick {

	public Item item;
	public int numberOfCoins = 0;
	private bool _isSolid = false;

	void Start(){
		if(item != null && numberOfCoins > 0){
			Debug.LogWarning("Spawner only requires the Item or the coins. Assuming this is a coin spawner");
			item = null;
		}
	}

	override public void OnHit(GameObject hitter){
		if(!_isSolid){
			if(item != null){
				Instantiate(item);
				TurnSolid();
			}
			else{
				numberOfCoins--;
				if(numberOfCoins <= 0)
					TurnSolid();
				if(numberOfCoins >= 0)
					hitter.GetComponent<Mario>().GetCoin();
			}
		}
	}

	private void TurnSolid(){
		_isSolid = true;
		Sprite blockSprite = Resources.Load("block", typeof(Sprite)) as Sprite;
		GetComponent<SpriteRenderer>().sprite = blockSprite;
	}
}
