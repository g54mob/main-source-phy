using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class FireMissionCardPrinter : MonoBehaviour
{
	public enum BearingMonitorUpdateMode
	{
		EveryFrame,
		FixedRate
	}

	public enum ShellTypeMonitorUpdateMode
	{
		EveryFrame,
		FixedRate
	}

	private ArtilleryComputer artilleryComputer;

	private GameObject fireMissionCardPrefab;

	private Transform spawnParent;

	private Transform spawnPoint;

	private DialInteractable bearingDial;

	private DialInteractable shellTypeDial;

	private DialInteractable gunDial;

	private SplitFlipTextureDisplay targetSplitFlipDisplay;

	private string targetTexturePropertyName = "_MainTex";

	private bool useInstancedMaterialsForTarget = true;

	private List<Texture> powderChargeTextures;

	private string powderChargeTexturePropertyName;

	private bool useInstancedMaterialsForPowderCharge;

	private bool resetCalculateOnPrinterInputChange;

	private bool detectTargetChangesByPolling;

	private bool requireBearingUnlockToPrint;

	private float bearingUnlockEpsilon;

	private OdometerDisplay bearingOdometerDisplay;

	private BearingMonitorUpdateMode bearingMonitorUpdateMode;

	private float bearingMonitorUpdatesPerSecond;

	private bool clampBearingForOdometerTo360;

	private SplitFlipStringController shellTypeSplitFlipDisplay;

	private ShellTypeMonitorUpdateMode shellTypeMonitorUpdateMode;

	private float shellTypeMonitorUpdatesPerSecond;

	private bool uppercaseShellTypeForSplitFlip;

	private List<ShellDefinition> shellDefinitions;

	private string distanceFormat;

	private string distanceSuffix;

	private string bearingFormat;

	private string bearingSuffix;

	private string elevationFormat;

	private string elevationSuffix;

	private int gun1Value;

	private int gun2Value;

	private string gun1Label;

	private string gun2Label;

	private string unknownGunLabel;

	private string notAvailableText;

	private float _bearingMonitorTimer;

	private float _shellTypeMonitorTimer;

	private int _targetTexturePropertyID;

	private int _powderChargeTexturePropertyID;

	private Texture _lastCommittedTargetTexture;

	private bool _bearingPrintUnlocked;

	private void Awake()
	{
		int targetTexturePropertyID = Shader.PropertyToID(targetTexturePropertyName);
		_targetTexturePropertyID = targetTexturePropertyID;
		int powderChargeTexturePropertyID = Shader.PropertyToID(powderChargeTexturePropertyName);
		_powderChargeTexturePropertyID = powderChargeTexturePropertyID;
	}

	private void OnEnable()
	{
		if (this.artilleryComputer != null)
		{
			ArtilleryComputer artilleryComputer = this.artilleryComputer;
			if (artilleryComputer.OnCalculationSuccess != null)
			{
				UnityAction<float, float, int, bool> call = HandleCalculationSuccess;
				artilleryComputer.OnCalculationSuccess.AddListener(call);
			}
		}
		if (bearingDial != null)
		{
			DialInteractable dialInteractable = bearingDial;
			UnityAction<float> call2 = HandleBearingDialChanged;
			dialInteractable.OnValueChanged.AddListener(call2);
		}
		if (shellTypeDial != null)
		{
			DialInteractable dialInteractable2 = shellTypeDial;
			UnityAction<float> call3 = HandlePrinterDialChanged;
			dialInteractable2.OnValueChanged.AddListener(call3);
		}
		if (gunDial != null)
		{
			DialInteractable dialInteractable3 = gunDial;
			UnityAction<float> call4 = HandlePrinterDialChanged;
			dialInteractable3.OnValueChanged.AddListener(call4);
		}
		_bearingMonitorTimer = 0f;
		bool flag = targetSplitFlipDisplay != null;
		bool flag2 = !flag;
		Texture lastCommittedTargetTexture = null;
		if (!flag2)
		{
			Texture currentCommittedTexture = targetSplitFlipDisplay.CurrentCommittedTexture;
			lastCommittedTargetTexture = currentCommittedTexture;
		}
		_lastCommittedTargetTexture = lastCommittedTargetTexture;
		bool bearingPrintUnlocked = !requireBearingUnlockToPrint;
		_bearingPrintUnlocked = bearingPrintUnlocked;
		UpdateBearingOdometerMirror(force: true);
		UpdateShellTypeSplitFlipMirror(force: true);
	}

	private void OnDisable()
	{
		if (this.artilleryComputer != null)
		{
			ArtilleryComputer artilleryComputer = this.artilleryComputer;
			if (artilleryComputer.OnCalculationSuccess != null)
			{
				UnityAction<float, float, int, bool> call = HandleCalculationSuccess;
				artilleryComputer.OnCalculationSuccess.RemoveListener(call);
			}
		}
		if (bearingDial != null)
		{
			DialInteractable dialInteractable = bearingDial;
			UnityAction<float> call2 = HandleBearingDialChanged;
			dialInteractable.OnValueChanged.RemoveListener(call2);
		}
		if (shellTypeDial != null)
		{
			DialInteractable dialInteractable2 = shellTypeDial;
			UnityAction<float> call3 = HandlePrinterDialChanged;
			dialInteractable2.OnValueChanged.RemoveListener(call3);
		}
		if (gunDial != null)
		{
			DialInteractable dialInteractable3 = gunDial;
			UnityAction<float> call4 = HandlePrinterDialChanged;
			dialInteractable3.OnValueChanged.RemoveListener(call4);
		}
	}

	private void Update()
	{
		//IL_0070: Invalid comparison between F4 and I4
		//IL_0101: Invalid comparison between F4 and I4
		if (bearingOdometerDisplay != null && bearingDial != null)
		{
			if (bearingMonitorUpdateMode != BearingMonitorUpdateMode.EveryFrame)
			{
				float num = ((!(bearingMonitorUpdatesPerSecond > 0f)) ? 10f : bearingMonitorUpdatesPerSecond);
				float deltaTime = Time.deltaTime;
				float num2 = deltaTime + _bearingMonitorTimer;
				float num3 = 1f / num;
				_bearingMonitorTimer = num2;
				if (num3 > num2)
				{
					goto IL_00b9;
				}
				_bearingMonitorTimer = 0f;
			}
			UpdateBearingOdometerMirror(force: true);
		}
		goto IL_00b9;
		IL_029c:
		if (resetCalculateOnPrinterInputChange && detectTargetChangesByPolling && targetSplitFlipDisplay != null)
		{
			Texture currentCommittedTexture = targetSplitFlipDisplay.CurrentCommittedTexture;
			if (currentCommittedTexture != _lastCommittedTargetTexture)
			{
				_lastCommittedTargetTexture = currentCommittedTexture;
				ResetCalculateGateFromPrinterIfEnabled();
			}
		}
		return;
		IL_00b9:
		if (shellTypeSplitFlipDisplay != null)
		{
			if (shellTypeMonitorUpdateMode != ShellTypeMonitorUpdateMode.EveryFrame)
			{
				bool flag = !(shellTypeMonitorUpdatesPerSecond > 0f);
				float num4 = 10f;
				if (!flag)
				{
					num4 = shellTypeMonitorUpdatesPerSecond;
				}
				float deltaTime2 = Time.deltaTime;
				float num5 = deltaTime2 + _shellTypeMonitorTimer;
				float num6 = 1f / num4;
				_shellTypeMonitorTimer = num5;
				if (num6 > num5)
				{
					goto IL_029c;
				}
				_shellTypeMonitorTimer = 0f;
			}
			UpdateShellTypeSplitFlipMirror(force: true);
		}
		goto IL_029c;
	}

	private void HandlePrinterDialChanged(float _)
	{
		ResetCalculateGateFromPrinterIfEnabled();
	}

	private void HandleBearingDialChanged(float _)
	{
		//IL_006b: Invalid comparison between F4 and I4
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e8: Invalid comparison between O and F4
		if (!_bearingPrintUnlocked)
		{
			if (!requireBearingUnlockToPrint)
			{
				goto IL_008d;
			}
			if (bearingDial != null)
			{
				DialInteractable dialInteractable = bearingDial;
				float num = bearingUnlockEpsilon;
				if (!(bearingUnlockEpsilon > 0f))
				{
					num = 0.0001f;
				}
				float accumulatedValue = dialInteractable.accumulatedValue;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj = accumulatedValue & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
				{
					goto IL_008d;
				}
			}
		}
		goto IL_00bf;
		IL_008d:
		_bearingPrintUnlocked = true;
		goto IL_00bf;
		IL_00bf:
		ResetCalculateGateFromPrinterIfEnabled();
	}

	private void TryUnlockBearingPrint()
	{
		//IL_006b: Invalid comparison between F4 and I4
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00e2: Invalid comparison between O and F4
		if (_bearingPrintUnlocked)
		{
			return;
		}
		if (requireBearingUnlockToPrint)
		{
			if (!(bearingDial != null))
			{
				return;
			}
			DialInteractable dialInteractable = bearingDial;
			float num = bearingUnlockEpsilon;
			if (!(bearingUnlockEpsilon > 0f))
			{
				num = 0.0001f;
			}
			float accumulatedValue = dialInteractable.accumulatedValue;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = accumulatedValue & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
			{
				return;
			}
		}
		_bearingPrintUnlocked = true;
	}

	private void UpdateTargetChangeDetectionTick()
	{
		if (resetCalculateOnPrinterInputChange && detectTargetChangesByPolling && targetSplitFlipDisplay != null)
		{
			Texture currentCommittedTexture = targetSplitFlipDisplay.CurrentCommittedTexture;
			if (currentCommittedTexture != _lastCommittedTargetTexture)
			{
				_lastCommittedTargetTexture = currentCommittedTexture;
				ResetCalculateGateFromPrinterIfEnabled();
			}
		}
	}

	private void ResetCalculateGateFromPrinterIfEnabled()
	{
		if (resetCalculateOnPrinterInputChange && this.artilleryComputer != null)
		{
			ArtilleryComputer artilleryComputer = this.artilleryComputer;
			artilleryComputer.waitingForCalculation = true;
			artilleryComputer.UpdateCalculateButtonState(true);
			if (artilleryComputer.successDelayRoutine != null)
			{
				artilleryComputer.StopCoroutine(artilleryComputer.successDelayRoutine);
				artilleryComputer.successDelayRoutine = null;
			}
		}
	}

	private void UpdateBearingMonitorTick()
	{
		//IL_0070: Invalid comparison between F4 and I4
		if (!(bearingOdometerDisplay != null) || !(bearingDial != null))
		{
			return;
		}
		if (bearingMonitorUpdateMode != BearingMonitorUpdateMode.EveryFrame)
		{
			float num = ((!(bearingMonitorUpdatesPerSecond > 0f)) ? 10f : bearingMonitorUpdatesPerSecond);
			float deltaTime = Time.deltaTime;
			float num2 = deltaTime + _bearingMonitorTimer;
			float num3 = 1f / num;
			_bearingMonitorTimer = num2;
			if (num3 > num2)
			{
				return;
			}
			_bearingMonitorTimer = 0f;
		}
		UpdateBearingOdometerMirror(force: true);
	}

	private void UpdateShellTypeMonitorTick()
	{
		//IL_004d: Invalid comparison between F4 and I4
		if (!(shellTypeSplitFlipDisplay != null))
		{
			return;
		}
		if (shellTypeMonitorUpdateMode != ShellTypeMonitorUpdateMode.EveryFrame)
		{
			float num = ((!(shellTypeMonitorUpdatesPerSecond > 0f)) ? 10f : shellTypeMonitorUpdatesPerSecond);
			float deltaTime = Time.deltaTime;
			float num2 = deltaTime + _shellTypeMonitorTimer;
			float num3 = 1f / num;
			_shellTypeMonitorTimer = num2;
			if (num3 > num2)
			{
				return;
			}
			_shellTypeMonitorTimer = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 114 Invalid \"Jump target not found in method: 0x1803F7960\"");
	}

	private void UpdateBearingOdometerMirror(bool force)
	{
		if (bearingOdometerDisplay != null && bearingDial != null)
		{
			DialInteractable dialInteractable = bearingDial;
			bool flag = !clampBearingForOdometerTo360;
			float num = dialInteractable.accumulatedValue;
			if (!flag)
			{
				num = Mathf.Repeat(num, 360f);
			}
			OdometerDisplay odometerDisplay = bearingOdometerDisplay;
			odometerDisplay.targetNumber = num;
		}
	}

	private void UpdateShellTypeSplitFlipMirror(bool force)
	{
		if (shellTypeSplitFlipDisplay != null)
		{
			string text = ResolveShellTypeForPrint();
			if (uppercaseShellTypeForSplitFlip && text != null)
			{
				text = text.ToUpperInvariant();
			}
			shellTypeSplitFlipDisplay.SetTextAndApply(text);
		}
	}

	private unsafe void HandleCalculationSuccess(float elevationDegrees, float clampedRange, int powderCharge, bool wasClamped)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0147: Expected O, but got Ref
		//IL_0155: Expected O, but got Ref
		//IL_019d: Expected O, but got Ref
		//IL_01b7: Expected O, but got I
		//IL_00da: Expected O, but got Ref
		//IL_00e8: Expected O, but got Ref
		//IL_01ce: Expected O, but got I
		//IL_01ff: Expected Ref, but got F4
		//IL_072f: Expected Ref, but got F4
		//IL_0335: Expected Ref, but got F4
		//IL_029c: Invalid comparison between F4 and I4
		//IL_04e8: Expected O, but got I
		//IL_04e8: Expected O, but got I
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Expected O, but got Unknown
		//IL_06fc: Invalid comparison between F4 and O
		//IL_02e0: Expected O, but got I
		//IL_02f0: Expected O, but got I
		//IL_055c: Expected O, but got I
		//IL_062d: Expected O, but got I4
		//IL_0603: Expected O, but got I
		//IL_0640: Expected O, but got Ref
		//IL_05e8: Expected O, but got I4
		//IL_0661: Expected O, but got I
		//IL_069c: Expected O, but got I
		//IL_069c: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		string bearingToTarget;
		if (fireMissionCardPrefab != null)
		{
			Transform transform;
			if (spawnPoint != null)
			{
				transform = spawnPoint;
			}
			else
			{
				Transform transform2 = base.transform;
				transform = transform2;
			}
			if (spawnParent != null)
			{
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				_ = rotation.x;
				_ = position.x;
				_ = position.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180733CA0");
			}
			else
			{
				Vector3 position2 = transform.position;
				Quaternion rotation2 = transform.rotation;
				Quaternion rotation3 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				Vector3 position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				_ = rotation2.x;
				_ = position2.x;
				_ = position2.z;
				GameObject gameObject = UnityEngine.Object.Instantiate(fireMissionCardPrefab, position3, rotation3);
			}
			object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D90C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-39]");
			UnityEngine.Object obj6 = (UnityEngine.Object)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-39]");
			if ((UnityEngine.Object)0 != null)
			{
				float num = (float)(ref obj2) + 103f;
				string text = ((float*)num)->ToString(distanceFormat);
				string text2 = text + distanceSuffix;
				_ = 0;
				bool flag = bearingDial == null;
				if (!flag)
				{
					if (requireBearingUnlockToPrint != flag && _bearingPrintUnlocked == flag)
					{
						DialInteractable dialInteractable = bearingDial;
						float num2 = ((!(bearingUnlockEpsilon > 0f)) ? 0.0001f : bearingUnlockEpsilon);
						float accumulatedValue = dialInteractable.accumulatedValue;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj7 = accumulatedValue & 0;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							object obj8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v752 @ rax_v78+B8]");
							object obj9 = 0;
							bearingToTarget = (string)obj9;
							goto IL_0710;
						}
					}
					DialInteractable dialInteractable2 = bearingDial;
					float num3 = (float)(ref obj2) + 87f;
					_ = dialInteractable2.accumulatedValue;
					string text3 = ((float*)num3)->ToString(bearingFormat);
					string text4 = text3 + bearingSuffix;
					bearingToTarget = text4;
				}
				else
				{
					bool flag2 = notAvailableText == null;
					bearingToTarget = "N/A";
					if (!flag2)
					{
						bearingToTarget = notAvailableText;
					}
				}
				goto IL_0710;
			}
			Debug.LogWarning("[FireMissionCardPrinter] Spawned prefab has no FireMissionCard component; cannot populate.");
			return;
		}
		Debug.LogWarning("[FireMissionCardPrinter] No Fire Mission Card Prefab assigned; cannot print.");
		return;
		IL_04b7:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-39]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-29]");
		string gunElevation;
		string powderCharge2 = default(string);
		string shellType = default(string);
		string gunSelection = default(string);
		((FireMissionCard)num4).Apply((string)0, bearingToTarget, gunElevation, powderCharge2, shellType, gunSelection);
		if (targetSplitFlipDisplay != null)
		{
			Texture currentCommittedTexture = targetSplitFlipDisplay.CurrentCommittedTexture;
			if (currentCommittedTexture != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v338 @ rsi_v4 (UnityEngine.Object)+50]");
				FireMissionCard.ApplyTextureToRenderers((List<MeshRenderer>)0, currentCommittedTexture, _targetTexturePropertyID, useInstancedMaterialsForTarget);
			}
		}
		if (powderChargeTextures == null)
		{
			return;
		}
		List<Texture> list = powderChargeTextures;
		if (list._size == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+6F]");
		object obj10;
		if ((nint)0 >= (nint)1)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+6F]");
			if ((nint)0 > (nint)6)
			{
				obj10 = 5;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+6F]");
				obj10 = -1;
				if ((nint)obj10 < 0)
				{
					return;
				}
			}
		}
		else
		{
			obj10 = 0;
		}
		if ((nint)obj10 < list._size)
		{
			object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 87));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+57]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v338 @ rsi_v4 (UnityEngine.Object)+58]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+57]");
				FireMissionCard.ApplyTextureToRenderers((List<MeshRenderer>)num5, (Texture)0, _powderChargeTexturePropertyID, useInstancedMaterialsForPowderCharge);
			}
		}
		return;
		IL_049d:
		if (notAvailableText == null)
		{
		}
		goto IL_04b7;
		IL_0710:
		float num6 = (float)(ref obj2) + 95f;
		string text5 = ((float*)num6)->ToString(elevationFormat);
		gunElevation = text5 + elevationSuffix;
		int num7 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		string text6 = ((int*)num7)->ToString();
		string text7 = ResolveShellTypeForPrint();
		if (!(gunDial != null))
		{
			goto IL_049d;
		}
		DialInteractable dialInteractable3 = gunDial;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		object obj12 = default(object);
		if ((nint)obj12 != gun1Value)
		{
			if ((nint)obj12 != gun2Value)
			{
				if (string.IsNullOrEmpty(unknownGunLabel))
				{
					goto IL_049d;
				}
			}
			else if (!string.IsNullOrEmpty(gun2Label))
			{
			}
		}
		else if (!string.IsNullOrEmpty(gun1Label))
		{
		}
		goto IL_04b7;
	}

	private void ApplyTargetTextureToCard(FireMissionCard card)
	{
		if (targetSplitFlipDisplay != null)
		{
			Texture currentCommittedTexture = targetSplitFlipDisplay.CurrentCommittedTexture;
			if (currentCommittedTexture != null)
			{
				FireMissionCard.ApplyTextureToRenderers(card.targetQuads, currentCommittedTexture, _targetTexturePropertyID, useInstancedMaterialsForTarget);
			}
		}
	}

	private void ApplyPowderChargeTextureToCard(FireMissionCard card, int powderCharge)
	{
		//IL_00b4: Expected O, but got I4
		//IL_008a: Expected O, but got I4
		//IL_0077: Expected O, but got I4
		if (powderChargeTextures == null)
		{
			return;
		}
		List<Texture> list = powderChargeTextures;
		if (list._size == 0)
		{
			return;
		}
		object obj;
		if (powderCharge >= 1)
		{
			if (powderCharge > 6)
			{
				obj = 5;
			}
			else
			{
				obj = powderCharge - 1;
				if ((nint)obj < 0)
				{
					return;
				}
			}
		}
		else
		{
			obj = 0;
		}
		if ((nint)obj < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			if (obj2 != null)
			{
				FireMissionCard.ApplyTextureToRenderers(card.powderChargeQuads, (Texture)obj2, _powderChargeTexturePropertyID, useInstancedMaterialsForPowderCharge);
			}
		}
	}

	private string ResolveBearingForPrint()
	{
		//IL_0096: Invalid comparison between F4 and I4
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01aa: Invalid comparison between F4 and O
		//IL_00da: Expected O, but got I
		//IL_00ea: Expected O, but got I
		bool flag = bearingDial == null;
		if (!flag)
		{
			if (requireBearingUnlockToPrint != flag && _bearingPrintUnlocked == flag)
			{
				DialInteractable dialInteractable = bearingDial;
				if ((object)bearingDial == null)
				{
					goto IL_0175;
				}
				float num = ((!(bearingUnlockEpsilon > 0f)) ? 0.0001f : bearingUnlockEpsilon);
				float accumulatedValue = dialInteractable.accumulatedValue;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj = accumulatedValue & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rax_v14+B8]");
					return (string)0;
				}
			}
			if ((object)bearingDial != null)
			{
				float num2 = default(float);
				string text = num2.ToString(bearingFormat);
				return text + bearingSuffix;
			}
			goto IL_0175;
		}
		bool flag2 = notAvailableText == null;
		string result = "N/A";
		if (!flag2)
		{
			result = notAvailableText;
		}
		return result;
		IL_0175:
		return (string)(object)new NullReferenceException();
	}

	private string ResolveShellTypeForPrint()
	{
		//IL_012c: Expected O, but got I4
		//IL_00fa: Expected O, but got I4
		//IL_017b: Expected O, but got I
		//IL_019a: Expected O, but got I
		if (shellTypeDial != null && shellDefinitions != null)
		{
			List<ShellDefinition> list = shellDefinitions;
			if (list._size != 0)
			{
				DialInteractable dialInteractable = shellTypeDial;
				if ((object)shellTypeDial != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
					List<ShellDefinition> list2 = shellDefinitions;
					if (shellDefinitions != null)
					{
						object obj = default(object);
						if ((nint)obj >= 0)
						{
							object obj2 = list2._size - 1;
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
							{
								obj2 = obj;
							}
						}
						else
						{
							object obj2 = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						UnityEngine.Object obj3 = default(UnityEngine.Object);
						if (obj3 != null)
						{
							if ((object)obj3 == null)
							{
								goto IL_01d1;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ stack_8_v3 (UnityEngine.Object)+18]");
							if (!string.IsNullOrEmpty((string)0))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ stack_8_v3 (UnityEngine.Object)+18]");
								return (string)0;
							}
						}
						goto IL_019a;
					}
				}
				goto IL_01d1;
			}
		}
		goto IL_019a;
		IL_01d1:
		return (string)(object)new NullReferenceException();
		IL_019a:
		bool flag = notAvailableText == null;
		string result = "N/A";
		if (!flag)
		{
			result = notAvailableText;
		}
		return result;
	}

	private string ResolveGunSelectionForPrint()
	{
		if (gunDial != null)
		{
			DialInteractable dialInteractable = gunDial;
			if ((object)gunDial == null)
			{
				return (string)(object)new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			object obj = default(object);
			if ((nint)obj == gun1Value)
			{
				if (string.IsNullOrEmpty(gun1Label))
				{
					return "Gun 1";
				}
				return gun1Label;
			}
			if ((nint)obj == gun2Value)
			{
				if (string.IsNullOrEmpty(gun2Label))
				{
					return "Gun 2";
				}
				return gun2Label;
			}
			if (!string.IsNullOrEmpty(unknownGunLabel))
			{
				return unknownGunLabel;
			}
		}
		bool flag = notAvailableText == null;
		string result = "N/A";
		if (!flag)
		{
			result = notAvailableText;
		}
		return result;
	}

	public FireMissionCardPrinter()
	{
		List<Texture> list = new List<Texture>(6);
		powderChargeTextures = list;
		powderChargeTexturePropertyName = "_MainTex";
		useInstancedMaterialsForPowderCharge = true;
		bearingUnlockEpsilon = 0.0001f;
		bearingMonitorUpdatesPerSecond = 20f;
		clampBearingForOdometerTo360 = true;
		shellTypeMonitorUpdatesPerSecond = 10f;
		uppercaseShellTypeForSplitFlip = true;
		shellDefinitions = new List<ShellDefinition>();
		distanceFormat = "0";
		distanceSuffix = " m";
		bearingFormat = "0.0";
		bearingSuffix = "°";
		elevationFormat = "0.0";
		elevationSuffix = "°";
		gun1Value = 1;
		gun2Value = 2;
		gun1Label = "Gun 1";
		gun2Label = "Gun 2";
		unknownGunLabel = "Unknown";
		notAvailableText = "N/A";
		base._002Ector();
	}
}
