using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNumberSelection : MonoBehaviour {

	public int number_of_players;
	
	//Player 1 Settings
	public GameObject player_1;
	public int player_1_controller_num;
	
	//Player 2 Settings
	public GameObject player_2;
	public int player_2_controller_num;
	
	//Player 3 Settings
	public GameObject player_3;
	public int player_3_controller_num;
	
	//Player 4 Settings
	public GameObject player_4;
	public int player_4_controller_num;

	public Button players_2;
	public Button players_3;
	public Button players_4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartLevel1() {
//		Application.LoadLevel (level1name);
	}

}
