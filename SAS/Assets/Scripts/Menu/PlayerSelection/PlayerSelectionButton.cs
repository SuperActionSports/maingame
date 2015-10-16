using System;
using UnityEngine;
using InControl;

	public class PlayerSelectionButton : MonoBehaviour
	{
		Renderer cachedRenderer;

		public PlayerSelectionButton up = null;
		public PlayerSelectionButton down = null;
		public PlayerSelectionButton left = null;
		public PlayerSelectionButton right = null;


		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
		}


		void Update()
		{
			// Find out if we're the focused button.
			bool hasFocus = transform.parent.GetComponent<PlayerSelectionButtonManager>().focusedButton == this;

			// Fade alpha in and out depending on focus.
			//var color = cachedRenderer.material.color;
			//color.a = Mathf.MoveTowards( color.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f );
			//cachedRenderer.material.color = color;
		}
	}


