using UnityEngine;
using System.Collections;

public class BaseballStatsCard : StatsCard {

	/*--------------------BASEBALL--------------------*/
	public int HitsAttempted;
	public int HitsMade;
	//public int longestHit;
	public float BattingAverage;
	private int random;

	public void Start() {
		random = (int)(Random.Range (0.000f, 4.999f));
	}
	
	public BaseballStatsCard()
	{
		HardResetStats();
		random = (int)(Random.Range (0f, 4.999f));
	}

	public void HardResetStats() {
		ResetHitsAttempted ();
		ResetHitsMade ();
		//ResetLongestHit ();
		ResetBattingAverage ();
		ResetStats ();
	}
	
	public void AddAttemptedHit () {
		HitsAttempted++;
		CalculateBattingAverage ();
		Debug.Log ("Added attempted hit. Total attempted hits: " + HitsAttempted);
	}
	
	public void ResetHitsAttempted() {
		HitsAttempted = 0;
	}
	
	public void AddMadeHit () {
		HitsMade++;
		//Debug.Log ("Added made hit. Total hits made: " + HitsMade);
		//PrintStats ();
	}
	
	public void ResetHitsMade() {
		HitsMade = 0;
	}
	
	public void CalculateBattingAverage()	{
		//Debug.Log ("stats card before calculation: " + HitsMade + " / " + HitsAttempted);
		if (HitsAttempted != 0) {
			BattingAverage = (float)HitsMade / (float)HitsAttempted;
		}
		//Debug.Log ("Current batting average: " + BattingAverage);
	}
	
	public void ResetBattingAverage()	{
		BattingAverage = 0;
	}
	
	/*
	public float LongestHit()
	{
		//Calculate
	}
	*/
	
	/*
	public float ResetLongestHit()
	{
		//Calculate
	}
	*/
	
	/*
	public float IndividualScoring()
	{
		//Calculate
	}
	*/
	
	/*
	public float TeamScoring()
	{
		//Calculate
	}
	*/

	override public void GenerateStats()
	{
		Debug.Log("Starting to generate statistics");
		stats = new Statistic[5];
		stats[0] = new Statistic("Points", (int)TotalScore());
		stats[1] = new Statistic("KILLS", kills);
		stats[2] = new Statistic("DEATHS", deaths);
		stats[3] = new Statistic("K/D", KillDeathRatio(), Statistic.Format.ratio);
		stats[4] = new Statistic("BATTING AVERAGE", BattingAverage, Statistic.Format.ratio);
	}
	
	public void PrintStats() {
		Debug.Log ("Kills: " +kills+ " Deaths: " +deaths+ " Jumps: " +jumps+ " Attempted Attacks: " +attemptedAttacks+ 
		           " Kill Streak: " +killStreak+ " Longest Kill Streak: " +longestKillStreak+ " Longest Time Alive: " +longestTimeAlive+ 
		           " Shortest Time Alive: " +shortestTimeAlive+ " Kill/Death Ratio: " +KillDeathRatio()+ " Attempted Hits: " +HitsAttempted+ 
		           " Hits Made: " +HitsMade+ " Batting Average: " +BattingAverage);
	}
	
	/*--------------------END BASEBALL--------------------*/

	public override int TotalScore() {
		float Pw = 0.5f;
		float KDRw = 0.32f;
		float KSw = 0.1f;
		float ACCw = 0.05f;
		float RANw = 0.03f;


		float RAN;
		switch (random) {
		case 0:
			RAN = jumps;
			break;
		case 1:
			RAN = attemptedAttacks;
			break;
		case 2: 
			RAN = attackSuccessRate;
			break;
		case 3:
			RAN = longestTimeAlive;
			break;
		case 4:
			RAN = shortestTimeAlive;
			break;
		default:
			RAN = 0;
			break;
		}

		float tsP = HitsMade * Pw;
		float tsKDR = kDR * KDRw;
		float tsKS = longestKillStreak * KSw;
		float tsACC = BattingAverage * ACCw;
		float tsRAN = RAN * RANw;

		float ts = tsP + tsKDR + tsKS + tsACC + tsRAN;

		return (int)(ts * 10);
	}









}
