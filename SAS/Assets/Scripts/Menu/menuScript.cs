using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuScript : MonoBehaviour {
	
	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
	public Button creditsText;
	
	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		creditsText = creditsText.GetComponent<Button> ();
		quitMenu.enabled = false;
	}
	
	public void QuitPress(){
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
		creditsText.enabled = false;
	}
	
	public void NoPress(){
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
		creditsText.enabled = true;
	}
	
	public void StartLevel() {
		Application.LoadLevel ("LevelScreen");
	}
	
	public void ExitGame()	{
		Application.Quit ();
	}

	public void CreditsScene() {
		Application.LoadLevel ("CreditsScreen");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
