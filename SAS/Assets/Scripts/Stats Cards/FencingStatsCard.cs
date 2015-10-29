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
	public int AttackAttempts;
	public int AttacksSuccessful;
	public float AttackSuccessRate;
	public int BlockAttempts;
	public int BlocksSuccessful;
	public float BlockSuccessRate;
	public float LongestTimeUnarmed;
	public float ShortestTimeUnarmed;
	public float unarmed;
	public float rearmed;
	public float unarmedTime;
	
	public void ResetStats () {
		ResetStabAttempts();
		ResetStabKills();
		ResetStabAccuracy ();
		ResetThrowAttempts();
		ResetThrowKills();
		ResetThrowAccuracy ();
		ResetAttackAttempts();
		ResetAttacksSuccessful();
		ResetAttackSuccessRate ();
		ResetBlockAttempts();
		ResetBlocksSuccessful();
		ResetBlockSuccessRate ();
		ResetLongestTimeUnarmed();
		ResetShortestTimeUnarmed ();
	}
	
	public void AddStabAttempts (){
		AddAttackAttempts ();
		StabAttempts++;
		CalculateStabAccuracy ();
	}
	
	public void ResetStabAttempts(){
		StabAttempts = 0;
	}
	
	public void AddStabKills(){
		AddStabAttempts ();
		AddAttacksSuccessful ();
		StabKills++;
		CalculateStabAccuracy ();
	}
	
	public void ResetStabKills(){
		StabKills = 0;
	}
	
	public void CalculateStabAccuracy(){
		if (StabAttempts != 0) {
			StabAccuracy = StabKills / StabAttempts;
		}
	}
	
	public void ResetStabAccuracy(){
		StabAccuracy = 0;
	}
	
	public void AddThrowAttempts(){
		AddAttackAttempts ();
		ThrowAttempts++;
		CalculateThrowAccuracy ();
	}
	
	public void ResetThrowAttempts(){
		ThrowAttempts = 0;
	}
	
	public void AddThrowKills(){
		AddThrowAttempts ();
		AddAttacksSuccessful ();
		ThrowKills++;
		CalculateThrowAccuracy ();
	}
	
	public void ResetThrowKills(){
		ThrowKills = 0;
	}
	
	public void CalculateThrowAccuracy(){
		if (ThrowAttempts != 0) {
			ThrowAccuracy = ThrowKills / ThrowAttempts;
		}
	}
	
	public void ResetThrowAccuracy(){
		ThrowAccuracy = 0;
	}
	
	public void AddAttackAttempts(){
		AttackAttempts++;
		CalculateAttackSuccessRate ();
	}
	
	public void ResetAttackAttempts(){
		AttackAttempts = 0;
	}
	
	public void AddAttacksSuccessful(){
		AttacksSuccessful++;
		CalculateAttackSuccessRate ();
	}
	
	public void ResetAttacksSuccessful(){
		AttacksSuccessful = 0;
	}
	
	public void CalculateAttackSuccessRate(){
		if (AttackAttempts != 0) {
			AttackSuccessRate = AttacksSuccessful / AttackAttempts;
		}
	}
	
	public void ResetAttackSuccessRate(){
		AttackSuccessRate = 0;
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
			BlockSuccessRate = BlocksSuccessful / BlockAttempts;
		}
	}
	
	public void ResetBlockSuccessRate(){
		BlockSuccessRate = 0;
	}
	
	public void BecameUnarmed() {
		unarmed = Time.deltaTime;
	}
	
	public void Rearmed(){
		rearmed = Time.deltaTime;
		CalculateLongestTimeUnarmed ();
		CalculateShortestTimeUnarmed ();
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
	
	public void CalculateShortestTimeUnarmed(){
		unarmedTime = rearmed - unarmed;
		if (unarmedTime < ShortestTimeUnarmed)	{
			ShortestTimeUnarmed = unarmedTime;
		}
	}
	
	public void ResetShortestTimeUnarmed(){
		ShortestTimeUnarmed = -1;
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
	
	/*--------------------END FENCING--------------------*/
}
