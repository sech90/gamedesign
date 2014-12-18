﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class StartScreen : MonoBehaviour {

	public Image StartButtons;
	public Sprite NewGameSprite;
	public Sprite HowtoSprite;
	public AudioClip TransitionSound;
	//public AudioClip StartGameSound;

	private GameObject _panel;
	private bool _newGameSelected = true;
	private bool _instructionVisible = false;
	private float cooldown = 0.2f;


	void Start(){
		_panel = GameObject.FindGameObjectWithTag("HowTo");
		_panel.SetActive(false);

		//_pointer = new PointerEventData(EventSystem.current);
		//ShowHowTo.onClick.AddListener(showInstructionPanel);
		//StartGame.onClick.AddListener(LoadGame);
		//StartGame.Select();
	}

	void Update(){

		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && !_instructionVisible){
			_newGameSelected = true;
			StartButtons.sprite = NewGameSprite;
			return;
		}

		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && !_instructionVisible){
			_newGameSelected = false;
			StartButtons.sprite = HowtoSprite;
			return;
		}

		if(Input.GetKeyDown(KeyCode.Return)){
			if(_newGameSelected == true)
				LoadGame();
			else{
				if(!_instructionVisible)
					showInstructionPanel();
				else
					hideInstructionPanel();
			}
		}
	}

	private void LoadGame(){
	//	AudioSource.PlayClipAtPoint(StartGameSound,Vector3.zero);

		Application.LoadLevel("RikuScene");
	}


	private void showInstructionPanel(){
		_instructionVisible = true;
		AudioSource.PlayClipAtPoint(TransitionSound,Vector3.zero);
		_panel.SetActive(true);
	}

	private void hideInstructionPanel(){
		_instructionVisible = false;
		//AudioSource.PlayClipAtPoint(TransitionSound,Vector3.zero);
		_panel.SetActive(false);
	}
}
