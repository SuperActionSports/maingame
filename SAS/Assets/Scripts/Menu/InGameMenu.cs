using UnityEngine;
using System.Collections;
using InControl;

public class InGameMenu : MonoBehaviour {

	public KeyCode escape;
	public GameObject menu;

	public InputDevice device {get; set;}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		
		if (StartButtonWasPressedOnDevice( inputDevice ))
		{
			Debug.Log ("Start was pressed pressed");
			Time.timeScale = 0.0f;
			menu.SetActive(true);
		}
	}

	bool StartButtonWasPressedOnDevice( InputDevice inputDevice )
	{
		return inputDevice.Command.WasPressed;
	}


	public void quitMiniGame() {
		Application.LoadLevel ("AdvancedMainMenu");
	}

	public void resumeMiniGame() {

		menu.SetActive(false);
		Time.timeScale = 1.0f;
	}

	public void toLevelSelect() {
		Application.LoadLevel ("AdvancedLevelSelection");
	}
}
