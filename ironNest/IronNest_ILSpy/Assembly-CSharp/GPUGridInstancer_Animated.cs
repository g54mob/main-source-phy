using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

public class GPUGridInstancer_Animated : MonoBehaviour
{
	[Serializable]
	public class RowSettings
	{
		public int count = 10;

		public float spacingAfter = 1f;
	}

	public enum RotationDirectionMode
	{
		BothDirectionsRandom,
		ClockwiseOnly,
		CounterClockwiseOnly
	}

	public enum TickEasing
	{
		Smoothstep,
		SineInOut
	}

	public enum BurstTriggerMode
	{
		Manual,
		AnimatorBoolEdge
	}

	private Mesh mesh;

	private Material material;

	private List<RowSettings> rows;

	private int legacyRows;

	private int legacyColumns;

	private float legacyRowSpacing;

	private float widthSpacing;

	private float uniformScale;

	private Vector3 baseEulerRotation;

	private BurstTriggerMode burstTriggerMode;

	private Animator burstAnimator;

	private string burstAnimatorBoolParameter;

	private bool burstTriggerLocalBool;

	private bool useLocalTriggerBoolInsteadOfAnimatorParameter;

	private bool autoResetAnimatorBoolToFalse;

	private float burstDurationSeconds;

	private bool restartBurstIfTriggeredWhileActive;

	private bool rerollSpeedAndDirectionOnBurst;

	private Vector3 rotationAxisLocal;

	private RotationDirectionMode rotationDirectionMode;

	private float minSpeedDegPerSec;

	private float maxSpeedDegPerSec;

	private int randomSeed;

	private bool randomizeStartPhase;

	private bool pivotPerInstance;

	private bool useTickMotion;

	private int tickSegments;

	private TickEasing tickEasing;

	private float tickSnapStrength;

	private bool useDeltaTimeIntegration;

	private ShadowCastingMode shadowCasting;

