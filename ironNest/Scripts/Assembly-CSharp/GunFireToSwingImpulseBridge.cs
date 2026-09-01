using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class GunFireToSwingImpulseBridge : MonoBehaviour
{
	[Header("References")]
	[SerializeField]
	[Tooltip("SwingController that will receive a one-shot impulse whenever a watched gun fires.\nThis bridge does NOT use Input Actions; it only listens to GunController events.")]
	private SwingController swingController;

	[SerializeField]
	[Tooltip("List of GunController instances to watch.\nThis script subscribes to each GunController.OnGunFired event on Enable and unsubscribes on Disable.\nNull entries are ignored safely.")]
	private List<GunController> gunsToWatch;

	[Header("Impulse Settings (World Space)")]
	[SerializeField]
	[Tooltip("World-space direction (XZ) of the impulse applied when a gun fires.\nX = world +X, Y = world +Z.\nThis direction is used exactly as entered; it does not need to be normalized.\nIf you want direction-only with separate strength, set this to a unit-like vector such as (1,0) or (0,1).")]
	private Vector2 impulseDirectionWorldXZ;

	[SerializeField]
	[Tooltip("Strength multiplier applied to the direction when a gun fires.\nFinal impulse = ImpulseDirectionWorldXZ * ImpulseStrength.\nUnits are arbitrary and should be tuned against SwingReceiver impulseScale/stiffness/damping.")]
	private float impulseStrength;

	[SerializeField]
	[Tooltip("Optional twist impulse around WORLD Y applied when a gun fires.\nThis is passed through to the receivers' twist channel.\nSet to 0 for no twist.")]
	private float twistImpulseWorldY;

	[Header("Variation (Optional)")]
	[SerializeField]
	[Tooltip("If enabled, applies a small random multiplier to the impulse strength per gun-fire event.\nThis is additional variation on top of any randomization already configured inside SwingController.")]
	private bool randomizeStrengthPerShot;

	[SerializeField]
	[Tooltip("Random multiplier range applied to impulseStrength when Randomize Strength Per Shot is enabled.\nExample: (0.8, 1.2) varies each shot by +/-20%.\nIf min and max are both 1, this does nothing.")]
	private Vector2 strengthMultiplierMinMax;

	[SerializeField]
	[Tooltip("If enabled, rotates the impulse direction in XZ by a random angle per shot.\nThis helps avoid identical responses when many guns fire.\nAngle is in degrees, applied around world Y.")]
	private bool randomizeDirectionPerShot;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Max absolute degrees used when Randomize Direction Per Shot is enabled.\nExample: 10 means direction is jittered within +/-10 degrees around world Y.")]
	private float directionJitterDegrees;

	private void Reset()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Subscribe(bool subscribe)
	{
	}

	private void HandleGunFired()
	{
	}
}
