using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class RTSMapCameraPauseZoom : MonoBehaviour
{
	public float pauseMinZoom = 14f;

	public float pauseMaxZoom = 30f;

	public float pauseTransitionTau = 0.15f;

	public float restoreTransitionTau = 0.15f;

	public float arrivalThreshold = 0.01f;

	private RTSMapCameraController controller;

	private bool wasPaused;

	private bool didAdjust;

	private float preAdjustZoom;

	private float pauseTargetZoom;

	private bool pauseAnimDone;

	private bool restoring;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		RTSMapCameraController rTSMapCameraController = default(RTSMapCameraController);
		controller = rTSMapCameraController;
	}

	private void Update()
	{
		//IL_026b: Expected O, but got I4
		//IL_03da: Expected O, but got I4
		//IL_04f7: Expected O, but got I4
		//IL_02e1: Invalid comparison between F4 and I
		//IL_011f: Expected O, but got I4
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Expected O, but got Unknown
		//IL_0543: Invalid comparison between I4 and F4
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Expected O, but got Unknown
		//IL_0416: Invalid comparison between O and F4
		//IL_0344: Expected F4, but got I4
		//IL_0308: Expected F4, but got I
		//IL_0166: Invalid comparison between F4 and I
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Expected O, but got Unknown
		//IL_0388: Invalid comparison between F4 and O
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Expected O, but got Unknown
		//IL_0490: Invalid comparison between I4 and F4
		//IL_01c9: Expected F4, but got I4
		//IL_018d: Expected F4, but got I
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		//IL_020d: Invalid comparison between F4 and O
		//IL_021f: Expected O, but got I4
		//IL_0252: Expected O, but got I4
		float timeScale = Time.timeScale;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		bool flag = default(bool);
		float currentZoom;
		float num2;
		if (flag)
		{
			if (wasPaused)
			{
				goto IL_03c0;
			}
			currentZoom = controller.GetCurrentZoom();
			float num = pauseMinZoom;
			if (!(pauseMinZoom > currentZoom))
			{
				num = pauseMaxZoom;
				bool flag2 = !(currentZoom > pauseMaxZoom);
				num2 = currentZoom;
				if (flag2)
				{
					goto IL_03e8;
				}
			}
			num2 = num;
			goto IL_03e8;
		}
		bool flag3 = (byte)(~(wasPaused ? 1u : 0u)) != 0;
		object obj = 0;
		if (!flag3)
		{
			if (didAdjust)
			{
				restoring = true;
			}
			didAdjust = false;
			pauseAnimDone = false;
			obj = 0;
		}
		goto IL_042a;
		IL_03e8:
		float num3 = currentZoom - num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num3 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)arrivalThreshold))
		{
			didAdjust = false;
			pauseAnimDone = true;
		}
		else
		{
			preAdjustZoom = currentZoom;
			pauseTargetZoom = num2;
			didAdjust = true;
			pauseAnimDone = false;
		}
		goto IL_03c0;
		IL_03c0:
		bool flag4 = !didAdjust;
		obj = 0;
		if (!flag4)
		{
			bool flag5 = pauseAnimDone;
			obj = 0;
			if (!flag5)
			{
				float currentZoom2 = controller.GetCurrentZoom();
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float num4 = pauseTransitionTau;
				float num5 = pauseTransitionTau;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
				if (num5 < 0f)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
					num4 = 0f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				object obj3 = unscaledDeltaTime ^ 0;
				float num6 = (float)obj3 / num4;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
				float num7 = 1f - num6;
				if (!(0f > num7))
				{
					if (num7 > 1f)
					{
						num7 = 1f;
					}
				}
				else
				{
					num7 = 0f;
				}
				float num8 = pauseTargetZoom - currentZoom2;
				float num9 = num8 * num7;
				float num10 = num9 + currentZoom2;
				controller.SetZoomDirect(num10);
				float num11 = num10 - pauseTargetZoom;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj4 = num11 & 0;
				float num12 = arrivalThreshold;
				bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num12) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4);
				obj = 0;
				if (!flag6)
				{
					controller.SetZoomDirect(pauseTargetZoom);
					pauseAnimDone = true;
					obj = 0;
				}
			}
		}
		goto IL_042a;
		IL_042a:
		if (restoring)
		{
			float currentZoom3 = controller.GetCurrentZoom();
			float deltaTime = Time.deltaTime;
			float num13 = restoreTransitionTau;
			float num14 = restoreTransitionTau;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
			if (num14 < 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
				num13 = 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj5 = deltaTime ^ 0;
			float num15 = (float)obj5 / num13;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num16 = 1f - num15;
			if (!(0f > num16))
			{
				if (num16 > 1f)
				{
					num16 = 1f;
				}
			}
			else
			{
				num16 = 0f;
			}
			float num17 = preAdjustZoom - currentZoom3;
			float num18 = num17 * num16;
			float num19 = num18 + currentZoom3;
			controller.SetZoomDirect(num19);
			float num20 = num19 - preAdjustZoom;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj6 = num20 & 0;
			float num21 = arrivalThreshold;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num21) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
			{
				controller.SetZoomDirect(preAdjustZoom);
				restoring = false;
			}
		}
		wasPaused = flag;
	}

	private void OnBecamePaused()
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00e7: Invalid comparison between O and F4
		float currentZoom = controller.GetCurrentZoom();
		float num = pauseMinZoom;
		float num2;
		if (!(pauseMinZoom > currentZoom))
		{
			num = pauseMaxZoom;
			bool flag = !(currentZoom > pauseMaxZoom);
			num2 = currentZoom;
			if (flag)
			{
				goto IL_00b9;
			}
		}
		num2 = num;
		goto IL_00b9;
		IL_00b9:
		float num3 = currentZoom - num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num3 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)arrivalThreshold))
		{
			didAdjust = false;
			pauseAnimDone = true;
			return;
		}
		preAdjustZoom = currentZoom;
		pauseTargetZoom = num2;
		didAdjust = true;
		pauseAnimDone = false;
	}

	private void OnBecameUnpaused()
	{
		bool flag = !didAdjust;
		didAdjust = false;
		if (!flag)
		{
			pauseAnimDone = false;
		}
		else
		{
			pauseAnimDone = false;
		}
	}

	private void TickPauseAnimation()
	{
		//IL_0039: Invalid comparison between F4 and I
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_015f: Invalid comparison between I4 and F4
		//IL_009c: Expected F4, but got I4
		//IL_0060: Expected F4, but got I
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00e0: Invalid comparison between F4 and O
		float currentZoom = controller.GetCurrentZoom();
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		float num = pauseTransitionTau;
		float num2 = pauseTransitionTau;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
		if (num2 < 0f)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
			num = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj = unscaledDeltaTime ^ 0;
		float num3 = (float)obj / num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
		float num4 = 1f - num3;
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
		float num5 = pauseTargetZoom - currentZoom;
		float num6 = num5 * num4;
		float num7 = num6 + currentZoom;
		controller.SetZoomDirect(num7);
		float num8 = num7 - pauseTargetZoom;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num8 & 0;
		float num9 = arrivalThreshold;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			controller.SetZoomDirect(pauseTargetZoom);
			pauseAnimDone = true;
		}
	}

	private void TickRestoreAnimation()
	{
		//IL_0039: Invalid comparison between F4 and I
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_015f: Invalid comparison between I4 and F4
		//IL_009c: Expected F4, but got I4
		//IL_0060: Expected F4, but got I
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00e0: Invalid comparison between F4 and O
		float currentZoom = controller.GetCurrentZoom();
		float deltaTime = Time.deltaTime;
		float num = restoreTransitionTau;
		float num2 = restoreTransitionTau;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
		if (num2 < 0f)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC4]");
			num = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj = deltaTime ^ 0;
		float num3 = (float)obj / num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
		float num4 = 1f - num3;
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
		float num5 = preAdjustZoom - currentZoom;
		float num6 = num5 * num4;
		float num7 = num6 + currentZoom;
		controller.SetZoomDirect(num7);
		float num8 = num7 - preAdjustZoom;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num8 & 0;
		float num9 = arrivalThreshold;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			controller.SetZoomDirect(preAdjustZoom);
			restoring = false;
		}
	}
}
