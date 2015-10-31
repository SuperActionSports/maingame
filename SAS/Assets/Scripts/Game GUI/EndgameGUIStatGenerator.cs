using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndgameGUIStatGenerator : MonoBehaviour {

	
	public GameObject statCard;
	public GameObject statistic;
	//private int playerCount;
	private Player[] players;
	//player stat cards
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
		GameObject c;
		GameObject sc;
		for (int i = 0; i < players.Length; i++)
		{
			c = Instantiate(statCard, Vector3.zero, Quaternion.identity) as GameObject;
			c.GetComponent<Image>().color = players[i].color;
			// Set ranking here
			c.GetComponentInChildren<EndgameGUIRank>().ranking = 0;
			/*
				for (int s = 0; s < player[i].statcard.stats.length; s++)
				{
					sc = Instantiate(statistic, Vector3.zero, Quaternion.identity);
					sc.GetComponent<EndgameGUIRank>().ranking = players[i[.statcard.rank;
					sc.GetComponent<Image>().color = SetBrightness(s,players[i].color);
					sc.GetComponent<GUIStatistic>().name = players[i].statcard[s].name;
					sc.GetComponent<GUIStatistic>().value = players[i].statcard[s].value;
				}
			*/
			
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
		for (int i = 0; i < 2; i++)
		{
			newColor[i] = Mathf.Clamp(c[i] - (step + 1) * 30, 0, 255);
			
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
}

