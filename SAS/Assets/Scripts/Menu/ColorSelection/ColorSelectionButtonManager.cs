using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class ColorSelectionButtonManager : MonoBehaviour
{
	public ColorSelectionButton focusedButton;
	public InputDevice device { get; set; }
	public GameObject liaison;
	private GameControlLiaison layla;
	public ColorSelectionButton confirmColors;
	public String levelToLoad;
	public List<ColorSelectionButton> colorButtons;

	private Image hand;
	private GameObject confirmedButton;
	private Button colorsConfirmed;

	TwoAxisInputControl filteredDirection;
			
	void Awake()
	{
		filteredDirection = new TwoAxisInputControl();
		filteredDirection.StateThreshold = 0.5f;
	}

	void Start()
	{
		confirmedButton = GameObject.Find ("ColorsConfirmed");
		colorsConfirmed = confirmedButton.GetComponent<Button> ();
		GameObject playerInputManager = GameObject.Find ("ColorInputManager");
		hand = this.GetComponentInChildren<Image> ();
		ColorInputManager colorInputManagerScript = playerInputManager.GetComponent<ColorInputManager> ();
		//Debug.Log ("COlor Button Manager Position: " + transform.position);
		int i = 0;
		while (colorInputManagerScript.colorStarts[i].focusedUponTheNightWhenTheHorsesAreFree)
		{
			i++;
		}
		focusedButton = colorInputManagerScript.colorStarts[i];
		focusedButton.focusedUponTheNightWhenTheHorsesAreFree = true;
		layla = liaison.GetComponent<GameControlLiaison>();
		//colorInputManagerScript.colorStarts.RemoveAt (i);
	}
	
	public void ResetNumber()
	{
	//	GetComponentInChildren<Text>().text = (playerNumber+1).ToString();
	}

	void Update()
	{
		foreach (ColorSelectionButton btn in colorButtons) {
			btn.focusedUponTheNightWhenTheHorsesAreFree = true;
		}
		// Use last device which provided input.
		//var inputDevice = InputManager.ActiveDevice;
		filteredDirection.Filter( device.Direction, Time.deltaTime );
		// filteredDirection = inputDevice.Direction;
//		if (layla.AllPlayersActive())
//		{
//			focusedButton = confirmColors;
//		}
		if (filteredDirection.Left.WasRepeated)
		{
			Debug.Log( "!!!" );
		}

		// Move focus with directional inputs.
		if (filteredDirection.Up.WasPressed)
		{
			focusedButton = GetAvailableUp(focusedButton);
		}
			
		if (filteredDirection.Down.WasPressed)
		{
			focusedButton = GetAvailableDown(focusedButton);
		}
			
		if (filteredDirection.Left.WasPressed)
		{
			focusedButton = GetAvailableLeft(focusedButton);
		}
			
		if (filteredDirection.Right.WasPressed)
		{
			focusedButton = GetAvailableRight(focusedButton);
		}
			
		if (device.Action1.WasPressed) 
		{
			if (focusedButton.isColor) 
			{	
				// Debug.Log("Focused button: " + focusedButton.name);
				Color c = focusedButton.GetComponent<ColorSelectionButton>().GetColor();
				c.a = 1;
				hand.color = c;
				layla.SetPlayerColor(device,c);
				if(colorButtons.Count > 0)
				{
					ColorSelectionButton btn = colorButtons[0];
					btn.focusedUponTheNightWhenTheHorsesAreFree = false;
					colorButtons.Clear();
				}
				colorButtons.Add(focusedButton);
			}
			else
			{
				LoadScene();
			}
		}
		if (device.Action2.WasPressed) 
		{
			//Application.LoadLevel("PlayerSelectionWithController");
			layla.ErasePlayerColor(device);
			hand.color = Color.white;
			if(colorButtons.Count > 0)
			{
				foreach(ColorSelectionButton btn in colorButtons)
				{
					btn.focusedUponTheNightWhenTheHorsesAreFree = false;
				}

				colorButtons.Clear();
			}
		}
		
	}

	void LoadScene()
	{
		Application.LoadLevel(focusedButton.levelToLoad);
	}
		
	ColorSelectionButton GetAvailableLeft(ColorSelectionButton current)
	{
		ColorSelectionButton tempFocus = current.left;
		while (tempFocus.focusedUponTheNightWhenTheHorsesAreFree && tempFocus.left != tempFocus)
		{
			tempFocus = tempFocus.left;
		}
		if (!tempFocus.focusedUponTheNightWhenTheHorsesAreFree)
		{
			tempFocus.focusedUponTheNightWhenTheHorsesAreFree = true;
			//if(!colorButtonNames.Contains(tempFocus.name))
			//{
				current.focusedUponTheNightWhenTheHorsesAreFree = false;
			//}
			return tempFocus;
		}
		return current;
	}
	
	ColorSelectionButton GetAvailableRight(ColorSelectionButton current)
	{
		ColorSelectionButton tempFocus = current.right;
		
		while (tempFocus.focusedUponTheNightWhenTheHorsesAreFree && tempFocus.right != tempFocus)
		{
			tempFocus = tempFocus.right;
		}
		if (!tempFocus.focusedUponTheNightWhenTheHorsesAreFree)
		{
			tempFocus.focusedUponTheNightWhenTheHorsesAreFree = true;
			//if(!colorButtonNames.Contains(tempFocus.name))
			//{
				current.focusedUponTheNightWhenTheHorsesAreFree = false;
			//}
			return tempFocus;
		}
		else
		{
			return current;
		}
	}
	
	ColorSelectionButton GetAvailableDown(ColorSelectionButton current)
	{
		ColorSelectionButton tempFocus = current.down;
		while (tempFocus.focusedUponTheNightWhenTheHorsesAreFree && tempFocus.down != tempFocus)
		{
			tempFocus = tempFocus.down;
		}
		if (!tempFocus.focusedUponTheNightWhenTheHorsesAreFree)
		{
			tempFocus.focusedUponTheNightWhenTheHorsesAreFree = true;
			current.focusedUponTheNightWhenTheHorsesAreFree = false;
			return tempFocus;
		}
		return current;
	}
	
	ColorSelectionButton GetAvailableUp(ColorSelectionButton current)
	{
		ColorSelectionButton tempFocus = current.up;
		while (tempFocus.focusedUponTheNightWhenTheHorsesAreFree && tempFocus.up != tempFocus)
		{
			tempFocus = tempFocus.up;
		}
		if (!tempFocus.focusedUponTheNightWhenTheHorsesAreFree) {
			tempFocus.focusedUponTheNightWhenTheHorsesAreFree = true;
			current.focusedUponTheNightWhenTheHorsesAreFree = false;
			return tempFocus;
		}
			return current;
	}
	
			
//	bool MoveFocusTo( ColorSelectionButton newFocusedButton )
//	{
//		//Debug.Log ("newFocusedButton Position: " + newFocusedButton.transform.localPosition);
//		//Debug.Log ("newFocusedButton: " + newFocusedButton);
//		if (newFocusedButton != null && !newFocusedButton.focusedUponTheNightWhenTheHorsesAreFree)
//		{
//			focusedButton = newFocusedButton;
//			focusedButton.focusedUponTheNightWhenTheHorsesAreFree = true;
//			return true;
//		}
//		else
//		{
//		return false;
//		}
//	}
}
