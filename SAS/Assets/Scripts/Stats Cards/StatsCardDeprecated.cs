using UnityEngine;
using System.Collections;

public class StatsCardDeprecated : MonoBehaviour {

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
		Golf_ResetStats ();
		Tennis_ResetStats ();
		Baseball_ResetStats ();
		Fencing_ResetStats ();
		Hockey_ResetStats ();
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

	/*--------------------GOLF--------------------*/
	public int golf_PuttsAttempted;
	public int golf_PuttsMade;
	//public float golf_longestPutt;
	//public float golf_ShortestPutt;
	public float golf_Accuracy;

	public void Golf_ResetStats() {
		Golf_ResetAttemptedPutts ();
		Golf_ResetMadePutts ();
		//Golf_ResetLongestPutt();
		Golf_ResetAccuracy ();
	}

	public void Golf_AddAttemptedPutt()	{
		golf_PuttsAttempted++;
		Golf_CalculateAccuracy ();
	}
	
	public void Golf_ResetAttemptedPutts()	{
		golf_PuttsAttempted = 0;
	}
	
	public void Golf_AddMadePutt()	{
		golf_PuttsMade++;
		Golf_AddAttemptedPutt ();
	}
	
	public void Golf_ResetMadePutts()	{
		golf_PuttsMade = 0;
	}
	
	public void Golf_CalculateAccuracy()	{
		if (golf_PuttsAttempted != 0) {
			golf_Accuracy = golf_PuttsMade / golf_PuttsAttempted;
		}
	}

	public void Golf_ResetAccuracy()	{
		golf_Accuracy = 0;
	}

	/*
	public float Golf_LongestPutt()
	{
		//Calculate
	}
	*/

	/*
	public float Golf_ResetLongestPutt()
	{
		//Calculate
	}
	*/

	/*
	public float Golf_ShortestPutt()
	{
		//Calculate
	}
	*/
	
	/*
	public float Golf_ResetShortestPutt()
	{
		//Calculate
	}
	*/

	/*
	public float Golf_IndividualScoring()
	{
		//Calculate
	}
	*/

	/*
	public float Golf_TeamScoring()
	{
		//Calculate
	}
	*/

	/*--------------------END GOLF--------------------*/

	/*--------------------TENNIS--------------------*/
	public int tennis_Swings;
	public int tennis_Contact; //Swings that hit the ball not other players
	public float tennis_Accuracy;

	public void Tennis_ResetStats() {
		Tennis_ResetSwings ();
		Tennis_ResetContact();
		Tennis_ResetAccuracy ();
	}

	public void Tennis_AddSwing()	{
		tennis_Swings++;
	}

	public void Tennis_ResetSwings()	{
		tennis_Swings = 0;
	}

	public void Tennis_AddContact()	{
		tennis_Contact++;
		Tennis_AddSwing ();
	}
	
	public void Tennis_ResetContact()	{
		tennis_Contact = 0;
	}

	public void Tennis_CalculateAccuracy()	{
		if (tennis_Swings != 0) {
			tennis_Accuracy = tennis_Contact / tennis_Swings;
		}
	}
	
	public void Tennis_ResetAccuracy()	{
		tennis_Accuracy = 0;
	}

	/*
	public float Tennis_IndividualScoring()
	{
		//Calculate
	}
	*/
		
		/*
	public float Tennis_TeamScoring()
	{
		//Calculate
	}
	*/

	/*--------------------END TENNIS--------------------*/

	/*--------------------BASEBALL--------------------*/
	public int baseball_HitsAttempted;
	public int baseball_HitsMade;
	//public int baseball_longestHit;
	public float baseball_ERA;

	public void Baseball_ResetStats() {
		Baseball_ResetHitsAttempted ();
		Baseball_ResetHitsMade ();
		//Baseball_ResetLongestHit ();
		Baseball_ResetERA ();
	}

	public void Baseball_AddAttemptedHit () {
		baseball_HitsAttempted++;
	}

	public void Baseball_ResetHitsAttempted() {
		baseball_HitsAttempted = 0;
	}

