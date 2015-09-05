using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelScript : MonoBehaviour {
	
	public Button l1;
	public Button l2;
	public Button l3;
	public Button backButton;
	public string level1name;
	public string level2name;
	public string level3name;
	
	// Use this for initialization
	void Start () {
		l1 = l1.GetComponent<Button> ();
		l2 = l2.GetComponent<Button> ();
		l3 = l3.GetComponent<Button> ();
		backButton = backButton.GetComponent<Button> ();
	}
	
	public void StartLevel1() {
		Application.LoadLevel (level1name);
	}

	public void StartLevel2() {
		Application.LoadLevel (level2name);
	}

	public void StartLevel3() {
		Application.LoadLevel (level3name);
	}
	
	public void MainMenu() {
		Application.LoadLevel ("TitleScreen");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
