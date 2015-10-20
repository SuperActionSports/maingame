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

	TwoAxisInputControl filteredDirection;
			
	void Awake()
	{
		filteredDirection = new TwoAxisInputControl();
		filteredDirection.StateThreshold = 0.5f;
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
		}
			
		if (filteredDirection.Down.WasPressed)
		{
			MoveFocusTo( focusedButton.down );
		}
			
		if (filteredDirection.Left.WasPressed)
		{
			MoveFocusTo( focusedButton.left );
		}
			
		if (filteredDirection.Right.WasPressed)
		{
			MoveFocusTo( focusedButton.right );
		}
			
		if (inputDevice.Action1.WasPressed) 
		{
			LoadScene();
		}
		if (inputDevice.Action2.WasPressed) 
		{
			//Application.LoadLevel("AdvancedPlayerColorSelection");
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
			Application.LoadLevel("ColorSelection");
		}
	}
		
		
	void MoveFocusTo( LevelSelectionButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