	public void Baseball_AddMadeHit () {
		baseball_HitsMade++;
		Baseball_AddAttemptedHit ();
	}

	public void Baseball_ResetHitsMade() {
		baseball_HitsMade = 0;
	}

	public void Baseball_CalculateERA()	{
		if (baseball_HitsAttempted != 0) {
			baseball_ERA = baseball_HitsMade / baseball_HitsAttempted;
		}
	}
	
	public void Baseball_ResetERA()	{
		baseball_ERA = 0;
	}

	/*
	public float Baseball_LongestHit()
	{
		//Calculate
	}
	*/

	/*
	public float Baseball_ResetLongestHit()
	{
		//Calculate
	}
	*/

	/*
	public float Baseball_IndividualScoring()
	{
		//Calculate
	}
	*/
	
	/*
	public float Baseball_TeamScoring()
	{
		//Calculate
	}
	*/
	
	/*--------------------END BASEBALL--------------------*/

	/*--------------------FENCING--------------------*/
	public int fencing_StabAttempts;
	public int fencing_StabKills;
	public float fencing_StabAccuracy;
	public int fencing_ThrowAttempts;
	public int fencing_ThrowKills;
	public float fencing_ThrowAccuracy;
	public int fencing_AttackAttempts;
	public int fencing_AttacksSuccessful;
	public float fencing_AttackSuccessRate;
	public int fencing_BlockAttempts;
	public int fencing_BlocksSuccessful;
	public float fencing_BlockSuccessRate;
	public float fencing_LongestTimeUnarmed;
	public float fencing_ShortestTimeUnarmed;
	public float fencing_unarmed;
	public float fencing_rearmed;
	public float fencing_unarmedTime;

	public void Fencing_ResetStats () {
		Fencing_ResetStabAttempts();
		Fencing_ResetStabKills();
		Fencing_ResetStabAccuracy ();
		Fencing_ResetThrowAttempts();
		Fencing_ResetThrowKills();
		Fencing_ResetThrowAccuracy ();
		Fencing_ResetAttackAttempts();
		Fencing_ResetAttacksSuccessful();
		Fencing_ResetAttackSuccessRate ();
		Fencing_ResetBlockAttempts();
		Fencing_ResetBlocksSuccessful();
		Fencing_ResetBlockSuccessRate ();
		Fencing_ResetLongestTimeUnarmed();
		Fencing_ResetShortestTimeUnarmed ();
	}

	public void Fencing_AddStabAttempts (){
		Fencing_AddAttackAttempts ();
		fencing_StabAttempts++;
		Fencing_CalculateStabAccuracy ();
	}

	public void Fencing_ResetStabAttempts(){
		fencing_StabAttempts = 0;
	}

	public void Fencing_AddStabKills(){
		Fencing_AddStabAttempts ();
		Fencing_AddAttacksSuccessful ();
		fencing_StabKills++;
		Fencing_CalculateStabAccuracy ();
	}

	public void Fencing_ResetStabKills(){
		fencing_StabKills = 0;
	}

	public void Fencing_CalculateStabAccuracy(){
		if (fencing_StabAttempts != 0) {
			fencing_StabAccuracy = fencing_StabKills / fencing_StabAttempts;
		}
	}

	public void Fencing_ResetStabAccuracy(){
		fencing_StabAccuracy = 0;
	}

	public void Fencing_AddThrowAttempts(){
		Fencing_AddAttackAttempts ();
		fencing_ThrowAttempts++;
		Fencing_CalculateThrowAccuracy ();
	}

	public void Fencing_ResetThrowAttempts(){
		fencing_ThrowAttempts = 0;
	}

	public void Fencing_AddThrowKills(){
		Fencing_AddThrowAttempts ();
		Fencing_AddAttacksSuccessful ();
		fencing_ThrowKills++;
		Fencing_CalculateThrowAccuracy ();
	}

	public void Fencing_ResetThrowKills(){
		fencing_ThrowKills = 0;
	}

