using System;
using UnityEngine;
using System.Collections.Generic;
using InControl;

public class FencingGameManager : MonoBehaviour {
	
	public GameObject playerPrefab;
	//private GameObject[] players;
	private PlayerControllerMatt[] controls;
	private Color[] colors;
	private Vector3[] respawnPointPositions;
	public GameObject[] respawnPoints;
	private int totalPlayers;
	private int remainingPlayers;
	private int winner;
	public GameObject winnerPlayer;
	public GameObject victory;
	private float victoryDuration;
	private float gameWinTime;
	private int matchCount;
	private FencingCameraController cam;
	private FencingPlayerManager inputManager;
	private InputDevice[] devices;
	private FencingCameraController camScript;
	
	const int maxPlayers = 4;
	
	List<Vector3> spawnPoints;
	
	public List<PlayerInputHandlerMatt> players = new List<PlayerInputHandlerMatt>( maxPlayers );
	public int PlayersCount()
	{
		return players.Count;
	}
	
	Color[] playerColors2;
	Color[] playerColors3;
	Color[] playerColors4;
	Color[] playerColors;
	
	public GameObject[] Respawns;
	
	private FencingGameManager wizard;
	public bool frozen;
	// Use this for initialization
	void Start () {
	/*
	Get a list of valid players from UserPrefs along with their device
	Store these players into players[] and their colors into colors[]
	*/
		frozen = false;
		playerColors[0] = Color.cyan;
		playerColors[1] = Color.magenta;
		playerColors[2] = Color.yellow;
		playerColors[3] = Color.black;
		for (int i = 0; i < maxPlayers; i++)
			InputManager.OnDeviceDetached += OnDeviceDetached;
		spawnPoints = new List<Vector3>();
		try{
			Respawns.GetLength(0);
		}
		catch (NullReferenceException e)
		{
			Debug.Log("You didn't set the Respawns prefab, dingus." + e);
		}
		foreach (GameObject g in respawnPoints)
		{
			spawnPoints.Add(g.transform.position);
		}
		//cam = Camera.main

		//players = GameObject.FindGameObjectsWithTag("Player");
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
		if (players.Count > 0)
		{
			totalPlayers = players.Count;
			controls = new PlayerControllerMatt[totalPlayers];
			devices = new InputDevice[totalPlayers];
			remainingPlayers = totalPlayers;
		}
		matchCount = 0;
		victoryDuration = 3;
		gameWinTime = -10000;
		
		camScript = Camera.main.GetComponent<FencingCameraController>();
	}

	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		
		if (JoinButtonWasPressedOnDevice( inputDevice ))
		{
			if (ThereIsNoPlayerUsingDevice( inputDevice ))
			{
				CreatePlayer( inputDevice );
				camScript.RecountPlayers();
				Debug.Log(this + " player count: " + players.Count);
				UpdatePlayerTotal(players.Count);
				
			}
		}
		if (Time.time < gameWinTime + victoryDuration)
		{
			camScript.FollowWinner(winnerPlayer);
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
		switch(players.Count)
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
			Debug.Log ("Invalid number of players [2,4]: " + players.Count);
			break;
		}
	}
	
	
	private PlayerControllerMatt Spawn(Vector3 position, int playerNumber)
	{
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		
		PlayerControllerMatt pController = p.GetComponent<PlayerControllerMatt>();
		// Replace this noise with the player prefs file information
		p.GetComponent<PlayerControllerMatt>().color = colors[playerNumber];
		p.GetComponent<PlayerControllerMatt>().wizard = this;
		p.GetComponent<PlayerControllerMatt>().wizardNumber = playerNumber;
		
		// Replace this with the device information from userprefs
		p.GetComponent<PlayerInputHandlerMatt>().device = players[playerNumber].device;
		players[playerNumber] = p.GetComponent<PlayerInputHandlerMatt>();
		
		
		//p.GetComponent<PlayerInputHandlerMatt>().device = null; 
		return pController;
	}
	
	public void UpdatePlayerCount()
	{
		winner = 0;
		for (int i = 0; i<totalPlayers;i++)
		{
			Debug.Log("Player " + i + " (" + controls[i].color.ToString() + ") is alive? " + controls[i].alive);
			if (controls[i].alive) 
			{
				winner = i;
				Debug.Log("Winner: " + winner);
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
		Debug.Log("Updating player totals to " + newCount);
		totalPlayers = newCount;		
		winner = 0;
		controls = new PlayerControllerMatt[totalPlayers];
		remainingPlayers = totalPlayers;
		controls = new PlayerControllerMatt[totalPlayers];
		colors = new Color[totalPlayers];
		devices = new InputDevice[totalPlayers];

	 	for (int c = 0; c < controls.Length; c++)
	 	{
//	 		Debug.Log("Input manager player list entry: " + playerInputs[c]);
	 		controls[c] = players[c].gameObject.GetComponent<PlayerControllerMatt>();
//	 		Debug.Log(players[c].GetComponent<PlayerInputHandlerMatt>());
//			Debug.Log(players[c].GetComponent<PlayerInputHandlerMatt>().device);
			devices[c] = players[c].gameObject.GetComponent<PlayerInputHandlerMatt>().device;
	 		colors[c] = controls[c].color;
		//	Debug.Log("Device: " + devices[c].ToString() + " Color: " + colors[c]);
	 	}
		
		
	}
	
	private void Victory()
	{
		//Debug.Log("Winner: " + winner);
		victory.GetComponent<VictoryScript>().Party (controls[winner].color);
		gameWinTime = Time.time;
		camScript.won = true;
		winnerPlayer = players[winner].gameObject;
	}
	
	private void ResetPlayers()
	{
		matchCount++;
		camScript.won = false;
		SetPlayers();
		camScript.RecountPlayers();
		UpdatePlayerTotal(totalPlayers);
	}
	
	private void RespawnPlayers()
	{
		
	}
	
	void Awake()
	{
		playerColors = new Color[maxPlayers];
	}	
	
	bool JoinButtonWasPressedOnDevice( InputDevice inputDevice )
	{
		//return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
		return inputDevice.Command.WasPressed;
	}
	
	
	PlayerInputHandlerMatt FindPlayerUsingDevice( InputDevice inputDevice )
	{
		var playerCount = players.Count;
		for (int i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.device == inputDevice)
			{
				return player;
			}
		}
		
		return null;
		//wizard needs to talk to input manager to have an identical player list
	}
	
	
	bool ThereIsNoPlayerUsingDevice( InputDevice inputDevice )
	{
		return FindPlayerUsingDevice( inputDevice ) == null;
	}
	
	
	void OnDeviceDetached( InputDevice inputDevice )
	{
		var player = FindPlayerUsingDevice( inputDevice );
		if (player != null)
		{
			//RemovePlayer( player );
		}
	}
	
	
	PlayerInputHandlerMatt CreatePlayer( InputDevice inputDevice )
	{
		if (players.Count < maxPlayers)
		{
			// Pop a position off the list. We'll add it back if the player is removed.
			var playerPosition = spawnPoints[0];
			spawnPoints.RemoveAt( 0 );
			var gameObject = (GameObject) Instantiate( playerPrefab, playerPosition, Quaternion.identity );
			var player = gameObject.GetComponent<PlayerInputHandlerMatt>();
			player.device = inputDevice;
			player.GetComponent<PlayerControllerMatt>().color = playerColors[players.Count];
			player.transform.parent = this.transform;
			players.Add( player );
			
			return player;
		}
		
		return null;
	}
	
	
	void RemovePlayer( PlayerInputHandlerMatt player )
	{
		spawnPoints.Insert( 0, player.transform.position );
		//players.Remove( player );
		player.device = null;
		Destroy( player.gameObject );
	}
}
