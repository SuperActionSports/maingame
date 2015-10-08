using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedPlayerSelection : MonoBehaviour {

	public Button twoPlayerButton;
	public Button threePlayerButton;
	public Button fourPlayerButton;
	public Button back;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Load2PlayerColorSelection() {
		//save number of players selected
		PlayerPrefs.SetInt ("numOfPlayers", 2);
		//Debug.Log ("Game is loading the Player Color Selection Screen for 2 players");
		Application.LoadLevel ("AdvancedPlayerColorSelection");
	}

	public void Load3PlayerColorSelection() {
		//save number of players selected
		PlayerPrefs.SetInt ("numOfPlayers", 3);
		//Debug.Log ("Game is loading the Player Color Selection Screen for 3 players");
		Application.LoadLevel ("AdvancedPlayerColorSelection");
	}

	public void Load4PlayerColorSelection() {
		//save number of players selected
		PlayerPrefs.SetInt ("numOfPlayers", 4);
		//Debug.Log ("Game is loading the Player Color Selection Screen for 4 players");
		Application.LoadLevel ("AdvancedPlayerColorSelection");
	}

	public void Back() {
		//Debug.Log ("<<---Back--->>");
		Application.LoadLevel ("AdvancedMainMenu");
	}
}
