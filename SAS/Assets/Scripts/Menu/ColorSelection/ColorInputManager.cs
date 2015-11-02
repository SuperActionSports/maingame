using System;
using UnityEngine;
using System.Collections.Generic;
using InControl;
	
	public class ColorInputManager : MonoBehaviour
	{
		public GameObject colorSelectionManagerPrefab;
		
		const int maxColorSelectors = 4;

		List<Vector3> spawnPoints;
		
		public GameObject layla;

		List<ColorSelectionButtonManager> colorSelectionManagers = new List<ColorSelectionButtonManager>( maxColorSelectors );

		public List<ColorSelectionButton> colorStarts;
	
		void Start()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;

			spawnPoints = new List<Vector3>();
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
					CreateButton(inputDevice );
				}
			}
		}


		bool JoinButtonWasPressedOnDevice( InputDevice inputDevice )
		{
			return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
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


		ColorSelectionButtonManager CreateButton( InputDevice inputDevice )
		{
			if (colorSelectionManagers.Count < maxColorSelectors)
			{
				// Pop a position off the list. We'll add it back if the player is removed.
				//var playerPosition = spawnPoints[0];
				//spawnPoints.RemoveAt( 0 );

				var gameObject = (GameObject) Instantiate( colorSelectionManagerPrefab, new Vector3(0,0,0), Quaternion.identity );
				gameObject.transform.SetParent(GameObject.Find("ColorInputManager").transform);
				gameObject.transform.localPosition = new Vector3(0,0,0);
				gameObject.transform.localScale = new Vector3(1,1,1);
				var buttonManager = gameObject.GetComponent<ColorSelectionButtonManager>();
				buttonManager.device = inputDevice;
				buttonManager.liaison = layla;
				//scale.c1 = Color.cyan;
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


		void OnGUI()
		{
			const float h = 22.0f;
			var y = 10.0f;

			GUI.Label( new Rect( 10, y, 300, y + h ), "Active players: " + colorSelectionManagers.Count + "/" + maxColorSelectors );
			y += h;

			if (colorSelectionManagers.Count < maxColorSelectors)
			{
				GUI.Label( new Rect( 10, y, 300, y + h ), "Press a button to join!" );
				y += h;
			}
		}
	}