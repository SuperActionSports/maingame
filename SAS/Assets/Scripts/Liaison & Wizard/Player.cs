using UnityEngine;
using System.Collections;
using InControl;

public class Player : MonoBehaviour
{
		public int number;
		public Color color;
		public InputDevice device;
		public Vector3[] respawn_points;
		public Team team;
		public IPlayerController control;
}

