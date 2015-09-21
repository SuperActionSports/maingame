using UnityEngine;
using System.Collections;
using System;

public class PlayerActivation : MonoBehaviour {

	public int number_of_players;

	//Player 1 Settings
	public GameObject player_1;
	public int[] player_1_color;
	public int player_1_controller_num;

	//Player 2 Settings
	public GameObject player_2;
	public int[] player_2_color;
	public int player_2_controller_num;

	//Player 3 Settings
	public GameObject player_3;
	public int[] player_3_color;
	public int player_3_controller_num;

	//Player 4 Settings
	public GameObject player_4;
	public int[] player_4_color;
	public int player_4_controller_num;

	// Use this for initialization
	void Start () {

		try {
			number_of_players = PlayerPrefs.GetInt ("number_of_players");
		}
		catch (Exception e) {
			print ("Error: Unable to get the number of players from PlayerPrefs");
			number_of_players = 1;
		}
		/*
		if(number_of_players>=1){
			//Retrieving Player 1's Settings
			player_1 = GameObject.Find ("player_1");
			player_1_color = new int[4];
			player_1_color[0] = PlayerPrefs.GetInt ("player_1_color_r");
			player_1_color[1] = PlayerPrefs.GetInt ("player_1_color_g");
			player_1_color[2] = PlayerPrefs.GetInt ("player_1_color_b");
			player_1_color[3] = PlayerPrefs.GetInt ("player_1_color_a");
			player_1_controller_num = PlayerPrefs.GetInt ("player_1_controller_num");
		}

		if(number_of_players>=2){
			//Retrieving Player 2's Settings
			player_2 = GameObject.Find ("player_2");
			player_2_color = new int[4];
			player_2_color[0] = PlayerPrefs.GetInt ("player_2_color_r");
			player_2_color[1] = PlayerPrefs.GetInt ("player_2_color_g");
			player_2_color[2] = PlayerPrefs.GetInt ("player_2_color_b");
			player_2_color[3] = PlayerPrefs.GetInt ("player_2_color_a");
			player_2_controller_num = PlayerPrefs.GetInt ("player_2_controller_num");
		}

		if(number_of_players>=3){
			//Retrieving Player 3's Settings
			player_3 = GameObject.Find ("player_3");
			player_3_color = new int[4];
			player_3_color[0] = PlayerPrefs.GetInt ("player_3_color_r");
			player_3_color[1] = PlayerPrefs.GetInt ("player_3_color_g");
			player_3_color[2] = PlayerPrefs.GetInt ("player_3_color_b");
			player_3_color[3] = PlayerPrefs.GetInt ("player_3_color_a");
			player_3_controller_num = PlayerPrefs.GetInt ("player_3_controller_num");
		}

		if(number_of_players>=4){
			//Retrieving Player 4's Settings
			player_4 = GameObject.Find ("player_4");
			player_4_color = new int[4];
			player_4_color[0] = PlayerPrefs.GetInt ("player_4_color_r");
			player_4_color[1] = PlayerPrefs.GetInt ("player_4_color_g");
			player_4_color[2] = PlayerPrefs.GetInt ("player_4_color_b");
			player_4_color[3] = PlayerPrefs.GetInt ("player_4_color_a");
			player_4_controller_num = PlayerPrefs.GetInt ("player_4_controller_num");
		}
*/
		if (number_of_players == 2) {
			player_1.SetActive (true);
			player_2.SetActive (true);
			player_3.SetActive (false);
			player_4.SetActive (false);
		}
		else if (number_of_players == 3) {
			player_1.SetActive (true);
			player_2.SetActive (true);
			player_3.SetActive (true);
			player_4.SetActive (false);
		}
		else if (number_of_players == 4) {
			player_1.SetActive (true);
			player_2.SetActive (true);
			player_3.SetActive (true);
			player_4.SetActive (true);
		}
		else {
			player_1.SetActive (true);
			player_2.SetActive (true);
			player_3.SetActive (false);
			player_4.SetActive (false);
			number_of_players = 2;
		}

	}
	
	// Update is called once per frame
	void Update () {

		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
		//--------------------FOR TESTING PURPOSES ONLY - TO ALLOW MID-GAME CHANGES (BELOW)--------------------//
		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
		/*if(number_of_players>=1){
			//Retrieving Player 1's Settings
			player_1_color[0] = PlayerPrefs.GetInt ("player_1_color_r");
			player_1_color[1] = PlayerPrefs.GetInt ("player_1_color_g");
			player_1_color[2] = PlayerPrefs.GetInt ("player_1_color_b");
			player_1_color[3] = PlayerPrefs.GetInt ("player_1_color_a");
			player_1_controller_num = PlayerPrefs.GetInt ("player_1_controller_num");
		}
		
		if(number_of_players>=2){
			//Retrieving Player 2's Settings
			player_2_color[0] = PlayerPrefs.GetInt ("player_2_color_r");
			player_2_color[1] = PlayerPrefs.GetInt ("player_2_color_g");
			player_2_color[2] = PlayerPrefs.GetInt ("player_2_color_b");
			player_2_color[3] = PlayerPrefs.GetInt ("player_2_color_a");
			player_2_controller_num = PlayerPrefs.GetInt ("player_2_controller_num");
		}
		
		if(number_of_players>=3){
			//Retrieving Player 3's Settings
			player_3_color[0] = PlayerPrefs.GetInt ("player_3_color_r");
			player_3_color[1] = PlayerPrefs.GetInt ("player_3_color_g");
			player_3_color[2] = PlayerPrefs.GetInt ("player_3_color_b");
			player_3_color[3] = PlayerPrefs.GetInt ("player_3_color_a");
			player_3_controller_num = PlayerPrefs.GetInt ("player_3_controller_num");
		}
		
		if(number_of_players>=4){
			//Retrieving Player 4's Settings
			player_4_color[0] = PlayerPrefs.GetInt ("player_4_color_r");
			player_4_color[1] = PlayerPrefs.GetInt ("player_4_color_g");
			player_4_color[2] = PlayerPrefs.GetInt ("player_4_color_b");
			player_4_color[3] = PlayerPrefs.GetInt ("player_4_color_a");
			player_4_controller_num = PlayerPrefs.GetInt ("player_4_controller_num");
		}*/
		
		if (number_of_players == 2) {
			player_1.SetActive (true);
			player_2.SetActive (true);
			player_3.SetActive (false);
			player_4.SetActive (false);
		}
		else if (number_of_players == 3) {
			player_1.SetActive (true);
			player_2.SetActive (true);
			player_3.SetActive (true);
			player_4.SetActive (false);
		}
		else if (number_of_players == 4) {
			player_1.SetActive (true);
			player_2.SetActive (true);
			player_3.SetActive (true);
			player_4.SetActive (true);
		}
		else {
			player_1.SetActive (true);
			player_2.SetActive (false);
			player_3.SetActive (false);
			player_4.SetActive (false);
			number_of_players = 1;
		}
		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
		//--------------------FOR TESTING PURPOSES ONLY - TO ALLOW MID-GAME CHANGES (ABOVE)--------------------//
		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
	}
}
