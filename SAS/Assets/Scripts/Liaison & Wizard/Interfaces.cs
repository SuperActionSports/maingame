﻿using UnityEngine;
using System.Collections;
using InControl;

public interface IPlayerController
{
	bool Alive();
	Color Color();
	IInputHandler InputHandler();
}

public interface IInputHandler
{
	InputDevice Device();
	IPlayerController Controller();
	IWizard Wizard();
}

public interface IWizard
{
	Color[] Colors();
	int TotalPlayers();
	int Winner();
	float GameWinTime();
	int MatchCount();
	IInputHandler InputManager();
	InputDevice[] Devices();
}