using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace MinimalVolumeCulling;

public sealed class CullTarget : MonoBehaviour
{
	public enum CullingAction
	{
		DisableGameObjects
	}

	private List<GameObject> toggleRoots;

	private bool warnIfSelfIsInToggleRoots;

	private bool neverCull;

	private bool restoreOriginalActiveStateOnUncull;

	private bool debugIsCulled;

	private bool _isCulled;

	private bool _capturedInitialStates;

	private readonly Dictionary<GameObject, bool> _initialActiveSelf;

	public bool IsCulled => _isCulled;

	private void Awake()
	{
		CaptureInitialActiveStatesIfNeeded();
	}

	private void OnValidate()
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		if (!warnIfSelfIsInToggleRoots)
		{
			return;
		}
		List<GameObject> list = toggleRoots;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				GameObject gameObject = base.gameObject;
				if (obj3 == gameObject)
				{
					string text = base.name;
					string message = "[CullTarget] '" + text + "' has its own GameObject in Toggle Roots. This can break un-culling (because the script will disable itself). Remove it from the list.";
					Debug.LogWarning(message, this);
				}
			}
			list = toggleRoots;
			obj++;
			obj2 = obj;
		}
	}

	private unsafe void CaptureInitialActiveStatesIfNeeded()
	{
		//IL_0033: Expected O, but got I4
		//IL_003c: Expected O, but got I4
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		if (_capturedInitialStates)
		{
			return;
		}
		_capturedInitialStates = true;
		_initialActiveSelf.Clear();
		List<GameObject> list = toggleRoots;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		bool flag = default(bool);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null && !_initialActiveSelf.ContainsKey((GameObject)obj3))
			{
				bool activeSelf = ((GameObject)obj3).activeSelf;
				_initialActiveSelf.Add((GameObject)obj3, (byte)(&flag) != 0);
			}
			list = toggleRoots;
			obj++;
			obj2 = obj;
		}
	}

	public void ApplyCulled(bool culled)
	{
		bool flag = neverCull;
		bool flag2 = false;
		if (!flag)
		{
			flag2 = culled;
		}
		if (_isCulled == flag2)
		{
			return;
		}
		_isCulled = flag2;
		debugIsCulled = flag2;
		CaptureInitialActiveStatesIfNeeded();
		List<GameObject> list = toggleRoots;
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!flag2)
		{
			bool flag3 = false;
			bool value = false;
			bool flag4 = false;
			bool flag7;
			do
			{
				if ((flag4 ? 1 : 0) >= list._size)
				{
					return;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool active;
				if (obj != null)
				{
					GameObject gameObject = base.gameObject;
					bool flag5 = obj == gameObject;
					if (!flag5)
					{
						if (restoreOriginalActiveStateOnUncull == flag5)
						{
							if (!((GameObject)obj).activeSelf)
							{
								goto IL_0172;
							}
						}
						else if (!_initialActiveSelf.TryGetValue((GameObject)obj, out value))
						{
							if (!((GameObject)obj).activeSelf)
							{
								goto IL_0172;
							}
						}
						else
						{
							bool activeSelf = ((GameObject)obj).activeSelf;
							bool flag6 = activeSelf == value;
							active = value;
							if (!flag6)
							{
								goto IL_01b1;
							}
						}
					}
				}
				goto IL_01c3;
				IL_0172:
				active = true;
				goto IL_01b1;
				IL_01c3:
				list = toggleRoots;
				flag3 = (byte)((flag3 ? 1u : 0u) + 1u) != 0;
				flag7 = toggleRoots != null;
				flag4 = flag3;
				continue;
				IL_01b1:
				((GameObject)obj).SetActive(active);
				goto IL_01c3;
			}
			while (flag7);
			throw new NullReferenceException();
		}
		bool flag8 = false;
		bool flag9 = false;
		while ((flag9 ? 1 : 0) < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj != null)
			{
				GameObject gameObject2 = base.gameObject;
				if (obj != gameObject2 && ((GameObject)obj).activeSelf)
				{
					((GameObject)obj).SetActive(false);
				}
			}
			list = toggleRoots;
			flag8 = (byte)((flag8 ? 1u : 0u) + 1u) != 0;
			flag9 = flag8;
		}
	}

	public CullTarget()
	{
		List<GameObject> list = new List<GameObject>();
		toggleRoots = list;
		warnIfSelfIsInToggleRoots = true;
		restoreOriginalActiveStateOnUncull = true;
		_initialActiveSelf = new Dictionary<GameObject, bool>();
		base._002Ector();
	}
}
