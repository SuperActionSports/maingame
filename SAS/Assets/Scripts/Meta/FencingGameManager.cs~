using UnityEngine;
using System.Collections;

public class FencingGameManager : MonoBehaviour {

	public GameObject playerPrefab;
	private GameObject[] players;
	private PlayerControllerMatt[] controls;
	private Color[] colors;
	private Vector3[] respawnPointPositions;
	public GameObject[] respawnPoints;
	private int totalPlayers;
	private int remainingPlayers;
	private int winner;
	public GameObject victory;
	private float victoryDuration;
	private float victoryWait;
	private float gameWinTime;
	private int matchCount;
	
	// Use this for initialization
	void Start () {
	/*
	Get a list of valid players from UserPrefs along with their device
	Store these players into players[] and their colors into colors[]
	*/
		if (respawnPoints.Length != 5)
		{
			Debug.LogError("Invalid number of respawn points. You need 5, you have " + respawnPoints.Length);
		}
		for (int i = 0; i < respawnPoints.Length; i++)
		{
			respawnPointPositions[i] = respawnPoints[i].transform.position;	
		}
		winner = 0;
		totalPlayers = players.Length;
		controls = new PlayerControllerMatt[totalPlayers];
		remainingPlayers = totalPlayers;
		matchCount = 0;
		victoryDuration = 3;
		victoryWait = 3;
		gameWinTime = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time < gameWinTime + victoryDuration)
		{
			//Do thing
			if (Time.time + Time.deltaTime >= gameWinTime + victoryDuration)
			{
				ResetPlayers();
				//Fix camera
			}
		}
	}
	
	private void SetPlayers()
	{
		
		switch(players.Length)
		{
			case(2):
			if(matchCount < 1 || controls[0].alive) controls[0] = Spawn (respawnPointPositions[0],0);
			if(matchCount < 1 || controls[1].alive)controls[1] = Spawn (respawnPointPositions[4],1);
			break;
			
			case (3):
			if(matchCount < 1 || controls[0].alive)controls[0] = Spawn (respawnPointPositions[0],0);
			if(matchCount < 1 || controls[1].alive)controls[1] = Spawn (respawnPointPositions[2],1);
			if(matchCount < 1 || controls[2].alive)controls[2] = Spawn (respawnPointPositions[4],2);
			break;
			
			case(4):
			if(matchCount < 1 || controls[0].alive)controls[0] = Spawn (respawnPointPositions[0],0);
			if(matchCount < 1 || controls[1].alive)controls[1] = Spawn (respawnPointPositions[1],1);
			if(matchCount < 1 || controls[2].alive)controls[2] = Spawn (respawnPointPositions[3],2);
			if(matchCount < 1 || controls[3].alive)controls[3] = Spawn (respawnPointPositions[4],3);
			break;
			
			default:
			Debug.Log ("Invalid number of players [2,4]: " + players.Length);
			break;
		}
	}
	
	
	private PlayerControllerMatt Spawn(Vector3 position, int playerNumber)
	{
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		
		// Replace this noise with the player prefs file information
		p.GetComponent<PlayerControllerMatt>().color = Color.black;
		
		p.GetComponent<PlayerControllerMatt>().wizard = this;
		
		// Replace this with the device information from userprefs
		p.GetComponent<PlayerInputHandlerMatt>().device = null; 
		return p.GetComponent<PlayerControllerMatt>();
	}
	
	private void UpdatePlayerCount()
	{
		for (int i = 0; i<totalPlayers;i++)
		{
			if (controls[i].alive) {winner = i;}
			else {remainingPlayers--;}
		}
		if (remainingPlayers <2)
		{
			Victory();
		}
	}
	
	private void Victory()
	{
		//victory.GetComponent<>().Celebrate(colors[winner]);
		gameWinTime = Time.time;
	}
	
	private void ResetPlayers()
	{
		matchCount++;
		SetPlayers();
	}
}
