using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class PrinterAlertSystem : MonoBehaviour
{
	public enum AlertTier
	{
		None,
		Success,
		LowPriority,
		HighPriority
	}

	public PrinterAlertLight successLight;

	public PrinterAlertLight lowPriorityLight;

	public PrinterAlertLight highPriorityLight;

	public UnityEvent onSuccessAlertOn;

	public UnityEvent onSuccessAlertOff;

	public UnityEvent onLowPriorityAlertOn;

	public UnityEvent onLowPriorityAlertOff;

	public UnityEvent onHighPriorityAlertOn;

	public UnityEvent onHighPriorityAlertOff;

	public bool keepResidentIdleDuringOverride = true;

	private AlertTier _debugResidentTier;

	private AlertTier _debugOverrideTier;

	public bool debugSuccess;

	public bool debugLowPriority;

	public bool debugHighPriority;

	private AlertTier _003CResidentTier_003Ek__BackingField;

	private AlertTier _003COverrideTier_003Ek__BackingField;

	public AlertTier ResidentTier
	{
		get
		{
			return _003CResidentTier_003Ek__BackingField;
		}
		private set
		{
			_003CResidentTier_003Ek__BackingField = value;
		}
	}

	public AlertTier OverrideTier
	{
		get
		{
			return _003COverrideTier_003Ek__BackingField;
		}
		private set
		{
			_003COverrideTier_003Ek__BackingField = value;
		}
	}

	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (!debugSuccess)
		{
			if (_003CResidentTier_003Ek__BackingField == AlertTier.Success && _003COverrideTier_003Ek__BackingField == AlertTier.None)
			{
				DismissAllAlerts();
			}
		}
		else
		{
			HandleIncoming(AlertTier.Success);
		}
		if (!debugLowPriority)
		{
			if (_003CResidentTier_003Ek__BackingField == AlertTier.LowPriority && _003COverrideTier_003Ek__BackingField == AlertTier.None)
			{
				DismissAllAlerts();
			}
		}
		else
		{
			HandleIncoming(AlertTier.LowPriority);
		}
		if (!debugHighPriority)
		{
			if (_003CResidentTier_003Ek__BackingField == AlertTier.HighPriority && _003COverrideTier_003Ek__BackingField == AlertTier.None)
			{
				DismissAllAlerts();
			}
		}
		else
		{
			HandleIncoming(AlertTier.HighPriority);
		}
	}

	public void TriggerSuccessAlert()
	{
		HandleIncoming(AlertTier.Success);
	}

	public void TriggerLowPriorityAlert()
	{
		HandleIncoming(AlertTier.LowPriority);
	}

	public void TriggerHighPriorityAlert()
	{
		HandleIncoming(AlertTier.HighPriority);
	}

	public void ToggleSuccessAlert()
	{
		if (_003CResidentTier_003Ek__BackingField != AlertTier.Success)
		{
			HandleIncoming(AlertTier.Success);
		}
		else
		{
			DismissAllAlerts();
		}
	}

	public void ToggleLowPriorityAlert()
	{
		if (_003CResidentTier_003Ek__BackingField != AlertTier.LowPriority)
		{
			HandleIncoming(AlertTier.LowPriority);
		}
		else
		{
			DismissAllAlerts();
		}
	}

	public void ToggleHighPriorityAlert()
	{
		if (_003CResidentTier_003Ek__BackingField != AlertTier.HighPriority)
		{
			HandleIncoming(AlertTier.HighPriority);
		}
		else
		{
			DismissAllAlerts();
		}
	}

	public void DismissAllAlerts()
	{
		//IL_0070: Expected O, but got I4
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		if (_003CResidentTier_003Ek__BackingField == AlertTier.None && _003COverrideTier_003Ek__BackingField == AlertTier.None)
		{
			return;
		}
		bool flag = _003CResidentTier_003Ek__BackingField == AlertTier.None;
		if (!flag)
		{
			object obj = _003CResidentTier_003Ek__BackingField - 1;
			UnityEvent unityEvent;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						goto IL_00f5;
					}
					unityEvent = onHighPriorityAlertOff;
				}
				else
				{
					unityEvent = onLowPriorityAlertOff;
				}
			}
			else
			{
				unityEvent = onSuccessAlertOff;
			}
			unityEvent?.Invoke();
		}
		goto IL_00f5;
		IL_00f5:
		UnityEngine.Object obj3 = successLight;
		if (successLight != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rdi_v2 (UnityEngine.Object)+9C]");
			if ((nint)0 > (nint)0)
			{
				successLight.Deactivate();
			}
		}
		UnityEngine.Object obj4 = lowPriorityLight;
		if (lowPriorityLight != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rdi_v4 (UnityEngine.Object)+9C]");
			if ((nint)0 > (nint)0)
			{
				lowPriorityLight.Deactivate();
			}
		}
		UnityEngine.Object obj5 = highPriorityLight;
		if (highPriorityLight != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rdi_v5 (UnityEngine.Object)+9C]");
			if ((nint)0 > (nint)0)
			{
				highPriorityLight.Deactivate();
			}
		}
		_003CResidentTier_003Ek__BackingField = AlertTier.None;
		_debugResidentTier = AlertTier.None;
		debugSuccess = false;
		debugHighPriority = false;
	}

	private void HandleIncoming(AlertTier incoming)
	{
		//IL_002e: Expected O, but got I4
		//IL_003d: Expected O, but got I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_0779: Expected O, but got I4
		//IL_00ac: Expected O, but got I4
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_035e: Expected O, but got I4
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Expected O, but got Unknown
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		//IL_0410: Expected O, but got I4
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Expected O, but got Unknown
		//IL_06fd: Expected O, but got I4
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Expected O, but got Unknown
		//IL_0191: Expected O, but got I4
		//IL_00fe: Expected O, but got I4
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_06c7: Expected O, but got I
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_02ca: Expected O, but got I4
		//IL_0301: Expected O, but got I4
		//IL_032a: Expected O, but got I4
		//IL_0525: Expected O, but got I4
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Expected O, but got Unknown
		//IL_02b5: Expected O, but got I
		bool flag6;
		if (_003CResidentTier_003Ek__BackingField != AlertTier.None)
		{
			object obj = incoming - _003CResidentTier_003Ek__BackingField;
			object obj2 = incoming ^ _003CResidentTier_003Ek__BackingField;
			object obj3 = incoming ^ obj;
			object obj4 = obj2 & obj3;
			bool flag = (nint)obj4 < 0;
			bool flag2 = (nint)obj < 0;
			bool flag3 = incoming == _003CResidentTier_003Ek__BackingField;
			if (!flag3)
			{
				bool flag4 = flag2 == flag;
				object obj5 = !flag3;
				object obj6 = flag4 & obj5;
				if (obj6 == null)
				{
					if (_003COverrideTier_003Ek__BackingField != AlertTier.None)
					{
						return;
					}
					bool flag5 = !keepResidentIdleDuringOverride;
					_003COverrideTier_003Ek__BackingField = incoming;
					object obj7 = !flag5;
					if (obj7 == null)
					{
						object obj8 = _003CResidentTier_003Ek__BackingField - 1;
						PrinterAlertLight printerAlertLight;
						if (!flag5)
						{
							object obj9 = obj8 - 1;
							if (!flag5)
							{
								if ((nint)obj9 != 1)
								{
									goto IL_0183;
								}
								printerAlertLight = highPriorityLight;
							}
							else
							{
								printerAlertLight = lowPriorityLight;
							}
						}
						else
						{
							printerAlertLight = successLight;
						}
						printerAlertLight?.Deactivate();
					}
					goto IL_0183;
				}
				object obj10 = _003CResidentTier_003Ek__BackingField - 1;
				UnityEvent unityEvent;
				if (!flag3)
				{
					object obj11 = obj10 - 1;
					if (!flag3)
					{
						object obj12 = obj11 - 1;
						flag6 = obj12 == null;
						if ((nint)obj11 != 1)
						{
							goto IL_0400;
						}
						unityEvent = onHighPriorityAlertOff;
					}
					else
					{
						unityEvent = onLowPriorityAlertOff;
					}
				}
				else
				{
					unityEvent = onSuccessAlertOff;
				}
				flag6 = unityEvent == null;
				if (!flag6)
				{
					unityEvent.Invoke();
				}
				goto IL_0400;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A429]");
			bool flag7 = (nint)0 == 0;
			object obj13 = _003CResidentTier_003Ek__BackingField - 1;
			UnityEngine.Object obj15;
			if (!flag7)
			{
				object obj14 = obj13 - 1;
				obj15 = (flag7 ? lowPriorityLight : (((nint)obj14 == 1) ? highPriorityLight : null));
			}
			else
			{
				obj15 = successLight;
			}
			if (obj15 != null)
			{
				Action action = OnResidentAlertCurveDone;
				_ = 1;
				_ = 1;
				((PrinterAlertLight)obj15).SetLightEnabled(true);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rdi_v5 (UnityEngine.Object)+80]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rdi_v5 (UnityEngine.Object)+80]");
					((UnityEvent)0).Invoke();
				}
			}
			return;
		}
		goto IL_06c8;
		IL_0183:
		object obj16 = incoming - 1;
		bool flag8 = incoming == AlertTier.Success;
		UnityEngine.Object obj18;
		if (!flag8)
		{
			object obj17 = obj16 - 1;
			obj18 = (flag8 ? lowPriorityLight : (((nint)obj17 == 1) ? highPriorityLight : null));
		}
		else
		{
			obj18 = successLight;
		}
		if (obj18 != null)
		{
			Action action2 = OnOverrideAlertCurveDone;
			_ = 1;
			_ = 1;
			((PrinterAlertLight)obj18).SetLightEnabled(true);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v444 @ rdi_v16 (UnityEngine.Object)+80]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v444 @ rdi_v16 (UnityEngine.Object)+80]");
				((UnityEvent)0).Invoke();
			}
		}
		object obj19 = _003CResidentTier_003Ek__BackingField - 1;
		bool flag9 = obj19 == null;
		_debugResidentTier = _003CResidentTier_003Ek__BackingField;
		_debugOverrideTier = _003COverrideTier_003Ek__BackingField;
		object obj20 = _003CResidentTier_003Ek__BackingField - 2;
		bool flag10 = obj20 == null;
		debugSuccess = flag9;
		object obj21 = _003CResidentTier_003Ek__BackingField - 3;
		bool flag11 = obj21 == null;
		debugLowPriority = flag10;
		debugHighPriority = flag11;
		return;
		IL_06c8:
		SetResident(incoming);
		return;
		IL_0400:
		object obj22 = _003CResidentTier_003Ek__BackingField - 1;
		UnityEngine.Object obj24;
		if (!flag6)
		{
			object obj23 = obj22 - 1;
			obj24 = (flag6 ? lowPriorityLight : (((nint)obj23 == 1) ? highPriorityLight : null));
		}
		else
		{
			obj24 = successLight;
		}
		if (obj24 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v11 (UnityEngine.Object)+9C]");
			if ((nint)0 > (nint)0)
			{
				((PrinterAlertLight)obj24).Deactivate();
			}
		}
		bool flag12 = _003COverrideTier_003Ek__BackingField == AlertTier.None;
		_003CResidentTier_003Ek__BackingField = AlertTier.None;
		if (!flag12)
		{
			object obj25 = _003COverrideTier_003Ek__BackingField - 1;
			PrinterAlertLight light;
			if (!flag12)
			{
				object obj26 = obj25 - 1;
				light = (flag12 ? lowPriorityLight : (((nint)obj26 == 1) ? highPriorityLight : null));
			}
			else
			{
				light = successLight;
			}
			DeactivateLight(light);
			_003COverrideTier_003Ek__BackingField = AlertTier.None;
		}
		goto IL_06c8;
	}

	private void SetResident(AlertTier tier)
	{
		//IL_02a4: Expected O, but got I4
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0212: Expected O, but got I4
		//IL_0249: Expected O, but got I4
		//IL_0272: Expected O, but got I4
		//IL_004c: Expected O, but got I4
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_01fd: Expected O, but got I
		object obj = tier - 1;
		_003CResidentTier_003Ek__BackingField = tier;
		bool flag = tier == AlertTier.Success;
		UnityEngine.Object obj5;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (!flag)
			{
				if ((nint)obj2 != 1)
				{
					object obj3 = tier - 1;
					bool flag2 = tier == AlertTier.Success;
					if (flag2)
					{
						goto IL_0157;
					}
					object obj4 = obj3 - 1;
					if (flag2)
					{
						goto IL_0119;
					}
					if ((nint)obj4 != 1)
					{
						obj5 = null;
						goto IL_0166;
					}
				}
				else if (onHighPriorityAlertOn != null)
				{
					onHighPriorityAlertOn.Invoke();
				}
				obj5 = highPriorityLight;
				goto IL_0166;
			}
			if (onLowPriorityAlertOn != null)
			{
				onLowPriorityAlertOn.Invoke();
			}
			goto IL_0119;
		}
		if (onSuccessAlertOn != null)
		{
			onSuccessAlertOn.Invoke();
		}
		goto IL_0157;
		IL_0119:
		obj5 = lowPriorityLight;
		goto IL_0166;
		IL_0166:
		if (obj5 != null)
		{
			Action action = OnResidentAlertCurveDone;
			_ = 1;
			_ = 1;
			((PrinterAlertLight)obj5).SetLightEnabled(true);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rdi_v2 (UnityEngine.Object)+80]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rdi_v2 (UnityEngine.Object)+80]");
				((UnityEvent)0).Invoke();
			}
		}
		object obj6 = _003CResidentTier_003Ek__BackingField - 1;
		bool flag3 = obj6 == null;
		_debugResidentTier = _003CResidentTier_003Ek__BackingField;
		_debugOverrideTier = _003COverrideTier_003Ek__BackingField;
		object obj7 = _003CResidentTier_003Ek__BackingField - 2;
		bool flag4 = obj7 == null;
		debugSuccess = flag3;
		object obj8 = _003CResidentTier_003Ek__BackingField - 3;
		bool flag5 = obj8 == null;
		debugLowPriority = flag4;
		debugHighPriority = flag5;
		return;
		IL_0157:
		obj5 = successLight;
		goto IL_0166;
	}

	private void RetriggerResident()
	{
		//IL_0138: Expected O, but got I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0123: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A429]");
		bool flag = (nint)0 == 0;
		object obj = _003CResidentTier_003Ek__BackingField - 1;
		UnityEngine.Object obj3;
		if (!flag)
		{
			object obj2 = obj - 1;
			obj3 = (flag ? lowPriorityLight : (((nint)obj2 == 1) ? highPriorityLight : null));
		}
		else
		{
			obj3 = successLight;
		}
		if (obj3 != null)
		{
			Action action = OnResidentAlertCurveDone;
			_ = 1;
			_ = 1;
			((PrinterAlertLight)obj3).SetLightEnabled(true);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (UnityEngine.Object)+80]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (UnityEngine.Object)+80]");
				((UnityEvent)0).Invoke();
			}
		}
	}

	private void PlayTemporaryOverride(AlertTier tier)
	{
		//IL_028b: Expected O, but got I4
		//IL_00a8: Expected O, but got I4
		//IL_0015: Expected O, but got I4
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_01e1: Expected O, but got I4
		//IL_0218: Expected O, but got I4
		//IL_0241: Expected O, but got I4
		//IL_01cc: Expected O, but got I
		bool flag = !keepResidentIdleDuringOverride;
		_003COverrideTier_003Ek__BackingField = tier;
		object obj = !flag;
		if (obj == null)
		{
			object obj2 = _003CResidentTier_003Ek__BackingField - 1;
			PrinterAlertLight printerAlertLight;
			if (!flag)
			{
				object obj3 = obj2 - 1;
				if (!flag)
				{
					if ((nint)obj3 != 1)
					{
						goto IL_009a;
					}
					printerAlertLight = highPriorityLight;
				}
				else
				{
					printerAlertLight = lowPriorityLight;
				}
			}
			else
			{
				printerAlertLight = successLight;
			}
			printerAlertLight?.Deactivate();
		}
		goto IL_009a;
		IL_009a:
		object obj4 = tier - 1;
		bool flag2 = tier == AlertTier.Success;
		UnityEngine.Object obj6;
		if (!flag2)
		{
			object obj5 = obj4 - 1;
			obj6 = (flag2 ? lowPriorityLight : (((nint)obj5 == 1) ? highPriorityLight : null));
		}
		else
		{
			obj6 = successLight;
		}
		if (obj6 != null)
		{
			Action action = OnOverrideAlertCurveDone;
			_ = 1;
			_ = 1;
			((PrinterAlertLight)obj6).SetLightEnabled(true);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rdi_v2 (UnityEngine.Object)+80]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rdi_v2 (UnityEngine.Object)+80]");
				((UnityEvent)0).Invoke();
			}
		}
		object obj7 = _003CResidentTier_003Ek__BackingField - 1;
		bool flag3 = obj7 == null;
		_debugResidentTier = _003CResidentTier_003Ek__BackingField;
		_debugOverrideTier = _003COverrideTier_003Ek__BackingField;
		object obj8 = _003CResidentTier_003Ek__BackingField - 2;
		bool flag4 = obj8 == null;
		debugSuccess = flag3;
		object obj9 = _003CResidentTier_003Ek__BackingField - 3;
		bool flag5 = obj9 == null;
		debugLowPriority = flag4;
		debugHighPriority = flag5;
	}

	private void OnResidentAlertCurveDone()
	{
		//IL_002f: Expected O, but got I4
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		bool flag = _003CResidentTier_003Ek__BackingField == AlertTier.None;
		if (flag)
		{
			return;
		}
		object obj = _003CResidentTier_003Ek__BackingField - 1;
		PrinterAlertLight printerAlertLight;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (!flag)
			{
				if ((nint)obj2 != 1)
				{
					return;
				}
				printerAlertLight = highPriorityLight;
			}
			else
			{
				printerAlertLight = lowPriorityLight;
			}
		}
		else
		{
			printerAlertLight = successLight;
		}
		if ((object)printerAlertLight != null)
		{
			printerAlertLight._onAlertCurveDone = null;
			printerAlertLight._mode = PrinterAlertLight.PlayMode.IdleCurve;
			printerAlertLight.debugPlayMode = PrinterAlertLight.PlayMode.IdleCurve;
			printerAlertLight.SetLightEnabled(true);
		}
	}

	private void OnOverrideAlertCurveDone()
	{
		//IL_0015: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0210: Expected O, but got I4
		//IL_0247: Expected O, but got I4
		//IL_0270: Expected O, but got I4
		//IL_012a: Expected O, but got I4
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		bool flag = _003COverrideTier_003Ek__BackingField == AlertTier.None;
		if (flag)
		{
			return;
		}
		object obj = _003COverrideTier_003Ek__BackingField - 1;
		UnityEngine.Object obj3;
		if (!flag)
		{
			object obj2 = obj - 1;
			obj3 = (flag ? lowPriorityLight : (((nint)obj2 == 1) ? highPriorityLight : null));
		}
		else
		{
			obj3 = successLight;
		}
		if (obj3 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rsi_v2 (UnityEngine.Object)+9C]");
			if ((nint)0 > (nint)0)
			{
				((PrinterAlertLight)obj3).Deactivate();
			}
		}
		_003COverrideTier_003Ek__BackingField = AlertTier.None;
		bool flag2 = _003CResidentTier_003Ek__BackingField == AlertTier.None;
		if (!flag2)
		{
			object obj4 = _003CResidentTier_003Ek__BackingField - 1;
			UnityEngine.Object obj6;
			if (!flag2)
			{
				object obj5 = obj4 - 1;
				if (!flag2)
				{
					bool flag3 = (nint)obj5 != 1;
					obj6 = null;
					if (!flag3)
					{
						obj6 = highPriorityLight;
					}
				}
				else
				{
					obj6 = lowPriorityLight;
				}
			}
			else
			{
				obj6 = successLight;
			}
			if (obj6 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ rdi_v6 (UnityEngine.Object)+9C]");
				if ((nint)0 <= (nint)0)
				{
					((PrinterAlertLight)obj6).PlayIdleCurve();
				}
			}
		}
		object obj7 = _003CResidentTier_003Ek__BackingField - 1;
		bool flag4 = obj7 == null;
		_debugResidentTier = _003CResidentTier_003Ek__BackingField;
		_debugOverrideTier = _003COverrideTier_003Ek__BackingField;
		object obj8 = _003CResidentTier_003Ek__BackingField - 2;
		bool flag5 = obj8 == null;
		debugSuccess = flag4;
		object obj9 = _003CResidentTier_003Ek__BackingField - 3;
		bool flag6 = obj9 == null;
		debugLowPriority = flag5;
		debugHighPriority = flag6;
	}

	private PrinterAlertLight GetLight(AlertTier tier)
	{
		//IL_000e: Expected O, but got I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		object obj = tier - 1;
		object obj2 = default(object);
		if (obj2 == null)
		{
			object obj3 = obj - 1;
			if (obj2 == null)
			{
				if ((nint)obj3 != 1)
				{
					return null;
				}
				return highPriorityLight;
			}
			return lowPriorityLight;
		}
		return successLight;
	}

	private void DeactivateLight(PrinterAlertLight light)
	{
		if (light != null && light._mode > PrinterAlertLight.PlayMode.Inactive)
		{
			light.Deactivate();
		}
	}

	private void FireAlertOn(AlertTier tier)
	{
		//IL_000e: Expected O, but got I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		object obj = tier - 1;
		object obj2 = default(object);
		UnityEvent unityEvent;
		if (obj2 == null)
		{
			object obj3 = obj - 1;
			if (obj2 == null)
			{
				if ((nint)obj3 != 1)
				{
					return;
				}
				unityEvent = onHighPriorityAlertOn;
			}
			else
			{
				unityEvent = onLowPriorityAlertOn;
			}
		}
		else
		{
			unityEvent = onSuccessAlertOn;
		}
		unityEvent?.Invoke();
	}

	private void FireAlertOff(AlertTier tier)
	{
		//IL_000e: Expected O, but got I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		object obj = tier - 1;
		object obj2 = default(object);
		UnityEvent unityEvent;
		if (obj2 == null)
		{
			object obj3 = obj - 1;
			if (obj2 == null)
			{
				if ((nint)obj3 != 1)
				{
					return;
				}
				unityEvent = onHighPriorityAlertOff;
			}
			else
			{
				unityEvent = onLowPriorityAlertOff;
			}
		}
		else
		{
			unityEvent = onSuccessAlertOff;
		}
		unityEvent?.Invoke();
	}

	private void SyncDebugState()
	{
		//IL_0010: Expected O, but got I4
		//IL_0047: Expected O, but got I4
		//IL_0070: Expected O, but got I4
		object obj = _003CResidentTier_003Ek__BackingField - 1;
		bool flag = obj == null;
		_debugResidentTier = _003CResidentTier_003Ek__BackingField;
		_debugOverrideTier = _003COverrideTier_003Ek__BackingField;
		object obj2 = _003CResidentTier_003Ek__BackingField - 2;
		bool flag2 = obj2 == null;
		debugSuccess = flag;
		object obj3 = _003CResidentTier_003Ek__BackingField - 3;
		bool flag3 = obj3 == null;
		debugLowPriority = flag2;
		debugHighPriority = flag3;
	}
}
