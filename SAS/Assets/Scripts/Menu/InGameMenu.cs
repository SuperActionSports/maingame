using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public KeyCode escape;
	public GameObject menu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(escape))
		{
			Time.timeScale = 0.0f;
			menu.SetActive(true);
		}
	}

	public void quitMiniGame() {
		Application.LoadLevel ("AdvancedMainMenu");
	}

	public void resumeMiniGame() {
		menu.SetActive(false);
		Time.timeScale = 1.0f;
	}

	public void toLevelSelect() {
		Application.LoadLevel ("AdvancedLevelSelection");
	}
}
