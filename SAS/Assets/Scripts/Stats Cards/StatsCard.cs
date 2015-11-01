using UnityEngine;
using System.Collections;

public class StatsCard : MonoBehaviour {

	public float individualScore;
	public float teamAScore;
	public float teamBScore;

	void Start () {
		ResetStats ();
	}

	public void ResetIndividualScore()	{
		individualScore = 0;
	}

	public void ResetTeamScores()	{
		ResetTeamAScore ();
		ResetTeamBScore ();
	}

	public void ResetTeamAScore()	{
		teamAScore = 0;
	}

	public void ResetTeamBScore()	{
		teamBScore = 0;
	}

	/*--------------------ALL GAMES--------------------*/
	public int kills;
	public int deaths;
	public int jumps;
	public int attemptedAttacks;
	public int killStreak;
	public int longestKillStreak;
	//public float distanceRan;
	public float longestTimeAlive;
	public float shortestTimeAlive;
	public float birth;
	public float death;
	public float lifetime;
	public int rank;
	public Statistic[] stats;
	
	/// <summary>
	/// Returns the number of kills the player did
	/// </summary>
	public int Kills{get { return kills; } }
	/// <summary>
	/// Returns the number of times the player died
	/// </summary>
	public int Deaths{get { return deaths; } }
	/// <summary>
	/// Returns the number of times the player jumped
	/// </summary>
	public int Jumps{get { return jumps; } }
	/// <summary>
	/// Returns the number of times the player tried to attack
	/// </summary>
	public int AttemptedAttacks{get { return kills; } }
	/// <summary>
	/// Returns the most number of kills the player got without dying
	/// </summary>
	public int LongestKillStreak{get { return longestKillStreak; } }
	/// <summary>
	/// Returns a ratio of kills to deaths the player accomplished
	/// </summary>
	public float KillDeathRatio() { 
		return deaths > 0 ? (float)kills/(float)deaths : (float)kills;
		} 
	/// <summary>
	/// Returns the shortest amount of time a player was alive
	/// </summary>
	public float ShortestTimeAlive{get { return shortestTimeAlive; } }
	/// <summary>
	/// Returns the longest time the player was alive
	/// </summary>
	public float LongestTimeAlive{get { return longestTimeAlive; } }
	
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetStats()	{
		kills = 0;
		deaths = 0;
		jumps = 0;
		attemptedAttacks = 0;
		killStreak = 0;
		longestKillStreak = 0;
		//distanceRan = 0;
		longestTimeAlive = 0;
		shortestTimeAlive = 0;
	}

	public void ResetIndividualStats()	{
		ResetKills ();
		ResetDeaths ();
		ResetJumps ();
		ResetAttemptedAttack ();
		ResetlongestKillStreak ();
		//ResetDistanceRan ();
		ResetLongestTimeAlive ();
		ResetShortestTimeAlive ();
		//ResetKDRatio ();
	}

	public void AddKill()	{
		kills++;
		killStreak++;
		AddAttemptedAttack ();
		//CalculateKDRatio ();
	}

	public void ResetKills() {
		kills = 0;
		ResetkillStreak ();
	}

	public void AddDeath()	{
		deaths++;
		if (killStreak > longestKillStreak) {
			longestKillStreak = killStreak;
		}
		ResetkillStreak ();
		//CalculateKDRatio ();
	}

	public void ResetDeaths()	{
		deaths = 0;
	}

	public void AddJump()	{
		jumps++;
	}

	public void ResetJumps()	{
		jumps = 0;
	}

	public void AddAttemptedAttack()	{
		attemptedAttacks++;
	}

	public void ResetAttemptedAttack()	{
		attemptedAttacks = 0;
	}

	public void ResetkillStreak()	{
		killStreak = 0;
	}

	public void ResetlongestKillStreak()	{
		longestKillStreak = 0;
	}

	public void StartLifeTime()	{
		birth = Time.time;
	}

	public void EndLifeTime()	{
		death = Time.time;
		lifetime = death - birth;
		if (lifetime > longestTimeAlive) {
			longestTimeAlive = lifetime;
		}
		if (lifetime < shortestTimeAlive)	{
			shortestTimeAlive = lifetime;
		}
	}
	
	/// <summary> 
	/// Sets stats to relevant statistics and values. By default, this will set stats to Kills, Deaths, KDR, and Longest life time. 
	///</summary>
	virtual public void GenerateStats()
	{
		Debug.Log("Starting to generate statistics");
		stats = new Statistic[4];
		stats[0] = new Statistic("KILLS", kills);
		stats[1] = new Statistic("DEATHS", deaths);
		stats[2] = new Statistic("K/D", KillDeathRatio(), false);
		stats[3] = new Statistic("LONGEST LIFE", LongestTimeAlive, true);
	}

	public void ResetLongestTimeAlive() {
		longestTimeAlive = 0;
	}

	public void ResetShortestTimeAlive() {
		shortestTimeAlive = -1;
	}

	/*
	public void CalculateDistanceRan() {
		//Calculate
	}
	*/

	/*
	public void ResetDistanceRan() {
		//Calculate
	}
	*/

	/*--------------------END ALL GAMES--------------------*/

}

public class Statistic 
{
	private string name;
	private string value;
	public Statistic(string name, float value, bool isTime)
	{
		this.name = name;
		Debug.Log("Real KDR: " + value);
		this.value = RoundFloat(value,2).ToString();
		if (isTime)
		{
			Debug.Log ((value / 600) + (value / 60) + ":" + value % 60);
			this.value =  string.Format("{0}:{01:00}", (int)value / 60, (int)value % 60);
		}
	}
	public Statistic(string name, int value)
	{
		this.name = name;
		this.value = value.ToString();
	}
	/// <summary>Returns the name of the statistic</summary>
	public string Name
	{
		get { return  name; }
	}
	/// <summary>Returns the value of the statistic</summary>
	public string Value
	{
		get { return  this.value; }
	}	
	/// <summary>
	/// Returns a float rounded to "places" numbers after the decimal
	/// </summary>
	/// <returns>The rounded float.</returns>
	/// <param name="value">Value to round</param>
	/// <param name="places">Places to round by</param>
	private float RoundFloat(float value, int places)
	{
		return Mathf.Round(value*(Mathf.Pow(10,places))) / Mathf.Pow(10,places);
	}
	
	public string kDRatio
	{
		get { return "Wrong KDR, dingus."; }
	}
}
