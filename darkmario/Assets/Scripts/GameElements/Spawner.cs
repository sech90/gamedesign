using UnityEngine;
using System.Collections;

public class Spawner : Brick {
	
	public Item item;
	public int numberOfCoins = 0;
	public AudioClip SpawnAudio;
	public AudioClip CoinAudio;

	private bool _isSolid = false;
	private Vector3 _spawnPosition;

	void Start(){
		float yExtent = renderer.bounds.extents.y;
		_spawnPosition = transform.position;
		_spawnPosition.y += yExtent;

		if(item != null && numberOfCoins > 0){
			Debug.LogWarning("Spawner only requires the Item or the coins. Assuming this is a coin spawner");
			item = null;
		}
	}

	override public void SpecialEffect(GameObject hitter){
		if(!_isSolid){
			if(item != null){
				Instantiate(item,_spawnPosition,transform.rotation);
				AudioSource.PlayClipAtPoint(SpawnAudio,transform.position);
				TurnSolid();
			}
			else{
				AudioSource.PlayClipAtPoint(CoinAudio,transform.position);
				numberOfCoins--;
				if(numberOfCoins <= 0)
					TurnSolid();
				if(numberOfCoins >= 0)
					hitter.GetComponent<Mario>().GetCoin();
			}
			_animator.SetTrigger("Bounce");
		}
	}

	private void TurnSolid(){
		_isSolid = true;
		_animator.SetTrigger("TurnSolid");
		Destroy(GetComponent<Spawner>());
	}
}
