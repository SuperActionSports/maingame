using UnityEngine;
using System.Collections;

public class FencingStatsCard : StatsCard {

	/*--------------------FENCING--------------------*/
	public int StabAttempts;
	public int StabKills;
	public float StabAccuracy;
	public int ThrowAttempts;
	public int ThrowKills;
	public float ThrowAccuracy;
	public int BlockAttempts;
	public int BlocksSuccessful;
	public float BlockSuccessRate;
	public float LongestTimeUnarmed;
	//public float ShortestTimeUnarmed;
	public float unarmed;
	public float rearmed;
	public float unarmedTime;
	public bool stabThrowFlag; //True is stab. False is throw.
	
	public void HardResetStats () {
		ResetStabAttempts();
		ResetStabKills();
		ResetStabAccuracy ();
		ResetThrowAttempts();
		ResetThrowKills();
		ResetThrowAccuracy ();
		ResetAttackSuccessRate ();
		ResetBlockAttempts();
		ResetBlocksSuccessful();
		ResetBlockSuccessRate ();
		ResetLongestTimeUnarmed();
		//ResetShortestTimeUnarmed ();
		ResetStats ();
	}
	
	public void AddStabAttempts (){
		AddAttemptedAttack ();
		StabAttempts++;
		CalculateStabAccuracy ();
		SetAttackFlagToStab ();
	}
	
	public void ResetStabAttempts(){
		StabAttempts = 0;
	}
	
	public void AddStabKills(){
		AddKill ();
		StabKills++;
		CalculateStabAccuracy ();
	}
	
	public void ResetStabKills(){
		StabKills = 0;
	}
	
	public void CalculateStabAccuracy(){
		if (StabAttempts != 0) {
			StabAccuracy = (float)StabKills / (float)StabAttempts;
		}
	}
	
	public void ResetStabAccuracy(){
		StabAccuracy = 0;
	}
	
	public void AddThrowAttempts(){
		AddAttemptedAttack ();
		ThrowAttempts++;
		CalculateThrowAccuracy ();
		SetAttackFlagToThrow ();
	}
	
	public void ResetThrowAttempts(){
		ThrowAttempts = 0;
	}
	
	public void AddThrowKills(){
		AddKill ();
		ThrowKills++;
		CalculateThrowAccuracy ();
	}
	
	public void ResetThrowKills(){
		ThrowKills = 0;
	}
	
	public void CalculateThrowAccuracy(){
		if (ThrowAttempts != 0) {
			ThrowAccuracy = (float)ThrowKills / (float)ThrowAttempts;
		}
	}
	
	public void ResetThrowAccuracy(){
		ThrowAccuracy = 0;
	}
	
	public void AddBlockAttempts(){
		BlockAttempts++;
	}
	
	public void ResetBlockAttempts(){
		BlockAttempts = 0;
	}
	
	public void AddBlocksSuccessful(){
		BlocksSuccessful++;
	}
	
	public void ResetBlocksSuccessful(){
		BlocksSuccessful = 0;
	}
	
	public void CalculateBlockSuccessRate(){
		if (BlockAttempts != 0) {
			BlockSuccessRate = (float)BlocksSuccessful / (float)BlockAttempts;
		}
	}
	
	public void ResetBlockSuccessRate(){
		BlockSuccessRate = 0;
	}
	
	public void BecameUnarmed() {
		unarmed = Time.time;
	}
	
	public void Rearmed(){
		rearmed = Time.time;
		CalculateLongestTimeUnarmed ();
		//CalculateShortestTimeUnarmed ();
	}
	
	public void CalculateLongestTimeUnarmed(){
		unarmedTime = rearmed - unarmed;
		if (unarmedTime > LongestTimeUnarmed) {
			LongestTimeUnarmed = unarmedTime;
		}
	}
	
	public void ResetLongestTimeUnarmed(){
		LongestTimeUnarmed = 0;
	}

	public void SetAttackFlagToStab() {
		stabThrowFlag = true;
	}

	public void SetAttackFlagToThrow() {
		stabThrowFlag = false;
	}

	public bool AttackFlag{ get { return stabThrowFlag; } }

	/*
	public void CalculateShortestTimeUnarmed(){
		unarmedTime = rearmed - unarmed;
		if (unarmedTime < ShortestTimeUnarmed)	{
			ShortestTimeUnarmed = unarmedTime;
		}
	}
	
	public void ResetShortestTimeUnarmed(){
		ShortestTimeUnarmed = -1;
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
		stats[3] = new Statistic("Attack Accuracy", AttackSuccessRate, false);
	}

	/*--------------------END FENCING--------------------*/
}
