using UnityEngine;
using System.Collections;

public class BaseballStatsCard : StatsCard {

	/*--------------------BASEBALL--------------------*/
	public int HitsAttempted;
	public int HitsMade;
	//public int longestHit;
	public float BattingAverage;
	
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
		AddAttemptedHit ();
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
		stats = new Statistic[4];
		stats[0] = new Statistic("KILLS", kills);
		stats[1] = new Statistic("DEATHS", deaths);
		stats[2] = new Statistic("K/D", KillDeathRatio(), false);
		stats[3] = new Statistic("BATTING AVERAGE", BattingAverage, false);
	}
	
	public void PrintStats() {
		Debug.Log ("Kills: " +kills+ " Deaths: " +deaths+ " Jumps: " +jumps+ " Attempted Attacks: " +attemptedAttacks+ 
		           " Kill Streak: " +killStreak+ " Longest Kill Streak: " +longestKillStreak+ " Longest Time Alive: " +longestTimeAlive+ 
		           " Shortest Time Alive: " +shortestTimeAlive+ " Kill/Death Ratio: " +KillDeathRatio()+ " Attempted Hits: " +HitsAttempted+ 
		           " Hits Made: " +HitsMade+ " Batting Average: " +BattingAverage);
	}
	
	/*--------------------END BASEBALL--------------------*/
}
