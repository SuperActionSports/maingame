using UnityEngine;
using System.Collections;

public class FencingWizard : MonoBehaviour,IWizard {

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
	private float roundWinTime;				// Layla, customized games
	public float gameDuration;
	private float gameStartTime;
	private int matchCount;					// Layla, customized games
	private FencingCameraController cam;	// Game
	private FencingCameraController camScript;	// Game
	public GameObject layla;
	private GameControlLiaison liaison;
	private bool finished;
	public GameObject endGame;
	public GameObject inGame;
	public PerlinAudienceManager audienceManager;
	
	// Use this for initialization
	void Start () {
	if (layla == null) { layla = GameObject.Find("Layla");
	}
	finished = false;
	liaison = layla.GetComponent<GameControlLiaison>();
	players = new Player[liaison.numberOfActivePlayers];
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
	ResetExistingPlayers();
	camScript = Camera.main.GetComponent<FencingCameraController>();
	SetPlayers();
	roundWinTime = -1;
	victoryDuration = 3;
	gameDuration = 60;
	gameStartTime = Time.time;
	UpdateStatCards();
	inGame.GetComponent<InGamePlayerBoard>().SetPlayers = players;
		audienceManager.SetAudience(players);
	}
	
	void ResetExistingPlayers()
	{
		foreach (Player p in players)
		{
			p.control = null;
		}
	}
	
	public void SmallEvent()
	{
		audienceManager.SendSmallEvent();
		if (Random.Range(0,100) < 50) ChangeAudienceColor();
	}
	
	public void BigEvent()
	{
		audienceManager.SendBigEvent();
		if (Random.Range(0,100) < 50) ChangeAudienceColor();
	}
	
	public void ChangeAudienceColor()
	{
		int[] scores = new int[players.Length];
		for (int s = 0; s < players.Length; s++)
		{
			scores[s] = players[s].statCard.TotalScore();
		}
		audienceManager.ChangeCrowdColor(scores);
	}
	
	private void SetPlayers()
	{
		// Set players should be SPAWN players. SpawnPlayers should reset the position of the players to one of the given respawn points, set them to active and alive, etc
		// This should handle the device concern too. 	
		remainingPlayers = players.Length;
		switch(players.Length)
		{
		
		case (1):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			break;
		case(2):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[4],players[1]);
			break;
			
		case (3):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[2],players[1]);
			if (ShouldBeSpawned(players[2])) Spawn(respawnPointPositions[4],players[2]);
			break;
			
		case(4):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[1],players[1]);
			if (ShouldBeSpawned(players[2])) Spawn(respawnPointPositions[3],players[2]);
			if (ShouldBeSpawned(players[3])) Spawn(respawnPointPositions[4],players[3]);
			break;
			
		default:
			Debug.Log ("Invalid number of players [2,4]: " + players.Length);
			break;
		}
		
		camScript.RecountPlayers();
	}
	
	public void DisableMovement()
	{
		for (int i = 0; i < players.Length; i++)
		{
			players[i].control.MovementAllowed(false);
		}	
	}
	
	public void EnableMovement()
	{
		audienceManager.KillInvisibleChildren();
		for (int i = 0; i < players.Length; i++)
		{
			players[i].control.MovementAllowed(true);
		}	
	}
	
	private bool ShouldBeSpawned(Player p)
	{
	/*	if (p.control == null)
		{
			return true;	
		}
		else { return !p.control.Alive(); } */
		return p.control == null;
	}
	
	private FencingPlayerController Spawn(Vector3 position, Player player)
	{
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		player.gameObject = p;
		FencingPlayerController pController = p.GetComponent<FencingPlayerController>();
		player.control = pController;
		pController.color = player.color;
		pController.wizard = this;
		pController.respawnPoint = position;
		pController.InitializeStatCard();
		p.GetComponent<FencingInputHandler>().device = player.device;
		
		return pController;
	}
	
	private void ResetPlayers()
	{
		ChangeAudienceColor();
		BigEvent();
		remainingPlayers = players.Length;
		foreach (Player p in players)
		{
			if (!p.control.Alive()) ((FencingPlayerController)p.control).Respawn();
		}
	}
	// Update is called once per frame
	void Update () {
		UpdateStatCards();
		if (gameDuration > (Time.time - gameStartTime))
		{
			if (roundWinTime > 0 && roundWinTime + victoryDuration <= Time.time)
			{
				//SetPlayers();
				ResetPlayers();
				camScript.won = false;
				camScript.RecountPlayers();
				roundWinTime = -1;
			}
		}
		else if (!finished)
		{
			DisableMovement();
			endGame.GetComponentInChildren<EndgameGUIStatGenerator>().SetPlayers = players;
			finished = true;
			inGame.GetComponent<InGamePlayerBoard>().KillInGameBoard();
		}
	}
	
	private void UpdateStatCards()
	{
		for (int p = 0; p < players.Length; p++)
		{
			players[p].statCard = ((FencingPlayerController)players[p].control).Stats;
		}
	}
	
	public void UpdatePlayerCount()
	{
		winner = 0;
		for (int i = 0; i<players.Length;i++)
		{
			//Debug.Log("Player " + i + " (" + controls[i].color.ToString() + ") is alive? " + controls[i].alive);
			if (players[i].control.Alive()) 
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
	
	private void Victory()
	{
		victory.GetComponent<VictoryScript>().Party (players[winner].color);
		roundWinTime = Time.time;
		camScript.won = true;
		winnerPlayer = players[winner].gameObject;
		if (winnerPlayer != null ) {camScript.FollowWinner(winnerPlayer);}
		else { Debug.Log("Winner Player has no game object"); }
	}
	
	
}
