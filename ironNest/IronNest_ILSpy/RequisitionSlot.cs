using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

public class RequisitionSlot : MonoBehaviour
{
	private sealed class _003CCR_DestroyConsoleAfterDelay_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public RequisitionSlot _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCR_DestroyConsoleAfterDelay_003Ed__40(int _003C_003E1__state)
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
			//IL_0075: Expected I4, but got I8
			//IL_010a: Expected I4, but got O
			RequisitionSlot requisitionSlot = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
				_003C_003E2__current = waitForSeconds;
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
				if (requisitionSlot.pendingConsoleDestroy != null)
				{
					UnityEngine.Object.Destroy(requisitionSlot.pendingConsoleDestroy);
					requisitionSlot.pendingConsoleDestroy = null;
				}
				requisitionSlot.pendingConsoleDestroyCoroutine = null;
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

	private sealed class _003CCR_RunPunchcardGraph_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RequisitionSlot _003C_003E4__this;

		private PunchcardGraph _003CnewGraph_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCR_RunPunchcardGraph_003Ed__46(int _003C_003E1__state)
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
			//IL_03a4: Expected I4, but got I8
			//IL_0454: Expected I4, but got O
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_029b: Expected O, but got Unknown
			//IL_02a4: Expected O, but got I4
			//IL_02ad: Expected O, but got I4
			//IL_0324: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Expected O, but got Unknown
			//IL_0332: Unknown result type (might be due to invalid IL or missing references)
			//IL_0337: Expected O, but got Unknown
			RequisitionSlot requisitionSlot = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (!(requisitionSlot.CurrentCard != null))
					{
						goto IL_03f6;
					}
					PunchcardRuntime currentCard = requisitionSlot.CurrentCard;
					if ((object)requisitionSlot.CurrentCard != null)
					{
						PunchcardDefinitionV2 currentDefinition = currentCard.CurrentDefinition;
						UnityEngine.Object obj = (((object)currentCard.CurrentDefinition == null) ? null : currentDefinition.Graph);
						if (!(obj != null))
						{
							goto IL_03f6;
						}
						PunchcardRuntime currentCard2 = requisitionSlot.CurrentCard;
						if ((object)requisitionSlot.CurrentCard != null)
						{
							PunchcardDefinitionV2 currentDefinition2 = currentCard2.CurrentDefinition;
							if ((object)currentCard2.CurrentDefinition != null)
							{
								PunchcardGraph punchcardGraph = UnityEngine.Object.Instantiate(currentDefinition2.Graph);
								_003CnewGraph_003E5__2 = punchcardGraph;
								PunchcardGraph punchcardGraph2 = _003CnewGraph_003E5__2;
								if ((object)_003CnewGraph_003E5__2 != null && punchcardGraph2.nodes != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
									List<Node>.Enumerator enumerator = default(List<Node>.Enumerator);
									object obj2 = default(object);
									while (enumerator.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										if (obj2 != null)
										{
											_ = _003CnewGraph_003E5__2;
											continue;
										}
										throw new NullReferenceException();
									}
									enumerator.Dispose();
									if (!(requisitionSlot.CurrentCardConsole != null))
									{
										goto IL_0344;
									}
									if ((object)requisitionSlot.CurrentCardConsole != null)
									{
										PunchcardVariable[] componentsInChildren = requisitionSlot.CurrentCardConsole.GetComponentsInChildren<PunchcardVariable>();
										if (componentsInChildren != null)
										{
											object obj3 = componentsInChildren + 32;
											object obj4 = 0;
											object obj5 = 0;
											while ((nint)obj5 < componentsInChildren.Length)
											{
												PunchcardVariable punchcardVariable = (PunchcardVariable)obj3;
												if (obj3 != null)
												{
													object obj6 = ((PunchcardVariable)obj3).Get();
													if ((object)_003CnewGraph_003E5__2 != null)
													{
														_003CnewGraph_003E5__2.SetVariable(punchcardVariable.VariableID, obj6);
														obj4++;
														obj3 += 8;
														obj5 = obj4;
														continue;
													}
												}
												goto IL_0446;
											}
											goto IL_0344;
										}
									}
								}
							}
						}
					}
				}
				goto IL_0446;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_03f6;
			}
			_003C_003E1__state = -1;
			goto IL_04b1;
			IL_03f6:
			return false;
			IL_0446:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_04b1:
			PunchcardGraph punchcardGraph3 = _003CnewGraph_003E5__2;
			if ((object)_003CnewGraph_003E5__2 != null)
			{
				if (punchcardGraph3.CurrentState != null)
				{
					_003CnewGraph_003E5__2.Update();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_03f6;
			}
			goto IL_0446;
			IL_0344:
			if ((object)_003CnewGraph_003E5__2 == null)
			{
				goto IL_0446;
			}
			_003CnewGraph_003E5__2.Run();
			goto IL_04b1;
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

	private sealed class _003CCR_SpendRequisitionPoints_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public RequisitionSlot _003C_003E4__this;

		public int cost;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCR_SpendRequisitionPoints_003Ed__45(int _003C_003E1__state)
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
			//IL_0075: Expected I4, but got I8
			//IL_00e7: Expected I4, but got O
			RequisitionSlot requisitionSlot = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null || (object)requisitionSlot.statsTracker == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				bool flag = requisitionSlot.statsTracker.SpendPoints(cost);
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

	public PunchcardRuntime CurrentCard;

	public GameObject CurrentCardConsole;

	public MissionStatsTracker statsTracker;

	public LookAtTarget lever;

	public Animator slotAnimator;

	public Transform Transform_ConsoleAnchor;

	public string hasCardBoolParam;

	public string scanTriggerParam;

	public string requisitionTriggerParam;

	public string requisitionFailTriggerParam;

	public bool autoScanOnInsert;

	public bool clearSlotAfterRedemption;

	public float CardConsoleDestroyDelay;

	public float RedemptionCooldown;

	public float PointsDeductionDelay;

	public float CardConsumedDelay;

	public bool fireFailTriggerOnRedemptionFail;

	public bool fireRequisitionTriggerOnInsufficientPoints;

	public UnityEvent onCardRequisitioned;

	public UnityEvent onCooldownRejection;

	public UnityEvent onRejection;

	public bool debugLogs;

	private ItemSlot itemSlot;

	private bool leverCallbackRegistered;

	private int hasCardBoolHash;

	private int scanTriggerHash;

	private int requisitionTriggerHash;

	private int requisitionFailTriggerHash;

	private float lastRedemptionTime;

	private GameObject pendingConsoleDestroy;

	private Coroutine pendingConsoleDestroyCoroutine;

	public bool HasCard => CurrentCard != null;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		ItemSlot itemSlot = default(ItemSlot);
		this.itemSlot = itemSlot;
		if (this.itemSlot == null)
		{
			Debug.LogError("[RequisitionSlot] No ItemSlot component found on this GameObject. Add one.", this);
		}
		int num = Animator.StringToHash(hasCardBoolParam);
		hasCardBoolHash = num;
		int num2 = Animator.StringToHash(scanTriggerParam);
		scanTriggerHash = num2;
		int num3 = Animator.StringToHash(requisitionTriggerParam);
		requisitionTriggerHash = num3;
		int num4 = Animator.StringToHash(requisitionFailTriggerParam);
		requisitionFailTriggerHash = num4;
		if (statsTracker == null)
		{
			MissionStatsTracker missionStatsTracker = UnityEngine.Object.FindFirstObjectByType<MissionStatsTracker>();
			statsTracker = missionStatsTracker;
		}
	}

	private void OnEnable()
	{
		if (this.itemSlot == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			ItemSlot itemSlot = default(ItemSlot);
			this.itemSlot = itemSlot;
		}
		if (!(this.itemSlot != null))
		{
			Debug.LogError("[RequisitionSlot] OnEnable: ItemSlot still null — events not subscribed.", this);
		}
		else
		{
			ItemSlot itemSlot2 = this.itemSlot;
			UnityAction<GameObject> call = OnItemAdded;
			itemSlot2.onItemAdded.AddListener(call);
			ItemSlot itemSlot3 = this.itemSlot;
			UnityAction<GameObject> call2 = OnItemRemoved;
			itemSlot3.onItemRemoved.AddListener(call2);
		}
		RegisterLeverCallback();
		UpdateVisualAndLeverState();
	}

	private void OnDisable()
	{
		if (this.itemSlot != null)
		{
			ItemSlot itemSlot = this.itemSlot;
			UnityAction<GameObject> call = OnItemAdded;
			itemSlot.onItemAdded.RemoveListener(call);
			ItemSlot itemSlot2 = this.itemSlot;
			UnityAction<GameObject> call2 = OnItemRemoved;
			itemSlot2.onItemRemoved.RemoveListener(call2);
		}
	}

	private void Start()
	{
		RegisterLeverCallback();
		UpdateVisualAndLeverState();
		if (debugLogs)
		{
			string message = "[RequisitionSlot] Start. Animator params -> Has:'" + hasCardBoolParam + "' Scan:'" + scanTriggerParam + "' Req:'" + requisitionTriggerParam + "' Fail:'" + requisitionFailTriggerParam + "'";
			Debug.Log(message, this);
		}
	}

	private void OnItemAdded(GameObject itemGO)
	{
		//IL_019d: Expected O, but got I
		//IL_01ca: Expected O, but got I
		//IL_022e: Expected O, but got I
		if (debugLogs)
		{
			string text = itemGO.name;
			string message = "[RequisitionSlot] OnItemAdded fired for '" + text + "'.";
			Debug.Log(message, this);
		}
		if (pendingConsoleDestroyCoroutine != null)
		{
			StopCoroutine(pendingConsoleDestroyCoroutine);
			pendingConsoleDestroyCoroutine = null;
		}
		if (pendingConsoleDestroy != null)
		{
			if (debugLogs)
			{
				Debug.Log("[RequisitionSlot] Immediately destroying pending outgoing console.", this);
			}
			UnityEngine.Object.Destroy(pendingConsoleDestroy);
			pendingConsoleDestroy = null;
		}
		if (CurrentCardConsole != null)
		{
			if (debugLogs)
			{
				Debug.Log("[RequisitionSlot] Immediately destroying lingering CurrentCardConsole.", this);
			}
			UnityEngine.Object.Destroy(CurrentCardConsole);
			CurrentCardConsole = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_8_v2 (UnityEngine.Object)+20]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_8_v2 (UnityEngine.Object)+20]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rax_v64+61]");
				if ((nint)0 != 0)
				{
					if (debugLogs)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rax_v64+18]");
						string message2 = "[RequisitionSlot] AutoEject card '" + (string)0 + "' — ejecting.";
						Debug.Log(message2, this);
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					if (obj != null)
					{
						itemSlot.RemoveItem((DraggableItem)obj, autoEject: true);
					}
					return;
				}
			}
			CurrentCard = (PunchcardRuntime)obj;
			PunchcardRuntime currentCard = CurrentCard;
			if (currentCard.CurrentDefinition != null)
			{
				PunchcardRuntime currentCard2 = CurrentCard;
				PunchcardDefinitionV2 currentDefinition = currentCard2.CurrentDefinition;
				if (currentDefinition.Prefab_ConsoleControls != null)
				{
					PunchcardRuntime currentCard3 = CurrentCard;
					PunchcardDefinitionV2 currentDefinition2 = currentCard3.CurrentDefinition;
					GameObject currentCardConsole = UnityEngine.Object.Instantiate(currentDefinition2.Prefab_ConsoleControls, Transform_ConsoleAnchor);
					CurrentCardConsole = currentCardConsole;
					if (debugLogs)
					{
						PunchcardRuntime currentCard4 = CurrentCard;
						string text2 = currentCard4.CurrentDefinition.name;
						string message3 = "[RequisitionSlot] Console instantiated for '" + text2 + "'.";
						Debug.Log(message3, this);
					}
					goto IL_044b;
				}
			}
			if (debugLogs)
			{
				string text3 = obj.name;
				string message4 = "[RequisitionSlot] Card '" + text3 + "' has no Prefab_ConsoleControls assigned on its definition.";
				Debug.LogWarning(message4, this);
			}
			goto IL_044b;
		}
		if (debugLogs)
		{
			Debug.LogWarning("[RequisitionSlot] Dropped item has no PunchcardRuntime — ignoring.", this);
		}
		return;
		IL_044b:
		UpdateVisualAndLeverState();
		if (autoScanOnInsert && (bool)slotAnimator && !string.IsNullOrEmpty(scanTriggerParam))
		{
			slotAnimator.ResetTrigger(scanTriggerHash);
			slotAnimator.SetTrigger(scanTriggerHash);
			if (debugLogs)
			{
				Debug.Log("[RequisitionSlot] Scan trigger fired.", this);
			}
		}
	}

	private void OnItemRemoved(GameObject itemGO)
	{
		if (debugLogs)
		{
			string text = itemGO.name;
			string message = "[RequisitionSlot] OnItemRemoved fired for '" + text + "'.";
			Debug.Log(message, this);
		}
		CurrentCard = null;
		if (CurrentCardConsole != null)
		{
			pendingConsoleDestroy = CurrentCardConsole;
			CurrentCardConsole = null;
			if (pendingConsoleDestroyCoroutine != null)
			{
				StopCoroutine(pendingConsoleDestroyCoroutine);
			}
			_003CCR_DestroyConsoleAfterDelay_003Ed__40 obj = new _003CCR_DestroyConsoleAfterDelay_003Ed__40(0);
			obj._003C_003E4__this = this;
			obj.delay = CardConsoleDestroyDelay;
			Coroutine coroutine = StartCoroutine(obj);
			pendingConsoleDestroyCoroutine = coroutine;
		}
		UpdateVisualAndLeverState();
	}

	private void ImmediatelyDestroyPendingConsole()
	{
		if (pendingConsoleDestroyCoroutine != null)
		{
			StopCoroutine(pendingConsoleDestroyCoroutine);
			pendingConsoleDestroyCoroutine = null;
		}
		if (pendingConsoleDestroy != null)
		{
			if (debugLogs)
			{
				Debug.Log("[RequisitionSlot] Immediately destroying pending outgoing console.", this);
			}
			UnityEngine.Object.Destroy(pendingConsoleDestroy);
			pendingConsoleDestroy = null;
		}
		if (CurrentCardConsole != null)
		{
			if (debugLogs)
			{
				Debug.Log("[RequisitionSlot] Immediately destroying lingering CurrentCardConsole.", this);
			}
			UnityEngine.Object.Destroy(CurrentCardConsole);
			CurrentCardConsole = null;
		}
	}

	private IEnumerator CR_DestroyConsoleAfterDelay(float delay)
	{
		_003CCR_DestroyConsoleAfterDelay_003Ed__40 obj = new _003CCR_DestroyConsoleAfterDelay_003Ed__40(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.delay = delay;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	public void PlaceCard(PunchcardRuntime card)
	{
		if ((bool)card)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (!(obj == null))
			{
				itemSlot.PlaceItem((DraggableItem)obj);
				return;
			}
			string text = card.name;
			string message = "[RequisitionSlot] PlaceCard: '" + text + "' has no DraggableItem component.";
			Debug.LogWarning(message, this);
		}
	}

	public void RemoveCard(PunchcardRuntime card, bool autoEject = false)
	{
		if ((bool)card)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (obj != null)
			{
				itemSlot.RemoveItem((DraggableItem)obj, autoEject);
			}
		}
	}

	public void ClearSlot()
	{
		itemSlot.ClearSlot();
	}

	public void AttemptRequisition()
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00fe: Expected O, but got I8
		//IL_0190: Expected O, but got I4
		//IL_0199: Expected O, but got I4
		//IL_01a2: Expected O, but got I4
		//IL_01b7: Expected O, but got I
		//IL_0aa1: Expected O, but got I
		//IL_01d1: Expected O, but got I
		//IL_01f7: Expected O, but got I
		//IL_012f: Expected O, but got I8
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//IL_0237: Expected O, but got I
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Expected O, but got Unknown
		//IL_0260: Expected O, but got I4
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Expected O, but got Unknown
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		//IL_0647: Expected F8, but got I4
		//IL_0672: Expected O, but got I
		//IL_069f: Expected O, but got I
		//IL_06ef: Expected O, but got I
		if ((bool)CurrentCard)
		{
			if ((bool)statsTracker)
			{
				float num = lastRedemptionTime + RedemptionCooldown;
				float time = Time.time;
				if (!(num > time))
				{
					Dictionary<string, object> variables = new Dictionary<string, object>();
					if (CurrentCardConsole != null)
					{
						PunchcardVariable[] componentsInChildren = CurrentCardConsole.GetComponentsInChildren<PunchcardVariable>();
						object obj = componentsInChildren + 32;
						object obj2 = 6442450944L;
						if (0 < componentsInChildren.Length)
						{
							object obj3 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rbx_v38+28]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rbx_v38+28]");
							if ((nint)0 > (nint)5)
							{
								PunchcardVariable[] componentsInChildren2 = ((GameObject)(object)typeof(NotImplementedException)).GetComponentsInChildren<PunchcardVariable>();
								NotImplementedException ex = new NotImplementedException();
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
								throw ex;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r13_v9+45D750+v273 @ rax_v139*4]");
							object obj5 = 0 + 6442450944L;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v279 @ rcx_v117 (should have been resolved before IL gen)");
						}
					}
					PunchcardRuntime currentCard = CurrentCard;
					PunchcardDefinitionV2 currentDefinition = currentCard.CurrentDefinition;
					RequirementSet requirements = currentDefinition.Requirements;
					RequirementSet.RequirementPair[] requirements2 = requirements.requirements;
					bool flag = true;
					object obj6 = 32;
					object obj7 = 0;
					object obj8 = 0;
					object value = default(object);
					object value2 = default(object);
					object arg = default(object);
					object arg2 = default(object);
					while (true)
					{
						if ((nint)obj8 < requirements2.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rsi_v9+v876 @ rax_v34 (RequirementPair[])]");
							object obj9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ rax_v127+18]");
							bool flag2 = ((Requirement)0).Execute(variables);
							RequirementSet.RequirementPair[] requirements3 = requirements.requirements;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rsi_v9+v562 @ rax_v129 (RequirementPair[])]");
							object obj10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v563 @ rax_v130+10]");
							bool flag3 = (nint)0 == 0;
							if (flag3)
							{
								requirements2 = requirements.requirements;
								obj7++;
								obj6 += 8;
								flag = flag2;
								obj8 = obj7;
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v563 @ rax_v130+10]");
							object obj11 = -1;
							if (!flag3)
							{
								bool flag4 = (nint)obj11 != 1;
								object obj12 = 0;
								bool flag5 = flag2;
								if (!flag4)
								{
									requirements2 = requirements.requirements;
									flag |= flag2;
									obj7++;
									obj6 += 8;
									obj8 = obj7;
									continue;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
								NotImplementedException ex2 = new NotImplementedException();
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
								throw ex2;
							}
							if (flag || flag2)
							{
								requirements2 = requirements.requirements;
								flag &= flag2;
								obj7++;
								obj6 += 8;
								obj8 = obj7;
								continue;
							}
						}
						else if (flag)
						{
							PunchcardRuntime currentCard2 = CurrentCard;
							int num2;
							if (currentCard2.CurrentDefinition != null)
							{
								PunchcardRuntime currentCard3 = CurrentCard;
								PunchcardDefinitionV2 currentDefinition2 = currentCard3.CurrentDefinition;
								num2 = currentDefinition2.Cost;
							}
							else
							{
								num2 = 1;
							}
							int requisitionPoints = statsTracker.RequisitionPoints;
							if (requisitionPoints >= num2)
							{
								float time2 = Time.time;
								lastRedemptionTime = time2;
								_003CCR_SpendRequisitionPoints_003Ed__45 obj13 = new _003CCR_SpendRequisitionPoints_003Ed__45(0);
								obj13._003C_003E4__this = this;
								obj13.delay = PointsDeductionDelay;
								obj13.cost = num2;
								Coroutine coroutine = StartCoroutine(obj13);
								FireRequisitionTrigger(success: true);
								_003CCR_RunPunchcardGraph_003Ed__46 obj14 = new _003CCR_RunPunchcardGraph_003Ed__46(0);
								obj14._003C_003E4__this = this;
								Coroutine coroutine2 = StartCoroutine(obj14);
								MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
								if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
								{
									MissionManager.MissionState currentMissionState = missionManager.CurrentMissionState;
									if (missionManager.CurrentMissionState != null)
									{
										MedalTrackedValues trackingValues = currentMissionState.TrackingValues;
										if (currentMissionState.TrackingValues != null && trackingValues.Data_PunchcardsUsed != null)
										{
											MissionManager missionManager2 = MissionManager._003CInstance_003Ek__BackingField;
											MissionManager.MissionState currentMissionState2 = missionManager2.CurrentMissionState;
											MedalTrackedValues.Data_PunchcardUsed data_PunchcardUsed = new MedalTrackedValues.Data_PunchcardUsed();
											PunchcardRuntime currentCard4 = CurrentCard;
											data_PunchcardUsed.Punchcard = currentCard4.CurrentDefinition;
											time2 = Time.time;
											data_PunchcardUsed.UsedAtTime = time2;
											currentMissionState2.TrackingValues.TrackPunchcard(data_PunchcardUsed);
											missionManager = null;
										}
									}
								}
								Dictionary<string, object> dictionary = new Dictionary<string, object>();
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								dictionary.Add("RQAvaliable", value);
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								dictionary.Add("RQSpent", value2);
								PunchcardRuntime currentCard5 = CurrentCard;
								PunchcardDefinitionV2 currentDefinition3 = currentCard5.CurrentDefinition;
								dictionary.Add("Card", currentDefinition3.ID);
								AnalyticsManager.Analytics_Generic("PunchcardUsed", num2, dictionary);
								Component currentCard6 = CurrentCard;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1061 @ rbx_v28 (UnityEngine.Component)+20]");
								if ((UnityEngine.Object)0 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1061 @ rbx_v28 (UnityEngine.Component)+20]");
									object obj15 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1070 @ rax_v95+24]");
									if ((nint)0 > (nint)0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1070 @ rax_v95+64]");
										_ = -1;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1061 @ rbx_v28 (UnityEngine.Component)+20]");
										object obj16 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1071 @ rax_v96+64]");
										if ((nint)0 <= (nint)0)
										{
											GameObject obj17 = currentCard6.gameObject;
											UnityEngine.Object.Destroy(obj17, CardConsumedDelay);
											if (CurrentCardConsole != null)
											{
												UnityEngine.Object.Destroy(CurrentCardConsole, CardConsumedDelay);
												CurrentCardConsole = null;
											}
											itemSlot.ClearSlot();
										}
									}
								}
								if (onCardRequisitioned != null)
								{
									onCardRequisitioned.Invoke();
								}
								if (clearSlotAfterRedemption)
								{
									itemSlot.ClearSlot();
								}
								break;
							}
							if (debugLogs)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								string message = $"[RequisitionSlot] Not enough points (Have {arg}, Need {arg2}).";
								Debug.Log(message, this);
							}
							if (fireRequisitionTriggerOnInsufficientPoints)
							{
								FireRequisitionTrigger(success: false);
							}
							goto IL_0976;
						}
						FireRequisitionTrigger(success: false);
						if (fireFailTriggerOnRedemptionFail && (bool)slotAnimator)
						{
							bool flag6 = string.IsNullOrEmpty(requisitionFailTriggerParam);
							if (!flag6 && fireFailTriggerOnRedemptionFail != flag6)
							{
								slotAnimator.ResetTrigger(requisitionFailTriggerHash);
								slotAnimator.SetTrigger(requisitionFailTriggerHash);
								if (debugLogs)
								{
									Debug.Log("[RequisitionSlot] Fail trigger fired.", this);
								}
							}
						}
						goto IL_0976;
						IL_0976:
						if (onRejection != null)
						{
							onRejection.Invoke();
						}
						break;
					}
				}
				else if (onCooldownRejection != null)
				{
					onCooldownRejection.Invoke();
				}
			}
			else
			{
				Debug.LogWarning("[RequisitionSlot] AttemptRequisition: No MissionStatsTracker reference.", this);
			}
		}
		else if (debugLogs)
		{
			Debug.Log("[RequisitionSlot] AttemptRequisition: No card.", this);
		}
	}

	private IEnumerator CR_SpendRequisitionPoints(float delay, int cost)
	{
		_003CCR_SpendRequisitionPoints_003Ed__45 obj = new _003CCR_SpendRequisitionPoints_003Ed__45(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.delay = delay;
			obj.cost = cost;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private IEnumerator CR_RunPunchcardGraph()
	{
		_003CCR_RunPunchcardGraph_003Ed__46 obj = new _003CCR_RunPunchcardGraph_003Ed__46(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private void FireScan()
	{
		if ((bool)slotAnimator && !string.IsNullOrEmpty(scanTriggerParam))
		{
			slotAnimator.ResetTrigger(scanTriggerHash);
			slotAnimator.SetTrigger(scanTriggerHash);
			if (debugLogs)
			{
				Debug.Log("[RequisitionSlot] Scan trigger fired.", this);
			}
		}
	}

	private void FireRequisitionTrigger(bool success)
	{
		if ((bool)slotAnimator && !string.IsNullOrEmpty(requisitionTriggerParam))
		{
			slotAnimator.ResetTrigger(requisitionTriggerHash);
			slotAnimator.SetTrigger(requisitionTriggerHash);
			if (debugLogs)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[RequisitionSlot] Requisition trigger fired (success={arg}).";
				Debug.Log(message, this);
			}
		}
	}

	private void FireFailAnimation()
	{
		if (!slotAnimator)
		{
			return;
		}
		bool flag = string.IsNullOrEmpty(requisitionFailTriggerParam);
		if (!flag && fireFailTriggerOnRedemptionFail != flag)
		{
			slotAnimator.ResetTrigger(requisitionFailTriggerHash);
			slotAnimator.SetTrigger(requisitionFailTriggerHash);
			if (debugLogs)
			{
				Debug.Log("[RequisitionSlot] Fail trigger fired.", this);
			}
		}
	}

	private void UpdateVisualAndLeverState()
	{
		if ((bool)slotAnimator && !string.IsNullOrEmpty(hasCardBoolParam))
		{
			bool value = CurrentCard != null;
			slotAnimator.SetBool(hasCardBoolHash, value);
		}
		if ((bool)lever)
		{
			bool active = CurrentCard != null;
			lever.SetActive(active);
		}
		if (debugLogs)
		{
			bool flag = CurrentCard != null;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[RequisitionSlot] State updated: HasCard={arg}";
			Debug.Log(message, this);
		}
	}

	private void RegisterLeverCallback()
	{
		if ((bool)lever && !leverCallbackRegistered)
		{
			UnityAction action = AttemptRequisition;
			lever.RegisterOnClickDown(action);
			bool flag = !debugLogs;
			leverCallbackRegistered = true;
			if (!flag)
			{
				Debug.Log("[RequisitionSlot] Lever callback registered.", this);
			}
		}
	}

	private void CacheAnimatorHashes()
	{
		int num = Animator.StringToHash(hasCardBoolParam);
		hasCardBoolHash = num;
		int num2 = Animator.StringToHash(scanTriggerParam);
		scanTriggerHash = num2;
		int num3 = Animator.StringToHash(requisitionTriggerParam);
		requisitionTriggerHash = num3;
		int num4 = Animator.StringToHash(requisitionFailTriggerParam);
		requisitionFailTriggerHash = num4;
	}

	public RequisitionSlot()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A372]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		hasCardBoolParam = "HasCard";
		scanTriggerParam = "Scan";
		requisitionTriggerParam = "Requisition";
		requisitionFailTriggerParam = "RequisitionFail";
		autoScanOnInsert = true;
		CardConsoleDestroyDelay = 2f;
		RedemptionCooldown = 2f;
		PointsDeductionDelay = 2f;
		CardConsumedDelay = 2f;
		fireFailTriggerOnRedemptionFail = true;
		base._002Ector();
	}
}
