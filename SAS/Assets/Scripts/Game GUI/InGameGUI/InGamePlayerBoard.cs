using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGamePlayerBoard : MonoBehaviour {

	private Player[] debugPlayers;
	
	public GameObject playerCardPrefab;
	//private int playerCount;
	private Player[] players;
	private InGamePlayerCard[] cards;
	// Use this for initialization
	void Start () {
		
		debugPlayers = new Player[4];
		
		debugPlayers[0] = new Player();
		debugPlayers[0].statCard = new StatsCard();
		debugPlayers[0].color = Color.cyan;
		debugPlayers[0].statCard.rank = 0;
		debugPlayers[0].statCard.kills = 15;
		debugPlayers[0].statCard.deaths = 4;
		debugPlayers[0].statCard.longestKillStreak = 3;
		debugPlayers[0].statCard.longestTimeAlive = 74.386543682f;
		debugPlayers[0].statCard.TotalScore();
		
		debugPlayers[1] = new Player();
		debugPlayers[1].statCard = new StatsCard();
		debugPlayers[1].color = Color.magenta;
		debugPlayers[1].statCard.rank = 1;
		debugPlayers[1].statCard.kills = 10;
		debugPlayers[1].statCard.deaths = 8;
		debugPlayers[1].statCard.longestKillStreak = 1;
		debugPlayers[1].statCard.longestTimeAlive = 60.265436985219f;
		debugPlayers[1].statCard.TotalScore();
		
		debugPlayers[2] = new Player();
		debugPlayers[2].statCard = new StatsCard();
		debugPlayers[2].color = Color.yellow;
		debugPlayers[2].statCard.rank = 2;
		debugPlayers[2].statCard.kills = 8;
		debugPlayers[2].statCard.deaths = 12;
		debugPlayers[2].statCard.longestKillStreak = 1;
		debugPlayers[2].statCard.longestTimeAlive = 48.235365879f;
		debugPlayers[2].statCard.TotalScore();
		
		debugPlayers[3] = new Player();
		debugPlayers[3].statCard = new StatsCard();
		debugPlayers[3].color = Color.red;
		debugPlayers[3].statCard.rank = 3;
		debugPlayers[3].statCard.kills = 3;
		debugPlayers[3].statCard.deaths = 35;
		debugPlayers[3].statCard.longestKillStreak = 1;
		debugPlayers[3].statCard.longestTimeAlive = 31.574104829f;
		debugPlayers[3].statCard.TotalScore();
		
		//SetPlayers = debugPlayers;
	}
	
	public Player[] SetPlayers
	{
		set
		{
			players = value;
			Debug.Log("Setting players");
			CreateStatCard();
		}
	}
	
	/// <summary>
	/// Creates the end game statistics board. Requires a stat card and statistic prefab
	/// </summary>
	/// <param name="c">Statcard prefab</param>
	/// <param name="sc">Statistic prefab</param>
	private void CreateStatCard()
	{
		Debug.Log("Generating players");
		GameObject c;
		cards = new InGamePlayerCard[players.Length];
		for (int i = 0; i < players.Length; i++)
		{
			c = Instantiate(playerCardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			c.GetComponent<Image>().color = players[i].color;
			InGamePlayerCard pc = c.GetComponent<InGamePlayerCard>();
			Debug.Log("Player: " + players[i]);
			Debug.Log("Ingame: " + pc + "- value: " + pc.Value + "statCard: " + players[i].statCard + "0x");
			//pc.Value = players[i].statCard.Kills.ToString();
			pc.Name = "P"+(i+1);
			cards[i] = pc;
			c.transform.parent = this.transform;
			c.transform.localScale = new Vector3(1,1,1);
		}	
	}
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < cards.Length; i++)
		{
			//cards[i].Value = players[i].statCard.Kills.ToString();
			cards[i].Value = players[i].statCard.TotalScore().ToString();
			cards[i].GenerateStatistic();
		}
		
	}
}
