using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MapMarkerLineUI : MonoBehaviour, IFloatValueProvider
{
	[Serializable]
	public class DistanceAngleEvent : UnityEvent<float, float>
	{
	}

	public Line line;

	private List<Line> additionalLines;

	public Disc disc;

	private List<Disc> additionalDiscs;

	private float discRadiusMultiplier;

	private float discRadiusMin;

	private float discRadiusMax;

	private bool driveDiscThicknessFromRadius;

	private float discThicknessFraction;

	public Transform pointerTip;

	private bool rotatePointerTip;

	private float pointerRotationOffsetDegrees;

	private bool hidePointerTipUntilDragged;

	public Transform faceDragDirectionTarget;

	private bool rotateFaceDragDirectionTarget;

	private float faceDragDirectionRotationOffsetDegrees;

	public TMP_Text angleLabel;

	public TMP_Text distanceLabel;

	public bool hideLabelsUntilDragged;

	public float minimumDragDistance;

	public GameObject placementTooltip;

	public bool hidePlacementTooltipOnDrag;

	private bool allowNoteLogging;

	private float speedNormalizationRange;

	private bool useUnscaledTime;

	private bool resetSpeedOnFinalize;

	private float inspectorRawSpeedUnitsPerSecond;

	private float inspectorNormalizedSpeed;

	public DistanceAngleEvent onDragProgress;

	public DistanceAngleEvent onMinimumDragDistanceReached;

	public DistanceAngleEvent onPlacementFinalized;

	private RectTransform markerRectTransform;

	private float _003CAngleValue_003Ek__BackingField;

	private float _003CDistanceValue_003Ek__BackingField;

	private Vector2 _003COriginLocal_003Ek__BackingField;

	private Vector3 _003CTipLocalPosition_003Ek__BackingField;

	private bool _003CHasReachedMinimumDragDistance_003Ek__BackingField;

	private float _003CNormalizedMarkerSpeed_003Ek__BackingField;

	private bool placementTooltipFinalized;

	private bool placementEventFired;

	private bool minimumDistanceEventFired;

	private Vector3 previousTipLocalPosition;

	private bool hasPreviousTipPosition;

	private bool isDragging;

	public bool AllowNoteLogging => allowNoteLogging;

	public float AngleValue
	{
		get
		{
			return _003CAngleValue_003Ek__BackingField;
		}
		private set
		{
			_003CAngleValue_003Ek__BackingField = value;
		}
	}

	public float DistanceValue
	{
		get
		{
			return _003CDistanceValue_003Ek__BackingField;
		}
		private set
		{
			_003CDistanceValue_003Ek__BackingField = value;
		}
	}

	public Vector2 OriginLocal
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
		private set
		{
			_003COriginLocal_003Ek__BackingField = value;
		}
	}

	public unsafe Vector3 TipLocalPosition
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)_003CTipLocalPosition_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MapMarkerLineUI)+F0]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		private set
		{
			//IL_000f: Expected O, but got F4
			_003CTipLocalPosition_003Ek__BackingField = (Vector3)value.x;
			_ = value.z;
		}
	}

	public string AngleLabelText
	{
		get
		{
			//IL_005f: Expected I, but got O
			if (angleLabel != null)
			{
				TMP_Text tMP_Text = angleLabel;
				if ((object)angleLabel == null)
				{
					return (string)(object)new NullReferenceException();
				}
				nint num = (nint)tMP_Text;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v77 @ rdx_v4 (Il2CppClass<TMPro.TMP_Text>)+548] (should have been resolved before IL gen)");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			return $"{arg:F1}°";
		}
	}

	public string DistanceLabelText
	{
		get
		{
			//IL_005f: Expected I, but got O
			if (distanceLabel != null)
			{
				TMP_Text tMP_Text = distanceLabel;
				if ((object)distanceLabel == null)
				{
					return (string)(object)new NullReferenceException();
				}
				nint num = (nint)tMP_Text;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v77 @ rdx_v4 (Il2CppClass<TMPro.TMP_Text>)+548] (should have been resolved before IL gen)");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			return $"{arg:F2}km";
		}
	}

	public bool HasReachedMinimumDragDistance
	{
		get
		{
			return _003CHasReachedMinimumDragDistance_003Ek__BackingField;
		}
		private set
		{
			_003CHasReachedMinimumDragDistance_003Ek__BackingField = value;
		}
	}

	public float NormalizedMarkerSpeed
	{
		get
		{
			return _003CNormalizedMarkerSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CNormalizedMarkerSpeed_003Ek__BackingField = value;
		}
	}

	public float GetFloatValue()
	{
		return _003CNormalizedMarkerSpeed_003Ek__BackingField;
	}

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		RectTransform rectTransform = default(RectTransform);
		markerRectTransform = rectTransform;
		if (hideLabelsUntilDragged)
		{
			if (angleLabel != null)
			{
				GameObject gameObject = angleLabel.gameObject;
				if (gameObject.activeSelf)
				{
					GameObject gameObject2 = angleLabel.gameObject;
					gameObject2.SetActive(value: false);
				}
			}
			if (distanceLabel != null)
			{
				GameObject gameObject3 = distanceLabel.gameObject;
				if (gameObject3.activeSelf)
				{
					GameObject gameObject4 = distanceLabel.gameObject;
					gameObject4.SetActive(value: false);
				}
			}
		}
		if (placementTooltip != null && !placementTooltipFinalized)
		{
			placementTooltip.SetActive(value: true);
		}
		if (pointerTip != null && hidePointerTipUntilDragged)
		{
			GameObject gameObject5 = pointerTip.gameObject;
			gameObject5.SetActive(value: false);
		}
		_003CNormalizedMarkerSpeed_003Ek__BackingField = 0f;
		inspectorRawSpeedUnitsPerSecond = 0f;
		_003CHasReachedMinimumDragDistance_003Ek__BackingField = false;
	}

	public void Initialize(Vector2 originLocal, RectTransform mapRect)
	{
		placementTooltipFinalized = false;
		_003CNormalizedMarkerSpeed_003Ek__BackingField = 0f;
		inspectorRawSpeedUnitsPerSecond = 0f;
		minimumDistanceEventFired = false;
		_003CHasReachedMinimumDragDistance_003Ek__BackingField = false;
		hasPreviousTipPosition = false;
		_003COriginLocal_003Ek__BackingField = originLocal;
		UpdateLine(originLocal, originLocal, mapRect);
	}

	public unsafe void UpdateLine(Vector2 originLocal, Vector2 targetLocal, RectTransform mapRect)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0039: Expected O, but got Ref
		//IL_00a7: Invalid comparison between F4 and I4
		//IL_09b9: Expected I, but got O
		//IL_09d9: Expected F4, but got I
		//IL_09fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ff: Expected O, but got Unknown
		//IL_0a46: Expected O, but got Ref
		//IL_02df: Expected F4, but got O
		//IL_0114: Invalid comparison between I4 and F4
		//IL_02f7: Expected F4, but got O
		//IL_0313: Expected O, but got Ref
		//IL_0a59: Expected I, but got O
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7f: Expected O, but got Unknown
		//IL_0a95: Expected O, but got I
		//IL_03fb: Expected O, but got Ref
		//IL_01fc: Invalid comparison between F4 and I4
		//IL_028a: Expected F4, but got I4
		//IL_022d: Invalid comparison between I4 and F4
		//IL_052d: Expected O, but got Ref
		//IL_05c8: Expected O, but got Ref
		//IL_0600: Expected I, but got O
		//IL_06ca: Expected O, but got Ref
		//IL_0864: Invalid comparison between F4 and I4
		//IL_07c6: Expected O, but got I4
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Expected I4, but got Unknown
		//IL_08c4: Expected F4, but got Ref
		//IL_095d: Expected F4, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (!(markerRectTransform != null))
		{
			return;
		}
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 95));
		object obj5 = default(object);
		object obj6 = default(object);
		object obj4 = obj5 - obj6;
		_003COriginLocal_003Ek__BackingField = originLocal;
		float num = (float)targetLocal - (float)originLocal;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
		object obj7 = obj4 * obj4;
		float num2 = num * num;
		float num3 = (float)obj7 + num2;
		float num4;
		Vector2 vector = default(Vector2);
		if (!(num3 > 0f))
		{
			num4 = _003CAngleValue_003Ek__BackingField;
		}
		else
		{
			nint num5 = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v348 @ rax_v82 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ rcx_v68 (Il2CppStaticFields<UnityEngine.Vector2>)+14]");
			num3 = 0f;
			float num7 = Vector2.SignedAngle(vector, vector);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj8 = num7 ^ 0;
			float x = (float)obj8 + 360f;
			float num8 = MathF.FMod(x, 360f);
			num4 = num8;
		}
		_003CAngleValue_003Ek__BackingField = num4;
		_003CTipLocalPosition_003Ek__BackingField = vector;
		_ = 0;
		float num9 = default(float);
		_003CDistanceValue_003Ek__BackingField = num9;
		float num17;
		float num18;
		if (hasPreviousTipPosition)
		{
			float num10 = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
			if (0f < num10)
			{
				nint num11 = (nint)typeof(Math);
				float num12 = num - (float)previousTipLocalPosition;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-7D]");
				object obj9 = obj4 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapMarkerLineUI)+108]");
				object obj10 = -0;
				object obj11 = obj9 * obj9;
				previousTipLocalPosition = vector;
				float num13 = num12 * num12;
				object obj12 = obj10 * obj10;
				float num14 = (float)obj11 + num13;
				_ = 0;
				float num15 = num14 + (float)obj12;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rcx_v65 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
				}
				else
				{
					double num16 = Math.Sqrt(num15);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				num17 = 0f / num10;
				if (speedNormalizationRange > 0f)
				{
					num18 = num17 / speedNormalizationRange;
					if (!(0f > num18))
					{
						bool flag = !(num18 > 1f);
						num3 = 1f;
						if (!flag)
						{
							num18 = 1f;
							num3 = 1f;
						}
						goto IL_0a9a;
					}
				}
				num18 = 0f;
				goto IL_0a9a;
			}
		}
		else
		{
			hasPreviousTipPosition = true;
			previousTipLocalPosition = vector;
			_ = 0;
			_003CNormalizedMarkerSpeed_003Ek__BackingField = 0f;
			inspectorRawSpeedUnitsPerSecond = 0f;
		}
		goto IL_0a2f;
		IL_0a2f:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapMarkerLineUI)+F0]");
		_ = 0;
		Vector3 euler = default(Vector3);
		DriveLines((Vector3)(&euler));
		bool flag2 = pointerTip != null;
		float num19 = (float)_003CTipLocalPosition_003Ek__BackingField;
		if (flag2)
		{
			num19 = (float)_003CTipLocalPosition_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapMarkerLineUI)+F0]");
			_ = 0;
			pointerTip.localPosition = (Vector3)(&euler);
			if (rotatePointerTip)
			{
				object obj13 = obj4 * obj4;
				num19 = num * num;
				float num20 = (float)obj13 + num19;
				if (num20 > 1E-06f)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
					float num21 = (float)obj4 * 57.29578f;
					float num22 = num21 - 90f;
					float num23 = num22 + pointerRotationOffsetDegrees;
					float num24 = num23 * ((float)Math.PI / 180f);
					num19 = Quaternion.Internal_FromEulerRad(ref euler).x;
					pointerTip.localRotation = (Quaternion)(&euler);
				}
			}
		}
		DriveDiscs(num9);
		bool flag3 = faceDragDirectionTarget != null;
		float num25 = num9;
		if (flag3)
		{
			bool flag4 = !rotateFaceDragDirectionTarget;
			num25 = num9;
			if (!flag4)
			{
				object obj14 = obj4 * obj4;
				num3 = num * num;
				float num26 = (float)obj14 + num3;
				bool flag5 = !(num26 > 1E-06f);
				num25 = num9;
				if (!flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
					float num27 = (float)obj4 * 57.29578f;
					float num28 = num27 - 90f;
					float num29 = num28 + faceDragDirectionRotationOffsetDegrees;
					float num30 = num29 * ((float)Math.PI / 180f);
					num19 = Quaternion.Internal_FromEulerRad(ref euler).x;
					faceDragDirectionTarget.localRotation = (Quaternion)(&euler);
					num25 = num;
				}
			}
		}
		bool flag6 = num9 < minimumDragDistance;
		bool flag7 = !flag6;
		bool flag8 = !hideLabelsUntilDragged;
		bool flag9 = flag8 | flag7;
		if (angleLabel != null)
		{
			if (flag9)
			{
				object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 95));
				TMP_Text tMP_Text = angleLabel;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string text = $"{arg:F1}°";
				nint num31 = (nint)tMP_Text;
				tMP_Text.text = text;
			}
			GameObject gameObject = angleLabel.gameObject;
			bool activeSelf = gameObject.activeSelf;
			if (activeSelf != flag9)
			{
				GameObject gameObject2 = angleLabel.gameObject;
				gameObject2.SetActive(flag9);
			}
		}
		if (distanceLabel != null)
		{
			if (flag9)
			{
				object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 95));
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string text2 = $"{arg2:F2}km";
				distanceLabel.text = text2;
			}
			GameObject gameObject3 = distanceLabel.gameObject;
			bool activeSelf2 = gameObject3.activeSelf;
			if (activeSelf2 != flag9)
			{
				GameObject gameObject4 = distanceLabel.gameObject;
				gameObject4.SetActive(flag9);
			}
		}
		if (placementTooltip != null && !placementTooltipFinalized)
		{
			object obj17 = hidePlacementTooltipOnDrag & flag7;
			bool active = (byte)(obj17 ^ 1) != 0;
			placementTooltip.SetActive(active);
		}
		if (pointerTip != null && hidePointerTipUntilDragged)
		{
			GameObject gameObject5 = pointerTip.gameObject;
			gameObject5.SetActive(flag7);
		}
		float num32 = default(float);
		if (num9 > 0f && onDragProgress != null)
		{
			float arg3 = (float)(ref obj2) + 95f;
			_ = _003CAngleValue_003Ek__BackingField;
			onDragProgress.Invoke((nint)(&num32), arg3);
			num32 = _003CDistanceValue_003Ek__BackingField;
		}
		if (flag7)
		{
			_003CHasReachedMinimumDragDistance_003Ek__BackingField = true;
		}
		bool flag10 = minimumDistanceEventFired;
		bool flag11 = false;
		if (!flag10)
		{
			flag11 = flag7;
		}
		if (flag11)
		{
			bool flag12 = onMinimumDragDistanceReached == null;
			minimumDistanceEventFired = true;
			if (!flag12)
			{
				float arg4 = (float)(ref obj2) + 95f;
				_ = _003CAngleValue_003Ek__BackingField;
				onMinimumDragDistanceReached.Invoke((nint)(&num32), arg4);
			}
		}
		return;
		IL_0a9a:
		_003CNormalizedMarkerSpeed_003Ek__BackingField = num18;
		inspectorNormalizedSpeed = num18;
		inspectorRawSpeedUnitsPerSecond = num17;
		euler = previousTipLocalPosition;
		goto IL_0a2f;
	}

	public unsafe void FinalizePlacement()
	{
		//IL_00bd: Expected F4, but got Ref
		//IL_00bd: Expected F4, but got Ref
		bool flag = !resetSpeedOnFinalize;
		hasPreviousTipPosition = false;
		if (!flag)
		{
			_003CNormalizedMarkerSpeed_003Ek__BackingField = 0f;
			inspectorRawSpeedUnitsPerSecond = 0f;
		}
		placementTooltipFinalized = true;
		if (placementTooltip != null)
		{
			placementTooltip.SetActive(value: false);
		}
		if (!placementEventFired)
		{
			bool flag2 = onPlacementFinalized == null;
			placementEventFired = true;
			if (!flag2)
			{
				object obj = default(object);
				object obj2 = default(object);
				onPlacementFinalized.Invoke((nint)(&obj), (nint)(&obj2));
			}
		}
	}

	private void MeasureAndUpdateSpeed(Vector3 currentTipLocal)
	{
		//IL_01e6: Expected O, but got F4
		//IL_0063: Invalid comparison between I4 and F4
		//IL_0221: Expected I, but got O
		//IL_00c8: Expected O, but got F4
		//IL_0156: Invalid comparison between F4 and I4
		//IL_01d2: Expected F4, but got I4
		//IL_0187: Invalid comparison between I4 and F4
		if (!hasPreviousTipPosition)
		{
			previousTipLocalPosition = (Vector3)currentTipLocal.x;
			_ = currentTipLocal.z;
			_003CNormalizedMarkerSpeed_003Ek__BackingField = 0f;
			inspectorRawSpeedUnitsPerSecond = 0f;
			hasPreviousTipPosition = true;
			return;
		}
		float num = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
		if (!(0f < num))
		{
			return;
		}
		nint num2 = (nint)typeof(Math);
		float num3 = currentTipLocal.x - (float)previousTipLocalPosition;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		float num4 = currentTipLocal.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapMarkerLineUI)+108]");
		float num5 = num4 - 0f;
		object obj4 = obj * obj;
		float num6 = num3 * num3;
		float num7 = num5 * num5;
		float num8 = (float)obj4 + num6;
		previousTipLocalPosition = (Vector3)currentTipLocal.x;
		_ = currentTipLocal.z;
		float num9 = num8 + num7;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ rcx_v4 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
		}
		else
		{
			double num10 = Math.Sqrt(num9);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
		float num11 = 0f / num;
		float num12;
		if (speedNormalizationRange > 0f)
		{
			num12 = num11 / speedNormalizationRange;
			if (!(0f > num12))
			{
				if (num12 > 1f)
				{
					num12 = 1f;
				}
				goto IL_0265;
			}
		}
		num12 = 0f;
		goto IL_0265;
		IL_0265:
		_003CNormalizedMarkerSpeed_003Ek__BackingField = num12;
		inspectorNormalizedSpeed = num12;
		inspectorRawSpeedUnitsPerSecond = num11;
	}

	private void SetNormalizedSpeed(float normalized, float rawUnitsPerSec = 0f)
	{
		_003CNormalizedMarkerSpeed_003Ek__BackingField = normalized;
		inspectorNormalizedSpeed = normalized;
		inspectorRawSpeedUnitsPerSecond = rawUnitsPerSec;
	}

	private unsafe void DriveLines(Vector3 endLocal)
	{
		//IL_00a3: Expected O, but got Ref
		//IL_00b2: Expected O, but got Ref
		//IL_00e9: Expected O, but got I4
		//IL_00f2: Expected O, but got I4
		//IL_012f: Expected O, but got Ref
		//IL_013e: Expected O, but got Ref
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		if (line == null)
		{
			if (additionalLines == null)
			{
				return;
			}
			List<Line> list = additionalLines;
			if (list._size <= 0)
			{
				return;
			}
		}
		if (line != null)
		{
			Vector3 vector = default(Vector3);
			line.Start = (Vector3)(&vector);
			line.End = (Vector3)(&vector);
		}
		if (additionalLines == null)
		{
			return;
		}
		List<Line> list2 = additionalLines;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		Vector3 vector2 = default(Vector3);
		float num = default(float);
		while ((nint)obj2 < list2._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				((Line)obj3).Start = (Vector3)(&vector2);
				((Line)obj3).End = (Vector3)(&num);
			}
			list2 = additionalLines;
			obj++;
			obj2 = obj;
		}
	}

	private unsafe void ApplyDrivenLineEndpoints(Line l, Vector3 start, Vector3 end)
	{
		//IL_002f: Expected O, but got Ref
		//IL_003c: Expected O, but got Ref
		if (l != null)
		{
			float num = default(float);
			l.Start = (Vector3)(&num);
			l.End = (Vector3)(&num);
		}
	}

	private unsafe void DrivePointerTip(Vector2 directionLocalOnMap)
	{
		//IL_0032: Expected O, but got Ref
		//IL_0081: Invalid comparison between O and F4
		//IL_00c3: Expected O, but got Ref
		if (!(pointerTip != null))
		{
			return;
		}
		Vector3 euler = default(Vector3);
		pointerTip.localPosition = (Vector3)(&euler);
		if (rotatePointerTip)
		{
			object obj2 = default(object);
			object obj = obj2 * obj2;
			object obj3 = directionLocalOnMap * directionLocalOnMap;
			object obj4 = obj + obj3;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
				Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
				pointerTip.localRotation = (Quaternion)(&euler);
			}
		}
	}

	private unsafe void DriveFaceDragDirectionTarget(Vector2 directionLocalOnMap)
	{
		//IL_0071: Invalid comparison between O and F4
		//IL_00b3: Expected O, but got Ref
		bool flag = faceDragDirectionTarget == null;
		if (!flag && rotateFaceDragDirectionTarget != flag)
		{
			object obj2 = default(object);
			object obj = obj2 * obj2;
			object obj3 = directionLocalOnMap * directionLocalOnMap;
			object obj4 = obj + obj3;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
				Vector3 euler = default(Vector3);
				Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
				faceDragDirectionTarget.localRotation = (Quaternion)(&euler);
			}
		}
	}

	private void DriveDiscs(float lengthLocalOnMap)
	{
		//IL_020f: Invalid comparison between F4 and I4
		//IL_00fa: Expected O, but got I4
		//IL_0103: Expected O, but got I4
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_01af: Invalid comparison between I4 and F4
		//IL_01cf: Expected F4, but got I4
		if (disc == null)
		{
			if (additionalDiscs == null)
			{
				return;
			}
			List<Disc> list = additionalDiscs;
			if (list._size <= 0)
			{
				return;
			}
		}
		float num2 = default(float);
		float num = num2 * discRadiusMultiplier;
		float num3 = discRadiusMin;
		if (!(discRadiusMin > num))
		{
			num3 = num;
		}
		if (discRadiusMax > 0f && num3 > discRadiusMax)
		{
			num3 = discRadiusMax;
		}
		ApplyDrivenDiscValues(disc, num3);
		if (additionalDiscs == null)
		{
			return;
		}
		List<Disc> list2 = additionalDiscs;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list2._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				((Disc)obj3).Radius = num3;
				bool flag = !driveDiscThicknessFromRadius;
				num2 = num3;
				if (!flag)
				{
					bool hasThickness = ((Disc)obj3).HasThickness;
					bool flag2 = !hasThickness;
					num2 = num3;
					if (!flag2)
					{
						num2 = num3 * discThicknessFraction;
						if (0f > num2)
						{
							num2 = 0f;
						}
						((Disc)obj3).RadiusInner = num2;
					}
				}
			}
			list2 = additionalDiscs;
			obj++;
			obj2 = obj;
		}
	}

	private void ApplyDrivenDiscValues(Disc d, float drivenRadius)
	{
		//IL_008d: Invalid comparison between I4 and F4
		//IL_009f: Expected F4, but got I4
		if (!(d != null))
		{
			return;
		}
		d.Radius = drivenRadius;
		if (driveDiscThicknessFromRadius && d.HasThickness)
		{
			float num = drivenRadius * discThicknessFraction;
			bool flag = !(0f < num);
			float radiusInner = 0f;
			if (!flag)
			{
				radiusInner = num;
			}
			d.RadiusInner = radiusInner;
		}
	}

	private void SetLabelsVisible(bool visible)
	{
		if (angleLabel != null)
		{
			GameObject gameObject = angleLabel.gameObject;
			bool activeSelf = gameObject.activeSelf;
			if (activeSelf != visible)
			{
				GameObject gameObject2 = angleLabel.gameObject;
				gameObject2.SetActive(visible);
			}
		}
		if (distanceLabel != null)
		{
			GameObject gameObject3 = distanceLabel.gameObject;
			bool activeSelf2 = gameObject3.activeSelf;
			if (activeSelf2 != visible)
			{
				GameObject gameObject4 = distanceLabel.gameObject;
				gameObject4.SetActive(visible);
			}
		}
	}

	public MapMarkerLineUI()
	{
		List<Line> list = new List<Line>();
		additionalLines = list;
		additionalDiscs = new List<Disc>();
		discRadiusMultiplier = 1f;
		discThicknessFraction = 0.1f;
		rotatePointerTip = true;
		hidePointerTipUntilDragged = true;
		rotateFaceDragDirectionTarget = true;
		hideLabelsUntilDragged = true;
		minimumDragDistance = 0.05f;
		hidePlacementTooltipOnDrag = true;
		speedNormalizationRange = 200f;
		useUnscaledTime = true;
		base._002Ector();
	}
}
