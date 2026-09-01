using UnityEngine;

[DisallowMultipleComponent]
public sealed class SwingImpulseOnEnable : MonoBehaviour
{
	[Header("Target")]
	[SerializeField]
	[Tooltip("The SwingController instance that will receive the one-shot impulse.\nRequired.\n\nThis component calls:\n  SwingController.TriggerExternalImpulse(worldXZ, worldTwistY)\nSo your SwingController must implement that public method.")]
	private SwingController swingController;

	[Header("Impulse (World Space)")]
	[SerializeField]
	[Tooltip("World-space impulse direction in the XZ plane.\nX = world +X, Y = world +Z.\n\nNotes:\n- This vector does NOT need to be normalized.\n- If you want 'direction only', use values like (1, 0), (0, 1), (-1, 0), (0, -1).\n- If you want to bake strength into the vector, you can (e.g., (2, 0)).\n\nExamples:\n- ( 1,  0) => push toward world +X\n- ( 0,  1) => push toward world +Z\n- (-1, -1) => push diagonally toward world -X and -Z")]
	private Vector2 worldDirectionXZ;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Scalar multiplier applied to World Direction XZ.\nFinal impulse sent = worldDirectionXZ * strength.\n\nUnits are arbitrary and depend on your SwingReceiver tuning (impulseScale, stiffness, damping).\nSafe starting range: 0.1 to 5.")]
	private float strength;

	[SerializeField]
	[Tooltip("Optional twist impulse around WORLD Y applied together with the XZ impulse.\nThis is passed through to SwingController.TriggerExternalImpulse as the 'worldTwistImpulse'.\n\nSet to 0 for no twist.\nPositive/negative values twist in opposite directions (sign depends on your receiver implementation).\nSafe starting range: -0.5 to 0.5.")]
	private float worldTwistImpulseY;

	[Header("When To Fire")]
	[SerializeField]
	[Tooltip("If true, triggers the impulse in Awake.\nAwake is called before OnEnable, and can run even if the component is disabled at start (depending on Unity lifecycle usage).\n\nRecommendation:\n- Leave OFF unless you specifically need the impulse as early as possible.")]
	private bool triggerOnAwake;

	[SerializeField]
	[Tooltip("If true, triggers the impulse in OnEnable.\nThis is the most common choice for 'fire once when this object becomes active'.\n\nNote:\n- If this GameObject is toggled off/on, the impulse will fire each time it becomes enabled.\n- To prevent re-firing, enable 'Only Once Per Lifetime'.")]
	private bool triggerOnEnable;

	[SerializeField]
	[Tooltip("If true, the impulse will only be fired once for the lifetime of this component.\nThat means it will fire on the first Awake/OnEnable (whichever is enabled), and never again, even if re-enabled.\n\nUseful for scene-start impulses where objects might be toggled active later.")]
	private bool onlyOncePerLifetime;

	[Header("Debug")]
	[SerializeField]
	[Tooltip("If true, logs when the impulse is fired (Play Mode only).\nDisable in production.")]
	private bool logWhenFired;

	private bool _hasFired;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void FireIfAllowed(string source)
	{
	}
}
