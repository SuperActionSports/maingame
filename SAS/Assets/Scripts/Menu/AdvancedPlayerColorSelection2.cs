using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedPlayerColorSelection2 : MonoBehaviour {


	public Button skip;
	public Button back;
	public int numOfPlayers;

	// Use this for initialization
	void Start () {
		try {
			numOfPlayers = PlayerPrefs.GetInt ("numOfPlayers");
		}
		catch {
			Debug.Log("error loading numOfPlayers");
		}
		if (numOfPlayers == 2) {
			GameObject.Find("PlayerColorPicker (1)").SetActive(true);
			GameObject.Find("PlayerColorPicker (2)").SetActive(true);
			GameObject.Find("PlayerColorPicker (3)").SetActive(false);
			GameObject.Find("PlayerColorPicker (4)").SetActive(false);
		}
		if (numOfPlayers == 3) {
			GameObject.Find("PlayerColorPicker (1)").SetActive(true);
			GameObject.Find("PlayerColorPicker (2)").SetActive(true);
			GameObject.Find("PlayerColorPicker (3)").SetActive(true);
			GameObject.Find("PlayerColorPicker (4)").SetActive(false);
		}
		if (numOfPlayers == 4) {
			GameObject.Find("PlayerColorPicker (1)").SetActive(true);
			GameObject.Find("PlayerColorPicker (2)").SetActive(true);
			GameObject.Find("PlayerColorPicker (3)").SetActive(true);
			GameObject.Find("PlayerColorPicker (4)").SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LoadLevelSelection() {
		//Debug.Log ("Game is loading the Level Selection Screen");
		Application.LoadLevel ("AdvancedLevelSelection");
	}

	public void Back() {
		//Debug.Log ("<<---Back--->>");
		Application.LoadLevel ("AdvancedPlayerSelection");
	}
}
