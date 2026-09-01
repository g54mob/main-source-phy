using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class MapReconClearer : MonoBehaviour
{
	public UnityEvent OnCleared;

	private bool _debugLog;

	private readonly List<MapReconClearHandle> _handles;

	public int ActiveCount
	{
		get
		{
			//IL_001d: Expected I4, but got O
			List<MapReconClearHandle> handles = _handles;
			if (_handles != null)
			{
				return handles._size;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public void Register(MapReconClearHandle handle)
	{
		if (handle != null && !_handles.Contains(handle))
		{
			_handles.Add(handle);
		}
	}

	public void Unregister(MapReconClearHandle handle)
	{
		bool flag = _handles.Remove(handle);
	}

	public unsafe void ClearAll()
	{
		//IL_0048: Expected I, but got O
		//IL_0081: Expected I, but got O
		List<MapReconClearHandle> list = new List<MapReconClearHandle>(_handles);
		if (list != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num = 0;
			List<MapReconClearHandle>.Enumerator enumerator = default(List<MapReconClearHandle>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj != null;
				bool flag2 = !flag;
				num = unchecked((nint)null);
				if (!flag2)
				{
					if ((object)obj == null)
					{
						throw new NullReferenceException();
					}
					((MapReconClearHandle)obj).DestroyAll();
					num = unchecked((nint)null);
				}
			}
			enumerator.Dispose();
			List<MapReconClearHandle> handles = _handles;
			if (_handles != null)
			{
				int version = handles._version + 1;
				handles._version = version;
				((List<MapReconClearHandle>.Enumerator*)null)->Dispose();
				object obj2 = default(object);
				if (obj2 == null)
				{
					handles._size = 0;
					int num2 = (int)num;
				}
				else
				{
					int num2 = handles._size;
					handles._size = 0;
					if (handles._size > 0)
					{
						Array.Clear(handles._items, 0, handles._size);
					}
				}
				if (_debugLog)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"[MapReconClearer] Cleared {arg} recon photo(s).";
					Debug.Log(message);
				}
				if (OnCleared != null)
				{
					OnCleared.Invoke();
				}
				return;
			}
		}
		throw new NullReferenceException();
	}

	public MapReconClearer()
	{
		List<MapReconClearHandle> handles = new List<MapReconClearHandle>();
		_handles = handles;
		base._002Ector();
	}
}
