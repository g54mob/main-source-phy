using UnityEngine;

public interface IFootstepCrouchStateProvider
{
	[Tooltip("True if the player is currently crouched.\nImplementations should return a stable state (not a 'button held' transient) matching gameplay crouch.\nUsed only for optional stride scaling.")]
	bool IsCrouched { get; }
}
