using UnityEngine;
using System.Collections;

public class StatsCard : MonoBehaviour {

	private int kills;
	public int Kills{get { return kills; } }
	private int deaths;
	public int Deaths{get { return deaths; } }
	private int jumps;
	public int Jumps{get { return jumps; } }
	private int attemptedAttacks;
	public int AttemptedAttacks{get { return kills; } }
	private int killStreak;
	private int longestKillStreak;
	public int LongestKillStreak{get { return longestKillStreak; } }
	public float KillDeathRatio() { return ((float)(kills)/deaths); } 
	//public float distanceRan;
	private float longestTimeAlive;
	public float LongestTimeAlive{get { return longestTimeAlive; } }
	private float shortestTimeAlive;
	public float ShortestTimeAlive{get { return shortestTimeAlive; } }

	private float birth;
	private float death;
	private float lifetime;


	// Use this for initialization
	void Start () {
		kills = 0;
		deaths = 0;
		jumps = 0;
		attemptedAttacks = 0;
		killStreak = 0;
		longestKillStreak = 0;
		//distanceRan = 0;
		longestTimeAlive = 0;
		shortestTimeAlive = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetStats()	{
		kills = 0;
		deaths = 0;
		jumps = 0;
		attemptedAttacks = 0;
		killStreak = 0;
		longestKillStreak = 0;
		//distanceRan = 0;
		longestTimeAlive = 0;
		shortestTimeAlive = 0;
	}

	public void AddKill()	{
		kills++;
		killStreak++;
	}

	public void ResetKills()	{
		kills = 0;
		ResetkillStreak ();
	}

	public void AddDeath()	{
		deaths++;
		if (killStreak > longestKillStreak) {
			longestKillStreak = killStreak;
		}
		ResetkillStreak ();
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
}
