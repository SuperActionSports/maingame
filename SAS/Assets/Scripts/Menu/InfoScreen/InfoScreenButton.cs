using System;
using UnityEngine;
using InControl;

	public class InfoScreenButton : MonoBehaviour
	{
		Renderer cachedRenderer;

		public InfoScreenButton up = null;
		public InfoScreenButton down = null;
		public InfoScreenButton left = null;
		public InfoScreenButton right = null;


		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
		}


		void Update()
		{
			// Find out if we're the focused button.
			if (transform.parent.GetComponent<InfoScreenButton>() != null)
			{
				bool hasFocus = transform.parent.GetComponent<InfoScreenButtonManager>().focusedButton == this;
			}

			// Fade alpha in and out depending on focus.
			//var color = cachedRenderer.material.color;
			//color.a = Mathf.MoveTowards( color.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f );
			//cachedRenderer.material.color = color;
		}
	}


