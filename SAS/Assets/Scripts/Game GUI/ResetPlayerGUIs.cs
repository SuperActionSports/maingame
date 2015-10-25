using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ResetPlayerGUIs : MonoBehaviour {

	public Transform players;
	public GameObject textIcon;
	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			ResetPlayers();
		}	
	}
	
	void ResetPlayers()
	{
		int p = 1;
		BaseballPlayerController[] newPlayers = players.GetComponentsInChildren<BaseballPlayerController>();
		foreach (BaseballPlayerController b in newPlayers)
		{
			GameObject t = Instantiate(textIcon);
			PlayerIconScript ts = t.GetComponent<PlayerIconScript>();
			t.GetComponent<Text>().text = "P" + p;
			ts.target = b.transform;
			ts.color = b.color;
			t.transform.SetParent(transform,false);
			p++;
		}
	}
	
	
}
