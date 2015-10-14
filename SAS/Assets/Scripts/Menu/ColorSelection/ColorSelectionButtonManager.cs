using UnityEngine;
using InControl;

public class ColorSelectionButtonManager : MonoBehaviour
{
	public ColorSelectionButton focusedButton;
	public InputDevice device { get; set; }

	TwoAxisInputControl filteredDirection;
			
	void Awake()
	{
		filteredDirection = new TwoAxisInputControl();
		filteredDirection.StateThreshold = 0.5f;
	}

	void Update()
	{
		// Use last device which provided input.
		//var inputDevice = InputManager.ActiveDevice;
		filteredDirection.Filter( device.Direction, Time.deltaTime );
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
			
		if (device.Action1.WasPressed) 
		{
			LoadScene();
		}
		if (device.Action2.WasPressed) 
		{
			Application.LoadLevel("PlayerSelectionWithController");
		}
	}

	void LoadScene()
	{
		Debug.Log ("Btn: " + focusedButton.name);
	}
		
		
	void MoveFocusTo( ColorSelectionButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
