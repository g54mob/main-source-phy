using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class HapticCurve : MonoBehaviour
{
	public float Amplitude = 1f;

	public float Frequency;

	public int PointsCount = 50;

	public float AmplitudeFactor = 3f;

	private float Period = 1f;

	public RectTransform StartPoint;

	public RectTransform EndPoint;

	public bool Move;

	public float MovementSpeed = 1f;

	protected LineRenderer _targetLineRenderer;

	protected List<Vector3> Points;

	protected Canvas _canvas;

	protected Camera _camera;

	protected Vector3 _startPosition;

	protected Vector3 _endPosition;

	protected Vector3 _workPoint;

	protected virtual void Awake()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+188]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+190]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected virtual void Initialization()
	{
		//IL_0078: Expected I, but got O
		//IL_0088: Expected O, but got I
		//IL_0098: Expected O, but got I
		Canvas canvas = default(Canvas);
		LineRenderer targetLineRenderer = default(LineRenderer);
		while (true)
		{
			List<Vector3> points = new List<Vector3>();
			Points = points;
			GameObject gameObject = base.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9300");
			_canvas = canvas;
			GameObject gameObject2 = base.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			_targetLineRenderer = targetLineRenderer;
			Camera worldCamera = _canvas.worldCamera;
			_camera = worldCamera;
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+198]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rdx_v12 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+1A0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v114 @ rax_v15 (should have been resolved before IL gen)");
		}
	}

	protected unsafe virtual void DrawCurve()
	{
		//IL_0035: Expected O, but got F4
		//IL_0091: Expected O, but got F4
		//IL_014b: Expected I4, but got O
		//IL_0151: Expected O, but got I
		//IL_0108: Expected I4, but got O
		//IL_010e: Expected O, but got I
		//IL_016f: Expected O, but got I
		//IL_0191: Expected O, but got I
		//IL_01ca: Expected O, but got I4
		//IL_032a: Invalid comparison between I4 and F4
		//IL_0285: Expected F4, but got I4
		//IL_0379: Expected O, but got F4
		//IL_0299: Expected O, but got Ref
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		Transform transform = StartPoint.transform;
		Vector3 position = transform.position;
		_startPosition = (Vector3)position.x;
		_ = position.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.HapticCurve)+78]");
		float num = 0f - 0.1f;
		Transform transform2 = EndPoint.transform;
		Vector3 position2 = transform2.position;
		_endPosition = (Vector3)position2.x;
		_ = position2.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.HapticCurve)+84]");
		float num2 = 0f - 0.1f;
		List<Vector3> points = Points;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rsi_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<Vector3>())
		{
			_ = 0;
			int num3 = 0;
			int num4 = (int)transform2;
			Array array = (Array)0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rsi_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
			int num3 = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rsi_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
			bool flag = (nint)0 <= (nint)0;
			int num4 = (int)transform2;
			Array array = (Array)0;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rsi_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+10]");
				array = (Array)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rsi_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+10]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rsi_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
				Array.Clear((Array)num5, 0, 0);
				num4 = 0;
			}
		}
		if (PointsCount > 0)
		{
			object obj = 0;
			Vector3 workPoint = default(Vector3);
			bool flag3;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,dword ptr [rbx+28h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm6,edi\"");
				float num6 = 0f / 0f;
				float num7 = num6 * Period;
				float num8 = num7 + 1f;
				float num9 = num8 * ((float)Math.PI * 2f);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
				bool flag2 = !Move;
				float num10 = num9 * AmplitudeFactor;
				if (!flag2)
				{
					float time = Time.time;
					float num11 = time * MovementSpeed;
					float num12 = num11 + num6;
					float num13 = num12 * Period;
					float num14 = num13 + 1f;
					float num15 = num14 * ((float)Math.PI * 2f);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
					num10 = num15 * AmplitudeFactor;
				}
				if (!(0f > num6))
				{
					if (num6 > 1f)
					{
						num6 = 1f;
					}
				}
				else
				{
					num6 = 0f;
				}
				object obj2 = _endPosition - _startPosition;
				float num16 = (float)obj2 * num6;
				float num17 = num16 + (float)_startPosition;
				_workPoint = (Vector3)num17;
				float num18 = num10 * Amplitude;
				float num19 = num18;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.HapticCurve)+74]");
				float num20 = num19 + 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.HapticCurve)+78]");
				_ = 0;
				Points.Add((Vector3)(&workPoint));
				obj++;
				flag3 = (nint)obj < PointsCount;
				workPoint = _workPoint;
				int num3 = 0;
				int num4 = (int)(&workPoint);
				Array array = (Array)(object)Points;
			}
			while (flag3);
		}
		_targetLineRenderer.positionCount = PointsCount;
		Vector3[] positions = Points.ToArray();
		_targetLineRenderer.SetPositions(positions);
	}

	protected virtual void Update()
	{
		//IL_0005: Expected I, but got O
		//IL_0029: Expected O, but got I
		//IL_0039: Expected O, but got I
		nint num = (nint)this;
		float frequency = Frequency;
		float amplitude = Amplitude;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r9_v1 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+1B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r9_v1 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+1C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v4 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public virtual void UpdateCurve(float amplitude, float frequency)
	{
		//IL_0005: Expected I, but got O
		//IL_0053: Expected O, but got I
		//IL_0063: Expected O, but got I
		nint num = (nint)this;
		Frequency = frequency;
		Amplitude = amplitude;
		float num2 = frequency * 3f;
		float period = num2 + 1f;
		Period = period;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+198]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.HapticCurve>)+1A0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v8 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
