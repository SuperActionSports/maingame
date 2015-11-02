using UnityEngine;
using System.Collections;

public class GolfStatsCard : StatsCard {
	
	/*--------------------GOLF--------------------*/
	public int PuttsAttempted;
	public int PuttsMade;
	//public float longestPutt;
	//public float ShortestPutt;
	public float Accuracy;
	
	public void HardResetStats() {
		ResetAttemptedPutts ();
		ResetMadePutts ();
		//ResetLongestPutt();
		//ResetShortestPutt();
		ResetAccuracy ();
		ResetStats ();
	}
	
	public void AddAttemptedPutt()	{
		PuttsAttempted++;
		CalculateAccuracy ();
	}
	
	public void ResetAttemptedPutts()	{
		PuttsAttempted = 0;
	}
	
	public void AddMadePutt()	{
		PuttsMade++;
		AddAttemptedPutt ();
		PrintStats ();
	}
	
	public void ResetMadePutts()	{
		PuttsMade = 0;
	}
	
	public float CalculateAccuracy()	{
		if (PuttsAttempted != 0) {
			Accuracy = (float)PuttsMade / (float)PuttsAttempted;
		}
		return Accuracy;
	}
	
	public void ResetAccuracy()	{
		Accuracy = 0;
	}
	
	/*
	public float LongestPutt()
	{
		//Calculate
	}
	*/
	
	/*
	public float ResetLongestPutt()
	{
		//Calculate
	}
	*/
	
	/*
	public float ShortestPutt()
	{
		//Calculate
	}
	*/
	
	/*
	public float ResetShortestPutt()
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
		stats[0] = new Statistic("KILLS", kills);
		stats[1] = new Statistic("DEATHS", deaths);
		stats[2] = new Statistic("K/D", KillDeathRatio(), Statistic.Format.ratio);
		stats[3] = new Statistic("SANK PUTTS", PuttsMade, Statistic.Format.none);
		stats[4] = new Statistic("ACCURACY", CalculateAccuracy(),Statistic.Format.percentage);
	}

	public void PrintStats() {
		//Debug.Log ("Kills: " +kills+ " Deaths: " +deaths+ " Attempted Attacks: " +attemptedAttacks+ 
		//           " Kill Streak: " +killStreak+ " Longest Kill Streak: " +longestKillStreak+ " Longest Time Alive: " +longestTimeAlive+ 
		//           " Shortest Time Alive: " +shortestTimeAlive+ " Kill/Death Ratio: " +kDRatio+ " Attempted Putts: " +PuttsAttempted+ 
		//           " Putts Made: " +PuttsMade+ " Putting Accuracy: " +Accuracy);
	}

	/*--------------------END GOLF--------------------*/
	
}
