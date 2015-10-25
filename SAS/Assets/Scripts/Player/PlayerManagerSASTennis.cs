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
	public class PlayerManagerSASTennis : MonoBehaviour
	{
		public GameObject playerPrefab;
		
		const int maxPlayers = 4;

		List<Vector3> spawnPoints;

		List<TennisInputHandler> players = new List<TennisInputHandler>( maxPlayers );

		Color[] playerColors2;
		Color[] playerColors3;
		Color[] playerColors4;
		Color[] playerColors;
	
		public GameObject[] Respawns;
		
		void Awake()
		{
				playerColors = new Color[maxPlayers];
		}
		
		void Start()
		{
			playerColors[0] = Color.cyan;
			playerColors[1] = Color.magenta;
			playerColors[2] = Color.yellow;
			playerColors[3] = Color.black;
			for (int i = 0; i < maxPlayers; i++)
			{
//				Debug.Log(playerColors[i]);
			}
			InputManager.OnDeviceDetached += OnDeviceDetached;
			spawnPoints = new List<Vector3>();
			try{
				Respawns.GetLength(0);
			}
			catch
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
					CreatePlayer( inputDevice );
				}
			}
		}


		bool JoinButtonWasPressedOnDevice( InputDevice inputDevice )
		{
			return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
		}


		TennisInputHandler FindPlayerUsingDevice( InputDevice inputDevice )
		{
			var playerCount = players.Count;
			for (int i = 0; i < playerCount; i++)
			{
				var player = players[i];
				if (player.device == inputDevice)
				{
					return player;
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


		TennisInputHandler CreatePlayer( InputDevice inputDevice )
		{
			if (players.Count < maxPlayers)
			{
				// Pop a position off the list. We'll add it back if the player is removed.
				var playerPosition = spawnPoints[0];
				spawnPoints.RemoveAt( 0 );
				var gameObject = (GameObject) Instantiate( playerPrefab, playerPosition, Quaternion.identity );
				var player = gameObject.GetComponent<TennisInputHandler>();
				player.device = inputDevice;
				Debug.Log("Player count: " + players.Count + " color: " + playerColors[players.Count]);
				player.GetComponent<TennisController>().c1 = playerColors[players.Count];
				player.transform.parent = this.transform;
				players.Add( player );

				return player;
			}

			return null;
		}


		void RemovePlayer( TennisInputHandler player )
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

			GUI.Label( new Rect( 10, y, 300, y + h ), "Active players: " + players.Count + "/" + maxPlayers );
			y += h;

			if (players.Count < maxPlayers)
			{
				GUI.Label( new Rect( 10, y, 300, y + h ), "Press a button to join!" );
				y += h;
			}
		}
	}
