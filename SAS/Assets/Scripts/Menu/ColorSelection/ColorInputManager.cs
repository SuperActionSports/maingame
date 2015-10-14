using System;
using UnityEngine;
using System.Collections.Generic;
using InControl;
	
	// This example roughly illustrates the proper way to add multiple players from existing
	// devices. Notice how InputManager.Devices is not used and no index into it is taken.
	// Rather a device references are stored in each player and we use InputManager.OnDeviceDetached
	// to know when one is detached.
	//
	// InputManager.Devices should be considered a pool from which devices may be chosen,
	// not a player list. It could contain non-responsive or unsupported controllers, or there could
	// be more connected controllers than your game supports, so that isn't a good strategy.
	//
	// To detect a joining player, we just check the current active device (which is the last
	// device to provide input) for a relevant button press, check that it isn't already assigned
	// to a player, and then create a new player with it.
	//
	// NOTE: Due to how Unity handles joysticks, disconnecting a single device will currently cause
	// all devices to detach, and the remaining ones to reattach. There is no reliable workaround
	// for this issue. As a result, a disconnecting controller essentially resets this example.
	// In a more real world scenario, we might keep the players around and throw up some UI to let
	// users activate controllers and pick their players again before resuming.
	//
	// This example could easily be extended to use bindings. The process would be very similar,
	// just creating a new instance of your action set subclass per player and assigning the
	// device to its Device property.
	//
	public class ColorInputManager : MonoBehaviour
	{
		public GameObject colorSelectionManagerPrefab;
		
		const int maxColorSelectors = 4;

		List<Vector3> spawnPoints;

		List<ColorSelectionButtonManager> colorSelectionManagers = new List<ColorSelectionButtonManager>( maxColorSelectors );

		public ColorSelectionButton[] colorStarts;
	
		void Start()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;

			spawnPoints = new List<Vector3>();
			try{
				colorStarts.GetLength(0);
			}
			catch (NullReferenceException e)
			{
				Debug.Log("You didn't set the Respawns prefab, dingus.");
			}
			foreach (ColorSelectionButton c in colorStarts)
			{
				spawnPoints.Add(c.transform.position);
			}
		}

		void Update()
		{
			var inputDevice = InputManager.ActiveDevice;

			if (JoinButtonWasPressedOnDevice( inputDevice ))
			{
				if (ThereIsNoPlayerUsingDevice( inputDevice ))
				{
					CreateButton( inputDevice );
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
				var playerPosition = spawnPoints[0];
				spawnPoints.RemoveAt( 0 );

				var gameObject = (GameObject) Instantiate( colorSelectionManagerPrefab, playerPosition, Quaternion.identity );
				var buttonManager = gameObject.GetComponent<ColorSelectionButtonManager>();
				buttonManager.device = inputDevice;
				//scale.c1 = Color.cyan;
				colorSelectionManagers.Add( buttonManager );

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