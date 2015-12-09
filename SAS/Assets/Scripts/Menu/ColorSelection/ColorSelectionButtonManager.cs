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
	public List<ColorSelectionButton> colorsThatHaveBeenSelected;

	private Image handImage;
	private Image labelImage;
	private GameObject confirmedButton;
	private Button colorsConfirmed;

	TwoAxisInputControl filteredDirection;
	
	public MenuAudioManager audioManager;
			
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
		GameObject selectionChild = transform.GetChild (0).gameObject;

		GameObject cursor = selectionChild.transform.GetChild(0).gameObject;
		handImage = cursor.GetComponent<Image> ();
		GameObject label = selectionChild.transform.GetChild(1).gameObject;
		labelImage = label.GetComponent<Image> ();
		ColorInputManager colorInputManagerScript = playerInputManager.GetComponent<ColorInputManager> ();

		int i = 0;
		while (colorInputManagerScript.colorStarts[i].focusedUponTheNightWhenTheHorsesAreFree)
		{
			i++;
		}
		focusedButton = colorInputManagerScript.colorStarts[i];
		focusedButton.focusedUponTheNightWhenTheHorsesAreFree = true;
		layla = liaison.GetComponent<GameControlLiaison>();
	}
	
	public void ResetNumber()
	{
	//	GetComponentInChildren<Text>().text = (playerNumber+1).ToString();
	}

	void Update()
	{
		foreach (ColorSelectionButton btn in colorsThatHaveBeenSelected) {
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

		string previousButtonName = focusedButton.name;
		// Move focus with directional inputs.
		if (filteredDirection.Up.WasPressed)
		{
			audioManager.PlayMoveSelection();
			focusedButton = GetAvailableUp(focusedButton);
			Debug.Log ("prev: " + previousButtonName);
			if(focusedButton.name == previousButtonName && (focusedButton.name == "Back" || focusedButton.name == "ColorsConfirmed"))
			{
				focusedButton.focusedUponTheNightWhenTheHorsesAreFree = false;
				ColorInputManager manager = GameObject.Find ("ColorInputManager").GetComponent<ColorInputManager>();
				foreach(ColorSelectionButton btn in manager.colorStarts)
				{
					if(!btn.focusedUponTheNightWhenTheHorsesAreFree)
					{
						focusedButton = btn;
						focusedButton.focusedUponTheNightWhenTheHorsesAreFree = true;
						break;
					}
				}
			}
		}
			
		if (filteredDirection.Down.WasPressed)
		{
			focusedButton = GetAvailableDown(focusedButton);
			audioManager.PlayMoveSelection();
		}
			
		if (filteredDirection.Left.WasPressed)
		{
			focusedButton = GetAvailableLeft(focusedButton);
			audioManager.PlayMoveSelection();

		}
			
		if (filteredDirection.Right.WasPressed)
		{
			focusedButton = GetAvailableRight(focusedButton);
			audioManager.PlayMoveSelection();

		}
			
		if (device.Action1.WasPressed) 
		{
			if (focusedButton.isColor) 
			{	
				// Debug.Log("Focused button: " + focusedButton.name);
				Color c = focusedButton.GetComponent<ColorSelectionButton>().GetColor();
				c.a = 1;
				handImage.color = c;
				labelImage.color = c;
				layla.SetPlayerColor(device,c);
				if(colorsThatHaveBeenSelected.Count > 0)
				{
					ColorSelectionButton btn = colorsThatHaveBeenSelected[0];
					btn.focusedUponTheNightWhenTheHorsesAreFree = false;
					colorsThatHaveBeenSelected.Clear();
				}
				colorsThatHaveBeenSelected.Add(focusedButton);
			}
			else
			{
				LoadScene();
			}
			audioManager.PlayConfirm();
		}
		if (device.Action2.WasPressed) 
		{
			//Application.LoadLevel("PlayerSelectionWithController");
			layla.ErasePlayerColor(device);
			handImage.color = Color.white;
			labelImage.color = Color.white;
			if(colorsThatHaveBeenSelected.Count > 0)
			{
				foreach(ColorSelectionButton btn in colorsThatHaveBeenSelected)
				{
					btn.focusedUponTheNightWhenTheHorsesAreFree = false;
				}

				colorsThatHaveBeenSelected.Clear();
			}
			audioManager.PlayDecline();
		}
		
	}

	void LoadScene()
	{
		if (layla.number_of_players == layla.numberOfActivePlayers) 
		{
			Application.LoadLevel(focusedButton.levelToLoad);
		}
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
			Debug.Log ("Green is covered up: " + tempFocus.name);
		}
		if (!tempFocus.focusedUponTheNightWhenTheHorsesAreFree) 
		{
			tempFocus.focusedUponTheNightWhenTheHorsesAreFree = true;
			current.focusedUponTheNightWhenTheHorsesAreFree = false;
			Debug.Log ("Returning tempFocus " + tempFocus.name);
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
