using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Ladder : MonoBehaviour
{
	public Transform downEmpty;

	public Transform upEmpty;

	public Transform downExit;

	public Transform upExit;

	public bool invertRotationForCoopAnimation;

	[HideInInspector]
	public EventReference ladderSoundEvent;

	[NonSerialized]
	public EventInstance ladderSoundEventInstance;

	[NonSerialized]
	public CollisionData collisionData;

	[Header("VR")]
	public Vector3 vrDirectionWhenClimbedUp;

	public Vector3 vrDirectionWhenClimbedDown;

	public void init(Game game)
	{
		collisionData = game.addCollisionData(base.gameObject);
		ladderSoundEventInstance = PineFmod.createInstance(ladderSoundEvent.Guid);
		PineFmod.set3DAttributes(ladderSoundEventInstance, PineFmod.to3DAttributes(base.transform));
	}
}
