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
			print("Error: Unable to get the number of players from PlayerPrefs");
			number_of_players = 1;
		}

		if(number_of_players>=1){
			//Retrieving Player 1's Settings and Activating
			player_1 = GameObject.Find ("player_1");
			player_1.SetActive (true);
			player_1_color = new int[4];
			player_1_color = PlayerPrefs.GetInt ("player_1_color");
			player_1_controller_num = PlayerPrefs.GetInt ("player_1_controller_num");
		}

		if(number_of_players>=2){
			//Retrieving Player 2's Settings and Activating
			player_2 = GameObject.Find ("player_2");
			player_2.SetActive (true);
			player_2_color = new int[4];
			player_2_color = PlayerPrefs.GetInt ("player_2_color");
			player_2_controller_num = PlayerPrefs.GetInt ("player_2_controller_num");
		}

		if(number_of_players>=3){
			//Retrieving Player 3's Settings and Activating
			player_3 = GameObject.Find ("player_3");
			player_3.SetActive (true);
			player_3_color = new int[4];
			player_3_color = PlayerPrefs.GetInt ("player_3_color");
			player_3_controller_num = PlayerPrefs.GetInt ("player_3_controller_num");
		}

		if(number_of_players>=4){
			//Retrieving Player 4's Settings and Activating
			player_4 = GameObject.Find ("player_4");
			player_4.SetActive (true);
			player_4_color = new int[4];
			player_4_color = PlayerPrefs.GetInt ("player_4_color");
			player_4_controller_num = PlayerPrefs.GetInt ("player_4_controller_num");
		}

	}
	
	// Update is called once per frame
	void Update () {

		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
		//--------------------FOR TESTING PURPOSES ONLY - TO ALLOW MID-GAME CHANGES (BELOW)--------------------//
		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
		if(number_of_players>=1){
			//Retrieving Player 1's Settings and Activating
			player_1 = GameObject.Find ("player_1");
			player_1.SetActive (true);
			player_1_color = new int[4];
			player_1_color = PlayerPrefs.GetInt ("player_1_color");
			player_1_controller_num = PlayerPrefs.GetInt ("player_1_controller_num");
		}
		
		if(number_of_players>=2){
			//Retrieving Player 2's Settings and Activating
			player_2 = GameObject.Find ("player_2");
			player_2.SetActive (true);
			player_2_color = new int[4];
			player_2_color = PlayerPrefs.GetInt ("player_2_color");
			player_2_controller_num = PlayerPrefs.GetInt ("player_2_controller_num");
		}
		
		if(number_of_players>=3){
			//Retrieving Player 3's Settings and Activating
			player_3 = GameObject.Find ("player_3");
			player_3.SetActive (true);
			player_3_color = new int[4];
			player_3_color = PlayerPrefs.GetInt ("player_3_color");
			player_3_controller_num = PlayerPrefs.GetInt ("player_3_controller_num");
		}
		
		if(number_of_players>=4){
			//Retrieving Player 4's Settings and Activating
			player_4 = GameObject.Find ("player_4");
			player_4.SetActive (true);
			player_4_color = new int[4];
			player_4_color = PlayerPrefs.GetInt ("player_4_color");
			player_4_controller_num = PlayerPrefs.GetInt ("player_4_controller_num");
		}
		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
		//--------------------FOR TESTING PURPOSES ONLY - TO ALLOW MID-GAME CHANGES (ABOVE)--------------------//
		//-----------------------------------------------------------------------------------------------------//
		//-----------------------------------------------------------------------------------------------------//
	}
}
