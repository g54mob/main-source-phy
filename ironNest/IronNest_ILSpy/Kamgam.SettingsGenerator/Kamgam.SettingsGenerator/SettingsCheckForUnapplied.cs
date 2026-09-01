using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingsCheckForUnapplied : MonoBehaviour
{
	[NonSerialized]
	private static List<SettingsCheckForUnapplied> _registry;

	[NonSerialized]
	private static int _lastCheckFrame;

	public bool CheckOnDisable = true;

	public SettingsProvider Provider;

	public bool FallBackOnConfiguredProvider;

	public UnityEvent<List<ISetting>> OnUnappliedSettingsDetected;

	public List<GameObject> ObjectsToShowOnUnapplied;

	[NonSerialized]
	public List<ISetting> _unappliedSettings;

	public static void TriggerCheck()
	{
		//IL_0080: Expected O, but got I
		//IL_00af: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingsCheckForUnapplied>.Enumerator enumerator = default(List<SettingsCheckForUnapplied>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if ((object)obj == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_10_v4 (UnityEngine.Object)+28]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_10_v4 (UnityEngine.Object)+28]");
					if (((SettingsProvider)0).HasSettings())
					{
						((SettingsCheckForUnapplied)obj).Check();
						enumerator.Dispose();
						return;
					}
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	protected SettingsProvider getProvider()
	{
		bool flag = Provider != null;
		if (!flag)
		{
			if (FallBackOnConfiguredProvider != flag)
			{
				return SettingsGeneratorSettings.GetProvider();
			}
			return SettingsProvider.LastUsedSettingsProvider;
		}
		return Provider;
	}

	public void OnEnable()
	{
		if (!_registry.Contains(this))
		{
			_registry.Add(this);
		}
	}

	public void OnDisable()
	{
		if (_registry.Contains(this))
		{
			bool flag = _registry.Remove(this);
		}
		if (CheckOnDisable)
		{
			Check();
		}
	}

	public void Check()
	{
		//IL_01fa: Expected O, but got I4
		if (!(Provider != null) || !Provider.HasSettings())
		{
			return;
		}
		int frameCount = Time.frameCount;
		object obj = frameCount - _lastCheckFrame;
		if ((nint)obj < 2)
		{
			return;
		}
		int frameCount2 = Time.frameCount;
		_lastCheckFrame = frameCount2;
		Settings settings = Provider.Settings;
		List<ISetting> unappliedSettings = settings.GetUnappliedSettings(_unappliedSettings);
		List<ISetting> unappliedSettings2 = _unappliedSettings;
		if (unappliedSettings2._size <= 0)
		{
			return;
		}
		if (OnUnappliedSettingsDetected != null)
		{
			OnUnappliedSettingsDetected.Invoke(unappliedSettings2);
		}
		if (ObjectsToShowOnUnapplied == null)
		{
			return;
		}
		List<GameObject> objectsToShowOnUnapplied = ObjectsToShowOnUnapplied;
		if (objectsToShowOnUnapplied._size <= 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj2 != null)
				{
					if ((object)obj2 == null)
					{
						break;
					}
					((GameObject)obj2).SetActive(true);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void LogSettings(List<ISetting> settings)
	{
		//IL_0105: Expected O, but got I4
		//IL_00b2: Expected O, but got I
		//IL_00bb: Expected O, but got I4
		//IL_0144: Expected O, but got I
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		if (settings != null && settings._size != 0)
		{
			Debug.Log("SettingsCheckForUnapplied: Unapplied settings found:");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
			object obj = default(object);
			object obj12 = default(object);
			string text = default(string);
			while (true)
			{
				object obj3;
				object obj11;
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (obj == null)
					{
						break;
					}
					object obj2 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v3+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v3+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ r8_v5+v287 @ rax_v27*8]");
							if (0 == (nint)typeof(ISetting))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v3+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_00f2;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ r8_v5+8+v343 @ rcx_v22*8]");
						object obj8 = (nint)0 + (nint)4;
						object obj9 = obj8 << 4;
						object obj10 = obj9 + 312;
						obj11 = obj10 + obj2;
						goto IL_021d;
					}
					goto IL_00f2;
				}
				enumerator.Dispose();
				return;
				IL_00f2:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj3 = 4;
				obj11 = obj12;
				goto IL_021d;
				IL_021d:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v351 @ rdx_v10] (should have been resolved before IL gen)");
				string message = " * " + text;
				Debug.Log(message);
			}
			throw new NullReferenceException();
		}
		Debug.Log("SettingsCheckForUnapplied: Settings is null!");
	}

	public SettingsCheckForUnapplied()
	{
		List<ISetting> unappliedSettings = new List<ISetting>(10);
		_unappliedSettings = unappliedSettings;
		base._002Ector();
	}

	static SettingsCheckForUnapplied()
	{
		List<SettingsCheckForUnapplied> registry = new List<SettingsCheckForUnapplied>();
		_registry = registry;
		_lastCheckFrame = 0;
	}
}
