using UnityEngine;
using System.Collections;
using InControl;

public class PlayerL
{
		public int number;
		public Color color;
		public InputDevice device;
		public Vector3[] respawn_points;
		public Team team;
		public IPlayerController control;
		public GameObject gameObject;
		
		public PlayerL()
		{
			Debug.Log("New player, yo.");
		}
		
		public PlayerL(InputDevice device)
		{
			this.device = device;
			Debug.Log(this + " is new. Device is " + this.device);
		}
}

