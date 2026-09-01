using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class GPUGridInstancer_Animated : MonoBehaviour
{
	[Serializable]
	public class RowSettings
	{
		[Tooltip("How many instances are in this row.\n\nRules:\n- 0 is allowed (row is empty; its spacingAfter still affects subsequent rows).\n- Values < 0 are clamped to 0.\n\nPlacement:\n- Instances extend to the RIGHT along the spawner's local +X.\n\nExample:\n- count = 4 => X positions: 0, widthSpacing, 2*widthSpacing, 3*widthSpacing.")]
		[Min(0f)]
		public int count;

		[Tooltip("Gap (distance) AFTER this row to the next row, along the row direction (spawner local +Z).\n\nHow it works:\n- Row 0 starts at Z = 0.\n- Row 1 starts at Z = spacingAfter(row 0).\n- Row 2 starts at Z = spacingAfter(row 0) + spacingAfter(row 1), etc.\n\nRules:\n- Must be >= 0.\n\nPattern example:\n- Two close rows then a gap: spacingAfter = 0.5, 2.0, 0.5, 2.0...")]
		[Min(0f)]
		public float spacingAfter;
	}

	public enum RotationDirectionMode
	{
		[Tooltip("Each instance randomly chooses clockwise (+) or counter-clockwise (-) when direction is (re)rolled.\n\nDefinition:\n- Direction follows Unity's right-hand rule around the chosen axis.")]
		BothDirectionsRandom = 0,
		[Tooltip("All instances rotate clockwise only (positive direction).\n\nDefinition:\n- Clockwise follows Unity's right-hand rule around the chosen axis.")]
		ClockwiseOnly = 1,
		[Tooltip("All instances rotate counter-clockwise only (negative direction).\n\nDefinition:\n- Counter-clockwise is the negative direction by Unity's right-hand rule around the chosen axis.")]
		CounterClockwiseOnly = 2
	}

	public enum TickEasing
	{
		[Tooltip("Smoothstep easing inside each tick segment (ease-in/ease-out). Typically feels mechanical.")]
		Smoothstep = 0,
		[Tooltip("Sine easing inside each tick segment. Slightly softer than Smoothstep.")]
		SineInOut = 1
	}

	public enum BurstTriggerMode
	{
		[Tooltip("No automatic triggering.\n\nYou must call TriggerRotationBurst() / TriggerRotationBurstWithDuration() from code or Animation Events.")]
		Manual = 0,
		[Tooltip("Trigger burst when an Animator bool parameter transitions from false -> true.\n\nNotes:\n- Works even if the Animator component is on a parent/root and this instancer is on a child.\n- You configure the Animator reference + parameter name in the inspector.")]
		AnimatorBoolEdge = 1
	}

	[Header("Mesh + Material")]
	[Tooltip("Mesh to GPU-instance.\n\nRequired:\n- Assign a Mesh.\n\nNotes:\n- Draws submesh 0.\n- Uses Graphics.DrawMeshInstanced (max 1023 instances per draw call).\n- For large instance counts, keep mesh vertex count low.")]
	[SerializeField]
	private Mesh mesh;

	[Tooltip("Material used for instanced rendering.\n\nRequired:\n- Enable 'Enable GPU Instancing' on this Material.\n\nNotes:\n- Animation is applied by updating the instance matrices (no custom shader required).")]
	[SerializeField]
	private Material material;

	[Header("Rows (Per-Row Overrides)")]
	[Tooltip("If non-empty, this list defines the row layout.\n\nPer row you can set:\n- count: number of objects in that row\n- spacingAfter: gap after that row (local +Z)\n\nRules:\n- If this list has N entries, there are N rows.\n- If this list is empty, the grid falls back to the 'Legacy Rows/Columns' section.\n\nRuntime expectation:\n- Intended to remain stable during play.")]
	[SerializeField]
	private List<RowSettings> rows;

	[Header("Legacy Rows/Columns (Used When Rows List Is Empty)")]
	[Tooltip("Number of rows when the per-row 'Rows' list is empty. Minimum 1.")]
	[Min(1f)]
	[SerializeField]
	private int legacyRows;

	[Tooltip("Number of columns (objects per row) when the per-row 'Rows' list is empty. Minimum 1.")]
	[Min(1f)]
	[SerializeField]
	private int legacyColumns;

	[Tooltip("Row spacing (gap AFTER each row) when the per-row 'Rows' list is empty. Must be >= 0.")]
	[Min(0f)]
	[SerializeField]
	private float legacyRowSpacing;

	[Header("Spacing (Width / X)")]
	[Tooltip("Universal spacing between instances along the grid width direction (spawner local +X).\n\nRules:\n- Must be >= 0.")]
	[Min(0f)]
	[SerializeField]
	private float widthSpacing;

	[Header("Instance Transform (Base)")]
	[Tooltip("Uniform scale applied to every instance. Must be > 0.")]
	[Min(0.0001f)]
	[SerializeField]
	private float uniformScale;

	[Tooltip("Static rotation offset applied to every instance (degrees), in addition to this GameObject's rotation.\n\nRules:\n- Changing this rebuilds the instance set.")]
	[SerializeField]
	private Vector3 baseEulerRotation;

	[Header("Rotation Burst Trigger")]
	[Tooltip("How rotation bursts are triggered.\n\nManual:\n- TriggerRotationBurst() / TriggerRotationBurstWithDuration() must be called.\n\nAnimatorBoolEdge:\n- This component reads an Animator bool parameter and triggers a burst when it goes false->true.\n\nRecommended setup (prefab-friendly):\n- Put Animator on the prefab root.\n- Put this instancer on a child.\n- Set Burst Trigger Mode = AnimatorBoolEdge and leave Burst Animator empty to auto-find in parents.")]
	[SerializeField]
	private BurstTriggerMode burstTriggerMode;

	[Tooltip("Animator to read when Burst Trigger Mode = AnimatorBoolEdge.\n\nAuto-find:\n- If left null, this script will try to find an Animator in parents using GetComponentInParent<Animator>().\n\nUse this field if:\n- You want to explicitly reference a specific Animator (e.g., multiple Animators in parents).")]
	[SerializeField]
	private Animator burstAnimator;

	[Tooltip("Animator bool parameter name used when Burst Trigger Mode = AnimatorBoolEdge.\n\nRules:\n- Must match the Animator parameter name exactly (case sensitive).\n\nSafe examples:\n- \"TriggerSpin\"\n- \"RotateBurst\"\n\nNotes:\n- If this parameter does not exist on the Animator, the trigger will never fire (GetBool returns false).")]
	[SerializeField]
	private string burstAnimatorBoolParameter;

	[Tooltip("EXPOSED MIRROR of the Animator bool (for convenience).\n\nWhat this is:\n- This is a local bool on THIS component that you can animate with Unity's Animator.\n- When it transitions from false->true, this component triggers a burst.\n\nWhy this exists:\n- Lets you drive burst triggering via Animator even if the Animator is NOT directly setting a parameter on itself,\n  or if you want the trigger to live entirely on the child.\n\nImportant:\n- This does NOT automatically modify the Animator's own parameters.\n- It is just a serialized field that can be animated.\n\nHow to use:\n- In an animation clip, add a curve for this component -> 'Burst Trigger (Local Bool Mirror)'.\n- Set it to 1 for one frame (or a short window), then back to 0.\n\nRecommended:\n- Keep it as a one-frame pulse.\n\nNotes:\n- This field is ONLY used if 'Use Local Trigger Bool (Instead Of Animator Parameter)' is enabled.")]
	[SerializeField]
	private bool burstTriggerLocalBool;

	[Tooltip("If enabled, burst triggering is driven by 'Burst Trigger (Local Bool Mirror)' instead of reading an Animator parameter.\n\nUse this when:\n- You want to animate a property on this child component directly (no Animator parameter needed).\n\nIf disabled:\n- Burst triggering uses the Animator bool parameter specified in 'Animator Bool Parameter'.")]
	[SerializeField]
	private bool useLocalTriggerBoolInsteadOfAnimatorParameter;

	[Tooltip("If enabled, after detecting a false->true edge on the Animator bool parameter, this script will attempt to set it back to false.\n\nWhy:\n- Lets the Animator parameter behave like a one-shot trigger even though it's a bool.\n\nNotes:\n- Only applies when using the Animator parameter mode (not the local mirror bool mode).")]
	[SerializeField]
	private bool autoResetAnimatorBoolToFalse;

	[Header("Rotation Burst Settings")]
	[Tooltip("Default duration (seconds) that a rotation burst lasts when triggered.\n\nRules:\n- Must be >= 0.\n\nExample:\n- 0.75 => rotate for 0.75 seconds, then freeze exactly in place.")]
	[Min(0f)]
	[SerializeField]
	private float burstDurationSeconds;

	[Tooltip("If enabled, triggering a burst while one is already active will restart the timer.\n\nIf disabled, additional triggers during an active burst are ignored.")]
	[SerializeField]
	private bool restartBurstIfTriggeredWhileActive;

	[Tooltip("If enabled, each time a burst is triggered, every instance re-rolls its rotation speed and direction.\n\nNotes:\n- Angles are NOT reset, so the pose does not jump.")]
	[SerializeField]
	private bool rerollSpeedAndDirectionOnBurst;

	[Header("Rotation Parameters")]
	[Tooltip("Axis of rotation in this spawner's LOCAL space.\n\nRules:\n- If near-zero, defaults to (0, 1, 0).\n\nNotes:\n- Axis is converted to world space using this GameObject's rotation.")]
	[SerializeField]
	private Vector3 rotationAxisLocal;

	[Tooltip("Controls whether instances rotate in both directions (random), clockwise only, or counter-clockwise only.\n\nDefinition:\n- Directions follow Unity's right-hand rule around the chosen axis.")]
	[SerializeField]
	private RotationDirectionMode rotationDirectionMode;

	[Tooltip("Minimum absolute rotation speed in degrees/second. Must be >= 0.")]
	[Min(0f)]
	[SerializeField]
	private float minSpeedDegPerSec;

	[Tooltip("Maximum absolute rotation speed in degrees/second. Must be >= Min Speed.")]
	[Min(0f)]
	[SerializeField]
	private float maxSpeedDegPerSec;

	[Tooltip("Base seed used for deterministic random generation.\n\nNotes:\n- Build/layout uses this seed.\n- Each burst can optionally re-roll using a derived seed (seed combined with burst count).\n\nRules:\n- Changing this rebuilds and resets angles (expected).")]
	[SerializeField]
	private int randomSeed;

	[Tooltip("If enabled, each instance starts with a random initial angle offset (0..360 degrees). Applied once on build.")]
	[SerializeField]
	private bool randomizeStartPhase;

	[Tooltip("If enabled, each instance rotates in-place around its own pivot (its own position).\n\nIf disabled, instances orbit around the SAME pivot (this GameObject's position) while also rotating.")]
	[SerializeField]
	private bool pivotPerInstance;

	[Header("Tick / Clock-like Motion")]
	[Tooltip("If enabled, rotation is quantized into segments like a clock tick.\n\nHow it works:\n- Rotation angle is driven by a continuous internal angle.\n- The displayed angle is derived from that internal angle by easing within each segment,\n  so it 'accelerates' and then 'clicks' into each segment.")]
	[SerializeField]
	private bool useTickMotion;

	[Tooltip("Number of segments per full 360° rotation. Must be >= 1.\n\nExamples:\n- 60 => second-hand ticks.\n- 12 => coarse steps.")]
	[Min(1f)]
	[SerializeField]
	private int tickSegments;

	[Tooltip("Easing used within each tick segment.")]
	[SerializeField]
	private TickEasing tickEasing;

	[Tooltip("How strongly the motion snaps into the segment edge.\n\nRange:\n- 0..1")]
	[Range(0f, 1f)]
	[SerializeField]
	private float tickSnapStrength;

	[Header("Timing")]
	[Tooltip("If enabled, stepping uses delta time:\n- Play Mode: Time.deltaTime\n- Edit Mode: derived from realtimeSinceStartupAsDouble (clamped)\n\nNotes:\n- Helps mimic runtime behavior in edit mode for testing.")]
	[SerializeField]
	private bool useDeltaTimeIntegration;

	[Header("Rendering")]
	[Tooltip("Shadow casting mode for instanced rendering.")]
	[SerializeField]
	private ShadowCastingMode shadowCasting;

	[Tooltip("Whether instances receive shadows.")]
	[SerializeField]
	private bool receiveShadows;

	[Tooltip("Layer used for rendering these instances. Range 0..31.")]
	[SerializeField]
	private int layer;

	private const int BatchSize = 1023;

	private readonly List<Matrix4x4[]> _matrixBatches;

	private readonly List<int> _batchCounts;

	private Vector3[] _baseWorldPositions;

	private float[] _speedDegPerSec;

	private float[] _dir;

	private float[] _angleDegRaw;

	private bool _burstActive;

	private float _burstRemaining;

	private int _burstCount;

	private bool _animParamPrev;

	private bool _localMirrorPrev;

	private double _lastEditorTime;

	private Mesh _lastMesh;

	private Material _lastMaterial;

	private float _lastWidthSpacing;

	private float _lastUniformScale;

	private Vector3 _lastBaseEulerRotation;

	private float _lastMinSpeed;

	private float _lastMaxSpeed;

	private int _lastSeed;

	private bool _lastRandomizeStartPhase;

	private bool _lastPivotPerInstance;

	private Vector3 _lastRotationAxisLocal;

	private RotationDirectionMode _lastRotationDirectionMode;

	private bool _lastUseTickMotion;

	private int _lastTickSegments;

	private TickEasing _lastTickEasing;

	private float _lastTickSnapStrength;

	private bool _lastUseDeltaTimeIntegration;

	private ShadowCastingMode _lastShadowCasting;

	private bool _lastReceiveShadows;

	private int _lastLayer;

	private Vector3 _lastPos;

	private Quaternion _lastRot;

	private int _lastLegacyRows;

	private int _lastLegacyColumns;

	private float _lastLegacyRowSpacing;

	private int _lastRowsHash;

	private BurstTriggerMode _lastBurstTriggerMode;

	private Animator _lastBurstAnimator;

	private string _lastBurstAnimatorBoolParameter;

	private bool _lastAutoResetAnimatorBoolToFalse;

	private bool _lastUseLocalTriggerBoolInsteadOfAnimatorParameter;

	private void OnEnable()
	{
	}

	private void OnValidate()
	{
	}

	private void Update()
	{
	}

	private void PollLocalMirrorBoolAndTriggerIfNeeded()
	{
	}

	private void ResolveAnimatorIfNeeded(bool force)
	{
	}

	private bool ReadAnimatorBoolSafe()
	{
		return false;
	}

	private void PollAnimatorBoolAndTriggerIfNeeded()
	{
	}

	public void TriggerRotationBurst()
	{
	}

	public void TriggerRotationBurstWithDuration(float durationSeconds)
	{
	}

	private void DrawBatches()
	{
	}

	private void RebuildIfNeeded(bool force)
	{
	}

	private int ComputeRowsHash()
	{
		return 0;
	}

	private void BuildAllPreservingAnglesWhenPossible()
	{
	}

	private void AllocateBatchesAndFillMatrices()
	{
	}

	private void RebuildMatricesFromCurrentState()
	{
	}

	private void StepBurstAndUpdateMatrices()
	{
	}

	private float EvaluateTickAngle(float rawAngleDeg)
	{
		return 0f;
	}

	private float ApplyTickEasing(float t)
	{
		return 0f;
	}

	private float PickDirection(System.Random rng)
	{
		return 0f;
	}

	private void RerollSpeedAndDirectionForBurst()
	{
	}

	private Matrix4x4 BuildMatrixForIndex(int idx, Vector3 scale)
	{
		return default(Matrix4x4);
	}

	private float GetDeltaTimeSeconds()
	{
		return 0f;
	}

	private int[] GetRowPlan(out float[] rowStartZ)
	{
		rowStartZ = null;
		return null;
	}
}
