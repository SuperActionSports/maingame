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
	public class ScaleManager : MonoBehaviour
	{
		public GameObject scalePrefab;
		
		const int maxScales = 4;

		List<Vector3> spawnPoints;

		List<Scale> scales = new List<Scale>( maxScales );

		public GameObject[] Respawns;
	
		void Start()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;

			spawnPoints = new List<Vector3>();
			try{
				Respawns.GetLength(0);
			}
			catch (NullReferenceException e)
			{
				Debug.Log("You didn't set the Respawns prefab, dingus.");
			}
			foreach (GameObject g in Respawns)
			{
				spawnPoints.Add(g.transform.position);
			}
		}

		void Update()
		{
			var inputDevice = InputManager.ActiveDevice;

			if (JoinButtonWasPressedOnDevice( inputDevice ))
			{
				if (ThereIsNoPlayerUsingDevice( inputDevice ))
				{
					CreateScale( inputDevice );
				}
			}
		}


		bool JoinButtonWasPressedOnDevice( InputDevice inputDevice )
		{
			return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
		}


		Scale FindPlayerUsingDevice( InputDevice inputDevice )
		{
			var scaleCount = scales.Count;
			for (int i = 0; i < scaleCount; i++)
			{
				var scale = scales[i];
				if (scale.device == inputDevice)
				{
					return scale;
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


		Scale CreateScale( InputDevice inputDevice )
		{
			if (scales.Count < maxScales)
			{
				// Pop a position off the list. We'll add it back if the player is removed.
				var playerPosition = spawnPoints[0];
				spawnPoints.RemoveAt( 0 );

				var gameObject = (GameObject) Instantiate( scalePrefab, playerPosition, Quaternion.identity );
				var scale = gameObject.GetComponent<Scale>();
				scale.device = inputDevice;
				//scale.c1 = Color.cyan;
				scales.Add( scale );

				return scale;
			}

			return null;
		}


		void RemovePlayer( GolfPlayerController player )
		{
			spawnPoints.Insert( 0, player.transform.position );
			//players.Remove( player );
			player.device = null;
			Destroy( player.gameObject );
		}


		void OnGUI()
		{
			const float h = 22.0f;
			var y = 10.0f;

			GUI.Label( new Rect( 10, y, 300, y + h ), "Active players: " + scales.Count + "/" + maxScales );
			y += h;

			if (scales.Count < maxScales)
			{
				GUI.Label( new Rect( 10, y, 300, y + h ), "Press a button to join!" );
				y += h;
			}
		}
	}