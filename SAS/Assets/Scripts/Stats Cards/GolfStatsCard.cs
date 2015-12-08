using UnityEngine;
using System.Collections;

public class GolfStatsCard : StatsCard {
	
	/*--------------------GOLF--------------------*/
	public int PuttsAttempted;
	public int PuttsMade;
	//public float longestPutt;
	//public float ShortestPutt;
	public float Accuracy;
	private int random;
	
	public void Start() {
		random = (int)(Random.Range (0.000f, 4.999f));
	}
	
	public GolfStatsCard()
	{
		HardResetStats();
		random = (int)(Random.Range (0f, 4.999f));
	}

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
		stats[0] = new Statistic("POINTS", (int)TotalScore());
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
		
		float tsP = PuttsMade * Pw;
		float tsKDR = kDR * KDRw;
		float tsKS = longestKillStreak * KSw;
		float tsACC = Accuracy * ACCw;
		float tsRAN = RAN * RANw;
		
		float ts = tsP + tsKDR + tsKS + tsACC + tsRAN;
		
		return (int)(ts * 10);
	}
}
