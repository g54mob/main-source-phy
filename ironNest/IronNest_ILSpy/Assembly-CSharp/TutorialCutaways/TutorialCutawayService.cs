using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace TutorialCutaways;

public class TutorialCutawayService : MonoBehaviour
{
	[Serializable]
	public class KeySettings
	{
		public string key;

		public int usageLimit;

		public KeySettings()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A817]");
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
		public readonly HashSet<TutorialCutawayCue> registered;

		public readonly HashSet<TutorialCutawayCue> pending;

		public int usedCount;

		public int usageLimit;

		public ChannelState()
		{
			HashSet<TutorialCutawayCue> hashSet = new HashSet<TutorialCutawayCue>();
			registered = hashSet;
			HashSet<TutorialCutawayCue> hashSet2 = new HashSet<TutorialCutawayCue>();
			pending = hashSet2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	private static TutorialCutawayService _003CInstance_003Ek__BackingField;

	public string serviceTag = "TutorialCutawayService";

	public List<KeySettings> keys;

	public bool verboseLogging;

	private readonly Dictionary<string, ChannelState> _channels;

	private TutorialCutawayCue _active;

	private readonly List<TutorialCutawayCue> _tempList;

	public static TutorialCutawayService Instance
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

	public TutorialCutawayCue ActiveCue => _active;

	private void Awake()
	{
		if ((bool)_003CInstance_003Ek__BackingField && _003CInstance_003Ek__BackingField != this)
		{
			if (verboseLogging)
			{
				Debug.LogWarning("[TutorialCutawayService] Duplicate instance detected. Destroying new one.");
			}
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
			return;
		}
		_003CInstance_003Ek__BackingField = this;
		GameObject go = base.gameObject;
		if (!CompareTagSafe(go, serviceTag))
		{
			string message = "[TutorialCutawayService] GameObject is not tagged '" + serviceTag + "'. For tag-based discovery, create/add this tag in Project Settings and assign it.";
			Debug.LogWarning(message);
		}
		RebuildChannelsFromInspector();
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
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		//IL_00d5: Expected O, but got I
		//IL_010d: Expected O, but got I
		//IL_011d: Expected O, but got I
		string text = serviceTag;
		if (serviceTag == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rax_v25+B8]");
			object obj2 = 0;
			text = (string)obj2;
			bool flag = obj2 == null;
			string text2 = (string)obj2;
			if (flag)
			{
				goto IL_01bf;
			}
		}
		string text3 = text.Trim();
		serviceTag = text3;
		if (keys != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<KeySettings>.Enumerator enumerator = default(List<KeySettings>.Enumerator);
			object obj3 = default(object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (obj3 == null)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ stack_18_v3+10]");
					string text4 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ stack_18_v3+10]");
					if ((nint)0 == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						object obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ rax_v24+B8]");
						object obj5 = 0;
						text4 = (string)obj5;
						if (obj5 == null)
						{
							break;
						}
					}
					string text5 = text4.Trim();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ stack_18_v3+18]");
					if ((nint)0 < (nint)0)
					{
						_ = 0;
					}
					continue;
				}
				enumerator.Dispose();
				if (!Application.isPlaying)
				{
					RebuildChannelsFromInspector();
				}
				return;
			}
			throw new NullReferenceException();
		}
		goto IL_01bf;
		IL_01bf:
		throw new NullReferenceException();
	}

	private void RebuildChannelsFromInspector()
	{
		//IL_0076: Expected O, but got I
		//IL_00ae: Expected O, but got I
		//IL_00be: Expected O, but got I
		_channels.Clear();
		HashSet<string> hashSet = new HashSet<string>(StringComparer.s_ordinal);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<KeySettings>.Enumerator enumerator = default(List<KeySettings>.Enumerator);
		object obj = default(object);
		object obj4 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj == null)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ stack_8_v3+10]");
				string text = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ stack_8_v3+10]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rax_v39+B8]");
					object obj3 = 0;
					text = (string)obj3;
					if (obj3 == null)
					{
						break;
					}
				}
				string text2 = text.Trim();
				if (string.IsNullOrEmpty(text2))
				{
					continue;
				}
				if (hashSet != null)
				{
					hashSet.Add(text2);
					if (obj4 != null)
					{
						ChannelState channelState = new ChannelState();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ stack_8_v3+18]");
						bool flag = (nint)0 < (nint)0;
						int usageLimit = 0;
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ stack_8_v3+18]");
							usageLimit = 0;
						}
						channelState.usageLimit = usageLimit;
						channelState.usedCount = 0;
						_channels.set_Item(text2, channelState);
					}
					else if (verboseLogging)
					{
						string message = "[TutorialCutawayService] Duplicate key '" + text2 + "' (keeping first).";
						Debug.LogWarning(message);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
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

	private bool IsKeyEligibleForNewActivation(string key)
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

	private void IncrementKeyUsage(string key)
	{
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		bool flag = key != null;
		string text = key;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rax_v9+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		string text2 = text.Trim();
		if (!string.IsNullOrEmpty(text2) && _channels.TryGetValue(text2, out var value))
		{
			int usedCount = value.usedCount + 1;
			value.usedCount = usedCount;
		}
	}

	public void RegisterCue(TutorialCutawayCue cue)
	{
		//IL_004d: Expected O, but got I
		//IL_005d: Expected O, but got I
		if (!cue)
		{
			return;
		}
		string text = cue.key;
		if (cue.key == null)
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
			value.registered.Add(cue);
			if (verboseLogging)
			{
				string arg = cue.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"[TutorialCutawayService] Registered cue '{arg}' (key '{cue.key}', priority {arg2}).";
				Debug.Log(message);
			}
		}
		else if (verboseLogging)
		{
			string text3 = cue.name;
			string message2 = "[TutorialCutawayService] Cue '" + text3 + "' registered with unknown key '" + cue.key + "'. Requests will be denied.";
			Debug.LogWarning(message2);
		}
	}

	public void UnregisterCue(TutorialCutawayCue cue)
	{
		//IL_004d: Expected O, but got I
		//IL_005d: Expected O, but got I
		if (!cue)
		{
			return;
		}
		string text = cue.key;
		if (cue.key == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rax_v30+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		string text2 = text.Trim();
		if (!string.IsNullOrEmpty(text2) && _channels.TryGetValue(text2, out var value))
		{
			bool flag = value.registered.Remove(cue);
			bool flag2 = value.pending.Remove(cue);
		}
		if (_active == cue)
		{
			if (verboseLogging)
			{
				string text3 = cue.name;
				string message = "[TutorialCutawayService] Active cue '" + text3 + "' unregistered. Interrupting and evaluating next.";
				Debug.Log(message);
			}
			cue.Internal_End(interrupted: true);
			_active = null;
			EvaluateQueued();
		}
	}

	public unsafe bool RequestActivation(TutorialCutawayCue cue)
	{
		//IL_0062: Expected I, but got O
		//IL_00ba: Expected O, but got I
		//IL_0077: Expected O, but got I
		//IL_0087: Expected O, but got I
		//IL_008f: Expected I, but got O
		//IL_146b: Expected I, but got O
		//IL_1475: Expected O, but got I4
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected Ref, but got Unknown
		//IL_17b6: Expected I4, but got O
		//IL_1054: Expected O, but got I
		//IL_1062: Unknown result type (might be due to invalid IL or missing references)
		//IL_1067: Expected O, but got Unknown
		//IL_108e: Expected O, but got I
		//IL_109c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a1: Expected O, but got Unknown
		//IL_0e5c: Expected I, but got O
		//IL_01ce: Expected O, but got I4
		//IL_0d22: Expected O, but got I
		//IL_0d3e: Expected I, but got O
		//IL_1264: Expected O, but got I
		//IL_1149: Expected I, but got O
		//IL_1159: Expected O, but got I
		//IL_1172: Expected O, but got I
		//IL_1189: Expected I, but got O
		//IL_0d6e: Expected I, but got O
		//IL_0d84: Expected O, but got I
		//IL_023f: Expected I, but got O
		//IL_1272: Unknown result type (might be due to invalid IL or missing references)
		//IL_1277: Expected O, but got Unknown
		//IL_11dc: Expected I, but got O
		//IL_11ec: Expected O, but got I
		//IL_120a: Expected O, but got I
		//IL_1221: Expected I, but got O
		//IL_0eda: Expected I, but got O
		//IL_0ee4: Expected O, but got I4
		//IL_132c: Expected O, but got I
		//IL_0da7: Expected O, but got I
		//IL_133a: Unknown result type (might be due to invalid IL or missing references)
		//IL_133f: Expected O, but got Unknown
		//IL_12b3: Expected I, but got O
		//IL_12c3: Expected O, but got I
		//IL_12dc: Expected O, but got I
		//IL_12f3: Expected I, but got O
		//IL_07d4: Expected I, but got O
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_137b: Expected I, but got O
		//IL_138b: Expected O, but got I
		//IL_13a4: Expected O, but got I
		//IL_13bb: Expected I, but got O
		//IL_0815: Expected I, but got O
		//IL_081f: Expected O, but got I4
		//IL_0dfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dff: Expected O, but got Unknown
		//IL_0850: Expected I, but got O
		//IL_0324: Expected I, but got O
		//IL_0fd3: Expected I, but got O
		//IL_091a: Expected I, but got O
		//IL_0898: Expected I, but got O
		//IL_08a8: Expected O, but got I
		//IL_08be: Expected O, but got I
		//IL_08da: Expected I, but got O
		//IL_08eb: Expected O, but got I
		//IL_03fd: Expected I, but got O
		//IL_0372: Expected I, but got O
		//IL_0382: Expected O, but got I
		//IL_039b: Expected O, but got I
		//IL_03ba: Expected I, but got O
		//IL_03cb: Expected O, but got I
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Expected O, but got Unknown
		//IL_043c: Expected O, but got I
		//IL_0a09: Expected I, but got O
		//IL_0987: Expected I, but got O
		//IL_0997: Expected O, but got I
		//IL_09ad: Expected O, but got I
		//IL_09c9: Expected I, but got O
		//IL_09da: Expected O, but got I
		//IL_0505: Expected I, but got O
		//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Expected O, but got Unknown
		//IL_0a50: Expected O, but got I
		//IL_047a: Expected I, but got O
		//IL_048a: Expected O, but got I
		//IL_04a3: Expected O, but got I
		//IL_04c2: Expected O, but got I
		//IL_04d2: Expected I, but got O
		//IL_0549: Expected I, but got O
		//IL_0b10: Expected I, but got O
		//IL_0a8e: Expected I, but got O
		//IL_0a9e: Expected O, but got I
		//IL_0ab4: Expected O, but got I
		//IL_0ad0: Expected I, but got O
		//IL_0ae1: Expected O, but got I
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b35: Expected O, but got Unknown
		//IL_0b67: Expected I, but got O
		//IL_062e: Expected I, but got O
		//IL_05a3: Expected I, but got O
		//IL_05b3: Expected O, but got I
		//IL_05cc: Expected O, but got I
		//IL_05eb: Expected O, but got I
		//IL_05fb: Expected I, but got O
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Expected O, but got Unknown
		//IL_0b9c: Expected O, but got I
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Expected O, but got Unknown
		//IL_0688: Expected I, but got O
		//IL_0c57: Expected I, but got O
		//IL_0bd5: Expected I, but got O
		//IL_0be5: Expected O, but got I
		//IL_0bfb: Expected O, but got I
		//IL_0c17: Expected I, but got O
		//IL_0c28: Expected O, but got I
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Expected O, but got Unknown
		//IL_06bd: Expected O, but got I
		//IL_0781: Expected I, but got O
		//IL_06f6: Expected I, but got O
		//IL_0706: Expected O, but got I
		//IL_071f: Expected O, but got I
		//IL_073e: Expected O, but got I
		//IL_074e: Expected I, but got O
		//IL_0caa: Expected I, but got O
		_ = 0;
		if (!cue)
		{
			goto IL_0211;
		}
		bool flag = (object)cue == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj2 = cue;
		string message;
		string[] array5;
		object obj20;
		nint num3;
		if (!flag)
		{
			nint num = (nint)cue.key;
			if (cue.key == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ rax_v185+B8]");
				obj2 = (UnityEngine.Object)0;
				num = (nint)obj2;
				bool flag2 = (object)obj2 == null;
				obj = null;
				if (flag2)
				{
					goto IL_15c7;
				}
			}
			string text = ((string)num).Trim();
			if (string.IsNullOrEmpty(text))
			{
				_ = 0;
				goto IL_140b;
			}
			bool flag3 = _channels == null;
			obj = null;
			obj2 = (UnityEngine.Object)(object)_channels;
			if (!flag3)
			{
				object obj4 = default(object);
				if (!_channels.TryGetValue(text, out *(ChannelState*)(obj4 - 24)))
				{
					goto IL_140b;
				}
				if (!IsKeyEligibleForNewActivation(cue.key))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-18]");
					object obj5 = 0;
					object obj6 = obj4 + 48;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v720 @ rax_v83+20]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-18]");
					object obj7 = 0;
					object obj8 = obj4 + 64;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v768 @ rcx_v61+24]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					object arg2 = default(object);
					string text2 = $"Used={arg} Limit={arg2}";
					cue.Internal_Denied(TutorialCutawayCue.DenialReason.KeyUsageExceeded, text2);
					if (!verboseLogging)
					{
						goto IL_0211;
					}
					object[] array = new object[4];
					string text3 = cue.name;
					if (text3 != null)
					{
						nint num2 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1430 @ rdx_v78 (Il2CppClass<System.Object[]>)+40]");
						string key = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1430 @ rdx_v78 (Il2CppClass<System.Object[]>)+40]");
						bool flag4 = ((Dictionary<string, ChannelState>)(object)text3).TryGetValue((string)0, out *(ChannelState*)text2);
						bool flag5 = !flag4;
						num3 = (nint)text2;
						UnityEngine.Object obj9 = (UnityEngine.Object)(object)text3;
						if (flag5)
						{
							bool flag6 = ((Dictionary<string, ChannelState>)(object)obj9).TryGetValue(key, out *(ChannelState*)num3);
							throw flag6;
						}
					}
					array[0] = text3;
					if (cue.key != null)
					{
						nint num4 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1684 @ rdx_v76 (Il2CppClass<System.Object[]>)+40]");
						string key2 = (string)0;
						string key3 = cue.key;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1684 @ rdx_v76 (Il2CppClass<System.Object[]>)+40]");
						bool flag7 = ((Dictionary<string, ChannelState>)(object)key3).TryGetValue((string)0, out *(ChannelState*)text2);
						bool flag8 = !flag7;
						num3 = (nint)text2;
						Dictionary<string, ChannelState> key4 = (Dictionary<string, ChannelState>)(object)cue.key;
						if (flag8)
						{
							bool flag9 = key4.TryGetValue(key2, out *(ChannelState*)num3);
							throw flag9;
						}
					}
					array[1] = cue.key;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-18]");
					object obj10 = 0;
					object obj11 = obj4 + 48;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1296 @ rax_v95+20]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Dictionary<string, ChannelState> dictionary = default(Dictionary<string, ChannelState>);
					if (dictionary != null)
					{
						nint num5 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1975 @ rdx_v74 (Il2CppClass<System.Object[]>)+40]");
						string key5 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1975 @ rdx_v74 (Il2CppClass<System.Object[]>)+40]");
						bool flag10 = dictionary.TryGetValue((string)0, out *(ChannelState*)text2);
						bool flag11 = !flag10;
						num3 = (nint)text2;
						Dictionary<string, ChannelState> dictionary2 = dictionary;
						if (flag11)
						{
							bool flag12 = dictionary2.TryGetValue(key5, out *(ChannelState*)num3);
							throw flag12;
						}
					}
					array[2] = dictionary;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-18]");
					object obj12 = 0;
					object obj13 = obj4 + 64;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1297 @ rax_v100+24]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Dictionary<string, ChannelState> dictionary3 = default(Dictionary<string, ChannelState>);
					if (dictionary3 != null)
					{
						nint num6 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2076 @ rdx_v72 (Il2CppClass<System.Object[]>)+40]");
						string key6 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2076 @ rdx_v72 (Il2CppClass<System.Object[]>)+40]");
						bool flag13 = dictionary3.TryGetValue((string)0, out *(ChannelState*)text2);
						bool flag14 = !flag13;
						num3 = (nint)text2;
						Dictionary<string, ChannelState> dictionary4 = dictionary3;
						if (flag14)
						{
							bool flag15 = dictionary4.TryGetValue(key6, out *(ChannelState*)num3);
							throw flag15;
						}
					}
					array[3] = dictionary3;
					message = string.Format("[TutorialCutawayService] Cue '{0}' denied (key usage exhausted for '{1}' Used={2} Limit={3}).", array);
					goto IL_15b9;
				}
				if (!(_active != null))
				{
					goto IL_0cfa;
				}
				bool flag16 = cue.overlapMode == TutorialCutawayCue.OverlapMode.Ignore;
				if (!flag16)
				{
					obj2 = (UnityEngine.Object)(cue.overlapMode - 1);
					if (!flag16)
					{
						if ((nint)obj2 != 1)
						{
							cue.Internal_Denied(TutorialCutawayCue.DenialReason.UnknownKey, "Invalid overlap mode");
						}
						else
						{
							TutorialCutawayCue active = _active;
							bool flag17 = (object)_active == null;
							num3 = unchecked((nint)null);
							obj = null;
							if (flag17)
							{
								goto IL_15c7;
							}
							if (cue.priority > active.priority)
							{
								bool flag18 = !verboseLogging;
								num3 = unchecked((nint)null);
								obj = null;
								if (flag18)
								{
									goto IL_0cb4;
								}
								object[] array2 = new object[4];
								UnityEngine.Object obj9 = _active;
								bool flag19 = (object)_active == null;
								num3 = unchecked((nint)null);
								string key = (string)4;
								if (!flag19)
								{
									string text4 = _active.name;
									bool flag20 = array2 == null;
									num3 = unchecked((nint)null);
									key = null;
									if (!flag20)
									{
										bool flag21 = text4 == null;
										key = null;
										Dictionary<string, ChannelState> dictionary5 = (Dictionary<string, ChannelState>)(object)_active;
										if (!flag21)
										{
											nint num7 = (nint)array2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1654 @ rdx_v121 (Il2CppClass<System.Object[]>)+40]");
											key = (string)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1654 @ rdx_v121 (Il2CppClass<System.Object[]>)+40]");
											bool flag22 = ((Dictionary<string, ChannelState>)(object)text4).TryGetValue((string)0, out *(ChannelState*)null);
											bool flag23 = !flag22;
											dictionary5 = (Dictionary<string, ChannelState>)(object)text4;
											num3 = unchecked((nint)null);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1654 @ rdx_v121 (Il2CppClass<System.Object[]>)+40]");
											string key7 = (string)0;
											UnityEngine.Object obj14 = (UnityEngine.Object)(object)text4;
											if (flag23)
											{
												bool flag24 = ((Dictionary<string, ChannelState>)(object)obj14).TryGetValue(key7, out *(ChannelState*)num3);
												throw flag24;
											}
										}
										bool flag25 = array2.Length <= 0;
										num3 = unchecked((nint)null);
										obj9 = (UnityEngine.Object)(object)dictionary5;
										if (!flag25)
										{
											array2[0] = text4;
											string text5 = cue.name;
											bool flag26 = text5 == null;
											key = null;
											Dictionary<string, ChannelState> dictionary6 = (Dictionary<string, ChannelState>)(object)cue;
											if (!flag26)
											{
												nint num8 = (nint)array2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1904 @ rdx_v119 (Il2CppClass<System.Object[]>)+40]");
												key = (string)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1904 @ rdx_v119 (Il2CppClass<System.Object[]>)+40]");
												bool flag27 = ((Dictionary<string, ChannelState>)(object)text5).TryGetValue((string)0, out *(ChannelState*)null);
												bool flag28 = !flag27;
												dictionary6 = (Dictionary<string, ChannelState>)(object)text5;
												num3 = unchecked((nint)null);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1904 @ rdx_v119 (Il2CppClass<System.Object[]>)+40]");
												string key8 = (string)0;
												Dictionary<string, ChannelState> dictionary7 = (Dictionary<string, ChannelState>)(object)text5;
												if (flag28)
												{
													bool flag29 = dictionary7.TryGetValue(key8, out *(ChannelState*)num3);
													throw flag29;
												}
											}
											bool flag30 = array2.Length <= 1;
											num3 = unchecked((nint)null);
											obj9 = (UnityEngine.Object)(object)dictionary6;
											if (!flag30)
											{
												array2[1] = text5;
												key = (string)(obj4 + 48);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												Dictionary<string, ChannelState> dictionary8 = (Dictionary<string, ChannelState>)0;
												_ = cue.priority;
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												Dictionary<string, ChannelState> dictionary9 = default(Dictionary<string, ChannelState>);
												if (dictionary9 != null)
												{
													nint num9 = (nint)array2;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2020 @ rdx_v117 (Il2CppClass<System.Object[]>)+40]");
													key = (string)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2020 @ rdx_v117 (Il2CppClass<System.Object[]>)+40]");
													bool flag31 = dictionary9.TryGetValue((string)0, out *(ChannelState*)null);
													bool flag32 = !flag31;
													dictionary8 = dictionary9;
													num3 = unchecked((nint)null);
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2020 @ rdx_v117 (Il2CppClass<System.Object[]>)+40]");
													string key9 = (string)0;
													Dictionary<string, ChannelState> dictionary10 = dictionary9;
													if (flag32)
													{
														bool flag33 = dictionary10.TryGetValue(key9, out *(ChannelState*)num3);
														throw flag33;
													}
												}
												bool flag34 = array2.Length <= 2;
												num3 = unchecked((nint)null);
												obj9 = (UnityEngine.Object)(object)dictionary8;
												if (!flag34)
												{
													obj9 = (UnityEngine.Object)(array2 + 48);
													array2[2] = dictionary9;
													TutorialCutawayCue active2 = _active;
													bool flag35 = (object)_active == null;
													num3 = unchecked((nint)null);
													key = (string)(object)dictionary9;
													if (flag35)
													{
														goto IL_16d7;
													}
													key = (string)(obj4 + 64);
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
													Dictionary<string, ChannelState> dictionary11 = (Dictionary<string, ChannelState>)0;
													_ = active2.priority;
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													Dictionary<string, ChannelState> dictionary12 = default(Dictionary<string, ChannelState>);
													if (dictionary12 != null)
													{
														nint num10 = (nint)array2;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2118 @ rdx_v115 (Il2CppClass<System.Object[]>)+40]");
														key = (string)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2118 @ rdx_v115 (Il2CppClass<System.Object[]>)+40]");
														bool flag36 = dictionary12.TryGetValue((string)0, out *(ChannelState*)null);
														bool flag37 = !flag36;
														dictionary11 = dictionary12;
														num3 = unchecked((nint)null);
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2118 @ rdx_v115 (Il2CppClass<System.Object[]>)+40]");
														string key10 = (string)0;
														Dictionary<string, ChannelState> dictionary13 = dictionary12;
														if (flag37)
														{
															bool flag38 = dictionary13.TryGetValue(key10, out *(ChannelState*)num3);
															throw flag38;
														}
													}
													bool flag39 = array2.Length <= 3;
													num3 = unchecked((nint)null);
													obj9 = (UnityEngine.Object)(object)dictionary11;
													if (!flag39)
													{
														array2[3] = dictionary12;
														string message2 = string.Format("[TutorialCutawayService] Preempting '{0}' with '{1}' (prio {2} > {3}).", array2);
														Debug.Log(message2);
														num3 = unchecked((nint)null);
														obj = null;
														goto IL_0cb4;
													}
												}
											}
										}
										throw new IndexOutOfRangeException();
									}
								}
								goto IL_16d7;
							}
							TutorialCutawayCue active3 = _active;
							object obj15 = obj4 + 48;
							_ = active3.priority;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg3 = default(object);
							string text6 = $"ActivePrio={arg3}";
							cue.Internal_Denied(TutorialCutawayCue.DenialReason.PreemptPriorityInsufficient, text6);
							if (verboseLogging)
							{
								object[] array3 = new object[4];
								string text7 = cue.name;
								bool flag40 = array3 == null;
								num3 = (nint)text6;
								string key7 = null;
								UnityEngine.Object obj14 = cue;
								if (!flag40)
								{
									bool flag41 = text7 == null;
									UnityEngine.Object obj16 = null;
									obj14 = cue;
									if (!flag41)
									{
										nint num11 = (nint)array3;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1826 @ rdx_v146 (Il2CppClass<System.Object[]>)+40]");
										obj16 = (UnityEngine.Object)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1826 @ rdx_v146 (Il2CppClass<System.Object[]>)+40]");
										bool flag42 = ((Dictionary<string, ChannelState>)(object)text7).TryGetValue((string)0, out *(ChannelState*)text6);
										bool flag43 = !flag42;
										obj14 = (UnityEngine.Object)(object)text7;
										num3 = (nint)text6;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1826 @ rdx_v146 (Il2CppClass<System.Object[]>)+40]");
										obj = (UnityEngine.Object)0;
										obj2 = (UnityEngine.Object)(object)text7;
										if (flag43)
										{
											bool flag44 = ((Dictionary<string, ChannelState>)(object)obj2).TryGetValue((string)(object)obj, out *(ChannelState*)num3);
											throw flag44;
										}
									}
									bool flag45 = array3.Length <= 0;
									num3 = (nint)text6;
									if (!flag45)
									{
										array3[0] = text7;
										obj16 = (UnityEngine.Object)(obj4 + 48);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
										obj14 = (UnityEngine.Object)0;
										_ = cue.priority;
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										Dictionary<string, ChannelState> dictionary14 = default(Dictionary<string, ChannelState>);
										if (dictionary14 != null)
										{
											nint num12 = (nint)array3;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1988 @ rdx_v144 (Il2CppClass<System.Object[]>)+40]");
											string key11 = (string)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1988 @ rdx_v144 (Il2CppClass<System.Object[]>)+40]");
											bool flag46 = dictionary14.TryGetValue((string)0, out *(ChannelState*)text6);
											bool flag47 = !flag46;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1988 @ rdx_v144 (Il2CppClass<System.Object[]>)+40]");
											obj16 = (UnityEngine.Object)0;
											obj14 = (UnityEngine.Object)(object)dictionary14;
											num3 = (nint)text6;
											Dictionary<string, ChannelState> dictionary15 = dictionary14;
											if (flag47)
											{
												bool flag48 = dictionary15.TryGetValue(key11, out *(ChannelState*)num3);
												throw flag48;
											}
										}
										bool flag49 = array3.Length <= 1;
										num3 = (nint)text6;
										if (!flag49)
										{
											array3[1] = dictionary14;
											obj14 = _active;
											bool flag50 = (object)_active == null;
											num3 = (nint)text6;
											key7 = (string)(object)dictionary14;
											if (flag50)
											{
												goto IL_1603;
											}
											string text8 = _active.name;
											bool flag51 = text8 == null;
											obj16 = null;
											string text9 = (string)(object)_active;
											if (!flag51)
											{
												nint num13 = (nint)array3;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2089 @ rdx_v142 (Il2CppClass<System.Object[]>)+40]");
												string key12 = (string)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2089 @ rdx_v142 (Il2CppClass<System.Object[]>)+40]");
												bool flag52 = ((Dictionary<string, ChannelState>)(object)text8).TryGetValue((string)0, out *(ChannelState*)text6);
												bool flag53 = !flag52;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2089 @ rdx_v142 (Il2CppClass<System.Object[]>)+40]");
												obj16 = (UnityEngine.Object)0;
												text9 = text8;
												num3 = (nint)text6;
												Dictionary<string, ChannelState> dictionary16 = (Dictionary<string, ChannelState>)(object)text8;
												if (flag53)
												{
													bool flag54 = dictionary16.TryGetValue(key12, out *(ChannelState*)num3);
													throw flag54;
												}
											}
											bool flag55 = array3.Length <= 2;
											num3 = (nint)text6;
											obj14 = (UnityEngine.Object)(object)text9;
											if (!flag55)
											{
												obj14 = (UnityEngine.Object)(array3 + 48);
												array3[2] = text8;
												TutorialCutawayCue active4 = _active;
												bool flag56 = (object)_active == null;
												num3 = (nint)text6;
												key7 = text8;
												if (flag56)
												{
													goto IL_1603;
												}
												obj16 = (UnityEngine.Object)(obj4 + 64);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												obj14 = (UnityEngine.Object)0;
												_ = active4.priority;
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												Dictionary<string, ChannelState> dictionary17 = default(Dictionary<string, ChannelState>);
												if (dictionary17 != null)
												{
													nint num14 = (nint)array3;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2150 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
													string key13 = (string)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2150 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
													bool flag57 = dictionary17.TryGetValue((string)0, out *(ChannelState*)text6);
													bool flag58 = !flag57;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2150 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
													obj16 = (UnityEngine.Object)0;
													obj14 = (UnityEngine.Object)(object)dictionary17;
													num3 = (nint)text6;
													Dictionary<string, ChannelState> dictionary18 = dictionary17;
													if (flag58)
													{
														bool flag59 = dictionary18.TryGetValue(key13, out *(ChannelState*)num3);
														throw flag59;
													}
												}
												bool flag60 = array3.Length <= 3;
												num3 = (nint)text6;
												if (!flag60)
												{
													array3[3] = dictionary17;
													message = string.Format("[TutorialCutawayService] Preempt failed: '{0}' prio {1} <= active '{2}' prio {3}. Denied.", array3);
													goto IL_15b9;
												}
											}
										}
									}
									throw new IndexOutOfRangeException();
								}
								goto IL_1603;
							}
						}
						goto IL_0211;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-18]");
					object obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-18]");
					bool flag61 = (nint)0 == 0;
					num3 = unchecked((nint)null);
					obj = null;
					if (!flag61)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rax_v124+18]");
						bool flag62 = (nint)0 == 0;
						num3 = unchecked((nint)null);
						obj = null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rax_v124+18]");
						obj2 = (UnityEngine.Object)0;
						if (!flag62)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rax_v124+18]");
							((HashSet<TutorialCutawayCue>)0).Add(cue);
							object obj18 = default(object);
							if (obj18 == null || !verboseLogging)
							{
								goto IL_0211;
							}
							string arg4 = cue.name;
							object obj19 = obj4 + 48;
							_ = cue.priority;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg5 = default(object);
							message = $"[TutorialCutawayService] Cue '{arg4}' queued (key '{cue.key}', priority {arg5}).";
							goto IL_15b9;
						}
					}
				}
				else
				{
					obj2 = _active;
					bool flag63 = (object)_active == null;
					num3 = unchecked((nint)null);
					obj = null;
					if (!flag63)
					{
						string text10 = _active.name;
						cue.Internal_Denied(TutorialCutawayCue.DenialReason.ActiveIgnoreOverlap, text10);
						if (!verboseLogging)
						{
							goto IL_0211;
						}
						string[] array4 = new string[5];
						bool flag64 = array4 == null;
						num3 = (nint)text10;
						obj = (UnityEngine.Object)5;
						obj2 = (UnityEngine.Object)(object)typeof(string[]);
						if (!flag64)
						{
							if (array4.Length > 0)
							{
								array4[0] = "[TutorialCutawayService] Cue '";
								string text11 = cue.name;
								if (array4.Length > 1)
								{
									array4[1] = text11;
									if (array4.Length > 2)
									{
										array4[2] = "' denied (active '";
										obj2 = _active;
										bool flag65 = (object)_active == null;
										num3 = (nint)text10;
										obj = (UnityEngine.Object)(object)"' denied (active '";
										if (flag65)
										{
											goto IL_15c7;
										}
										string text12 = _active.name;
										if (array4.Length > 3)
										{
											array4[3] = text12;
											array5 = array4;
											obj20 = "', overlap=Ignore).";
											goto IL_17b6;
										}
									}
								}
							}
							goto IL_17a8;
						}
					}
				}
			}
		}
		goto IL_15c7;
		IL_15c7:
		throw new NullReferenceException();
		IL_15b9:
		Debug.Log(message);
		goto IL_0211;
		IL_0211:
		return false;
		IL_0cfa:
		ActivateNow(cue);
		return true;
		IL_17b6:
		if (array5.Length > 4)
		{
			array5[4] = (string)obj20;
			message = string.Concat(array5);
			goto IL_15b9;
		}
		goto IL_17a8;
		IL_1603:
		throw new NullReferenceException();
		IL_16d7:
		throw new NullReferenceException();
		IL_140b:
		cue.Internal_Denied(TutorialCutawayCue.DenialReason.UnknownKey, cue.key);
		if (!verboseLogging)
		{
			goto IL_0211;
		}
		string[] array6 = new string[5];
		bool flag66 = array6 == null;
		num3 = (nint)cue.key;
		obj = (UnityEngine.Object)5;
		obj2 = (UnityEngine.Object)(object)typeof(string[]);
		if (flag66)
		{
			goto IL_15c7;
		}
		if (array6.Length > 0)
		{
			array6[0] = "[TutorialCutawayService] Cue '";
			string text13 = cue.name;
			if (array6.Length > 1)
			{
				array6[1] = text13;
				if (array6.Length > 2)
				{
					array6[2] = "' denied (unknown key '";
					if (array6.Length > 3)
					{
						array6[3] = cue.key;
						array5 = array6;
						obj20 = "').";
						goto IL_17b6;
					}
				}
			}
		}
		goto IL_17a8;
		IL_0cb4:
		bool flag67 = (object)_active == null;
		obj2 = _active;
		if (!flag67)
		{
			_active.Internal_End(interrupted: true);
			_active = null;
			goto IL_0cfa;
		}
		goto IL_15c7;
		IL_17a8:
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
	}

	public void CompleteActive(TutorialCutawayCue cue)
	{
		if (cue != null && cue == _active)
		{
			cue.Internal_End(interrupted: false);
			_active = null;
			EvaluateQueued();
		}
	}

	public void CancelActive(TutorialCutawayCue cue)
	{
		if (cue != null && cue == _active)
		{
			cue.Internal_End(interrupted: true);
			_active = null;
			EvaluateQueued();
		}
	}

	public bool ForceEndActive(bool interrupt)
	{
		//IL_00e7: Expected I4, but got O
		bool flag = _active == null;
		if (!flag)
		{
			if (verboseLogging != flag)
			{
				string arg = _active.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"[TutorialCutawayService] ForceEndActive called. Cue='{arg}' Interrupt={arg2}";
				Debug.Log(message);
			}
			if ((object)_active != null)
			{
				_active.Internal_End(interrupt);
				_active = null;
				EvaluateQueued();
				return true;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private unsafe void ActivateNow(TutorialCutawayCue cue)
	{
		//IL_003a: Expected I, but got O
		//IL_005d: Expected I, but got O
		//IL_00ba: Expected O, but got I
		//IL_0072: Expected O, but got I
		//IL_0082: Expected O, but got I
		//IL_008a: Expected I, but got O
		//IL_00a3: Expected I, but got O
		//IL_01e9: Expected I, but got O
		//IL_00f9: Expected I, but got O
		//IL_021a: Expected O, but got I
		//IL_022a: Expected O, but got I
		//IL_030a: Expected I, but got O
		//IL_0362: Expected O, but got I
		//IL_031f: Expected O, but got I
		//IL_032f: Expected O, but got I
		//IL_0337: Expected I, but got O
		//IL_0429: Expected I, but got O
		//IL_0442: Expected O, but got I
		//IL_0462: Expected O, but got I
		//IL_04bc: Expected I, but got O
		//IL_04cc: Expected O, but got I
		//IL_04ea: Expected O, but got I
		//IL_0568: Expected I, but got O
		//IL_0578: Expected O, but got I
		//IL_0591: Expected O, but got I
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Expected I4, but got Unknown
		//IL_0639: Expected I, but got O
		//IL_0649: Expected O, but got I
		//IL_0662: Expected O, but got I
		if (!(cue != null))
		{
			return;
		}
		bool flag = (object)cue == null;
		UnityEngine.Object obj = null;
		nint num = unchecked((nint)null);
		UnityEngine.Object obj2 = cue;
		if (!flag)
		{
			nint num2 = (nint)cue.key;
			if (cue.key == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rax_v79+B8]");
				obj2 = (UnityEngine.Object)0;
				num2 = (nint)obj2;
				bool flag2 = (object)obj2 == null;
				obj = null;
				num = unchecked((nint)null);
				if (flag2)
				{
					goto IL_06cd;
				}
			}
			string text = ((string)num2).Trim();
			if (string.IsNullOrEmpty(text))
			{
				num = unchecked((nint)null);
				goto IL_01ee;
			}
			bool flag3 = _channels == null;
			obj = null;
			num = unchecked((nint)null);
			obj2 = (UnityEngine.Object)(object)_channels;
			if (!flag3)
			{
				bool flag4 = _channels.TryGetValue(text, out var value);
				bool flag5 = !flag4;
				num = (nint)(&value);
				if (flag5)
				{
					goto IL_01ee;
				}
				bool flag6 = value == null;
				obj = (UnityEngine.Object)(object)text;
				num = (nint)(&value);
				obj2 = (UnityEngine.Object)(object)_channels;
				if (!flag6)
				{
					bool flag7 = value.pending == null;
					obj = (UnityEngine.Object)(object)text;
					num = (nint)(&value);
					obj2 = (UnityEngine.Object)(object)value.pending;
					if (!flag7)
					{
						bool flag8 = value.pending.Remove(cue);
						num = 0;
						goto IL_01ee;
					}
				}
			}
		}
		goto IL_06cd;
		IL_01ee:
		_active = cue;
		string text2 = cue.key;
		if (cue.key == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v548 @ rax_v73+B8]");
			object obj5 = 0;
			text2 = (string)obj5;
		}
		string text3 = text2.Trim();
		ChannelState value2;
		if (!string.IsNullOrEmpty(text3))
		{
			bool flag9 = _channels.TryGetValue(text3, out value2);
			bool flag10 = !flag9;
			num = (nint)(&value2);
			if (!flag10)
			{
				int usedCount = value2.usedCount + 1;
				value2.usedCount = usedCount;
				num = (nint)(&value2);
			}
		}
		else
		{
			value2 = null;
		}
		cue.Internal_Begin();
		if (!verboseLogging)
		{
			return;
		}
		nint num3 = (nint)cue.key;
		if (cue.key == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rax_v69+B8]");
			obj2 = (UnityEngine.Object)0;
			num3 = (nint)obj2;
			bool flag11 = (object)obj2 == null;
			obj = null;
			if (flag11)
			{
				goto IL_06cd;
			}
		}
		string text4 = ((string)num3).Trim();
		if (string.IsNullOrEmpty(text4))
		{
			return;
		}
		bool flag12 = _channels == null;
		obj = null;
		obj2 = (UnityEngine.Object)(object)_channels;
		if (!flag12)
		{
			if (!_channels.TryGetValue(text4, out var value3))
			{
				return;
			}
			object[] array = new object[4];
			string text5 = cue.name;
			if (text5 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rdx_v50 (Il2CppClass<System.Object[]>)+40]");
				bool flag13 = ((Dictionary<string, ChannelState>)(object)text5).TryGetValue((string)0, out value3);
				bool flag14 = !flag13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rdx_v50 (Il2CppClass<System.Object[]>)+40]");
				obj = (UnityEngine.Object)0;
				ref ChannelState value4 = ref value3;
				obj2 = (UnityEngine.Object)(object)text5;
				if (flag14)
				{
					bool flag15 = ((Dictionary<string, ChannelState>)(object)obj2).TryGetValue((string)(object)obj, out value4);
					throw flag15;
				}
			}
			array[0] = text5;
			if (cue.key != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v976 @ rdx_v48 (Il2CppClass<System.Object[]>)+40]");
				string key = (string)0;
				string key2 = cue.key;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v976 @ rdx_v48 (Il2CppClass<System.Object[]>)+40]");
				bool flag16 = ((Dictionary<string, ChannelState>)(object)key2).TryGetValue((string)0, out value3);
				bool flag17 = !flag16;
				ref ChannelState value4 = ref value3;
				Dictionary<string, ChannelState> key3 = (Dictionary<string, ChannelState>)(object)cue.key;
				if (flag17)
				{
					bool flag18 = key3.TryGetValue(key, out value4);
					throw flag18;
				}
			}
			array[1] = cue.key;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Dictionary<string, ChannelState> dictionary = default(Dictionary<string, ChannelState>);
			if (dictionary != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1004 @ rdx_v46 (Il2CppClass<System.Object[]>)+40]");
				string key4 = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1004 @ rdx_v46 (Il2CppClass<System.Object[]>)+40]");
				bool flag19 = dictionary.TryGetValue((string)0, out value3);
				bool flag20 = !flag19;
				ref ChannelState value4 = ref value3;
				Dictionary<string, ChannelState> dictionary2 = dictionary;
				if (flag20)
				{
					bool flag21 = dictionary2.TryGetValue(key4, out value4);
					throw flag21;
				}
			}
			array[2] = dictionary;
			string text6;
			if (value3.usageLimit == 0)
			{
				text6 = "∞";
			}
			else
			{
				int num7 = value3 + 36;
				string text7 = ((int*)num7)->ToString();
				text6 = text7;
			}
			if (text6 != null)
			{
				nint num8 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1046 @ rdx_v43 (Il2CppClass<System.Object[]>)+40]");
				string key5 = (string)0;
				string text8 = text6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1046 @ rdx_v43 (Il2CppClass<System.Object[]>)+40]");
				bool flag22 = ((Dictionary<string, ChannelState>)(object)text8).TryGetValue((string)0, out value3);
				bool flag23 = !flag22;
				ref ChannelState value4 = ref value3;
				Dictionary<string, ChannelState> dictionary3 = (Dictionary<string, ChannelState>)(object)text6;
				if (flag23)
				{
					bool flag24 = dictionary3.TryGetValue(key5, out value4);
					throw flag24;
				}
			}
			array[3] = text6;
			string message = string.Format("[TutorialCutawayService] Activated cue '{0}' (Key='{1}' Used={2}/{3}).", array);
			Debug.Log(message);
			return;
		}
		goto IL_06cd;
		IL_06cd:
		throw new NullReferenceException();
	}

	private unsafe void EvaluateQueued()
	{
		//IL_001d: Expected O, but got I8
		//IL_0062: Expected O, but got Ref
		//IL_0110: Expected O, but got I
		//IL_042c: Expected O, but got I
		//IL_023c: Expected O, but got I
		//IL_02f8: Expected O, but got I
		//IL_0374: Expected O, but got I
		//IL_03ce: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
		object obj = 2147483648L;
		int num = 2147483647;
		UnityEngine.Object obj2 = null;
		Dictionary<string, ChannelState>.Enumerator enumerator = default(Dictionary<string, ChannelState>.Enumerator);
		object obj3 = default(object);
		object obj4 = default(object);
		HashSet<TutorialCutawayCue>.Enumerator enumerator2 = default(HashSet<TutorialCutawayCue>.Enumerator);
		TutorialCutawayCue item = default(TutorialCutawayCue);
		List<TutorialCutawayCue>.Enumerator enumerator3 = default(List<TutorialCutawayCue>.Enumerator);
		UnityEngine.Object obj5 = default(UnityEngine.Object);
		object arg = default(object);
		object arg2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
				bool flag = obj3 == null;
				KeyValuePair<string, ChannelState> keyValuePair = (KeyValuePair<string, ChannelState>)(&obj4);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+24]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+20]");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+24]");
					if (num2 >= 0)
					{
						continue;
					}
				}
				keyValuePair = (KeyValuePair<string, ChannelState>)_tempList;
				if (_tempList != null)
				{
					ChannelState value = ((KeyValuePair<string, ChannelState>*)_tempList)->Value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+18]");
					keyValuePair = (KeyValuePair<string, ChannelState>)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+18]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
						while (enumerator2.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (_tempList != null)
							{
								_tempList.Add(item);
								continue;
							}
							throw new NullReferenceException();
						}
						enumerator2.Dispose();
						keyValuePair = (KeyValuePair<string, ChannelState>)_tempList;
						if (_tempList != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							while (enumerator3.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								bool flag2 = obj5;
								bool flag3 = !flag2;
								keyValuePair = (KeyValuePair<string, ChannelState>)obj5;
								if (!flag3)
								{
									if ((object)obj5 == null)
									{
										throw new NullReferenceException();
									}
									bool flag4 = ((Behaviour)obj5).isActiveAndEnabled;
									bool flag5 = !flag4;
									keyValuePair = (KeyValuePair<string, ChannelState>)obj5;
									if (!flag5)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_-D0 (UnityEngine.Object)+38]");
										if (IsKeyEligibleForNewActivation((string)0))
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_-D0 (UnityEngine.Object)+40]");
											if (0 <= (nint)obj)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_-D0 (UnityEngine.Object)+40]");
												if (0 == (nint)obj)
												{
													int instanceID = obj5.GetInstanceID();
													if (instanceID < num)
													{
														num = instanceID;
														obj2 = obj5;
													}
												}
											}
											else
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_-D0 (UnityEngine.Object)+40]");
												obj = 0;
												int instanceID2 = obj5.GetInstanceID();
												num = instanceID2;
												obj2 = obj5;
											}
										}
										else
										{
											if (obj3 == null)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+18]");
											if ((nint)0 == 0)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+18]");
											bool flag6 = ((HashSet<TutorialCutawayCue>)0).Remove((TutorialCutawayCue)obj5);
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											string reasonExtra = $"Used={arg} Limit={arg2}";
											((TutorialCutawayCue)obj5).Internal_Denied(TutorialCutawayCue.DenialReason.KeyUsageExceeded, reasonExtra);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+24]");
											object obj6 = 0;
										}
										continue;
									}
								}
								if (obj3 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+18]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_-E8_v12+18]");
										bool flag7 = ((HashSet<TutorialCutawayCue>)0).Remove((TutorialCutawayCue)obj5);
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							enumerator3.Dispose();
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (obj2 != null)
			{
				ActivateNow((TutorialCutawayCue)obj2);
			}
			return;
		}
		throw new NullReferenceException();
	}

	internal static bool CompareTagSafe(GameObject go, string tag)
	{
		//IL_0095: Expected I4, but got O
		if ((bool)go && !string.IsNullOrEmpty(tag))
		{
			if ((object)go != null)
			{
				return go.CompareTag(tag);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public TutorialCutawayService()
	{
		List<KeySettings> list = new List<KeySettings>();
		KeySettings keySettings = new KeySettings();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A817]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		keySettings.key = "";
		keySettings.key = "Default";
		keySettings.usageLimit = 0;
		list.Add(keySettings);
		keys = list;
		_channels = new Dictionary<string, ChannelState>(StringComparer.s_ordinal);
		_tempList = new List<TutorialCutawayCue>();
		base._002Ector();
	}
}
