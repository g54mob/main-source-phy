using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator;

public static class InputActionAssetUtils
{
	public static float _lastCacheTime = -100000f;

	public static List<InputActionAsset> _cachedResults;

	public unsafe static List<InputActionAsset> FindInstancesOf(InputActionAsset baseAsset, List<InputActionAsset> results = null, float cacheDurationInSec = 0f)
	{
		//IL_02f9: Expected I, but got O
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		//IL_026d: Expected O, but got Ref
		//IL_026d: Expected O, but got Ref
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		bool flag = results != null;
		nint num = (nint)results;
		List<InputActionAsset> list = results;
		if (!flag)
		{
			List<InputActionAsset> list2 = new List<InputActionAsset>();
			num = 0;
			list = list2;
		}
		int version = list._version + 1;
		list._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			list._size = 0;
			int num2 = (int)num;
		}
		else
		{
			list._size = 0;
			bool flag2 = list._size <= 0;
			int num2 = (int)num;
			if (!flag2)
			{
				Array.Clear(list._items, 0, list._size);
				num2 = 0;
			}
		}
		if (cacheDurationInSec > 0.001f)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num3 = realtimeSinceStartup - _lastCacheTime;
			if (cacheDurationInSec > num3)
			{
				list.AddRange(_cachedResults);
				goto IL_0124;
			}
		}
		List<InputActionAsset> cachedResults = _cachedResults;
		int version2 = cachedResults._version + 1;
		cachedResults._version = version2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj2 = default(object);
		if (obj2 == null)
		{
			cachedResults._size = 0;
		}
		else
		{
			cachedResults._size = 0;
			if (cachedResults._size > 0)
			{
				Array.Clear(cachedResults._items, 0, cachedResults._size);
			}
		}
		string name = baseAsset.name;
		InputActionAsset[] array = Resources.FindObjectsOfTypeAll<InputActionAsset>();
		Guid firstBindingGuid = getFirstBindingGuid(baseAsset);
		object obj3 = array + 32;
		int num4 = 0;
		int num5 = 0;
		int a = default(int);
		int num6 = default(int);
		while (num5 < array.Length)
		{
			if (num4 < array.Length)
			{
				Guid firstBindingGuid2 = getFirstBindingGuid((InputActionAsset)obj3);
				if ((Guid)(&a) == (Guid)(&num6))
				{
					list.Add((InputActionAsset)obj3);
					_cachedResults.Add((InputActionAsset)obj3);
				}
				num4++;
				obj3 += 8;
				a = firstBindingGuid2._a;
				num5 = num4;
				continue;
			}
			return (List<InputActionAsset>)(object)new IndexOutOfRangeException();
		}
		goto IL_0124;
		IL_0124:
		return list;
	}

	private unsafe static Guid getFirstBindingGuid(InputActionAsset asset)
	{
		//IL_00ef: Expected native int or pointer, but got O
		if ((object)asset != null)
		{
			IEnumerable<InputBinding> bindings = asset.bindings;
			if (bindings != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj2 = default(object);
					int a;
					if (obj2 == null)
					{
						a = 0;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18018D060");
						InputBinding inputBinding = default(InputBinding);
						a = inputBinding.id._a;
					}
					Guid guid = default(Guid);
					((Guid*)(nint)guid)->_a = a;
					return guid;
				}
			}
		}
		return (Guid)new NullReferenceException();
	}

	static InputActionAssetUtils()
	{
		List<InputActionAsset> cachedResults = new List<InputActionAsset>();
		_cachedResults = cachedResults;
	}
}
