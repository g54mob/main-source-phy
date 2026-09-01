using System;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(CharacterController))]
public class FirstPersonFootstepsBridge : MonoBehaviour
{
	[Serializable]
	public class FootstepSurfaceBoolEvent : UnityEvent<bool>
	{
	}

	[Header("References")]
	[Tooltip("FirstPersonController to follow/freeze awareness. If left null, this component auto-resolves it in Awake.\nUsed only for: (1) honoring playerCanMove, (2) optional crouch multiplier integration if you provide crouch state.")]
	public FirstPersonController controller;

	[Tooltip("CharacterController used to read grounded state and velocity. If left null, this component auto-resolves it in Awake.\nFootstep timing is based on CharacterController.velocity (horizontal only).")]
	public CharacterController characterController;

	[Header("Enable / Suppression")]
	[Tooltip("Master toggle. When disabled, no footsteps will be generated and distance accumulation resets.")]
	public bool enableFootsteps;

	[Tooltip("If enabled, footsteps will not play when the controller's playerCanMove is false (e.g., SetFrozen(true)).\nThis is a safe default for UI/console interactions.")]
	public bool suppressWhenPlayerCannotMove;

	[Tooltip("If enabled, footsteps will only play while CharacterController.isGrounded is true.\nRecommended for typical FPS controllers so you don't get footsteps while falling/sliding in air.")]
	public bool requireGrounded;

	[Header("Distance-Based Step Timing")]
	[Tooltip("Base distance (in meters) between footsteps while walking at normal stride.\nFootsteps trigger every time the player accumulates this much horizontal travel.\nSafe starting point: ~1.6 to 2.2 for a typical FPS.\nSmaller values = more frequent steps.")]
	[Min(0.1f)]
	public float metersPerStep;

	[Tooltip("Minimum horizontal speed (m/s) required before distance accumulation counts toward a footstep.\nPrevents tiny micro-movements from generating steps.\nSafe default: 0.15.")]
	[Min(0f)]
	public float minHorizontalSpeedForSteps;

	[Tooltip("Maximum number of footstep events that can be emitted in a single frame if the player moves very fast or teleports.\nThis is a safety cap to prevent event spam.\nSafe default: 2.")]
	[Range(1f, 10f)]
	public int maxStepsPerFrame;

	[Header("Optional Crouch Stride Scaling (Optional)")]
	[Tooltip("If enabled, the stride distance (metersPerStep) will be multiplied by crouchStrideMultiplier when crouched.\nThis makes crouch steps less frequent even if speed is similar.\nNote: Your FirstPersonController does not expose crouch state publicly, so you must provide it via 'crouchStateProvider'.\nIf you do not provide a crouch state provider, this setting has no effect.")]
	public bool useCrouchStrideMultiplier;

	[Tooltip("Stride multiplier applied when crouched (metersPerStep * crouchStrideMultiplier).\nValues > 1 = fewer steps (longer time between steps).\nValues < 1 = more steps.\nSafe default: 1.25.")]
	[Range(0.25f, 3f)]
	public float crouchStrideMultiplier;

	[Tooltip("Optional provider used to tell this bridge whether the player is currently crouched.\nThis must be a component on the SAME GameObject implementing IFootstepCrouchStateProvider.\nIf left null, crouch stride scaling cannot be applied (because FirstPersonController keeps crouch state private).")]
	public MonoBehaviour crouchStateProvider;

	[Header("Surface Detection (Custom Component)")]
	[Tooltip("Physics layers considered valid ground for surface detection raycast.\nInclude all layers your player can walk on (terrain, level geometry, moving platforms).\nRaycast uses QueryTriggerInteraction.Ignore.")]
	public LayerMask groundMask;

	[Tooltip("Raycast distance downwards to find the ground surface.\nKeep slightly larger than half the CharacterController height + a small margin.\nSafe default: 1.25.")]
	[Min(0.05f)]
	public float groundRayDistance;

	[Tooltip("Vertical offset added to the raycast origin (upwards).\nHelps ensure the ray starts above the ground and not inside colliders.\nSafe default: 0.1.")]
	[Min(0f)]
	public float groundRayOriginUpOffset;

	[Tooltip("When enabled, the bridge treats surfaces WITH a FootstepSurfaceMarker (found on the hit collider or any of its parents)\nas 'Special'. When disabled, surface detection is skipped and Default is always used.")]
	public bool enableSpecialSurfaceDetection;

	[Header("Events (Hook these to FMOD emitters)")]
	[Tooltip("Invoked every time a footstep occurs on a DEFAULT surface (no FootstepSurfaceMarker found).\nHook this to your Default FMOD StudioEventEmitter.Play().")]
	public UnityEvent OnFootstepDefault;

	[Tooltip("Invoked every time a footstep occurs on a SPECIAL surface (FootstepSurfaceMarker found and enabled).\nHook this to your Special FMOD StudioEventEmitter.Play().")]
	public UnityEvent OnFootstepSpecial;

	[Tooltip("Optional: invoked on every footstep with a bool indicating if the surface is special.\ntrue = special, false = default.\nUse this if you want a single listener to route logic yourself.")]
	public FootstepSurfaceBoolEvent OnFootstep;

	[Header("Debug (Optional)")]
	[Tooltip("If enabled, draws a debug ray in the Scene view when grounded.\nGreen = hit, Red = no hit.\nEditor-only visualization; no gameplay effect.")]
	public bool debugDrawRay;

	[Tooltip("If enabled, logs footstep events to the Console (useful while wiring FMOD).\nRecommended to disable in production.")]
	public bool debugLogSteps;

	private float _accumulatedDistance;

	private bool _wasGrounded;

	private IFootstepCrouchStateProvider _crouchProvider;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void TriggerStep()
	{
	}

	private float GetCurrentStepDistance()
	{
		return 0f;
	}

	private bool ResolveIsSpecialSurface()
	{
		return false;
	}
}
