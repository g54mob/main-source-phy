using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class ImpactVisualCorrections : MonoBehaviour
{
	public enum TargetSelectionMode
	{
		NearestActiveTarget,
		SpecificID
	}

	public enum RotationAxis
	{
		LocalX,
		LocalY,
		LocalZ
	}

	public enum PointerAxis
	{
		LocalUp,
		LocalRight,
		LocalForward,
		LocalDown,
		LocalLeft,
		LocalBack
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<EntityLocation, bool> _003C_003E9__55_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CAnyTargetOrEnemyInsideRadius_003Eb__55_0(EntityLocation x)
		{
			//IL_00ef: Expected I4, but got O
			//IL_00a6: Expected O, but got I4
			if ((object)x != null && x.Entity != null)
			{
				bool isAlive = x.Entity.IsAlive;
				if (!isAlive)
				{
					return isAlive;
				}
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					object obj = entity.Role & EntityRoles.Target;
					if (obj != null)
					{
						return true;
					}
					return (byte)(entity.Role & EntityRoles.Enemy) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003CDeferredInitialEvaluation_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImpactVisualCorrections _003C_003E4__this;

		private int _003Ci_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDeferredInitialEvaluation_003Ed__43(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_00f6: Expected I4, but got I8
			//IL_00e7: Expected I4, but got O
			ImpactVisualCorrections impactVisualCorrections = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003Ci_003E5__2 = 0;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0115;
				}
				int num = _003Ci_003E5__2 + 1;
				_003Ci_003E5__2 = num;
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null)
			{
				if (_003Ci_003E5__2 < impactVisualCorrections.evaluationFrameDelay)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E4__this.PerformEvaluation();
				impactVisualCorrections._initialEvaluated = true;
				goto IL_0115;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0115:
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

	public ImpactLocation impactLocation;

	public bool suppressWhenScoutingStripsBlocked;

	public Transform missionParentOverride;

	public GameObject arrowRoot;

	public TMP_Text rangeText;

	public string rangeFormat;

	public bool liveUpdate;

	public float liveUpdateInterval;

	public TargetSelectionMode targetSelection;

	public string specificTargetID;

	public int evaluationFrameDelay;

	public bool autoResolveReferences;

	public RotationAxis rotationAxis;

	public PointerAxis pointerAxis;

	public bool preserveInitialRotation;

	public bool useMinDirectionMagnitude;

	public float minDirectionSqrMagnitude;

	public bool attemptAutoLocateTierController;

	public bool listenForTierChanges;

	public ImpactCorrectionTierController explicitTierController;

	public bool debugLogs;

	private Transform _missionParent;

	private float _nextUpdateTime;

	private bool _initialEvaluated;

	private bool _isHit;

	private bool _purgedSuppressedUIElements;

	private Vector2 _impactLocalPos;

	private EntityLocation _currentTarget;

	private Vector2 _currentTargetLocalPos;

	private Quaternion _initialArrowLocalRotation;

	private float _directionErrorOffsetDeg;

	private EntityLocation _lastTargetRef;

	private bool _errorOffsetValid;

	private bool _subscribedTierEvents;

	private bool _pendingTierRetry;

	private float _tierRetryTime;

	private const float TierLookupRetryDelay = 0.5f;

	private ImpactCorrectionTierController ActiveTierController
	{
		get
		{
			if ((bool)explicitTierController)
			{
				return explicitTierController;
			}
			return ImpactCorrectionTierController._003CInstance_003Ek__BackingField;
		}
	}

	private void Awake()
	{
		if (autoResolveReferences)
		{
			if (!this.impactLocation)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				ImpactLocation impactLocation = default(ImpactLocation);
				this.impactLocation = impactLocation;
			}
			if (!missionParentOverride)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("MissionParent");
				if ((bool)gameObject)
				{
					Transform transform = gameObject.transform;
					missionParentOverride = transform;
				}
			}
		}
		_missionParent = missionParentOverride;
	}

	private void OnEnable()
	{
		//IL_005d: Expected O, but got F4
		_initialEvaluated = false;
		_purgedSuppressedUIElements = false;
		if ((bool)arrowRoot)
		{
			Transform transform = arrowRoot.transform;
			_initialArrowLocalRotation = (Quaternion)transform.localRotation.x;
		}
		AttemptTierSubscription(initial: true);
		_003CDeferredInitialEvaluation_003Ed__43 obj = new _003CDeferredInitialEvaluation_003Ed__43(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private void OnDisable()
	{
		//IL_006a: Expected I, but got O
		//IL_0080: Expected O, but got I
		if (!_subscribedTierEvents)
		{
			return;
		}
		Action value = HandleTiersChanged;
		Delegate obj = ImpactCorrectionTierController.OnActiveTiersChanged;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj2 = Delegate.Remove(obj, value);
			bool flag = (object)obj2 == null;
			Delegate obj3 = null;
			if (!flag)
			{
				bool flag2 = (object)obj2.GetType() != typeof(Action);
				obj3 = null;
				if (!flag2)
				{
					obj3 = obj2;
				}
				if ((object)obj3 == null)
				{
					break;
				}
			}
			nint num = (nint)typeof(ImpactCorrectionTierController);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rax_v9 (Il2CppClass<ImpactCorrectionTierController>)+B8]");
			object obj4 = (nint)0 + (nint)8;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj;
			obj = obj5;
			if (!flag3)
			{
				_subscribedTierEvents = false;
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private IEnumerator DeferredInitialEvaluation()
	{
		_003CDeferredInitialEvaluation_003Ed__43 obj = new _003CDeferredInitialEvaluation_003Ed__43(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void Update()
	{
		//IL_00cc: Invalid comparison between I4 and F4
		if (!_initialEvaluated || _isHit)
		{
			return;
		}
		if (_pendingTierRetry)
		{
			float time = Time.time;
			if (!(time < _tierRetryTime))
			{
				_pendingTierRetry = false;
				AttemptTierSubscription();
			}
		}
		if (!liveUpdate)
		{
			return;
		}
		if (0f < liveUpdateInterval)
		{
			float time2 = Time.time;
			if (time2 < _nextUpdateTime)
			{
				return;
			}
		}
		float time3 = Time.time;
		float nextUpdateTime = time3 + liveUpdateInterval;
		_nextUpdateTime = nextUpdateTime;
		UpdateMissVisuals(liveUpdateTrigger: true);
	}

	private void AttemptTierSubscription(bool initial = false)
	{
		//IL_010d: Expected I, but got O
		//IL_0123: Expected O, but got I
		if (!attemptAutoLocateTierController && !explicitTierController)
		{
			return;
		}
		ImpactCorrectionTierController activeTierController = ActiveTierController;
		if ((bool)activeTierController)
		{
			if (listenForTierChanges && !_subscribedTierEvents)
			{
				Action b = HandleTiersChanged;
				Delegate obj = ImpactCorrectionTierController.OnActiveTiersChanged;
				bool flag3;
				Delegate obj5 = default(Delegate);
				do
				{
					Delegate obj2 = Delegate.Combine(obj, b);
					bool flag = (object)obj2 == null;
					Delegate obj3 = null;
					if (!flag)
					{
						bool flag2 = (object)obj2.GetType() != typeof(Action);
						obj3 = null;
						if (!flag2)
						{
							obj3 = obj2;
						}
						if ((object)obj3 == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							return;
						}
					}
					nint num = (nint)typeof(ImpactCorrectionTierController);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v442 @ rax_v21 (Il2CppClass<ImpactCorrectionTierController>)+B8]");
					object obj4 = (nint)0 + (nint)8;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					flag3 = (object)obj5 != obj;
					obj = obj5;
				}
				while (flag3);
				bool flag4 = !debugLogs;
				_subscribedTierEvents = true;
				if (!flag4)
				{
					Debug.Log("[ImpactVisualCorrections] Subscribed to tier change events.", this);
				}
			}
			if (_initialEvaluated && !_isHit)
			{
				_errorOffsetValid = false;
				UpdateMissVisuals();
			}
		}
		else
		{
			_pendingTierRetry = true;
			float time = Time.time;
			bool flag5 = !debugLogs;
			float tierRetryTime = time + 0.5f;
			_tierRetryTime = tierRetryTime;
			if (!flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[ImpactVisualCorrections] Tier controller not found. Will retry in {arg:0.00}s.";
				Debug.Log(message, this);
			}
		}
	}

	private void UnsubscribeTierEvents()
	{
		//IL_006a: Expected I, but got O
		//IL_0080: Expected O, but got I
		if (!_subscribedTierEvents)
		{
			return;
		}
		Action value = HandleTiersChanged;
		Delegate obj = ImpactCorrectionTierController.OnActiveTiersChanged;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj2 = Delegate.Remove(obj, value);
			bool flag = (object)obj2 == null;
			Delegate obj3 = null;
			if (!flag)
			{
				bool flag2 = (object)obj2.GetType() != typeof(Action);
				obj3 = null;
				if (!flag2)
				{
					obj3 = obj2;
				}
				if ((object)obj3 == null)
				{
					break;
				}
			}
			nint num = (nint)typeof(ImpactCorrectionTierController);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rax_v9 (Il2CppClass<ImpactCorrectionTierController>)+B8]");
			object obj4 = (nint)0 + (nint)8;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj;
			obj = obj5;
			if (!flag3)
			{
				_subscribedTierEvents = false;
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void HandleTiersChanged()
	{
		if (debugLogs)
		{
			Debug.Log("[ImpactVisualCorrections] Tiers changed -> refreshing visuals.", this);
		}
		bool flag = !_initialEvaluated;
		_errorOffsetValid = false;
		if (!flag && !_isHit)
		{
			UpdateMissVisuals(liveUpdateTrigger: true);
		}
	}

	private void ResetDirectionError()
	{
		_errorOffsetValid = false;
	}

	private unsafe void PerformEvaluation()
	{
		//IL_00be: Expected O, but got Ref
		//IL_00d1: Expected O, but got F4
		//IL_0132: Invalid comparison between F4 and I4
		if (suppressWhenScoutingStripsBlocked && (bool)this.impactLocation)
		{
			ImpactLocation impactLocation = this.impactLocation;
			if (impactLocation._003CScoutingStripsBlocked_003Ek__BackingField)
			{
				goto IL_017f;
			}
		}
		bool flag = _missionParent != null;
		ImpactVisualCorrections impactVisualCorrections = this;
		if (!flag)
		{
			goto IL_0196;
		}
		Transform transform = base.transform;
		Vector3 position = transform.position;
		object obj = default(object);
		_impactLocalPos = (Vector2)_missionParent.InverseTransformPoint((Vector3)(&obj)).x;
		bool flag2;
		if ((bool)this.impactLocation)
		{
			ImpactLocation impactLocation2 = this.impactLocation;
			ShellDefinition shell = impactLocation2.shell;
			if (shell.ImpactRadius > 0f)
			{
				Vector2 impactLocalPos = default(Vector2);
				flag2 = AnyTargetOrEnemyInsideRadius(impactLocalPos, shell.ImpactRadius);
				goto IL_01d7;
			}
		}
		flag2 = false;
		goto IL_01d7;
		IL_017f:
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 277 Invalid \"Jump target not found in method: 0x1805752C0\"");
		ImpactVisualCorrections impactVisualCorrections2 = default(ImpactVisualCorrections);
		impactVisualCorrections = impactVisualCorrections2;
		goto IL_0196;
		IL_01d7:
		_isHit = flag2;
		if (!flag2)
		{
			UpdateMissVisuals();
			return;
		}
		goto IL_017f;
		IL_0196:
		Debug.LogWarning("[ImpactVisualCorrections] No MissionParent found. Disabling visuals.", impactVisualCorrections);
		impactVisualCorrections.DisableAll();
	}

	private unsafe void UpdateMissVisuals(bool liveUpdateTrigger = false)
	{
		//IL_0013: Expected O, but got Ref
		//IL_011e: Expected O, but got Ref
		//IL_00ff: Expected O, but got Ref
		object obj = default(object);
		EntityLocation currentTarget;
		if (targetSelection != TargetSelectionMode.SpecificID)
		{
			(EntityLocation, float, float) nearestTargetOrEnemy = ImpactTracker.GetNearestTargetOrEnemy((Vector2)(&obj));
			currentTarget = (EntityLocation)nearestTargetOrEnemy;
		}
		else
		{
			bool flag = ImpactTracker.EntityLocations.TryGetValue(specificTargetID, out var value);
			bool flag2 = !flag;
			currentTarget = null;
			if (!flag2)
			{
				currentTarget = value;
			}
		}
		_currentTarget = currentTarget;
		if (!(_currentTarget != null))
		{
			goto IL_020f;
		}
		bool targetChanged = _lastTargetRef != _currentTarget;
		_lastTargetRef = _currentTarget;
		Vector2 localPosition = _currentTarget.LocalPosition;
		bool flag3 = targetSelection == TargetSelectionMode.NearestActiveTarget;
		_currentTargetLocalPos = localPosition;
		float missDistance;
		float num = default(float);
		if (!flag3)
		{
			Vector2 vector = default(Vector2);
			bool flag4 = ((Dictionary<string, EntityLocation>)(&vector)).TryGetValue(null, out *(EntityLocation*)null);
			missDistance = num;
		}
		else
		{
			(EntityLocation, float, float) nearestTargetOrEnemy2 = ImpactTracker.GetNearestTargetOrEnemy((Vector2)(&obj));
			if (!((UnityEngine.Object)nearestTargetOrEnemy2 != null))
			{
				goto IL_020f;
			}
			_currentTarget = (EntityLocation)nearestTargetOrEnemy2;
			Vector2 localPosition2 = _currentTarget.LocalPosition;
			_currentTargetLocalPos = localPosition2;
			missDistance = num;
		}
		ApplyArrowRotationWithTiers(missDistance, targetChanged, liveUpdateTrigger);
		ApplyDistanceDisplay(missDistance);
		if ((bool)arrowRoot)
		{
			arrowRoot.SetActive(value: true);
		}
		if ((bool)rangeText)
		{
			GameObject gameObject = rangeText.gameObject;
			gameObject.SetActive(value: true);
		}
		return;
		IL_020f:
		DisableAll();
	}

	private void SuppressCorrectionsOnHit()
	{
		if (!_purgedSuppressedUIElements)
		{
			if ((bool)arrowRoot)
			{
				arrowRoot = null;
				UnityEngine.Object.Destroy(arrowRoot);
			}
			if ((bool)rangeText)
			{
				GameObject obj = rangeText.gameObject;
				rangeText = null;
				UnityEngine.Object.Destroy(obj);
			}
			_purgedSuppressedUIElements = true;
		}
	}

	private void DisableAll()
	{
		if ((bool)arrowRoot)
		{
			arrowRoot.SetActive(value: false);
		}
		if ((bool)rangeText)
		{
			GameObject gameObject = rangeText.gameObject;
			gameObject.SetActive(value: false);
		}
	}

	private unsafe bool AnyTargetOrEnemyInsideRadius(Vector2 impactLocalPos, float radius)
	{
		//IL_005d: Expected O, but got Ref
		//IL_0310: Expected I, but got O
		//IL_00b8: Expected I, but got O
		//IL_0143: Expected O, but got I4
		//IL_00f0: Expected O, but got I
		//IL_00f9: Expected O, but got I4
		//IL_01a6: Invalid comparison between F4 and O
		//IL_01ba: Expected I, but got O
		//IL_0225: Expected O, but got I
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Expected O, but got Unknown
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		float num = radius * radius;
		if (ImpactTracker.EntityLocations != null)
		{
			Dictionary<string, EntityLocation>.ValueCollection values = ImpactTracker.EntityLocations.Values;
			Func<EntityLocation, bool> predicate = _003C_003Ec._003C_003E9__55_0;
			if (_003C_003Ec._003C_003E9__55_0 == null)
			{
				Func<EntityLocation, bool> func = (_003C_003Ec._003C_003E9__55_0 = delegate(EntityLocation x)
				{
					//IL_00ef: Expected I4, but got O
					//IL_00a6: Expected O, but got I4
					if ((object)x != null && x.Entity != null)
					{
						bool isAlive = x.Entity.IsAlive;
						if (!isAlive)
						{
							return isAlive;
						}
						MapEntity entity = x.Entity;
						if (x.Entity != null)
						{
							object obj19 = entity.Role & EntityRoles.Target;
							if (obj19 != null)
							{
								return true;
							}
							return (byte)(entity.Role & EntityRoles.Enemy) != 0;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
				nint num2 = unchecked((nint)null);
				predicate = func;
			}
			IEnumerable<EntityLocation> enumerable = Enumerable.Where(values, predicate);
			if (enumerable != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Dictionary<string, EntityLocation> dictionary = default(Dictionary<string, EntityLocation>);
				object obj = (object)(&dictionary);
				Dictionary<string, EntityLocation> dictionary2 = null;
				object obj2 = default(object);
				object obj11 = default(object);
				EntityLocation entityLocation = default(EntityLocation);
				object obj14 = default(object);
				object obj15 = default(object);
				while (true)
				{
					object obj3;
					object obj10;
					if (dictionary != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj2 != null)
						{
							bool flag = dictionary == null;
							dictionary2 = null;
							if (!flag)
							{
								nint num3 = (nint)dictionary;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ r10_v6 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, EntityLocation>>)+12E]");
								if ((nint)0 >= (nint)0)
								{
									goto IL_0130;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ r10_v6 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, EntityLocation>>)+B0]");
								obj3 = 0;
								object obj4 = 0;
								while (true)
								{
									object obj5 = obj4 + obj4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r8_v13+v498 @ rax_v37*8]");
									if (0 == (nint)typeof(IEnumerator<EntityLocation>))
									{
										break;
									}
									obj4++;
									object obj6 = obj4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ r10_v6 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, EntityLocation>>)+12E]");
									if ((nint)obj6 < 0)
									{
										continue;
									}
									goto IL_0130;
								}
								object obj7 = obj4 + obj4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r8_v13+8+v571 @ rcx_v28*8]");
								object obj8 = (nint)0 << 4;
								object obj9 = obj8 + 312;
								obj10 = obj9 + num3;
								goto IL_037c;
							}
							throw new NullReferenceException();
						}
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						return false;
					}
					throw new NullReferenceException();
					IL_0130:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj3 = 0;
					obj10 = obj11;
					goto IL_037c;
					IL_037c:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v576 @ rdx_v15] (should have been resolved before IL gen)");
					if ((object)entityLocation == null)
					{
						break;
					}
					Vector2 localPosition = entityLocation.LocalPosition;
					object obj12 = localPosition - impactLocalPos;
					object obj13 = obj14 - obj15;
					object obj16 = obj13 * obj13;
					object obj17 = obj12 * obj12;
					object obj18 = obj17 + obj16;
					bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18);
					nint num2 = (nint)typeof(IEnumerator<EntityLocation>);
					if (!flag2)
					{
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						return true;
					}
				}
				throw new NullReferenceException();
			}
		}
		throw new NullReferenceException();
	}

	private unsafe EntityLocation SelectTargetData(Vector2 fromLocal)
	{
		//IL_0013: Expected O, but got Ref
		object obj = default(object);
		if (targetSelection != TargetSelectionMode.SpecificID)
		{
			return (EntityLocation)ImpactTracker.GetNearestTargetOrEnemy((Vector2)(&obj));
		}
		if (ImpactTracker.EntityLocations != null)
		{
			bool flag = ImpactTracker.EntityLocations.TryGetValue(specificTargetID, out var value);
			bool flag2 = !flag;
			EntityLocation result = null;
			if (!flag2)
			{
				result = value;
			}
			return result;
		}
		return (EntityLocation)(object)new NullReferenceException();
	}

	private unsafe void ApplyArrowRotationWithTiers(float missDistance, bool targetChanged, bool liveUpdateTrigger)
	{
		//IL_0008: Expected O, but got Ref
		//IL_01e0: Expected F4, but got I
		//IL_01f0: Expected F4, but got I
		//IL_0205: Expected F4, but got I
		//IL_0212: Expected O, but got I4
		//IL_01b5: Expected F4, but got I4
		//IL_01be: Expected O, but got I4
		//IL_0121: Expected O, but got I
		//IL_013b: Expected O, but got I4
		//IL_0259: Expected O, but got Ref
		//IL_0342: Expected O, but got I4
		//IL_09c8: Expected O, but got I
		//IL_09e5: Expected O, but got I
		//IL_0435: Expected O, but got Ref
		//IL_036d: Expected F4, but got I
		//IL_0380: Expected O, but got Ref
		//IL_048c: Expected O, but got Ref
		//IL_0465: Expected F4, but got I4
		//IL_0aa9: Expected I, but got O
		//IL_0ad2: Expected O, but got I
		//IL_0bd9: Expected O, but got Ref
		//IL_0bfb: Expected O, but got I
		//IL_0c18: Expected O, but got I
		//IL_0532: Expected O, but got Ref
		//IL_0a6d: Expected I, but got O
		//IL_0a96: Expected O, but got I
		//IL_0a31: Expected I, but got O
		//IL_0a5a: Expected O, but got I
		//IL_0710: Expected F4, but got I
		//IL_0720: Expected F4, but got I
		//IL_0729: Expected O, but got I4
		//IL_05e1: Expected F4, but got I
		//IL_0b13: Expected I, but got O
		//IL_073b: Expected O, but got I8
		//IL_0755: Expected O, but got I8
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Expected F4, but got Unknown
		//IL_0683: Expected O, but got Ref
		//IL_0691: Expected O, but got Ref
		//IL_06fb: Expected O, but got I4
		//IL_0b3c: Expected O, but got Ref
		//IL_0b57: Expected O, but got Ref
		//IL_0948: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (!arrowRoot || !(_currentTarget != null))
		{
			return;
		}
		ImpactCorrectionTierController activeTierController = ActiveTierController;
		UnityEngine.Object obj3 = ((!activeTierController) ? null : activeTierController._003CActiveDirectionTier_003Ek__BackingField);
		if (!obj3)
		{
			_directionErrorOffsetDeg = 0f;
			_errorOffsetValid = false;
		}
		else
		{
			if (!targetChanged && _errorOffsetValid)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+2C]");
				bool flag = default(bool);
				object obj4 = (nint)0 & (nint)(flag ? 1 : 0);
				bool flag2 = obj4 == null;
				object obj5 = !flag2;
				if (obj5 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+2D]");
					bool flag3 = (nint)0 != 0;
					bool flag4 = false;
					if (!flag3)
					{
						flag4 = flag;
					}
					if (!flag4)
					{
						goto IL_0293;
					}
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+28]");
			float directionErrorOffsetDeg;
			if ((nint)0 >= (nint)0)
			{
				directionErrorOffsetDeg = 0f;
				object obj6 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+28]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				float minInclusive = num ^ 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+28]");
				float num2 = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+28]");
				directionErrorOffsetDeg = UnityEngine.Random.Range(minInclusive, 0f);
				object obj6 = 0;
			}
			_directionErrorOffsetDeg = directionErrorOffsetDeg;
			bool flag5 = !debugLogs;
			_errorOffsetValid = true;
			if (!flag5)
			{
				object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 95));
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[ImpactVisualCorrections] Rolled direction error offset: {arg:0.00}°";
				Debug.Log(message, this);
			}
		}
		goto IL_0293;
		IL_0adc:
		PointerAxis pointerAxis = this.pointerAxis;
		if (this.pointerAxis <= PointerAxis.LocalBack)
		{
			object obj8 = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ rcx_v42+5741D8+v292 @ rax_v30 (ImpactVisualCorrections+PointerAxis)*4]");
			object obj9 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v289 @ rdx_v27 (should have been resolved before IL gen)");
		}
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1201 @ rax_v32 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		Quaternion quaternion = ((!preserveInitialRotation) ? Quaternion.identityQuaternion : _initialArrowLocalRotation);
		_ = Vector3.upVector;
		Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1202 @ rcx_v32 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		Quaternion quaternion2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
		Vector3 vector2 = quaternion2 * vector;
		ref Vector3 toDirection = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
		ref Vector3 fromDirection = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
		_ = vector2.x;
		_ = vector2.z;
		Quaternion quaternion3 = Quaternion.Internal_FromToRotation(ref fromDirection, ref toDirection);
		Transform transform = arrowRoot.transform;
		object obj10 = default(object);
		float num5 = (float)obj10 * quaternion3.x;
		object obj11 = (object)quaternion * obj10;
		object obj12 = obj10 * obj10;
		float num6 = (float)obj11 + num5;
		object obj13 = obj10 * obj10;
		object obj14 = obj10 * obj10;
		float num7 = num6 + (float)obj14;
		object obj15 = obj10 * obj10;
		float num8 = num7 - (float)obj15;
		object obj16 = obj10 * obj10;
		object obj17 = obj12 + obj16;
		float num9 = (float)obj10 * quaternion3.x;
		object obj18 = (object)quaternion * obj10;
		object obj19 = obj10 * obj10;
		object obj20 = obj17 + obj18;
		float num10 = (float)obj10 * quaternion3.x;
		object obj21 = obj10 * obj10;
		float num11 = (float)obj20 - num9;
		object obj22 = obj10 * obj10;
		object obj23 = obj10 * obj10;
		object obj24 = obj13 + obj22;
		float num12 = (float)quaternion * quaternion3.x;
		object obj25 = (object)quaternion * obj10;
		float num13 = (float)obj23 - num12;
		float num14 = (float)obj24 + num10;
		float num15 = num13 - (float)obj21;
		float num16 = num14 - (float)obj25;
		float num17 = num15 - (float)obj19;
		Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 65));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-41]");
		_ = 0;
		transform.localRotation = localRotation;
		return;
		IL_0293:
		Transform transform2 = arrowRoot.transform;
		Transform parent = transform2.parent;
		bool flag6 = (object)parent != null;
		Transform transform3 = parent;
		if (!flag6)
		{
			Transform transform4 = arrowRoot.transform;
			transform3 = transform4;
		}
		Transform transform5 = base.transform;
		Vector3 position = transform5.position;
		_ = position.x;
		_ = position.x;
		bool flag7 = _missionParent != null;
		bool flag8 = !flag7;
		Vector3 vector3 = (Vector3)0;
		float z = position.z;
		if (!flag8)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ImpactVisualCorrections)+BC]");
			float num2 = 0f;
			vector3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			_ = 0;
			Vector3 vector4 = _missionParent.TransformPoint(vector3);
			z = vector4.z;
			_ = vector4.x;
		}
		bool flag9 = !useMinDirectionMagnitude;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-71]");
		nint num18 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-51]");
		object obj26 = num18 - 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-6D]");
		nint num19 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-4D]");
		object obj27 = num19 - 0;
		float num20 = z - position.z;
		float z2 = position.z;
		if (!flag9)
		{
			object obj28 = obj27 * obj27;
			object obj29 = obj26 * obj26;
			float num2 = num20 * num20;
			object obj30 = obj28 + obj29;
			z2 = minDirectionSqrMagnitude;
			float num21 = (float)obj30 + num2;
			if (minDirectionSqrMagnitude > num21)
			{
				return;
			}
		}
		object obj31 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (!(z2 > 1E-05f))
		{
			float num22 = 0f;
		}
		else
		{
			float num22 = num20 / z2;
		}
		Vector3 direction = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
		Vector3 vector5 = transform3.InverseTransformDirection(direction);
		_ = vector5.x;
		bool flag10 = rotationAxis == RotationAxis.LocalX;
		object obj32;
		if (!flag10)
		{
			if (!flag10)
			{
				nint num23 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1009 @ rax_v53 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num24 = 0;
				Vector3 forwardVector = Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1010 @ rcx_v54 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				obj32 = 0;
			}
			else
			{
				nint num25 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1017 @ rax_v50 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num26 = 0;
				Vector3 forwardVector = Vector3.upVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1018 @ rcx_v51 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
				obj32 = 0;
			}
		}
		else
		{
			nint num27 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1001 @ rax_v47 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num28 = 0;
			Vector3 forwardVector = Vector3.rightVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1002 @ rcx_v47 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			obj32 = 0;
		}
		object obj33 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-4D]");
		nint num29 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-6D]");
		object obj34 = num29 * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-51]");
		nint num30 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-71]");
		object obj35 = num30 * 0;
		object obj36 = obj35 + obj34;
		float num31 = vector5.z * (float)obj32;
		float num32 = (float)obj36 + num31;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-71]");
		float num33 = 0f * num32;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-6D]");
		float num34 = 0f * num32;
		float num35 = (float)obj32 * num32;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-51]");
		float num36 = 0f - num33;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-4D]");
		float num37 = 0f - num34;
		float num38 = vector5.z - num35;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (useMinDirectionMagnitude)
		{
			num31 = minDirectionSqrMagnitude;
			num35 *= num35;
			if (minDirectionSqrMagnitude > num35)
			{
				return;
			}
		}
		object obj37 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (!(num35 > 1E-05f))
		{
			_ = 0;
			_ = 0;
		}
		else
		{
			float num39 = num36 / num35;
			float num40 = num37 / num35;
			float num41 = num38 / num35;
		}
		float z3;
		float x;
		object obj38;
		if ((bool)obj3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+28]");
			num35 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdi_v6 (UnityEngine.Object)+28]");
			if ((nint)0 > (nint)0)
			{
				float directionErrorOffsetDeg2 = _directionErrorOffsetDeg;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				num35 = directionErrorOffsetDeg2 & 0;
				if (num35 > 0.001f)
				{
					num31 = _directionErrorOffsetDeg;
					Quaternion quaternion4 = Quaternion.Internal_AngleAxis(_directionErrorOffsetDeg, ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81)));
					direction = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
					Quaternion quaternion5 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-71]");
					_ = 0;
					num35 = quaternion4.x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-69]");
					_ = 0;
					_ = quaternion4.x;
					Vector3 vector6 = quaternion5 * direction;
					x = vector6.x;
					z3 = vector6.z;
					obj38 = 0;
					goto IL_0adc;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-69]");
		z3 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-71]");
		x = 0f;
		obj38 = 0;
		goto IL_0adc;
	}

	private unsafe Vector3 GetAxisVector(RotationAxis axis)
	{
		//IL_00d4: Expected I, but got O
		//IL_00f2: Expected F4, but got O
		//IL_00ed: Expected native int or pointer, but got O
		//IL_0107: Expected F4, but got I
		//IL_0102: Expected native int or pointer, but got O
		//IL_008e: Expected I, but got O
		//IL_00ac: Expected F4, but got O
		//IL_00a7: Expected native int or pointer, but got O
		//IL_00c1: Expected F4, but got I
		//IL_00bc: Expected native int or pointer, but got O
		//IL_0048: Expected I, but got O
		//IL_0066: Expected F4, but got O
		//IL_0061: Expected native int or pointer, but got O
		//IL_007b: Expected F4, but got I
		//IL_0076: Expected native int or pointer, but got O
		bool flag = axis == RotationAxis.LocalX;
		Vector3 vector = default(Vector3);
		if (!flag)
		{
			if (!flag)
			{
				nint num = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rax_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num2 = 0;
				((Vector3*)(nint)vector)->x = (float)Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			((Vector3*)(nint)vector)->x = (float)Vector3.upVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rax_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		nint num5 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num6 = 0;
		((Vector3*)(nint)vector)->x = (float)Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	private Vector3 GetPointerAxisVector(PointerAxis axis)
	{
		//IL_000f: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 17 Invalid \"Jump target not found in method: 0x180574C63\"");
		return (Vector3)axis;
	}

	private void ApplyDistanceDisplay(float missDistance)
	{
		if (!rangeText)
		{
			return;
		}
		ImpactCorrectionTierController activeTierController = ActiveTierController;
		if ((bool)activeTierController && (bool)activeTierController._003CActiveDistanceTier_003Ek__BackingField)
		{
			string text = activeTierController._003CActiveDistanceTier_003Ek__BackingField.FormatDistance(missDistance);
			rangeText.text = text;
			return;
		}
		string text2;
		if (!string.IsNullOrEmpty(rangeFormat))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			text2 = string.Format(rangeFormat, arg);
		}
		else
		{
			float num = default(float);
			text2 = num.ToString("0");
			if ((object)rangeText == null)
			{
				throw new NullReferenceException();
			}
		}
		rangeText.text = text2;
	}

	private unsafe void DestroyAndNull(ref GameObject go)
	{
		if ((bool)go)
		{
			ref GameObject reference = ref *(GameObject*)null;
			UnityEngine.Object.Destroy(go);
		}
	}

	private unsafe void DestroyAndNullTMP(ref TMP_Text tmp)
	{
		if ((bool)tmp)
		{
			GameObject obj = tmp.gameObject;
			ref TMP_Text reference = ref *(TMP_Text*)null;
			UnityEngine.Object.Destroy(obj);
		}
	}

	public ImpactVisualCorrections()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3ABE8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		suppressWhenScoutingStripsBlocked = true;
		rangeFormat = "{0:0}";
		specificTargetID = "Target1";
		evaluationFrameDelay = 1;
		autoResolveReferences = true;
		rotationAxis = RotationAxis.LocalZ;
		preserveInitialRotation = true;
		minDirectionSqrMagnitude = 1E-06f;
		attemptAutoLocateTierController = true;
		base._002Ector();
	}
}
