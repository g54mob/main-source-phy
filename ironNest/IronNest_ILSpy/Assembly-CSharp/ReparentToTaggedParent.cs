using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ReparentToTaggedParent : MonoBehaviour
{
	private sealed class _003CReparentEndOfFrame_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ReparentToTaggedParent _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CReparentEndOfFrame_003Ed__10(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0031: Expected I4, but got I8
			//IL_007a: Expected I4, but got I8
			//IL_00bd: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				_003C_003E4__this.DoReparent();
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private string parentTag = "WorldCanvas";

	private bool requireWorldSpaceCanvas = true;

	private bool reparentAtEndOfFrame;

	private bool reparentOnlyOnce = true;

	private bool logWarnings;

	private UnityEvent onReparentComplete;

	private bool _done;

	private void OnEnable()
	{
		TryStartReparent();
	}

	private void Start()
	{
		TryStartReparent();
	}

	private void TryStartReparent()
	{
		if (!_done || !reparentOnlyOnce)
		{
			if (!reparentAtEndOfFrame)
			{
				DoReparent();
				return;
			}
			_003CReparentEndOfFrame_003Ed__10 obj = new _003CReparentEndOfFrame_003Ed__10(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	private IEnumerator ReparentEndOfFrame()
	{
		_003CReparentEndOfFrame_003Ed__10 obj = new _003CReparentEndOfFrame_003Ed__10(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void DoReparent()
	{
		if (_done && reparentOnlyOnce)
		{
			return;
		}
		GameObject gameObject;
		if (!string.IsNullOrEmpty(parentTag))
		{
			gameObject = GameObject.FindWithTag(parentTag);
			bool flag = gameObject == null;
			string[] array;
			object obj2;
			if (!flag)
			{
				if (requireWorldSpaceCanvas == flag)
				{
					goto IL_0161;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9300");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj != null)
				{
					RenderMode renderMode = ((Canvas)obj).renderMode;
					if (renderMode == RenderMode.WorldSpace)
					{
						goto IL_0161;
					}
				}
				if (!logWarnings)
				{
					return;
				}
				string text = gameObject.name;
				array = new string[5] { "ReparentToTaggedParent: Target '", text, null, null, null };
				obj2 = "' doesn't have a World Space Canvas in its parents for ";
			}
			else
			{
				if (!logWarnings)
				{
					return;
				}
				array = new string[5] { "ReparentToTaggedParent: No object found with tag '", parentTag, null, null, null };
				obj2 = "' for ";
			}
			array[2] = (string)obj2;
			string text2 = base.name;
			array[3] = text2;
			array[4] = ".";
			string message = string.Concat(array);
			Debug.LogWarning(message);
		}
		else if (logWarnings)
		{
			string text3 = base.name;
			string message2 = "ReparentToTaggedParent: parentTag is empty on " + text3 + ".";
			Debug.LogWarning(message2);
		}
		return;
		IL_0161:
		Transform transform = gameObject.transform;
		Transform transform2 = base.transform;
		Transform parent = transform2.parent;
		if (parent != transform)
		{
			Transform transform3 = base.transform;
			transform3.SetParent(transform, worldPositionStays: true);
			_done = true;
			onReparentComplete.Invoke();
		}
		else
		{
			_done = true;
		}
	}

	public ReparentToTaggedParent()
	{
		UnityEvent unityEvent = new UnityEvent();
		onReparentComplete = unityEvent;
		base._002Ector();
	}
}
