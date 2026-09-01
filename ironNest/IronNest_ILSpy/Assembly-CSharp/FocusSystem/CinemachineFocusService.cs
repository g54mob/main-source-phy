using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Unity.Cinemachine;
using UnityEngine;

namespace FocusSystem;

public class CinemachineFocusService : MonoBehaviour
{
	[Serializable]
	public class KeySettings
	{
		public string key;

		public int usageLimit;

		public KeySettings()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A846]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			key = "";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	private class ChannelState
	{
		public readonly HashSet<CinemachineFocusTarget> registered;

		public readonly HashSet<CinemachineFocusTarget> requesting;

		public int usedCount;

		public int usageLimit;

		public ChannelState()
		{
			HashSet<CinemachineFocusTarget> hashSet = new HashSet<CinemachineFocusTarget>();
			registered = hashSet;
			HashSet<CinemachineFocusTarget> hashSet2 = new HashSet<CinemachineFocusTarget>();
			requesting = hashSet2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	private static CinemachineFocusService _003CInstance_003Ek__BackingField;

	public CinemachineCamera focusCamera;

	public bool bindFollow = true;

	public bool bindLookAt;

	public bool disableCameraWhenIdle;

	public bool stickToCurrentUntilReleased;

	public bool preferSameKeyOnRelease = true;

	public bool verboseLogging;

	public List<KeySettings> keys;

	private readonly Dictionary<string, ChannelState> _channels;

	private CinemachineFocusTarget _currentTarget;

	private string _currentKey;

	public static CinemachineFocusService Instance
	{
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		private set
		{
			_003CInstance_003Ek__BackingField = value;
		}
	}

	public static bool HasInstance => _003CInstance_003Ek__BackingField != null;

	private void Awake()
	{
		if ((bool)_003CInstance_003Ek__BackingField && _003CInstance_003Ek__BackingField != this)
		{
			if (verboseLogging)
			{
				Debug.LogWarning("[CinemachineFocusService] Duplicate instance found. Destroying the new one.");
			}
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
			return;
		}
		_003CInstance_003Ek__BackingField = this;
		if (!focusCamera)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
			CinemachineCamera cinemachineCamera = default(CinemachineCamera);
			focusCamera = cinemachineCamera;
			bool flag = focusCamera;
			if (!flag && verboseLogging != flag)
			{
				Debug.LogWarning("[CinemachineFocusService] No CinemachineCamera assigned or found under this object.");
			}
		}
		RebuildChannelsFromConfig();
		ApplyIdleState();
	}

	private void OnDestroy()
	{
		if (_003CInstance_003Ek__BackingField == this)
		{
			_003CInstance_003Ek__BackingField = null;
		}
	}

	private void OnValidate()
	{
		//IL_004b: Expected O, but got I
		//IL_0083: Expected O, but got I
		//IL_0093: Expected O, but got I
		//IL_00db: Expected O, but got I
		List<KeySettings> list = keys;
		int num = 0;
		object obj = default(object);
		for (int num2 = 0; num2 < list._size; num2 = num)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ stack_18+10]");
				string text = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ stack_18+10]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rax_v27+B8]");
					object obj3 = 0;
					text = (string)obj3;
				}
				string a = text.Trim();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ stack_20+10]");
				if (!string.Equals(a, (string)0, StringComparison.Ordinal))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_-40+18]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					_ = 0;
				}
			}
			else
			{
				KeySettings value = new KeySettings();
				keys.set_Item(num, value);
			}
			list = keys;
			num++;
		}
		if (!Application.isPlaying)
		{
			RebuildChannelsFromConfig();
		}
	}

	private void RebuildChannelsFromConfig()
	{
		//IL_0041: Expected O, but got I4
		//IL_004a: Expected O, but got I4
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0086: Expected O, but got I
		//IL_00be: Expected O, but got I
		//IL_00ce: Expected O, but got I
		//IL_0168: Expected O, but got I4
		//IL_0186: Expected O, but got I
		_channels.Clear();
		HashSet<string> hashSet = new HashSet<string>(StringComparer.s_ordinal);
		List<KeySettings> list = keys;
		object obj = 0;
		object obj2 = 0;
		object obj3 = default(object);
		object obj6 = default(object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ stack_8_v3+10]");
				string text = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ stack_8_v3+10]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v25+B8]");
					object obj5 = 0;
					text = (string)obj5;
				}
				string text2 = text.Trim();
				if (!string.IsNullOrEmpty(text2))
				{
					hashSet.Add(text2);
					if (obj6 != null)
					{
						ChannelState value = new ChannelState();
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ stack_8_v3+18]");
						bool flag = (nint)0 < (nint)0;
						object obj7 = 0;
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ stack_8_v3+18]");
							obj7 = 0;
						}
						_channels.set_Item(text2, value);
					}
					else if (verboseLogging)
					{
						string message = "[CinemachineFocusService] Duplicate key '" + text2 + "' found in config (keeping first, ignoring later entries).";
						Debug.LogWarning(message);
					}
				}
			}
			list = keys;
			obj++;
			obj2 = obj;
		}
	}

	private unsafe bool TryGetChannel(string key, out ChannelState state)
	{
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		//IL_00cf: Expected I4, but got O
		bool flag = key != null;
		string text = key;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v10+B8]");
			object obj2 = 0;
			text = (string)obj2;
			if (obj2 == null)
			{
				goto IL_00c1;
			}
		}
		string text2 = text.Trim();
		if (!string.IsNullOrEmpty(text2))
		{
			if (_channels != null)
			{
				return _channels.TryGetValue(text2, out state);
			}
			goto IL_00c1;
		}
		ref ChannelState reference = ref *(ChannelState*)null;
		return false;
		IL_00c1:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool IsKeyEligibleForNewGrab(string key)
	{
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		//IL_01a3: Expected I4, but got O
		//IL_0125: Expected O, but got I4
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected I4, but got Unknown
		bool flag = key != null;
		string text = key;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rax_v14+B8]");
			object obj2 = 0;
			text = (string)obj2;
			if (obj2 == null)
			{
				goto IL_0195;
			}
		}
		string text2 = text.Trim();
		if (string.IsNullOrEmpty(text2))
		{
			goto IL_018f;
		}
		if (_channels != null)
		{
			if (!_channels.TryGetValue(text2, out var value))
			{
				goto IL_018f;
			}
			if (value != null)
			{
				if (value.usageLimit == 0)
				{
					return true;
				}
				object obj3 = value.usedCount - value.usageLimit;
				int num = value.usedCount ^ value.usageLimit;
				int num2 = value.usedCount ^ obj3;
				int num3 = num & num2;
				bool flag2 = num3 < 0;
				bool flag3 = (nint)obj3 < 0;
				return flag3 != flag2;
			}
		}
		goto IL_0195;
		IL_0195:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_018f:
		return false;
	}

	public void RegisterTarget(CinemachineFocusTarget target)
	{
		//IL_004d: Expected O, but got I
		//IL_005d: Expected O, but got I
		if (!target)
		{
			return;
		}
		string text = target.key;
		if (target.key == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rax_v36+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		string text2 = text.Trim();
		if (!string.IsNullOrEmpty(text2) && _channels.TryGetValue(text2, out var value))
		{
			value.registered.Add(target);
			if (verboseLogging)
			{
				string arg = target.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"[CinemachineFocusService] Registered target '{arg}' (key '{target.key}', prio {arg2}).";
				Debug.Log(message);
			}
		}
		else if (verboseLogging)
		{
			string text3 = target.name;
			string message2 = "[CinemachineFocusService] Target '" + text3 + "' registered with unknown key '" + target.key + "'. It cannot grab focus until this key is added to the service.";
			Debug.LogWarning(message2);
		}
	}

	public void UnregisterTarget(CinemachineFocusTarget target)
	{
		//IL_004d: Expected O, but got I
		//IL_005d: Expected O, but got I
		if (!target)
		{
			return;
		}
		string text = target.key;
		if (target.key == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v43+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		string text2 = text.Trim();
		if (string.IsNullOrEmpty(text2) || !_channels.TryGetValue(text2, out var value))
		{
			return;
		}
		bool flag = value.registered.Remove(target);
		bool flag2 = value.requesting.Remove(target);
		bool flag3 = _currentTarget == target;
		bool flag4 = !flag3;
		string preferKey = null;
		if (!flag4)
		{
			if (verboseLogging)
			{
				string text3 = target.name;
				string message = "[CinemachineFocusService] Current target '" + text3 + "' unregistered. Re-evaluating.";
				Debug.Log(message);
			}
			_currentTarget = null;
			_currentKey = null;
			bool flag5 = !preferSameKeyOnRelease;
			preferKey = null;
			if (!flag5)
			{
				preferKey = target.key;
			}
		}
		Evaluate(preferKey);
		if (verboseLogging)
		{
			string text4 = target.name;
			string message2 = "[CinemachineFocusService] Unregistered target '" + text4 + "' (key '" + target.key + "').";
			Debug.Log(message2);
		}
	}

	public bool RequestFocus(CinemachineFocusTarget target)
	{
		//IL_004d: Expected O, but got I
		//IL_005d: Expected O, but got I
		//IL_04da: Expected I4, but got O
		if ((bool)target)
		{
			string text = target.key;
			if (target.key == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rax_v58+B8]");
				object obj2 = 0;
				text = (string)obj2;
			}
			string text2 = text.Trim();
			if (!string.IsNullOrEmpty(text2) && _channels.TryGetValue(text2, out var value))
			{
				value.requesting.Add(target);
				if (!stickToCurrentUntilReleased || !(_currentTarget != null))
				{
					Evaluate(null);
					if (_currentTarget == target)
					{
						return _currentTarget != _currentTarget;
					}
					goto IL_0494;
				}
				if (!verboseLogging)
				{
					goto IL_02b0;
				}
				string[] array = new string[5];
				if (array.Length > 0)
				{
					array[0] = "[CinemachineFocusService] Focus requested by '";
					string text3 = target.name;
					if (array.Length > 1)
					{
						array[1] = text3;
						if (array.Length > 2)
						{
							array[2] = "', but sticking to current '";
							string text4 = _currentTarget.name;
							if (array.Length > 3)
							{
								array[3] = text4;
								if (array.Length > 4)
								{
									array[4] = "'.";
									string message = string.Concat(array);
									Debug.Log(message);
									goto IL_02b0;
								}
							}
						}
					}
				}
			}
			else
			{
				if (!verboseLogging)
				{
					goto IL_0494;
				}
				string[] array2 = new string[5];
				if (array2.Length > 0)
				{
					array2[0] = "[CinemachineFocusService] Target '";
					string text5 = target.name;
					if (array2.Length > 1)
					{
						array2[1] = text5;
						if (array2.Length > 2)
						{
							array2[2] = "' requested focus for unknown key '";
							if (array2.Length > 3)
							{
								array2[3] = target.key;
								if (array2.Length > 4)
								{
									array2[4] = "'. Request denied.";
									string message2 = string.Concat(array2);
									Debug.LogWarning(message2);
									goto IL_0494;
								}
							}
						}
					}
				}
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
		}
		goto IL_0494;
		IL_0494:
		return false;
		IL_02b0:
		return _currentTarget == target;
	}

	public void ReleaseFocus(CinemachineFocusTarget target)
	{
		//IL_004d: Expected O, but got I
		//IL_005d: Expected O, but got I
		if (!target)
		{
			return;
		}
		string text = target.key;
		if (target.key == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v29+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		string text2 = text.Trim();
		if (!string.IsNullOrEmpty(text2) && _channels.TryGetValue(text2, out var value))
		{
			bool flag = value.requesting.Remove(target);
		}
		string preferKey;
		if (!(_currentTarget == target))
		{
			preferKey = null;
		}
		else
		{
			if (verboseLogging)
			{
				string text3 = target.name;
				string message = "[CinemachineFocusService] Target '" + text3 + "' released focus. Re-evaluating.";
				Debug.Log(message);
			}
			_currentTarget = null;
			_currentKey = null;
			bool flag2 = !preferSameKeyOnRelease;
			string text4 = null;
			if (!flag2)
			{
				text4 = target.key;
			}
			preferKey = text4;
		}
		Evaluate(preferKey);
	}

	private void Evaluate(string preferKey)
	{
		//IL_00a2: Expected O, but got I
		//IL_025d: Expected O, but got I
		//IL_026d: Expected O, but got I
		//IL_00b7: Expected O, but got I
		//IL_00c7: Expected O, but got I
		if (stickToCurrentUntilReleased && _currentTarget != null)
		{
			UnityEngine.Object currentTarget = _currentTarget;
			if ((bool)_currentTarget && _currentTarget.isActiveAndEnabled)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rsi_v12 (UnityEngine.Object)+20]");
				string text = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rsi_v12 (UnityEngine.Object)+20]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v353 @ rax_v53+B8]");
					object obj2 = 0;
					text = (string)obj2;
				}
				string text2 = text.Trim();
				if (!string.IsNullOrEmpty(text2) && _channels.TryGetValue(text2, out var value) && value.requesting.Contains(_currentTarget))
				{
					goto IL_032e;
				}
			}
			_currentTarget = null;
			_currentKey = null;
		}
		bool flag = string.IsNullOrEmpty(preferKey);
		string selectedKey = null;
		CinemachineFocusTarget cinemachineFocusTarget = null;
		if (!flag)
		{
			CinemachineFocusTarget cinemachineFocusTarget2 = SelectBestFromKey(preferKey, out selectedKey);
			cinemachineFocusTarget = cinemachineFocusTarget2;
		}
		if (cinemachineFocusTarget == null)
		{
			CinemachineFocusTarget cinemachineFocusTarget3 = SelectBestAcrossAllKeys(out selectedKey);
			cinemachineFocusTarget = cinemachineFocusTarget3;
		}
		if (cinemachineFocusTarget != _currentTarget)
		{
			if (cinemachineFocusTarget != null)
			{
				bool flag2 = selectedKey != null;
				string text3 = selectedKey;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v659 @ rax_v29+B8]");
					object obj4 = 0;
					text3 = (string)obj4;
				}
				string text4 = text3.Trim();
				if (!string.IsNullOrEmpty(text4) && _channels.TryGetValue(text4, out var value2))
				{
					if (value2.usageLimit != 0 && value2.usedCount >= value2.usageLimit)
					{
						selectedKey = null;
						cinemachineFocusTarget = null;
					}
					else
					{
						int usedCount = value2.usedCount + 1;
						value2.usedCount = usedCount;
					}
				}
			}
			_currentTarget = cinemachineFocusTarget;
			_currentKey = selectedKey;
		}
		goto IL_032e;
		IL_032e:
		ApplyBinding(_currentTarget);
	}

	private bool IsStillEligible(CinemachineFocusTarget t)
	{
		//IL_01af: Expected I4, but got O
		//IL_0090: Expected O, but got I
		//IL_00a0: Expected O, but got I
		if ((bool)t)
		{
			if ((object)t == null)
			{
				goto IL_01a1;
			}
			if (t.isActiveAndEnabled)
			{
				string text = t.key;
				if (t.key == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rax_v17+B8]");
					object obj2 = 0;
					text = (string)obj2;
					if (obj2 == null)
					{
						goto IL_01a1;
					}
				}
				string text2 = text.Trim();
				if (!string.IsNullOrEmpty(text2))
				{
					if (_channels != null)
					{
						if (!_channels.TryGetValue(text2, out var value))
						{
							goto IL_019b;
						}
						if (value != null && value.requesting != null)
						{
							return value.requesting.Contains(t);
						}
					}
					goto IL_01a1;
				}
			}
		}
		goto IL_019b;
		IL_019b:
		return false;
		IL_01a1:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private unsafe CinemachineFocusTarget SelectBestFromKey(string key, out string selectedKey)
	{
		//IL_0022: Expected O, but got I
		//IL_0032: Expected O, but got I
		//IL_018f: Expected O, but got I8
		//IL_02b0: Expected O, but got I
		ref string reference = ref *(string*)null;
		string text;
		if (key != null)
		{
			text = key;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v41+B8]");
			object obj2 = 0;
			text = (string)obj2;
			if (obj2 == null)
			{
				goto IL_032d;
			}
		}
		string text2 = text.Trim();
		if (string.IsNullOrEmpty(text2))
		{
			goto IL_0323;
		}
		if (_channels != null)
		{
			if (!_channels.TryGetValue(text2, out var value) || (_currentTarget == null && !IsKeyEligibleForNewGrab(key)))
			{
				goto IL_0323;
			}
			if (value != null && value.requesting != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
				UnityEngine.Object obj3 = null;
				int num = 2147483647;
				object obj4 = 2147483648L;
				HashSet<CinemachineFocusTarget>.Enumerator enumerator = default(HashSet<CinemachineFocusTarget>.Enumerator);
				UnityEngine.Object obj5 = default(UnityEngine.Object);
				while (true)
				{
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (!obj5)
						{
							continue;
						}
						if ((object)obj5 == null)
						{
							break;
						}
						if (!((Behaviour)obj5).isActiveAndEnabled)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ stack_-68 (UnityEngine.Object)+28]");
						if (0 <= (nint)obj4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ stack_-68 (UnityEngine.Object)+28]");
							if (0 == (nint)obj4)
							{
								int instanceID = obj5.GetInstanceID();
								if (instanceID < num)
								{
									obj3 = obj5;
									num = instanceID;
								}
							}
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ stack_-68 (UnityEngine.Object)+28]");
							obj4 = 0;
							int instanceID2 = obj5.GetInstanceID();
							obj3 = obj5;
							num = instanceID2;
						}
						continue;
					}
					enumerator.Dispose();
					if (obj3 != null)
					{
						reference = ref *(string*)key;
					}
					return (CinemachineFocusTarget)obj3;
				}
				throw new NullReferenceException();
			}
		}
		goto IL_032d;
		IL_032d:
		throw new NullReferenceException();
		IL_0323:
		return null;
	}

	private unsafe CinemachineFocusTarget SelectBestAcrossAllKeys(out string selectedKey)
	{
		//IL_0028: Expected O, but got I8
		//IL_00e7: Expected O, but got I
		//IL_0245: Expected O, but got I
		ref string reference = ref *(string*)null;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
		int num = 2147483647;
		object obj = 2147483648L;
		CinemachineFocusTarget result = null;
		Dictionary<string, ChannelState>.Enumerator enumerator = default(Dictionary<string, ChannelState>.Enumerator);
		string text = default(string);
		object obj2 = default(object);
		HashSet<CinemachineFocusTarget>.Enumerator enumerator2 = default(HashSet<CinemachineFocusTarget>.Enumerator);
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
				bool flag = _currentTarget == null;
				bool flag2 = !flag;
				CinemachineFocusService currentTarget = (CinemachineFocusService)(object)_currentTarget;
				if (!flag2)
				{
					bool flag3 = IsKeyEligibleForNewGrab(text);
					bool flag4 = !flag3;
					currentTarget = this;
					if (flag4)
					{
						continue;
					}
				}
				if (obj2 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ stack_-C8_v5+18]");
				currentTarget = (CinemachineFocusService)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ stack_-C8_v5+18]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
					while (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (!obj3)
						{
							continue;
						}
						bool flag5 = (object)obj3 == null;
						currentTarget = (CinemachineFocusService)obj3;
						if (!flag5)
						{
							if (!((Behaviour)obj3).isActiveAndEnabled)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ stack_-C0 (UnityEngine.Object)+28]");
							if (0 <= (nint)obj)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ stack_-C0 (UnityEngine.Object)+28]");
								if (0 == (nint)obj)
								{
									int instanceID = obj3.GetInstanceID();
									if (instanceID < num)
									{
										reference = ref *(string*)text;
										num = instanceID;
										result = (CinemachineFocusTarget)obj3;
									}
								}
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ stack_-C0 (UnityEngine.Object)+28]");
								obj = 0;
								reference = ref *(string*)text;
								int instanceID2 = obj3.GetInstanceID();
								num = instanceID2;
								result = (CinemachineFocusTarget)obj3;
							}
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator2.Dispose();
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return result;
		}
		throw new NullReferenceException();
	}

	private void ApplyBinding(CinemachineFocusTarget target)
	{
		if ((bool)focusCamera)
		{
			object message;
			if (!target)
			{
				if (~(bindFollow ? 1u : 0u) == 0)
				{
					focusCamera.Follow = null;
				}
				if (bindLookAt)
				{
					focusCamera.LookAt = null;
				}
				ApplyIdleState();
				if (!verboseLogging)
				{
					return;
				}
				message = "[CinemachineFocusService] No eligible target. Camera unbound.";
			}
			else
			{
				Transform follow;
				if (~(bindFollow ? 1u : 0u) == 0 && target.overrideFollow)
				{
					if ((bool)target.followOverride)
					{
						follow = target.followOverride;
					}
					else
					{
						Transform transform = target.transform;
						follow = transform;
					}
				}
				else
				{
					follow = null;
				}
				bool flag = !bindLookAt;
				Transform lookAt = null;
				if (!flag)
				{
					bool flag2 = !target.overrideLookAt;
					lookAt = null;
					if (!flag2)
					{
						if ((bool)target.lookAtOverride)
						{
							lookAt = target.lookAtOverride;
						}
						else
						{
							Transform transform2 = target.transform;
							lookAt = transform2;
						}
					}
				}
				if (bindFollow)
				{
					CinemachineCamera cinemachineCamera = focusCamera;
					cinemachineCamera.Follow = follow;
				}
				if (bindLookAt)
				{
					CinemachineCamera cinemachineCamera2 = focusCamera;
					cinemachineCamera2.LookAt = lookAt;
				}
				if (disableCameraWhenIdle)
				{
					GameObject gameObject = focusCamera.gameObject;
					if (!gameObject.activeSelf)
					{
						GameObject gameObject2 = focusCamera.gameObject;
						gameObject2.SetActive(value: true);
					}
				}
				if (!verboseLogging)
				{
					return;
				}
				string text2;
				if (bindFollow)
				{
					UnityEngine.Object follow2 = focusCamera.Follow;
					if ((bool)follow2)
					{
						UnityEngine.Object follow3 = focusCamera.Follow;
						string text = follow3.name;
						text2 = text;
					}
					else
					{
						text2 = "null";
					}
				}
				else
				{
					text2 = "off";
				}
				string text4;
				if (bindLookAt)
				{
					UnityEngine.Object lookAt2 = focusCamera.LookAt;
					if ((bool)lookAt2)
					{
						UnityEngine.Object lookAt3 = focusCamera.LookAt;
						string text3 = lookAt3.name;
						text4 = text3;
					}
					else
					{
						text4 = "null";
					}
				}
				else
				{
					text4 = "off";
				}
				string text5 = target.name;
				string text6 = "[CinemachineFocusService] Bound camera to '" + text5 + "' (Key='" + _currentKey + "', Follow=" + text2 + ", LookAt=" + text4 + ").";
				message = text6;
			}
			Debug.Log(message);
		}
		else if (verboseLogging)
		{
			Debug.LogWarning("[CinemachineFocusService] Cannot bind: focusCamera not assigned.");
		}
	}

	private void ApplyIdleState()
	{
		if ((bool)focusCamera && disableCameraWhenIdle)
		{
			GameObject gameObject = focusCamera.gameObject;
			if (gameObject.activeSelf)
			{
				GameObject gameObject2 = focusCamera.gameObject;
				gameObject2.SetActive(value: false);
			}
		}
	}

	private void Context_RebuildKeys()
	{
		RebuildChannelsFromConfig();
		if (verboseLogging)
		{
			Debug.Log("[CinemachineFocusService] Rebuilt key channels from config and reset usage counts.");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 58 Invalid \"Jump target not found in method: 0x1804E80F0\"");
	}

	private void Context_ClearBinding()
	{
		_currentTarget = null;
		_currentKey = null;
		ApplyBinding(null);
	}

	private unsafe void Context_LogUsage()
	{
		//IL_0018: Expected O, but got I4
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected I4, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
		object obj = 0;
		Dictionary<string, ChannelState>.Enumerator enumerator = default(Dictionary<string, ChannelState>.Enumerator);
		object obj2 = default(object);
		object arg2 = default(object);
		object arg3 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
				if (obj2 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_18_v3+24]");
				string arg;
				if ((nint)0 == 0)
				{
					arg = "unlimited";
				}
				else
				{
					int num = obj2 + 36;
					string text = ((int*)num)->ToString();
					arg = text;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				string message = $"[CinemachineFocusService] Key '{arg2}': Used={arg3} / Limit={arg}";
				Debug.Log(message);
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public CinemachineFocusService()
	{
		List<KeySettings> list = new List<KeySettings>();
		KeySettings keySettings = new KeySettings();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A846]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		keySettings.key = "";
		keySettings.key = "Player";
		keySettings.usageLimit = 0;
		list.Add(keySettings);
		keys = list;
		_channels = new Dictionary<string, ChannelState>(StringComparer.s_ordinal);
		base._002Ector();
	}
}
