using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class MapReconClearHandle : MonoBehaviour
{
	private List<GameObject> _prelinkedChildren;

	private readonly List<GameObject> _allChildren;

	private MapReconClearer _clearer;

	private void Awake()
	{
		if (_prelinkedChildren != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if (_allChildren == null)
					{
						throw new NullReferenceException();
					}
					_allChildren.Add((GameObject)obj);
				}
			}
			enumerator.Dispose();
			MapReconClearer clearer = UnityEngine.Object.FindFirstObjectByType<MapReconClearer>();
			_clearer = clearer;
			if (!(_clearer != null))
			{
				string text = base.name;
				string message = "[MapReconClearHandle] No MapReconClearer found in scene. '" + text + "' will not be tracked for clearing.";
				Debug.LogWarning(message, this);
				return;
			}
			MapReconClearer clearer2 = _clearer;
			if ((object)_clearer != null)
			{
				if (!(this != null))
				{
					return;
				}
				if (clearer2._handles != null)
				{
					if (clearer2._handles.Contains(this))
					{
						return;
					}
					if (clearer2._handles != null)
					{
						clearer2._handles.Add(this);
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private void OnDestroy()
	{
		MapReconClearer clearer = _clearer;
		if ((object)_clearer != null)
		{
			bool flag = clearer._handles.Remove(this);
		}
	}

	public void RegisterChild(GameObject child)
	{
		if (child != null && !_allChildren.Contains(child))
		{
			_allChildren.Add(child);
		}
	}

	public unsafe void DestroyAll()
	{
		//IL_0218: Expected O, but got Ref
		MapReconClearer clearer = _clearer;
		if ((object)_clearer != null)
		{
			if (clearer._handles == null)
			{
				goto IL_0178;
			}
			bool flag = clearer._handles.Remove(this);
		}
		_clearer = null;
		if (_allChildren != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					UnityEngine.Object.Destroy(obj);
				}
			}
			enumerator.Dispose();
			List<GameObject> allChildren = _allChildren;
			bool flag2 = _allChildren == null;
			List<MapReconClearHandle> list = (List<MapReconClearHandle>)(&enumerator);
			if (!flag2)
			{
				int version = allChildren._version + 1;
				allChildren._version = version;
				((List<GameObject>.Enumerator*)null)->Dispose();
				object obj2 = default(object);
				if (obj2 == null)
				{
					allChildren._size = 0;
				}
				else
				{
					allChildren._size = 0;
					if (allChildren._size > 0)
					{
						Array.Clear(allChildren._items, 0, allChildren._size);
					}
				}
				GameObject obj3 = base.gameObject;
				UnityEngine.Object.Destroy(obj3);
				return;
			}
		}
		goto IL_0178;
		IL_0178:
		throw new NullReferenceException();
	}

	public MapReconClearHandle()
	{
		List<GameObject> prelinkedChildren = new List<GameObject>();
		_prelinkedChildren = prelinkedChildren;
		_allChildren = new List<GameObject>();
		base._002Ector();
	}
}
