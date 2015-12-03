﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndgameGUIStatGenerator : MonoBehaviour {

	private Player[] debugPlayers;
	
	public BaseballStatsCard statCard;
	public GameObject statistic;
	//private int playerCount;
	private Player[] players;
	public GameObject masthead;
	//player stat cards
	
	void Start()
	{
		if (testingMode)
		{
			debugPlayers = new Player[4];
			
			debugPlayers[0] = new Player();
			debugPlayers[0].statCard = new BaseballStatsCard();
			debugPlayers[0].color = Color.cyan;
			debugPlayers[0].statCard.rank = 0;
			debugPlayers[0].statCard.kills = 35;
			debugPlayers[0].statCard.deaths = 4;
			debugPlayers[0].statCard.longestKillStreak = 3;
			debugPlayers[0].statCard.longestTimeAlive = 74.386543682f;
			//debugPlayers[0].statCard.BattingAverage = .367;
			//debugPlayers[0].statCard.HitsMade = 13;
			debugPlayers[0].statCard.TotalScore();
			Debug.Log ("Score: " + debugPlayers[0].statCard.TotalScore()*1000);

			debugPlayers[1] = new Player();
			debugPlayers[1].statCard = new BaseballStatsCard();
			debugPlayers[1].color = Color.magenta;
			debugPlayers[1].statCard.rank = 1;
			debugPlayers[1].statCard.kills = 10;
			debugPlayers[1].statCard.deaths = 8;
			debugPlayers[1].statCard.longestKillStreak = 1;
			debugPlayers[1].statCard.longestTimeAlive = 60.265436985219f;
			debugPlayers[1].statCard.TotalScore();
			Debug.Log ("Score: " + debugPlayers[1].statCard.TotalScore()*1000);

			debugPlayers[2] = new Player();
			debugPlayers[2].statCard = new BaseballStatsCard();
			debugPlayers[2].color = Color.yellow;
			debugPlayers[2].statCard.rank = 2;
			debugPlayers[2].statCard.kills = 8;
			debugPlayers[2].statCard.deaths = 12;
			debugPlayers[2].statCard.longestKillStreak = 1;
			debugPlayers[2].statCard.longestTimeAlive = 48.235365879f;
			debugPlayers[2].statCard.TotalScore();
			Debug.Log ("Score: " + debugPlayers[2].statCard.TotalScore()*1000);

			debugPlayers[3] = new Player();
			debugPlayers[3].statCard = new StatsCard();
			debugPlayers[3].color = Color.red;
			debugPlayers[3].statCard.rank = 3;
			debugPlayers[3].statCard.kills = 3;
			debugPlayers[3].statCard.deaths = 35;
			debugPlayers[3].statCard.longestKillStreak = 1;
			debugPlayers[3].statCard.longestTimeAlive = 31.574104829f;
			debugPlayers[3].statCard.TotalScore();
			Debug.Log ("Score: " + debugPlayers[3].statCard.TotalScore());

			SetPlayers = debugPlayers;

		}
		
	}
	
	public Player[] SetPlayers
	{
			set
			{
			players = value;
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
		masthead.SetActive(true);
		GameObject c;
		GameObject sc;
		for (int i = 0; i < players.Length; i++)
		{
			Debug.Log("Creating stat card for player " + i + " of " + players.Length);
			c = Instantiate(statCard, Vector3.zero, Quaternion.identity) as GameObject;
			c.GetComponent<Image>().color = players[i].color;
			//c.GetComponentInChildren<EndgameGUIRank>().ranking = 0;
			players[i].statCard.GenerateStats();
				for (int s = 0; s < players[i].statCard.stats.Length; s++)
				{
					Debug.Log("Creating statistic");
					sc = Instantiate(statistic, Vector3.zero, Quaternion.identity) as GameObject;
					c.GetComponentInChildren<EndgameGUIRank>().ranking = players[i].statCard.rank;
					sc.GetComponent<Image>().color = SetBrightness(s,players[i].color);
					sc.GetComponent<EndGameGUIStatistic>().Name = players[i].statCard.stats[s].Name;
					sc.GetComponent<EndGameGUIStatistic>().Value = players[i].statCard.stats[s].Value;
					sc.GetComponent<EndGameGUIStatistic>().GenerateStatistic();
					sc.transform.parent = c.transform;
				}
			Debug.Log("Stat card scale: " + c.transform.localScale);
			c.transform.parent = this.transform;
			c.transform.localScale = new Vector3(1,1,1);
		}	
	}
	
	/// <summary>
	/// Creates color with corrected brightness
	/// </summary>
	/// <param name="step">The steps of brightness to move. A very dark color will cycle back to bright.</param>
	/// <param name="c">Color to correct.</param>
	/// <returns>
	/// Corrected <see cref="Color"/> structure.
	/// </returns>
	private Color SetBrightness(int step, Color c)
	{
		Color newColor = new Color();
		for (int i = 0; i < 3; i++)
		{
			newColor[i] = Mathf.Clamp((c[i]*255) - (step + 1) * 30, 0, 255)/255;		
		}
		newColor[3] = 1;
		return newColor;
	}
	
	/// <summary>
	/// Subtract value from original. 
	/// If result is greater than 0, return result
	/// If result is less than 0, subtract the absolute value of the result from max
	/// </summary>
	/// <param name="original">Number to subtract from</param>
	/// <param name="value">Subtracted from oroginal</param>
	/// <param name="max">Maximum number original can be once the subtraction is less than 0</param>
	/// <returns>
	/// Corrected <see cref="Color"/> structure.
	/// </returns>
	private int CycleSubtract(int original, int value, int max)
	{
		return (original - value) < 0 ? max + (original - value) : (original - value);
	}

	public void update() {
		Debug.Log ("Score: " + debugPlayers[0].statCard.TotalScore());
	}
}

