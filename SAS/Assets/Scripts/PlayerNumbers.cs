using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNumbers : MonoBehaviour {

	public Button twoPlayers;
	public Button threePlayers;
	public Button fourPlayers;
	public Button backButton;
	public int numOfPlayers;

	// Use this for initialization
	void Start () {
		twoPlayers = twoPlayers.GetComponent<Button> ();
		threePlayers = threePlayers.GetComponent<Button> ();
		fourPlayers = fourPlayers.GetComponent<Button> ();
		backButton = backButton.GetComponent<Button> ();
	}

	public void TwoPlayers() {
		PlayerPrefs.SetInt("numOfPlayers", 2);
		numOfPlayers = 2;
		Application.LoadLevel ("LevelScreen");
	}
	
	public void ThreePlayers() {
		PlayerPrefs.SetInt("numOfPlayers", 3);
		numOfPlayers = 3;
		Application.LoadLevel ("LevelScreen");
	}
	
	public void FourPlayers() {
		PlayerPrefs.SetInt("numOfPlayers", 4);
		numOfPlayers = 4;
		Application.LoadLevel ("LevelScreen");
	}
	
	public void MainMenu() {
		Application.LoadLevel ("TitleScreen");
	}

	public static void Save()
	{

	}

	// Update is called once per frame
	void Update () {
		Save();
	}

	void Example() {
		PlayerPrefs.SetInt("Player Score", 10);
		PlayerPrefs.SetFloat("Player Score", 10.0F);
		PlayerPrefs.SetString("Player Name", "Foobar");
	}
}
