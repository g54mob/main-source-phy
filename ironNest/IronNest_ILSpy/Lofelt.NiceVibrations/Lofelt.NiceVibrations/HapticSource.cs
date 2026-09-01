using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class HapticSource : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003C_002Ecctor_003Eb__22_0()
		{
			loadedHapticSource = null;
		}

		internal void _003C_002Ecctor_003Eb__22_1()
		{
			lastPlayedHapticSource = null;
		}
	}

	private const int DEFAULT_PRIORITY = 128;

	public HapticClip clip;

	public int priority;

	private float seekTime;

	private HapticPatterns.PresetType _fallbackPreset;

	private bool _loop;

	private float _level;

	private float _frequencyShift;

	private static HapticSource loadedHapticSource;

	private static HapticSource lastPlayedHapticSource;

	public HapticPatterns.PresetType fallbackPreset
	{
		get
		{
			return _fallbackPreset;
		}
		set
		{
			_fallbackPreset = value;
		}
	}

	public bool loop
	{
		get
		{
			return _loop;
		}
		set
		{
			_loop = value;
		}
	}

	public float level
	{
		get
		{
			return _level;
		}
		set
		{
			_level = value;
			if ((object)this == loadedHapticSource)
			{
				HapticController.clipLevel = _level;
			}
		}
	}

	public float frequencyShift
	{
		get
		{
			return _frequencyShift;
		}
		set
		{
			_frequencyShift = value;
			if ((object)this == loadedHapticSource)
			{
				bool flag = HapticController.Init();
			}
		}
	}

	static HapticSource()
	{
		//IL_01a8: Expected I, but got O
		//IL_01b1: Expected O, but got I4
		//IL_0224: Expected O, but got I4
		//IL_023a: Expected I, but got O
		//IL_0260: Expected O, but got I4
		//IL_0276: Expected I, but got O
		//IL_02a1: Expected I, but got O
		//IL_02aa: Expected O, but got I4
		Action b = delegate
		{
			loadedHapticSource = null;
		};
		Delegate obj = Delegate.Combine(HapticController.LoadedClipChanged, b);
		nint num;
		object obj3;
		Delegate obj4;
		if ((object)obj == null)
		{
			HapticController.LoadedClipChanged = null;
		}
		else
		{
			bool flag = (object)obj.GetType() != typeof(Action);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			if ((object)obj2 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				num = (nint)typeof(Action);
				obj3 = 0;
				obj4 = obj;
				goto IL_02e0;
			}
			HapticController.LoadedClipChanged = (Action)obj2;
			bool flag2 = (object)obj.GetType() != typeof(Action);
			Delegate obj5 = null;
			if (!flag2)
			{
				obj5 = obj;
			}
			bool flag3 = (object)obj5 == null;
			obj3 = 0;
			obj4 = obj;
			nint num2 = (nint)typeof(Action);
			if (flag3)
			{
				goto IL_02c5;
			}
		}
		Action b2 = delegate
		{
			lastPlayedHapticSource = null;
		};
		Delegate obj6 = Delegate.Combine(HapticController.PlaybackStarted, b2);
		if ((object)obj6 == null)
		{
			HapticController.PlaybackStarted = null;
			return;
		}
		bool flag4 = (object)obj6.GetType() != typeof(Action);
		Delegate obj7 = null;
		if (!flag4)
		{
			obj7 = obj6;
		}
		bool flag5 = (object)obj7 == null;
		obj3 = 0;
		obj4 = obj6;
		nint num3 = (nint)typeof(Action);
		if (flag5)
		{
			goto IL_02d0;
		}
		HapticController.PlaybackStarted = (Action)obj7;
		bool flag6 = (object)obj6.GetType() != typeof(Action);
		Delegate obj8 = null;
		if (!flag6)
		{
			obj8 = obj6;
		}
		bool flag7 = (object)obj8 == null;
		num = (nint)typeof(Action);
		obj3 = 0;
		obj4 = obj6;
		if (!flag7)
		{
			return;
		}
		goto IL_02e0;
		IL_02c5:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_02d0:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_02c5;
		IL_02e0:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_02d0;
	}

	public unsafe void Play()
	{
		//IL_00a8: Expected O, but got Ref
		//IL_00c6: Invalid comparison between F4 and I4
		if (HapticController.IsPlaying())
		{
			if (!(lastPlayedHapticSource != null))
			{
				return;
			}
			HapticSource hapticSource = lastPlayedHapticSource;
			if (priority > hapticSource.priority)
			{
				return;
			}
		}
		HapticClip hapticClip = clip;
		GamepadRumble gamepadRumble = default(GamepadRumble);
		HapticController.Load(hapticClip.json, (GamepadRumble)(&gamepadRumble));
		loadedHapticSource = this;
		HapticController.Loop(_loop);
		HapticController.clipLevel = _level;
		bool flag = HapticController.Init();
		bool flag2 = seekTime == 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A80B40h\"");
		if (!flag2 && !_loop)
		{
			HapticController.Seek(seekTime);
		}
		HapticController._fallbackPreset = _fallbackPreset;
		HapticController.Play();
		lastPlayedHapticSource = this;
	}

	private bool CanPlay()
	{
		//IL_00f0: Expected I4, but got O
		//IL_0062: Expected O, but got I4
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected I4, but got Unknown
		if (!HapticController.IsPlaying())
		{
			return true;
		}
		bool flag = lastPlayedHapticSource != null;
		if (!flag)
		{
			return flag;
		}
		HapticSource hapticSource = lastPlayedHapticSource;
		if ((object)lastPlayedHapticSource != null)
		{
			object obj = priority - hapticSource.priority;
			int num = priority ^ hapticSource.priority;
			int num2 = priority ^ obj;
			int num3 = num & num2;
			bool flag2 = num3 < 0;
			bool flag3 = (nint)obj < 0;
			bool flag4 = obj == null;
			bool flag5 = flag3 != flag2;
			return flag5 | flag4;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool IsLoaded()
	{
		object obj = (object)this - (object)loadedHapticSource;
		return obj == null;
	}

	public void Stop()
	{
		if ((object)this == loadedHapticSource)
		{
			HapticController.Stop();
		}
	}

	public void Seek(float time)
	{
		seekTime = time;
	}

	public void OnDisable()
	{
		if (HapticController.IsPlaying() && (object)this == loadedHapticSource && (object)this == loadedHapticSource)
		{
			HapticController.Stop();
		}
	}

	public HapticSource()
	{
		//IL_001a: Expected I4, but got I8
		priority = 128;
		_fallbackPreset = HapticPatterns.PresetType.None;
		_level = 1f;
		base._002Ector();
	}
}
