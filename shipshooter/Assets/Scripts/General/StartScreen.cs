using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public Button ShowHowTo;
	public Button CloseHowTo;
	public AudioClip TransitionSound;
	public Text FadeText;

	private GameObject _panel;


	void Start(){
		_panel = GameObject.FindGameObjectWithTag("HowTo");
		ShowHowTo.onClick.AddListener(showInstructionPanel);
		CloseHowTo.onClick.AddListener(hideInstructionPanel);
		_panel.SetActive(false);
	}

	void Update(){

	 

		if(Input.anyKeyDown)
			if(!Input.GetKeyDown(KeyCode.Mouse0) && !_panel.activeInHierarchy)
				Application.LoadLevel("MainScene");
		
	}


	private void showInstructionPanel(){
		AudioSource.PlayClipAtPoint(TransitionSound,Vector3.zero);
		_panel.SetActive(true);
	}

	private void hideInstructionPanel(){
		AudioSource.PlayClipAtPoint(TransitionSound,Vector3.zero);
		_panel.SetActive(false);
	}
}
