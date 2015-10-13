using System;
using UnityEngine;
using InControl;
	// This is just a simple "player" script that rotates and colors a cube
	// based on input read from the actions field.
	//
	// See comments in PlayerManager.cs for more details.
	//
	public class Scale : MonoBehaviour
	{
		public InputDevice device { get; set; }

		Renderer cachedRenderer;

		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
		}


		void Update()
		{
			if (device == null)
			{
				// If no controller exists for this cube, just make it translucent.
				cachedRenderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
			}
			else
			{
				// Set object material color.
				cachedRenderer.material.color = GetColorFromInput();

				// Rotate target object.
				transform.Rotate( Vector3.down, 500.0f * Time.deltaTime * device.Direction.X, Space.World );
				transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * device.Direction.Y, Space.World );
			}
		}


		Color GetColorFromInput()
		{
			if (device.Action1)
			{
				return Color.green;
			}

			if (device.Action2)
			{
				return Color.red;
			}

			if (device.Action3)
			{
				return Color.blue;
			}

			if (device.Action4)
			{
				return Color.yellow;
			}

			return Color.white;
		}
	}


