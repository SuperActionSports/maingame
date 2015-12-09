using UnityEngine;
using System.Collections;
using System;

public class PerlinAudienceManager : MonoBehaviour {

	PerlinAudienceController[] members;
	public GameObject[] culledChildren;
	public GameObject audienceMember;
	private float audienceMemberWidth;
	public int audienceCount;
	public float overLimit = 200;
	public float lim = 0;
	public Color[] colors;
	
	void Start () {
		//audienceMemberWidth = audienceMember.transform.lossyScale.x;
		/*for (int i = 0; i < audienceCount; i++)
		{
			Vector3 position = new Vector3(transform.position.x + (audienceMemberWidth * i) + (i/10f),transform.position.y,transform.position.z);
			GameObject a = Instantiate(audienceMember,position,transform.rotation) as GameObject;
			a.transform.parent = this.transform;
		}*/
		members = GetComponentsInChildren<PerlinAudienceController>();
		audienceCount = members.Length;
	}
	
	public void SetAudience(Player[] players)
	{
		colors = new Color[players.Length];
		for (int i = 0; i < players.Length; i++)
		{
			colors[i] = players[i].color;
		}
	}
	
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			SendSmallEvent();
		}	
		if (Input.GetKeyDown(KeyCode.B))
		{
			SendBigEvent();
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			KillInvisibleChildren();
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			Color[] c = {Color.cyan,Color.magenta,Color.yellow,Color.black};
			int[] s = {1,2,3,4};
			ChangeCrowdColor(c,s);
		}
		
	}
	/// <summary>
	/// Kills audience memebers who will never be on camera
	/// </summary>
	public void KillInvisibleChildren()
	{
		foreach (GameObject g in culledChildren)
		{
			g.SetActive(false);
		}
		members = GetComponentsInChildren<PerlinAudienceController>();
		audienceCount = members.Length;
	}
	
	public void SendSmallEvent()
	{
		foreach (PerlinAudienceController p in members)
		{
			p.SmallEvent();
		}
	}
	
	public void SendBigEvent()
	{
		foreach (PerlinAudienceController p in members)
		{
			p.BigEvent();
		}
	}
	
	public void ChangeCrowdColor(int[] s)
	{
		ChangeCrowdColor(colors,s);
	}
	
	public void ChangeCrowdColor(Color[] teamColors, int[] scores)
	{	
		float timeStart = Time.time;
		bool[] visitedAudience = new bool[audienceCount];
		float totalScore = 0;  
		foreach (int i in scores)
		{
			totalScore += i;
			if (lim > overLimit)
			{
				Debug.Break ();
			}
			lim++;
		} 
		lim = 0;
		float[]crowdWeights = new float[teamColors.Length]; 
		for (int w = 0; w < scores.Length; w++)
		{
			crowdWeights[w] = (scores[w]/totalScore) * audienceCount;
			if (lim > overLimit)
			{
				Debug.Break ();
			}
			lim++;
		}
		lim = 0;
		int p = 0;
		Array.Sort(crowdWeights,teamColors);
		int count = 0;
		for (int c = 0; c < scores.Length ; c++)
		{
			while(p < crowdWeights[c])
			{	
				count++;
				int pick = (int)UnityEngine.Random.Range(0,members.Length);
				if (!visitedAudience[pick])
				{
					PerlinAudienceController a = members[pick];
					a.ChangeColor(teamColors[c]);
					visitedAudience[pick] = true;	
					p++;
				}
			}
		}
		
		
	}
}
