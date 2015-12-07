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
	private int random;
	
	void Start() {
		random = (int)(Random.Range (0.000f, 13.999f));
		Debug.Log("Random is " + random);
		float vavvvvvvvvv = Random.Range(0,14);
		Debug.Log ("Random should be: " + vavvvvvvvvv + " translates to " + (int)vavvvvvvvvv);
	}
	
	public FencingStatsCard()
	{
		HardResetStats();
		random = (int)(Random.Range (0f, 13.999f));
		Debug.Log("Fencing Stats Card: Random is " + random);
	}

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
		ResetStats ();
		//random = (int)(Random.Range (0f, 13.999f));
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
		stats[2] = new Statistic("K/D", KillDeathRatio(), Statistic.Format.ratio);
		stats[3] = new Statistic("Attack Accuracy", AttackSuccessRate, Statistic.Format.percentage);
	}

	public override float TotalScore() {
		float Pw = 0.5f;
		float KDRw = 0.32f;
		float KSw = 0.1f;
		float ACCw = 0.05f;
		float RANw = 0.03f;
		float RAN;

		float P;
		if (BlocksSuccessful == 0) {
			P = kills;
		} else {
			P = kills * BlocksSuccessful;
		}

		switch (random) {
		case 0:
			RAN = jumps;
			break;
		case 1:
			RAN = attemptedAttacks;
			break;
		case 2:
			RAN = longestTimeAlive;
			break;
		case 3:
			RAN = shortestTimeAlive;
			break;
		case 4:
			RAN = StabAttempts;
			break;
		case 5:
			RAN = StabKills;
			break;
		case 6:
			RAN = StabAccuracy;
			break;
		case 7:
			RAN = ThrowAttempts;
			break;
		case 8:
			RAN = ThrowKills;
			break;
		case 9:
			RAN = ThrowAccuracy;
			break;
		case 10:
			RAN = BlockAttempts;
			break;
		case 11:
			RAN = BlocksSuccessful;
			break;
		case 12:
			RAN = BlockSuccessRate;
			break;
		case 13:
			RAN = LongestTimeUnarmed;
			break;
		default:
			RAN = 0;
			break;
		}
		
		float tsP = P * Pw;
		float tsKDR = kDR * KDRw;
		float tsKS = longestKillStreak * KSw;
		float tsACC = attackSuccessRate * ACCw;
		float tsRAN = RAN * RANw;
		
		float ts = tsP + tsKDR + tsKS + tsACC + tsRAN;
		
		return ts;
	}
	/*--------------------END FENCING--------------------*/
}
