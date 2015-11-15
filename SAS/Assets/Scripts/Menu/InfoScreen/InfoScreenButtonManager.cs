using UnityEngine;
using InControl;

public class InfoScreenButtonManager : MonoBehaviour
{
	public InfoScreenButton focusedButton;
	public string levelName;

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
	}

	void LoadScene()
	{
		Debug.Log ("Btn: " + focusedButton.name);
		if (focusedButton.name == "Play") 
		{
			Application.LoadLevel (levelName);
		}
		if (focusedButton.name == "Back") 
		{
			Application.LoadLevel("LevelSelection");
		}
	}
		
		
	void MoveFocusTo( InfoScreenButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
