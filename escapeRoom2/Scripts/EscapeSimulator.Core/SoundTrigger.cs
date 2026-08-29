using System;
using FMODUnity;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
	public EventReference onEnter;

	public EventReference onExit;

	public EventReference footsteps;

	public ParamRef ambienceParam;

	public ParamRef musicParam;

	[NonSerialized]
	public StudioEventEmitter onEnterEmitter;
}
