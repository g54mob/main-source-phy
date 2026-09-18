using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VInspector;

[Serializable]
public class TutorialCheck
{
	[HideInInspector]
	public string name;

	public TutorialStepType tutorialStepID;

	public List<InputActionReference> input;

	[ReadOnly]
	public bool isCompleted;

	public void UpdataName()
	{
		name = tutorialStepID.ToString();
	}
}
