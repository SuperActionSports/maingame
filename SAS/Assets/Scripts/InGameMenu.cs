using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public KeyCode escape;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(escape))
		{
			Application.LoadLevel ("AdvancedMainMenu");
		}
	}
}
