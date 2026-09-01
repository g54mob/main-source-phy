using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReloadStateDef
{
	public string stateKey;

	public string displayName;

	public List<string> triggers;

	[Tooltip("Is this the state where reloading is considered complete?")]
	public bool isReloadCompleteState;

	[Tooltip("If true, this state will automatically advance to the next state after being entered.")]
	public bool autoAdvanceToNextState;

	[Header("Optional: Button to advance from this state")]
	[Tooltip("If assigned, this button will be shown as active in this state and will trigger an advance when clicked.")]
	public LookAtTarget advanceButton;
}
