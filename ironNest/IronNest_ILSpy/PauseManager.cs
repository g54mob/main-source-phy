using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
	public enum AudioPauseMode
	{
		None,
		AudioListenerPause,
		AudioMixerSnapshots,
		PauseAudioSources,
		FMOD
	}

	public enum FocusPauseScope
	{
		Disabled,
		RuntimeOnly,
		EditorAndRuntime
	}

	private bool pauseOnEnable;

	private FocusPauseScope focusPauseScope = FocusPauseScope.RuntimeOnly;

	private InputActionReference toggleAction;

	private bool restoreTimeScaleToOne = true;

	private AudioPauseMode audioPauseMode = AudioPauseMode.AudioListenerPause;

	private AudioMixerSnapshot pausedSnapshot;

	private AudioMixerSnapshot unpausedSnapshot;

	private float snapshotTransitionTime = 0.1f;

	private UnityEvent onPaused;

	private UnityEvent onUnpaused;

	private static int s_pauseRequestCount = 0;

	private static float s_originalFixedDeltaTime = -1f;

	private static float s_savedPrePauseTimeScale = 1f;

	private static bool s_savedAudioListenerPause = false;

	private readonly List<AudioSource> m_pausedAudioSources;

	private bool m_requestedPauseOnEnable;

	private bool m_pausedByFocus;

	private static bool _003CPauseOnFocusLoss_003Ek__BackingField;

	public static bool IsPaused
	{
		get
		{
			int num = s_pauseRequestCount ^ s_pauseRequestCount;
			int num2 = s_pauseRequestCount & num;
			bool flag = num2 < 0;
			bool flag2 = s_pauseRequestCount < 0;
			bool flag3 = s_pauseRequestCount == 0;
			bool flag4 = flag2 == flag;
			bool flag5 = !flag3;
			return flag5 & flag4;
		}
	}

	public static bool PauseOnFocusLoss
	{
		get
		{
			return _003CPauseOnFocusLoss_003Ek__BackingField;
		}
		set
		{
			_003CPauseOnFocusLoss_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		//IL_0031: Invalid comparison between I4 and F4
		if (0f > s_originalFixedDeltaTime)
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			s_originalFixedDeltaTime = fixedDeltaTime;
		}
	}

	private void OnEnable()
	{
		if (toggleAction != null)
		{
			InputAction action = toggleAction.action;
			if (action != null)
			{
				InputAction action2 = toggleAction.action;
				Action<InputAction.CallbackContext> value = OnToggleActionPerformed;
				action2.performed += value;
				InputAction action3 = toggleAction.action;
				action3.Enable();
			}
		}
		if (pauseOnEnable)
		{
			RequestPause();
			m_requestedPauseOnEnable = true;
		}
	}

	private void OnDisable()
	{
		if (toggleAction != null)
		{
			InputAction action = toggleAction.action;
			if (action != null)
			{
				InputAction action2 = toggleAction.action;
				Action<InputAction.CallbackContext> value = OnToggleActionPerformed;
				action2.performed -= value;
				InputAction action3 = toggleAction.action;
				action3.Disable();
			}
		}
		if (pauseOnEnable && m_requestedPauseOnEnable)
		{
			ReleasePause();
			m_requestedPauseOnEnable = false;
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (!_003CPauseOnFocusLoss_003Ek__BackingField || focusPauseScope == FocusPauseScope.Disabled)
		{
			return;
		}
		if (focusPauseScope == FocusPauseScope.RuntimeOnly)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180742270");
			object obj = default(object);
			if (obj != null)
			{
				return;
			}
		}
		if (hasFocus)
		{
			if (m_pausedByFocus)
			{
				ReleasePause();
				m_pausedByFocus = false;
			}
		}
		else if (!IsPaused)
		{
			RequestPause();
			m_pausedByFocus = true;
		}
	}

	private void OnToggleActionPerformed(InputAction.CallbackContext ctx)
	{
		if (s_pauseRequestCount <= 0)
		{
			RequestPause();
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 88 Invalid \"Jump target not found in method: 0x180524A50\"");
		}
	}

	public void TogglePause()
	{
		if (s_pauseRequestCount <= 0)
		{
			RequestPause();
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 88 Invalid \"Jump target not found in method: 0x180524A50\"");
		}
	}

	public void RequestPause()
	{
		//IL_0076: Expected I, but got O
		//IL_00cc: Expected I, but got O
		nint num = (nint)typeof(PauseManager);
		if (s_pauseRequestCount == 0)
		{
			float timeScale = Time.timeScale;
			s_savedPrePauseTimeScale = timeScale;
			ApplyAudioPause();
			Time.timeScale = 0f;
			float timeScale2 = Time.timeScale;
			float fixedDeltaTime = s_originalFixedDeltaTime * timeScale2;
			Time.fixedDeltaTime = fixedDeltaTime;
			if (onPaused != null)
			{
				onPaused.Invoke();
			}
			num = (nint)typeof(PauseManager);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdx_v3 (Il2CppClass<PauseManager>)+E4]");
		bool flag = (nint)0 < (nint)0;
		int num2 = s_pauseRequestCount + 1;
		int num3 = 0;
		if (!flag)
		{
			num3 = num2;
		}
		s_pauseRequestCount = num3;
	}

	public void ReleasePause()
	{
		//IL_0088: Expected I, but got O
		nint num = (nint)typeof(PauseManager);
		if (s_pauseRequestCount <= 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdx_v2 (Il2CppClass<PauseManager>)+E4]");
		bool flag = (nint)0 < (nint)0;
		int num2 = s_pauseRequestCount - 1;
		int num3 = 0;
		if (!flag)
		{
			num3 = num2;
		}
		s_pauseRequestCount = num3;
		if (s_pauseRequestCount == 0)
		{
			RestoreAudioOnUnpause();
			float timeScale = ((!restoreTimeScaleToOne) ? s_savedPrePauseTimeScale : 1f);
			Time.timeScale = timeScale;
			Time.fixedDeltaTime = s_originalFixedDeltaTime;
			if (onUnpaused != null)
			{
				onUnpaused.Invoke();
			}
		}
	}

	private void ApplyAudioPause()
	{
		//IL_0015: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_0215: Invalid comparison between I4 and F4
		//IL_0224: Expected F4, but got I4
		//IL_011e: Expected O, but got I4
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		bool flag = audioPauseMode == AudioPauseMode.None;
		if (flag)
		{
			return;
		}
		object obj = audioPauseMode - 1;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (flag)
			{
				if (pausedSnapshot != null)
				{
					bool flag2 = 0f > snapshotTransitionTime;
					float timeToReach = 0f;
					if (!flag2)
					{
						timeToReach = snapshotTransitionTime;
					}
					pausedSnapshot.TransitionTo(timeToReach);
				}
				return;
			}
			object obj3 = obj2 - 1;
			if (flag)
			{
				m_pausedAudioSources.Clear();
				AudioSource[] array = UnityEngine.Object.FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
				object obj4 = 0;
				while ((nint)obj4 < array.Length)
				{
					if (array[obj4] != null && array[obj4].isPlaying)
					{
						m_pausedAudioSources.Add(array[obj4]);
						array[obj4].Pause();
					}
					obj4++;
				}
				return;
			}
			if ((nint)obj3 != 1)
			{
				return;
			}
			FMOD.Studio.System studioSystem = RuntimeManager.StudioSystem;
			FMOD.Studio.System system = default(FMOD.Studio.System);
			if (system.isValid())
			{
				RuntimeManager.PauseAllEvents(paused: true);
				FMOD.System coreSystem = RuntimeManager.CoreSystem;
				FMOD.System system2 = default(FMOD.System);
				RESULT rESULT = system2.mixerSuspend();
			}
		}
		bool pause = AudioListener.pause;
		s_savedAudioListenerPause = pause;
		AudioListener.pause = true;
	}

	private void RestoreAudioOnUnpause()
	{
		//IL_0015: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_0056: Expected O, but got I4
		//IL_01ae: Invalid comparison between I4 and F4
		//IL_01bd: Expected F4, but got I4
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		bool flag = audioPauseMode == AudioPauseMode.None;
		if (flag)
		{
			return;
		}
		object obj = audioPauseMode - 1;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (flag)
			{
				if (unpausedSnapshot != null)
				{
					bool flag2 = 0f > snapshotTransitionTime;
					float timeToReach = 0f;
					if (!flag2)
					{
						timeToReach = snapshotTransitionTime;
					}
					unpausedSnapshot.TransitionTo(timeToReach);
				}
				return;
			}
			object obj3 = obj2 - 1;
			object obj4 = 0;
			if (flag)
			{
				UnityEngine.Object obj5 = default(UnityEngine.Object);
				while (true)
				{
					List<AudioSource> pausedAudioSources = m_pausedAudioSources;
					if ((nint)obj4 >= pausedAudioSources._size)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj5 != null)
					{
						((AudioSource)obj5).UnPause();
					}
					obj4++;
				}
				m_pausedAudioSources.Clear();
				return;
			}
			if ((nint)obj3 != 1)
			{
				return;
			}
			FMOD.Studio.System studioSystem = RuntimeManager.StudioSystem;
			FMOD.Studio.System system = default(FMOD.Studio.System);
			if (system.isValid())
			{
				RuntimeManager.PauseAllEvents(paused: false);
				FMOD.System coreSystem = RuntimeManager.CoreSystem;
				FMOD.System system2 = default(FMOD.System);
				RESULT rESULT = system2.mixerResume();
			}
		}
		AudioListener.pause = s_savedAudioListenerPause;
	}

	public static void RequestGlobalPause()
	{
		InstanceRequestHelper(request: true);
	}

	public static void ReleaseGlobalPause()
	{
		InstanceRequestHelper(request: false);
	}

	private static void InstanceRequestHelper(bool request)
	{
		PauseManager pauseManager = UnityEngine.Object.FindAnyObjectByType<PauseManager>();
		if (pauseManager != null)
		{
			if (!request)
			{
				pauseManager.ReleasePause();
			}
			else
			{
				pauseManager.RequestPause();
			}
		}
	}

	public PauseManager()
	{
		List<AudioSource> pausedAudioSources = new List<AudioSource>();
		m_pausedAudioSources = pausedAudioSources;
		base._002Ector();
	}
}
