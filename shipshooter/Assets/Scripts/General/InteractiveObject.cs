using UnityEngine;
using System.Collections;

public abstract class InteractiveObject : MonoBehaviour {

	abstract protected void OnButtonPressed(KeyCode key);
	abstract protected void OnButtonHold(KeyCode key);
	abstract protected void OnButtonRelease(KeyCode key);

	private bool _isOnTriggerArea = false;
	protected KeyCode[] _keyList; 

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.GetComponent<Sailorman>() != null)
			_isOnTriggerArea = true;
	}

	void OnTriggerExit2D(Collider2D coll){
		if(coll.GetComponent<Sailorman>() != null)
			_isOnTriggerArea = false;
	}

	void Update(){
		if(_isOnTriggerArea && Input.anyKey){

			for(int i=0;i<_keyList.Length; i++){

				if(Input.GetKeyDown(_keyList[i]))
					OnButtonPressed(_keyList[i]);

				else if(Input.GetKeyUp(_keyList[i]))
					OnButtonRelease(_keyList[i]);

				else if(Input.GetKey(_keyList[i]))
					OnButtonHold(_keyList[i]);


			}
		}
	}

}
