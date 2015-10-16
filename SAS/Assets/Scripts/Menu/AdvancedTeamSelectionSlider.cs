using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedTeamSelectionSlider : MonoBehaviour {

	public Slider player_slider;
	public int player_num;
	public int numOfPlayers;

	// Use this for initialization
	void Start () {
		player_slider.maxValue = 2;
		player_slider.minValue = 0;
		player_slider.value = 1;
		player_slider.wholeNumbers = true;
		try {
			numOfPlayers = PlayerPrefs.GetInt ("numOfPlayers");
		}
		catch (Exception e) {
			Debug.Log("error loading numOfPlayers. Error Code: " + e.ToString());
		}
		try {
			GetPlayerTeam();
		}
		catch (Exception e) {
			Debug.Log("error loading player teams. Error Code: " + e.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		player_Team ((int)player_slider.value);
	}

	void player_Team(int slideNum){
		switch (slideNum) {
		case 0:
			//Player is on Home Team
			SetPlayerTeam(0);
			break;
		case 1:
			//Player is neutral/no team
			SetPlayerTeam(1);
			break;
		case 2:
			//Player is on Away Team
			SetPlayerTeam(2);
			break;
		}
	}

	public void SetPlayerTeam(int team) {
		switch(player_num){
		case 1:
			PlayerPrefs.SetInt("player_1_team",team);
			break;
		case 2:
			PlayerPrefs.SetInt("player_2_team",team);
			break;
		case 3:
			PlayerPrefs.SetInt("player_3_team",team);
			break;
		case 4:
			PlayerPrefs.SetInt("player_4_team",team);
			break;
		}
	}

	public int GetPlayerTeam() {
		switch(player_num){
		case 1:
			return PlayerPrefs.GetInt("player_1_team");
		case 2:
			return PlayerPrefs.GetInt("player_2_team");
		case 3:
			return PlayerPrefs.GetInt("player_3_team");
		case 4:
			return PlayerPrefs.GetInt("player_4_team");
		default:
			return -1;
		}
	}


}
