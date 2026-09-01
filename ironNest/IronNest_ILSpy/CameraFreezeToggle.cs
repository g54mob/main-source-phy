using System;
using Cpp2ILInjected;
using UnityEngine;

public class CameraFreezeToggle : MonoBehaviour
{
	private GameObject playerVirtualCamera;

	private bool respectCameraZoneTrigger;

	private bool frozen;

	private bool lockFreezePlayerController;

	private bool lockUseFreeMouse;

	private bool lockUseUIActionMap;

	private string debugLabel;

	private string brokerTag;

	private bool enableTagAutoWiring;

	private bool retryAutoWiringIfMissing;

	private string virtualCameraTag;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _handle;

	private void Awake()
	{
		TryAutoWireReferences();
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(brokerTag);
		_broker = broker;
	}

	private void Start()
	{
		TryAutoWireReferences();
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(brokerTag);
		_broker = broker;
		EvaluateAndApply();
	}

	private void Update()
	{
		if (enableTagAutoWiring && retryAutoWiringIfMissing && respectCameraZoneTrigger && playerVirtualCamera == null)
		{
			TryAutoWireReferences();
		}
		EvaluateAndApply();
	}

	public void SetFrozen(bool value)
	{
		frozen = value;
		EvaluateAndApply();
	}

	public void Toggle()
	{
		bool flag = !frozen;
		frozen = flag;
		EvaluateAndApply();
	}

	private void EvaluateAndApply()
	{
		bool flag = !respectCameraZoneTrigger || !(playerVirtualCamera != null) || playerVirtualCamera.activeInHierarchy;
		if (frozen && flag)
		{
			EnsureLock();
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 112 Invalid \"Jump target not found in method: 0x18056BD50\"");
		throw new NullReferenceException();
	}

	private bool ShouldBlockDueToZone()
	{
		//IL_0088: Expected I4, but got O
		if (respectCameraZoneTrigger && playerVirtualCamera != null)
		{
			if ((object)playerVirtualCamera != null)
			{
				bool activeInHierarchy = playerVirtualCamera.activeInHierarchy;
				return (byte)((activeInHierarchy ? 1u : 0u) ^ 1u) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private unsafe void EnsureLock()
	{
		//IL_009b: Expected O, but got Ref
		if ((object)_handle != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CameraFreezeToggle)+5C]");
			if ((nint)0 > (nint)0)
			{
				return;
			}
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
		if (!(_broker == null))
		{
			bool flag = default(bool);
			InteractionLockBroker.LockHandle handle = _broker.Acquire((InteractionLockBroker.LockRequest)(&flag));
			_handle = handle;
		}
		else
		{
			string message = "[CameraFreezeToggle] InteractionLockBroker not found (tag='" + brokerTag + "'). Cannot apply freeze lock.";
			Debug.LogWarning(message, this);
		}
	}

	private void ReleaseLock()
	{
		//IL_00b9: Expected O, but got I4
		if ((object)_handle == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CameraFreezeToggle)+5C]");
		if ((nint)0 > (nint)0)
		{
			if (_broker == null)
			{
				TryFindBroker();
			}
			if (_broker != null)
			{
				bool flag = _broker.Release(_handle);
			}
			_handle = (InteractionLockBroker.LockHandle)0;
		}
	}

	private void OnDisable()
	{
		ReleaseLock();
	}

	private void TryFindBroker()
	{
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(brokerTag);
		_broker = broker;
	}

	private void TryAutoWireReferences()
	{
		if (enableTagAutoWiring && playerVirtualCamera == null)
		{
			GameObject gameObject = FindGameObjectByTagSafe(virtualCameraTag);
			playerVirtualCamera = gameObject;
		}
	}

	private GameObject FindGameObjectByTagSafe(string tag)
	{
		if (!string.IsNullOrWhiteSpace(tag))
		{
			return GameObject.FindGameObjectWithTag(tag);
		}
		return null;
	}

	public CameraFreezeToggle()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC11]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		respectCameraZoneTrigger = true;
		lockFreezePlayerController = true;
		debugLabel = "CameraFreezeToggle";
		brokerTag = "LockBroker";
		enableTagAutoWiring = true;
		virtualCameraTag = "CMCam";
		base._002Ector();
	}
}
