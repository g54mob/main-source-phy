using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ClipboardToolSelector : MonoBehaviour
{
	[Serializable]
	public class ToolChangedEvent : UnityEvent<ClipboardToolSlot>
	{
	}

	private class TransitionState
	{
		public Vector3 startPos;

		public Quaternion startRot;

		public Vector3 startScale;

		public Vector3 endPos;

		public Quaternion endRot;

		public Vector3 endScale;

		public float startTime;

		public float duration;
	}

	private static class ListPool<T>
	{
		private static readonly Stack<List<T>> Pool;

		public static List<T> Get()
		{
			//IL_002a: Expected O, but got I
			//IL_003f: Expected O, but got I
			//IL_012e: Expected O, but got I
			//IL_0148: Expected O, but got I4
			//IL_00aa: Expected O, but got I
			//IL_00c4: Expected O, but got I4
			//IL_018a: Expected O, but got I
			//IL_019f: Expected O, but got I
			//IL_0209: Expected O, but got I
			//IL_0239: Expected O, but got I
			//IL_0249: Expected O, but got I
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v9 (Il2CppRgctx<ClipboardToolSelector+ListPool`1>)+10]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rax_v11+B8]");
			object obj2 = 0;
			object obj3 = obj2;
			List<T> result;
			if (obj2 != null)
			{
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rbx_v1+18]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ rax_v16 (Il2CppClass<ClipboardToolSelector+ListPool`1>)+135]");
					object obj4 = (nint)0 & (nint)1;
					bool flag = obj4 == null;
					object obj5 = !flag;
					if (obj5 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
					}
					nint num3 = 0;
					List<T> list = null;
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
					result = list;
					goto IL_0113;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ rax_v16 (Il2CppClass<ClipboardToolSelector+ListPool`1>)+135]");
				object obj6 = (nint)0 & (nint)1;
				bool flag2 = obj6 == null;
				object obj7 = !flag2;
				if (obj7 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
				}
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ rax_v24 (Il2CppRgctx<ClipboardToolSelector+ListPool`1>)+10]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ rax_v26+B8]");
				object obj9 = 0;
				if (obj9 != null)
				{
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
					List<T> list2 = default(List<T>);
					if (list2 != null)
					{
						nint num7 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ rax_v33 (Il2CppRgctx<ClipboardToolSelector+ListPool`1>)+30]");
						object obj10 = 0;
						int version = list2._version + 1;
						list2._version = version;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ rcx_v17+20]");
						object obj11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rax_v34+C0]");
						object obj12 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
						object obj13 = default(object);
						if (obj13 == null)
						{
							list2._size = 0;
							return list2;
						}
						list2._size = 0;
						bool flag3 = list2._size <= 0;
						result = list2;
						if (!flag3)
						{
							Array.Clear(list2._items, 0, list2._size);
							return list2;
						}
						goto IL_0113;
					}
				}
			}
			return (List<T>)(object)new NullReferenceException();
			IL_0113:
			return result;
		}

		public static void Release(List<T> list)
		{
			//IL_0038: Expected O, but got I
			//IL_0068: Expected O, but got I
			//IL_0078: Expected O, but got I
			//IL_0133: Expected O, but got I
			//IL_0148: Expected O, but got I
			if (list == null)
			{
				return;
			}
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v4 (Il2CppRgctx<ClipboardToolSelector+ListPool`1>)+30]");
			object obj = 0;
			int version = list._version + 1;
			list._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rcx_v3+20]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v5+C0]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj4 = default(object);
			if (obj4 == null)
			{
				list._size = 0;
			}
			else
			{
				list._size = 0;
				if (list._size > 0)
				{
					Array.Clear(list._items, 0, list._size);
				}
			}
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rax_v16 (Il2CppRgctx<ClipboardToolSelector+ListPool`1>)+10]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v239 @ rax_v18+B8]");
			object obj6 = 0;
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1809176D0");
		}

		static ListPool()
		{
			//IL_0045: Expected O, but got I
			//IL_005a: Expected O, but got I
			nint num = 0;
			object obj = null;
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180918060");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v12 (Il2CppRgctx<ClipboardToolSelector+ListPool`1>)+10]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v14+B8]");
			object obj3 = 0;
			obj3 = obj;
		}
	}

	private sealed class _003CAutoSelectOnFirstEnableRoutine_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClipboardToolSelector _003C_003E4__this;

		private float _003Cstart_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoSelectOnFirstEnableRoutine_003Ed__40(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_00aa: Expected I4, but got I8
			//IL_03b1: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0096: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_03fb: Invalid comparison between I and F4
			//IL_01ec: Expected O, but got I
			//IL_028e: Expected O, but got I4
			//IL_024c: Expected O, but got I
			//IL_0280: Expected O, but got I
			//IL_0348: Expected O, but got I
			//IL_0368: Expected F4, but got I
			Behaviour behaviour = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			float num;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (flag)
				{
					_003C_003E1__state = -1;
					goto IL_00f7;
				}
				if ((nint)obj != 1)
				{
					goto IL_0385;
				}
				_003C_003E1__state = -1;
				float unscaledTime = Time.unscaledTime;
				if ((object)_003C_003E4__this != null)
				{
					num = unscaledTime;
					goto IL_03da;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					_ = 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+68]");
					if ((nint)0 == 0)
					{
						goto IL_00f7;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			goto IL_03a3;
			IL_0184:
			_ = 0;
			if (_003C_003E4__this.isActiveAndEnabled)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+38]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+38]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rax_v9+18]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+64]");
						if ((nint)0 >= (nint)0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rax_v9+18]");
							object obj3 = -1;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+64]");
							if (0 <= (nint)obj3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+64]");
								obj3 = 0;
							}
						}
						else
						{
							object obj3 = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						UnityEngine.Object obj4 = default(UnityEngine.Object);
						if (obj4 != null)
						{
							_003C_003E4__this.SelectTool((ClipboardToolSlot)obj4);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+73]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+74]");
								if ((nint)0 > (nint)0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+B0]");
									if ((nint)0 != 0)
									{
										ClipboardToolSelector clipboardToolSelector = _003C_003E4__this;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+B0]");
										clipboardToolSelector.StopCoroutine((Coroutine)0);
									}
									_003CCursorOverrideRetryRoutine_003Ed__41 obj5 = new _003CCursorOverrideRetryRoutine_003Ed__41(0);
									obj5._003C_003E1__state = 0;
									obj5._003C_003E4__this = _003C_003E4__this;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+74]");
									obj5.retrySeconds = 0f;
									Coroutine coroutine = _003C_003E4__this.StartCoroutine(obj5);
								}
							}
						}
					}
				}
			}
			goto IL_0385;
			IL_03a3:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_03da:
			float num2 = num - _003Cstart_003E5__2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+6C]");
			if (0f > num2)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			goto IL_0184;
			IL_00f7:
			if ((object)_003C_003E4__this == null)
			{
				goto IL_03a3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Behaviour)+6C]");
			if ((nint)0 <= (nint)0)
			{
				goto IL_0184;
			}
			float unscaledTime2 = Time.unscaledTime;
			_003Cstart_003E5__2 = unscaledTime2;
			float unscaledTime3 = Time.unscaledTime;
			num = unscaledTime3;
			goto IL_03da;
			IL_0385:
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

	private sealed class _003CCursorOverrideRetryRoutine_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClipboardToolSelector _003C_003E4__this;

		public float retrySeconds;

		private float _003Cstart_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCursorOverrideRetryRoutine_003Ed__41(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_005d: Expected I4, but got I8
			//IL_014c: Expected I4, but got O
			//IL_00dc: Expected O, but got I
			//IL_0110: Expected O, but got I
			Behaviour behaviour = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				float unscaledTime = Time.unscaledTime;
				_003Cstart_003E5__2 = unscaledTime;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0138;
				}
				_003C_003E1__state = -1;
			}
			if ((object)_003C_003E4__this != null)
			{
				if (_003C_003E4__this.isActiveAndEnabled)
				{
					float unscaledTime2 = Time.unscaledTime;
					float num = unscaledTime2 - _003Cstart_003E5__2;
					if (retrySeconds > num)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rdi_v1 (UnityEngine.Behaviour)+88]");
						if ((UnityEngine.Object)0 != null)
						{
							ClipboardToolSelector clipboardToolSelector = _003C_003E4__this;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rdi_v1 (UnityEngine.Behaviour)+88]");
							clipboardToolSelector.ApplyMapCursorOverrideForSelection((ClipboardToolSlot)0);
						}
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
				_ = 0;
				goto IL_0138;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0138:
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

	public MapMarkerPlacer mapMarkerPlacer;

	public Transform selectedAnchor;

	public InteractableRuntimeCursorOverride mapCursorOverride;

	public List<ClipboardToolSlot> slots;

	public bool autoFindSlotsInChildrenIfEmpty;

	public bool animateTransitions;

	public float transitionSeconds;

	public AnimationCurve transitionCurve;

	public bool enableDepthPopDuringTransition;

	public float depthPopAmount;

	public AnimationCurve depthPopCurve;

	public bool autoSelectOnFirstEnable;

	public int autoSelectIndex;

	public bool autoSelectWaitOneFrame;

	public float autoSelectDelaySeconds;

	public bool allowHoverVisualsOnSelectedTool;

	public bool applyHoverCursorOverride;

	public bool applyGrabCursorOverride;

	public bool reapplyCursorOverrideAfterAutoSelect;

	public float cursorOverrideRetrySeconds;

	public ToolChangedEvent onToolChanged;

	public bool debugLogs;

	private ClipboardToolSlot _003CCurrentSelected_003Ek__BackingField;

	private ClipboardToolSlot _003CCurrentHovered_003Ek__BackingField;

	private readonly Dictionary<ClipboardToolSlot, TransitionState> _transitions;

	private bool _didAutoSelectOnceThisInstance;

	private Coroutine _autoSelectRoutine;

	private Coroutine _cursorRetryRoutine;

	public ClipboardToolSlot CurrentSelected
	{
		get
		{
			return _003CCurrentSelected_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentSelected_003Ek__BackingField = value;
		}
	}

	public ClipboardToolSlot CurrentHovered
	{
		get
		{
			return _003CCurrentHovered_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentHovered_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		//IL_023e: Expected O, but got I4
		//IL_0247: Expected O, but got I4
		//IL_021d: Expected O, but got I4
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		_didAutoSelectOnceThisInstance = false;
		bool flag = selectedAnchor == null;
		if (!flag)
		{
			if (autoFindSlotsInChildrenIfEmpty != flag)
			{
				if (slots != null)
				{
					List<ClipboardToolSlot> list = slots;
					if (list._size != 0)
					{
						goto IL_0114;
					}
				}
				ClipboardToolSlot[] componentsInChildren = GetComponentsInChildren<ClipboardToolSlot>(includeInactive: true);
				List<ClipboardToolSlot> list2 = new List<ClipboardToolSlot>(componentsInChildren);
				slots = list2;
				if (debugLogs)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"[ClipboardToolSelector] Auto-found {arg} slot(s) in children.";
					Debug.Log(message, this);
				}
			}
			goto IL_0114;
		}
		Debug.LogError("ClipboardToolSelector: Assign 'Selected Anchor' (top of clipboard pose).", this);
		base.enabled = false;
		return;
		IL_0114:
		if (slots == null)
		{
			List<ClipboardToolSlot> list3 = new List<ClipboardToolSlot>();
			slots = list3;
		}
		List<ClipboardToolSlot> list4 = slots;
		bool flag2 = (nint)slots < 0;
		int num = list4._size - 1;
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!flag2)
		{
			object obj2;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag3 = obj == null;
				bool flag4 = (flag3 ? 1 : 0) < (false ? 1 : 0);
				if (flag3)
				{
					flag4 = (nint)slots < 0;
					slots.RemoveAt(num);
				}
				num--;
				obj2 = !flag4;
			}
			while (obj2 != null);
		}
		List<ClipboardToolSlot> list5 = slots;
		object obj3 = 0;
		object obj4 = 0;
		while ((nint)obj4 < list5._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj != null)
			{
				((ClipboardToolSlot)obj).ApplySelected(false);
				((ClipboardToolSlot)obj).ApplyHover(false);
			}
			list5 = slots;
			obj3++;
			obj4 = obj3;
		}
	}

	private void OnEnable()
	{
		if (autoSelectOnFirstEnable && !_didAutoSelectOnceThisInstance)
		{
			if (_autoSelectRoutine != null)
			{
				StopCoroutine(_autoSelectRoutine);
			}
			_003CAutoSelectOnFirstEnableRoutine_003Ed__40 obj = new _003CAutoSelectOnFirstEnableRoutine_003Ed__40(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine autoSelectRoutine = StartCoroutine(obj);
			_autoSelectRoutine = autoSelectRoutine;
		}
	}

	private void OnDisable()
	{
		if (_autoSelectRoutine != null)
		{
			StopCoroutine(_autoSelectRoutine);
			_autoSelectRoutine = null;
		}
		if (_cursorRetryRoutine != null)
		{
			StopCoroutine(_cursorRetryRoutine);
			_cursorRetryRoutine = null;
		}
	}

	public void RefreshMapCursorOverride()
	{
		if (_003CCurrentSelected_003Ek__BackingField != null)
		{
			ApplyMapCursorOverrideForSelection(_003CCurrentSelected_003Ek__BackingField);
		}
	}

	private IEnumerator AutoSelectOnFirstEnableRoutine()
	{
		_003CAutoSelectOnFirstEnableRoutine_003Ed__40 obj = new _003CAutoSelectOnFirstEnableRoutine_003Ed__40(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator CursorOverrideRetryRoutine(float retrySeconds)
	{
		_003CCursorOverrideRetryRoutine_003Ed__41 obj = new _003CCursorOverrideRetryRoutine_003Ed__41(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.retrySeconds = retrySeconds;
		return obj;
	}

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0089: Expected O, but got I4
		//IL_009a: Expected O, but got I4
		//IL_00ad: Expected O, but got Ref
		//IL_00ce: Expected O, but got I
		//IL_010e: Expected O, but got I
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Expected O, but got Unknown
		//IL_04bd: Expected O, but got I
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		//IL_013b: Expected O, but got I
		//IL_01ca: Invalid comparison between I4 and F4
		//IL_0215: Expected F4, but got I4
		//IL_02d6: Expected O, but got I
		//IL_0334: Expected O, but got I
		//IL_0236: Invalid comparison between I4 and F4
		//IL_0299: Expected F4, but got I4
		//IL_03e3: Expected O, but got I
		//IL_055e: Expected O, but got I
		//IL_03f9: Expected O, but got Ref
		//IL_0407: Expected O, but got Ref
		//IL_0376: Invalid comparison between F4 and I4
		//IL_0433: Expected O, but got Ref
		//IL_0282: Expected O, but got I
		//IL_0480: Expected F4, but got I
		//IL_04a1: Expected O, but got I
		//IL_0467: Expected O, but got I
		//IL_03cd: Expected F4, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		if (!animateTransitions || _transitions.Count == 0)
		{
			return;
		}
		List<ClipboardToolSlot> list = ListPool<ClipboardToolSlot>.Get();
		Dictionary<ClipboardToolSlot, TransitionState>.KeyCollection keys = _transitions.Keys;
		list.AddRange(keys);
		float unscaledTime = Time.unscaledTime;
		object obj3 = 0;
		float num = unscaledTime;
		object obj4 = 0;
		Quaternion b = default(Quaternion);
		float num15 = default(float);
		float num16 = default(float);
		float num17 = default(float);
		while ((nint)obj4 < list._size)
		{
			object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 152));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
			float num2;
			float num7;
			if ((UnityEngine.Object)0 != null)
			{
				ref TransitionState value = ref System.Runtime.CompilerServices.Unsafe.As<object, TransitionState>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
				Dictionary<ClipboardToolSlot, TransitionState> transitions = _transitions;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
				if (transitions.TryGetValue((ClipboardToolSlot)0, out value))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+90]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+90]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v25+64]");
						if ((nint)0 >= (nint)0)
						{
							num2 = 1f;
						}
						else
						{
							float num3 = num;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v25+60]");
							float num4 = num3 - 0f;
							float num5 = num4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v25+64]");
							num2 = num5 / 0f;
							if (!(0f > num2))
							{
								if (num2 > 1f)
								{
									num2 = 1f;
								}
							}
							else
							{
								num2 = 0f;
							}
						}
						if (transitionCurve != null)
						{
							float num6 = transitionCurve.Evaluate(num2);
							if (!(0f > num6))
							{
								bool flag = !(num6 > 1f);
								num7 = num6;
								if (!flag)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+90]");
									obj6 = 0;
									num7 = 1f;
									goto IL_02ab;
								}
							}
							else
							{
								num7 = 0f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+90]");
							obj6 = 0;
						}
						else
						{
							num7 = num2;
						}
						goto IL_02ab;
					}
				}
				goto IL_0514;
			}
			Dictionary<ClipboardToolSlot, TransitionState> transitions2 = _transitions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
			bool flag2 = transitions2.Remove((ClipboardToolSlot)0);
			obj3++;
			obj4 = obj3;
			continue;
			IL_0514:
			obj3++;
			obj4 = obj3;
			continue;
			IL_02ab:
			ref Quaternion a = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v25+40]");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v25+18]");
			object obj7 = num8 - 0;
			float num9 = (float)obj7 * num7;
			float num10 = num9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v25+18]");
			float num11 = num10 + 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v25+1C]");
			_ = 0;
			Quaternion quaternion = Quaternion.Internal_SlerpUnclamped(ref a, ref b, num7);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+90]");
			object obj8 = 0;
			_ = quaternion.x;
			if (enableDepthPopDuringTransition && depthPopAmount > 0f)
			{
				float num12 = ((depthPopCurve == null) ? 0f : depthPopCurve.Evaluate(num2));
				float num13 = num12 * depthPopAmount;
				float num14 = num13 + num11;
				num11 = num14;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
			Transform transform = ((Component)0).transform;
			transform.localPosition = (Vector3)(&num15);
			Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-80]");
			_ = 0;
			transform.localRotation = localRotation;
			transform.localScale = (Vector3)(&num16);
			if (!(num2 < 1f))
			{
				Dictionary<ClipboardToolSlot, TransitionState> transitions3 = _transitions;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
				bool flag3 = transitions3.Remove((ClipboardToolSlot)0);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+80]");
			num = 0f;
			num16 = num17;
			num15 = num17;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ rax_v27+54]");
			b = (Quaternion)0;
			goto IL_0514;
		}
		ListPool<ClipboardToolSlot>.Release(list);
	}

	public void NotifyToolHoverEnter(ClipboardToolSlot slot)
	{
		if (slot != null && _003CCurrentHovered_003Ek__BackingField != slot)
		{
			if (_003CCurrentHovered_003Ek__BackingField != null)
			{
				_003CCurrentHovered_003Ek__BackingField.ApplyHover(hovered: false);
			}
			_003CCurrentHovered_003Ek__BackingField = slot;
			if (!IsHoverSuppressedBySelection(slot))
			{
				slot.ApplyHover(hovered: true);
			}
			if (debugLogs)
			{
				string text = slot.name;
				string message = "[ClipboardToolSelector] Hover enter: " + text;
				Debug.Log(message, this);
			}
		}
	}

	public void NotifyToolHoverExit(ClipboardToolSlot slot)
	{
		if (slot != null && _003CCurrentHovered_003Ek__BackingField == slot)
		{
			slot.ApplyHover(hovered: false);
			_003CCurrentHovered_003Ek__BackingField = null;
			if (debugLogs)
			{
				string text = slot.name;
				string message = "[ClipboardToolSelector] Hover exit: " + text;
				Debug.Log(message, this);
			}
		}
	}

	public unsafe void SelectTool(ClipboardToolSlot slot)
	{
		//IL_05ea: Expected O, but got I4
		//IL_05f3: Expected O, but got I4
		//IL_00bb: Expected O, but got Ref
		//IL_00bb: Expected O, but got Ref
		//IL_00cb: Expected F4, but got I
		//IL_00db: Expected O, but got I
		//IL_01d0: Expected O, but got Ref
		//IL_01d0: Expected O, but got Ref
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Expected O, but got Unknown
		if (!(slot != null))
		{
			return;
		}
		if (_003CCurrentSelected_003Ek__BackingField != slot)
		{
			float num = default(float);
			Vector3 targetScale = default(Vector3);
			object obj3 = default(object);
			if (_003CCurrentSelected_003Ek__BackingField != null)
			{
				UnityEngine.Object obj = _003CCurrentSelected_003Ek__BackingField;
				if (_003CCurrentSelected_003Ek__BackingField != null)
				{
					object obj2 = default(object);
					MoveSlot(_003CCurrentSelected_003Ek__BackingField, (Vector3)(&obj2), (Quaternion)(&num), targetScale);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rbp_v18 (UnityEngine.Object)+78]");
					num = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rbp_v18 (UnityEngine.Object)+88]");
					obj3 = 0;
				}
				_003CCurrentSelected_003Ek__BackingField.ApplySelected(selected: false);
				if (_003CCurrentHovered_003Ek__BackingField == _003CCurrentSelected_003Ek__BackingField && !IsHoverSuppressedBySelection(_003CCurrentSelected_003Ek__BackingField))
				{
					_003CCurrentSelected_003Ek__BackingField.ApplyHover(hovered: true);
				}
			}
			_003CCurrentSelected_003Ek__BackingField = slot;
			if (slot != null)
			{
				Vector3 localPosition = selectedAnchor.localPosition;
				Quaternion localRotation = selectedAnchor.localRotation;
				Vector3 localScale = selectedAnchor.localScale;
				MoveSlot(slot, (Vector3)(&obj3), (Quaternion)(&num), targetScale);
			}
			slot.ApplySelected(selected: true);
			if (_003CCurrentHovered_003Ek__BackingField == slot)
			{
				bool hovered = !IsHoverSuppressedBySelection(slot);
				slot.ApplyHover(hovered);
			}
			if (mapMarkerPlacer != null && slot.markerPrefab != null)
			{
				mapMarkerPlacer.SetActiveMarkerPrefab(slot.markerPrefab);
			}
			ApplyMapCursorOverrideForSelection(slot);
			if (debugLogs)
			{
				string[] array = new string[5] { "[ClipboardToolSelector] Selected tool: ", null, null, null, null };
				string text = slot.name;
				array[1] = text;
				array[2] = " (markerPrefab=";
				string text2 = ((!(slot.markerPrefab != null)) ? "<null>" : slot.markerPrefab.name);
				array[3] = text2;
				array[4] = ")";
				string message = string.Concat(array);
				Debug.Log(message, this);
			}
			if (onToolChanged != null)
			{
				onToolChanged.Invoke(slot);
			}
			return;
		}
		if (debugLogs)
		{
			string text3 = slot.name;
			string message2 = "[ClipboardToolSelector] SelectTool called on already-selected slot: " + text3;
			Debug.Log(message2, this);
		}
		List<ClipboardToolSlot> list = slots;
		object obj4 = 0;
		object obj5 = 0;
		UnityEngine.Object obj6 = default(UnityEngine.Object);
		while ((nint)obj5 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj6 != null)
			{
				bool selected = obj6 == _003CCurrentSelected_003Ek__BackingField;
				bool hovered2;
				if (!(obj6 == _003CCurrentHovered_003Ek__BackingField))
				{
					hovered2 = false;
				}
				else
				{
					bool flag = IsHoverSuppressedBySelection((ClipboardToolSlot)obj6);
					hovered2 = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
				}
				((ClipboardToolSlot)obj6).ApplySelected(selected);
				((ClipboardToolSlot)obj6).ApplyHover(hovered2);
			}
			list = slots;
			obj4++;
			bool flag2 = slots != null;
			obj5 = obj4;
			if (!flag2)
			{
				throw new NullReferenceException();
			}
		}
	}

	private bool IsHoverSuppressedBySelection(ClipboardToolSlot slot)
	{
		bool flag = slot == null;
		if (!flag)
		{
			if (allowHoverVisualsOnSelectedTool == flag)
			{
				return _003CCurrentSelected_003Ek__BackingField == slot;
			}
			return false;
		}
		return true;
	}

	private void RefreshVisualsForAll()
	{
		//IL_00fa: Expected O, but got I4
		//IL_0103: Expected O, but got I4
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		List<ClipboardToolSlot> list = slots;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				bool selected = obj3 == _003CCurrentSelected_003Ek__BackingField;
				bool hovered;
				if (!(obj3 == _003CCurrentHovered_003Ek__BackingField))
				{
					hovered = false;
				}
				else
				{
					bool flag = IsHoverSuppressedBySelection((ClipboardToolSlot)obj3);
					hovered = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
				}
				((ClipboardToolSlot)obj3).ApplySelected(selected);
				((ClipboardToolSlot)obj3).ApplyHover(hovered);
			}
			list = slots;
			obj++;
			obj2 = obj;
		}
	}

	private unsafe void MoveSlotToSelectedAnchor(ClipboardToolSlot slot)
	{
		//IL_006e: Expected O, but got Ref
		//IL_006e: Expected O, but got Ref
		if (slot != null)
		{
			Vector3 localPosition = selectedAnchor.localPosition;
			Quaternion localRotation = selectedAnchor.localRotation;
			Vector3 localScale = selectedAnchor.localScale;
			object obj = default(object);
			object obj2 = default(object);
			Vector3 targetScale = default(Vector3);
			MoveSlot(slot, (Vector3)(&obj), (Quaternion)(&obj2), targetScale);
		}
	}

	private unsafe void MoveSlotToRest(ClipboardToolSlot slot)
	{
		//IL_0037: Expected O, but got Ref
		//IL_0037: Expected O, but got Ref
		if (slot != null)
		{
			object obj = default(object);
			object obj2 = default(object);
			Vector3 targetScale = default(Vector3);
			MoveSlot(slot, (Vector3)(&obj), (Quaternion)(&obj2), targetScale);
		}
	}

	private unsafe void MoveSlot(ClipboardToolSlot slot, Vector3 targetPos, Quaternion targetRot, Vector3 targetScale)
	{
		//IL_0195: Expected O, but got Ref
		//IL_01a3: Expected O, but got Ref
		//IL_01b5: Expected O, but got Ref
		//IL_0080: Expected O, but got F4
		//IL_00a9: Expected O, but got F4
		//IL_00cd: Expected O, but got F4
		//IL_00df: Expected O, but got F4
		//IL_00fb: Expected O, but got F4
		if (!(slot != null))
		{
			return;
		}
		Transform transform = slot.transform;
		if (animateTransitions)
		{
			TransitionState transitionState = new TransitionState();
			Vector3 localPosition = transform.localPosition;
			transitionState.startPos = (Vector3)localPosition.x;
			_ = localPosition.z;
			transitionState.startRot = (Quaternion)transform.localRotation.x;
			Vector3 localScale = transform.localScale;
			transitionState.startScale = (Vector3)localScale.x;
			transitionState.endPos = (Vector3)targetPos.x;
			_ = targetPos.z;
			transitionState.endRot = (Quaternion)targetRot.x;
			_ = localScale.z;
			object endScale = default(object);
			transitionState.endScale = (Vector3)endScale;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ stack_28+8]");
			_ = 0;
			float unscaledTime = Time.unscaledTime;
			transitionState.startTime = unscaledTime;
			bool flag = !(0.0001f < transitionSeconds);
			float duration = 0.0001f;
			if (!flag)
			{
				duration = transitionSeconds;
			}
			transitionState.duration = duration;
			_transitions.set_Item(slot, transitionState);
		}
		else
		{
			float num = default(float);
			transform.localPosition = (Vector3)(&num);
			transform.localRotation = (Quaternion)(&num);
			transform.localScale = (Vector3)(&num);
			bool flag2 = _transitions.Remove(slot);
		}
	}

	private void ApplyMapCursorOverrideForSelection(ClipboardToolSlot slot)
	{
		//IL_01a6: Expected O, but got I
		UnityEngine.Object context;
		object message3;
		if (mapCursorOverride != null)
		{
			bool flag = slot == null;
			if (!flag && slot.overrideMapCursorWhileSelected != flag)
			{
				bool flag2 = slot.selectedCursorTexture == null;
				if (!flag2)
				{
					if (applyHoverCursorOverride == flag2)
					{
						mapCursorOverride.ClearHoverOverride();
					}
					else
					{
						UnityEngine.Object obj = mapCursorOverride;
						_ = slot.selectedCursorTexture;
						bool flag3 = slot.selectedCursorTexture != null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rbx_v16 (UnityEngine.Object)+48]");
						if ((nint)0 != 0)
						{
							string arg = obj.name;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							string text = ((!(slot.selectedCursorTexture != null)) ? "<null>" : slot.selectedCursorTexture.name);
							object arg2 = default(object);
							string message = $"[InteractableRuntimeCursorOverride:{arg}] HoverOverride set. Active={arg2} Tex={text}";
							Debug.Log(message, obj);
							object obj2 = text;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rbx_v16 (UnityEngine.Object)+29]");
							object obj3 = 0;
						}
					}
					if (!applyGrabCursorOverride)
					{
						mapCursorOverride.ClearGrabOverride();
					}
					else
					{
						UnityEngine.Object obj4 = mapCursorOverride;
						_ = slot.selectedCursorTexture;
						bool flag4 = slot.selectedCursorTexture != null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v347 @ rbx_v15 (UnityEngine.Object)+48]");
						if ((nint)0 != 0)
						{
							string arg3 = obj4.name;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							string text2 = ((!(slot.selectedCursorTexture != null)) ? "<null>" : slot.selectedCursorTexture.name);
							object arg4 = default(object);
							string message2 = $"[InteractableRuntimeCursorOverride:{arg3}] GrabOverride set. Active={arg4} Tex={text2}";
							Debug.Log(message2, obj4);
							object obj2 = text2;
						}
					}
					if (!debugLogs)
					{
						return;
					}
					string[] array = new string[7] { "[ClipboardToolSelector] Map cursor override set from '", null, null, null, null, null, null };
					string text3 = slot.name;
					array[1] = text3;
					array[2] = "'. Hover=";
					bool flag5 = !applyHoverCursorOverride;
					object obj5 = "OFF";
					if (!flag5)
					{
						obj5 = "ON";
					}
					array[3] = (string)obj5;
					array[4] = " Grab=";
					bool flag6 = !applyGrabCursorOverride;
					object obj6 = "OFF";
					if (!flag6)
					{
						obj6 = "ON";
					}
					array[5] = (string)obj6;
					array[6] = ".";
					string text4 = string.Concat(array);
					context = this;
					message3 = text4;
					goto IL_050c;
				}
			}
			mapCursorOverride.ClearHoverOverride();
			mapCursorOverride.ClearGrabOverride();
			if (debugLogs)
			{
				context = this;
				message3 = "[ClipboardToolSelector] Map cursor override cleared (ClearAll).";
				goto IL_050c;
			}
			return;
		}
		if (debugLogs)
		{
			Debug.Log("[ClipboardToolSelector] Map cursor override provider is NULL; cannot apply override yet.", this);
		}
		return;
		IL_050c:
		Debug.Log(message3, context);
	}

	public ClipboardToolSelector()
	{
		List<ClipboardToolSlot> list = new List<ClipboardToolSlot>();
		slots = list;
		autoFindSlotsInChildrenIfEmpty = true;
		transitionSeconds = 0.15f;
		transitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		enableDepthPopDuringTransition = true;
		depthPopAmount = 0.02f;
		Keyframe[] keys = new Keyframe[3];
		Keyframe keyframe = new Keyframe(0f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe2 = new Keyframe(0.5f, 1f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe3 = new Keyframe(1f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		depthPopCurve = new AnimationCurve(keys);
		autoSelectOnFirstEnable = true;
		autoSelectWaitOneFrame = true;
		autoSelectDelaySeconds = 0.05f;
		allowHoverVisualsOnSelectedTool = true;
		reapplyCursorOverrideAfterAutoSelect = true;
		cursorOverrideRetrySeconds = 0.25f;
		onToolChanged = new ToolChangedEvent();
		Dictionary<ClipboardToolSlot, TransitionState> dictionary = new Dictionary<ClipboardToolSlot, TransitionState>();
		dictionary._002Ector();
		_transitions = dictionary;
		base._002Ector();
	}
}