	private bool receiveShadows;

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
		double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
		_lastEditorTime = realtimeSinceStartupAsDouble;
		if (burstTriggerMode == BurstTriggerMode.AnimatorBoolEdge && burstAnimator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			Animator animator = default(Animator);
			burstAnimator = animator;
		}
		bool animParamPrev = ReadAnimatorBoolSafe();
		_animParamPrev = animParamPrev;
		_localMirrorPrev = burstTriggerLocalBool;
		RebuildIfNeeded(force: true);
	}

	private void OnValidate()
	{
		//IL_0348: Invalid comparison between I4 and F4
		//IL_04d5: Invalid comparison between I4 and F4
		//IL_002a: Expected F4, but got I4
		//IL_050b: Invalid comparison between I4 and F4
		//IL_0038: Expected F4, but got I4
		//IL_03b0: Invalid comparison between I4 and F4
		//IL_0055: Expected F4, but got I4
		//IL_0063: Expected F4, but got I4
		//IL_056a: Invalid comparison between I4 and F4
		//IL_00bc: Expected F4, but got I4
		//IL_0412: Invalid comparison between I4 and F4
		//IL_00ca: Expected F4, but got I4
		//IL_0283: Expected O, but got I
		//IL_0293: Expected O, but got I
		//IL_019b: Expected O, but got I4
		//IL_01b9: Expected O, but got I
		//IL_01ec: Expected O, but got I
		//IL_021d: Expected O, but got I4
		int num = legacyRows;
		if (legacyRows < 1)
		{
			num = 1;
		}
		legacyRows = num;
		int num2 = legacyColumns;
		if (legacyColumns < 1)
		{
			num2 = 1;
		}
		float num3 = legacyRowSpacing;
		legacyColumns = num2;
		if (0f > legacyRowSpacing)
		{
			num3 = 0f;
		}
		legacyRowSpacing = num3;
		float num4 = widthSpacing;
		if (0f > widthSpacing)
		{
			num4 = 0f;
		}
		widthSpacing = num4;
		bool flag = 0.0001f > uniformScale;
		float num5 = 0.0001f;
		if (!flag)
		{
			num5 = uniformScale;
		}
		uniformScale = num5;
		float num6 = minSpeedDegPerSec;
		if (0f > minSpeedDegPerSec)
		{
			num6 = 0f;
		}
		float num7 = maxSpeedDegPerSec;
		minSpeedDegPerSec = num6;
		if (0f > maxSpeedDegPerSec)
		{
			num7 = 0f;
		}
		maxSpeedDegPerSec = num7;
		if (num6 > num7)
		{
			maxSpeedDegPerSec = num6;
		}
		int num8 = tickSegments;
		if (tickSegments < 1)
		{
			num8 = 1;
		}
		float num9 = tickSnapStrength;
		tickSegments = num8;
		if (!(0f > tickSnapStrength))
		{
			if (num9 > 1f)
			{
				num9 = 1f;
			}
		}
		else
		{
			num9 = 0f;
		}
		float num10 = burstDurationSeconds;
		tickSnapStrength = num9;
		if (0f > burstDurationSeconds)
		{
			num10 = 0f;
		}
		bool flag2 = rows == null;
		burstDurationSeconds = num10;
		Animator animator = default(Animator);
		if (!flag2)
		{
			List<RowSettings> list = rows;
			int num11 = 0;
			for (int num12 = 0; num12 < list._size; num12 = num11)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((object)animator == null)
				{
					RowSettings rowSettings = new RowSettings();
					rowSettings.count = 10;
					rowSettings.spacingAfter = 1f;
					rows.set_Item(num11, rowSettings);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v319 @ stack_18_v4+10]");
				bool flag3 = (nint)0 < (nint)0;
				object obj = 0;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v319 @ stack_18_v4+10]");
					obj = 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ stack_-38+14]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v252 @ stack_-38+14]");
				if ((nint)0 > (nint)0)
				{
					obj2 = 0;
				}
				num11++;
				list = rows;
			}
		}
		if (burstAnimatorBoolParameter == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v353 @ rax_v21+B8]");
			object obj4 = 0;
			burstAnimatorBoolParameter = (string)obj4;
		}
		if (burstTriggerMode == BurstTriggerMode.AnimatorBoolEdge && burstAnimator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			burstAnimator = animator;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 565 Invalid \"Jump target not found in method: 0x1803FA660\"");
		throw new NullReferenceException();
	}

	private void Update()
	{
		//IL_0168: Expected O, but got I4
		//IL_022e: Expected O, but got I4
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		ResolveAnimatorIfNeeded(force: false);
		RebuildIfNeeded(force: false);
		if (!(mesh != null) || !(material != null))
		{
			return;
		}
		if (burstTriggerMode == BurstTriggerMode.AnimatorBoolEdge)
		{
			if (useLocalTriggerBoolInsteadOfAnimatorParameter)
			{
				bool flag = !_localMirrorPrev;
				object obj = burstTriggerLocalBool & flag;
				if (obj != null)
				{
					TriggerRotationBurst();
				}
				_localMirrorPrev = burstTriggerLocalBool;
			}
			else
			{
				bool flag2 = ReadAnimatorBoolSafe();
				bool flag3 = _animParamPrev;
				bool flag4 = false;
				if (!flag3)
				{
					flag4 = flag2;
				}
				bool flag5 = !flag4;
				bool animParamPrev = flag2;
				if (!flag5)
				{
					TriggerRotationBurst();
					bool flag6 = !autoResetAnimatorBoolToFalse;
					animParamPrev = flag2;
					if (!flag6)
					{
						bool flag7 = burstAnimator != null;
						bool flag8 = !flag7;
						animParamPrev = flag2;
						if (!flag8)
						{
							bool flag9 = string.IsNullOrEmpty(burstAnimatorBoolParameter);
							animParamPrev = flag2;
							if (!flag9)
							{
								burstAnimator.SetBool(burstAnimatorBoolParameter, value: false);
								animParamPrev = false;
							}
						}
					}
				}
				_animParamPrev = animParamPrev;
			}
		}
		if (_burstActive)
		{
			StepBurstAndUpdateMatrices();
		}
		List<Matrix4x4[]> matrixBatches = _matrixBatches;
		Camera camera = null;
		Camera camera2 = null;
		Matrix4x4[] matrices = default(Matrix4x4[]);
		int count = default(int);
		MaterialPropertyBlock properties = default(MaterialPropertyBlock);
		ShadowCastingMode castShadows = default(ShadowCastingMode);
		bool flag10 = default(bool);
		int num = default(int);
		while ((nint)camera2 < matrixBatches._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Graphics.DrawMeshInstanced(mesh, 0, material, matrices, count, properties, castShadows, flag10, num, null, (LightProbeUsage)shadowCasting, (LightProbeProxyVolume)receiveShadows);
			matrixBatches = _matrixBatches;
			camera = (Camera)(camera + 1);
			camera2 = camera;
		}
	}

	private void PollLocalMirrorBoolAndTriggerIfNeeded()
	{
		//IL_0020: Expected O, but got I4
		bool flag = !_localMirrorPrev;
		object obj = burstTriggerLocalBool & flag;
		if (obj != null)
		{
			TriggerRotationBurst();
			_localMirrorPrev = burstTriggerLocalBool;
		}
		else
		{
			_localMirrorPrev = burstTriggerLocalBool;
		}
	}

	private void ResolveAnimatorIfNeeded(bool force)
	{
		if (burstTriggerMode == BurstTriggerMode.AnimatorBoolEdge && (force || burstAnimator == null) && burstAnimator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			Animator animator = default(Animator);
			burstAnimator = animator;
		}
	}

	private bool ReadAnimatorBoolSafe()
	{
		//IL_0092: Expected I4, but got O
		if (burstTriggerMode == BurstTriggerMode.AnimatorBoolEdge && burstAnimator != null && !string.IsNullOrEmpty(burstAnimatorBoolParameter))
		{
			if ((object)burstAnimator != null)
			{
				return burstAnimator.GetBool(burstAnimatorBoolParameter);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private void PollAnimatorBoolAndTriggerIfNeeded()
	{
		bool flag = ReadAnimatorBoolSafe();
		bool flag2 = _animParamPrev;
		bool flag3 = false;
		if (!flag2)
		{
			flag3 = flag;
		}
		bool flag4 = !flag3;
		bool animParamPrev = flag;
		if (!flag4)
		{
			TriggerRotationBurst();
			bool flag5 = !autoResetAnimatorBoolToFalse;
			animParamPrev = flag;
			if (!flag5)
			{
				bool flag6 = burstAnimator != null;
				bool flag7 = !flag6;
				animParamPrev = flag;
				if (!flag7)
				{
					bool flag8 = string.IsNullOrEmpty(burstAnimatorBoolParameter);
					animParamPrev = flag;
					if (!flag8)
					{
						burstAnimator.SetBool(burstAnimatorBoolParameter, value: false);
						animParamPrev = false;
					}
				}
			}
		}
		_animParamPrev = animParamPrev;
	}

	public void TriggerRotationBurst()
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		bool flag = !(0f < burstDurationSeconds);
		float burstRemaining = 0f;
		if (!flag)
		{
			burstRemaining = burstDurationSeconds;
		}
		if (_baseWorldPositions == null)
		{
			return;
		}
		Vector3[] baseWorldPositions = _baseWorldPositions;
		if (baseWorldPositions.Length != 0 && (!_burstActive || restartBurstIfTriggeredWhileActive))
		{
			int burstCount = _burstCount + 1;
			_burstCount = burstCount;
			_burstRemaining = burstRemaining;
			_burstActive = true;
			double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
			bool flag2 = !rerollSpeedAndDirectionOnBurst;
			_lastEditorTime = realtimeSinceStartupAsDouble;
			if (!flag2)
			{
				RerollSpeedAndDirectionForBurst();
			}
			RebuildMatricesFromCurrentState();
		}
	}

	public void TriggerRotationBurstWithDuration(float durationSeconds)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_001b: Expected F4, but got I4
		bool flag = !(0f < durationSeconds);
		float burstRemaining = 0f;
		if (!flag)
		{
			burstRemaining = durationSeconds;
		}
		if (_baseWorldPositions == null)
		{
			return;
		}
		Vector3[] baseWorldPositions = _baseWorldPositions;
		if (baseWorldPositions.Length != 0 && (!_burstActive || restartBurstIfTriggeredWhileActive))
		{
			int burstCount = _burstCount + 1;
			_burstCount = burstCount;
			_burstRemaining = burstRemaining;
			_burstActive = true;
			double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
			bool flag2 = !rerollSpeedAndDirectionOnBurst;
			_lastEditorTime = realtimeSinceStartupAsDouble;
			if (!flag2)
			{
				RerollSpeedAndDirectionForBurst();
			}
			RebuildMatricesFromCurrentState();
		}
	}

	private void DrawBatches()
	{
		//IL_005e: Expected O, but got I4
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		List<Matrix4x4[]> matrixBatches = _matrixBatches;
		Camera camera = null;
		Camera camera2 = null;
		Matrix4x4[] matrices = default(Matrix4x4[]);
		int count = default(int);
		MaterialPropertyBlock properties = default(MaterialPropertyBlock);
		ShadowCastingMode castShadows = default(ShadowCastingMode);
		bool flag = default(bool);
		int num = default(int);
		while ((nint)camera2 < matrixBatches._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Graphics.DrawMeshInstanced(mesh, 0, material, matrices, count, properties, castShadows, flag, num, null, (LightProbeUsage)shadowCasting, (LightProbeProxyVolume)receiveShadows);
			matrixBatches = _matrixBatches;
			camera = (Camera)(camera + 1);
			camera2 = camera;
		}
	}

	private void RebuildIfNeeded(bool force)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_00c2: Invalid comparison between F4 and I4
		//IL_00eb: Expected O, but got I4
		//IL_0244: Expected O, but got I4
		//IL_01cf: Expected O, but got I4
		//IL_01d8: Expected O, but got I4
		//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Expected O, but got Unknown
		//IL_09b2: Expected O, but got F4
		//IL_09e7: Expected O, but got F4
		//IL_0226: Expected O, but got I
		//IL_0a46: Expected O, but got I4
		//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Expected O, but got Unknown
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6f: Expected O, but got Unknown
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7c: Expected I4, but got Unknown
		//IL_0210: Expected O, but got I4
		//IL_031e: Expected O, but got I
		//IL_039c: Invalid comparison between F4 and I4
		//IL_03c5: Expected O, but got I4
		//IL_04c7: Expected O, but got I
		//IL_0545: Invalid comparison between F4 and I4
		//IL_056e: Expected O, but got I4
		Transform transform = base.transform;
		Vector3 position = transform.position;
		float num = (float)_lastPos - position.x;
		object obj = default(object);
		float num3 = default(float);
		float num2 = (float)obj - num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GPUGridInstancer_Animated)+16C]");
		object obj2 = 0 - position.z;
		float num4 = num2 * num2;
		float num5 = num * num;
		object obj3 = obj2 * obj2;
		float num6 = num4 + num5;
		float num7 = num6 + (float)obj3;
		bool flag = 9.9999994E-11f < num7;
		float num8 = 9.9999994E-11f - num7;
		bool flag2 = num8 == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		object obj4 = flag4 & flag3;
		if (obj4 != null)
		{
			Transform transform2 = base.transform;
			Quaternion rotation = transform2.rotation;
			float num9 = num3 * num3;
			float num10 = (float)_lastRot * rotation.x;
			float num11 = num9 + num10;
			float num12 = num3 * num3;
			float num13 = num3 * num3;
			float num14 = num11 + num12;
			num7 = num14 + num13;
			num5 = num3;
		}
		bool flag5 = rows == null;
		int num15 = 527;
		if (!flag5)
		{
			List<RowSettings> list = rows;
			num15 = 527 + list._size;
			object obj5 = 0;
			object obj6 = 0;
			object obj8 = default(object);
			float num16 = default(float);
			while (true)
			{
				object obj7 = obj6 - list._size;
				flag5 = obj7 == null;
				if ((nint)obj6 >= list._size)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj9;
				if (obj8 == null)
				{
					obj9 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ stack_20_v5+10]");
					obj9 = 0;
				}
				int hashCode = num16.GetHashCode();
				object obj10 = num15 * 31;
				obj5++;
				object obj11 = obj10 + obj9;
				object obj12 = obj11 * 31;
				num15 = obj12 + hashCode;
				list = rows;
				obj6 = obj5;
			}
		}
		object obj13 = !flag5;
		if (obj13 == null && _lastMesh == mesh && _lastMaterial == material)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj14 = default(object);
			if (obj14 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj15 = default(object);
				if (obj15 != null)
				{
					object obj16 = _lastBaseEulerRotation - baseEulerRotation;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GPUGridInstancer_Animated)+120]");
					nint num17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GPUGridInstancer_Animated)+54]");
					object obj17 = num17 - 0;
					float num18 = num3 - num3;
					float num19 = num18 * num18;
					object obj18 = obj16 * obj16;
					object obj19 = obj17 * obj17;
					float num20 = num19 + (float)obj18;
					float num21 = num20 + (float)obj19;
					bool flag6 = 9.9999994E-11f < num21;
					float num22 = 9.9999994E-11f - num21;
					bool flag7 = num22 == 0f;
					bool flag8 = !flag6;
					bool flag9 = !flag7;
					object obj20 = flag9 & flag8;
					if (obj20 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
						object obj21 = default(object);
						if (obj21 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
							object obj22 = default(object);
							if (obj22 != null && _lastSeed == randomSeed && _lastRandomizeStartPhase == randomizeStartPhase && _lastPivotPerInstance == pivotPerInstance)
							{
								object obj23 = _lastRotationAxisLocal - rotationAxisLocal;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GPUGridInstancer_Animated)+13C]");
								nint num23 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GPUGridInstancer_Animated)+84]");
								object obj24 = num23 - 0;
								float num24 = num3 - num3;
								float num25 = num24 * num24;
								object obj25 = obj23 * obj23;
								object obj26 = obj24 * obj24;
								float num26 = num25 + (float)obj25;
								float num27 = num26 + (float)obj26;
								bool flag10 = 9.9999994E-11f < num27;
								float num28 = 9.9999994E-11f - num27;
								bool flag11 = num28 == 0f;
								bool flag12 = !flag10;
								bool flag13 = !flag11;
								object obj27 = flag13 & flag12;
								if (obj27 != null && _lastRotationDirectionMode == rotationDirectionMode && _lastUseTickMotion == useTickMotion && _lastTickSegments == tickSegments && _lastTickEasing == tickEasing)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
									object obj28 = default(object);
									if (obj28 != null && _lastUseDeltaTimeIntegration == useDeltaTimeIntegration && _lastShadowCasting == shadowCasting && _lastReceiveShadows == receiveShadows && _lastLayer == layer && _lastLegacyRows == legacyRows && _lastLegacyColumns == legacyColumns)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
										object obj29 = default(object);
										if (obj29 != null && _lastRowsHash == num15 && _lastBurstTriggerMode == burstTriggerMode && _lastBurstAnimator == burstAnimator && _lastBurstAnimatorBoolParameter == burstAnimatorBoolParameter && _lastAutoResetAnimatorBoolToFalse == autoResetAnimatorBoolToFalse && _lastUseLocalTriggerBoolInsteadOfAnimatorParameter == useLocalTriggerBoolInsteadOfAnimatorParameter)
										{
											return;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		_lastMesh = mesh;
		_lastMaterial = material;
		_lastWidthSpacing = widthSpacing;
		_lastUniformScale = uniformScale;
		_lastBaseEulerRotation = baseEulerRotation;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GPUGridInstancer_Animated)+54]");
		_ = 0;
		_lastMinSpeed = minSpeedDegPerSec;
		_lastMaxSpeed = maxSpeedDegPerSec;
		_lastSeed = randomSeed;
		_lastRandomizeStartPhase = randomizeStartPhase;
		_lastPivotPerInstance = pivotPerInstance;
		_lastRotationAxisLocal = rotationAxisLocal;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GPUGridInstancer_Animated)+84]");
		_ = 0;
		_lastRotationDirectionMode = rotationDirectionMode;
		_lastUseTickMotion = useTickMotion;
		_lastTickSegments = tickSegments;
		_lastTickEasing = tickEasing;
		_lastTickSnapStrength = tickSnapStrength;
		_lastUseDeltaTimeIntegration = useDeltaTimeIntegration;
		_lastShadowCasting = shadowCasting;
		_lastReceiveShadows = receiveShadows;
		_lastLayer = layer;
		_lastLegacyRows = legacyRows;
		_lastLegacyColumns = legacyColumns;
		_lastLegacyRowSpacing = legacyRowSpacing;
		_lastBurstTriggerMode = burstTriggerMode;
		_lastRowsHash = num15;
		_lastBurstAnimator = burstAnimator;
		_lastBurstAnimatorBoolParameter = burstAnimatorBoolParameter;
		_lastAutoResetAnimatorBoolToFalse = autoResetAnimatorBoolToFalse;
		_lastUseLocalTriggerBoolInsteadOfAnimatorParameter = useLocalTriggerBoolInsteadOfAnimatorParameter;
		Transform transform3 = base.transform;
		Vector3 position2 = transform3.position;
		_lastPos = (Vector3)position2.x;
		_ = position2.z;
		Transform transform4 = base.transform;
		_lastRot = (Quaternion)transform4.rotation.x;
		BuildAllPreservingAnglesWhenPossible();
		bool animParamPrev = ReadAnimatorBoolSafe();
		_animParamPrev = animParamPrev;
		_localMirrorPrev = burstTriggerLocalBool;
	}

	private int ComputeRowsHash()
	{
		//IL_002b: Expected O, but got I4
		//IL_0034: Expected O, but got I4
		//IL_00c7: Expected I4, but got O
		//IL_00a1: Expected O, but got I
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_0118: Expected O, but got I4
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected I4, but got Unknown
		//IL_008c: Expected O, but got I4
		bool flag = rows == null;
		int num = 527;
		if (!flag)
		{
			List<RowSettings> list = rows;
			num = 527 + list._size;
			object obj = 0;
			object obj2 = 0;
			object obj3 = default(object);
			float num2 = default(float);
			while ((nint)obj < list._size)
			{
				if (rows != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					object obj4;
					if (obj3 == null)
					{
						obj4 = 0;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ stack_18_v4+10]");
						obj4 = 0;
					}
					int hashCode = num2.GetHashCode();
					obj2++;
					object obj5 = num * 31;
					object obj6 = obj5 + obj4;
					object obj7 = obj6 * 31;
					list = rows;
					num = obj7 + hashCode;
					if (rows != null)
					{
						obj = obj2;
						continue;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
		}
		return num;
	}

	private unsafe void BuildAllPreservingAnglesWhenPossible()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0152: Expected O, but got I
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Expected O, but got Unknown
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Expected O, but got Unknown
		//IL_0314: Expected O, but got I4
		//IL_035e: Expected I4, but got O
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Expected O, but got Unknown
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Expected O, but got Unknown
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_07f7: Expected O, but got Ref
		//IL_081f: Expected O, but got Ref
		//IL_0483: Expected F4, but got I4
		//IL_044c: Invalid comparison between I4 and F4
		//IL_045e: Expected F4, but got I4
		//IL_04f9: Expected O, but got I4
		//IL_0586: Expected I4, but got O
		//IL_061d: Expected F4, but got I
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Expected O, but got Unknown
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Expected O, but got Unknown
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Expected O, but got Unknown
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Expected O, but got Unknown
		//IL_0603: Expected F4, but got I4
		//IL_0632: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		List<Matrix4x4[]> matrixBatches = _matrixBatches;
		int version = matrixBatches._version + 1;
		matrixBatches._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj3 = default(object);
		if (obj3 == null)
		{
			matrixBatches._size = 0;
		}
		else
		{
			matrixBatches._size = 0;
			if (matrixBatches._size > 0)
			{
				Array.Clear(matrixBatches._items, 0, matrixBatches._size);
			}
		}
		List<int> batchCounts = _batchCounts;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<int>())
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		if (mesh != null && material != null)
		{
			int[] rowPlan = GetRowPlan(out var _);
			object obj4 = rowPlan + 32;
			Vector3[] array = null;
			Vector3[] array2 = null;
			Vector3[] array3 = null;
			while ((nint)array3 < rowPlan.Length)
			{
				bool flag = (nint)obj4 < 0;
				Vector3[] array4 = null;
				if (!flag)
				{
					array4 = (Vector3[])obj4;
				}
				array2 = (Vector3[])(array2 + 1);
				array = (Vector3[])(object)((object)array + (object)array4);
				obj4 += 4;
				array3 = array2;
			}
			float[] angleDegRaw = _angleDegRaw;
			if (_angleDegRaw == null)
			{
				_ = 0;
			}
			else
			{
				object obj5 = angleDegRaw.Length - array;
				bool flag2 = obj5 == null;
			}
			Vector3[] baseWorldPositions = new Vector3[(object)array];
			_baseWorldPositions = baseWorldPositions;
			float[] speedDegPerSec = new float[(object)array];
			_speedDegPerSec = speedDegPerSec;
			float[] dir = new float[(object)array];
			_dir = dir;
			float[] angleDegRaw2 = new float[(object)array];
			_angleDegRaw = angleDegRaw2;
			Transform transform = base.transform;
			Vector3 position = transform.position;
			Transform transform2 = base.transform;
			Quaternion rotation = transform2.rotation;
			int num2 = randomSeed;
			System.Random random = new System.Random(randomSeed);
			_ = 0;
			Vector3[] array5 = null;
			Vector3[] array6 = null;
			object obj6 = 32;
			float num3 = position.x;
			int[] array7 = rowPlan;
			Vector3[] array8 = null;
			Vector3[] array9 = null;
			Vector3[] array10 = null;
			object obj13 = default(object);
			object obj14 = default(object);
			while ((nint)array8 < array7.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v997 @ r14_v7 (System.Int32[])+v1064 @ r10_v6]");
				bool flag3 = (nint)0 < (nint)0;
				int num4 = (int)array5;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v997 @ r14_v7 (System.Int32[])+v1064 @ r10_v6]");
					num4 = 0;
				}
				if (num4 > 0)
				{
					object obj7 = array10 * 2;
					object obj8 = (object)array10 + obj7;
					object obj9 = array10 * 4;
					object obj10 = obj9 + 32;
					object obj11 = obj8 << 2;
					Vector3[] array11 = array5;
					float num5;
					bool flag6;
					do
					{
						Vector3[] baseWorldPositions2 = _baseWorldPositions;
						Quaternion quaternion = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
						_ = rotation.x;
						num5 = (float)array11 * widthSpacing;
						Vector3 vector = quaternion * (Vector3)(&num3);
						_ = vector.x;
						object obj12 = obj13 + obj14;
						float num6 = position.z + vector.z;
						double num7 = random.NextDouble();
						float[] speedDegPerSec2 = _speedDegPerSec;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
						float num8;
						if (0 <= 0)
						{
							bool flag4 = !(0f > 1f);
							num8 = 0f;
							if (!flag4)
							{
								num8 = 1f;
							}
						}
						else
						{
							num8 = 0f;
						}
						float num9 = maxSpeedDegPerSec - minSpeedDegPerSec;
						float num10 = num9 * num8;
						float num11 = num10 + minSpeedDegPerSec;
						float[] dir2 = _dir;
						bool flag5 = rotationDirectionMode == RotationDirectionMode.BothDirectionsRandom;
						if (!flag5)
						{
							object obj15 = rotationDirectionMode - 1;
							if (flag5)
							{
								goto IL_0569;
							}
							if ((nint)obj15 == 1)
							{
								goto IL_055b;
							}
						}
						if (random.Next(0, 2) == 0)
						{
							goto IL_055b;
						}
						goto IL_0569;
						IL_0569:
						float num12 = 1f;
						goto IL_0577;
						IL_055b:
						num12 = -1f;
						goto IL_0577;
						IL_0577:
						num2 = (int)_angleDegRaw;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+60]");
						if ((nint)0 == 0)
						{
							if (randomizeStartPhase)
							{
								double num13 = random.NextDouble();
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
								float num14 = 0f * 360f;
							}
							else
							{
								float num14 = 0f;
							}
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v898 @ rax_v24 (System.Single[])+v192 @ r14_v10]");
							float num14 = 0f;
						}
						array10 = (Vector3[])(array6 + 1);
						obj10 += 4;
						obj11 += 12;
						array11 = (Vector3[])(array11 + 1);
						Vector3[] array12 = array11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+78]");
						flag6 = (nint)array12 < 0;
						num3 = num5;
					}
					while (flag6);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+70]");
					array9 = (Vector3[])0;
					array5 = null;
					array6 = array10;
					num3 = num5;
					array7 = rowPlan;
				}
				array9 = (Vector3[])(array9 + 1);
				obj6 += 4;
				array8 = array9;
			}
			AllocateBatchesAndFillMatrices();
		}
		else
		{
			_baseWorldPositions = null;
			_speedDegPerSec = null;
			_dir = null;
			_angleDegRaw = null;
		}
	}

	private unsafe void AllocateBatchesAndFillMatrices()
	{
		//IL_00c7: Expected O, but got I4
		//IL_0170: Expected O, but got I
		//IL_0179: Expected O, but got I4
		//IL_01b1: Expected O, but got I4
		//IL_0200: Expected O, but got I4
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Expected I4, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_03f2: Expected O, but got Ref
		//IL_02a7: Expected O, but got F4
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_02eb: Expected O, but got F4
		//IL_0302: Expected O, but got F4
		//IL_0322: Expected O, but got Ref
		List<Matrix4x4[]> matrixBatches = _matrixBatches;
		int version = matrixBatches._version + 1;
		matrixBatches._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			matrixBatches._size = 0;
		}
		else
		{
			int size = matrixBatches._size;
			matrixBatches._size = 0;
			if (matrixBatches._size > 0)
			{
				Array.Clear(matrixBatches._items, 0, matrixBatches._size);
				Vector3 vector = (Vector3)0;
			}
		}
		List<int> batchCounts = _batchCounts;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<int>())
		{
			_ = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+18]");
			int size = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rbx_v4 (System.Collections.Generic.List`1<System.Int32>)+18]");
				Array.Clear((Array)num, 0, 0);
				Vector3 vector = (Vector3)0;
			}
		}
		if (_baseWorldPositions == null)
		{
			return;
		}
		Vector3[] baseWorldPositions = _baseWorldPositions;
		object obj2 = baseWorldPositions.Length;
		float num2 = uniformScale * (float)Vector3.oneVector;
		if (baseWorldPositions.Length <= 0)
		{
			return;
		}
		Vector3 vector2 = Vector3.oneVector;
		int num3 = 0;
		int num4 = 0;
		bool flag3;
		do
		{
			int num5;
			Matrix4x4[] array2;
			object obj3;
			int num6;
			Matrix4x4[] item;
			object obj4;
			if ((nint)obj2 > 1023)
			{
				Matrix4x4[] array = new Matrix4x4[1023];
				num5 = 0;
				array2 = array;
				obj3 = 1023;
				num6 = 0;
			}
			else
			{
				Matrix4x4[] array3 = new Matrix4x4[1023];
				bool flag = obj2 == null;
				num5 = 0;
				array2 = array3;
				obj3 = obj2;
				num6 = 0;
				item = array3;
				obj4 = obj2;
				if (flag)
				{
					goto IL_0327;
				}
			}
			object obj5 = array2 + 32;
			bool flag2;
			do
			{
				int idx = num3 + num6;
				Matrix4x4 matrix4x = BuildMatrixForIndex(idx, (Vector3)(&vector2));
				num6++;
				num5++;
				obj5 = matrix4x.m00;
				_ = matrix4x.m01;
				_ = matrix4x.m02;
				_ = matrix4x.m03;
				obj5 += 64;
				flag2 = num5 < (nint)obj3;
				vector2 = (Vector3)num2;
			}
			while (flag2);
			vector2 = (Vector3)num2;
			item = array2;
			obj4 = obj3;
			num4 = num3;
			Vector3 vector = (Vector3)(&vector2);
			goto IL_0327;
			IL_0327:
			_matrixBatches.Add(item);
			_batchCounts.Add((int)(&num3));
			num4 += obj4;
			obj2 -= obj4;
			flag3 = (nint)obj2 > 0;
			num3 = num4;
			int size = 0;
		}
		while (flag3);
	}

	private unsafe void RebuildMatricesFromCurrentState()
	{
		//IL_0221: Expected I, but got O
		//IL_001d: Expected O, but got I4
		//IL_0048: Expected O, but got I4
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_00c5: Expected O, but got I4
		//IL_00d9: Expected O, but got Ref
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_010c: Expected O, but got F4
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_0169: Expected O, but got I4
		//IL_0171: Expected O, but got F4
		//IL_01a4: Expected O, but got I4
		//IL_01ac: Expected O, but got F4
		if (_baseWorldPositions == null)
		{
			return;
		}
		Vector3[] baseWorldPositions = _baseWorldPositions;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		List<Matrix4x4[]> matrixBatches = _matrixBatches;
		float num3 = uniformScale;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		float num4 = num3 * 0f;
		float num5 = uniformScale * (float)Vector3.oneVector;
		object obj = 0;
		float num7 = default(float);
		float num6 = num7;
		int num8 = 0;
		float num9 = num5;
		Vector3 vector = Vector3.oneVector;
		object obj2 = 0;
		object obj3 = default(object);
		object obj5 = default(object);
		object obj7 = default(object);
		float num14 = default(float);
		while ((nint)obj2 < matrixBatches._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if ((nint)obj3 > 0)
			{
				object obj4 = obj5 + 32;
				float num10 = num6;
				int num11 = num8;
				float num12 = num9;
				object obj6 = obj7;
				float num13 = num14;
				object obj8 = 0;
				bool flag2;
				do
				{
					bool flag = num11 >= baseWorldPositions.Length;
					num6 = num10;
					num8 = num11;
					num9 = num12;
					obj7 = obj6;
					num14 = num13;
					object obj9 = obj4;
					if (flag)
					{
						break;
					}
					Matrix4x4 matrix4x = BuildMatrixForIndex(num11, (Vector3)(&vector));
					num8 = num11 + 1;
					obj8++;
					obj4 = matrix4x.m00;
					_ = matrix4x.m01;
					num9 = matrix4x.m02;
					_ = matrix4x.m02;
					num6 = matrix4x.m03;
					_ = matrix4x.m03;
					obj9 = obj4 + 64;
					flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
					obj7 = 0;
					vector = (Vector3)num5;
					num14 = num4;
					num10 = matrix4x.m03;
					num11 = num8;
					num12 = matrix4x.m02;
					obj6 = 0;
					vector = (Vector3)num5;
					num13 = num4;
					obj4 = obj9;
				}
				while (flag2);
			}
			matrixBatches = _matrixBatches;
			obj++;
			obj2 = obj;
		}
	}

	private void StepBurstAndUpdateMatrices()
	{
		//IL_02d4: Invalid comparison between I4 and F4
		//IL_02fe: Expected F4, but got I4
		//IL_007c: Expected F4, but got I4
		//IL_0267: Invalid comparison between I4 and F4
		//IL_0155: Expected O, but got I4
		//IL_015e: Expected O, but got I4
		//IL_019f: Expected O, but got I
		//IL_01ea: Invalid comparison between I and F4
		//IL_023c: Expected F4, but got I
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Expected O, but got Unknown
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_020f: Invalid comparison between F4 and I
		float num;
		if (useDeltaTimeIntegration)
		{
			bool isPlaying = Application.isPlaying;
			if (isPlaying)
			{
				goto IL_02b5;
			}
			double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rbx+0F8h]\"");
			_lastEditorTime = realtimeSinceStartupAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,xmm1\"");
			if ((isPlaying ? 1 : 0) > (false ? 1 : 0))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm1\"");
				num = 0f;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm0\"");
				if ((isPlaying ? 1 : 0) > (false ? 1 : 0))
				{
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm1\"");
				num = 0f;
			}
		}
		else
		{
			if (Application.isPlaying)
			{
				goto IL_02b5;
			}
			num = 1f / 60f;
		}
		goto IL_02cb;
		IL_02b5:
		float deltaTime = Time.deltaTime;
		num = deltaTime;
		goto IL_02cb;
		IL_02cb:
		if (!(0f < num))
		{
			return;
		}
		if (num > 0.05f)
		{
			num = 0.05f;
		}
		if (!(_burstRemaining > num))
		{
			num = _burstRemaining;
		}
		Vector3[] baseWorldPositions = _baseWorldPositions;
		float burstRemaining = _burstRemaining - num;
		_burstRemaining = burstRemaining;
		if (baseWorldPositions.Length > 0)
		{
			object obj = 32;
			object obj2 = 0;
			do
			{
				float[] angleDegRaw = _angleDegRaw;
				float[] dir = _dir;
				float[] speedDegPerSec = _speedDegPerSec;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ rdi_v7+v370 @ rcx_v11 (System.Single[])]");
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ rdi_v7+v347 @ rdx_v7 (System.Single[])]");
				object obj3 = num2 * 0;
				float num3 = (float)obj3 * num;
				float num4 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ rdi_v7+v366 @ rax_v12 (System.Single[])]");
				float num5 = num4 + 0f;
				float[] angleDegRaw2 = _angleDegRaw;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ rdi_v7+v367 @ rax_v13 (System.Single[])]");
				if (!(0f > 360f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ rdi_v7+v367 @ rax_v13 (System.Single[])]");
					if (!(-360f > 0f))
					{
						goto IL_0321;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ rdi_v7+v367 @ rax_v13 (System.Single[])]");
				float num6 = Mathf.Repeat(0f, 360f);
				float[] angleDegRaw3 = _angleDegRaw;
				goto IL_0321;
				IL_0321:
				obj2++;
				obj += 4;
			}
			while ((nint)obj2 < baseWorldPositions.Length);
		}
		RebuildMatricesFromCurrentState();
		if (!(0f < _burstRemaining))
		{
			_burstRemaining = 0f;
			_burstActive = false;
		}
	}

	private float EvaluateTickAngle(float rawAngleDeg)
	{
		//IL_0090: Invalid comparison between I4 and F4
		//IL_00db: Expected F4, but got I4
		//IL_022b: Invalid comparison between F4 and I4
		//IL_01ac: Invalid comparison between I4 and F4
		//IL_01f7: Expected F4, but got I4
		if (useTickMotion && tickSegments > 1)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,dword ptr [rcx+9Ch]\"");
			float num = 360f / 0f;
			float num2 = rawAngleDeg / num;
			float num3 = MathF.Floor(num2);
			float num4 = num2 - num3;
			if (!(0f > num4))
			{
				if (num4 > 1f)
				{
					num4 = 1f;
				}
			}
			else
			{
				num4 = 0f;
			}
			float num7;
			if (tickEasing != TickEasing.Smoothstep && tickEasing == TickEasing.SineInOut)
			{
				float num5 = num4 * (float)Math.PI;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
				float num6 = num5 * 0.5f;
				num7 = 0.5f - num6;
			}
			else
			{
				float num8 = num4 + num4;
				float num9 = num4 * num4;
				float num10 = 3f - num8;
				num7 = num10 * num9;
			}
			if (tickSnapStrength > 0f)
			{
				float num11 = num7 * num7;
				float num12 = num11 * tickSnapStrength;
				if (!(0f > num12))
				{
					if (num12 > 1f)
					{
						num12 = 1f;
					}
				}
				else
				{
					num12 = 0f;
				}
				float num13 = 1f - num7;
				float num14 = num13 * num12;
				float num15 = num14 + num7;
				num7 = num15;
			}
			float num16 = num7 + num3;
			return num16 * num;
		}
		return rawAngleDeg;
	}

	private float ApplyTickEasing(float t)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_005c: Expected F4, but got I4
		float num;
		if (!(0f > t))
		{
			bool flag = !(t > 1f);
			num = t;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		if (tickEasing != TickEasing.Smoothstep && tickEasing == TickEasing.SineInOut)
		{
			float num2 = num * (float)Math.PI;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
			float num3 = num2 * 0.5f;
			return 0.5f - num3;
		}
		float num4 = num + num;
		float num5 = num * num;
		float num6 = 3f - num4;
		return num6 * num5;
	}

	private float PickDirection(System.Random rng)
	{
		//IL_002f: Expected O, but got I4
		bool flag = rotationDirectionMode == RotationDirectionMode.BothDirectionsRandom;
		if (!flag)
		{
			object obj = rotationDirectionMode - 1;
			if (flag)
			{
				goto IL_0097;
			}
			if ((nint)obj == 1)
			{
				goto IL_005d;
			}
		}
		if (rng.Next(0, 2) == 0)
		{
			goto IL_005d;
		}
		goto IL_0097;
		IL_0097:
		return 1f;
		IL_005d:
		return -1f;
	}

	private void RerollSpeedAndDirectionForBurst()
	{
		//IL_007f: Expected O, but got I4
		//IL_008f: Expected O, but got I4
		//IL_009c: Expected I4, but got O
		//IL_00ba: Expected O, but got I4
		//IL_00cc: Expected O, but got I4
		//IL_00d5: Expected O, but got I4
		//IL_015a: Expected F4, but got I4
		//IL_0123: Invalid comparison between I4 and F4
		//IL_0135: Expected F4, but got I4
		//IL_0213: Expected I, but got O
		//IL_0223: Expected O, but got I
		//IL_0265: Expected O, but got I
		//IL_01d0: Expected O, but got I4
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		if (_speedDegPerSec == null || _dir == null || (_burstActive && !restartBurstIfTriggeredWhileActive))
		{
			return;
		}
		int seed = default(int);
		System.Random random = new System.Random(seed);
		object obj = randomSeed * 486187739;
		object obj2 = _burstCount * 16777619;
		seed = (int)(obj + obj2);
		float[] speedDegPerSec = _speedDegPerSec;
		object obj3 = 32;
		int num = 0;
		object obj4 = 0;
		object obj8 = default(object);
		for (object obj5 = 0; (nint)obj4 < speedDegPerSec.Length; obj5++, speedDegPerSec = _speedDegPerSec, obj3 += 4, obj4 = obj5)
		{
			double num2 = random.NextDouble();
			float[] speedDegPerSec2 = _speedDegPerSec;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
			float num3;
			if (0 <= 0)
			{
				bool flag = !(0f > 1f);
				num3 = 0f;
				if (!flag)
				{
					num3 = 1f;
				}
			}
			else
			{
				num3 = 0f;
			}
			float num4 = maxSpeedDegPerSec - minSpeedDegPerSec;
			float num5 = num4 * num3;
			float num6 = num5 + minSpeedDegPerSec;
			float[] dir = _dir;
			bool flag2 = rotationDirectionMode == RotationDirectionMode.BothDirectionsRandom;
			object obj7;
			int num7;
			if (!flag2)
			{
				object obj6 = rotationDirectionMode - 1;
				if (flag2)
				{
					goto IL_029a;
				}
				bool flag3 = (nint)obj6 == 1;
				obj7 = obj8;
				num7 = num;
				if (flag3)
				{
					goto IL_027c;
				}
			}
			nint num8 = (nint)random;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v445 @ r9_v8 (Il2CppClass<System.Random>)+1A0]");
			obj7 = 0;
			int num9 = random.Next(0, 2);
			bool flag4 = num9 != 0;
			num7 = 2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v445 @ r9_v8 (Il2CppClass<System.Random>)+1A0]");
			obj8 = 0;
			num = 2;
			if (!flag4)
			{
				goto IL_027c;
			}
			goto IL_029a;
			IL_029a:
			float num10 = 1f;
			continue;
			IL_027c:
			obj8 = obj7;
			num10 = -1f;
			num = num7;
		}
	}

	private unsafe Matrix4x4 BuildMatrixForIndex(int idx, Vector3 scale)
	{
		//IL_00c0: Expected O, but got I
		//IL_0130: Expected O, but got Ref
		//IL_0130: Expected O, but got Ref
		//IL_01f9: Invalid comparison between I4 and F4
		//IL_0244: Expected F4, but got I4
		//IL_0418: Expected O, but got F4
		//IL_051e: Expected native int or pointer, but got O
		//IL_0530: Expected native int or pointer, but got O
		//IL_0542: Expected native int or pointer, but got O
		//IL_0554: Expected native int or pointer, but got O
		//IL_03e8: Expected O, but got Ref
		//IL_03e8: Expected O, but got Ref
		//IL_03f9: Expected O, but got F4
		//IL_0406: Expected O, but got F4
		//IL_04aa: Invalid comparison between F4 and I4
		//IL_0315: Invalid comparison between I4 and F4
		//IL_0360: Expected F4, but got I4
		Transform transform = base.transform;
		Quaternion rotation = transform.rotation;
		Vector3 euler = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		float num2 = default(float);
		float num = num2 * num2;
		float num3 = num2 * num2;
		object obj = (object)rotationAxisLocal * (object)rotationAxisLocal;
		float num4 = num3 + (float)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (GPUGridInstancer_Animated)+84]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (GPUGridInstancer_Animated)+84]");
		object obj2 = num5 * 0;
		float num6 = num4 + (float)obj2;
		bool flag = !(1E-06f > num6);
		Vector3 s = baseEulerRotation;
		euler = rotationAxisLocal;
		float num7 = 1E-06f;
		if (!flag)
		{
			s = Vector3.upVector;
			euler = Vector3.upVector;
			num7 = num2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (!(num7 > 1E-05f))
		{
		}
		Transform transform2 = base.transform;
		Quaternion rotation2 = transform2.rotation;
		object obj3 = default(object);
		Vector3 vector = (Quaternion)(&obj3) * (Vector3)(&euler);
		float[] angleDegRaw = _angleDegRaw;
		if (idx < angleDegRaw.Length)
		{
			float angle;
			if (useTickMotion && tickSegments > 1)
			{
				float num8 = 360f / (float)tickSegments;
				float num9 = angleDegRaw[idx] / num8;
				float num10 = MathF.Floor(num9);
				float num11 = num9 - num10;
				if (!(0f > num11))
				{
					if (num11 > 1f)
					{
						num11 = 1f;
					}
				}
				else
				{
					num11 = 0f;
				}
				float num14;
				if (tickEasing != TickEasing.Smoothstep && tickEasing == TickEasing.SineInOut)
				{
					float num12 = num11 * (float)Math.PI;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
					float num13 = num12 * 0.5f;
					num14 = 0.5f - num13;
				}
				else
				{
					float num15 = num11 + num11;
					float num16 = num11 * num11;
					float num17 = 3f - num15;
					num14 = num17 * num16;
				}
				if (tickSnapStrength > 0f)
				{
					float num18 = num14 * num14;
					float num19 = num18 * tickSnapStrength;
					if (!(0f > num19))
					{
						if (num19 > 1f)
						{
							num19 = 1f;
						}
					}
					else
					{
						num19 = 0f;
					}
					float num20 = 1f - num14;
					float num21 = num20 * num19;
					float num22 = num21 + num14;
					num14 = num22;
				}
				float num23 = num14 + num10;
				angle = num23 * num8;
			}
			else
			{
				angle = angleDegRaw[idx];
			}
			Quaternion quaternion2 = Quaternion.Internal_AngleAxis(angle, ref euler);
			Transform transform3 = base.transform;
			Vector3 position = transform3.position;
			Vector3[] baseWorldPositions = _baseWorldPositions;
			if (idx < baseWorldPositions.Length)
			{
				Quaternion q = default(Quaternion);
				if (!pivotPerInstance)
				{
					float num24 = default(float);
					Vector3 vector2 = (Quaternion)(&num24) * (Vector3)(&euler);
					q = (Quaternion)quaternion2.x;
					euler = (Vector3)vector2.x;
				}
				else
				{
					euler = (Vector3)vector.x;
				}
				Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref euler, ref q, ref s);
				Matrix4x4 matrix4x2 = default(Matrix4x4);
				((Matrix4x4*)(nint)matrix4x2)->m00 = matrix4x.m00;
				((Matrix4x4*)(nint)matrix4x2)->m01 = matrix4x.m01;
				((Matrix4x4*)(nint)matrix4x2)->m02 = matrix4x.m02;
				((Matrix4x4*)(nint)matrix4x2)->m03 = matrix4x.m03;
				return matrix4x2;
			}
		}
		return (Matrix4x4)new IndexOutOfRangeException();
	}

	private float GetDeltaTimeSeconds()
	{
		if (useDeltaTimeIntegration)
		{
			if (!Application.isPlaying)
			{
				double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,qword ptr [rbx+0F8h]\"");
				_lastEditorTime = realtimeSinceStartupAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"maxsd xmm1,xmm2\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"minsd xmm0,xmm1\"");
				return 0.1f;
			}
		}
		else if (!Application.isPlaying)
		{
			return 1f / 60f;
		}
		return Time.deltaTime;
	}

	private unsafe int[] GetRowPlan(out float[] rowStartZ)
	{
		//IL_02b0: Expected O, but got I4
		//IL_02b9: Expected O, but got I4
		//IL_02c2: Expected O, but got I4
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_00c0: Expected O, but got I4
		//IL_00d2: Expected F4, but got I4
		//IL_0382: Invalid comparison between I4 and F4
		//IL_01c7: Expected F4, but got I4
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		int[] array;
		ref float[] reference;
		if (rows != null)
		{
			List<RowSettings> list = rows;
			if (list._size > 0)
			{
				array = new int[list._size];
				List<RowSettings> list2 = rows;
				if (rows != null)
				{
					float[] array2 = new float[list2._size];
					reference = ref *(float[]*)array2;
					List<RowSettings> list3 = rows;
					if (rows != null)
					{
						object obj = 32;
						int num = 0;
						float num2 = 0f;
						int num3 = 0;
						RowSettings rowSettings = default(RowSettings);
						while (num3 < list3._size)
						{
							if (rows != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								bool flag = rowSettings != null;
								RowSettings rowSettings2 = rowSettings;
								if (!flag)
								{
									RowSettings rowSettings3 = new RowSettings();
									rowSettings3.count = 10;
									rowSettings3.spacingAfter = 1f;
									bool flag2 = rowSettings3 == null;
									rowSettings2 = rowSettings3;
									if (flag2)
									{
										goto IL_0359;
									}
								}
								bool flag3 = rowSettings2.count < 0;
								int num4 = 0;
								if (!flag3)
								{
									num4 = rowSettings2.count;
								}
								float num5 = rowSettings2.spacingAfter;
								if (0f > rowSettings2.spacingAfter)
								{
									num5 = 0f;
								}
								if (array != null && rowStartZ != null)
								{
									num++;
									list3 = rows;
									num2 += num5;
									obj += 4;
									if (rows != null)
									{
										num3 = num;
										continue;
									}
								}
							}
							goto IL_0359;
						}
						goto IL_03bd;
					}
				}
				goto IL_0359;
			}
		}
		array = new int[legacyRows];
		float[] array3 = new float[legacyRows];
		reference = ref *(float[]*)array3;
		if (legacyRows > 0)
		{
			object obj2 = 0;
			object obj3 = 32;
			object obj4 = 0;
			while (array != null)
			{
				_ = legacyColumns;
				if (rowStartZ == null)
				{
					break;
				}
				obj4++;
				obj2 += legacyRowSpacing;
				obj3 += 4;
				if ((nint)obj4 < legacyRows)
				{
					continue;
				}
				goto IL_03bd;
			}
			goto IL_0359;
		}
		goto IL_03bd;
		IL_0359:
		return (int[])(object)new NullReferenceException();
		IL_03bd:
		return array;
	}

	public GPUGridInstancer_Animated()
	{
		//IL_008d: Expected I, but got O
		//IL_00ff: Expected I, but got O
		List<RowSettings> list = new List<RowSettings>();
		rows = list;
		legacyRows = 10;
		legacyColumns = 10;
		legacyRowSpacing = 1f;
		widthSpacing = 1f;
		uniformScale = 1f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		baseEulerRotation = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v6 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		burstTriggerMode = BurstTriggerMode.AnimatorBoolEdge;
		burstAnimatorBoolParameter = "TriggerSpin";
		useLocalTriggerBoolInsteadOfAnimatorParameter = true;
		burstDurationSeconds = 1f;
		restartBurstIfTriggeredWhileActive = true;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rax_v11 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		rotationAxisLocal = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		minSpeedDegPerSec = 30f;
		maxSpeedDegPerSec = 120f;
		randomSeed = 12345;
		randomizeStartPhase = true;
		useTickMotion = true;
		tickSegments = 60;
		tickSnapStrength = 0.6f;
		useDeltaTimeIntegration = true;
		shadowCasting = ShadowCastingMode.On;
		receiveShadows = true;
		List<Matrix4x4[]> matrixBatches = new List<Matrix4x4[]>();
		_matrixBatches = matrixBatches;
		_batchCounts = new List<int>();
		base._002Ector();
	}
}