	public void Fencing_CalculateThrowAccuracy(){
		if (fencing_ThrowAttempts != 0) {
			fencing_ThrowAccuracy = fencing_ThrowKills / fencing_ThrowAttempts;
		}
	}

	public void Fencing_ResetThrowAccuracy(){
		fencing_ThrowAccuracy = 0;
	}

	public void Fencing_AddAttackAttempts(){
		fencing_AttackAttempts++;
		Fencing_CalculateAttackSuccessRate ();
	}

	public void Fencing_ResetAttackAttempts(){
		fencing_AttackAttempts = 0;
	}

	public void Fencing_AddAttacksSuccessful(){
		fencing_AttacksSuccessful++;
		Fencing_CalculateAttackSuccessRate ();
	}

	public void Fencing_ResetAttacksSuccessful(){
		fencing_AttacksSuccessful = 0;
	}

	public void Fencing_CalculateAttackSuccessRate(){
		if (fencing_AttackAttempts != 0) {
			fencing_AttackSuccessRate = fencing_AttacksSuccessful / fencing_AttackAttempts;
		}
	}

	public void Fencing_ResetAttackSuccessRate(){
		fencing_AttackSuccessRate = 0;
	}

	public void Fencing_AddBlockAttempts(){
		fencing_BlockAttempts++;
	}

	public void Fencing_ResetBlockAttempts(){
		fencing_BlockAttempts = 0;
	}

	public void Fencing_AddBlocksSuccessful(){
		fencing_BlocksSuccessful++;
	}

	public void Fencing_ResetBlocksSuccessful(){
		fencing_BlocksSuccessful = 0;
	}

	public void Fencing_CalculateBlockSuccessRate(){
		if (fencing_BlockAttempts != 0) {
			fencing_BlockSuccessRate = fencing_BlocksSuccessful / fencing_BlockAttempts;
		}
	}

	public void Fencing_ResetBlockSuccessRate(){
		fencing_BlockSuccessRate = 0;
	}

	public void Fencing_BecameUnarmed() {
		fencing_unarmed = Time.deltaTime;
	}

	public void Fencing_Rearmed(){
		fencing_rearmed = Time.deltaTime;
		Fencing_CalculateLongestTimeUnarmed ();
		Fencing_CalculateShortestTimeUnarmed ();
	}

	public void Fencing_CalculateLongestTimeUnarmed(){
		fencing_unarmedTime = fencing_rearmed - fencing_unarmed;
		if (fencing_unarmedTime > fencing_LongestTimeUnarmed) {
			fencing_LongestTimeUnarmed = fencing_unarmedTime;
		}
	}

	public void Fencing_ResetLongestTimeUnarmed(){
		fencing_LongestTimeUnarmed = 0;
	}

	public void Fencing_CalculateShortestTimeUnarmed(){
		fencing_unarmedTime = fencing_rearmed - fencing_unarmed;
		if (fencing_unarmedTime < fencing_ShortestTimeUnarmed)	{
			fencing_ShortestTimeUnarmed = fencing_unarmedTime;
		}
	}

	public void Fencing_ResetShortestTimeUnarmed(){
		fencing_ShortestTimeUnarmed = -1;
	}

	/*
	public float Fencing_IndividualScoring()
	{
		//Calculate
	}
	*/
	
	/*
	public float Fencing_TeamScoring()
	{
		//Calculate
	}
	*/

	/*--------------------END FENCING--------------------*/

	/*--------------------HOCKEY--------------------*/
	public float hockey_PuckPossession; //Increases Everytime the player touches the puck

	public void Hockey_ResetStats () {
		Hockey_ResetPuckPossession ();
	}

	public void Hockey_AddPuckPossession() {
		hockey_PuckPossession++;
	}

	public void Hockey_ResetPuckPossession() {
		hockey_PuckPossession = 0;
	}

	/*
	public float Hockey_IndividualScoring()
	{
		//Calculate
	}
	*/
	
	/*
	public float Hockey_TeamScoring()
	{
		//Calculate
	}
	*/
	
	
	/*--------------------END HOCKEY--------------------*/



}
