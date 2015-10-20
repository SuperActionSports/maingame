using System;
using UnityEngine;
using System.Collections.Generic;
using InControl;

	public class LTMPlayerManager : MonoBehaviour
	{
		public GameObject playerPrefab;
		
		const int maxPlayers = 4;
		
		private bool ready = false;

		List<Vector3> playerPositions = new List<Vector3>() {
			new Vector3( 0, 0.5f, 0 ),
			new Vector3( 0,0.5f,0 ),
		new Vector3( 0,0.5f,0 ),
		new Vector3( 0, 0.5f, 0 ),
		};

		List<LiaisonTestPlayerController> players = new List<LiaisonTestPlayerController>( maxPlayers );

		public GameObject layla;
		public GameControlLiaison liaison;
		void Start()
		{
			//InputManager.OnDeviceDetached += OnDeviceDetached;
			layla = GameObject.Find("Layla");
			liaison = layla.GetComponent<GameControlLiaison>();
			if (liaison.numberOfActivePlayers > 0)
			{
				Debug.Log(InputManager.Devices);
				PullPlayersFromLiaison();
				ready = true;
			}
		}

		void PullPlayersFromLiaison()
		{
			for (int i = 0; i < liaison.numberOfActivePlayers; i++)
			{
				var gameObject = (GameObject) Instantiate( playerPrefab, playerPositions[0], Quaternion.identity );
				var player = gameObject.GetComponent<LiaisonTestPlayerController>();
				player.c1 = liaison.players[i].color;
				player.device = liaison.players[i].device;
			}
		}

		void Update()
		{
			if (!ready)
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
		}


		bool JoinButtonWasPressedOnDevice( InputDevice inputDevice )
		{
			return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
		}


		LiaisonTestPlayerController FindPlayerUsingDevice( InputDevice inputDevice )
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


		LiaisonTestPlayerController CreatePlayer( InputDevice inputDevice )
		{
			if (players.Count < maxPlayers)
			{
				// Pop a position off the list. We'll add it back if the player is removed.
				var playerPosition = playerPositions[0];
				playerPositions.RemoveAt( 0 );

				var gameObject = (GameObject) Instantiate( playerPrefab, playerPosition, Quaternion.identity );
				var player = gameObject.GetComponent<LiaisonTestPlayerController>();
				player.device = inputDevice;
				player.c1 = Color.cyan;
				players.Add( player );

				return player;
			}

			return null;
		}


		void RemovePlayer( LiaisonTestPlayerController player )
		{
			playerPositions.Insert( 0, player.transform.position );
			//players.Remove( player );
			player.device = null;
			Destroy( player.gameObject );
		}

	}
