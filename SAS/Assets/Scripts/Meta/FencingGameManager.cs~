using UnityEngine;
using System.Collections;
using InControl;

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
	private float gameWinTime;
	private int matchCount;
	private FencingCameraController cam;
	private FencingPlayerManager inputManager;
	private InputDevice[] devices;
	// Use this for initialization
	void Start () {
	/*
	Get a list of valid players from UserPrefs along with their device
	Store these players into players[] and their colors into colors[]
	*/
		players = GameObject.FindGameObjectsWithTag("Player");
		if (respawnPoints.Length != 5)
		{
			Debug.LogError("Invalid number of respawn points. You need 5, you have " + respawnPoints.Length);
		}
		else{
			respawnPointPositions = new Vector3[respawnPoints.Length];
		}
		for (int i = 0; i < respawnPoints.Length; i++)
		{
			respawnPointPositions[i] = respawnPoints[i].transform.position;	
		}
		winner = 0;
		if (players.Length > 0)
		{
			totalPlayers = players.Length;
			controls = new PlayerControllerMatt[totalPlayers];
			devices = new InputDevice[totalPlayers];
			remainingPlayers = totalPlayers;
		}
		matchCount = 0;
		victoryDuration = 3;
		gameWinTime = -10000;
		
		cam = Camera.main.GetComponent<FencingCameraController>();
		inputManager = GetComponent<FencingPlayerManager>();
	}

	// Update is called once per frame
	void Update () {
		if (Time.time < gameWinTime + victoryDuration)
		{
			cam.FollowWinner(players[winner]);
			if (Time.time + Time.deltaTime >= gameWinTime + victoryDuration)
			{
				ResetPlayers();
			}
		}
	}
	
	private void SetPlayers()
	{
		// Set players should be SPAWN players. SpawnPlayers should reset the position of the players to one of the given respawn points, set them to active and alive, etc
		// This should handle the device concern too. 	
		switch(players.Length)
		{
			case(2):
			if(matchCount < 1 || !controls[0].alive)controls[0] = Spawn (respawnPointPositions[0],0);
			if(matchCount < 1 || !controls[1].alive)controls[1] = Spawn (respawnPointPositions[4],1);
			break;
			
			case (3):
			if(matchCount < 1 || !controls[0].alive)controls[0] = Spawn (respawnPointPositions[0],0);
			if(matchCount < 1 || !controls[1].alive)controls[1] = Spawn (respawnPointPositions[2],1);
			if(matchCount < 1 || !controls[2].alive)controls[2] = Spawn (respawnPointPositions[4],2);
			break;
			
			case(4):
			if(matchCount < 1 || !controls[0].alive)controls[0] = Spawn (respawnPointPositions[0],0);
			if(matchCount < 1 || !controls[1].alive)controls[1] = Spawn (respawnPointPositions[1],1);
			if(matchCount < 1 || !controls[2].alive)controls[2] = Spawn (respawnPointPositions[3],2);
			if(matchCount < 1 || !controls[3].alive)controls[3] = Spawn (respawnPointPositions[4],3);
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
		p.GetComponent<PlayerControllerMatt>().color = colors[playerNumber];
		p.GetComponent<PlayerControllerMatt>().wizard = this;
		
		// Replace this with the device information from userprefs
		p.GetComponent<PlayerInputHandlerMatt>().device = devices[playerNumber];
		
		//p.GetComponent<PlayerInputHandlerMatt>().device = null; 
		return p.GetComponent<PlayerControllerMatt>();
	}
	
	public void UpdatePlayerCount()
	{
		for (int i = 0; i<=totalPlayers;i++)
		{
			if (controls[i].alive) 
			{
				winner = i;
			}
			else 
			{
				remainingPlayers--;
			}
		}
		if (remainingPlayers <2)
		{
			Victory();
		}
	}
	
	public void UpdatePlayerTotal(int newCount)
	{
		totalPlayers = newCount;		
		winner = 0;
		totalPlayers = players.Length;
		controls = new PlayerControllerMatt[totalPlayers];
		remainingPlayers = totalPlayers;
		players = new GameObject[inputManager.PlayersCount()];
		players = GameObject.FindGameObjectsWithTag("Player");
	 	controls = new PlayerControllerMatt[inputManager.PlayersCount()];
	 	colors = new Color[inputManager.PlayersCount()];
	 	devices = new InputDevice[inputManager.PlayersCount()];
	 	for (int c = 0; c < controls.Length; c++)
	 	{
	 		controls[c] = players[c].GetComponent<PlayerControllerMatt>();
	 		devices[c] = players[c].GetComponent<PlayerInputHandlerMatt>().device;
	 		colors[c] = controls[c].color;
		//	Debug.Log("Device: " + devices[c].ToString() + " Color: " + colors[c]);
	 	}
		
		
	}
	
	private void Victory()
	{
		//Debug.Log("Winner: " + winner);
		victory.GetComponent<VictoryScript>().Party (controls[winner].color);
		gameWinTime = Time.time;
		cam.won = true;
	}
	
	private void ResetPlayers()
	{
		matchCount++;
		cam.won = false;
		SetPlayers();
		cam.RecountPlayers();
	}
	
	private void RespawnPlayers()
	{
		
	}
}
