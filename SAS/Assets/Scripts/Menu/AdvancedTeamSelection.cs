using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedTeamSelection : MonoBehaviour {


	public Button skip;
	public Button back;
	public int numOfPlayers;

	// Use this for initialization
	void Start () {
		try {
			numOfPlayers = PlayerPrefs.GetInt ("numOfPlayers");
		}
		catch (Exception e) {
			Debug.Log("error loading numOfPlayers");
		}
		if (numOfPlayers == 2) {
			GameObject.Find("PlayerTeamPicker (1)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (2)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (3)").SetActive(false);
			GameObject.Find("PlayerTeamPicker (4)").SetActive(false);
		}
		if (numOfPlayers == 3) {
			GameObject.Find("PlayerTeamPicker (1)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (2)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (3)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (4)").SetActive(false);
		}
		if (numOfPlayers == 4) {
			GameObject.Find("PlayerTeamPicker (1)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (2)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (3)").SetActive(true);
			GameObject.Find("PlayerTeamPicker (4)").SetActive(true);
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
