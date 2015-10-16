using UnityEngine;
using System.Collections;

public class LiaisonClass : MonoBehaviour {
	
	/* --------------------------------------------------------------------
	 * --------------------------------------------------------------------
	 * --------------------------------------------------------------------
	 * PlayerPrefs
	 * 
	 * Integers
	 * ------------------------
	 * numOfPlayers
	 * 
	 * 
	 * 
	 * Floats
	 * ------------------------
	 * player_1_color_SliderVal
	 * player_1_colorR
	 * player_1_colorG
	 * player_1_colorB
	 * player_2_color_SliderVal
	 * player_2_colorR
	 * player_2_colorG
	 * player_2_colorB
	 * player_3_color_SliderVal
	 * player_3_colorR
	 * player_3_colorG
	 * player_3_colorB
	 * player_4_color_SliderVal
	 * player_4_colorR
	 * player_4_colorG
	 * player_4_colorB
	 * ------------------------
	 * 
	 * 
	 * Strings
	 * ------------------------
	 * 
	 * 
	 * --------------------------------------------------------------------
	 * --------------------------------------------------------------------
	 * --------------------------------------------------------------------*/

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool SetPlayerColor(float[] player_color, int player_number){
		if (player_color.Length == 3) {
			if(player_color[0] >= 0 && player_color[0] < 256 && player_color[1] >= 0 && player_color[1] < 256 && player_color[2] >= 0 && player_color[2] < 256) {
				switch(player_number){
				case 1:
					PlayerPrefs.SetFloat("player_1_colorR",player_color[0]);
					PlayerPrefs.SetFloat("player_1_colorG",player_color[1]);
					PlayerPrefs.SetFloat("player_1_colorB",player_color[2]);
					break;
				case 2:
					PlayerPrefs.SetFloat("player_2_colorR",player_color[0]);
					PlayerPrefs.SetFloat("player_2_colorG",player_color[1]);
					PlayerPrefs.SetFloat("player_2_colorB",player_color[2]);
					break;
				case 3:
					PlayerPrefs.SetFloat("player_3_colorR",player_color[0]);
					PlayerPrefs.SetFloat("player_3_colorG",player_color[1]);
					PlayerPrefs.SetFloat("player_3_colorB",player_color[2]);
					break;
				case 4:
					PlayerPrefs.SetFloat("player_4_colorR",player_color[0]);
					PlayerPrefs.SetFloat("player_4_colorG",player_color[1]);
					PlayerPrefs.SetFloat("player_4_colorB",player_color[2]);
					break;
				}
				return true;
			} else {
				Debug.LogError("Color values need to be between 0 and 255, inclusive.");
				return false;
			}
		} else {
			Debug.LogError("Argument needs to be a float[] of length 3.");
			return false;
		}
	}
	
	public float[] GetPlayerColor(int player_number){
		float[] player_color = new float[3];
		switch (player_number) {
		case 1:
			player_color[0] = PlayerPrefs.GetFloat ("player_1_colorR");
			player_color[1] = PlayerPrefs.GetFloat ("player_1_colorG");
			player_color[2] = PlayerPrefs.GetFloat ("player_1_colorB");
			break;
		case 2:
			player_color[0] = PlayerPrefs.GetFloat ("player_2_colorR");
			player_color[1] = PlayerPrefs.GetFloat ("player_2_colorG");
			player_color[2] = PlayerPrefs.GetFloat ("player_2_colorB");
			break;
		case 3:
			player_color[0] = PlayerPrefs.GetFloat ("player_3_colorR");
			player_color[1] = PlayerPrefs.GetFloat ("player_3_colorG");
			player_color[2] = PlayerPrefs.GetFloat ("player_3_colorB");
			break;
		case 4:
			player_color[0] = PlayerPrefs.GetFloat ("player_4_colorR");
			player_color[1] = PlayerPrefs.GetFloat ("player_4_colorG");
			player_color[2] = PlayerPrefs.GetFloat ("player_4_colorB");
			break;
		}
		return player_color;
	}

	public float[] GetAllPlayerColors() {
		int numOfPlayers = PlayerPrefs.GetInt("numOfPlayers");

		if (numOfPlayers == 2) {

			float[] all_player_color = new float[6];

			all_player_color[0] = PlayerPrefs.GetFloat ("player_1_colorR");
			all_player_color[1] = PlayerPrefs.GetFloat ("player_1_colorG");
			all_player_color[2] = PlayerPrefs.GetFloat ("player_1_colorB");

			all_player_color[3] = PlayerPrefs.GetFloat ("player_2_colorR");
			all_player_color[4] = PlayerPrefs.GetFloat ("player_2_colorG");
			all_player_color[5] = PlayerPrefs.GetFloat ("player_2_colorB");

			return all_player_color;
		} else if (numOfPlayers == 3) {

			float[] all_player_color = new float[9];

			all_player_color[0] = PlayerPrefs.GetFloat ("player_1_colorR");
			all_player_color[1] = PlayerPrefs.GetFloat ("player_1_colorG");
			all_player_color[2] = PlayerPrefs.GetFloat ("player_1_colorB");

			all_player_color[3] = PlayerPrefs.GetFloat ("player_2_colorR");
			all_player_color[4] = PlayerPrefs.GetFloat ("player_2_colorG");
			all_player_color[5] = PlayerPrefs.GetFloat ("player_2_colorB");

			all_player_color[6] = PlayerPrefs.GetFloat ("player_3_colorR");
			all_player_color[7] = PlayerPrefs.GetFloat ("player_3_colorG");
			all_player_color[8] = PlayerPrefs.GetFloat ("player_3_colorB");

			return all_player_color;
		} else if (numOfPlayers == 4) {

			float[] all_player_color = new float[12];

			all_player_color[0] = PlayerPrefs.GetFloat ("player_1_colorR");
			all_player_color[1] = PlayerPrefs.GetFloat ("player_1_colorG");
			all_player_color[2] = PlayerPrefs.GetFloat ("player_1_colorB");

			all_player_color[3] = PlayerPrefs.GetFloat ("player_2_colorR");
			all_player_color[4] = PlayerPrefs.GetFloat ("player_2_colorG");
			all_player_color[5] = PlayerPrefs.GetFloat ("player_2_colorB");

			all_player_color[6] = PlayerPrefs.GetFloat ("player_3_colorR");
			all_player_color[7] = PlayerPrefs.GetFloat ("player_3_colorG");
			all_player_color[8] = PlayerPrefs.GetFloat ("player_3_colorB");

			all_player_color[9] = PlayerPrefs.GetFloat ("player_4_colorR");
			all_player_color[10] = PlayerPrefs.GetFloat ("player_4_colorG");
			all_player_color[11] = PlayerPrefs.GetFloat ("player_4_colorB");

			return all_player_color;
		} else {
			Debug.LogError("Incorrect numOfPlayers value or not stored correctly");
			return null;
		}
	}
}


























