using UnityEngine;
using InControl;

public class LevelSelectionButtonManager : MonoBehaviour
{
	public LevelSelectionButton focusedButton;
	public string actionSticksName;
	public string actionStickEggName;
	public string actionFuzzyEggName;
	public string hockeyName;
	public string actionTinyEggName;

	private GameControlLiaison liason;

	TwoAxisInputControl filteredDirection;
	
	public MenuAudioManager audioManager;
			
	void Awake()
	{
		filteredDirection = new TwoAxisInputControl();
		filteredDirection.StateThreshold = 0.5f;
	}
	
	void Start()
	{
		audioManager = GetComponentInChildren<MenuAudioManager>();
		GameObject layla = GameObject.Find ("Layla");
		liason = layla.GetComponent<GameControlLiaison> ();
		Time.timeScale = 1;
	}

	void Update()
	{
		// Use last device which provided input.
		var inputDevice = InputManager.ActiveDevice;
		filteredDirection.Filter( inputDevice.Direction, Time.deltaTime );
		// filteredDirection = inputDevice.Direction;

		if (filteredDirection.Left.WasRepeated)
		{
			Debug.Log( "!!!" );
		}

		// Move focus with directional inputs.
		if (filteredDirection.Up.WasPressed)
		{
			MoveFocusTo( focusedButton.up );
			audioManager.PlayMoveSelection();
		}
			
		if (filteredDirection.Down.WasPressed)
		{
			MoveFocusTo( focusedButton.down );
			audioManager.PlayMoveSelection();
		}
			
		if (filteredDirection.Left.WasPressed)
		{
			MoveFocusTo( focusedButton.left );
			audioManager.PlayMoveSelection();
		}
			
		if (filteredDirection.Right.WasPressed)
		{
			MoveFocusTo( focusedButton.right );
			audioManager.PlayMoveSelection();
		}
			
		if (inputDevice.Action1.WasPressed) 
		{
			audioManager.PlayConfirm();
			LoadScene();
		}
		if (inputDevice.Action2.WasPressed) 
		{
			audioManager.PlayDecline();
			Application.LoadLevel("Use This Color Selection");
		}
	}

	void LoadScene()
	{
		Debug.Log ("Btn: " + focusedButton.name);
		if (focusedButton.name == "Action Sticks!") 
		{
			Application.LoadLevel (actionSticksName);
		}
		if (focusedButton.name == "Action Stick Egg!") 
		{
			Application.LoadLevel (actionStickEggName);
		}
		if (focusedButton.name == "Action Fuzzy Egg!") 
		{
			Application.LoadLevel (actionFuzzyEggName);
		}
		if (focusedButton.name == "Hockey!") 
		{
			Application.LoadLevel (hockeyName);
		}
		if (focusedButton.name == "Action Tiny Egg!") 
		{
			Application.LoadLevel (actionTinyEggName);
		}
		if (focusedButton.name == "Back") 
		{
			ResetPlayers();
			Application.LoadLevel("New Color Select Cursor");
		}
	}
		

	void ResetPlayers()
	{
		liason.SetNumberOfPlayers (0);
		liason.numberOfActivePlayers = 0;
	}
		
	void MoveFocusTo( LevelSelectionButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
