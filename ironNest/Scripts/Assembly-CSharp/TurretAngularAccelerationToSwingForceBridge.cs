using UnityEngine;

[DisallowMultipleComponent]
public sealed class TurretAngularAccelerationToSwingForceBridge : MonoBehaviour
{
	[Header("References")]
	[SerializeField]
	[Tooltip("SwingController that will receive a continuous world-space impulse each frame.\nThis bridge does NOT use Input Actions; it derives motion purely from TurretController telemetry.")]
	private SwingController swingController;

	[SerializeField]
	[Tooltip("TurretController to observe.\nSource of truth used here:\n- TurretController.CurrentRotationSpeed (deg/sec, signed; per TurretController docs: + = CCW).\n\nThis script computes angular acceleration from the change in that value.")]
	private TurretController turretController;

	[Header("Sign Convention (Direction)")]
	[SerializeField]
	[Tooltip("If true (default), the applied WORLD X force sign is:\n  sign(forceX) = sign(angularAcceleration)\n\nGiven TurretController's convention (+ speed = CCW), this means:\n- accelerating CCW OR braking while previously moving CW => angular accel > 0 => +X\n- accelerating CW OR braking while previously moving CCW => angular accel < 0 => -X\n\nIf your scene feels reversed, disable this (or toggle Invert Output below).")]
	private bool useAccelerationSignForXDirection;

	[SerializeField]
	[Tooltip("If true, negates the final WORLD X force.\nUse this if the sign convention in your content feels backwards even though the math is correct.")]
	private bool invertOutput;

	[Header("Acceleration Measurement")]
	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("Optional smoothing applied to the computed angular acceleration before mapping.\n0 = no smoothing.\nHigher values = smoother but slower response.\nRecommended starting range: 0.05 to 0.25.")]
	private float accelerationSmoothing;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Clamp applied to the computed angular acceleration magnitude (deg/sec^2) to prevent extreme spikes.\nThis protects against frame hitches, pause/unpause, or sudden simulation changes.\n0 disables clamping.\nRecommended starting range: 200 to 3000 depending on turret dynamics.")]
	private float maxAbsAngularAcceleration;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Deadzone for acceleration (deg/sec^2). If |accel| is below this, output force is treated as 0.\nUse this to eliminate tiny jitter when turret speed is nearly constant.\nRecommended starting range: 0 to 10.")]
	private float accelerationDeadzone;

	[Header("Mapping: |Angular Acceleration| -> |World X Force|")]
	[SerializeField]
	[Tooltip("AnimationCurve mapping ABSOLUTE angular acceleration magnitude to ABSOLUTE WORLD X force magnitude.\n\nCurve X-axis: |angularAcceleration| in deg/sec^2 (non-negative).\nCurve Y-axis: |worldXForce| (non-negative recommended, but not required).\n\nImportant:\n- This curve is evaluated with abs(accel).\n- The final sign (+X/-X) is applied separately using accel sign (see 'Use Acceleration Sign For X Direction').\n- No remapping is performed.\n\nSafe example curve:\n- Key (0, 0)\n- Key (1000, 1)")]
	private AnimationCurve absAngularAccelerationToAbsWorldXForce;

	[SerializeField]
	[Tooltip("Global multiplier applied to the mapped force (after curve evaluation).\nUse this to tune strength without editing the curve.")]
	private float outputMultiplier;

	[Header("Debug (Optional)")]
	[SerializeField]
	[Tooltip("If true, logs the computed speed/acceleration/force occasionally for tuning.\nDisable in production.")]
	private bool logDebug;

	[SerializeField]
	[Min(0.01f)]
	[Tooltip("Minimum seconds between debug log prints when Log Debug is enabled.")]
	private float debugLogInterval;

	private float _lastSpeedDegPerSec;

	private bool _hasLastSpeed;

	private float _smoothedAccel;

	private float _nextLogTime;

	private void Reset()
	{
	}

	private void Update()
	{
	}
}
