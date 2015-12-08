using UnityEngine;
using System.Collections;

public class PerlinAudienceManager : MonoBehaviour {

	PerlinAudienceController[] members;
	// Use this for initialization
	void Start () {
		members = GetComponentsInChildren<PerlinAudienceController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S))
		{
			foreach (PerlinAudienceController p in members)
			{
				p.SmallEvent();
			}
		}	
		if (Input.GetKeyDown(KeyCode.B))
		{
			foreach (PerlinAudienceController p in members)
			{
				p.BigEvent();
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			//foreach (PerlinAudienceController p in members)
			//{
		//		p.ChangeColor(Color.red);
		//	}
			for(int i = 0; i < members.Length/2; i++)
			{
				members[i].ChangeColor(Color.red);
			}
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			//foreach (PerlinAudienceController p in members)
			//{
			//	p.ChangeColor(Color.green);
			//}
			for(int i = members.Length/2; i < members.Length; i++)
			{
				members[i].ChangeColor(Color.green);
			}
		}
	}
}
