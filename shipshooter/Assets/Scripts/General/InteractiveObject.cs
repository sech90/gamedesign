using UnityEngine;
using System.Collections;


public abstract class InteractiveObject : MonoBehaviour {

	abstract protected void OnButtonPressed(ButtonDir key);
	abstract protected void OnButtonHold(ButtonDir key);
	abstract protected void OnButtonRelease(ButtonDir key);

	//private bool _isOnTriggerArea = false;
	//protected ButtonDir[] _keyList; 

	void OnTriggerEnter2D(Collider2D coll){
		Sailorman sailor = coll.GetComponent<Sailorman>();
		if(sailor != null){
			sailor.OnButtonPressed += OnButtonPressed;
			sailor.OnButtonHold += OnButtonHold;
			sailor.OnButtonRelease += OnButtonRelease;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		Sailorman sailor = coll.GetComponent<Sailorman>();
		if(sailor != null){
			sailor.OnButtonPressed -= OnButtonPressed;
			sailor.OnButtonHold -= OnButtonHold;
			sailor.OnButtonRelease -= OnButtonRelease;
		}
	}
	/*
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
	}*/

}
