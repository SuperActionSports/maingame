using UnityEngine;
using System.Collections;

public class GoalScored : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "East Net" || col.gameObject.name == "West Net")
		{
			Debug.Log("Goal Scored");
		}
	}
}
