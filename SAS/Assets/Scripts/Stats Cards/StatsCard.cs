using UnityEngine;
using System.Collections;

public class StatsCard : MonoBehaviour {

	public float individualScore;
	public float teamAScore;
	public float teamBScore;

	void Start () {
		HardResetStats ();
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

	public void HardResetStats()	{
		ResetIndividualScore ();
		ResetTeamScores ();
		ResetIndividualStats ();
		//Golf_ResetStats ();
		//Tennis_ResetStats ();
		//Baseball_ResetStats ();
		//Fencing_ResetStats ();
		//Hockey_ResetStats ();
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
	public float kDRatio;

	public void ResetIndividualStats()	{
		ResetKills ();
		ResetDeaths ();
		ResetJumps ();
		ResetAttemptedAttack ();
		ResetlongestKillStreak ();
		//ResetDistanceRan ();
		ResetLongestTimeAlive ();
		ResetShortestTimeAlive ();
		ResetKDRatio ();
	}

	public void AddKill()	{
		kills++;
		killStreak++;
		AddAttemptedAttack ();
		CalculateKDRatio ();
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
		CalculateKDRatio ();
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
		birth = Time.deltaTime;
	}

	public void EndLifeTime()	{
		death = Time.deltaTime;
		lifetime = death - birth;
		if (lifetime > longestTimeAlive) {
			longestTimeAlive = lifetime;
		}
		if (lifetime < shortestTimeAlive)	{
			shortestTimeAlive = lifetime;
		}
	}

	public void ResetLongestTimeAlive() {
		longestTimeAlive = 0;
	}

	public void ResetShortestTimeAlive() {
		shortestTimeAlive = -1;
	}

	public void CalculateKDRatio() {
		kDRatio = kills / deaths;
	}

	public void ResetKDRatio() {
		kDRatio = 0;
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
