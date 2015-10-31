﻿using UnityEngine;
using System.Collections;

public class BaseballWizard : MonoBehaviour, IWizard {
	
	public BaseballLauncher pitcher;
	private float lastPitch;
	private float pitchGap;
	private float madnessTime;
	private float madnessGap;
	private float madnessMod;
	private bool madness = false;
	public GameObject playerPrefab; 		//Provided by game
	public Player[] players;
	private Vector3[] respawnPointPositions; // Game
	public GameObject[] respawnPoints;		// Game
	public float respawnRate;
	private int totalPlayers;				 // Layla
	private int remainingPlayers;			// Game 
	private int winner;						// Shared
	public GameObject winnerPlayer;			// Game, private
	//public GameObject victory;				// Game, private
	//private float victoryDuration;			// Game, private
	private float gameWinTime;				// Layla, customized games
	private int matchCount;					// Layla, customized games
	public GameObject layla;
	private GameControlLiaison liaison;
	
	// Use this for initialization
	void Start () {
		if (layla == null) { layla = GameObject.Find("Layla");
		}
		liaison = layla.GetComponent<GameControlLiaison>();
		Debug.Log("Liaison's active players: " + liaison.numberOfActivePlayers);
		players = new Player[liaison.numberOfActivePlayers];
		Debug.Log("So, naturally, players' length is " + players.Length);
		for (int p = 0; p < players.Length; p++)
		{
			players[p] = liaison.players[p];
		}
		respawnPointPositions = new Vector3[respawnPoints.Length];
		for (int i = 0; i < respawnPoints.Length; i++)
		{
			respawnPointPositions[i] = respawnPoints[i].transform.position;
		}
		//players = liaison.players;
		Debug.Log("Wizard is setting " + players.Length +" players.");
		respawnRate = 2.5f;
		SetPlayers();
		//Magic Number
		pitchGap = 2;
		gameWinTime = -1;
		lastPitch = Mathf.Infinity;
		madnessMod = 5;
		madnessTime = 57;
		madnessGap = 5;
	}
	
	
	private void SetPlayers()
	{
		// Set players should be SPAWN players. SpawnPlayers should reset the position of the players to one of the given respawn points, set them to active and alive, etc
		// This should handle the device concern too. 	
		switch(players.Length)
		{
			
		case (1):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			break;
		case(2):
			Debug.Log("Spawning two");
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[1],players[1]);
			break;
			
		case (3):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[1],players[1]);
			if (ShouldBeSpawned(players[2])) Spawn(respawnPointPositions[2],players[2]);
			break;
			
		case(4):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[1],players[1]);
			if (ShouldBeSpawned(players[2])) Spawn(respawnPointPositions[2],players[2]);
			if (ShouldBeSpawned(players[3])) Spawn(respawnPointPositions[3],players[3]);
			break;
			
		default:
			Debug.Log ("Invalid number of players [2,4]: " + players.Length);
			break;
		}
	}
	
	public void DisableMovement()
	{
		Debug.Log("Disable From Wizard");
		for (int i = 0; i < players.Length; i++)
		{
			players[i].control.MovementAllowed(false);
		}	
	}
	
	public void EnableMovement()
	{
		for (int i = 0; i < players.Length; i++)
		{
			players[i].control.MovementAllowed(true);
		}	
		lastPitch = Time.time;
	}
	
	private bool ShouldBeSpawned(Player p)
	{
		if (p.control == null)
		{
			return true;	
		}
		else { return !p.control.Alive(); } 
	}
	
	private BaseballPlayerController Spawn(Vector3 position, Player player)
	{
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		player.gameObject = p;
		BaseballPlayerController pController = p.GetComponent<BaseballPlayerController>();
		player.control = pController;
		pController.color = player.color;
		pController.wizard = this;
		pController.respawnPoint = position;
		pController.respawnTime = respawnRate;
		p.GetComponent<BaseballPlayerController>().device = player.device;
		//players[playerNumber] = p.GetComponent<PlayerInputHandlerMatt>();
		remainingPlayers++;
		return pController;
	}
	// Update is called once per frame
	void Update () {
		if (Time.time - lastPitch >= pitchGap && (Time.time < madnessTime || Time.time > madnessGap+madnessTime)) {
			lastPitch += pitchGap;
			pitcher.Pitch ();
		}
		if (madnessTime <= Time.time && !madness)
		{
			pitchGap /= madnessMod;
			madness = true;
		}
	}	
}