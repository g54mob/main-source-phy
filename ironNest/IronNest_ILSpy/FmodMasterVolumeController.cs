using Cpp2ILInjected;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public sealed class FmodMasterVolumeController : MonoBehaviour
{
	public float masterVolumeLinear;

	public bool applyEveryFrame;

	public string masterBusPath;

	private Bus _masterBus;

	private bool _busResolved;

	private bool _applyPending;

	private void Awake()
	{
	}

	private void OnEnable()
	{
		ResolveBusIfNeeded();
		ApplyVolume(masterVolumeLinear);
		_applyPending = false;
	}

	private void Update()
	{
		if (!applyEveryFrame)
		{
			if (_applyPending)
			{
				ResolveBusIfNeeded();
				ApplyVolume(masterVolumeLinear);
				_applyPending = false;
			}
		}
		else
		{
			ResolveBusIfNeeded();
			ApplyVolume(masterVolumeLinear);
		}
	}

	private void ResolveBusIfNeeded()
	{
		if (!_busResolved)
		{
			if (!string.IsNullOrWhiteSpace(masterBusPath))
			{
				Bus bus = RuntimeManager.GetBus(masterBusPath);
				_masterBus = bus;
				_busResolved = true;
			}
			else
			{
				UnityEngine.Debug.LogWarning("[FMOD] Master bus path is empty. Expected 'bus:/'.");
			}
		}
	}

	private unsafe void ApplyVolume(float linearVolume)
	{
		//IL_000e: Invalid comparison between I4 and F4
		//IL_0064: Expected F4, but got I4
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_0072: Expected I4, but got O
		if (!_busResolved)
		{
			return;
		}
		float volume;
		if (!(0f > linearVolume))
		{
			bool flag = !(linearVolume > 2f);
			volume = linearVolume;
			if (!flag)
			{
				volume = 2f;
			}
		}
		else
		{
			volume = 0f;
		}
		Bus bus = (Bus)(this + 48);
		if (((Bus*)bus)->setVolume(volume) != RESULT.OK)
		{
			object obj = default(object);
			object arg = (RESULT)obj;
			string message = $"[FMOD] setVolume failed on '{masterBusPath}' with result: {arg}";
			UnityEngine.Debug.LogWarning(message);
		}
	}

	public FmodMasterVolumeController()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AAA0]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		masterVolumeLinear = 1f;
		masterBusPath = "bus:/";
		base._002Ector();
	}
}
