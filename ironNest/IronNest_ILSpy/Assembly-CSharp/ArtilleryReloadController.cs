using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ArtilleryReloadController : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public string stateKey;

		internal bool _003CGoToState_003Eb__0(ReloadStateDef s)
		{
			//IL_0048: Expected I4, but got O
			if (s != null)
			{
				return s.stateKey == stateKey;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003CAutoAdvanceToNextStateCoroutine_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ArtilleryReloadController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoAdvanceToNextStateCoroutine_003Ed__27(int _003C_003E1__state)
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
				_003C_003E4__this.AdvanceState();
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

	public List<Animator> animators;

	public List<ReloadStateDef> reloadStates;

	public Transform chamberSlot;

	public GameObject chamberedShell;

	public GameObject shellPrefab;

	private int currentStateIndex;

	public CylinderShellSelector cylinderShellSelector;

	public Transform transferSlot;

	public GameObject transferShell;

	public GunController gunController;

	private bool working;

	private Action<ReloadStateDef> m_OnStateChanged;

	private readonly Dictionary<LookAtTarget, bool> buttonListenerRegistered;

	public ReloadStateDef CurrentState
	{
		get
		{
			if (reloadStates != null)
			{
				List<ReloadStateDef> list = reloadStates;
				if (list._size > 0 && currentStateIndex < list._size)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					ReloadStateDef result = default(ReloadStateDef);
					return result;
				}
			}
			return null;
		}
	}

	public int CurrentStateIndex => currentStateIndex;

	public event Action<ReloadStateDef> OnStateChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 120;
			Delegate obj2 = this.m_OnStateChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 120;
			Delegate obj2 = this.m_OnStateChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Start()
	{
		chamberedShell = null;
		transferShell = null;
		RegisterAdvanceButtonListeners();
		SetState(0, force: true);
	}

	private unsafe void RegisterAdvanceButtonListeners()
	{
		//IL_007e: Expected O, but got I
		//IL_00d2: Expected O, but got I
		//IL_0135: Expected O, but got I
		//IL_0155: Expected O, but got I
		buttonListenerRegistered.Clear();
		if (reloadStates == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ReloadStateDef>.Enumerator enumerator = default(List<ReloadStateDef>.Enumerator);
		object obj = default(object);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj == null)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_18_v4+30]");
				if (!((UnityEngine.Object)0 != null))
				{
					continue;
				}
				if (buttonListenerRegistered != null)
				{
					Dictionary<LookAtTarget, bool> dictionary = buttonListenerRegistered;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_18_v4+30]");
					if (!dictionary.ContainsKey((LookAtTarget)0))
					{
						UnityAction action = AdvanceState;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_18_v4+30]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_18_v4+30]");
						((LookAtTarget)0).RegisterOnClickDown(action);
						Dictionary<LookAtTarget, bool> dictionary2 = buttonListenerRegistered;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_18_v4+30]");
						dictionary2.set_Item((LookAtTarget)0, (byte)(&obj2) != 0);
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

	public void AdvanceState()
	{
		//IL_004b: Expected O, but got I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected I4, but got Unknown
		if (reloadStates != null)
		{
			List<ReloadStateDef> list = reloadStates;
			if (list._size != 0)
			{
				List<ReloadStateDef> list2 = reloadStates;
				object obj = currentStateIndex + 1;
				int newIndex = obj % list2._size;
				SetState(newIndex);
			}
		}
	}

	public void RegressState()
	{
		if (reloadStates == null)
		{
			return;
		}
		List<ReloadStateDef> list = reloadStates;
		if (list._size != 0)
		{
			int newIndex = currentStateIndex - 1;
			if (list._size < 0)
			{
				newIndex = list._size - 1;
			}
			SetState(newIndex);
		}
	}

	public void SetState(int newIndex, bool force = false)
	{
		//IL_02cc: Expected O, but got I4
		//IL_02d5: Expected O, but got I4
		//IL_0304: Expected O, but got I4
		//IL_030d: Expected O, but got I4
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0199: Expected O, but got I
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_01e4: Expected O, but got I
		//IL_0240: Expected O, but got I
		//IL_03ec: Expected O, but got I4
		//IL_042e: Expected O, but got I4
		if (reloadStates == null)
		{
			return;
		}
		List<ReloadStateDef> list = reloadStates;
		if (list._size == 0 || (working && !force) || (currentStateIndex == newIndex && !force))
		{
			return;
		}
		int num = list._size - 1;
		int num2;
		if (newIndex >> 31 == 0)
		{
			bool flag = newIndex <= num;
			num2 = newIndex;
			if (!flag)
			{
				num2 = num;
			}
		}
		else
		{
			num2 = 0;
		}
		currentStateIndex = num2;
		working = true;
		if (reloadStates != null)
		{
			List<ReloadStateDef> list2 = reloadStates;
			UnityEngine.Object obj = null;
			UnityEngine.Object obj2 = null;
			bool flag2 = default(bool);
			while ((nint)obj < list2._size)
			{
				if (reloadStates != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v438 @ stack_-B8_v9 (System.Boolean)+30]");
						if ((UnityEngine.Object)0 != null)
						{
							object obj3 = obj2 - currentStateIndex;
							bool flag3 = obj3 == null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v438 @ stack_-B8_v9 (System.Boolean)+30]");
							LookAtTarget lookAtTarget = (LookAtTarget)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v438 @ stack_-B8_v9 (System.Boolean)+30]");
							if ((nint)0 == 0)
							{
								goto IL_056a;
							}
							if (lookAtTarget.isActive != flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v438 @ stack_-B8_v9 (System.Boolean)+30]");
								((LookAtTarget)0).SetActive(flag3);
							}
						}
					}
					obj2 = (UnityEngine.Object)(obj2 + 1);
					list2 = reloadStates;
					if (reloadStates != null)
					{
						obj = obj2;
						continue;
					}
				}
				goto IL_056a;
			}
		}
		if (reloadStates != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj4 = default(object);
			bool flag4 = obj4 == null;
			object obj5 = 0;
			List<Animator>.Enumerator enumerator = (List<Animator>.Enumerator)0;
			if (!flag4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ stack_-58_v6+20]");
				bool flag5 = (nint)0 == 0;
				obj5 = 0;
				enumerator = (List<Animator>.Enumerator)0;
				if (!flag5)
				{
					if (animators == null)
					{
						goto IL_056a;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					object obj6 = default(object);
					obj5 = obj6;
					List<Animator>.Enumerator enumerator2 = default(List<Animator>.Enumerator);
					enumerator = enumerator2;
					List<Animator>.Enumerator enumerator3 = default(List<Animator>.Enumerator);
					UnityEngine.Object obj7 = default(UnityEngine.Object);
					List<string>.Enumerator enumerator4 = default(List<string>.Enumerator);
					bool flag6 = default(bool);
					object obj8 = default(object);
					List<Animator>.Enumerator enumerator5 = default(List<Animator>.Enumerator);
					while (enumerator3.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (!(obj7 != null))
						{
							continue;
						}
						if (obj4 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ stack_-58_v6+20]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								while (enumerator4.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if (!string.IsNullOrEmpty((string)flag6))
									{
										if ((object)obj7 == null)
										{
											throw new NullReferenceException();
										}
										((Animator)obj7).SetTrigger((string)flag6);
									}
								}
								enumerator4.Dispose();
								obj5 = obj8;
								enumerator = enumerator5;
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator3.Dispose();
				}
				if (obj4 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ stack_-58_v6+28]");
					if ((nint)0 != 0 && gunController != null)
					{
						if ((object)gunController == null)
						{
							goto IL_056a;
						}
						gunController.OnReloadingComplete();
					}
				}
			}
			Action<ReloadStateDef> onStateChanged = this.m_OnStateChanged;
			if (this.m_OnStateChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v720 @ rcx_v13 (System.Action`1<ReloadStateDef>)+18] (should have been resolved before IL gen)");
			}
			if (obj4 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ stack_-58_v6+29]");
				if ((nint)0 != 0)
				{
					_003CAutoAdvanceToNextStateCoroutine_003Ed__27 obj9 = new _003CAutoAdvanceToNextStateCoroutine_003Ed__27(0);
					obj9._003C_003E1__state = 0;
					obj9._003C_003E4__this = this;
					Coroutine coroutine = StartCoroutine(obj9);
				}
			}
			working = false;
			return;
		}
		goto IL_056a;
		IL_056a:
		throw new NullReferenceException();
	}

	public void ForceResetStateToInitial()
	{
		SetState(0, force: true);
	}

	public void ResetAnimators()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<Animator>.Enumerator enumerator = default(List<Animator>.Enumerator);
		Animator animator = default(Animator);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)animator == null)
				{
					break;
				}
				animator.ResetControllerState();
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private void UpdateAllAdvanceButtons()
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0064: Expected O, but got I
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_00af: Expected O, but got I
		if (reloadStates == null)
		{
			return;
		}
		List<ReloadStateDef> list = reloadStates;
		object obj = 0;
		object obj2 = 0;
		object obj3 = default(object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ stack_8_v4+30]");
				if ((UnityEngine.Object)0 != null)
				{
					object obj4 = obj - currentStateIndex;
					bool flag = obj4 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ stack_8_v4+30]");
					LookAtTarget lookAtTarget = (LookAtTarget)0;
					if (lookAtTarget.isActive != flag)
					{
						lookAtTarget.SetActive(flag);
					}
				}
			}
			list = reloadStates;
			obj++;
			obj2 = obj;
		}
	}

	private IEnumerator AutoAdvanceToNextStateCoroutine()
	{
		_003CAutoAdvanceToNextStateCoroutine_003Ed__27 obj = new _003CAutoAdvanceToNextStateCoroutine_003Ed__27(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public void GoToState(string stateKey)
	{
		_003C_003Ec__DisplayClass28_0 CS_0024_003C_003E8__locals2 = new _003C_003Ec__DisplayClass28_0();
		CS_0024_003C_003E8__locals2.stateKey = stateKey;
		if (reloadStates == null)
		{
			return;
		}
		Predicate<ReloadStateDef> match = delegate(ReloadStateDef s)
		{
			//IL_0048: Expected I4, but got O
			if (s == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return s.stateKey == CS_0024_003C_003E8__locals2.stateKey;
		};
		int num = reloadStates.FindIndex(match);
		if (num >= 0)
		{
			SetState(num);
		}
	}

	public void OnAnimationEvent_AdvanceState()
	{
		//IL_004b: Expected O, but got I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected I4, but got Unknown
		if (reloadStates != null)
		{
			List<ReloadStateDef> list = reloadStates;
			if (list._size != 0)
			{
				List<ReloadStateDef> list2 = reloadStates;
				object obj = currentStateIndex + 1;
				int newIndex = obj % list2._size;
				SetState(newIndex);
			}
		}
	}

	public void OnAnimationEvent_RegressState()
	{
		if (reloadStates == null)
		{
			return;
		}
		List<ReloadStateDef> list = reloadStates;
		if (list._size != 0)
		{
			int newIndex = currentStateIndex - 1;
			if (list._size < 0)
			{
				newIndex = list._size - 1;
			}
			SetState(newIndex);
		}
	}

	public void OnUserInput_Advance()
	{
		//IL_004b: Expected O, but got I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected I4, but got Unknown
		if (reloadStates != null)
		{
			List<ReloadStateDef> list = reloadStates;
			if (list._size != 0)
			{
				List<ReloadStateDef> list2 = reloadStates;
				object obj = currentStateIndex + 1;
				int newIndex = obj % list2._size;
				SetState(newIndex);
			}
		}
	}

	public void OnUserInput_Regress()
	{
		if (reloadStates == null)
		{
			return;
		}
		List<ReloadStateDef> list = reloadStates;
		if (list._size != 0)
		{
			int newIndex = currentStateIndex - 1;
			if (list._size < 0)
			{
				newIndex = list._size - 1;
			}
			SetState(newIndex);
		}
	}

	public unsafe void TryLoadShell()
	{
		//IL_00b3: Expected O, but got Ref
		if (chamberedShell == null && shellPrefab != null && chamberSlot != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(shellPrefab, chamberSlot);
			chamberedShell = gameObject;
			Transform transform = chamberedShell.transform;
			object obj = default(object);
			transform.localPosition = (Vector3)(&obj);
		}
	}

	public bool CanLoadBullet()
	{
		if (working)
		{
			return false;
		}
		return chamberedShell == null;
	}

	public unsafe void ReceiveChamberedBullet(GameObject bullet)
	{
		//IL_0096: Expected O, but got Ref
		if (!working && chamberedShell == null)
		{
			working = true;
			chamberedShell = bullet;
			Transform transform = chamberedShell.transform;
			transform.parent = chamberSlot;
			Transform transform2 = chamberedShell.transform;
			object obj = default(object);
			transform2.localPosition = (Vector3)(&obj);
			working = false;
		}
		else
		{
			Debug.LogWarning("Tried to load chamber while busy or already loaded.");
		}
	}

	public void EjectChamberedShell()
	{
		if ((bool)chamberedShell)
		{
			UnityEngine.Object.Destroy(chamberedShell);
			chamberedShell = null;
		}
	}

	public void ClearTransferredShell()
	{
		if ((bool)transferShell)
		{
			UnityEngine.Object.Destroy(transferShell);
			transferShell = null;
		}
	}

	public unsafe void AnimationEvent_MoveShellToTransferSlot()
	{
		//IL_0142: Expected O, but got Ref
		if (this.cylinderShellSelector != null)
		{
			CylinderShellSelector cylinderShellSelector = this.cylinderShellSelector;
			if (cylinderShellSelector.bullets != null)
			{
				List<GameObject> bullets = cylinderShellSelector.bullets;
				if (bullets._size != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (obj != null)
					{
						if (transferSlot != null)
						{
							CylinderShellSelector cylinderShellSelector2 = this.cylinderShellSelector;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							GameObject gameObject = default(GameObject);
							transferShell = gameObject;
							Transform transform = transferShell.transform;
							transform.parent = transferSlot;
							Transform transform2 = transferShell.transform;
							object obj2 = default(object);
							transform2.localPosition = (Vector3)(&obj2);
							CylinderShellSelector cylinderShellSelector3 = this.cylinderShellSelector;
							GameObject[] shellPrefabs = cylinderShellSelector3.shellPrefabs;
							cylinderShellSelector3.lastLoadedShellPrefab = shellPrefabs[0];
							CylinderShellSelector cylinderShellSelector4 = this.cylinderShellSelector;
							cylinderShellSelector4.bullets.set_Item(0, (GameObject)null);
							this.cylinderShellSelector.UpdateButtonActives(true);
						}
						else
						{
							Debug.LogWarning("ArtilleryReloadController: TransferSlot not set!");
						}
					}
					else
					{
						Debug.Log("Tried to move bullet but A-slot is empty!");
					}
					return;
				}
			}
			Debug.LogWarning("ArtilleryReloadController: Cylinder bullets list not initialized.");
		}
		else
		{
			Debug.LogWarning("ArtilleryReloadController: CylinderShellSelector not assigned!");
		}
	}

	public unsafe void AnimationEvent_TransferShellToChamber()
	{
		//IL_00d9: Expected O, but got Ref
		if (transferShell != null)
		{
			Transform transform = transferShell.transform;
			transform.parent = null;
			if (!working && chamberedShell == null)
			{
				working = true;
				chamberedShell = transferShell;
				Transform transform2 = chamberedShell.transform;
				transform2.parent = chamberSlot;
				Transform transform3 = chamberedShell.transform;
				object obj = default(object);
				transform3.localPosition = (Vector3)(&obj);
				working = false;
			}
			else
			{
				Debug.LogWarning("Tried to load chamber while busy or already loaded.");
			}
			transferShell = null;
			if ((object)cylinderShellSelector != null)
			{
				cylinderShellSelector.UpdateButtonActives(true);
			}
		}
		else
		{
			Debug.Log("Transfer slot is empty!");
		}
	}

	public ArtilleryReloadController()
	{
		List<Animator> list = new List<Animator>();
		animators = list;
		reloadStates = new List<ReloadStateDef>();
		buttonListenerRegistered = new Dictionary<LookAtTarget, bool>();
		base._002Ector();
	}
}
