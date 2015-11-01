using UnityEngine;
using System.Collections;

public class HockeyStatsCard : StatsCard {

	/*--------------------HOCKEY--------------------*/
	public int PuckPossession = 0; //Increases Everytime the player touches the puck
	private int succesfulGoals = 0;
	
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
}
