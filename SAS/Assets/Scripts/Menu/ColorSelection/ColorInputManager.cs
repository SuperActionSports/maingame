using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using InControl;
	
	public class ColorInputManager : MonoBehaviour
	{
		public GameObject gameSelectionManagerPrefab;
		
		const int maxColorSelectors = 4;

		List<Vector3> spawnPoints;
		
		public GameObject layla;

		List<ColorSelectionButtonManager> colorSelectionManagers = new List<ColorSelectionButtonManager>( maxColorSelectors );

		public List<ColorSelectionButton> colorStarts;

		public List<Sprite> labels;

		private GameObject confirmedButton;
		private Button colorsConfirmedButton;
		private ColorSelectionButton colorsConfirmedSelectionButton;

		void Start()
		{
			confirmedButton = GameObject.Find ("ColorsConfirmed");
			
			colorsConfirmedButton = confirmedButton.GetComponent<Button> ();

			colorsConfirmedSelectionButton = confirmedButton.GetComponent<ColorSelectionButton> ();
			
			InputManager.OnDeviceDetached += OnDeviceDetached;

			spawnPoints = new List<Vector3> ();

			try{
				int sizeOfBtns = colorStarts.Count;
			}
			catch
			{
				Debug.Log("You didn't set the Respawns prefab, dingus.");
			}
			foreach (ColorSelectionButton c in colorStarts)
			{
				spawnPoints.Add(c.transform.position);
			}
			layla = GameObject.Find("Layla");
			Debug.Log("Layla: " + layla);
			
		
		}

		void Update()
		{
			var inputDevice = InputManager.ActiveDevice;

			if (JoinButtonWasPressedOnDevice( inputDevice ))
			{
				if (ThereIsNoPlayerUsingDevice( inputDevice ))
				{
					CreateGameSelectionManager(inputDevice );
				}
			}
			
			bool activeDevicesHaveSelectedColors = CheckDevicesSelection ();

			if (!activeDevicesHaveSelectedColors) {
				colorsConfirmedSelectionButton.focusedUponTheNightWhenTheHorsesAreFree = true;
				colorsConfirmedButton.interactable = false;
			} else {
				colorsConfirmedSelectionButton.focusedUponTheNightWhenTheHorsesAreFree = false;
				colorsConfirmedButton.interactable = true;
			}
		}


		bool JoinButtonWasPressedOnDevice( InputDevice inputDevice )
		{
			return inputDevice.Action1.WasPressed;
		}


		ColorSelectionButtonManager FindPlayerUsingDevice( InputDevice inputDevice )
		{
			var btnCount = colorSelectionManagers.Count;
			for (int i = 0; i < btnCount; i++)
			{
				var btn = colorSelectionManagers[i];
				if (btn.device == inputDevice)
				{
					return btn;
				}
			}

			return null;
		}


		bool ThereIsNoPlayerUsingDevice( InputDevice inputDevice )
		{
			return FindPlayerUsingDevice( inputDevice ) == null;
		}


		void OnDeviceDetached( InputDevice inputDevice )
		{
			var player = FindPlayerUsingDevice( inputDevice );
			if (player != null)
			{
				//RemovePlayer( player );
			}
		}


		ColorSelectionButtonManager CreateGameSelectionManager( InputDevice inputDevice )
		{
			if (colorSelectionManagers.Count < maxColorSelectors)
			{
				Sprite playerLabelSprite = labels[0];
				labels.RemoveAt(0);

				// Pop a position off the list. We'll add it back if the player is removed.
				var manager = (GameObject) Instantiate( gameSelectionManagerPrefab, new Vector3(0,0,0), Quaternion.identity );
				manager.transform.SetParent(GameObject.Find("ColorInputManager").transform);
				manager.transform.localPosition = new Vector3(0,0,0);
				manager.transform.localScale = new Vector3(1,1,1);
				GameObject selectionChild = manager.transform.GetChild(0).gameObject;
				int grandChildCount = selectionChild.transform.childCount;
				
				for(int i = 0; i < grandChildCount; i++)
				{
					if(selectionChild.transform.GetChild(i).name == "PlayerLabel")
					{
						GameObject playerLabelObj = selectionChild.transform.GetChild(i).gameObject;
						Image img = playerLabelObj.GetComponent<Image>();
						img.sprite = playerLabelSprite;
					}
				}

				var buttonManager = manager.GetComponent<ColorSelectionButtonManager>();
				buttonManager.device = inputDevice;
				buttonManager.liaison = layla;
				buttonManager.ResetNumber();
				colorSelectionManagers.Add( buttonManager );

				Debug.Log("Adding a new player with device: " + inputDevice);
				layla.GetComponent<GameControlLiaison>().CreatePlayer(inputDevice);
				return buttonManager;
			}

			return null;
		}


		void Remove( ColorSelectionButtonManager c )
		{
			spawnPoints.Insert( 0, c.transform.position );
			//players.Remove( player );
			c.device = null;
			Destroy( c.gameObject );
		}

		bool CheckDevicesSelection ()
		{
			GameControlLiaison liaison = layla.GetComponent<GameControlLiaison> ();

			if (liaison.number_of_players == liaison.numberOfActivePlayers) {
				return true;
			} else {
				return false;
			}
		}
	}