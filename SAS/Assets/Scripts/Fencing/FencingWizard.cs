using UnityEngine;
using System.Collections;

public class FencingWizard : MonoBehaviour {

	public GameObject playerPrefab; 		//Provided by game
	public Player[] players;
	private Vector3[] respawnPointPositions; // Game
	public GameObject[] respawnPoints;		// Game
	private int totalPlayers;				 // Layla
	private int remainingPlayers;			// Game 
	private int winner;						// Shared
	public GameObject winnerPlayer;			// Game, private
	public GameObject victory;				// Game, private
	private float victoryDuration;			// Game, private
	private float gameWinTime;				// Layla, customized games
	private int matchCount;					// Layla, customized games
	private FencingCameraController cam;	// Game
	//private FencingPlayerManager inputManager;	// Layla
	//private InputDevice[] devices;				// Layla
	private FencingCameraController camScript;	// Game
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
	SetPlayers();
	camScript = Camera.main.GetComponent<FencingCameraController>();
	}
	
	
	private void SetPlayers()
	{
		// Set players should be SPAWN players. SpawnPlayers should reset the position of the players to one of the given respawn points, set them to active and alive, etc
		// This should handle the device concern too. 	
		switch(players.Length)
		{
		
		case (1):
		{
		Spawn(respawnPointPositions[0],players[0]);
		break;
		}
		case(2):
			break;
			
		case (3):
			break;
			
		case(4):
			break;
			
		default:
			Debug.Log ("Invalid number of players [2,4]: " + players.Length);
			break;
		}
	}
	
	
	private PlayerControllerMatt Spawn(Vector3 position, Player player)
	{
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		
		PlayerControllerMatt pController = p.GetComponent<PlayerControllerMatt>();
		player.control = pController;
		// Replace this noise with the player prefs file information
		p.GetComponent<PlayerControllerMatt>().color = player.color;
		p.GetComponent<PlayerControllerMatt>().wizard = this;
//		p.GetComponent<PlayerControllerMatt>().wizardNumber = playerNumber;
		
		// Replace this with the device information from userprefs
		p.GetComponent<PlayerInputHandlerMatt>().device = player.device;
		//players[playerNumber] = p.GetComponent<PlayerInputHandlerMatt>();
		
		
		//p.GetComponent<PlayerInputHandlerMatt>().device = null; 
		return pController;
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	public void UpdatePlayerCount()
	{
		winner = 0;
		for (int i = 0; i<totalPlayers;i++)
		{
			//Debug.Log("Player " + i + " (" + controls[i].color.ToString() + ") is alive? " + controls[i].alive);
			if (players[i].control.Alive()) 
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
	
	private void Victory()
	{
		//Debug.Log("Winner: " + winner);
		victory.GetComponent<VictoryScript>().Party (players[winner].color);
		gameWinTime = Time.time;
		camScript.won = true;
		winnerPlayer = players[winner].gameObject;
	}
	
	
}
