using UnityEngine;
using System.Collections;

public class HockeyStatsCard : StatsCard {

	/*--------------------HOCKEY--------------------*/
	public int PuckPossession = 0; //Increases Everytime the player touches the puck
	private int succesfulGoals = 0;
	private int random;
	
	public void Start() {
		random = (int)(Random.Range (0.000f, 4.999f));
	}

	public void HardResetStats () {
		ResetGoals();
		ResetPuckPossession ();
		ResetStats ();
	}
	
	public void AddPuckPossession() {
		PuckPossession++;
	}
	
	public void ResetPuckPossession() {
		PuckPossession = 0;
	}

	public void AddSuccesfulGoal()
	{
		succesfulGoals++;
	}
	
	private float GoalAccuracy()
	{
		return (float)PuckPossession/(float)succesfulGoals;
	}
	
	private void ResetGoals()
	{
		succesfulGoals = 0;
	}
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
		stats[2] = new Statistic("K/D", KillDeathRatio(), Statistic.Format.ratio);
		stats[3] = new Statistic("GOALS", succesfulGoals, Statistic.Format.none);
	}

	public void PrintStats() {
		//Debug.Log ("Kills: " +kills+ " Deaths: " +deaths+ " Jumps: " +jumps+ " Attempted Attacks: " +attemptedAttacks+ 
		 //          " Kill Streak: " +killStreak+ " Longest Kill Streak: " +longestKillStreak+ " Longest Time Alive: " +longestTimeAlive+ 
		  //         " Shortest Time Alive: " +shortestTimeAlive+ " Kill/Death Ratio: " +kDRatio+ " Puck Possession: " +PuckPossession);
	}
	
	/*--------------------END HOCKEY--------------------*/
	public override float TotalScore() {
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
		
		float tsP = succesfulGoals * Pw;
		float tsKDR = kDR * KDRw;
		float tsKS = longestKillStreak * KSw;
		float tsACC = PuckPossession * ACCw;
		float tsRAN = RAN * RANw;
		
		float ts = tsP + tsKDR + tsKS + tsACC + tsRAN;
		
		return ts;
	}
}
